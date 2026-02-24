using System;
using System.Collections.Generic;
using System.Diagnostics;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework
{
    // Token: 0x02000005 RID: 5
    internal class FrameworkTypes : MathHelper
    {
        // Token: 0x06000050 RID: 80 RVA: 0x00005504 File Offset: 0x00003704
        public static float[] toFloatArray(Quad2D[] quads)
        {
            float[] array = new float[quads.Length * 8];
            for (int i = 0; i < quads.Length; i++)
            {
                quads[i].toFloatArray().CopyTo(array, i * 8);
            }
            return array;
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00005544 File Offset: 0x00003744
        public static float[] toFloatArray(Quad3D[] quads)
        {
            float[] array = new float[quads.Length * 12];
            for (int i = 0; i < quads.Length; i++)
            {
                quads[i].toFloatArray().CopyTo(array, i * 12);
            }
            return array;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00005585 File Offset: 0x00003785
        public static Rectangle MakeRectangle(double xParam, double yParam, double width, double height)
        {
            return MakeRectangle((float)xParam, (float)yParam, (float)width, (float)height);
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00005594 File Offset: 0x00003794
        public static Rectangle MakeRectangle(float xParam, float yParam, float width, float height)
        {
            return new Rectangle(xParam, yParam, width, height);
        }

        // Token: 0x06000054 RID: 84 RVA: 0x0000559F File Offset: 0x0000379F
        public static float transformToRealX(float x)
        {
            return (x * VIEW_SCREEN_WIDTH / SCREEN_WIDTH) + VIEW_OFFSET_X;
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000055B4 File Offset: 0x000037B4
        public static float transformToRealY(float y)
        {
            return (y * VIEW_SCREEN_HEIGHT / SCREEN_HEIGHT) + VIEW_OFFSET_Y;
        }

        // Token: 0x06000056 RID: 86 RVA: 0x000055C9 File Offset: 0x000037C9
        public static float transformFromRealX(float x)
        {
            return (x - VIEW_OFFSET_X) * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        // Token: 0x06000057 RID: 87 RVA: 0x000055DE File Offset: 0x000037DE
        public static float transformFromRealY(float y)
        {
            return (y - VIEW_OFFSET_Y) * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

        // Token: 0x06000058 RID: 88 RVA: 0x000055F3 File Offset: 0x000037F3
        public static float transformToRealW(float w)
        {
            return w * VIEW_SCREEN_WIDTH / SCREEN_WIDTH;
        }

        // Token: 0x06000059 RID: 89 RVA: 0x00005602 File Offset: 0x00003802
        public static float transformToRealH(float h)
        {
            return h * VIEW_SCREEN_HEIGHT / SCREEN_HEIGHT;
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00005611 File Offset: 0x00003811
        public static float transformFromRealW(float w)
        {
            return w * SCREEN_WIDTH / VIEW_SCREEN_WIDTH;
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00005620 File Offset: 0x00003820
        public static float transformFromRealH(float h)
        {
            return h * SCREEN_HEIGHT / VIEW_SCREEN_HEIGHT;
        }

        // Token: 0x0600005C RID: 92 RVA: 0x0000562F File Offset: 0x0000382F
        public static void _LOG(string str)
        {
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00005631 File Offset: 0x00003831
        public static float WVGAH(double H, double L)
        {
            return (float)(IS_WVGA ? H : L);
        }

        // Token: 0x0600005E RID: 94 RVA: 0x0000563F File Offset: 0x0000383F
        public static float WVGAD(double V)
        {
            return (float)(IS_WVGA ? (V * 2.0) : V);
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00005657 File Offset: 0x00003857
        public static float CHOOSE3(double P1, double P2, double P3)
        {
            return WVGAH(P2, P1);
        }

        // Token: 0x040006CF RID: 1743
        public const int BLENDING_MODE_SRC_ALPHA = 0;

        // Token: 0x040006D0 RID: 1744
        public const int BLENDING_MODE_ONE = 1;

        // Token: 0x040006D1 RID: 1745
        public const int BLENDING_MODE_ADDITIVE = 2;

        // Token: 0x040006D2 RID: 1746
        public const int UNDEFINED = -1;

        // Token: 0x040006D3 RID: 1747
        public const float FLOAT_PRECISION = 1E-06f;

        // Token: 0x040006D4 RID: 1748
        public const int LEFT = 1;

        // Token: 0x040006D5 RID: 1749
        public const int HCENTER = 2;

        // Token: 0x040006D6 RID: 1750
        public const int RIGHT = 4;

        // Token: 0x040006D7 RID: 1751
        public const int TOP = 8;

        // Token: 0x040006D8 RID: 1752
        public const int VCENTER = 16;

        // Token: 0x040006D9 RID: 1753
        public const int BOTTOM = 32;

        // Token: 0x040006DA RID: 1754
        public const int CENTER = 18;

        // Token: 0x040006DB RID: 1755
        public const bool YES = true;

        // Token: 0x040006DC RID: 1756
        public const bool NO = false;

        // Token: 0x040006DD RID: 1757
        public const bool TRUE = true;

        // Token: 0x040006DE RID: 1758
        public const bool FALSE = false;

        // Token: 0x040006DF RID: 1759
        public const int GL_COLOR_BUFFER_BIT = 0;

        // Token: 0x040006E0 RID: 1760
        public static float SCREEN_WIDTH = 320f;

        // Token: 0x040006E1 RID: 1761
        public static float SCREEN_HEIGHT = 480f;

        // Token: 0x040006E2 RID: 1762
        public static float REAL_SCREEN_WIDTH = 480f;

        // Token: 0x040006E3 RID: 1763
        public static float REAL_SCREEN_HEIGHT = 800f;

        // Token: 0x040006E4 RID: 1764
        public static float SCREEN_OFFSET_Y;

        // Token: 0x040006E5 RID: 1765
        public static float SCREEN_OFFSET_X;

        // Token: 0x040006E6 RID: 1766
        public static float SCREEN_BG_SCALE_Y = 1f;

        // Token: 0x040006E7 RID: 1767
        public static float SCREEN_BG_SCALE_X = 1f;

        // Token: 0x040006E8 RID: 1768
        public static float SCREEN_WIDE_BG_SCALE_Y = 1f;

        // Token: 0x040006E9 RID: 1769
        public static float SCREEN_WIDE_BG_SCALE_X = 1f;

        // Token: 0x040006EA RID: 1770
        public static float SCREEN_HEIGHT_EXPANDED = SCREEN_HEIGHT;

        // Token: 0x040006EB RID: 1771
        public static float SCREEN_WIDTH_EXPANDED = SCREEN_WIDTH;

        // Token: 0x040006EC RID: 1772
        public static float VIEW_SCREEN_WIDTH = 480f;

        // Token: 0x040006ED RID: 1773
        public static float VIEW_SCREEN_HEIGHT = 800f;

        // Token: 0x040006EE RID: 1774
        public static float VIEW_OFFSET_X;

        // Token: 0x040006EF RID: 1775
        public static float VIEW_OFFSET_Y;

        // Token: 0x040006F0 RID: 1776
        public static float SCREEN_RATIO;

        // Token: 0x040006F1 RID: 1777
        private static readonly float PORTRAIT_SCREEN_WIDTH = 480f;

        // Token: 0x040006F2 RID: 1778
        private static readonly float PORTRAIT_SCREEN_HEIGHT = 320f;

        // Token: 0x040006F3 RID: 1779
        public static bool IS_IPAD;

        // Token: 0x040006F4 RID: 1780
        public static bool IS_RETINA;

        // Token: 0x040006F5 RID: 1781
        public static bool IS_WVGA;

        // Token: 0x040006F6 RID: 1782
        public static bool IS_QVGA;

        // Token: 0x02000006 RID: 6
        public sealed class FlurryAPI
        {
            // Token: 0x06000062 RID: 98 RVA: 0x00005741 File Offset: 0x00003941
            private static void EnableAnalytics(bool k)
            {
                enabled = k;
            }

            // Token: 0x06000063 RID: 99 RVA: 0x0000574C File Offset: 0x0000394C
            public static void logEvent(string s, List<string> Parameters_Double_String = null)
            {
                if (!enabled)
                {
                    return;
                }
            }

            // Token: 0x06000064 RID: 100 RVA: 0x000057AB File Offset: 0x000039AB
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

            // Token: 0x06000065 RID: 101 RVA: 0x000057D4 File Offset: 0x000039D4
            public static void logEventwithParams(string str, Dictionary<string, string> _params, bool flurryon = true, bool mixpanelon = false, bool attachManufacturerInfo = false)
            {
                if (_params == null)
                {
                    return;
                }
                injectGlobalLoggingParams(_params);
            }

            // Token: 0x06000066 RID: 102 RVA: 0x00005854 File Offset: 0x00003A54
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

            // Token: 0x06000067 RID: 103 RVA: 0x000058E0 File Offset: 0x00003AE0
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

            // Token: 0x040006F7 RID: 1783
            public static bool enabled = true;
        }

        // Token: 0x02000007 RID: 7
        public sealed class AndroidAPI
        {
            // Token: 0x0600006A RID: 106 RVA: 0x00005992 File Offset: 0x00003B92
            public static void openUrl(NSString url)
            {
                openUrl(url.ToString());
            }

            // Token: 0x0600006B RID: 107 RVA: 0x000059A0 File Offset: 0x00003BA0
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

            // Token: 0x0600006C RID: 108 RVA: 0x000059DC File Offset: 0x00003BDC
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

            // Token: 0x0600006D RID: 109 RVA: 0x00005A1F File Offset: 0x00003C1F
            public static void showBanner()
            {
            }

            // Token: 0x0600006E RID: 110 RVA: 0x00005A21 File Offset: 0x00003C21
            public static void showVideoBanner()
            {
            }

            // Token: 0x0600006F RID: 111 RVA: 0x00005A23 File Offset: 0x00003C23
            public static void hideBanner()
            {
            }

            // Token: 0x06000070 RID: 112 RVA: 0x00005A25 File Offset: 0x00003C25
            public static void disableBanners()
            {
            }

            // Token: 0x06000071 RID: 113 RVA: 0x00005A27 File Offset: 0x00003C27
            public static void exitApp()
            {
                App.Quit();
            }
        }
    }
}
