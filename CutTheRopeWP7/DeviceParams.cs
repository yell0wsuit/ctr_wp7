using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using Microsoft.Xna.Framework.GamerServices;

internal sealed class DeviceParams
{
    public static bool isEnglishISO(string s)
    {
        return (s[0] == 'e' && s[1] == 's') || s == "en-US";
    }

    private static string getDeviceLanguage2L()
    {
        return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }

    private static string getDeviceLanguageName()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }

    public static int getTimeZoneOffset()
    {
        return TimeZoneInfo.Local.BaseUtcOffset.Hours;
    }

    public static bool isEnglishDevice()
    {
        int timeZoneOffset = getTimeZoneOffset();
        return isEnglishISO(getDeviceLanguageName()) && timeZoneOffset >= -11 && timeZoneOffset <= -4 && timeZoneOffset != -9;
    }

    private static string string_isEnglishDevice()
    {
        return isEnglishDevice() ? "Yes" : "No";
    }

    public static void ShowMessageBox()
    {
        string text = "Is english device";
        string text2 = string_isEnglishDevice();
        List<string> list = ["Ok", "Cancel"];
        _ = Guide.BeginShowMessageBox(text, text2, list, 0, MessageBoxIcon.Error, delegate (IAsyncResult asyncResult)
        {
            _ = Guide.EndShowMessageBox(asyncResult);
        }, null);
    }

    public int owner_years_old;
}
