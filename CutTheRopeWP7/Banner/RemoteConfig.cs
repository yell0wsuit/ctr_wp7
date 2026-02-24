using System.Collections.Generic;
using System.IO;

namespace ctr_wp7.Banner
{
    public class RemoteConfig
    {
        public void SaveConfig(BinaryWriter file)
        {
            file.Write(currentBanner);
            file.Write(currentWeight);
            file.Write(totalBanners);
            file.Write(hideMainPromo);
            file.Write(hideSocialNetworks);
            file.Write(interstitialBannersPeriod);
            file.Write(changeInterstitialToVideoPeriod);
            file.Write(defaultInterstitial);
            file.Write(boxForCrossPromo);
            file.Write(videoBannersCount);
            file.Write(bannersList.Count);
            foreach (int num in bannersList)
            {
                file.Write(num);
            }
            file.Write(bannersWeights.Count);
            foreach (int num2 in bannersWeights)
            {
                file.Write(num2);
            }
        }

        public RemoteConfig(BinaryReader file)
        {
            currentBanner = file.ReadInt32();
            currentWeight = file.ReadInt32();
            totalBanners = file.ReadInt32();
            hideMainPromo = file.ReadBoolean();
            hideSocialNetworks = file.ReadBoolean();
            interstitialBannersPeriod = file.ReadInt32();
            changeInterstitialToVideoPeriod = file.ReadInt32();
            defaultInterstitial = file.ReadBoolean();
            boxForCrossPromo = file.ReadInt32();
            videoBannersCount = file.ReadInt32();
            int num = file.ReadInt32();
            bannersList = [];
            for (int i = 0; i < num; i++)
            {
                int num2 = file.ReadInt32();
                bannersList.Add(num2);
            }
            num = file.ReadInt32();
            bannersWeights = [];
            for (int j = 0; j < num; j++)
            {
                int num3 = file.ReadInt32();
                bannersWeights.Add(num3);
            }
        }

        public RemoteConfig(string pList, string pWeight)
        {
            bannersList = (pList.Length > 0) ? convertArray(pList.Split([','])) : [];
            bannersWeights = (pWeight.Length > 0) ? convertArray(pWeight.Split([','])) : [];
            currentBanner = 0;
            currentWeight = 0;
            hideMainPromo = false;
            totalBanners = bannersList.Count;
        }

        public bool getHideMainPromo()
        {
            return hideMainPromo;
        }

        public void setHideMainPromo(bool value)
        {
            hideMainPromo = value;
        }

        public int getInterstitialBannersPeriod()
        {
            return interstitialBannersPeriod;
        }

        public void setInterstitialBannersPeriod(int value)
        {
            interstitialBannersPeriod = value;
        }

        public int getChangeInterstitialToVideoPeriod()
        {
            return changeInterstitialToVideoPeriod;
        }

        public void setChangeInterstitialToVideoPeriod(int value)
        {
            changeInterstitialToVideoPeriod = value;
        }

        public bool getHideSocialNetworks()
        {
            return hideSocialNetworks;
        }

        public void setHideSocialNetworks(bool value)
        {
            hideSocialNetworks = value;
        }

        public bool getDefaultInterstitial()
        {
            return defaultInterstitial;
        }

        public void setDefaultInterstitial(bool value)
        {
            defaultInterstitial = value;
        }

        public int getBoxForCrossPromo()
        {
            return boxForCrossPromo;
        }

        public void setBoxForCrossPromo(int value)
        {
            boxForCrossPromo = value;
        }

        public int getVideoBannersCount()
        {
            return videoBannersCount;
        }

        public void setVideoBannersCount(int value)
        {
            videoBannersCount = value;
        }

        protected static List<int> convertArray(string[] arr)
        {
            List<int> list = [arr.Length];
            int num = 0;
            foreach (string text in arr)
            {
                list.Insert(num, int.Parse(text));
                num++;
            }
            return list;
        }

        public void iterateBanner()
        {
            if (totalBanners == 0)
            {
                return;
            }
            if (currentBanner >= totalBanners)
            {
                currentBanner = 0;
                currentWeight = 0;
            }
            int num = 0;
            foreach (int num2 in bannersWeights)
            {
                num += num2;
            }
            if (num <= 0)
            {
                currentBanner = int.MaxValue;
                return;
            }
            int num3;
            do
            {
                num3 = bannersWeights[currentBanner];
                if (currentWeight >= num3)
                {
                    currentWeight = 0;
                    currentBanner++;
                    currentBanner %= totalBanners;
                }
            }
            while (num3 == 0);
            currentWeight++;
        }

        public bool hasSenseToRotateBanners()
        {
            if (totalBanners == 0)
            {
                return false;
            }
            currentWeight = 0;
            int num = 0;
            foreach (int num2 in bannersWeights)
            {
                if (num2 > 0)
                {
                    num++;
                }
            }
            return num != 0;
        }

        public void nextBanner()
        {
            if (hasSenseToRotateBanners())
            {
                do
                {
                    currentBanner++;
                    currentBanner %= totalBanners;
                }
                while (bannersWeights[currentBanner] <= 0);
            }
        }

        public void prevBanner()
        {
            if (hasSenseToRotateBanners())
            {
                do
                {
                    currentBanner--;
                    if (currentBanner < 0)
                    {
                        currentBanner = totalBanners - 1;
                    }
                }
                while (bannersWeights[currentBanner] <= 0);
            }
        }

        public int getCurrentBannerID()
        {
            return totalBanners > 0 ? bannersList[currentBanner] : -1;
        }

        public void removeBanner(int id)
        {
            int num = bannersList.IndexOf(id);
            if (num != -1)
            {
                bannersList.RemoveAt(num);
                bannersWeights.RemoveAt(num);
                currentBanner = 0;
                totalBanners = bannersList.Count;
            }
        }

        private static readonly long serialVersionUID = 1L;

        private readonly List<int> bannersList;

        private readonly List<int> bannersWeights;

        private int currentBanner;

        private int currentWeight;

        private int totalBanners;

        private bool hideMainPromo;

        private bool hideSocialNetworks;

        private int interstitialBannersPeriod = 2;

        private int changeInterstitialToVideoPeriod = 2;

        private bool defaultInterstitial;

        private int boxForCrossPromo = -1;

        private int videoBannersCount = 1;
    }
}
