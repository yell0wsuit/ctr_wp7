using System;
using System.Collections.Generic;
using System.Diagnostics;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework
{
    internal class FrameworkTypes : MathHelper
    {
        public static float[] toFloatArray(Quad2D[] quads)
        {
            float[] array = new float[quads.Length * 8];
            for (int i = 0; i < quads.Length; i++)
            {
                quads[i].toFloatArray().CopyTo(array, i * 8);
            }
            return array;
        }

        public static float[] toFloatArray(Quad3D[] quads)
        {
            float[] array = new float[quads.Length * 12];
            for (int i = 0; i < quads.Length; i++)
            {
                quads[i].toFloatArray().CopyTo(array, i * 12);
            }
            return array;
        }

        public static Rectangle MakeRectangle(double xParam, double yParam, double width, double height)
        {
            return MakeRectangle((float)xParam, (float)yParam, (float)width, (float)height);
        }

        public static Rectangle MakeRectangle(float xParam, float yParam, float width, float height)
        {
            return new Rectangle(xParam, yParam, width, height);
        }

        public static float transformToRealX(float x)
        {
            return (x * VIEW_SCREEN_WIDTH / SCREEN_WIDTH) + VIEW_OFFSET_X;
        }

        public static float transformToRealY(float y)
        {
            return (y * VIEW_SCREEN_HEIGHT / SCREEN_HEIGHT) + VIEW_OFFSET_Y;
        }

        public static float transformFromRealX(float x)
        {
            return (x - VIEW_OFFSET_X) * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        public static float transformFromRealY(float y)
        {
            return (y - VIEW_OFFSET_Y) * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

        public static float transformToRealW(float w)
        {
            return w * VIEW_SCREEN_WIDTH / SCREEN_WIDTH;
        }

        public static float transformToRealH(float h)
        {
            return h * VIEW_SCREEN_HEIGHT / SCREEN_HEIGHT;
        }

        public static float transformFromRealW(float w)
        {
            return w * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        public static float transformFromRealH(float h)
        {
            return h * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

        public static void _LOG(string str)
        {
        }

        public static float WVGAH(double H, double L)
        {
            return (float)(IS_WVGA ? H : L);
        }

        public static float WVGAD(double V)
        {
            return (float)(IS_WVGA ? (V * 2.0) : V);
        }

        public static float CHOOSE3(double P1, double P2, double P3)
        {
            return WVGAH(P2, P1);
        }

        public const int BLENDING_MODE_SRC_ALPHA = 0;

        public const int BLENDING_MODE_ONE = 1;

        public const int BLENDING_MODE_ADDITIVE = 2;

        public const int UNDEFINED = -1;

        public const float FLOAT_PRECISION = 1E-06f;

        public const int LEFT = 1;

        public const int HCENTER = 2;

        public const int RIGHT = 4;

        public const int TOP = 8;

        public const int VCENTER = 16;

        public const int BOTTOM = 32;

        public const int CENTER = 18;

        public const bool YES = true;

        public const bool NO = false;

        public const bool TRUE = true;

        public const bool FALSE = false;

        public const int GL_COLOR_BUFFER_BIT = 0;

        public static float SCREEN_WIDTH = 320f;

        public static float SCREEN_HEIGHT = 480f;

        public static float REAL_SCREEN_WIDTH = 480f;

        public static float REAL_SCREEN_HEIGHT = 800f;

        public static float SCREEN_OFFSET_Y;

        public static float SCREEN_OFFSET_X;

        public static float SCREEN_BG_SCALE_Y = 1f;

        public static float SCREEN_BG_SCALE_X = 1f;

        public static float SCREEN_WIDE_BG_SCALE_Y = 1f;

        public static float SCREEN_WIDE_BG_SCALE_X = 1f;

        public static float SCREEN_HEIGHT_EXPANDED = SCREEN_HEIGHT;

        public static float SCREEN_WIDTH_EXPANDED = SCREEN_WIDTH;

        public static float VIEW_SCREEN_WIDTH = 480f;

        public static float VIEW_SCREEN_HEIGHT = 800f;

        public static float VIEW_OFFSET_X;

        public static float VIEW_OFFSET_Y;

        public static float SCREEN_RATIO;

        private static readonly float PORTRAIT_SCREEN_WIDTH = 480f;

        private static readonly float PORTRAIT_SCREEN_HEIGHT = 320f;

        public static bool IS_IPAD;

        public static bool IS_RETINA;

        public static bool IS_WVGA;

        public static bool IS_QVGA;

        public sealed class FlurryAPI
        {
            private static void EnableAnalytics(bool k)
            {
                enabled = k;
            }

            public static void logEvent(string s, List<string> Parameters_Double_String = null)
            {
                if (!enabled)
                {
                    return;
                }
            }

            public static void logEventwithParams(string s, List<string> Parameters_Double_String, bool flurryon = true, bool mixpanelon = false, bool attachManufacturerInfo = false)
            {
                if (enabled)
                {
                    Parameters_Double_String ??= [];
                    injectGlobalLoggingParams(Parameters_Double_String);
                    if (flurryon)
                    {
                        logEvent(s, Parameters_Double_String);
                    }
                }
            }

            public static void logEventwithParams(string str, Dictionary<string, string> _params, bool flurryon = true, bool mixpanelon = false, bool attachManufacturerInfo = false)
            {
                if (_params == null)
                {
                    return;
                }
                injectGlobalLoggingParams(_params);
            }

            public static void injectGlobalLoggingParams(Dictionary<string, string> feedme)
            {
                int num = 0;
                int i = 0;
                int packsCount = CTRPreferences.getPacksCount();
                while (i <= packsCount)
                {
                    if (CTRPreferences.getUnlockedForPackLevel(i, 0) != UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
                    {
                        num++;
                    }
                    i++;
                }
                feedme["levels_won"] = CTRPreferences.getTotalCompletedLevels().ToString();
                feedme["stars_collected"] = CTRPreferences.getTotalStars().ToString();
                feedme["times_played"] = CTRPreferences.getGameSessionsCount().ToString();
                feedme["boxes_unlocked"] = num.ToString();
            }

            public static void injectGlobalLoggingParams(List<string> feedme)
            {
                int num = 0;
                int i = 0;
                int packsCount = CTRPreferences.getPacksCount();
                while (i <= packsCount)
                {
                    if (CTRPreferences.getUnlockedForPackLevel(i, 0) != UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
                    {
                        num++;
                    }
                    i++;
                }
                feedme.Add("levels_won");
                feedme.Add(CTRPreferences.getTotalCompletedLevels().ToString());
                feedme.Add("stars_collected");
                feedme.Add(CTRPreferences.getTotalStars().ToString());
                feedme.Add("times_played");
                feedme.Add(CTRPreferences.getGameSessionsCount().ToString());
                feedme.Add("boxes_unlocked");
                feedme.Add(num.ToString());
            }

            public static bool enabled = true;
        }

        public sealed class AndroidAPI
        {
            public static void openUrl(NSString url)
            {
                openUrl(url.ToString());
            }

            public static void openUrl(string url)
            {
                try
                {
                    _ = Process.Start(new ProcessStartInfo(url)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception)
                {
                }
            }

            public static void share(NSString title, NSString subject, NSString text, bool isDrawing = false)
            {
                try
                {
                    _ = Process.Start(new ProcessStartInfo(text.ToString())
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception)
                {
                }
            }

            public static void showBanner()
            {
            }

            public static void showVideoBanner()
            {
            }

            public static void hideBanner()
            {
            }

            public static void disableBanners()
            {
            }

            public static void exitApp()
            {
                App.Quit();
            }
        }
    }
}
