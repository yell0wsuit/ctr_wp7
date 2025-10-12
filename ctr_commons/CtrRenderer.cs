using System;
using System.Collections.Generic;
using System.Diagnostics;
using ctre_wp7.Banner;
using ctre_wp7.ctr_original;
using ctre_wp7.game;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;
using ctre_wp7.wp7utilities;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctre_wp7.ctr_commons
{
	// Token: 0x020000EA RID: 234
	internal class CtrRenderer : NSObject
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x00038525 File Offset: 0x00036725
		public static void onSurfaceCreated()
		{
			if (CtrRenderer.state == 0)
			{
				CtrRenderer.state = 1;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00038534 File Offset: 0x00036734
		public static void onSurfaceChanged(int width, int height)
		{
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeResize(width, height, false);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0003853E File Offset: 0x0003673E
		public static void onPause()
		{
			if (CtrRenderer.state == 2 || CtrRenderer.state == 5)
			{
				CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativePause();
				CtrRenderer.state = 3;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0003855B File Offset: 0x0003675B
		public static void onPlaybackFinished()
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0003855D File Offset: 0x0003675D
		public static void onPlaybackStarted()
		{
			CtrRenderer.state = 5;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00038565 File Offset: 0x00036765
		public static void onResume()
		{
			if (CtrRenderer.state == 3)
			{
				CtrRenderer.state = 4;
				CtrRenderer.onResumeTimeStamp = DateTimeJavaHelper.currentTimeMillis();
				CtrRenderer.DRAW_NOTHING = false;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00038585 File Offset: 0x00036785
		public static void onDestroy()
		{
			if (CtrRenderer.state == 1)
			{
				return;
			}
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeDestroy();
			CtrRenderer.state = 1;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0003859C File Offset: 0x0003679C
		public static void update(float gameTime, TouchCollection touches)
		{
			try
			{
				CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeTouchProcess(touches);
				CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeTick(16f);
			}
			catch (GameUpdateRequiredException)
			{
				App.MakeUpdatePopup();
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000385D4 File Offset: 0x000367D4
		public static void onDrawFrame()
		{
			bool flag = false;
			if (!CtrRenderer.DRAW_NOTHING && CtrRenderer.state != 0)
			{
				if (CtrRenderer.state == 1)
				{
					CtrRenderer.state = 2;
				}
				if (CtrRenderer.state != 3)
				{
					if (CtrRenderer.state == 4)
					{
						long num = DateTimeJavaHelper.currentTimeMillis();
						if (num - CtrRenderer.onResumeTimeStamp >= 500L)
						{
							CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeResume();
							CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeRender();
							flag = true;
							CtrRenderer.state = 2;
						}
					}
					else if (CtrRenderer.state == 2)
					{
						long num2 = 1000000000L * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
						long num3 = num2 - CtrRenderer.prevTick;
						CtrRenderer.prevTick = num2;
						if (num3 < 1L)
						{
							num3 = 1L;
						}
						CtrRenderer.fpsDeltas[CtrRenderer.fpsDeltasPos++] = num3;
						int num4 = CtrRenderer.fpsDeltas.Length;
						if (CtrRenderer.fpsDeltasPos >= num4)
						{
							CtrRenderer.fpsDeltasPos = 0;
						}
						long num5 = 0L;
						for (int i = 0; i < num4; i++)
						{
							num5 += CtrRenderer.fpsDeltas[i];
						}
						if (num5 < 1L)
						{
							num5 = 1L;
						}
						int num6 = (int)(1000000000L * (long)num4 / num5);
						CtrRenderer.playedTicks += CtrRenderer.DELTA_NANOS;
						if (num2 - CtrRenderer.playedTicks < CtrRenderer.DELTA_NANOS_THRES)
						{
							if (CtrRenderer.playedTicks < num2)
							{
								CtrRenderer.playedTicks = num2;
							}
						}
						else if (CtrRenderer.state == 2)
						{
							CtrRenderer.playedTicks += CtrRenderer.DELTA_NANOS;
							if (num2 - CtrRenderer.playedTicks > CtrRenderer.DELTA_NANOS_THRES)
							{
								CtrRenderer.playedTicks = num2 - CtrRenderer.DELTA_NANOS_THRES;
							}
						}
						if (CtrRenderer.state == 2)
						{
							CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeRender();
							CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeDrawFps(num6);
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				try
				{
					OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
					OpenGL.glClear(0);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000387AC File Offset: 0x000369AC
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeInit(Language language)
		{
			if (CtrRenderer.gApp != null)
			{
				FrameworkTypes._LOG("Application already created");
				return;
			}
			ResDataPhoneFull.LANGUAGE = language;
			MathHelper.fmInit();
			RemoteDataManager.initRemoteDataMgr(new RemoteDataManager_Java());
			CtrRenderer.gApp = new CTRApp();
			CtrRenderer.gApp.init();
			CtrRenderer.gApp.applicationDidFinishLaunching(null);
			new Texture2D().initWithPath("ctr/ctr_live_tile_0", true);
			new Texture2D().initWithPath("ctr/ctr_live_tile_star", true);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00038822 File Offset: 0x00036A22
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeDestroy()
		{
			if (CtrRenderer.gApp == null)
			{
				FrameworkTypes._LOG("Application already destroyed");
				return;
			}
			Application.sharedSoundMgr().stopAllSounds();
			Application.sharedPreferences().savePreferences();
			NSObject.NSREL(CtrRenderer.gApp);
			CtrRenderer.gApp = null;
			CtrRenderer.gPaused = false;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00038860 File Offset: 0x00036A60
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativePause()
		{
			if (!CtrRenderer.gPaused)
			{
				CtrRenderer.gPaused = true;
				if (CtrRenderer.gApp != null)
				{
					CtrRenderer.gApp.applicationWillResignActive(null);
				}
				CTRSoundMgr._pause();
				Texture2D.suspendAll();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0003888C File Offset: 0x00036A8C
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeResume()
		{
			if (CtrRenderer.gPaused)
			{
				Texture2D.suspendAll();
				Texture2D.resumeAll();
				CtrRenderer.gPaused = false;
				int activeChildID = Application.sharedRootController().activeChildID;
				if (activeChildID == 3)
				{
					GameController gameController = (GameController)Application.sharedRootController().getCurrentController();
					if (!gameController.isGamePaused)
					{
						gameController.setPaused(true);
						if (CtrRenderer.gApp != null)
						{
							CtrRenderer.gApp.applicationDidBecomeActive(null);
						}
						return;
					}
				}
				else if (activeChildID == 1)
				{
					MenuController menuController = (MenuController)Application.sharedRootController().getCurrentController();
					if (menuController != null)
					{
						PromoBanner promoBanner = (PromoBanner)menuController.activeView().getChildWithName("promoBanner");
						if (promoBanner != null)
						{
							promoBanner.reset();
						}
					}
				}
				CTRSoundMgr._unpause();
				if (CtrRenderer.gApp != null)
				{
					CtrRenderer.gApp.applicationDidBecomeActive(null);
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00038944 File Offset: 0x00036B44
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeResize(int width, int height, bool isLowMem)
		{
			FrameworkTypes.REAL_SCREEN_WIDTH = (float)width;
			FrameworkTypes.REAL_SCREEN_HEIGHT = (float)height;
			FrameworkTypes.SCREEN_RATIO = FrameworkTypes.REAL_SCREEN_HEIGHT / FrameworkTypes.REAL_SCREEN_WIDTH;
			FrameworkTypes.IS_WVGA = width > 500 || height > 500;
			FrameworkTypes.IS_QVGA = width < 280 || height < 280;
			if (isLowMem)
			{
				FrameworkTypes.IS_WVGA = false;
			}
			FrameworkTypes.VIEW_SCREEN_WIDTH = FrameworkTypes.REAL_SCREEN_WIDTH;
			FrameworkTypes.VIEW_SCREEN_HEIGHT = FrameworkTypes.SCREEN_HEIGHT * FrameworkTypes.REAL_SCREEN_WIDTH / FrameworkTypes.SCREEN_WIDTH;
			if (FrameworkTypes.VIEW_SCREEN_HEIGHT > FrameworkTypes.REAL_SCREEN_HEIGHT)
			{
				FrameworkTypes.VIEW_SCREEN_HEIGHT = FrameworkTypes.REAL_SCREEN_HEIGHT;
				FrameworkTypes.VIEW_SCREEN_WIDTH = FrameworkTypes.SCREEN_WIDTH * FrameworkTypes.REAL_SCREEN_HEIGHT / FrameworkTypes.SCREEN_HEIGHT;
			}
			FrameworkTypes.VIEW_OFFSET_X = ((float)width - FrameworkTypes.VIEW_SCREEN_WIDTH) / 2f;
			FrameworkTypes.VIEW_OFFSET_Y = ((float)height - FrameworkTypes.VIEW_SCREEN_HEIGHT) / 2f;
			FrameworkTypes.SCREEN_HEIGHT_EXPANDED = FrameworkTypes.SCREEN_HEIGHT * FrameworkTypes.REAL_SCREEN_HEIGHT / FrameworkTypes.VIEW_SCREEN_HEIGHT;
			FrameworkTypes.SCREEN_WIDTH_EXPANDED = FrameworkTypes.SCREEN_WIDTH * FrameworkTypes.REAL_SCREEN_WIDTH / FrameworkTypes.VIEW_SCREEN_WIDTH;
			FrameworkTypes.SCREEN_OFFSET_Y = (FrameworkTypes.SCREEN_HEIGHT_EXPANDED - FrameworkTypes.SCREEN_HEIGHT) / 2f;
			FrameworkTypes.SCREEN_OFFSET_X = (FrameworkTypes.SCREEN_WIDTH_EXPANDED - FrameworkTypes.SCREEN_WIDTH) / 2f;
			FrameworkTypes.SCREEN_BG_SCALE_Y = FrameworkTypes.SCREEN_HEIGHT_EXPANDED / FrameworkTypes.SCREEN_HEIGHT;
			FrameworkTypes.SCREEN_BG_SCALE_X = FrameworkTypes.SCREEN_WIDTH_EXPANDED / FrameworkTypes.SCREEN_WIDTH;
			if (FrameworkTypes.IS_WVGA)
			{
				FrameworkTypes.SCREEN_WIDE_BG_SCALE_Y = (float)((double)FrameworkTypes.SCREEN_HEIGHT_EXPANDED * 1.5 / 800.0);
				FrameworkTypes.SCREEN_WIDE_BG_SCALE_X = FrameworkTypes.SCREEN_BG_SCALE_X;
				return;
			}
			FrameworkTypes.SCREEN_WIDE_BG_SCALE_Y = FrameworkTypes.SCREEN_BG_SCALE_Y;
			FrameworkTypes.SCREEN_WIDE_BG_SCALE_X = FrameworkTypes.SCREEN_BG_SCALE_X;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00038AD8 File Offset: 0x00036CD8
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeRender()
		{
			OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
			OpenGL.glClear(0);
			if (CtrRenderer.gApp == null || CtrRenderer.gPaused)
			{
				return;
			}
			Application.sharedRootController().performDraw();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00038B2D File Offset: 0x00036D2D
		public static float transformX(float x)
		{
			return (x - FrameworkTypes.VIEW_OFFSET_X) * FrameworkTypes.SCREEN_WIDTH / FrameworkTypes.VIEW_SCREEN_WIDTH;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00038B42 File Offset: 0x00036D42
		public static float transformY(float y)
		{
			return (y - FrameworkTypes.VIEW_OFFSET_Y) * FrameworkTypes.SCREEN_HEIGHT / FrameworkTypes.VIEW_SCREEN_HEIGHT;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00038B58 File Offset: 0x00036D58
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeTouchProcess(TouchCollection touches)
		{
			if (touches.Count > 0)
			{
				CtrRenderer.currentTouches.Clear();
				CtrRenderer.prevTouchesTemp.Clear();
				foreach (TouchLocation touchLocation in touches)
				{
					if (touchLocation.State == TouchLocationState.Moved)
					{
						using (List<CTRTouchState>.Enumerator enumerator2 = CtrRenderer.prevTouches.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								CTRTouchState ctrtouchState = enumerator2.Current;
								if (ctrtouchState.Id == touchLocation.Id)
								{
									if (!CtrRenderer.gUseFingerDelta || (ctrtouchState.Moved && touches.Count <= 1))
									{
										CTRTouchState ctrtouchState2 = new CTRTouchState();
										ctrtouchState2.Id = touchLocation.Id;
										ctrtouchState2.Position = touchLocation.Position;
										ctrtouchState2.State = touchLocation.State;
										ctrtouchState2.Moved = ctrtouchState.Moved;
										CtrRenderer.currentTouches.Add(ctrtouchState2);
										CtrRenderer.prevTouchesTemp.Add(ctrtouchState2);
										break;
									}
									float num = touchLocation.Position.X - ctrtouchState.Position.X;
									float num2 = touchLocation.Position.Y - ctrtouchState.Position.Y;
									if ((num != 0f || num2 != 0f) && num * num + num2 * num2 >= 81f)
									{
										CTRTouchState ctrtouchState3 = new CTRTouchState();
										ctrtouchState3.Id = touchLocation.Id;
										ctrtouchState3.Position = touchLocation.Position;
										ctrtouchState3.State = touchLocation.State;
										ctrtouchState3.Moved = true;
										CtrRenderer.currentTouches.Add(ctrtouchState3);
										CtrRenderer.prevTouchesTemp.Add(ctrtouchState3);
										break;
									}
									CtrRenderer.prevTouchesTemp.Add(ctrtouchState);
									break;
								}
							}
							continue;
						}
					}
					CTRTouchState ctrtouchState4 = new CTRTouchState();
					ctrtouchState4.Id = touchLocation.Id;
					ctrtouchState4.Position = touchLocation.Position;
					ctrtouchState4.State = touchLocation.State;
					ctrtouchState4.Moved = false;
					CtrRenderer.currentTouches.Add(ctrtouchState4);
					CtrRenderer.prevTouchesTemp.Add(ctrtouchState4);
				}
				Application.sharedCanvas().touchesEndedwithEvent(CtrRenderer.currentTouches);
				Application.sharedCanvas().touchesBeganwithEvent(CtrRenderer.currentTouches);
				Application.sharedCanvas().touchesMovedwithEvent(CtrRenderer.currentTouches);
			}
			CtrRenderer.prevTouches.Clear();
			List<CTRTouchState> list = CtrRenderer.prevTouchesTemp;
			CtrRenderer.prevTouchesTemp = CtrRenderer.prevTouches;
			CtrRenderer.prevTouches = list;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00038E08 File Offset: 0x00037008
		public static bool Java_com_zeptolab_ctr_CtrRenderer_nativeBackPressed()
		{
			GLCanvas glcanvas = Application.sharedCanvas();
			return glcanvas != null && glcanvas.backButtonPressed();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00038E28 File Offset: 0x00037028
		public static bool Java_com_zeptolab_ctr_CtrRenderer_nativeMenuPressed()
		{
			GLCanvas glcanvas = Application.sharedCanvas();
			return glcanvas != null && glcanvas.menuButtonPressed();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00038E48 File Offset: 0x00037048
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeDrawFps(int fps)
		{
			GLCanvas glcanvas = Application.sharedCanvas();
			if (glcanvas != null)
			{
				glcanvas.drawFPS(fps);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00038E68 File Offset: 0x00037068
		public static void Java_com_zeptolab_ctr_CtrRenderer_nativeTick(float delta)
		{
			if (CtrRenderer.gApp == null || CtrRenderer.gPaused)
			{
				return;
			}
			float num = delta / 1000f;
			NSTimer.fireTimers(num);
			Application.sharedRootController().performTick(num);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00038E9E File Offset: 0x0003709E
		public static bool IsPaused
		{
			get
			{
				return CtrRenderer.gPaused;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00038EA5 File Offset: 0x000370A5
		public static bool IsInit
		{
			get
			{
				return CtrRenderer.gApp != null;
			}
		}

		// Token: 0x04000C87 RID: 3207
		private const int UNKNOWN = 0;

		// Token: 0x04000C88 RID: 3208
		private const int UNINITIALIZED = 1;

		// Token: 0x04000C89 RID: 3209
		private const int RUNNING = 2;

		// Token: 0x04000C8A RID: 3210
		private const int PAUSED = 3;

		// Token: 0x04000C8B RID: 3211
		private const int NEED_RESUME = 4;

		// Token: 0x04000C8C RID: 3212
		private const int NEED_PAUSE = 5;

		// Token: 0x04000C8D RID: 3213
		private const long TICK_DELTA = 16L;

		// Token: 0x04000C8E RID: 3214
		private const long NANOS_IN_SECOND = 1000000000L;

		// Token: 0x04000C8F RID: 3215
		private const long NANOS_IN_MILLI = 1000000L;

		// Token: 0x04000C90 RID: 3216
		private const float MAX_FINGERS_DELTA = 9f;

		// Token: 0x04000C91 RID: 3217
		private const float MAX_FINGERS_DELTA_SQ = 81f;

		// Token: 0x04000C92 RID: 3218
		private static int state = 0;

		// Token: 0x04000C93 RID: 3219
		private static long onResumeTimeStamp = 0L;

		// Token: 0x04000C94 RID: 3220
		private static long playedTicks = 0L;

		// Token: 0x04000C95 RID: 3221
		private static long prevTick = 0L;

		// Token: 0x04000C96 RID: 3222
		private static long DELTA_NANOS = 18181818L;

		// Token: 0x04000C97 RID: 3223
		private static long DELTA_NANOS_THRES = (long)((double)CtrRenderer.DELTA_NANOS * 0.35);

		// Token: 0x04000C98 RID: 3224
		private static bool DRAW_NOTHING = false;

		// Token: 0x04000C99 RID: 3225
		private static CTRApp gApp;

		// Token: 0x04000C9A RID: 3226
		private static bool gPaused = false;

		// Token: 0x04000C9B RID: 3227
		private static long[] fpsDeltas = new long[10];

		// Token: 0x04000C9C RID: 3228
		private static int fpsDeltasPos = 0;

		// Token: 0x04000C9D RID: 3229
		public static bool gUseFingerDelta = true;

		// Token: 0x04000C9E RID: 3230
		private static List<CTRTouchState> prevTouches = new List<CTRTouchState>(5);

		// Token: 0x04000C9F RID: 3231
		private static List<CTRTouchState> prevTouchesTemp = new List<CTRTouchState>(5);

		// Token: 0x04000CA0 RID: 3232
		private static List<CTRTouchState> currentTouches = new List<CTRTouchState>(5);
	}
}
