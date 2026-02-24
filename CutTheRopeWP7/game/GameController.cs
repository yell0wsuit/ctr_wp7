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
    // Token: 0x02000101 RID: 257
    internal class GameController : ViewController, ButtonDelegate
    {
        // Token: 0x17000027 RID: 39
        // (get) Token: 0x060007BC RID: 1980 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
        public bool BoxCloseHandled
        {
            get
            {
                return this.boxCloseHandled;
            }
        }

        // Token: 0x17000028 RID: 40
        // (get) Token: 0x060007BD RID: 1981 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
        public bool BoxLevelWonClosing
        {
            get
            {
                return this.boxLevelWonClosing;
            }
        }

        // Token: 0x060007BE RID: 1982 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                this.createGameView();
                this.shouldDoNextLevel = false;
            }
            CtrRenderer.gUseFingerDelta = false;
            return this;
        }

        // Token: 0x060007BF RID: 1983 RVA: 0x0003C608 File Offset: 0x0003A808
        public override void activate()
        {
            CTRPreferences.gameViewChanged(NSObject.NSS("game"));
            Application.sharedRootController().setViewTransition(-1);
            base.activate();
            CTRSoundMgr._stopMusic();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            CTRSoundMgr._playMusic(59);
            this.initGameView();
            this.showView(0);
        }

        // Token: 0x060007C0 RID: 1984 RVA: 0x0003C659 File Offset: 0x0003A859
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x060007C1 RID: 1985 RVA: 0x0003C661 File Offset: 0x0003A861
        public override void update(float delta)
        {
            base.update(delta);
            if (App.NeedsUpdate)
            {
                UpdatePopup.showUpdatePopup();
            }
            if (this.shouldDoNextLevel)
            {
                this.shouldDoNextLevel = false;
                this.onNextLevel();
            }
        }

        // Token: 0x060007C2 RID: 1986 RVA: 0x0003C68C File Offset: 0x0003A88C
        public virtual void boxClosed()
        {
            Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int pack = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            int num = CTRPreferences.getLevelsInPackCount() - 1;
            GameController.checkForBoxPerfect(pack);
            int totalStars = CTRPreferences.getTotalStars();
            if (totalStars >= 50)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acBronzeScissors"));
            }
            if (totalStars >= 150)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acSilverScissors"));
            }
            if (totalStars >= 300)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acGoldenScissors"));
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
            this.boxCloseHandled = true;
            this.boxLevelWonClosing = false;
        }

        // Token: 0x060007C3 RID: 1987 RVA: 0x0003C766 File Offset: 0x0003A966
        public virtual void gameWon()
        {
            this.postLevelEventwithMask("LEVSCR_LEVEL_WON", 15, true);
            this.levelWon();
        }

        // Token: 0x060007C4 RID: 1988 RVA: 0x0003C77C File Offset: 0x0003A97C
        public virtual void gameLost()
        {
            this.postLevelEventwithMask("LEVSCR_LEVEL_LOST", 6, false);
        }

        // Token: 0x060007C5 RID: 1989 RVA: 0x0003C78C File Offset: 0x0003A98C
        public virtual void onButtonPressed(int n)
        {
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            View view = this.getView(0);
            view.onTouchMoveXY(-10000f, -10000f);
            CTRSoundMgr._playSound(21);
            switch (n)
            {
                case 0:
                    {
                        FrameworkTypes.AndroidAPI.hideBanner();
                        FrameworkTypes.FlurryAPI.logEvent("LEVMENU_CONTBT_PRESSED", null);
                        GameScene gameScene = (GameScene)view.getChild(0);
                        gameScene.dimTime = this.tmpDimTime;
                        this.tmpDimTime = 0f;
                        this.setPaused(false);
                        return;
                    }
                case 1:
                    break;
                case 2:
                    {
                        FrameworkTypes.AndroidAPI.hideBanner();
                        this.postLevelEventwithMask("LEVMENU_SKIPBT_PRESSED", 0, true);
                        if (this.lastLevelInPack() && !ctrrootController.isPicker())
                        {
                            this.levelQuit();
                            return;
                        }
                        this.unlockNextLevel();
                        this.setPaused(false);
                        GameScene gameScene2 = (GameScene)view.getChild(0);
                        gameScene2.loadNextMap();
                        return;
                    }
                case 3:
                    FrameworkTypes.AndroidAPI.hideBanner();
                    if (ctrrootController.getLevel() == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked())
                    {
                        this.exitCode = 3;
                    }
                    else
                    {
                        this.exitCode = 1;
                    }
                    CTRSoundMgr._stopAll();
                    this.levelQuit();
                    this.postLevelEventwithMask("LEVMENU_LEVSELBT_PRESSED", 0, true);
                    return;
                case 4:
                    FrameworkTypes.AndroidAPI.hideBanner();
                    this.exitCode = 0;
                    CTRSoundMgr._stopAll();
                    this.levelQuit();
                    FrameworkTypes.FlurryAPI.logEvent("LEVMENU_MMENUBT_PRESSED", null);
                    return;
                case 5:
                    FrameworkTypes.AndroidAPI.hideBanner();
                    if (ctrrootController.getLevel() == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked())
                    {
                        this.exitCode = 3;
                    }
                    else
                    {
                        this.exitCode = 1;
                    }
                    CTRSoundMgr._stopAll();
                    if (!this.boxCloseHandled)
                    {
                        this.boxClosed();
                    }
                    this.postLevelEventwithMask("LEVWONSCR_MENUBT_PRESSED", 1, false);
                    this.deactivate();
                    return;
                case 6:
                    {
                        FrameworkTypes.AndroidAPI.showBanner();
                        GameScene gameScene3 = (GameScene)view.getChild(0);
                        this.tmpDimTime = gameScene3.dimTime;
                        this.releaseAllTouches(gameScene3);
                        gameScene3.dimTime = 0f;
                        this.setPaused(true);
                        this.postLevelEventwithMask("LEVSCR_MENUBT_PRESSED", 0, true);
                        return;
                    }
                case 7:
                    FrameworkTypes.AndroidAPI.hideBanner();
                    this.onNextLevel();
                    return;
                case 8:
                    if (!this.boxCloseHandled)
                    {
                        this.boxClosed();
                    }
                    this.postLevelEventwithMask("LEVWONSCR_REPLAYBT_PRESSED", 1, false);
                    break;
                case 9:
                    {
                        GameScene gameScene4 = (GameScene)view.getChild(0);
                        this.releaseAllTouches(gameScene4);
                        FrameworkTypes.AndroidAPI.hideBanner();
                        CTRSoundMgr._stopLoopedSounds();
                        if (!this.boxCloseHandled)
                        {
                            this.boxClosed();
                        }
                        this.postLevelEventwithMask("LEVWONSCR_NEXTBT_PRESSED", 1, false);
                        int num = Preferences._getIntForKey("PREFS_LEVELS_WON");
                        Preferences._setIntforKey(num + 1, "PREFS_LEVELS_WON", false);
                        if (CTRPreferences.isBannersMustBeShown())
                        {
                            GameView gameView = (GameView)view;
                            gameView.videoAdLoading = true;
                            FrameworkTypes.AndroidAPI.showVideoBanner();
                            return;
                        }
                        this.onNextLevel();
                        return;
                    }
                default:
                    return;
            }
            FrameworkTypes.AndroidAPI.hideBanner();
            GameScene gameScene5 = (GameScene)view.getChild(0);
            if (!gameScene5.isEnabled())
            {
                this.levelStart();
            }
            gameScene5.animateRestartDim = n == 1;
            gameScene5.reload();
            if (n != 1)
            {
                this.setPaused(false);
            }
            this.postLevelEventwithMask("LEVSCR_RESTARTBT_PRESSED", 6, false);
        }

        // Token: 0x060007C6 RID: 1990 RVA: 0x0003CA70 File Offset: 0x0003AC70
        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            View view = this.getView(0);
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
                        if (this.touchAddressMap[i] == null)
                        {
                            this.touchAddressMap[i] = ctrtouchState;
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        gameScene.touchDownXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                }
            }
            return true;
        }

        // Token: 0x060007C7 RID: 1991 RVA: 0x0003CB60 File Offset: 0x0003AD60
        public override bool touchesEndedwithEvent(List<CTRTouchState> touches)
        {
            View view = this.getView(0);
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
                        if (this.touchAddressMap[i] != null && this.touchAddressMap[i].Id == ctrtouchState.Id)
                        {
                            this.touchAddressMap[i] = null;
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        gameScene.touchUpXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                    else
                    {
                        this.releaseAllTouches(gameScene);
                    }
                }
            }
            return true;
        }

        // Token: 0x060007C8 RID: 1992 RVA: 0x0003CC60 File Offset: 0x0003AE60
        public override bool touchesMovedwithEvent(List<CTRTouchState> touches)
        {
            View view = this.getView(0);
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
                        if (this.touchAddressMap[i] != null && this.touchAddressMap[i].Id == ctrtouchState.Id)
                        {
                            num = i;
                            break;
                        }
                    }
                    if (num != -1)
                    {
                        gameScene.touchMoveXYIndex(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y), num);
                    }
                }
            }
            return true;
        }

        // Token: 0x060007C9 RID: 1993 RVA: 0x0003CD4C File Offset: 0x0003AF4C
        public virtual void createGameView()
        {
            for (int i = 0; i < 5; i++)
            {
                this.touchAddressMap[i] = null;
            }
            GameView gameView = (GameView)new GameView().initFullscreen();
            GameScene gameScene = (GameScene)new GameScene().init();
            gameScene.gameSceneDelegate_gameWon = new GameScene.gameWonDelegate(this.gameWon);
            gameScene.gameSceneDelegate_gameLost = new GameScene.gameLostDelegate(this.gameLost);
            gameView.addChildwithID(gameScene, 0);
            Button button = MenuController.createButtonWithImageQuad1Quad2IDDelegate(100, 0, 1, 6, this);
            button.setTouchIncreaseLeftRightTopBottom(2f, 6f, 6f, 6f);
            button.y -= FrameworkTypes.SCREEN_OFFSET_Y;
            button.x += FrameworkTypes.SCREEN_OFFSET_X;
            button.x += 0.33f;
            button.y += 0.33f;
            gameView.addChildwithID(button, 1);
            Button button2 = MenuController.createButtonWithImageQuad1Quad2IDDelegate(92, 0, 1, 1, this);
            button2.setTouchIncreaseLeftRightTopBottom(6f, 2f, 6f, 6f);
            button2.y -= FrameworkTypes.SCREEN_OFFSET_Y;
            button2.x += FrameworkTypes.SCREEN_OFFSET_X;
            button2.x += 0.33f;
            button2.y += 0.33f;
            Button button3 = MenuController.createButtonWithImageQuad1Quad2IDDelegate(92, 0, 1, 7, this);
            button3.color = RGBAColor.redRGBA;
            button3.x = -40f;
            button3.setEnabled(false);
            button3.y -= FrameworkTypes.SCREEN_OFFSET_Y;
            button3.x += FrameworkTypes.SCREEN_OFFSET_X;
            gameView.addChildwithID(button2, 2);
            gameView.addChildwithID(button3, 3);
            Image image = Image.Image_createWithResIDQuad(96, 0);
            image.anchor = (image.parentAnchor = 10);
            image.passTransformationsToChilds = false;
            image.y = -FrameworkTypes.SCREEN_OFFSET_Y;
            image.scaleX = FrameworkTypes.SCREEN_BG_SCALE_X;
            this.mapNameLabel = new Text().initWithFont(Application.getFont(6));
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int scoreForPackLevel = CTRPreferences.getScoreForPackLevel(ctrrootController.getPack(), ctrrootController.getLevel());
            this.mapNameLabel.setString(NSObject.NSS(Application.getString(1310745) + ",  %d" + scoreForPackLevel));
            this.mapNameLabel.parentAnchor = 12;
            this.mapNameLabel.anchor = 20;
            this.mapNameLabel.x = -10f;
            this.mapNameLabel.y = 20f;
            this.mapNameLabel.x += FrameworkTypes.SCREEN_OFFSET_X;
            image.addChild(this.mapNameLabel);
            VBox vbox = new VBox().initWithOffsetAlignWidth(5.0, 2, (double)FrameworkTypes.SCREEN_WIDTH);
            Button button4 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310762), 0, this);
            vbox.addChild(button4);
            Button button5 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310763), 2, this);
            vbox.addChild(button5);
            Button button6 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310764), 3, this);
            vbox.addChild(button6);
            Button button7 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310765), 4, this);
            vbox.addChild(button7);
            vbox.anchor = (vbox.parentAnchor = 10);
            vbox.y = 140f;
            image.addChild(vbox);
            gameView.addChildwithID(image, 4);
            this.addViewwithID(gameView, 0);
            BoxOpenClose boxOpenClose = (BoxOpenClose)new BoxOpenClose().initWithButtonDelegate(this);
            boxOpenClose.delegateboxClosed = new BoxOpenClose.boxClosed(this.boxClosed);
            gameView.addChildwithID(boxOpenClose, 5);
        }

        // Token: 0x060007CA RID: 1994 RVA: 0x0003D10D File Offset: 0x0003B30D
        public virtual void initGameView()
        {
            this.setPaused(false);
            this.levelFirstStart();
        }

        // Token: 0x060007CB RID: 1995 RVA: 0x0003D11C File Offset: 0x0003B31C
        public virtual void levelFirstStart()
        {
            View view = this.getView(0);
            ((BoxOpenClose)view.getChild(5)).levelFirstStart();
            this.isGamePaused = false;
            view.getChild(0).touchable = true;
            view.getChild(1).touchable = true;
            view.getChild(2).touchable = true;
        }

        // Token: 0x060007CC RID: 1996 RVA: 0x0003D170 File Offset: 0x0003B370
        public virtual void levelStart()
        {
            View view = this.getView(0);
            ((BoxOpenClose)view.getChild(5)).levelStart();
            this.isGamePaused = false;
            view.getChild(0).touchable = true;
            view.getChild(1).touchable = true;
            view.getChild(2).touchable = true;
            view.getChild(5).touchable = false;
        }

        // Token: 0x060007CD RID: 1997 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
        public virtual void levelWon()
        {
            bool flag = false;
            this.boxCloseHandled = false;
            this.boxLevelWonClosing = true;
            Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            CTRSoundMgr._playSound(47);
            View view = this.getView(0);
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
            this.isGamePaused = true;
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
            CTRPreferences.gameViewChanged(NSObject.NSS("menu"));
            CTRPreferences.setWinsForPackLevel(winsForPackLevel + 1, pack, level);
            boxOpenClose.levelWon();
            this.postLevelEventwithMask("LEVWONSCR_SCREEN_SHOWN", 9, false);
            if (this.unlockNextLevel())
            {
                flag = false;
            }
            if (flag)
            {
                Preferences._savePreferences();
            }
        }

        // Token: 0x060007CE RID: 1998 RVA: 0x0003D42C File Offset: 0x0003B62C
        public virtual void levelLost()
        {
            View view = this.getView(0);
            ((BoxOpenClose)view.getChild(5)).levelLost();
        }

        // Token: 0x060007CF RID: 1999 RVA: 0x0003D454 File Offset: 0x0003B654
        public virtual void levelQuit()
        {
            View view = this.getView(0);
            ((BoxOpenClose)view.getChild(5)).levelQuit();
            view.getChild(0).touchable = false;
        }

        // Token: 0x060007D0 RID: 2000 RVA: 0x0003D488 File Offset: 0x0003B688
        public virtual void setPaused(bool p)
        {
            this.isGamePaused = p;
            View view = this.getView(0);
            view.getChild(4).setEnabled(p);
            view.getChild(1).setEnabled(!p);
            view.getChild(2).setEnabled(!p);
            view.getChild(0).touchable = !p;
            view.getChild(0).updateable = !p;
            if (!this.isGamePaused)
            {
                CTRPreferences.gameViewChanged(NSObject.NSS("game"));
                CTRSoundMgr._unpause();
                return;
            }
            CTRPreferences.gameViewChanged(NSObject.NSS("menu"));
            CTRSoundMgr._pause();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            if (ctrrootController.isPicker())
            {
                this.mapNameLabel.setString(NSObject.NSS(""));
                return;
            }
            int scoreForPackLevel = CTRPreferences.getScoreForPackLevel(ctrrootController.getPack(), ctrrootController.getLevel());
            this.mapNameLabel.setString(NSObject.NSS(Application.getString(1310745) + ": " + scoreForPackLevel));
        }

        // Token: 0x060007D1 RID: 2001 RVA: 0x0003D587 File Offset: 0x0003B787
        public static void checkForBoxPerfect(int pack)
        {
            CTRPreferences.isPackPerfect(pack);
        }

        // Token: 0x060007D2 RID: 2002 RVA: 0x0003D590 File Offset: 0x0003B790
        protected void postLevelEventwithMask(string s, int mask = 0, bool mixpanel = false)
        {
            View view = this.getView(0);
            GameScene gameScene = (GameScene)view.getChild(0);
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int pack = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Level"] = pack.ToString() + "-" + level.ToString();
            if ((mask & 1) == 1)
            {
                dictionary["stars"] = gameScene.starsCollected.ToString();
            }
            FrameworkTypes.FlurryAPI.logEventwithParams(s, dictionary, true, mixpanel, false);
        }

        // Token: 0x060007D3 RID: 2003 RVA: 0x0003D620 File Offset: 0x0003B820
        public virtual bool lastLevelInPack()
        {
            Application.sharedPreferences();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            if (level == CTRPreferences.getLevelsInPackCount() - 1)
            {
                this.exitCode = 2;
                CTRSoundMgr._stopAll();
                return true;
            }
            if (level == CTRPreferences.sharewareFreeLevels() - 1 && !CTRPreferences.isSharewareUnlocked())
            {
                this.exitCode = 3;
                CTRSoundMgr._stopAll();
                return true;
            }
            return false;
        }

        // Token: 0x060007D4 RID: 2004 RVA: 0x0003D684 File Offset: 0x0003B884
        public virtual bool unlockNextLevel()
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

        // Token: 0x060007D5 RID: 2005 RVA: 0x0003D6CC File Offset: 0x0003B8CC
        public override bool backButtonPressed()
        {
            View view = this.getView(0);
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
                this.onButtonPressed(6);
            }
            else if (view.getChild(4).isEnabled())
            {
                this.onButtonPressed(0);
            }
            else if (view.getChild(5).touchable)
            {
                this.onButtonPressed(5);
            }
            return true;
        }

        // Token: 0x060007D6 RID: 2006 RVA: 0x0003D760 File Offset: 0x0003B960
        public override bool menuButtonPressed()
        {
            View view = this.getView(0);
            if (view.getChild(1).touchable)
            {
                this.onButtonPressed(6);
            }
            else if (view.getChild(4).isEnabled())
            {
                this.onButtonPressed(0);
            }
            return true;
        }

        // Token: 0x060007D7 RID: 2007 RVA: 0x0003D7A4 File Offset: 0x0003B9A4
        public virtual void onNextLevel()
        {
            CTRPreferences.gameViewChanged(NSObject.NSS("game"));
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            View view = this.getView(0);
            GameView gameView = (GameView)view;
            gameView.videoAdLoading = false;
            gameView.unsetJSkipper();
            if (this.lastLevelInPack() && !ctrrootController.isPicker())
            {
                this.deactivate();
                return;
            }
            GameScene gameScene = (GameScene)view.getChild(0);
            gameScene.loadNextMap();
            this.levelStart();
        }

        // Token: 0x060007D8 RID: 2008 RVA: 0x0003D817 File Offset: 0x0003BA17
        public virtual void onVideoBannerFinished()
        {
            this.shouldDoNextLevel = true;
        }

        // Token: 0x060007D9 RID: 2009 RVA: 0x0003D820 File Offset: 0x0003BA20
        public virtual void releaseAllTouches(GameScene gs)
        {
            for (int i = 0; i < 5; i++)
            {
                this.touchAddressMap[i] = null;
                gs.touchUpXYIndex(-500f, -500f, i);
            }
        }

        // Token: 0x060007DA RID: 2010 RVA: 0x0003D854 File Offset: 0x0003BA54
        public virtual void setAdSkipper(object skipper)
        {
            View view = this.getView(0);
            GameView gameView = (GameView)view;
            gameView.setJSkipper(skipper);
            gameView.videoAdLoading = false;
        }

        // Token: 0x04000D0E RID: 3342
        private const int ANALYTICS_STARS = 1;

        // Token: 0x04000D0F RID: 3343
        private const int ANALYTICS_SUPERPOWER_COUNT = 2;

        // Token: 0x04000D10 RID: 3344
        private const int ANALYTICS_SUPERPOWER_ENABLED = 4;

        // Token: 0x04000D11 RID: 3345
        private const int ANALYTICS_BLUE_STAR_COLLECTED = 8;

        // Token: 0x04000D12 RID: 3346
        private const int BUTTON_PAUSE_RESUME = 0;

        // Token: 0x04000D13 RID: 3347
        private const int BUTTON_PAUSE_RESTART = 1;

        // Token: 0x04000D14 RID: 3348
        private const int BUTTON_PAUSE_SKIP = 2;

        // Token: 0x04000D15 RID: 3349
        private const int BUTTON_PAUSE_LEVEL_SELECT = 3;

        // Token: 0x04000D16 RID: 3350
        private const int BUTTON_PAUSE_EXIT = 4;

        // Token: 0x04000D17 RID: 3351
        public const int BUTTON_WIN_EXIT = 5;

        // Token: 0x04000D18 RID: 3352
        private const int BUTTON_PAUSE = 6;

        // Token: 0x04000D19 RID: 3353
        private const int BUTTON_NEXT_LEVEL = 7;

        // Token: 0x04000D1A RID: 3354
        public const int BUTTON_WIN_RESTART = 8;

        // Token: 0x04000D1B RID: 3355
        public const int BUTTON_WIN_NEXT_LEVEL = 9;

        // Token: 0x04000D1C RID: 3356
        public const int EXIT_CODE_FROM_PAUSE_MENU = 0;

        // Token: 0x04000D1D RID: 3357
        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT = 1;

        // Token: 0x04000D1E RID: 3358
        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT_NEXT_PACK = 2;

        // Token: 0x04000D1F RID: 3359
        public const int EXIT_CODE_FROM_PAUSE_MENU_LEVEL_SELECT_SHOW_UNLOCK = 3;

        // Token: 0x04000D20 RID: 3360
        public bool isGamePaused;

        // Token: 0x04000D21 RID: 3361
        public int exitCode;

        // Token: 0x04000D22 RID: 3362
        private Text mapNameLabel;

        // Token: 0x04000D23 RID: 3363
        private CTRTouchState[] touchAddressMap = new CTRTouchState[5];

        // Token: 0x04000D24 RID: 3364
        private bool boxCloseHandled;

        // Token: 0x04000D25 RID: 3365
        private bool boxLevelWonClosing;

        // Token: 0x04000D26 RID: 3366
        private bool shouldDoNextLevel;

        // Token: 0x04000D27 RID: 3367
        private float tmpDimTime;
    }
}
