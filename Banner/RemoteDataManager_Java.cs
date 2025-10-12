using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;
using ctre_wp7.iframework.core;
using ctre_wp7.ios;
using ctre_wp7.utils;

namespace ctre_wp7.Banner
{
	// Token: 0x02000041 RID: 65
	public class RemoteDataManager_Java
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000E35C File Offset: 0x0000C55C
		private void CheckCleanup()
		{
			string text = Preferences._getStringForKey("lastVersionLaunched");
			string fullName = Assembly.GetExecutingAssembly().FullName;
			string text2 = fullName.Split(new char[] { '=' })[1].Split(new char[] { ',' })[0];
			if (text2 != text)
			{
				using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
				{
					string[] fileNames = userStoreForApplication.GetFileNames();
					foreach (string text3 in fileNames)
					{
						if (text3.StartsWith(this.storedBannersPrefix) || text3.StartsWith(this.storedConfigPrefix) || text3.StartsWith(this.bannerPrefix))
						{
							userStoreForApplication.DeleteFile(text3);
						}
					}
				}
				Preferences._setStringforKey(text2, "lastVersionLaunched", true);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E444 File Offset: 0x0000C644
		public void initWith(string app, string platform, int pSet, int pWidth, int pHeight)
		{
			if (this.execution)
			{
				return;
			}
			this.CheckCleanup();
			this.set = pSet;
			this.width = pWidth;
			this.height = pHeight;
			this.banners = this.getStoredBanners();
			this.config = this.getStoredConfig();
			if (this.config == null)
			{
				this.config = new RemoteConfig("", "");
			}
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, Banner> keyValuePair in this.banners)
			{
				if (this.isValid(keyValuePair.Value))
				{
					list.Add(keyValuePair.Key);
				}
			}
			DeviceParams deviceParams = new DeviceParams();
			int minor = Environment.OSVersion.Version.Minor;
			string osversion = SystemInfo.getOSVersion();
			string appVersion = SystemInfo.getAppVersion();
			string text = deviceParams.getTimeZoneOffset().ToString();
			SystemInfo.getNetworkType().ToString();
			string phoneModel = SystemInfo.getPhoneModel();
			string text2 = Application.sharedAppSettings().getString(8).ToString();
			string appMarket = SystemInfo.getAppMarket();
			string text3 = string.Format("{0}&app={1}&platform={2}&d=480x300&set={3}&w={4}&h={5}&fv={6}&osversion={7}&version={8}&sisterapps=&model={9}&tz={10}&locale={11}&store={12}", new object[]
			{
				RemoteDataManager_Java.BANNER_SERVER_URL,
				app,
				platform,
				this.set,
				this.width,
				this.height,
				RemoteDataManager_Java.FORMAT_VERSION,
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
				text3 += string.Join<int>(",", list);
			}
			text3 += this.getAdditionalParameters();
			this.RequestDataTask_execute(text3);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E63C File Offset: 0x0000C83C
		public string getAdditionalParameters()
		{
			return "";
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E643 File Offset: 0x0000C843
		protected void RequestDataTask_execute(string url)
		{
			XMLNode.parseXML_URL(url, this);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E64C File Offset: 0x0000C84C
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
						this.bannersprocessing = true;
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
						this.config = new RemoteConfig(text2, text3);
						List<XMLNode> list = doc.getElementsByTagName("promobanner");
						if (list.Count == 0)
						{
							list = doc.getElementsByTagName("banner");
						}
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							Banner banner = new Banner(list[i], this.width, this.height);
							this.banners.Add(banner.id, banner);
							i++;
						}
						this.bannersprocessing = false;
					}
					this.config.setHideMainPromo(text == "hide");
					if (doc.getElementsByTagName("interstitialbannersperiod").Count > 0)
					{
						this.config.setInterstitialBannersPeriod(doc.getElementsByTagName("interstitialbannersperiod")[0]["value"].intValue());
					}
					if (doc.getElementsByTagName("changeinterstitialtovideoperiod").Count > 0)
					{
						this.config.setChangeInterstitialToVideoPeriod(doc.getElementsByTagName("changeinterstitialtovideoperiod")[0]["value"].intValue());
					}
					if (doc.getElementsByTagName("videobannerscount").Count > 0)
					{
						this.config.setVideoBannersCount(doc.getElementsByTagName("videobannerscount")[0]["value"].intValue());
					}
					if (doc.getElementsByTagName("hidesocialnetworks").Count > 0)
					{
						this.config.setHideSocialNetworks(doc.getElementsByTagName("hidesocialnetworks")[0]["value"].boolValue());
					}
					this.config.setHideSocialNetworks(true);
					if (doc.getElementsByTagName("defaultinterstitial").Count > 0)
					{
						this.config.setDefaultInterstitial(doc.getElementsByTagName("defaultinterstitial")[0]["value"].boolValue());
					}
					if (doc.getElementsByTagName("boxforcrosspromo").Count > 0)
					{
						this.config.setBoxForCrossPromo(doc.getElementsByTagName("boxforcrosspromo")[0]["value"].intValue());
					}
					this.SaveStoredConfig(this.config);
					this.SaveStoredBanners(this.banners);
					if (this.config != null)
					{
						this.config.iterateBanner();
					}
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public void SaveStoredBanners(Dictionary<int, Banner> banners)
		{
			using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.getStoredBannersPath(), 2, userStoreForApplication))
				{
					BinaryWriter binaryWriter = new BinaryWriter(isolatedStorageFileStream);
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

		// Token: 0x06000227 RID: 551 RVA: 0x0000EADC File Offset: 0x0000CCDC
		public void SaveStoredConfig(RemoteConfig config)
		{
			using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.getStoredConfigPath(), 2, userStoreForApplication))
				{
					BinaryWriter binaryWriter = new BinaryWriter(isolatedStorageFileStream);
					config.SaveConfig(binaryWriter);
					binaryWriter.Close();
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public Dictionary<int, Banner> getStoredBanners()
		{
			try
			{
				using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (userStoreForApplication.FileExists(this.getStoredBannersPath()))
					{
						using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.getStoredBannersPath(), 3, userStoreForApplication))
						{
							BinaryReader binaryReader = new BinaryReader(isolatedStorageFileStream);
							int num = binaryReader.ReadInt32();
							Dictionary<int, Banner> dictionary = new Dictionary<int, Banner>();
							for (int i = 0; i < num; i++)
							{
								int num2 = binaryReader.ReadInt32();
								Banner banner = new Banner(binaryReader);
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
			return new Dictionary<int, Banner>();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public RemoteConfig getStoredConfig()
		{
			try
			{
				using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (userStoreForApplication.FileExists(this.getStoredBannersPath()))
					{
						using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.getStoredConfigPath(), 3, userStoreForApplication))
						{
							BinaryReader binaryReader = new BinaryReader(isolatedStorageFileStream);
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

		// Token: 0x0600022A RID: 554 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public bool hasSenseToRotateBanners()
		{
			if (this.config != null)
			{
				return this.config.hasSenseToRotateBanners();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public bool getHideMainPromo()
		{
			if (this.config != null)
			{
				return this.config.getHideMainPromo();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public bool getHideSocialNetworks()
		{
			if (this.config != null)
			{
				return this.config.getHideSocialNetworks();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public int getInterstitialBannersPeriod()
		{
			if (this.config != null)
			{
				return this.config.getInterstitialBannersPeriod();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public int getChangeInterstitialToVideoPeriod()
		{
			if (this.config != null)
			{
				return this.config.getChangeInterstitialToVideoPeriod();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public bool getDefaultInterstitial()
		{
			if (this.config != null)
			{
				return this.config.getDefaultInterstitial();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public int getVideoBannersCount()
		{
			if (this.config != null)
			{
				return this.config.getVideoBannersCount();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public int getBoxForCrossPromo()
		{
			if (this.config != null)
			{
				return this.config.getBoxForCrossPromo();
			}
			throw new NullReferenceException("config is null");
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000ED9C File Offset: 0x0000CF9C
		public bool isValid(Banner banner)
		{
			return banner != null && banner.saved;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		public string getStoredBannersPath()
		{
			return string.Format(this.storedBannersPrefix + "_{0}_{1}", this.width, this.height);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		public string getStoredConfigPath()
		{
			return string.Format(this.storedConfigPrefix + "_{0}_{1}_{2}", this.set, this.width, this.height);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000EE14 File Offset: 0x0000D014
		public Banner getBanner()
		{
			if (this.banners != null && this.config != null && !this.bannersprocessing)
			{
				int currentBannerID = this.config.getCurrentBannerID();
				Banner banner;
				if (!this.banners.TryGetValue(currentBannerID, ref banner))
				{
					return null;
				}
				if (this.isValid(banner))
				{
					return banner;
				}
				this.banners.Remove(currentBannerID);
				this.config.removeBanner(currentBannerID);
			}
			return null;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public void nextBanner()
		{
			if (this.banners != null && this.config != null && !this.bannersprocessing)
			{
				this.config.nextBanner();
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000EEA3 File Offset: 0x0000D0A3
		public void prevBanner()
		{
			if (this.banners != null && this.config != null && !this.bannersprocessing)
			{
				this.config.prevBanner();
			}
		}

		// Token: 0x0400081D RID: 2077
		protected static string FORMAT_VERSION = "1";

		// Token: 0x0400081E RID: 2078
		protected static string BANNER_SERVER_URL = "http://bms.zeptolab.com/feeder/csp?";

		// Token: 0x0400081F RID: 2079
		protected string TAG = "RemoteDataManager";

		// Token: 0x04000820 RID: 2080
		private int set;

		// Token: 0x04000821 RID: 2081
		private int width;

		// Token: 0x04000822 RID: 2082
		private int height;

		// Token: 0x04000823 RID: 2083
		protected RemoteConfig config;

		// Token: 0x04000824 RID: 2084
		protected Dictionary<int, Banner> banners;

		// Token: 0x04000825 RID: 2085
		protected bool execution;

		// Token: 0x04000826 RID: 2086
		protected bool bannersprocessing;

		// Token: 0x04000827 RID: 2087
		protected string bannerPrefix = "banner";

		// Token: 0x04000828 RID: 2088
		protected string storedBannersPrefix = "storedBanners";

		// Token: 0x04000829 RID: 2089
		protected string storedConfigPrefix = "storedConfig";
	}
}
