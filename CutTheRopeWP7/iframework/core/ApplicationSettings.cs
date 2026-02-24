using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    internal sealed class ApplicationSettings : NSObject
    {
        public static int getInt(int s)
        {
            return s == 5 ? 50 : throw new NotImplementedException();
        }

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

        public enum AppSettings
        {
            APP_SETTING_INTERACTION_ENABLED,
            APP_SETTING_MULTITOUCH_ENABLED,
            APP_SETTING_STATUSBAR_HIDDEN,
            APP_SETTING_MAIN_LOOP_TIMERED,
            APP_SETTING_FPS_METER_ENABLED,
            APP_SETTING_FPS,
            APP_SETTING_ORIENTATION,
            APP_SETTING_LOCALIZATION_ENABLED,
            APP_SETTING_LOCALE,
            APP_SETTING_RETINA_SUPPORT,
            APP_SETTINGS_CUSTOM
        }
    }
}
