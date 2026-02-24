using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

using Microsoft.Xna.Framework.GamerServices;

internal sealed class CoppaLoader
{
    private CoppaLoader()
    {
        hideCoppaPopupIsExplicit = false;
        hideCoppaPopup = false;
        fetchXml();
    }

    private static void fetchXml()
    {
    }

    public static bool getHideCoppaPopupIsExplicit()
    {
        return hideCoppaPopupIsExplicit;
    }

    public static bool getHideCoppaPopup()
    {
        return hideCoppaPopup;
    }

    public static void setHideCoppaPopupIsExplicit(bool a)
    {
        hideCoppaPopupIsExplicit = a;
    }

    public static void setHideCoppaPopup(bool a)
    {
        hideCoppaPopup = a;
    }

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

    public static NSString GetCollectedParameters()
    {
        LinkBuilder linkBuilder = new("http://bms.zeptolab.com/feeder/csp?");
        ServerDataManager.public_InjectParameters(linkBuilder);
        return new NSString(linkBuilder.ToString());
    }

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

    private static bool hideCoppaPopupIsExplicit;

    private static bool hideCoppaPopup;
}
