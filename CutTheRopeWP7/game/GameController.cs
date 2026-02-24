using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.Specials;

using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.game
{
    internal sealed class GameController : ViewController, ButtonDelegate
    {
        // (get) Token: 0x060007BC RID: 1980 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
        public bool BoxCloseHandled => boxCloseHandled;

        // (get) Token: 0x060007BD RID: 1981 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
        public bool BoxLevelWonClosing => boxLevelWonClosing;

        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                createGameView();
                shouldDoNextLevel = false;
            }
            CtrRenderer.gUseFingerDelta = false;
            return this;
        }

        public override void activate()
        {
            CTRPreferences.gameViewChanged(NSS("game"));
            Application.sharedRootController().setViewTransition(-1);
            base.activate();
            CTRSoundMgr._stopMusic();
            _ = (CTRRootController)Application.sharedRootController();
            CTRSoundMgr._playMusic(59);
            initGameView();
            showView(0);
        }

        public override void dealloc()
        {
            base.dealloc();
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (App.NeedsUpdate)
            {
                UpdatePopup.showUpdatePopup();
            }
            if (shouldDoNextLevel)
            {
                shouldDoNextLevel = false;
                onNextLevel();
            }
        }

        public void boxClosed()
        {
            _ = Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int pack = ctrrootController.getPack();
            _ = ctrrootController.getLevel();
            _ = CTRPreferences.getLevelsInPackCount() - 1;
            checkForBoxPerfect(pack);
            int totalStars = CTRPreferences.getTotalStars();
            if (totalStars >= 50)
            {
                CTRRootController.postAchievementName(NSS("acBronzeScissors"));
            }
            if (totalStars >= 150)
            {
                CTRRootController.postAchievementName(NSS("acSilverScissors"));
            }
            if (totalStars >= 300)
            {
                CTRRootController.postAchievementName(NSS("acGoldenScissors"));
            }
            Preferences._savePreferences();
            int num2 = 0;
            for (int i = 0; i < CTRPreferences.getLevelsInPackCount(); i++)
            {
                num2 += CTRPreferences.getScoreForPackLevel(pack, i);
            }
            if (!CTRRootController.isHacked())
            {
                Application.sharedPreferences().setScoreHash();
                Preferences._savePreferences();
                Scorer.postLeaderboardResultforLaderboardIdlowestValFirstforGameCenter(num2, pack, false);
            }
            boxCloseHandled = true;
            boxLevelWonClosing = false;
        }

        public void gameWon()
        {
            postLevelEventwithMask("LEVSCR_LEVEL_WON", 15, true);
            levelWon();
        }

        public void gameLost()
        {
            postLevelEventwithMask("LEVSCR_LEVEL_LOST", 6, false);
        }

        public void onButtonPressed(int n)
        {
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            View view = getView(0);
            _ = view.onTouchMoveXY(-10000f, -10000f);
            CTRSoundMgr._playSound(21);
            switch (n)
            {
                case 0:
                    {
                        AndroidAPI.hideBanner();
                        FlurryAPI.logEvent("LEVMENU_CONTBT_PRESSED", null);
                        GameScene gameScene = (GameScene)view.getChild(0);
                        gameScene.dimTime = tmpDimTime;
                        tmpDimTime = 0f;
                        setPaused(false);
                        return;
                    }
                case 1:
                    break;
                case 2:
                    {
                        AndroidAPI.hideBanner();
                        postLevelEventwithMask("LEVMENU_SKIPBT_PRESSED", 0, true);
                        if (lastLevelInPack() && !ctrrootController.isPicker())
                        {
                            levelQuit();
                            return;
                        }
                        _ = unlockNextLevel();
                        setPaused(false);
                        GameScene gameScene2 = (GameScene)view.getChild(0);
                        gameScene2.loadNextMap();
                        return;
                    }
                case 3:
                    AndroidAPI.hideBanner();
                    exitCode = ctrrootController.getLevel() == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked() ? 3 : 1;
                    CTRSoundMgr._stopAll();
                    levelQuit();
                    postLevelEventwithMask("LEVMENU_LEVSELBT_PRESSED", 0, true);
                    return;
                case 4:
                    AndroidAPI.hideBanner();
                    exitCode = 0;
                    CTRSoundMgr._stopAll();
                    levelQuit();
                    FlurryAPI.logEvent("LEVMENU_MMENUBT_PRESSED", null);
                    return;
                case 5:
                    AndroidAPI.hideBanner();
                    exitCode = ctrrootController.getLevel() == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked() ? 3 : 1;
                    CTRSoundMgr._stopAll();
                    if (!boxCloseHandled)
                    {
                        boxClosed();
                    }
                    postLevelEventwithMask("LEVWONSCR_MENUBT_PRESSED", 1, false);
                    deactivate();
                    return;
                case 6:
                    {
                        AndroidAPI.showBanner();
                        GameScene gameScene3 = (GameScene)view.getChild(0);
                        tmpDimTime = gameScene3.dimTime;
                        releaseAllTouches(gameScene3);
                        gameScene3.dimTime = 0f;
                        setPaused(true);
                        postLevelEventwithMask("LEVSCR_MENUBT_PRESSED", 0, true);
                        return;
                    }
                case 7:
                    AndroidAPI.hideBanner();
                    onNextLevel();
                    return;
                case 8:
                    if (!boxCloseHandled)
                    {
                        boxClosed();
                    }
                    postLevelEventwithMask("LEVWONSCR_REPLAYBT_PRESSED", 1, false);
                    break;
                case 9:
                    {
                        GameScene gameScene4 = (GameScene)view.getChild(0);
                        releaseAllTouches(gameScene4);
                        AndroidAPI.hideBanner();
                        CTRSoundMgr._stopLoopedSounds();
                        if (!boxCloseHandled)
                        {
                            boxClosed();
                        }
                        postLevelEventwithMask("LEVWONSCR_NEXTBT_PRESSED", 1, false);
                        int num = Preferences._getIntForKey("PREFS_LEVELS_WON");
                        Preferences._setIntforKey(num + 1, "PREFS_LEVELS_WON", false);
                        if (CTRPreferences.isBannersMustBeShown())
                        {
                            GameView gameView = (GameView)view;
                            gameView.videoAdLoading = true;
                            AndroidAPI.showVideoBanner();
                            return;
                        }
                        onNextLevel();
                        return;
                    }
                default:
                    return;
            }
            AndroidAPI.hideBanner();
            GameScene gameScene5 = (GameScene)view.getChild(0);
            if (!gameScene5.isEnabled())
            {
                levelStart();
            }
            gameScene5.animateRestartDim = n == 1;
            gameScene5.reload();
            if (n != 1)
            {
                setPaused(false);
            }
            postLevelEventwithMask("LEVSCR_RESTARTBT_PRESSED", 6, false);
        }

        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            View view = getView(0);
            GameView gameView = (GameView)view;
            if (gameView.videoAdLoading)
            {
                return true;
            }
            GameScene gameScene = (GameScene)view.getChild(0);
            bool flag = base.touchesBeganwithEvent(touches);
            if (flag)
            {
                return true;
            }
            if (!gameScene.touchable)
            {
                return false;
            }
            foreach (CTRTouchState ctrtouchState in touches)
            {
                if (ctrtouchState.State == TouchLocationState.Pressed)
                {
                    int num = -1;
                    for (int i = 0; i < 5; i++)
                    {
                        if (touchAddressMap[i] == null)
                        {
                            touchAddressMap[i] = ctrtouchState;
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        _ = gameScene.touchDownXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                }
            }
            return true;
        }

        public override bool touchesEndedwithEvent(List<CTRTouchState> touches)
        {
            View view = getView(0);
            GameScene gameScene = (GameScene)view.getChild(0);
            bool flag = base.touchesEndedwithEvent(touches);
            if (flag)
            {
                return true;
            }
            if (!gameScene.touchable)
            {
                return false;
            }
            foreach (CTRTouchState ctrtouchState in touches)
            {
                if (ctrtouchState.State == TouchLocationState.Released)
                {
                    int num = -1;
                    for (int i = 0; i < 5; i++)
                    {
                        if (touchAddressMap[i] != null && touchAddressMap[i].Id == ctrtouchState.Id)
                        {
                            touchAddressMap[i] = null;
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        _ = gameScene.touchUpXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                    else
                    {
                        releaseAllTouches(gameScene);
                    }
                }
            }
            return true;
        }

        public override bool touchesMovedwithEvent(List<CTRTouchState> touches)
        {
            View view = getView(0);
            GameScene gameScene = (GameScene)view.getChild(0);
            bool flag = base.touchesMovedwithEvent(touches);
            if (flag)
            {
                return true;
            }
            if (!gameScene.touchable)
            {
                return false;
            }
            foreach (CTRTouchState ctrtouchState in touches)
            {
                if (ctrtouchState.State == TouchLocationState.Moved)
                {
                    int num = -1;
                    for (int i = 0; i < 5; i++)
                    {
                        if (touchAddressMap[i] != null && touchAddressMap[i].Id == ctrtouchState.Id)
                        {
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        _ = gameScene.touchMoveXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                }
            }
            return true;
        }

        public void createGameView()
        {
            for (int i = 0; i < 5; i++)
            {
                touchAddressMap[i] = null;
            }
            GameView gameView = (GameView)new GameView().initFullscreen();
            GameScene gameScene = (GameScene)new GameScene().init();
            gameScene.gameSceneDelegate_gameWon = new GameScene.gameWonDelegate(gameWon);
            gameScene.gameSceneDelegate_gameLost = new GameScene.gameLostDelegate(gameLost);
            _ = gameView.addChildwithID(gameScene, 0);
            Button button = MenuController.createButtonWithImageQuad1Quad2IDDelegate(100, 0, 1, 6, this);
            button.setTouchIncreaseLeftRightTopBottom(2f, 6f, 6f, 6f);
            button.y -= SCREEN_OFFSET_Y;
            button.x += SCREEN_OFFSET_X;
            button.x += 0.33f;
            button.y += 0.33f;
            _ = gameView.addChildwithID(button, 1);
            Button button2 = MenuController.createButtonWithImageQuad1Quad2IDDelegate(92, 0, 1, 1, this);
            button2.setTouchIncreaseLeftRightTopBottom(6f, 2f, 6f, 6f);
            button2.y -= SCREEN_OFFSET_Y;
            button2.x += SCREEN_OFFSET_X;
            button2.x += 0.33f;
            button2.y += 0.33f;
            Button button3 = MenuController.createButtonWithImageQuad1Quad2IDDelegate(92, 0, 1, 7, this);
            button3.color = RGBAColor.redRGBA;
            button3.x = -40f;
            button3.setEnabled(false);
            button3.y -= SCREEN_OFFSET_Y;
            button3.x += SCREEN_OFFSET_X;
            _ = gameView.addChildwithID(button2, 2);
            _ = gameView.addChildwithID(button3, 3);
            Image image = Image.Image_createWithResIDQuad(96, 0);
            image.anchor = image.parentAnchor = 10;
            image.passTransformationsToChilds = false;
            image.y = -SCREEN_OFFSET_Y;
            image.scaleX = SCREEN_BG_SCALE_X;
            mapNameLabel = new Text().initWithFont(Application.getFont(6));
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int scoreForPackLevel = CTRPreferences.getScoreForPackLevel(ctrrootController.getPack(), ctrrootController.getLevel());
            mapNameLabel.setString(NSS(Application.getString(1310745) + ",  %d" + scoreForPackLevel));
            mapNameLabel.parentAnchor = 12;
            mapNameLabel.anchor = 20;
            mapNameLabel.x = -10f;
            mapNameLabel.y = 20f;
            mapNameLabel.x += SCREEN_OFFSET_X;
            _ = image.addChild(mapNameLabel);
            VBox vbox = new VBox().initWithOffsetAlignWidth(5.0, 2, SCREEN_WIDTH);
            Button button4 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310762), 0, this);
            _ = vbox.addChild(button4);
            Button button5 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310763), 2, this);
            _ = vbox.addChild(button5);
            Button button6 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310764), 3, this);
            _ = vbox.addChild(button6);
            Button button7 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310765), 4, this);
            _ = vbox.addChild(button7);
            vbox.anchor = vbox.parentAnchor = 10;
            vbox.y = 140f;
            _ = image.addChild(vbox);
            _ = gameView.addChildwithID(image, 4);
            addViewwithID(gameView, 0);
            BoxOpenClose boxOpenClose = (BoxOpenClose)new BoxOpenClose().initWithButtonDelegate(this);
            boxOpenClose.delegateboxClosed = new BoxOpenClose.boxClosed(boxClosed);
            _ = gameView.addChildwithID(boxOpenClose, 5);
        }

        public void initGameView()
        {
            setPaused(false);
            levelFirstStart();
        }

        public void levelFirstStart()
        {
            View view = getView(0);
            ((BoxOpenClose)view.getChild(5)).levelFirstStart();
            isGamePaused = false;
            view.getChild(0).touchable = true;
            view.getChild(1).touchable = true;
            view.getChild(2).touchable = true;
        }

        public void levelStart()
        {
            View view = getView(0);
            ((BoxOpenClose)view.getChild(5)).levelStart();
            isGamePaused = false;
            view.getChild(0).touchable = true;
            view.getChild(1).touchable = true;
            view.getChild(2).touchable = true;
            view.getChild(5).touchable = false;
        }

        public void levelWon()
        {
            bool flag = false;
            boxCloseHandled = false;
            boxLevelWonClosing = true;
            _ = Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            CTRSoundMgr._playSound(47);
            View view = getView(0);
            view.getChild(5).touchable = true;
            GameScene gameScene = (GameScene)view.getChild(0);
            BoxOpenClose boxOpenClose = (BoxOpenClose)view.getChild(5);
            Image image = (Image)boxOpenClose.result.getChildWithName("star1");
            Image image2 = (Image)boxOpenClose.result.getChildWithName("star2");
            Image image3 = (Image)boxOpenClose.result.getChildWithName("star3");
            image.setDrawQuad((gameScene.starsCollected > 0) ? 13 : 14);
            image2.setDrawQuad((gameScene.starsCollected > 1) ? 13 : 14);
            image3.setDrawQuad((gameScene.starsCollected > 2) ? 13 : 14);
            Text text = (Text)boxOpenClose.result.getChildWithName("passText");
            text.setString(Application.getString(1310737 + gameScene.starsCollected));
            boxOpenClose.time = gameScene.time;
            boxOpenClose.starBonus = gameScene.starBonus;
            boxOpenClose.timeBonus = gameScene.timeBonus;
            boxOpenClose.score = gameScene.score;
            isGamePaused = true;
            gameScene.touchable = false;
            view.getChild(2).touchable = false;
            view.getChild(1).touchable = false;
            int pack = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            int scoreForPackLevel = CTRPreferences.getScoreForPackLevel(pack, level);
            int starsForPackLevel = CTRPreferences.getStarsForPackLevel(pack, level);
            int winsForPackLevel = CTRPreferences.getWinsForPackLevel(pack, level);
            boxOpenClose.shouldShowImprovedResult = false;
            if (gameScene.score > scoreForPackLevel)
            {
                flag = true;
                CTRPreferences.setScoreForPackLevel(gameScene.score, pack, level);
                if (scoreForPackLevel > 0)
                {
                    boxOpenClose.shouldShowImprovedResult = true;
                }
            }
            if (gameScene.starsCollected > starsForPackLevel)
            {
                flag = true;
                CTRPreferences.setStarsForPackLevel(gameScene.starsCollected, pack, level);
                if (starsForPackLevel > 0)
                {
                    boxOpenClose.shouldShowImprovedResult = true;
                }
            }
            boxOpenClose.shouldShowConfetti = gameScene.starsCollected == 3;
            CTRPreferences.gameViewChanged(NSS("menu"));
            CTRPreferences.setWinsForPackLevel(winsForPackLevel + 1, pack, level);
            boxOpenClose.levelWon();
            postLevelEventwithMask("LEVWONSCR_SCREEN_SHOWN", 9, false);
            if (unlockNextLevel())
            {
                flag = false;
            }
            if (flag)
            {
                Preferences._savePreferences();
            }
        }

        public void levelLost()
        {
            View view = getView(0);
            ((BoxOpenClose)view.getChild(5)).levelLost();
        }

        public void levelQuit()
        {
            View view = getView(0);
            ((BoxOpenClose)view.getChild(5)).levelQuit();
            view.getChild(0).touchable = false;
        }

        public void setPaused(bool p)
        {
            isGamePaused = p;
            View view = getView(0);
            view.getChild(4).setEnabled(p);
            view.getChild(1).setEnabled(!p);
            view.getChild(2).setEnabled(!p);
            view.getChild(0).touchable = !p;
            view.getChild(0).updateable = !p;
            if (!isGamePaused)
            {
                CTRPreferences.gameViewChanged(NSS("game"));
                CTRSoundMgr._unpause();
                return;
            }
            CTRPreferences.gameViewChanged(NSS("menu"));
            CTRSoundMgr._pause();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            if (ctrrootController.isPicker())
            {
                mapNameLabel.setString(NSS(""));
                return;
            }
            int scoreForPackLevel = CTRPreferences.getScoreForPackLevel(ctrrootController.getPack(), ctrrootController.getLevel());
            mapNameLabel.setString(NSS(Application.getString(1310745) + ": " + scoreForPackLevel));
        }

        public static void checkForBoxPerfect(int pack)
        {
            _ = CTRPreferences.isPackPerfect(pack);
        }

        private void postLevelEventwithMask(string s, int mask = 0, bool mixpanel = false)
        {
            View view = getView(0);
            GameScene gameScene = (GameScene)view.getChild(0);
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int pack = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            Dictionary<string, string> dictionary = new()
            {
                ["Level"] = pack.ToString() + "-" + level.ToString()
            };
            if ((mask & 1) == 1)
            {
                dictionary["stars"] = gameScene.starsCollected.ToString();
            }
            FlurryAPI.logEventwithParams(s, dictionary, true, mixpanel, false);
        }

        public bool lastLevelInPack()
        {
            _ = Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            _ = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            if (level == CTRPreferences.getLevelsInPackCount() - 1)
            {
                exitCode = 2;
                CTRSoundMgr._stopAll();
                return true;
            }
            if (level == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked())
            {
                exitCode = 3;
                CTRSoundMgr._stopAll();
                return true;
            }
            return false;
        }

        public static bool unlockNextLevel()
        {
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int pack = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            if (level < CTRPreferences.getLevelsInPackCount() - 1 && CTRPreferences.getUnlockedForPackLevel(pack, level + 1) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
            {
                CTRPreferences.setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_UNLOCKED, pack, level + 1);
                return true;
            }
            return false;
        }

        public override bool backButtonPressed()
        {
            View view = getView(0);
            Popup popup = (Popup)view.getChildWithName("popup");
            if (popup != null)
            {
                popup.hidePopup();
                return true;
            }
            AdSkipper adSkipper = (AdSkipper)view.getChild(7);
            if (adSkipper.active)
            {
                adSkipper.onButtonPressed(0);
            }
            else if (view.getChild(1).touchable)
            {
                onButtonPressed(6);
            }
            else if (view.getChild(4).isEnabled())
            {
                onButtonPressed(0);
            }
            else if (view.getChild(5).touchable)
            {
                onButtonPressed(5);
            }
            return true;
        }

        public override bool menuButtonPressed()
        {
            View view = getView(0);
            if (view.getChild(1).touchable)
            {
                onButtonPressed(6);
            }
            else if (view.getChild(4).isEnabled())
            {
                onButtonPressed(0);
            }
            return true;
        }

        public void onNextLevel()
        {
            CTRPreferences.gameViewChanged(NSS("game"));
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            View view = getView(0);
            GameView gameView = (GameView)view;
            gameView.videoAdLoading = false;
            gameView.unsetJSkipper();
            if (lastLevelInPack() && !ctrrootController.isPicker())
            {
                deactivate();
                return;
            }
            GameScene gameScene = (GameScene)view.getChild(0);
            gameScene.loadNextMap();
            levelStart();
        }

        public void onVideoBannerFinished()
        {
            shouldDoNextLevel = true;
        }

        public void releaseAllTouches(GameScene gs)
        {
            for (int i = 0; i < 5; i++)
            {
                touchAddressMap[i] = null;
                _ = gs.touchUpXYIndex(-500f, -500f, i);
            }
        }

        public void setAdSkipper(object skipper)
        {
            View view = getView(0);
            GameView gameView = (GameView)view;
            gameView.setJSkipper(skipper);
            gameView.videoAdLoading = false;
        }

        private const int ANALYTICS_STARS = 1;

        private const int ANALYTICS_SUPERPOWER_COUNT = 2;

        private const int ANALYTICS_SUPERPOWER_ENABLED = 4;

        private const int ANALYTICS_BLUE_STAR_COLLECTED = 8;

        private const int BUTTON_PAUSE_RESUME = 0;

        private const int BUTTON_PAUSE_RESTART = 1;

        private const int BUTTON_PAUSE_SKIP = 2;

        private const int BUTTON_PAUSE_LEVEL_SELECT = 3;

        private const int BUTTON_PAUSE_EXIT = 4;

        public const int BUTTON_WIN_EXIT = 5;

        private const int BUTTON_PAUSE = 6;

        private const int BUTTON_NEXT_LEVEL = 7;

        public const int BUTTON_WIN_RESTART = 8;

        public const int BUTTON_WIN_NEXT_LEVEL = 9;

        public const int EXIT_CODE_FROM_PAUSE_MENU = 0;

        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT = 1;

        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT_NEXT_PACK = 2;

        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT_SHOW_UNLOCK = 3;

        public bool isGamePaused;

        public int exitCode;

        private Text mapNameLabel;

        private readonly CTRTouchState[] touchAddressMap = new CTRTouchState[5];

        private bool boxCloseHandled;

        private bool boxLevelWonClosing;

        private bool shouldDoNextLevel;

        private float tmpDimTime;
    }
}
