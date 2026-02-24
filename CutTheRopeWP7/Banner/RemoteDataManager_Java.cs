using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;
using ctr_wp7.utils;

namespace ctr_wp7.Banner
{
    public class RemoteDataManager_Java
    {
        private void CheckCleanup()
        {
            string text = Preferences._getStringForKey("lastVersionLaunched");
            string fullName = Assembly.GetExecutingAssembly().FullName;
            string text2 = fullName.Split(['='])[1].Split([','])[0];
            if (text2 != text)
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    string[] fileNames = userStoreForApplication.GetFileNames();
                    foreach (string text3 in fileNames)
                    {
                        if (text3.StartsWith(storedBannersPrefix) || text3.StartsWith(storedConfigPrefix) || text3.StartsWith(bannerPrefix))
                        {
                            userStoreForApplication.DeleteFile(text3);
                        }
                    }
                }
                Preferences._setStringforKey(text2, "lastVersionLaunched", true);
            }
        }

        public void initWith(string app, string platform, int pSet, int pWidth, int pHeight)
        {
            if (execution)
            {
                return;
            }
            CheckCleanup();
            set = pSet;
            width = pWidth;
            height = pHeight;
            banners = getStoredBanners();
            config = getStoredConfig();
            config ??= new RemoteConfig("", "");
            List<int> list = [];
            foreach (KeyValuePair<int, Banner> keyValuePair in banners)
            {
                if (isValid(keyValuePair.Value))
                {
                    list.Add(keyValuePair.Key);
                }
            }

            _ = new DeviceParams();
            _ = Environment.OSVersion.Version.Minor;
            string osversion = SystemInfo.getOSVersion();
            string appVersion = SystemInfo.getAppVersion();
            string text = DeviceParams.getTimeZoneOffset().ToString();
            _ = SystemInfo.getNetworkType().ToString();
            string phoneModel = SystemInfo.getPhoneModel();
            string text2 = ApplicationSettings.getString(8).ToString();
            string appMarket = SystemInfo.getAppMarket();
            string text3 = string.Format("{0}&app={1}&platform={2}&d=480x300&set={3}&w={4}&h={5}&fv={6}&osversion={7}&version={8}&sisterapps=&model={9}&tz={10}&locale={11}&store={12}", new object[]
            {
                BANNER_SERVER_URL,
                app,
                platform,
                set,
                width,
                height,
                FORMAT_VERSION,
                osversion,
                appVersion,
                phoneModel,
                text,
                text2,
                appMarket
            });
            if (list.Count > 0)
            {
                text3 += "&existing=";
                text3 += string.Join(",", list);
            }
            text3 += getAdditionalParameters();
            RequestDataTask_execute(text3);
        }

        public static string getAdditionalParameters()
        {
            return "";
        }

        protected void RequestDataTask_execute(string url)
        {
            XMLNode.parseXML_URL(url, this);
        }

        internal bool XMLDownloadFinished(XMLNode doc)
        {
            try
            {
                if (doc != null)
                {
                    string text = "";
                    if (doc.findChildWithTagNameRecursively("promoaction", false) != null)
                    {
                        text = doc.findChildWithTagNameRecursively("promoaction", false).data.ToString();
                    }
                    else if (doc.findChildWithTagNameRecursively("response", false) != null)
                    {
                        text = doc.findChildWithTagNameRecursively("response", false).data.ToString();
                    }
                    XMLNode xmlnode = doc.findChildWithTagNameRecursively("hide-coppa-popup", false);
                    if (xmlnode != null)
                    {
                        CoppaLoader.setHideCoppaPopupIsExplicit(true);
                    }
                    if (xmlnode != null)
                    {
                        NSString nsstring = xmlnode["value"];
                        if (nsstring != null)
                        {
                            nsstring = xmlnode.data;
                        }
                        if (nsstring != null)
                        {
                            CoppaLoader.setHideCoppaPopup(nsstring.boolValue());
                        }
                    }
                    if (text == "change")
                    {
                        bannersprocessing = true;
                        string text2 = "";
                        if (doc.findChildWithTagNameRecursively("promolist", false) != null)
                        {
                            text2 = doc.findChildWithTagNameRecursively("promolist", false).data.ToString();
                        }
                        else if (doc.findChildWithTagNameRecursively("bannerslist", false) != null)
                        {
                            text2 = doc.findChildWithTagNameRecursively("bannerslist", false).data.ToString();
                        }
                        string text3 = "";
                        if (doc.findChildWithTagNameRecursively("promoweights", false) != null)
                        {
                            text3 = doc.findChildWithTagNameRecursively("promoweights", false).data.ToString();
                        }
                        else if (doc.findChildWithTagNameRecursively("bannersweight", false) != null)
                        {
                            text3 = doc.findChildWithTagNameRecursively("bannersweight", false).data.ToString();
                        }
                        config = new RemoteConfig(text2, text3);
                        List<XMLNode> list = doc.getElementsByTagName("promobanner");
                        if (list.Count == 0)
                        {
                            list = doc.getElementsByTagName("banner");
                        }
                        int i = 0;
                        int count = list.Count;
                        while (i < count)
                        {
                            Banner banner = new(list[i], width, height);
                            banners.Add(banner.id, banner);
                            i++;
                        }
                        bannersprocessing = false;
                    }
                    config.setHideMainPromo(text == "hide");
                    if (doc.getElementsByTagName("interstitialbannersperiod").Count > 0)
                    {
                        config.setInterstitialBannersPeriod(doc.getElementsByTagName("interstitialbannersperiod")[0]["value"].intValue());
                    }
                    if (doc.getElementsByTagName("changeinterstitialtovideoperiod").Count > 0)
                    {
                        config.setChangeInterstitialToVideoPeriod(doc.getElementsByTagName("changeinterstitialtovideoperiod")[0]["value"].intValue());
                    }
                    if (doc.getElementsByTagName("videobannerscount").Count > 0)
                    {
                        config.setVideoBannersCount(doc.getElementsByTagName("videobannerscount")[0]["value"].intValue());
                    }
                    if (doc.getElementsByTagName("hidesocialnetworks").Count > 0)
                    {
                        config.setHideSocialNetworks(doc.getElementsByTagName("hidesocialnetworks")[0]["value"].boolValue());
                    }
                    config.setHideSocialNetworks(true);
                    if (doc.getElementsByTagName("defaultinterstitial").Count > 0)
                    {
                        config.setDefaultInterstitial(doc.getElementsByTagName("defaultinterstitial")[0]["value"].boolValue());
                    }
                    if (doc.getElementsByTagName("boxforcrosspromo").Count > 0)
                    {
                        config.setBoxForCrossPromo(doc.getElementsByTagName("boxforcrosspromo")[0]["value"].intValue());
                    }
                    SaveStoredConfig(config);
                    SaveStoredBanners(banners);
                    config?.iterateBanner();
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public void SaveStoredBanners(Dictionary<int, Banner> banners)
        {
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isolatedStorageFileStream = new(getStoredBannersPath(), FileMode.Create, userStoreForApplication))
                {
                    BinaryWriter binaryWriter = new(isolatedStorageFileStream);
                    binaryWriter.Write(banners.Count);
                    foreach (KeyValuePair<int, Banner> keyValuePair in banners)
                    {
                        binaryWriter.Write(keyValuePair.Key);
                        keyValuePair.Value.SaveToFile(binaryWriter);
                    }
                    binaryWriter.Close();
                }
            }
        }

        public void SaveStoredConfig(RemoteConfig config)
        {
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isolatedStorageFileStream = new(getStoredConfigPath(), FileMode.Create, userStoreForApplication))
                {
                    BinaryWriter binaryWriter = new(isolatedStorageFileStream);
                    config.SaveConfig(binaryWriter);
                    binaryWriter.Close();
                }
            }
        }

        public Dictionary<int, Banner> getStoredBanners()
        {
            try
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (userStoreForApplication.FileExists(getStoredBannersPath()))
                    {
                        using (IsolatedStorageFileStream isolatedStorageFileStream = new(getStoredBannersPath(), FileMode.Open, userStoreForApplication))
                        {
                            BinaryReader binaryReader = new(isolatedStorageFileStream);
                            int num = binaryReader.ReadInt32();
                            Dictionary<int, Banner> dictionary = [];
                            for (int i = 0; i < num; i++)
                            {
                                int num2 = binaryReader.ReadInt32();
                                Banner banner = new(binaryReader);
                                dictionary.Add(num2, banner);
                            }
                            binaryReader.Close();
                            return dictionary;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return [];
        }

        public RemoteConfig getStoredConfig()
        {
            try
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (userStoreForApplication.FileExists(getStoredBannersPath()))
                    {
                        using (IsolatedStorageFileStream isolatedStorageFileStream = new(getStoredConfigPath(), FileMode.Open, userStoreForApplication))
                        {
                            BinaryReader binaryReader = new(isolatedStorageFileStream);
                            return new RemoteConfig(binaryReader);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public bool hasSenseToRotateBanners()
        {
            return config != null ? config.hasSenseToRotateBanners() : throw new NullReferenceException("config is null");
        }

        public bool getHideMainPromo()
        {
            return config != null ? config.getHideMainPromo() : throw new NullReferenceException("config is null");
        }

        public bool getHideSocialNetworks()
        {
            return config != null ? config.getHideSocialNetworks() : throw new NullReferenceException("config is null");
        }

        public int getInterstitialBannersPeriod()
        {
            return config != null ? config.getInterstitialBannersPeriod() : throw new NullReferenceException("config is null");
        }

        public int getChangeInterstitialToVideoPeriod()
        {
            return config != null ? config.getChangeInterstitialToVideoPeriod() : throw new NullReferenceException("config is null");
        }

        public bool getDefaultInterstitial()
        {
            return config != null ? config.getDefaultInterstitial() : throw new NullReferenceException("config is null");
        }

        public int getVideoBannersCount()
        {
            return config != null ? config.getVideoBannersCount() : throw new NullReferenceException("config is null");
        }

        public int getBoxForCrossPromo()
        {
            return config != null ? config.getBoxForCrossPromo() : throw new NullReferenceException("config is null");
        }

        public static bool isValid(Banner banner)
        {
            return banner != null && banner.saved;
        }

        public string getStoredBannersPath()
        {
            return string.Format(storedBannersPrefix + "_{0}_{1}", width, height);
        }

        public string getStoredConfigPath()
        {
            return string.Format(storedConfigPrefix + "_{0}_{1}_{2}", set, width, height);
        }

        public Banner getBanner()
        {
            if (banners != null && config != null && !bannersprocessing)
            {
                int currentBannerID = config.getCurrentBannerID();
                if (!banners.TryGetValue(currentBannerID, out Banner banner))
                {
                    return null;
                }
                if (isValid(banner))
                {
                    return banner;
                }
                _ = banners.Remove(currentBannerID);
                config.removeBanner(currentBannerID);
            }
            return null;
        }

        public void nextBanner()
        {
            if (banners != null && config != null && !bannersprocessing)
            {
                config.nextBanner();
            }
        }

        public void prevBanner()
        {
            if (banners != null && config != null && !bannersprocessing)
            {
                config.prevBanner();
            }
        }

        protected static string FORMAT_VERSION = "1";

        protected static string BANNER_SERVER_URL = "http://bms.zeptolab.com/feeder/csp?";

        protected string TAG = "RemoteDataManager";

        private int set;

        private int width;

        private int height;

        protected RemoteConfig config;

        protected Dictionary<int, Banner> banners;

        protected bool execution;

        protected bool bannersprocessing;

        protected string bannerPrefix = "banner";

        protected string storedBannersPrefix = "storedBanners";

        protected string storedConfigPrefix = "storedConfig";
    }
}
