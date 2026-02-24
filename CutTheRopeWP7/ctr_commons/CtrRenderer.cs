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
    internal sealed class CtrRenderer : NSObject
    {
        public static void onSurfaceCreated()
        {
            if (state == 0)
            {
                state = 1;
            }
        }

        public static void onSurfaceChanged(int width, int height)
        {
            Java_com_zeptolab_ctr_CtrRenderer_nativeResize(width, height, false);
        }

        public static void onPause()
        {
            if (state is 2 or 5)
            {
                Java_com_zeptolab_ctr_CtrRenderer_nativePause();
                state = 3;
            }
        }

        public static void onPlaybackFinished()
        {
        }

        public static void onPlaybackStarted()
        {
            state = 5;
        }

        public static void onResume()
        {
            if (state == 3)
            {
                state = 4;
                onResumeTimeStamp = DateTimeJavaHelper.currentTimeMillis();
                DRAW_NOTHING = false;
            }
        }

        public static void onDestroy()
        {
            if (state == 1)
            {
                return;
            }
            Java_com_zeptolab_ctr_CtrRenderer_nativeDestroy();
            state = 1;
        }

        public static void update(float gameTime, IList<TouchLocation> touches)
        {
            Java_com_zeptolab_ctr_CtrRenderer_nativeTouchProcess(touches);
            Java_com_zeptolab_ctr_CtrRenderer_nativeTick(16f);
        }

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
            _ = new Texture2D().initWithPath("ctr_live_tile_0", true);
            _ = new Texture2D().initWithPath("ctr_live_tile_star", true);
        }

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

        public static void Java_com_zeptolab_ctr_CtrRenderer_nativePause()
        {
            if (!gPaused)
            {
                gPaused = true;
                CTRApp.applicationWillResignActive(null);
                CTRSoundMgr._pause();
                Texture2D.suspendAll();
            }
        }

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
                        CTRApp.applicationDidBecomeActive(null);
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
                CTRApp.applicationDidBecomeActive(null);
            }
        }

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

        public static float transformX(float x)
        {
            return (x - VIEW_OFFSET_X) * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        public static float transformY(float y)
        {
            return (y - VIEW_OFFSET_Y) * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

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

        public static bool Java_com_zeptolab_ctr_CtrRenderer_nativeBackPressed()
        {
            GLCanvas glcanvas = Application.sharedCanvas();
            return glcanvas != null && glcanvas.backButtonPressed();
        }

        public static bool Java_com_zeptolab_ctr_CtrRenderer_nativeMenuPressed()
        {
            GLCanvas glcanvas = Application.sharedCanvas();
            return glcanvas != null && glcanvas.menuButtonPressed();
        }

        public static void Java_com_zeptolab_ctr_CtrRenderer_nativeDrawFps(int fps)
        {
            GLCanvas glcanvas = Application.sharedCanvas();
            glcanvas?.drawFPS(fps);
        }

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

        // (get) Token: 0x06000708 RID: 1800 RVA: 0x00038E9E File Offset: 0x0003709E
        public static bool IsPaused => gPaused;

        // (get) Token: 0x06000709 RID: 1801 RVA: 0x00038EA5 File Offset: 0x000370A5
        public static bool IsInit => gApp != null;

        private const int UNKNOWN = 0;

        private const int UNINITIALIZED = 1;

        private const int RUNNING = 2;

        private const int PAUSED = 3;

        private const int NEED_RESUME = 4;

        private const int NEED_PAUSE = 5;

        private const long TICK_DELTA = 16L;

        private const long NANOS_IN_SECOND = 1000000000L;

        private const long NANOS_IN_MILLI = 1000000L;

        private const float MAX_FINGERS_DELTA = 9f;

        private const float MAX_FINGERS_DELTA_SQ = 81f;

        private static int state;

        private static long onResumeTimeStamp;

        private static long playedTicks;

        private static long prevTick;

        private static readonly long DELTA_NANOS = 18181818L;

        private static readonly long DELTA_NANOS_THRES = (long)(DELTA_NANOS * 0.35);

        private static bool DRAW_NOTHING;

        private static CTRApp gApp;

        private static bool gPaused;

        private static readonly long[] fpsDeltas = new long[10];

        private static int fpsDeltasPos;

        public static bool gUseFingerDelta = true;

        private static List<CTRTouchState> prevTouches = new(5);
        private static List<CTRTouchState> prevTouchesTemp = new(5);
        private static readonly List<CTRTouchState> currentTouches = new(5);
    }
}
