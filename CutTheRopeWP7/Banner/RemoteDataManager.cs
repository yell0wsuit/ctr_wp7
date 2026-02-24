using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.Banner
{
    internal sealed class RemoteDataManager : NSObject, ButtonDelegate
    {
        private static BannerSize getBannerSize()
        {
            BannerSize bannerSize;
            bannerSize.width = 480;
            bannerSize.height = 300;
            return bannerSize;
        }

        public NSObject acquireInfo(int setID)
        {
            if (init() != null && remoteDataMgr != null)
            {
                BannerSize bannerSize = getBannerSize();
                string text = "ctr";
                string text2 = "winphone";
                remoteDataMgr.initWith(text, text2, setID, bannerSize.width, bannerSize.height);
            }
            return this;
        }

        public static void initRemoteDataMgr(RemoteDataManager_Java pRemoteDataMgr)
        {
            remoteDataMgr = pRemoteDataMgr;
        }

        public Image getBanner()
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
                texture2D.setScale(bannerSize.width / 321f, bannerSize.height / CHOOSE3(200.0, 200.0, 160.0));
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

        public static void nextBanner()
        {
            if (remoteDataMgr == null)
            {
                return;
            }
            remoteDataMgr.nextBanner();
        }

        public static void prevBanner()
        {
            if (remoteDataMgr == null)
            {
                return;
            }
            remoteDataMgr.prevBanner();
        }

        public static bool hasSenseToRotateBanners()
        {
            return remoteDataMgr != null && remoteDataMgr.hasSenseToRotateBanners();
        }

        public static bool getHideMainPromo()
        {
            return remoteDataMgr != null && remoteDataMgr.getHideMainPromo();
        }

        public static bool getHideSocialNetworks()
        {
            return remoteDataMgr != null && remoteDataMgr.getHideSocialNetworks();
        }

        public static bool getDefaultInterstitial()
        {
            return remoteDataMgr != null && remoteDataMgr.getDefaultInterstitial();
        }

        public static int getBoxForCrossPromo()
        {
            return remoteDataMgr == null ? -1 : remoteDataMgr.getBoxForCrossPromo();
        }

        public void onButtonPressed(int n)
        {
            if (bannerUrl != null)
            {
                string text = "MMENU_BANNER_PRESSED";
                List<string> list =
                [
                    "banner_id",
                    currentBanner.id.ToString(),
                    "language",
                    ApplicationSettings.getString(8).ToString(),
                    "game_unlocked",
                    CTRPreferences.isLiteVersion() ? "0" : "1",
                ];
                FlurryAPI.logEvent(text, list);
                AndroidAPI.openUrl(bannerUrl);
            }
        }

        public const int BANNER_MAIN_BUTTON = 0;

        private static RemoteDataManager_Java remoteDataMgr;

        private static Banner currentBanner;

        private static NSString bannerUrl;

        private struct BannerSize
        {
            public int width;

            public int height;
        }
    }
}
