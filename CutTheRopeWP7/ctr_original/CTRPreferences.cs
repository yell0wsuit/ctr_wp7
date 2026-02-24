using ctr_wp7.Banner;
using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_original
{
    internal sealed class CTRPreferences : Preferences
    {
        public override NSObject init()
        {
            if (base.init() != null)
            {
                remoteDataManager = (RemoteDataManager)new RemoteDataManager().acquireInfo(0);
                if (!isTrial)
                {
                    int levelsInPackCount = getLevelsInPackCount(true);
                    int packsCount = getPacksCount(true);
                    for (int i = 0; i < packsCount; i++)
                    {
                        if (getWinsForPackLevel(i, levelsInPackCount - 1) > 0)
                        {
                            setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_JUST_UNLOCKED, i, levelsInPackCount);
                        }
                    }
                }
                bool flag = !getBooleanForKey("PREFS_EXIST");
                if (flag)
                {
                    setBooleanforKey(true, "PREFS_EXIST", false);
                    setIntforKey(0, "PREFS_GAME_STARTS", false);
                    setIntforKey(0, "PREFS_LEVELS_WON", false);
                    resetToDefaults();
                    _setIntforKey(0, "GAME_SESSIONS_COUNT_", true);
                    resetMusicSound();
                    firstLaunch = true;
                    showPromoBanner = false;
                    playLevelScroll = false;
                }
                else
                {
                    int intForKey = getIntForKey("PREFS_VERSION");
                    if (intForKey < 1)
                    {
                        _ = getTotalScore();
                        int j = 0;
                        int packsCount2 = getPacksCount();
                        while (j < packsCount2)
                        {
                            int num = 0;
                            int k = 0;
                            int levelsInPackCount2 = getLevelsInPackCount();
                            while (k < levelsInPackCount2)
                            {
                                int intForKey2 = getIntForKey(getPackLevelKey("SCORE_", j, k));
                                if (intForKey2 > 5999)
                                {
                                    num = 150000;
                                    break;
                                }
                                num += intForKey2;
                                k++;
                            }
                            if (num > 149999)
                            {
                                resetToDefaults();
                                resetMusicSound();
                                break;
                            }
                            j++;
                        }
                        setScoreHash();
                    }
                    firstLaunch = false;
                    playLevelScroll = false;
                }
                setIntforKey(2, "PREFS_VERSION", true);
            }
            return this;
        }

        private void resetMusicSound()
        {
            setBooleanforKey(true, "SOUND_ON", true);
            setBooleanforKey(true, "MUSIC_ON", true);
        }

        public bool shouldShowCoppa()
        {
            bool flag;
            if (CoppaLoader.getHideCoppaPopupIsExplicit())
            {
                flag = !CoppaLoader.getHideCoppaPopup();
            }
            else
            {
                _ = new DeviceParams();
                flag = DeviceParams.isEnglishDevice();
            }
            bool flag2 = getCoppaShowed();
            if (!flag)
            {
                setCoppaShowed(true);
                flag2 = true;
            }
            return !flag2;
        }

        public bool getCoppaShowed()
        {
            return getBooleanForKey("PREFS_COPPA_SHOWED");
        }

        public void setCoppaShowed(bool b)
        {
            setBooleanforKey(b, "PREFS_COPPA_SHOWED", true);
        }

        public void setUserAge(int age)
        {
            setIntforKey(age, "PREFS_USER_AGE", true);
        }

        public int getUserAge()
        {
            return getIntForKey("PREFS_USER_AGE");
        }

        public bool isCoppaRestricted()
        {
            return getBooleanForKey("PREFS_COPPA_RESTRICTED");
        }

        public void setCoppaRestricted(bool b)
        {
            setBooleanforKey(b, "PREFS_COPPA_RESTRICTED", true);
        }

        private static bool isShareware()
        {
            return false;
        }

        public static bool isSharewareUnlocked()
        {
            bool flag = isShareware();
            return !flag || (flag && _getBooleanForKey("IAP_SHAREWARE"));
        }

        // (set) Token: 0x06000556 RID: 1366 RVA: 0x0002947F File Offset: 0x0002767F
        public static bool IsTrial
        {
            set => isTrial = value;
        }

        public static bool isLiteVersion()
        {
            return isTrial;
        }

        public static bool isBannersMustBeShown()
        {
            return false;
        }

        public static int getStarsForPackLevel(int p, int l)
        {
            return _getIntForKey(getPackLevelKey("STARS_", p, l));
        }

        public static UNLOCKED_STATE getUnlockedForPackLevel(int p, int l)
        {
            return (UNLOCKED_STATE)_getIntForKey(getPackLevelKey("UNLOCKED_", p, l));
        }

        public static int getAttemptsForPackLevel(int p, int l)
        {
            return _getIntForKey("ATTEMPTS_" + p.ToString() + l.ToString());
        }

        public static void setAttemptsForPackLevel(int a, int p, int l)
        {
            _setIntforKey(a, "ATTEMPTS_" + p.ToString() + l.ToString(), true);
        }

        public static int getPacksCount()
        {
            return getPacksCount(isLiteVersion());
        }

        public static int getPacksCount(bool isLite)
        {
            return !isLite ? 14 : 2;
        }

        public static int getLevelsInPackCount()
        {
            return getLevelsInPackCount(isLiteVersion());
        }

        public static int getLevelsInPackCount(bool isLite)
        {
            return !isLite ? 25 : 9;
        }

        public static int getTotalStars()
        {
            int num = 0;
            int i = 0;
            int packsCount = getPacksCount();
            while (i < packsCount)
            {
                int j = 0;
                int levelsInPackCount = getLevelsInPackCount();
                while (j < levelsInPackCount)
                {
                    num += getStarsForPackLevel(i, j);
                    j++;
                }
                i++;
            }
            return num;
        }

        public static int getTotalStarsInDelivery(int delivery = -1)
        {
            int num = 0;
            PackSelectInfo packSelectInfo = getPackSelectInfo(false, delivery);
            for (int i = 0; i < packSelectInfo.size; i++)
            {
                int saveIndex = BoxFabric.getSaveIndex(packSelectInfo.content[i]);
                if (saveIndex != -1)
                {
                    num += getTotalStarsInPack(saveIndex);
                }
            }
            return num;
        }

        public static int packUnlockStars(int n)
        {
            return !isLiteVersion() ? PACK_UNLOCK_STARS[n] : PACK_UNLOCK_STARS_LITE[n];
        }

        private static string getPackLevelKey(string prefs, int p, int l)
        {
            return prefs + p.ToString() + "_" + l.ToString();
        }

        public static void setUnlockedForPackLevel(UNLOCKED_STATE s, int p, int l)
        {
            _setIntforKey((int)s, getPackLevelKey("UNLOCKED_", p, l), true);
        }

        public static int sharewareFreeLevels()
        {
            return 10;
        }

        public static int sharewareFreePacks()
        {
            return 2;
        }

        public static void setLastPack(int p)
        {
            _setIntforKey(p, "PREFS_LAST_PACK" + getLastDelivery().ToString(), false);
        }

        public static int getLastDelivery()
        {
            int num = _getIntForKey("PREFS_LAST_DELIVERY");
            return MIN(MAX(0, num), 2);
        }

        public static bool isInLastDelivery()
        {
            return getLastDelivery() == 2;
        }

        public static void setLastDelivery(int d)
        {
            _setIntforKey(d, "PREFS_LAST_DELIVERY", true);
        }

        public static int getGameSessionsCount()
        {
            return _getIntForKey("GAME_SESSIONS_COUNT_");
        }

        public static void setGameSessionsCount(int c)
        {
            _setIntforKey(c, "GAME_SESSIONS_COUNT_", true);
        }

        public static bool isPackPerfect(int p)
        {
            int i = 0;
            int levelsInPackCount = getLevelsInPackCount();
            while (i < levelsInPackCount)
            {
                if (getStarsForPackLevel(p, i) < 3)
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        public static int getLastPack()
        {
            return _getIntForKey("PREFS_LAST_PACK" + getLastDelivery().ToString());
        }

        public static void gameViewChanged(NSString NameOfView)
        {
        }

        public static int getScoreForPackLevel(int p, int l)
        {
            return _getIntForKey(string.Concat(new object[] { "SCORE_", p, "_", l }));
        }

        public static void setScoreForPackLevel(int s, int p, int l)
        {
            _setIntforKey(s, string.Concat(new object[] { "SCORE_", p, "_", l }), false);
        }

        public static void setStarsForPackLevel(int s, int p, int l)
        {
            _setIntforKey(s, string.Concat(new object[] { "STARS_", p, "_", l }), false);
        }

        public static int getTotalStarsInPack(int p)
        {
            int num = 0;
            int i = 0;
            int levelsInPackCount = getLevelsInPackCount();
            while (i < levelsInPackCount)
            {
                num += getStarsForPackLevel(p, i);
                i++;
            }
            return num;
        }

        public static void disablePlayLevelScroll()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            ctrpreferences.playLevelScroll = false;
        }

        internal static bool shouldPlayLevelScroll()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.playLevelScroll;
        }

        internal static bool shouldShowPromo()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.showPromoBanner;
        }

        internal static void disablePromoBanner()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            ctrpreferences.showPromoBanner = false;
        }

        public void resetToDefaults()
        {
            int i = 0;
            int packsCount = getPacksCount();
            while (i < packsCount)
            {
                int j = 0;
                int levelsInPackCount = getLevelsInPackCount();
                while (j < levelsInPackCount)
                {
                    int num = ((packUnlockStars(i) == 0 || (isShareware() && i < sharewareFreePacks())) && j == 0) ? 1 : 0;
                    setIntforKey(0, getPackLevelKey("SCORE_", i, j), false);
                    setIntforKey(0, getPackLevelKey("STARS_", i, j), false);
                    setIntforKey(num, getPackLevelKey("UNLOCKED_", i, j), false);
                    setIntforKey(0, "ATTEMPTS_" + i.ToString() + j.ToString(), false);
                    setIntforKey(0, "WINS_" + i.ToString() + "_" + j.ToString(), false);
                    j++;
                }
                i++;
            }
            setIntforKey(1, "GAME_SESSIONS_COUNT_", false);
            setIntforKey(0, "PREFS_ROPES_CUT", false);
            setIntforKey(0, "PREFS_BUBBLES_POPPED", false);
            setIntforKey(0, "PREFS_SPIDERS_BUSTED", false);
            setIntforKey(0, "PREFS_SPIDERS_WON", false);
            setIntforKey(0, "PREFS_CANDIES_LOST", false);
            setIntforKey(0, "PREFS_CANDIES_UNITED", false);
            setIntforKey(0, "PREFS_SOCKS_USED", false);
            setIntforKey(0, "PREFS_SELECTED_CANDY", false);
            setBooleanforKey(false, "PREFS_CANDY_WAS_CHANGED", false);
            setBooleanforKey(true, "PREFS_GAME_CENTER_ENABLED", false);
            setIntforKey(0, "PREFS_NEW_DRAWINGS_COUNTER", false);
            _deleteKey("PREFS_LAST_DELIVERY", false);
            for (int k = 0; k < 3; k++)
            {
                _deleteKey("PREFS_LAST_PACK" + k.ToString(), false);
            }
            _deleteKeysStartWith("PREFS_CARTOON_WATCHED_", false);
            checkForUnlockIAP();
            savePreferences();
            setScoreHash();
        }

        private void checkForUnlockIAP()
        {
            if (getBooleanForKey("IAP_UNLOCK"))
            {
                int i = 0;
                int packsCount = getPacksCount();
                while (i < packsCount)
                {
                    if (getUnlockedForPackLevel(i, 0) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
                    {
                        setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_JUST_UNLOCKED, i, 0);
                    }
                    i++;
                }
            }
        }

        private int getTotalScore()
        {
            int num = 0;
            for (int i = 0; i < getPacksCount(); i++)
            {
                for (int j = 0; j < getLevelsInPackCount(); j++)
                {
                    num += getIntForKey(getPackLevelKey("SCORE_", i, j));
                }
            }
            return num;
        }

        public void setScoreHash()
        {
            NSString nsstring = NSS(getTotalScore().ToString());
            NSString md5Str = getMD5Str(nsstring);
            setStringforKey(md5Str.ToString(), "PREFS_SCORE_HASH", true);
        }

        public bool isScoreHashValid()
        {
            NSString nsstring = NSS(getTotalScore().ToString());
            NSString md5Str = getMD5Str(nsstring);
            NSString nsstring2 = NSS(getStringForKey("PREFS_SCORE_HASH"));
            return md5Str.isEqualToString(nsstring2);
        }

        internal static bool isFirstLaunch()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.firstLaunch;
        }

        public static void setCartoonWatched(NSString url)
        {
            _setIntforKey(1, "PREFS_CARTOON_WATCHED_" + url.ToString(), true);
        }

        public static bool getCartoonWatched(NSString url)
        {
            return _getIntForKey("PREFS_CARTOON_WATCHED_" + url.ToString()) != 0;
        }

        public void unlockAllLevels(int stars)
        {
            int i = 0;
            int packsCount = getPacksCount();
            while (i < packsCount)
            {
                int j = 0;
                int levelsInPackCount = getLevelsInPackCount();
                while (j < levelsInPackCount)
                {
                    setIntforKey(1, getPackLevelKey("UNLOCKED_", i, j), false);
                    setIntforKey(stars, getPackLevelKey("STARS_", i, j), false);
                    j++;
                }
                i++;
            }
            savePreferences();
        }

        public static PackSelectInfo getPackSelectInfo(bool zerobox, int delivery = -1)
        {
            if (delivery == -1)
            {
                delivery = getLastDelivery();
            }
            PackSelectInfo packSelectInfo = new();
            switch (delivery)
            {
                case 0:
                    if (isLiteVersion())
                    {
                        packSelectInfo = (PackSelectInfo)packSelectInfo.initWithSize(5 + (zerobox ? 1 : 0));
                        if (zerobox)
                        {
                            packSelectInfo.add(0);
                        }
                        packSelectInfo.add(3);
                        packSelectInfo.add(4);
                        packSelectInfo.add(5);
                        packSelectInfo.add(6);
                        packSelectInfo.add(7);
                    }
                    else
                    {
                        packSelectInfo = (PackSelectInfo)packSelectInfo.initWithSize(6 + (zerobox ? 1 : 0));
                        if (zerobox)
                        {
                            packSelectInfo.add(0);
                        }
                        packSelectInfo.add(3);
                        packSelectInfo.add(4);
                        packSelectInfo.add(5);
                        packSelectInfo.add(6);
                        packSelectInfo.add(7);
                        packSelectInfo.add(19);
                    }
                    break;
                case 1:
                    packSelectInfo = (PackSelectInfo)packSelectInfo.initWithSize(6 + (zerobox ? 1 : 0));
                    if (zerobox)
                    {
                        packSelectInfo.add(0);
                    }
                    packSelectInfo.add(8);
                    packSelectInfo.add(9);
                    packSelectInfo.add(10);
                    packSelectInfo.add(11);
                    packSelectInfo.add(12);
                    packSelectInfo.add(19);
                    break;
                case 2:
                    packSelectInfo = (PackSelectInfo)packSelectInfo.initWithSize(4 + (zerobox ? 1 : 0) + 1);
                    if (zerobox)
                    {
                        packSelectInfo.add(0);
                    }
                    packSelectInfo.add(13);
                    packSelectInfo.add(14);
                    packSelectInfo.add(15);
                    packSelectInfo.add(16);
                    packSelectInfo.add(18);
                    break;
            }
            return packSelectInfo;
        }

        public static int getTotalCompletedLevels()
        {
            int num = 0;
            int i = 0;
            int packsCount = getPacksCount();
            while (i < packsCount)
            {
                int j = 0;
                int levelsInPackCount = getLevelsInPackCount();
                while (j < levelsInPackCount)
                {
                    if (getWinsForPackLevel(i, j) > 0)
                    {
                        num++;
                    }
                    j++;
                }
                i++;
            }
            return num;
        }

        public static int getWinsForPackLevel(int p, int l)
        {
            return _getIntForKey("WINS_" + p.ToString() + "_" + l.ToString());
        }

        public static void setWinsForPackLevel(int a, int p, int l)
        {
            _setIntforKey(a, "WINS_" + p.ToString() + "_" + l.ToString(), false);
        }

        public const int VERSION_NUMBER_AT_WHICH_SCORE_HASH_INTRODUCED = 1;

        public const int VERSION_NUMBER = 2;

        public const int DELIVERIES_COUNT = 3;

        public const int MAX_LEVEL_SCORE = 5999;

        public const int MAX_PACK_SCORE = 149999;

        public const int CANDIES_COUNT = 3;

        public const string TWITTER_LINK = "https://mobile.twitter.com/zeptolab";

        public const string FACEBOOK_LINK = "http://www.facebook.com/cuttherope";

        public const string CTR_EXP_GETITNOW_LINK = "http://www.windowsphone.com/en-us/store/app/cut-the-rope-exp/d9f1608e-138a-4278-802f-25e32e44c068";

        public const string EXPERIMENTS_LINK = "http://www.amazon.com/gp/mas/dl/android?p=com.zeptolab.ctrexperiments.hd.amazon.paid";

        public const int BOXES_CUT_OUT = 0;

        public const int MAX_PACKS = 15;

        public const int MAX_LEVELS_IN_A_PACK = 25;

        public const string APP_ID = "ctr";

        public const string PREFS_COPPA_SHOWED = "PREFS_COPPA_SHOWED";

        public const string PREFS_USER_AGE = "PREFS_USER_AGE";

        public const string PREFS_COPPA_RESTRICTED = "PREFS_COPPA_RESTRICTED";

        public const string PREFS_IS_EXIST = "PREFS_EXIST";

        public const string PREFS_SOUND_ON = "SOUND_ON";

        public const string PREFS_MUSIC_ON = "MUSIC_ON";

        public const string PREFS_SCORE_ = "SCORE_";

        public const string PREFS_STARS_ = "STARS_";

        public const string PREFS_UNLOCKED_ = "UNLOCKED_";

        public const string PREFS_ATTEMPTS_ = "ATTEMPTS_";

        public const string PREFS_WINS_ = "WINS_";

        public const string PREFS_GAME_SESSIONS_COUNT = "GAME_SESSIONS_COUNT_";

        public const string PREFS_DRAWINGS_ = "DRAWINGS_";

        public const string PREFS_NEW_DRAWINGS_COUNTER = "PREFS_NEW_DRAWINGS_COUNTER";

        public const string PREFS_ROPES_CUT = "PREFS_ROPES_CUT";

        public const string PREFS_BUBBLES_POPPED = "PREFS_BUBBLES_POPPED";

        public const string PREFS_SPIDERS_BUSTED = "PREFS_SPIDERS_BUSTED";

        public const string PREFS_SPIDERS_WON = "PREFS_SPIDERS_WON";

        public const string PREFS_CANDIES_LOST = "PREFS_CANDIES_LOST";

        public const string PREFS_CANDIES_UNITED = "PREFS_CANDIES_UNITED";

        public const string PREFS_SOCKS_USED = "PREFS_SOCKS_USED";

        public const string PREFS_LAST_PACK = "PREFS_LAST_PACK";

        public const string PREFS_LAST_DELIVERY = "PREFS_LAST_DELIVERY";

        public const string PREFS_CANDY_WAS_CHANGED = "PREFS_CANDY_WAS_CHANGED";

        public const string PREFS_SELECTED_CANDY = "PREFS_SELECTED_CANDY";

        public const string PREFS_GAME_CENTER_ENABLED = "PREFS_GAME_CENTER_ENABLED";

        public const string PREFS_SCORE_HASH = "PREFS_SCORE_HASH";

        public const string PREFS_VERSION = "PREFS_VERSION";

        public const string PREFS_GAME_STARTS = "PREFS_GAME_STARTS";

        public const string PREFS_LEVELS_WON = "PREFS_LEVELS_WON";

        public const string PREFS_CARTOON_WATCHED_ = "PREFS_CARTOON_WATCHED_";

        public const string PREFS_IAP_UNLOCK = "IAP_UNLOCK";

        public const string PREFS_IAP_BANNERS = "IAP_BANNERS";

        public const string PREFS_IAP_SHAREWARE = "IAP_SHAREWARE";

        public const string acBronzeScissors = "acBronzeScissors";

        public const string acSilverScissors = "acSilverScissors";

        public const string acGoldenScissors = "acGoldenScissors";

        public const string acRopeCutter = "acRopeCutter";

        public const string acRopeCutterManiac = "acRopeCutterManiac";

        public const string acUltimateRopeCutter = "acUltimateRopeCutter";

        public const string acBubblePopper = "acBubblePopper";

        public const string acBubbleMaster = "acBubbleMaster";

        public const string acSpiderBuster = "acSpiderBuster";

        public const string acSpiderTamer = "acSpiderTamer";

        public const string acSpiderLover = "acSpiderLover";

        public const string acWeightLoser = "acWeightLoser";

        public const string acCalorieMinimizer = "acCalorieMinimizer";

        public const string acQuickFinger = "acQuickFinger";

        public const string acMasterFinger = "acMasterFinger";

        public const string acTummyTeaser = "acTummyTeaser";

        public const string acCandyJuggler = "acCandyJuggler";

        public const string acRomanticSoul = "acRomanticSoul";

        public const string acMagician = "acMagician";

        public const string lastVersionLaunched = "lastVersionLaunched";

        public RemoteDataManager remoteDataManager = new();

        private bool firstLaunch;

        private bool showPromoBanner;

        private bool playLevelScroll;

        private static bool isTrial;

        private static readonly int[] PACK_UNLOCK_STARS_LITE =
[
    0, 20, 80, 170, 230, 0, 40, 90, 150, 200,
            0, 40, 90, 150
];

        private static readonly int[] PACK_UNLOCK_STARS =
[
    0, 30, 80, 170, 230, 0, 40, 90, 150, 200,
            0, 40, 90, 150
];
    }
}
