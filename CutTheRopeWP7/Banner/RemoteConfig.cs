using System.Collections.Generic;
using System.IO;

namespace ctr_wp7.Banner
{
    // Token: 0x020000F3 RID: 243
    public class RemoteConfig
    {
        // Token: 0x06000749 RID: 1865 RVA: 0x0003AB74 File Offset: 0x00038D74
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

        // Token: 0x0600074A RID: 1866 RVA: 0x0003ACA4 File Offset: 0x00038EA4
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

        // Token: 0x0600074B RID: 1867 RVA: 0x0003ADB0 File Offset: 0x00038FB0
        public RemoteConfig(string pList, string pWeight)
        {
            bannersList = (pList.Length > 0) ? convertArray(pList.Split([','])) : [];
            bannersWeights = (pWeight.Length > 0) ? convertArray(pWeight.Split([','])) : [];
            currentBanner = 0;
            currentWeight = 0;
            hideMainPromo = false;
            totalBanners = bannersList.Count;
        }

        // Token: 0x0600074C RID: 1868 RVA: 0x0003AE63 File Offset: 0x00039063
        public bool getHideMainPromo()
        {
            return hideMainPromo;
        }

        // Token: 0x0600074D RID: 1869 RVA: 0x0003AE6B File Offset: 0x0003906B
        public void setHideMainPromo(bool value)
        {
            hideMainPromo = value;
        }

        // Token: 0x0600074E RID: 1870 RVA: 0x0003AE74 File Offset: 0x00039074
        public int getInterstitialBannersPeriod()
        {
            return interstitialBannersPeriod;
        }

        // Token: 0x0600074F RID: 1871 RVA: 0x0003AE7C File Offset: 0x0003907C
        public void setInterstitialBannersPeriod(int value)
        {
            interstitialBannersPeriod = value;
        }

        // Token: 0x06000750 RID: 1872 RVA: 0x0003AE85 File Offset: 0x00039085
        public int getChangeInterstitialToVideoPeriod()
        {
            return changeInterstitialToVideoPeriod;
        }

        // Token: 0x06000751 RID: 1873 RVA: 0x0003AE8D File Offset: 0x0003908D
        public void setChangeInterstitialToVideoPeriod(int value)
        {
            changeInterstitialToVideoPeriod = value;
        }

        // Token: 0x06000752 RID: 1874 RVA: 0x0003AE96 File Offset: 0x00039096
        public bool getHideSocialNetworks()
        {
            return hideSocialNetworks;
        }

        // Token: 0x06000753 RID: 1875 RVA: 0x0003AE9E File Offset: 0x0003909E
        public void setHideSocialNetworks(bool value)
        {
            hideSocialNetworks = value;
        }

        // Token: 0x06000754 RID: 1876 RVA: 0x0003AEA7 File Offset: 0x000390A7
        public bool getDefaultInterstitial()
        {
            return defaultInterstitial;
        }

        // Token: 0x06000755 RID: 1877 RVA: 0x0003AEAF File Offset: 0x000390AF
        public void setDefaultInterstitial(bool value)
        {
            defaultInterstitial = value;
        }

        // Token: 0x06000756 RID: 1878 RVA: 0x0003AEB8 File Offset: 0x000390B8
        public int getBoxForCrossPromo()
        {
            return boxForCrossPromo;
        }

        // Token: 0x06000757 RID: 1879 RVA: 0x0003AEC0 File Offset: 0x000390C0
        public void setBoxForCrossPromo(int value)
        {
            boxForCrossPromo = value;
        }

        // Token: 0x06000758 RID: 1880 RVA: 0x0003AEC9 File Offset: 0x000390C9
        public int getVideoBannersCount()
        {
            return videoBannersCount;
        }

        // Token: 0x06000759 RID: 1881 RVA: 0x0003AED1 File Offset: 0x000390D1
        public void setVideoBannersCount(int value)
        {
            videoBannersCount = value;
        }

        // Token: 0x0600075A RID: 1882 RVA: 0x0003AEDC File Offset: 0x000390DC
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

        // Token: 0x0600075B RID: 1883 RVA: 0x0003AF20 File Offset: 0x00039120
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

        // Token: 0x0600075C RID: 1884 RVA: 0x0003B000 File Offset: 0x00039200
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

        // Token: 0x0600075D RID: 1885 RVA: 0x0003B070 File Offset: 0x00039270
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

        // Token: 0x0600075E RID: 1886 RVA: 0x0003B0B0 File Offset: 0x000392B0
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

        // Token: 0x0600075F RID: 1887 RVA: 0x0003B0FE File Offset: 0x000392FE
        public int getCurrentBannerID()
        {
            return totalBanners > 0 ? bannersList[currentBanner] : -1;
        }

        // Token: 0x06000760 RID: 1888 RVA: 0x0003B11C File Offset: 0x0003931C
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

        // Token: 0x04000CD9 RID: 3289
        private static long serialVersionUID = 1L;

        // Token: 0x04000CDA RID: 3290
        private List<int> bannersList;

        // Token: 0x04000CDB RID: 3291
        private List<int> bannersWeights;

        // Token: 0x04000CDC RID: 3292
        private int currentBanner;

        // Token: 0x04000CDD RID: 3293
        private int currentWeight;

        // Token: 0x04000CDE RID: 3294
        private int totalBanners;

        // Token: 0x04000CDF RID: 3295
        private bool hideMainPromo;

        // Token: 0x04000CE0 RID: 3296
        private bool hideSocialNetworks;

        // Token: 0x04000CE1 RID: 3297
        private int interstitialBannersPeriod = 2;

        // Token: 0x04000CE2 RID: 3298
        private int changeInterstitialToVideoPeriod = 2;

        // Token: 0x04000CE3 RID: 3299
        private bool defaultInterstitial;

        // Token: 0x04000CE4 RID: 3300
        private int boxForCrossPromo = -1;

        // Token: 0x04000CE5 RID: 3301
        private int videoBannersCount = 1;
    }
}
