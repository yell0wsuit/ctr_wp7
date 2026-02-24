using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    // Token: 0x020000B6 RID: 182
    internal sealed class ApplicationSettings : NSObject
    {
        // Token: 0x06000531 RID: 1329 RVA: 0x00026026 File Offset: 0x00024226
        public static int getInt(int s)
        {
            if (s == 5)
            {
                return 50;
            }
            throw new NotImplementedException();
        }

        // Token: 0x06000532 RID: 1330 RVA: 0x00026034 File Offset: 0x00024234
        public static NSString getString(int s)
        {
            if (s != 8)
            {
                return NSS("");
            }
            switch (LANGUAGE)
            {
                case Language.LANG_EN:
                    return NSS("en");
                case Language.LANG_RU:
                    return NSS("ru");
                case Language.LANG_DE:
                    return NSS("de");
                case Language.LANG_FR:
                    return NSS("fr");
                case Language.LANG_ZH:
                    return NSS("zh");
                case Language.LANG_JA:
                    return NSS("ja");
                case Language.LANG_KO:
                    return NSS("ko");
                case Language.LANG_ES:
                    return NSS("es");
                case Language.LANG_IT:
                    return NSS("it");
                case Language.LANG_NL:
                    return NSS("nl");
                case Language.LANG_BR:
                    return NSS("br");
                default:
                    return NSS("en");
            }
        }

        // Token: 0x020000B7 RID: 183
        public enum AppSettings
        {
            // Token: 0x04000A74 RID: 2676
            APP_SETTING_INTERACTION_ENABLED,
            // Token: 0x04000A75 RID: 2677
            APP_SETTING_MULTITOUCH_ENABLED,
            // Token: 0x04000A76 RID: 2678
            APP_SETTING_STATUSBAR_HIDDEN,
            // Token: 0x04000A77 RID: 2679
            APP_SETTING_MAIN_LOOP_TIMERED,
            // Token: 0x04000A78 RID: 2680
            APP_SETTING_FPS_METER_ENABLED,
            // Token: 0x04000A79 RID: 2681
            APP_SETTING_FPS,
            // Token: 0x04000A7A RID: 2682
            APP_SETTING_ORIENTATION,
            // Token: 0x04000A7B RID: 2683
            APP_SETTING_LOCALIZATION_ENABLED,
            // Token: 0x04000A7C RID: 2684
            APP_SETTING_LOCALE,
            // Token: 0x04000A7D RID: 2685
            APP_SETTING_RETINA_SUPPORT,
            // Token: 0x04000A7E RID: 2686
            APP_SETTINGS_CUSTOM
        }
    }
}
