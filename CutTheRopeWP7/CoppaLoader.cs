using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

using Microsoft.Xna.Framework.GamerServices;

// Token: 0x02000072 RID: 114
internal sealed class CoppaLoader
{
    // Token: 0x0600036C RID: 876 RVA: 0x00015DBF File Offset: 0x00013FBF
    private CoppaLoader()
    {
        hideCoppaPopupIsExplicit = false;
        hideCoppaPopup = false;
        fetchXml();
    }

    // Token: 0x0600036D RID: 877 RVA: 0x00015DD9 File Offset: 0x00013FD9
    private static void fetchXml()
    {
    }

    // Token: 0x0600036E RID: 878 RVA: 0x00015DDB File Offset: 0x00013FDB
    public static bool getHideCoppaPopupIsExplicit()
    {
        return hideCoppaPopupIsExplicit;
    }

    // Token: 0x0600036F RID: 879 RVA: 0x00015DE2 File Offset: 0x00013FE2
    public static bool getHideCoppaPopup()
    {
        return hideCoppaPopup;
    }

    // Token: 0x06000370 RID: 880 RVA: 0x00015DE9 File Offset: 0x00013FE9
    public static void setHideCoppaPopupIsExplicit(bool a)
    {
        hideCoppaPopupIsExplicit = a;
    }

    // Token: 0x06000371 RID: 881 RVA: 0x00015DF1 File Offset: 0x00013FF1
    public static void setHideCoppaPopup(bool a)
    {
        hideCoppaPopup = a;
    }

    // Token: 0x06000372 RID: 882 RVA: 0x00015E40 File Offset: 0x00014040
    private static NSString getPossibleBannersResolutions()
    {
        int num = (int)FrameworkTypes.CHOOSE3(0.0, 1.0, 2.0);
        int[,] array = new int[,]
        {
            { 320, 200 },
            { 480, 300 },
            { 800, 400 }
        };
        NSString nsstring = new(string.Format("curtain:%dx%d", array[num, 0], array[num, 1]));
        int num2 = (FrameworkTypes.SCREEN_RATIO >= 1.5555555555555556) ? 1 : 0;
        int[,] array2 = new int[,]
        {
            { 768, 1024 },
            { 640, 1136 }
        };
        nsstring = new NSString(string.Format("%s,interstitial:%dx%d", nsstring.ToString(), array2[num2, 0], array2[num2, 1]));
        int num3 = (int)FrameworkTypes.CHOOSE3(0.0, 1.0, 2.0);
        int[,] array3 = new int[,]
        {
            { 60, 60 },
            { 90, 90 },
            { 150, 150 }
        };
        return new NSString(string.Format("%@,more_games:%dx%d", nsstring.ToString(), array3[num3, 0], array3[num3, 1]));
    }

    // Token: 0x06000373 RID: 883 RVA: 0x00015F74 File Offset: 0x00014174
    public static NSString GetCollectedParameters()
    {
        LinkBuilder linkBuilder = new("http://bms.zeptolab.com/feeder/csp?");
        ServerDataManager.public_InjectParameters(linkBuilder);
        return new NSString(linkBuilder.ToString());
    }

    // Token: 0x06000374 RID: 884 RVA: 0x00015FA8 File Offset: 0x000141A8
    public static void ShowMessageBox()
    {
        string text = "some";
        string text2 = GetCollectedParameters().ToString();
        List<string> list = ["Ok", "Cancel"];
        _ = Guide.BeginShowMessageBox(text, text2, list, 0, MessageBoxIcon.Error, delegate (IAsyncResult asyncResult)
        {
            _ = Guide.EndShowMessageBox(asyncResult);
        }, null);
        FrameworkTypes.AndroidAPI.openUrl(GetCollectedParameters().ToString());
    }

    // Token: 0x040008F9 RID: 2297
    private static bool hideCoppaPopupIsExplicit;

    // Token: 0x040008FA RID: 2298
    private static bool hideCoppaPopup;
}
