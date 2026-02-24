using System;
using System.Collections.Generic;
using ctr_wp7.ctr_commons;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace ctr_wp7.game
{
	// Token: 0x0200008D RID: 141
	internal class CTRRootController : RootController
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0001D26D File Offset: 0x0001B46D
		public virtual void setMap(XMLNode map)
		{
			this.loadedMap = map;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001D276 File Offset: 0x0001B476
		public virtual XMLNode getMap()
		{
			return this.loadedMap;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001D27E File Offset: 0x0001B47E
		public virtual NSString getMapName()
		{
			return this.mapName;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001D286 File Offset: 0x0001B486
		public virtual void setMapName(NSString map)
		{
			NSObject.NSREL(this.mapName);
			this.mapName = map;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001D29A File Offset: 0x0001B49A
		public virtual void setMapsList(Dictionary<string, XMLNode> l)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001D29C File Offset: 0x0001B49C
		public virtual Dictionary<string, XMLNode> getMapsList()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001D2A3 File Offset: 0x0001B4A3
		public virtual int getPack()
		{
			return this.pack;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		public override NSObject initWithParent(ViewController p)
		{
			if (base.initWithParent(p) != null)
			{
				this.hacked = false;
				this.loadedMap = null;
				ResourceMgr resourceMgr = Application.sharedResourceMgr();
				resourceMgr.initLoading();
				resourceMgr.loadPack(ResDataPhoneFull.PACK_STARTUP);
				resourceMgr.loadImmediately();
				StartupController startupController = (StartupController)new StartupController().initWithParent(this);
				this.addChildwithID(startupController, 0);
				NSObject.NSREL(startupController);
				this.viewTransition = -1;
			}
			return this;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001D314 File Offset: 0x0001B514
		public override void activate()
		{
			CTRPreferences.isFirstLaunch();
			base.activate();
			this.activateChild(0);
			Application.sharedCanvas().beforeRender();
			this.activeChild().activeView().draw();
			Application.sharedCanvas().afterRender();
			CTRPreferences.setGameSessionsCount(CTRPreferences.getGameSessionsCount() + 1);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001D364 File Offset: 0x0001B564
		public virtual void deleteMenu()
		{
			ResourceMgr resourceMgr = Application.sharedResourceMgr();
			this.deleteChild(1);
			resourceMgr.freePack(ResDataPhoneFull.PACK_MENU);
			GC.Collect();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001D38E File Offset: 0x0001B58E
		public virtual void disableGameCenter()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001D390 File Offset: 0x0001B590
		public virtual void enableGameCenter()
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001D392 File Offset: 0x0001B592
		public override void suspend()
		{
			this.suspended = true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001D39B File Offset: 0x0001B59B
		public override void resume()
		{
			if (this.inCrystal)
			{
				return;
			}
			this.suspended = false;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		private void initMenu(ResourceMgr rm)
		{
			FrameworkTypes._LOG("start deactivating");
			if (FrameworkTypes.IS_WVGA)
			{
				this.setViewTransition(4);
			}
			LoadingController loadingController = (LoadingController)new LoadingController().initWithParent(this);
			this.addChildwithID(loadingController, 2);
			FrameworkTypes._LOG("start deactivating2");
			FrameworkTypes._LOG("start deactivating3");
			MenuController menuController = (MenuController)new MenuController().initWithParent(this);
			this.addChildwithID(menuController, 1);
			FrameworkTypes._LOG("start deactivating4");
			this.deleteChild(0);
			rm.freePack(ResDataPhoneFull.PACK_STARTUP);
			menuController.viewToShow = MenuController.ViewID.VIEW_MAIN_MENU;
			if (Preferences._getBooleanForKey("PREFS_GAME_CENTER_ENABLED"))
			{
				this.enableGameCenter();
			}
			else
			{
				this.disableGameCenter();
			}
			if (Preferences._getBooleanForKey("IAP_BANNERS"))
			{
				FrameworkTypes.AndroidAPI.disableBanners();
			}
			FrameworkTypes._LOG("activate child menu");
			this.activateChild(1);
			if (CTRPreferences.isFirstLaunch() && SaveMgr.isSaveAvailable())
			{
				menuController.showYesNoPopup(menuController.activeView(), Application.getString(1310778), 19, 20);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public override void onChildDeactivated(int n)
		{
			base.onChildDeactivated(n);
			ResourceMgr resourceMgr = Application.sharedResourceMgr();
			switch (n)
			{
			case 0:
			{
				bool flag = false;
				if (flag)
				{
					if (FrameworkTypes.IS_WVGA)
					{
						this.setViewTransition(4);
					}
					CoppaController coppaController = (CoppaController)new CoppaController().initWithParent(this);
					this.addChildwithID(coppaController, 4);
					this.activateChild(4);
					return;
				}
				this.initMenu(resourceMgr);
				return;
			}
			case 1:
			{
				this.deleteMenu();
				resourceMgr.resourcesDelegate = (LoadingController)this.getChild(2);
				int[] array = null;
				switch (this.pack)
				{
				case 0:
					array = ResDataPhoneFull.PACK_GAME_01;
					break;
				case 1:
					array = ResDataPhoneFull.PACK_GAME_02;
					break;
				case 2:
					array = ResDataPhoneFull.PACK_GAME_03;
					break;
				case 3:
					array = ResDataPhoneFull.PACK_GAME_04;
					break;
				case 4:
					array = ResDataPhoneFull.PACK_GAME_05;
					break;
				case 5:
					array = ResDataPhoneFull.PACK_GAME_06;
					break;
				case 6:
					array = ResDataPhoneFull.PACK_GAME_07;
					break;
				case 7:
					array = ResDataPhoneFull.PACK_GAME_08;
					break;
				case 8:
					array = ResDataPhoneFull.PACK_GAME_09;
					break;
				case 9:
					array = ResDataPhoneFull.PACK_GAME_10;
					break;
				case 10:
					array = ResDataPhoneFull.PACK_GAME_11;
					break;
				case 11:
					array = ResDataPhoneFull.PACK_GAME_12;
					break;
				case 12:
					array = ResDataPhoneFull.PACK_GAME_13;
					break;
				case 13:
					array = ResDataPhoneFull.PACK_GAME_14;
					break;
				}
				resourceMgr.initLoading();
				resourceMgr.loadPack(ResDataPhoneFull.PACK_GAME);
				resourceMgr.loadPack(ResDataPhoneFull.PACK_GAME_NORMAL);
				resourceMgr.loadPack(array);
				resourceMgr.startLoading();
				LoadingController loadingController = (LoadingController)this.getChild(2);
				loadingController.nextController = 0;
				loadingController.MusicToLoad = 59;
				this.activateChild(2);
				return;
			}
			case 2:
			{
				LoadingController loadingController2 = (LoadingController)this.getChild(2);
				int nextController = loadingController2.nextController;
				switch (nextController)
				{
				case 0:
				{
					CTRRootController.setShowGreeting(true);
					GameController gameController = (GameController)new GameController().initWithParent(this);
					this.addChildwithID(gameController, 3);
					this.activateChild(3);
					return;
				}
				case 1:
				case 2:
				case 3:
				case 4:
				{
					MenuController menuController = (MenuController)new MenuController().initWithParent(this);
					this.addChildwithID(menuController, 1);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_01);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_02);
					if (!CTRPreferences.isLiteVersion())
					{
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_03);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_04);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_05);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_06);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_07);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_08);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_09);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_10);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_11);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_12);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_13);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_COVER_14);
					}
					if (FrameworkTypes.IS_WVGA)
					{
						this.setViewTransition(4);
					}
					if (nextController == 1)
					{
						menuController.viewToShow = MenuController.ViewID.VIEW_MAIN_MENU;
					}
					if (nextController == 2 || nextController == 4)
					{
						menuController.viewToShow = MenuController.ViewID.VIEW_LEVEL_SELECT;
					}
					if (nextController == 3)
					{
						menuController.viewToShow = ((this.pack < CTRPreferences.getPacksCount() - 1 || CTRPreferences.isLiteVersion()) ? MenuController.ViewID.VIEW_PACK_SELECT : MenuController.ViewID.VIEW_MOVIE);
					}
					this.activateChild(1);
					if (nextController == 4)
					{
						menuController.showUnlockShareware();
					}
					if (nextController == 3)
					{
						menuController.showNextPack();
					}
					GC.Collect();
					return;
				}
				default:
					return;
				}
				break;
			}
			case 3:
			{
				SaveMgr.backup();
				GameController gameController2 = (GameController)this.getChild(3);
				int exitCode = gameController2.exitCode;
				GameScene gameScene = (GameScene)gameController2.getView(0).getChild(0);
				switch (exitCode)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				{
					this.deleteChild(3);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_NORMAL);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_01);
					resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_02);
					if (!CTRPreferences.isLiteVersion())
					{
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_03);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_04);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_05);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_06);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_07);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_08);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_09);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_10);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_11);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_12);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_13);
						resourceMgr.freePack(ResDataPhoneFull.PACK_GAME_14);
					}
					resourceMgr.resourcesDelegate = (LoadingController)this.getChild(2);
					resourceMgr.initLoading();
					resourceMgr.loadPack(ResDataPhoneFull.PACK_MENU);
					resourceMgr.startLoading();
					LoadingController loadingController3 = (LoadingController)this.getChild(2);
					if (exitCode == 0)
					{
						loadingController3.nextController = 1;
					}
					else if (exitCode == 1)
					{
						loadingController3.nextController = 2;
					}
					else if (exitCode == 3)
					{
						loadingController3.nextController = 4;
					}
					else
					{
						loadingController3.nextController = 3;
					}
					loadingController3.MusicToLoad = 58;
					this.activateChild(2);
					GC.Collect();
					return;
				}
				default:
					return;
				}
				break;
			}
			case 4:
				this.deleteChild(4);
				this.initMenu(resourceMgr);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001D96D File Offset: 0x0001BB6D
		public override void dealloc()
		{
			this.loadedMap = null;
			this.mapName = null;
			base.dealloc();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001D984 File Offset: 0x0001BB84
		public static void checkMapIsValid(char[] data)
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			NSString md = ctr_wp7.iframework.helpers.MathHelper.getMD5(data);
			int num = ctrrootController.getPack();
			int num2 = ctrrootController.getLevel();
			if (!md.isEqualToString(LevelsList.LEVEL_HASHES[num, num2]))
			{
				CTRRootController.setHacked();
				FrameworkTypes._LOG("Map is hacked");
				return;
			}
			FrameworkTypes._LOG("Map is not hacked");
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public static bool isHacked()
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			return ctrrootController.hacked;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001DA00 File Offset: 0x0001BC00
		public static void setHacked()
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			ctrrootController.hacked = true;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001DA20 File Offset: 0x0001BC20
		public static void setInCrystal(bool b)
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			ctrrootController.inCrystal = b;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001DA3F File Offset: 0x0001BC3F
		public static void openFullVersionPage()
		{
			Guide.ShowMarketplace(PlayerIndex.One);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001DA47 File Offset: 0x0001BC47
		public virtual void setPack(int p)
		{
			this.pack = p;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001DA50 File Offset: 0x0001BC50
		public virtual void setLevel(int l)
		{
			this.level = l;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001DA59 File Offset: 0x0001BC59
		public virtual int getLevel()
		{
			return this.level;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001DA61 File Offset: 0x0001BC61
		public virtual void setPicker(bool p)
		{
			this.picker = p;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001DA6A File Offset: 0x0001BC6A
		public virtual bool isPicker()
		{
			return this.picker;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001DA72 File Offset: 0x0001BC72
		public virtual void setSurvival(bool s)
		{
			this.survival = s;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001DA7B File Offset: 0x0001BC7B
		public virtual bool isSurvival()
		{
			return this.survival;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001DA84 File Offset: 0x0001BC84
		public static bool isShowGreeting()
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			return ctrrootController.showGreeting;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		public static void setShowGreeting(bool s)
		{
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			ctrrootController.showGreeting = s;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001DAC3 File Offset: 0x0001BCC3
		public static void postAchievementName(NSString name)
		{
			Scorer.postAchievementName(name);
		}

		// Token: 0x0400097C RID: 2428
		public const int NEXT_GAME = 0;

		// Token: 0x0400097D RID: 2429
		public const int NEXT_MENU = 1;

		// Token: 0x0400097E RID: 2430
		public const int NEXT_PICKER = 2;

		// Token: 0x0400097F RID: 2431
		public const int NEXT_PICKER_NEXT_PACK = 3;

		// Token: 0x04000980 RID: 2432
		public const int NEXT_PICKER_SHOW_UNLOCK = 4;

		// Token: 0x04000981 RID: 2433
		public const int CHILD_START = 0;

		// Token: 0x04000982 RID: 2434
		public const int CHILD_MENU = 1;

		// Token: 0x04000983 RID: 2435
		public const int CHILD_LOADING = 2;

		// Token: 0x04000984 RID: 2436
		public const int CHILD_GAME = 3;

		// Token: 0x04000985 RID: 2437
		public const int CHILD_COPPA = 4;

		// Token: 0x04000986 RID: 2438
		public int pack;

		// Token: 0x04000987 RID: 2439
		private NSString mapName;

		// Token: 0x04000988 RID: 2440
		private XMLNode loadedMap;

		// Token: 0x04000989 RID: 2441
		private int level;

		// Token: 0x0400098A RID: 2442
		private bool picker;

		// Token: 0x0400098B RID: 2443
		private bool survival;

		// Token: 0x0400098C RID: 2444
		private bool inCrystal;

		// Token: 0x0400098D RID: 2445
		private bool showGreeting;

		// Token: 0x0400098E RID: 2446
		private bool hacked;
	}
}
