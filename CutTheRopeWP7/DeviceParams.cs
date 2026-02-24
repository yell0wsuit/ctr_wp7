using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using Microsoft.Xna.Framework.GamerServices;

// Token: 0x020000F2 RID: 242
internal class DeviceParams
{
    // Token: 0x06000741 RID: 1857 RVA: 0x0003AA5C File Offset: 0x00038C5C
    public bool isEnglishISO(string s)
    {
        return (s[0] == 'e' && s[1] == 's') || s == "en-US";
    }

    // Token: 0x06000742 RID: 1858 RVA: 0x0003AA84 File Offset: 0x00038C84
    private string getDeviceLanguage2L()
    {
        return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }

    // Token: 0x06000743 RID: 1859 RVA: 0x0003AA90 File Offset: 0x00038C90
    private string getDeviceLanguageName()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }

    // Token: 0x06000744 RID: 1860 RVA: 0x0003AAA4 File Offset: 0x00038CA4
    public int getTimeZoneOffset()
    {
        return TimeZoneInfo.Local.BaseUtcOffset.Hours;
    }

    // Token: 0x06000745 RID: 1861 RVA: 0x0003AAC4 File Offset: 0x00038CC4
    public bool isEnglishDevice()
    {
        int timeZoneOffset = this.getTimeZoneOffset();
        return this.isEnglishISO(this.getDeviceLanguageName()) && timeZoneOffset >= -11 && timeZoneOffset <= -4 && timeZoneOffset != -9;
    }

    // Token: 0x06000746 RID: 1862 RVA: 0x0003AAF8 File Offset: 0x00038CF8
    private string string_isEnglishDevice()
    {
        if (this.isEnglishDevice())
        {
            return "Yes";
        }
        return "No";
    }

    // Token: 0x06000747 RID: 1863 RVA: 0x0003AB18 File Offset: 0x00038D18
    public void ShowMessageBox()
    {
        string text = "Is english device";
        string text2 = this.string_isEnglishDevice();
        List<string> list = new List<string>();
        list.Add("Ok");
        list.Add("Cancel");
        Guide.BeginShowMessageBox(text, text2, list, 0, MessageBoxIcon.Error, delegate (IAsyncResult asyncResult)
        {
            Guide.EndShowMessageBox(asyncResult);
        }, null);
    }

    // Token: 0x04000CD7 RID: 3287
    public int owner_years_old;
}
