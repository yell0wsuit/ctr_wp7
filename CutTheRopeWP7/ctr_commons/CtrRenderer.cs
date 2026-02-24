using System;
using System.Collections.Generic;
using System.Diagnostics;

using ctr_wp7.Banner;
using ctr_wp7.ctr_original;
using ctr_wp7.game;
using ctr_wp7.game.remotedata;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x020000EA RID: 234
    internal class CtrRenderer : NSObject
    {
        // Token: 0x060006F2 RID: 1778 RVA: 0x00038525 File Offset: 0x00036725
        public static void onSurfaceCreated()
        {
            if (state == 0)
            {
                state = 1;
            }
        }

        // Token: 0x060006F3 RID: 1779 RVA: 0x00038534 File Offset: 0x00036734
        public static void onSurfaceChanged(int width, int height)
        {
            Java_com_zeptolab_ctr_CtrRenderer_nativeResize(width, height, false);
        }

        // Token: 0x060006F4 RID: 1780 RVA: 0x0003853E File Offset: 0x0003673E
        public static void onPause()
        {
            if (state == 2 || state == 5)
            {
                Java_com_zeptolab_ctr_CtrRenderer_nativePause();
                state = 3;
            }
        }

        // Token: 0x060006F5 RID: 1781 RVA: 0x0003855B File Offset: 0x0003675B
        public static void onPlaybackFinished()
        {
        }

        // Token: 0x060006F6 RID: 1782 RVA: 0x0003855D File Offset: 0x0003675D
        public static void onPlaybackStarted()
        {
            state = 5;
        }

        // Token: 0x060006F7 RID: 1783 RVA: 0x00038565 File Offset: 0x00036765
        public static void onResume()
        {
            if (state == 3)
            {
                state = 4;
                onResumeTimeStamp = DateTimeJavaHelper.currentTimeMillis();
                DRAW_NOTHING = false;
            }
        }

        // Token: 0x060006F8 RID: 1784 RVA: 0x00038585 File Offset: 0x00036785
        public static void onDestroy()
        {
            if (state == 1)
            {
                return;
            }
            Java_com_zeptolab_ctr_CtrRenderer_nativeDestroy();
            state = 1;
        }

        // Token: 0x060006F9 RID: 1785 RVA: 0x0003859C File Offset: 0x0003679C
        public static void update(float gameTime, IList<TouchLocation> touches)
        {
            Java_com_zeptolab_ctr_CtrRenderer_nativeTouchProcess(touches);
            Java_com_zeptolab_ctr_CtrRenderer_nativeTick(16f);
        }

        // Token: 0x060006FA RID: 1786 RVA: 0x000385D4 File Offset: 0x000367D4
        public static void onDrawFrame()
        {
            bool flag = false;
            if (!DRAW_NOTHING && state != 0)
            {
                if (state == 1)
                {
                    state = 2;
                }
                if (state != 3)
                {
                    if (state == 4)
                    {
                        long num = DateTimeJavaHelper.currentTimeMillis();
                        if (num - onResumeTimeStamp >= 500L)
                        {
                            Java_com_zeptolab_ctr_CtrRenderer_nativeResume();
                            Java_com_zeptolab_ctr_CtrRenderer_nativeRender();
                            flag = true;
                            state = 2;
                        }
                    }
                    else if (state == 2)
                    {
                        long num2 = 1000000000L * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                        long num3 = num2 - prevTick;
                        prevTick = num2;
                        if (num3 < 1L)
                        {
                            num3 = 1L;
                        }
                        fpsDeltas[fpsDeltasPos++] = num3;
                        int num4 = fpsDeltas.Length;
                        if (fpsDeltasPos >= num4)
                        {
                            fpsDeltasPos = 0;
                        }
                        long num5 = 0L;
                        for (int i = 0; i < num4; i++)
                        {
                            num5 += fpsDeltas[i];
                        }
                        if (num5 < 1L)
                        {
                            num5 = 1L;
                        }
                        int num6 = (int)(1000000000L * num4 / num5);
                        playedTicks += DELTA_NANOS;
                        if (num2 - playedTicks < DELTA_NANOS_THRES)
                        {
                            if (playedTicks < num2)
                            {
                                playedTicks = num2;
                            }
                        }
                        else if (state == 2)
                        {
                            playedTicks += DELTA_NANOS;
                            if (num2 - playedTicks > DELTA_NANOS_THRES)
                            {
                                playedTicks = num2 - DELTA_NANOS_THRES;
                            }
                        }
                        if (state == 2)
                        {
                            Java_com_zeptolab_ctr_CtrRenderer_nativeRender();
                            Java_com_zeptolab_ctr_CtrRenderer_nativeDrawFps(num6);
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
            if (gApp != null)
            {
                _LOG("Application already created");
                return;
            }
            LANGUAGE = language;
            fmInit();
            RemoteDataManager.initRemoteDataMgr(new RemoteDataManager_Java());
            VideoDataManager.initVideoDataManager();
            gApp = new CTRApp();
            _ = gApp.init();
            gApp.applicationDidFinishLaunching(null);
            _ = new Texture2D().initWithPath("ctr/ctr_live_tile_0", true);
            _ = new Texture2D().initWithPath("ctr/ctr_live_tile_star", true);
        }

        // Token: 0x060006FC RID: 1788 RVA: 0x00038822 File Offset: 0x00036A22
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeDestroy()
        {
            if (gApp == null)
            {
                _LOG("Application already destroyed");
                return;
            }
            Application.sharedSoundMgr().stopAllSounds();
            Application.sharedPreferences().savePreferences();
            NSREL(gApp);
            gApp = null;
            gPaused = false;
        }

        // Token: 0x060006FD RID: 1789 RVA: 0x00038860 File Offset: 0x00036A60
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativePause()
        {
            if (!gPaused)
            {
                gPaused = true;
                gApp?.applicationWillResignActive(null);
                CTRSoundMgr._pause();
                Texture2D.suspendAll();
            }
        }

        // Token: 0x060006FE RID: 1790 RVA: 0x0003888C File Offset: 0x00036A8C
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeResume()
        {
            if (gPaused)
            {
                Texture2D.suspendAll();
                Texture2D.resumeAll();
                gPaused = false;
                int activeChildID = Application.sharedRootController().activeChildID;
                if (activeChildID == 3)
                {
                    GameController gameController = (GameController)Application.sharedRootController().getCurrentController();
                    if (!gameController.isGamePaused)
                    {
                        gameController.setPaused(true);
                        gApp?.applicationDidBecomeActive(null);
                        return;
                    }
                }
                else if (activeChildID == 1)
                {
                    MenuController menuController = (MenuController)Application.sharedRootController().getCurrentController();
                    if (menuController != null)
                    {
                        PromoBanner promoBanner = (PromoBanner)menuController.activeView().getChildWithName("promoBanner");
                        promoBanner?.reset();
                    }
                }
                CTRSoundMgr._unpause();
                gApp?.applicationDidBecomeActive(null);
            }
        }

        // Token: 0x060006FF RID: 1791 RVA: 0x00038944 File Offset: 0x00036B44
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeResize(int width, int height, bool isLowMem)
        {
            REAL_SCREEN_WIDTH = width;
            REAL_SCREEN_HEIGHT = height;
            SCREEN_RATIO = REAL_SCREEN_HEIGHT / REAL_SCREEN_WIDTH;
            IS_WVGA = width > 500 || height > 500;
            IS_QVGA = width < 280 || height < 280;
            if (isLowMem)
            {
                IS_WVGA = false;
            }
            VIEW_SCREEN_WIDTH = REAL_SCREEN_WIDTH;
            VIEW_SCREEN_HEIGHT = SCREEN_HEIGHT * REAL_SCREEN_WIDTH / SCREEN_WIDTH;
            if (VIEW_SCREEN_HEIGHT > REAL_SCREEN_HEIGHT)
            {
                VIEW_SCREEN_HEIGHT = REAL_SCREEN_HEIGHT;
                VIEW_SCREEN_WIDTH = SCREEN_WIDTH * REAL_SCREEN_HEIGHT / SCREEN_HEIGHT;
            }
            VIEW_OFFSET_X = (width - VIEW_SCREEN_WIDTH) / 2f;
            VIEW_OFFSET_Y = (height - VIEW_SCREEN_HEIGHT) / 2f;
            SCREEN_HEIGHT_EXPANDED = SCREEN_HEIGHT * REAL_SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
            SCREEN_WIDTH_EXPANDED = SCREEN_WIDTH * REAL_SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
            SCREEN_OFFSET_Y = (SCREEN_HEIGHT_EXPANDED - SCREEN_HEIGHT) / 2f;
            SCREEN_OFFSET_X = (SCREEN_WIDTH_EXPANDED - SCREEN_WIDTH) / 2f;
            SCREEN_BG_SCALE_Y = SCREEN_HEIGHT_EXPANDED / SCREEN_HEIGHT;
            SCREEN_BG_SCALE_X = SCREEN_WIDTH_EXPANDED / SCREEN_WIDTH;
            if (IS_WVGA)
            {
                SCREEN_WIDE_BG_SCALE_Y = (float)(SCREEN_HEIGHT_EXPANDED * 1.5 / 800.0);
                SCREEN_WIDE_BG_SCALE_X = SCREEN_BG_SCALE_X;
                return;
            }
            SCREEN_WIDE_BG_SCALE_Y = SCREEN_BG_SCALE_Y;
            SCREEN_WIDE_BG_SCALE_X = SCREEN_BG_SCALE_X;
        }

        // Token: 0x06000700 RID: 1792 RVA: 0x00038AD8 File Offset: 0x00036CD8
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeRender()
        {
            OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
            OpenGL.glClear(0);
            if (gApp == null || gPaused)
            {
                return;
            }
            Application.sharedRootController().performDraw();
        }

        // Token: 0x06000701 RID: 1793 RVA: 0x00038B2D File Offset: 0x00036D2D
        public static float transformX(float x)
        {
            return (x - VIEW_OFFSET_X) * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        // Token: 0x06000702 RID: 1794 RVA: 0x00038B42 File Offset: 0x00036D42
        public static float transformY(float y)
        {
            return (y - VIEW_OFFSET_Y) * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

        // Token: 0x06000703 RID: 1795 RVA: 0x00038B58 File Offset: 0x00036D58
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeTouchProcess(IList<TouchLocation> touches)
        {
            if (touches.Count > 0)
            {
                currentTouches.Clear();
                prevTouchesTemp.Clear();
                foreach (TouchLocation touchLocation in touches)
                {
                    if (touchLocation.State == TouchLocationState.Moved)
                    {
                        using (List<CTRTouchState>.Enumerator enumerator2 = prevTouches.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                CTRTouchState ctrtouchState = enumerator2.Current;
                                if (ctrtouchState.Id == touchLocation.Id)
                                {
                                    if (!gUseFingerDelta || (ctrtouchState.Moved && touches.Count <= 1))
                                    {
                                        CTRTouchState ctrtouchState2 = new()
                                        {
                                            Id = touchLocation.Id,
                                            Position = touchLocation.Position,
                                            State = touchLocation.State,
                                            Moved = ctrtouchState.Moved
                                        };
                                        currentTouches.Add(ctrtouchState2);
                                        prevTouchesTemp.Add(ctrtouchState2);
                                        break;
                                    }
                                    float num = touchLocation.Position.X - ctrtouchState.Position.X;
                                    float num2 = touchLocation.Position.Y - ctrtouchState.Position.Y;
                                    if ((num != 0f || num2 != 0f) && (num * num) + (num2 * num2) >= 81f)
                                    {
                                        CTRTouchState ctrtouchState3 = new()
                                        {
                                            Id = touchLocation.Id,
                                            Position = touchLocation.Position,
                                            State = touchLocation.State,
                                            Moved = true
                                        };
                                        currentTouches.Add(ctrtouchState3);
                                        prevTouchesTemp.Add(ctrtouchState3);
                                        break;
                                    }
                                    prevTouchesTemp.Add(ctrtouchState);
                                    break;
                                }
                            }
                            continue;
                        }
                    }
                    CTRTouchState ctrtouchState4 = new()
                    {
                        Id = touchLocation.Id,
                        Position = touchLocation.Position,
                        State = touchLocation.State,
                        Moved = false
                    };
                    currentTouches.Add(ctrtouchState4);
                    prevTouchesTemp.Add(ctrtouchState4);
                }
                Application.sharedCanvas().touchesEndedwithEvent(currentTouches);
                Application.sharedCanvas().touchesBeganwithEvent(currentTouches);
                Application.sharedCanvas().touchesMovedwithEvent(currentTouches);
            }
            prevTouches.Clear();
            List<CTRTouchState> list = prevTouchesTemp;
            prevTouchesTemp = prevTouches;
            prevTouches = list;
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
            glcanvas?.drawFPS(fps);
        }

        // Token: 0x06000707 RID: 1799 RVA: 0x00038E68 File Offset: 0x00037068
        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeTick(float delta)
        {
            if (gApp == null || gPaused)
            {
                return;
            }
            float num = delta / 1000f;
            NSTimer.fireTimers(num);
            Application.sharedRootController().performTick(num);
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000708 RID: 1800 RVA: 0x00038E9E File Offset: 0x0003709E
        public static bool IsPaused => gPaused;

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000709 RID: 1801 RVA: 0x00038EA5 File Offset: 0x000370A5
        public static bool IsInit => gApp != null;

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
        private static int state;

        // Token: 0x04000C93 RID: 3219
        private static long onResumeTimeStamp;

        // Token: 0x04000C94 RID: 3220
        private static long playedTicks;

        // Token: 0x04000C95 RID: 3221
        private static long prevTick;

        // Token: 0x04000C96 RID: 3222
        private static long DELTA_NANOS = 18181818L;

        // Token: 0x04000C97 RID: 3223
        private static long DELTA_NANOS_THRES = (long)(DELTA_NANOS * 0.35);

        // Token: 0x04000C98 RID: 3224
        private static bool DRAW_NOTHING;

        // Token: 0x04000C99 RID: 3225
        private static CTRApp gApp;

        // Token: 0x04000C9A RID: 3226
        private static bool gPaused;

        // Token: 0x04000C9B RID: 3227
        private static long[] fpsDeltas = new long[10];

        // Token: 0x04000C9C RID: 3228
        private static int fpsDeltasPos;

        // Token: 0x04000C9D RID: 3229
        public static bool gUseFingerDelta = true;

        // Token: 0x04000C9E RID: 3230
        private static List<CTRTouchState> prevTouches = new(5);
        private static List<CTRTouchState> prevTouchesTemp = new(5);
        private static List<CTRTouchState> currentTouches = new(5);
    }
}
