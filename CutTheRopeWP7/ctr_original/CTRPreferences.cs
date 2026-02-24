using ctr_wp7.Banner;
using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_original
{
    // Token: 0x020000BB RID: 187
    internal sealed class CTRPreferences : Preferences
    {
        // Token: 0x0600054B RID: 1355 RVA: 0x00029218 File Offset: 0x00027418
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

        // Token: 0x0600054C RID: 1356 RVA: 0x0002939D File Offset: 0x0002759D
        private void resetMusicSound()
        {
            setBooleanforKey(true, "SOUND_ON", true);
            setBooleanforKey(true, "MUSIC_ON", true);
        }

        // Token: 0x0600054D RID: 1357 RVA: 0x000293BC File Offset: 0x000275BC
        public bool shouldShowCoppa()
        {
            bool flag;
            if (CoppaLoader.getHideCoppaPopupIsExplicit())
            {
                flag = !CoppaLoader.getHideCoppaPopup();
            }
            else
            {
                DeviceParams deviceParams = new();
                flag = deviceParams.isEnglishDevice();
            }
            bool flag2 = getCoppaShowed();
            if (!flag)
            {
                setCoppaShowed(true);
                flag2 = true;
            }
            return !flag2;
        }

        // Token: 0x0600054E RID: 1358 RVA: 0x000293FF File Offset: 0x000275FF
        public bool getCoppaShowed()
        {
            return getBooleanForKey("PREFS_COPPA_SHOWED");
        }

        // Token: 0x0600054F RID: 1359 RVA: 0x0002940C File Offset: 0x0002760C
        public void setCoppaShowed(bool b)
        {
            setBooleanforKey(b, "PREFS_COPPA_SHOWED", true);
        }

        // Token: 0x06000550 RID: 1360 RVA: 0x0002941B File Offset: 0x0002761B
        public void setUserAge(int age)
        {
            setIntforKey(age, "PREFS_USER_AGE", true);
        }

        // Token: 0x06000551 RID: 1361 RVA: 0x0002942A File Offset: 0x0002762A
        public int getUserAge()
        {
            return getIntForKey("PREFS_USER_AGE");
        }

        // Token: 0x06000552 RID: 1362 RVA: 0x00029437 File Offset: 0x00027637
        public bool isCoppaRestricted()
        {
            return getBooleanForKey("PREFS_COPPA_RESTRICTED");
        }

        // Token: 0x06000553 RID: 1363 RVA: 0x00029444 File Offset: 0x00027644
        public void setCoppaRestricted(bool b)
        {
            setBooleanforKey(b, "PREFS_COPPA_RESTRICTED", true);
        }

        // Token: 0x06000554 RID: 1364 RVA: 0x00029453 File Offset: 0x00027653
        private static bool isShareware()
        {
            return false;
        }

        // Token: 0x06000555 RID: 1365 RVA: 0x00029458 File Offset: 0x00027658
        public static bool isSharewareUnlocked()
        {
            bool flag = isShareware();
            return !flag || (flag && _getBooleanForKey("IAP_SHAREWARE"));
        }

        // Token: 0x17000014 RID: 20
        // (set) Token: 0x06000556 RID: 1366 RVA: 0x0002947F File Offset: 0x0002767F
        public static bool IsTrial
        {
            set
            {
                isTrial = value;
            }
        }

        // Token: 0x06000557 RID: 1367 RVA: 0x00029487 File Offset: 0x00027687
        public static bool isLiteVersion()
        {
            return isTrial;
        }

        // Token: 0x06000558 RID: 1368 RVA: 0x0002948E File Offset: 0x0002768E
        public static bool isBannersMustBeShown()
        {
            return false;
        }

        // Token: 0x06000559 RID: 1369 RVA: 0x00029491 File Offset: 0x00027691
        public static int getStarsForPackLevel(int p, int l)
        {
            return _getIntForKey(getPackLevelKey("STARS_", p, l));
        }

        // Token: 0x0600055A RID: 1370 RVA: 0x000294A4 File Offset: 0x000276A4
        public static UNLOCKED_STATE getUnlockedForPackLevel(int p, int l)
        {
            return (UNLOCKED_STATE)_getIntForKey(getPackLevelKey("UNLOCKED_", p, l));
        }

        // Token: 0x0600055B RID: 1371 RVA: 0x000294B7 File Offset: 0x000276B7
        public static int getAttemptsForPackLevel(int p, int l)
        {
            return _getIntForKey("ATTEMPTS_" + p.ToString() + l.ToString());
        }

        // Token: 0x0600055C RID: 1372 RVA: 0x000294D6 File Offset: 0x000276D6
        public static void setAttemptsForPackLevel(int a, int p, int l)
        {
            _setIntforKey(a, "ATTEMPTS_" + p.ToString() + l.ToString(), true);
        }

        // Token: 0x0600055D RID: 1373 RVA: 0x000294F7 File Offset: 0x000276F7
        public static int getPacksCount()
        {
            return getPacksCount(isLiteVersion());
        }

        // Token: 0x0600055E RID: 1374 RVA: 0x00029503 File Offset: 0x00027703
        public static int getPacksCount(bool isLite)
        {
            if (!isLite)
            {
                return 14;
            }
            return 2;
        }

        // Token: 0x0600055F RID: 1375 RVA: 0x0002950C File Offset: 0x0002770C
        public static int getLevelsInPackCount()
        {
            return getLevelsInPackCount(isLiteVersion());
        }

        // Token: 0x06000560 RID: 1376 RVA: 0x00029518 File Offset: 0x00027718
        public static int getLevelsInPackCount(bool isLite)
        {
            if (!isLite)
            {
                return 25;
            }
            return 9;
        }

        // Token: 0x06000561 RID: 1377 RVA: 0x00029524 File Offset: 0x00027724
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

        // Token: 0x06000562 RID: 1378 RVA: 0x00029564 File Offset: 0x00027764
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

        // Token: 0x06000563 RID: 1379 RVA: 0x000295A8 File Offset: 0x000277A8
        public static int packUnlockStars(int n)
        {
            if (!isLiteVersion())
            {
                return PACK_UNLOCK_STARS[n];
            }
            return PACK_UNLOCK_STARS_LITE[n];
        }

        // Token: 0x06000564 RID: 1380 RVA: 0x000295C0 File Offset: 0x000277C0
        private static string getPackLevelKey(string prefs, int p, int l)
        {
            return prefs + p.ToString() + "_" + l.ToString();
        }

        // Token: 0x06000565 RID: 1381 RVA: 0x000295DB File Offset: 0x000277DB
        public static void setUnlockedForPackLevel(UNLOCKED_STATE s, int p, int l)
        {
            _setIntforKey((int)s, getPackLevelKey("UNLOCKED_", p, l), true);
        }

        // Token: 0x06000566 RID: 1382 RVA: 0x000295F0 File Offset: 0x000277F0
        public static int sharewareFreeLevels()
        {
            return 10;
        }

        // Token: 0x06000567 RID: 1383 RVA: 0x000295F4 File Offset: 0x000277F4
        public static int sharewareFreePacks()
        {
            return 2;
        }

        // Token: 0x06000568 RID: 1384 RVA: 0x000295F8 File Offset: 0x000277F8
        public static void setLastPack(int p)
        {
            _setIntforKey(p, "PREFS_LAST_PACK" + getLastDelivery().ToString(), false);
        }

        // Token: 0x06000569 RID: 1385 RVA: 0x00029624 File Offset: 0x00027824
        public static int getLastDelivery()
        {
            int num = _getIntForKey("PREFS_LAST_DELIVERY");
            return MIN(MAX(0, num), 2);
        }

        // Token: 0x0600056A RID: 1386 RVA: 0x00029649 File Offset: 0x00027849
        public static bool isInLastDelivery()
        {
            return getLastDelivery() == 2;
        }

        // Token: 0x0600056B RID: 1387 RVA: 0x00029653 File Offset: 0x00027853
        public static void setLastDelivery(int d)
        {
            _setIntforKey(d, "PREFS_LAST_DELIVERY", true);
        }

        // Token: 0x0600056C RID: 1388 RVA: 0x00029661 File Offset: 0x00027861
        public static int getGameSessionsCount()
        {
            return _getIntForKey("GAME_SESSIONS_COUNT_");
        }

        // Token: 0x0600056D RID: 1389 RVA: 0x0002966D File Offset: 0x0002786D
        public static void setGameSessionsCount(int c)
        {
            _setIntforKey(c, "GAME_SESSIONS_COUNT_", true);
        }

        // Token: 0x0600056E RID: 1390 RVA: 0x0002967C File Offset: 0x0002787C
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

        // Token: 0x0600056F RID: 1391 RVA: 0x000296A8 File Offset: 0x000278A8
        public static int getLastPack()
        {
            return _getIntForKey("PREFS_LAST_PACK" + getLastDelivery().ToString());
        }

        // Token: 0x06000570 RID: 1392 RVA: 0x000296D3 File Offset: 0x000278D3
        public static void gameViewChanged(NSString NameOfView)
        {
        }

        // Token: 0x06000571 RID: 1393 RVA: 0x000296D8 File Offset: 0x000278D8
        public static int getScoreForPackLevel(int p, int l)
        {
            return _getIntForKey(string.Concat(new object[] { "SCORE_", p, "_", l }));
        }

        // Token: 0x06000572 RID: 1394 RVA: 0x0002971C File Offset: 0x0002791C
        public static void setScoreForPackLevel(int s, int p, int l)
        {
            _setIntforKey(s, string.Concat(new object[] { "SCORE_", p, "_", l }), false);
        }

        // Token: 0x06000573 RID: 1395 RVA: 0x00029760 File Offset: 0x00027960
        public static void setStarsForPackLevel(int s, int p, int l)
        {
            _setIntforKey(s, string.Concat(new object[] { "STARS_", p, "_", l }), false);
        }

        // Token: 0x06000574 RID: 1396 RVA: 0x000297A4 File Offset: 0x000279A4
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

        // Token: 0x06000575 RID: 1397 RVA: 0x000297D0 File Offset: 0x000279D0
        public static void disablePlayLevelScroll()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            ctrpreferences.playLevelScroll = false;
        }

        // Token: 0x06000576 RID: 1398 RVA: 0x000297EC File Offset: 0x000279EC
        internal static bool shouldPlayLevelScroll()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.playLevelScroll;
        }

        // Token: 0x06000577 RID: 1399 RVA: 0x00029808 File Offset: 0x00027A08
        internal static bool shouldShowPromo()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.showPromoBanner;
        }

        // Token: 0x06000578 RID: 1400 RVA: 0x00029824 File Offset: 0x00027A24
        internal static void disablePromoBanner()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            ctrpreferences.showPromoBanner = false;
        }

        // Token: 0x06000579 RID: 1401 RVA: 0x00029840 File Offset: 0x00027A40
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

        // Token: 0x0600057A RID: 1402 RVA: 0x00029A10 File Offset: 0x00027C10
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

        // Token: 0x0600057B RID: 1403 RVA: 0x00029A50 File Offset: 0x00027C50
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

        // Token: 0x0600057C RID: 1404 RVA: 0x00029A98 File Offset: 0x00027C98
        public void setScoreHash()
        {
            NSString nsstring = NSS(getTotalScore().ToString());
            NSString md5Str = getMD5Str(nsstring);
            setStringforKey(md5Str.ToString(), "PREFS_SCORE_HASH", true);
        }

        // Token: 0x0600057D RID: 1405 RVA: 0x00029AD4 File Offset: 0x00027CD4
        public bool isScoreHashValid()
        {
            NSString nsstring = NSS(getTotalScore().ToString());
            NSString md5Str = getMD5Str(nsstring);
            NSString nsstring2 = NSS(getStringForKey("PREFS_SCORE_HASH"));
            return md5Str.isEqualToString(nsstring2);
        }

        // Token: 0x0600057E RID: 1406 RVA: 0x00029B14 File Offset: 0x00027D14
        internal static bool isFirstLaunch()
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            return ctrpreferences.firstLaunch;
        }

        // Token: 0x0600057F RID: 1407 RVA: 0x00029B2D File Offset: 0x00027D2D
        public static void setCartoonWatched(NSString url)
        {
            _setIntforKey(1, "PREFS_CARTOON_WATCHED_" + url.ToString(), true);
        }

        // Token: 0x06000580 RID: 1408 RVA: 0x00029B46 File Offset: 0x00027D46
        public static bool getCartoonWatched(NSString url)
        {
            return _getIntForKey("PREFS_CARTOON_WATCHED_" + url.ToString()) != 0;
        }

        // Token: 0x06000581 RID: 1409 RVA: 0x00029B64 File Offset: 0x00027D64
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

        // Token: 0x06000582 RID: 1410 RVA: 0x00029BC4 File Offset: 0x00027DC4
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

        // Token: 0x06000583 RID: 1411 RVA: 0x00029D34 File Offset: 0x00027F34
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

        // Token: 0x06000584 RID: 1412 RVA: 0x00029D78 File Offset: 0x00027F78
        public static int getWinsForPackLevel(int p, int l)
        {
            return _getIntForKey("WINS_" + p.ToString() + "_" + l.ToString());
        }

        // Token: 0x06000585 RID: 1413 RVA: 0x00029D9C File Offset: 0x00027F9C
        public static void setWinsForPackLevel(int a, int p, int l)
        {
            _setIntforKey(a, "WINS_" + p.ToString() + "_" + l.ToString(), false);
        }

        // Token: 0x04000A91 RID: 2705
        public const int VERSION_NUMBER_AT_WHICH_SCORE_HASH_INTRODUCED = 1;

        // Token: 0x04000A92 RID: 2706
        public const int VERSION_NUMBER = 2;

        // Token: 0x04000A93 RID: 2707
        public const int DELIVERIES_COUNT = 3;

        // Token: 0x04000A94 RID: 2708
        public const int MAX_LEVEL_SCORE = 5999;

        // Token: 0x04000A95 RID: 2709
        public const int MAX_PACK_SCORE = 149999;

        // Token: 0x04000A96 RID: 2710
        public const int CANDIES_COUNT = 3;

        // Token: 0x04000A97 RID: 2711
        public const string TWITTER_LINK = "https://mobile.twitter.com/zeptolab";

        // Token: 0x04000A98 RID: 2712
        public const string FACEBOOK_LINK = "http://www.facebook.com/cuttherope";

        // Token: 0x04000A99 RID: 2713
        public const string CTR_EXP_GETITNOW_LINK = "http://www.windowsphone.com/en-us/store/app/cut-the-rope-exp/d9f1608e-138a-4278-802f-25e32e44c068";

        // Token: 0x04000A9A RID: 2714
        public const string EXPERIMENTS_LINK = "http://www.amazon.com/gp/mas/dl/android?p=com.zeptolab.ctrexperiments.hd.amazon.paid";

        // Token: 0x04000A9B RID: 2715
        public const int BOXES_CUT_OUT = 0;

        // Token: 0x04000A9C RID: 2716
        public const int MAX_PACKS = 15;

        // Token: 0x04000A9D RID: 2717
        public const int MAX_LEVELS_IN_A_PACK = 25;

        // Token: 0x04000A9E RID: 2718
        public const string APP_ID = "ctr";

        // Token: 0x04000A9F RID: 2719
        public const string PREFS_COPPA_SHOWED = "PREFS_COPPA_SHOWED";

        // Token: 0x04000AA0 RID: 2720
        public const string PREFS_USER_AGE = "PREFS_USER_AGE";

        // Token: 0x04000AA1 RID: 2721
        public const string PREFS_COPPA_RESTRICTED = "PREFS_COPPA_RESTRICTED";

        // Token: 0x04000AA2 RID: 2722
        public const string PREFS_IS_EXIST = "PREFS_EXIST";

        // Token: 0x04000AA3 RID: 2723
        public const string PREFS_SOUND_ON = "SOUND_ON";

        // Token: 0x04000AA4 RID: 2724
        public const string PREFS_MUSIC_ON = "MUSIC_ON";

        // Token: 0x04000AA5 RID: 2725
        public const string PREFS_SCORE_ = "SCORE_";

        // Token: 0x04000AA6 RID: 2726
        public const string PREFS_STARS_ = "STARS_";

        // Token: 0x04000AA7 RID: 2727
        public const string PREFS_UNLOCKED_ = "UNLOCKED_";

        // Token: 0x04000AA8 RID: 2728
        public const string PREFS_ATTEMPTS_ = "ATTEMPTS_";

        // Token: 0x04000AA9 RID: 2729
        public const string PREFS_WINS_ = "WINS_";

        // Token: 0x04000AAA RID: 2730
        public const string PREFS_GAME_SESSIONS_COUNT = "GAME_SESSIONS_COUNT_";

        // Token: 0x04000AAB RID: 2731
        public const string PREFS_DRAWINGS_ = "DRAWINGS_";

        // Token: 0x04000AAC RID: 2732
        public const string PREFS_NEW_DRAWINGS_COUNTER = "PREFS_NEW_DRAWINGS_COUNTER";

        // Token: 0x04000AAD RID: 2733
        public const string PREFS_ROPES_CUT = "PREFS_ROPES_CUT";

        // Token: 0x04000AAE RID: 2734
        public const string PREFS_BUBBLES_POPPED = "PREFS_BUBBLES_POPPED";

        // Token: 0x04000AAF RID: 2735
        public const string PREFS_SPIDERS_BUSTED = "PREFS_SPIDERS_BUSTED";

        // Token: 0x04000AB0 RID: 2736
        public const string PREFS_SPIDERS_WON = "PREFS_SPIDERS_WON";

        // Token: 0x04000AB1 RID: 2737
        public const string PREFS_CANDIES_LOST = "PREFS_CANDIES_LOST";

        // Token: 0x04000AB2 RID: 2738
        public const string PREFS_CANDIES_UNITED = "PREFS_CANDIES_UNITED";

        // Token: 0x04000AB3 RID: 2739
        public const string PREFS_SOCKS_USED = "PREFS_SOCKS_USED";

        // Token: 0x04000AB4 RID: 2740
        public const string PREFS_LAST_PACK = "PREFS_LAST_PACK";

        // Token: 0x04000AB5 RID: 2741
        public const string PREFS_LAST_DELIVERY = "PREFS_LAST_DELIVERY";

        // Token: 0x04000AB6 RID: 2742
        public const string PREFS_CANDY_WAS_CHANGED = "PREFS_CANDY_WAS_CHANGED";

        // Token: 0x04000AB7 RID: 2743
        public const string PREFS_SELECTED_CANDY = "PREFS_SELECTED_CANDY";

        // Token: 0x04000AB8 RID: 2744
        public const string PREFS_GAME_CENTER_ENABLED = "PREFS_GAME_CENTER_ENABLED";

        // Token: 0x04000AB9 RID: 2745
        public const string PREFS_SCORE_HASH = "PREFS_SCORE_HASH";

        // Token: 0x04000ABA RID: 2746
        public const string PREFS_VERSION = "PREFS_VERSION";

        // Token: 0x04000ABB RID: 2747
        public const string PREFS_GAME_STARTS = "PREFS_GAME_STARTS";

        // Token: 0x04000ABC RID: 2748
        public const string PREFS_LEVELS_WON = "PREFS_LEVELS_WON";

        // Token: 0x04000ABD RID: 2749
        public const string PREFS_CARTOON_WATCHED_ = "PREFS_CARTOON_WATCHED_";

        // Token: 0x04000ABE RID: 2750
        public const string PREFS_IAP_UNLOCK = "IAP_UNLOCK";

        // Token: 0x04000ABF RID: 2751
        public const string PREFS_IAP_BANNERS = "IAP_BANNERS";

        // Token: 0x04000AC0 RID: 2752
        public const string PREFS_IAP_SHAREWARE = "IAP_SHAREWARE";

        // Token: 0x04000AC1 RID: 2753
        public const string acBronzeScissors = "acBronzeScissors";

        // Token: 0x04000AC2 RID: 2754
        public const string acSilverScissors = "acSilverScissors";

        // Token: 0x04000AC3 RID: 2755
        public const string acGoldenScissors = "acGoldenScissors";

        // Token: 0x04000AC4 RID: 2756
        public const string acRopeCutter = "acRopeCutter";

        // Token: 0x04000AC5 RID: 2757
        public const string acRopeCutterManiac = "acRopeCutterManiac";

        // Token: 0x04000AC6 RID: 2758
        public const string acUltimateRopeCutter = "acUltimateRopeCutter";

        // Token: 0x04000AC7 RID: 2759
        public const string acBubblePopper = "acBubblePopper";

        // Token: 0x04000AC8 RID: 2760
        public const string acBubbleMaster = "acBubbleMaster";

        // Token: 0x04000AC9 RID: 2761
        public const string acSpiderBuster = "acSpiderBuster";

        // Token: 0x04000ACA RID: 2762
        public const string acSpiderTamer = "acSpiderTamer";

        // Token: 0x04000ACB RID: 2763
        public const string acSpiderLover = "acSpiderLover";

        // Token: 0x04000ACC RID: 2764
        public const string acWeightLoser = "acWeightLoser";

        // Token: 0x04000ACD RID: 2765
        public const string acCalorieMinimizer = "acCalorieMinimizer";

        // Token: 0x04000ACE RID: 2766
        public const string acQuickFinger = "acQuickFinger";

        // Token: 0x04000ACF RID: 2767
        public const string acMasterFinger = "acMasterFinger";

        // Token: 0x04000AD0 RID: 2768
        public const string acTummyTeaser = "acTummyTeaser";

        // Token: 0x04000AD1 RID: 2769
        public const string acCandyJuggler = "acCandyJuggler";

        // Token: 0x04000AD2 RID: 2770
        public const string acRomanticSoul = "acRomanticSoul";

        // Token: 0x04000AD3 RID: 2771
        public const string acMagician = "acMagician";

        // Token: 0x04000AD4 RID: 2772
        public const string lastVersionLaunched = "lastVersionLaunched";

        // Token: 0x04000AD5 RID: 2773
        public RemoteDataManager remoteDataManager = new();

        // Token: 0x04000AD6 RID: 2774
        private bool firstLaunch;

        // Token: 0x04000AD7 RID: 2775
        private bool showPromoBanner;

        // Token: 0x04000AD8 RID: 2776
        private bool playLevelScroll;

        // Token: 0x04000AD9 RID: 2777
        private static bool isTrial;

        // Token: 0x04000ADA RID: 2778
        private static int[] PACK_UNLOCK_STARS_LITE =
        [
            0, 20, 80, 170, 230, 0, 40, 90, 150, 200,
            0, 40, 90, 150
        ];

        // Token: 0x04000ADB RID: 2779
        private static int[] PACK_UNLOCK_STARS =
        [
            0, 30, 80, 170, 230, 0, 40, 90, 150, 200,
            0, 40, 90, 150
        ];
    }
}
