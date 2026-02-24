using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.Banner
{
    // Token: 0x0200002E RID: 46
    internal class RemoteDataManager : NSObject, ButtonDelegate
    {
        // Token: 0x060001AE RID: 430 RVA: 0x0000BE68 File Offset: 0x0000A068
        private BannerSize getBannerSize()
        {
            BannerSize bannerSize;
            bannerSize.width = 480;
            bannerSize.height = 300;
            return bannerSize;
        }

        // Token: 0x060001AF RID: 431 RVA: 0x0000BE90 File Offset: 0x0000A090
        public virtual NSObject acquireInfo(int setID)
        {
            if (base.init() != null && remoteDataMgr != null)
            {
                BannerSize bannerSize = getBannerSize();
                string text = "ctr";
                string text2 = "winphone";
                remoteDataMgr.initWith(text, text2, setID, bannerSize.width, bannerSize.height);
            }
            return this;
        }

        // Token: 0x060001B0 RID: 432 RVA: 0x0000BEDB File Offset: 0x0000A0DB
        public static void initRemoteDataMgr(RemoteDataManager_Java pRemoteDataMgr)
        {
            remoteDataMgr = pRemoteDataMgr;
        }

        // Token: 0x060001B1 RID: 433 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
        public virtual Image getBanner()
        {
            if (remoteDataMgr == null)
            {
                return null;
            }
            BannerSize bannerSize = getBannerSize();
            currentBanner = remoteDataMgr.getBanner();
            if (currentBanner != null)
            {
                string name = currentBanner.getName();
                string url = currentBanner.getUrl();
                bannerUrl = NSS(url);
                string @string = currentBanner.getString();
                NSString nsstring = NSS(@string);
                Texture2D.setAntiAliasTexParameters();
                Texture2D texture2D = new Texture2D().initWithImagePath(name);
                if (IS_WVGA)
                {
                    texture2D.setWvga();
                }
                texture2D.setScale((float)bannerSize.width / 321f, (float)bannerSize.height / CHOOSE3(200.0, 200.0, 160.0));
                Image image = Image.Image_create(texture2D);
                if (nsstring.ToString() != "#" && bannerUrl.ToString() != "#")
                {
                    Button button = MenuController.createButtonWithTextIDDelegate(nsstring, 0, this);
                    button.setTouchIncreaseLeftRightTopBottom(0f, 0f, 15f, 15f);
                    button.parentAnchor = 9;
                    button.anchor = 18;
                    Vector vector = Image.getQuadOffset(77, 7);
                    Vector quadOffset = Image.getQuadOffset(78, 0);
                    vector = vectSub(vector, quadOffset);
                    button.x = vector.x;
                    button.y = vector.y;
                    _ = image.addChild(button);
                }
                return image;
            }
            if (bannerUrl != null)
            {
                bannerUrl = null;
            }
            return null;
        }

        // Token: 0x060001B2 RID: 434 RVA: 0x0000C06F File Offset: 0x0000A26F
        public virtual void nextBanner()
        {
            if (remoteDataMgr == null)
            {
                return;
            }
            remoteDataMgr.nextBanner();
        }

        // Token: 0x060001B3 RID: 435 RVA: 0x0000C083 File Offset: 0x0000A283
        public virtual void prevBanner()
        {
            if (remoteDataMgr == null)
            {
                return;
            }
            remoteDataMgr.prevBanner();
        }

        // Token: 0x060001B4 RID: 436 RVA: 0x0000C097 File Offset: 0x0000A297
        public virtual bool hasSenseToRotateBanners()
        {
            return remoteDataMgr != null && remoteDataMgr.hasSenseToRotateBanners();
        }

        // Token: 0x060001B5 RID: 437 RVA: 0x0000C0AC File Offset: 0x0000A2AC
        public virtual bool getHideMainPromo()
        {
            return remoteDataMgr != null && remoteDataMgr.getHideMainPromo();
        }

        // Token: 0x060001B6 RID: 438 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
        public virtual bool getHideSocialNetworks()
        {
            return remoteDataMgr != null && remoteDataMgr.getHideSocialNetworks();
        }

        // Token: 0x060001B7 RID: 439 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
        public virtual bool getDefaultInterstitial()
        {
            return remoteDataMgr != null && remoteDataMgr.getDefaultInterstitial();
        }

        // Token: 0x060001B8 RID: 440 RVA: 0x0000C0EB File Offset: 0x0000A2EB
        public virtual int getBoxForCrossPromo()
        {
            if (remoteDataMgr == null)
            {
                return -1;
            }
            return remoteDataMgr.getBoxForCrossPromo();
        }

        // Token: 0x060001B9 RID: 441 RVA: 0x0000C100 File Offset: 0x0000A300
        public virtual void onButtonPressed(int n)
        {
            if (bannerUrl != null)
            {
                string text = "MMENU_BANNER_PRESSED";
                List<string> list = new List<string>();
                list.Add("banner_id");
                list.Add(currentBanner.id.ToString());
                list.Add("language");
                list.Add(Application.sharedAppSettings().getString(8).ToString());
                list.Add("game_unlocked");
                list.Add(CTRPreferences.isLiteVersion() ? "0" : "1");
                FlurryAPI.logEvent(text, list);
                AndroidAPI.openUrl(bannerUrl);
            }
        }

        // Token: 0x040007D1 RID: 2001
        public const int BANNER_MAIN_BUTTON = 0;

        // Token: 0x040007D2 RID: 2002
        private static RemoteDataManager_Java remoteDataMgr;

        // Token: 0x040007D3 RID: 2003
        private static Banner currentBanner;

        // Token: 0x040007D4 RID: 2004
        private static NSString bannerUrl;

        // Token: 0x0200002F RID: 47
        private struct BannerSize
        {
            // Token: 0x040007D5 RID: 2005
            public int width;

            // Token: 0x040007D6 RID: 2006
            public int height;
        }
    }
}
