using System;
using System.Net;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

// Token: 0x020000DD RID: 221
internal class CoppaController : ViewController, ButtonDelegate, TimelineDelegate
{
    // Token: 0x0600066B RID: 1643 RVA: 0x000312E5 File Offset: 0x0002F4E5
    private new void addViewwithID(View view, int n)
    {
        base.addViewwithID(view, n);
    }

    // Token: 0x0600066C RID: 1644 RVA: 0x000312F0 File Offset: 0x0002F4F0
    public override NSObject initWithParent(ViewController p)
    {
        base.initWithParent(p);
        float num = SCREEN_HEIGHT * 0.03f;
        CoppaView coppaView = (CoppaView)new CoppaView().initFullscreen();
        addViewwithID(coppaView, COPPA_VIEW_MAIN);
        coppaView.blendingMode = 1;
        Image image = Image.Image_createWithResIDQuad(407, 0);
        image.parentAnchor = (image.anchor = 18);
        image.passTransformationsToChilds = false;
        image.scaleY = SCREEN_BG_SCALE_Y;
        image.scaleX = SCREEN_BG_SCALE_X;
        coppaView.addChild(image);
        okb = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310754), COPPA_BUTTON_OK, this);
        Image.setElementPositionWithQuadCenter(okb, 409, 8);
        coppaView.addChild(okb);
        Vector vector = vectMult(Image.getQuadSize(409, 10), 0.2f);
        Text text = Text.createWithFontandString(5, Application.getString(3276952));
        text.setAlignment(2);
        text.scaleX /= 2.2f;
        text.scaleY /= 2.2f;
        Image.setElementPositionWithQuadCenter(text, 409, 10);
        text.y += (float)((double)vector.y * 1.1 * 0.5);
        text.anchor = 18;
        coppaView.addChild(text);
        Image image2 = Image.Image_createWithResIDQuad(409, 5);
        image2.setName("baloon");
        Image.setElementPositionWithQuadCenter(image2, 409, 5);
        coppaView.addChild(image2);
        string text2 = Application.getString(3276953).ToString();
        string[] array = text2.Split(new char[] { '\n' });
        Text[] array2 = new Text[array.Length];
        BaseElement baseElement = (BaseElement)new BaseElement().init();
        image2.y = (float)((double)SCREEN_HEIGHT - 0.75 * (double)SCREEN_HEIGHT) + num;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Replace('\n', ' ');
            array2[i] = new Text().initWithFont(Application.getFont(6));
            array2[i].setString(array[i]);
            array2[i].y = image2.y - 40f + (float)(i * 20);
            array2[i].x = image2.x - 10f;
            array2[i].color = RGBAColor.blackRGBA;
            array2[i].setAlignment(2);
            array2[i].anchor = 18;
            array2[i].scaleX /= 1.5f;
            array2[i].scaleY /= 1.5f;
            baseElement.addChild(array2[i]);
        }
        baseElement.setName(NSS("baloonText"));
        coppaView.addChild(baseElement);
        Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(7);
        timeline.addKeyFrame(KeyFrame.makePos((double)image2.x, (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 3f), (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x - 2f), (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 2f), (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + -3f), (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 3f), (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)image2.x, (double)image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        image2.addTimeline(timeline);
        roll = new Rollbar().Create();
        roll.setName("agePicker");
        Image.setElementPositionWithQuadOffset(roll, 409, 0);
        roll.x -= SCREEN_OFFSET_X;
        roll.y -= SCREEN_OFFSET_Y;
        roll.scrollWithSpeed(-16300f);
        coppaView.addChild(roll);
        okb.x = SCREEN_WIDTH / 2f;
        Button button = new Button().initWithUpElementDownElementandID(Image.Image_createWithResIDQuad(409, 6), Image.Image_createWithResIDQuad(409, 7), COPPA_BUTTON_PRIVACY);
        button.delegateButtonDelegate = this;
        button.y = SCREEN_HEIGHT - 40f;
        button.x += 20f;
        coppaView.addChild(button);
        return this;
    }

    // Token: 0x0600066D RID: 1645 RVA: 0x00031818 File Offset: 0x0002FA18
    public void onButtonPressed(int a)
    {
        CTRSoundMgr._playSound(21);
        if (a != COPPA_BUTTON_OK)
        {
            if (a == COPPA_BUTTON_PRIVACY)
            {
                FlurryAPI.logEvent("COPSCR_PRIVACY_PRESSED", null);
                AndroidAPI.openUrl(Application.getString(1310823));
            }
            return;
        }
        if (ageValid())
        {
            CTRPreferences ctrpreferences = Application.sharedPreferences();
            ctrpreferences.setCoppaShowed(true);
            int num = getSelectedAge();
            ctrpreferences.setCoppaRestricted(num < 13);
            if (num < 13)
            {
                FlurryAPI.enabled = false;
                num = -1;
            }
            trackCoppaParams(num);
            ctrpreferences.setUserAge(num);
            deactivate();
            return;
        }
        BaseElement childWithName = activeView().getChildWithName(NSS("baloon"));
        BaseElement childWithName2 = activeView().getChildWithName(NSS("baloonText"));
        Rollbar rollbar = (Rollbar)activeView().getChildWithName(NSS("agePicker"));
        if (Math.Abs(rollbar.getOffsetY()) > 1f)
        {
            return;
        }
        childWithName.playTimeline(0);
        childWithName2.playTimeline(0);
        rollbar.scrollWithSpeed(100f);
    }

    // Token: 0x0600066E RID: 1646 RVA: 0x0003191C File Offset: 0x0002FB1C
    public void timelineFinished(Timeline tl)
    {
    }

    // Token: 0x0600066F RID: 1647 RVA: 0x0003191E File Offset: 0x0002FB1E
    public void timelinereachedKeyFramewithIndex(Timeline tl, KeyFrame kf, int a)
    {
    }

    // Token: 0x06000670 RID: 1648 RVA: 0x00031920 File Offset: 0x0002FB20
    public override void showView(int n)
    {
        base.showView(n);
    }

    // Token: 0x06000671 RID: 1649 RVA: 0x00031929 File Offset: 0x0002FB29
    private bool ageValid()
    {
        return getSelectedAge() >= minAge && getSelectedAge() <= maxAge;
    }

    // Token: 0x06000672 RID: 1650 RVA: 0x0003194A File Offset: 0x0002FB4A
    private int getSelectedAge()
    {
        return roll.getIndex() + 1;
    }

    // Token: 0x06000673 RID: 1651 RVA: 0x00031959 File Offset: 0x0002FB59
    public override void activate()
    {
        base.activate();
        showView(COPPA_VIEW_MAIN);
    }

    // Token: 0x06000674 RID: 1652 RVA: 0x0003196C File Offset: 0x0002FB6C
    public override void update(float delta)
    {
        base.update(delta);
        okb.color = new RGBAColor(1f, 1f, 1f, (float)((ageValid() ? 1 : 0) + 1) * 0.5f);
    }

    // Token: 0x06000675 RID: 1653 RVA: 0x000319AC File Offset: 0x0002FBAC
    public void trackCoppaParams(int age)
    {
        string text = string.Format("{0}age={1}&app={2}", COPPA_URL, age, getAppName());
        WebRequest.Create(text);
    }

    // Token: 0x06000676 RID: 1654 RVA: 0x000319DC File Offset: 0x0002FBDC
    public NSString getAppName()
    {
        return NSS("ctr");
    }

    // Token: 0x04000BDF RID: 3039
    private Button okb;

    // Token: 0x04000BE0 RID: 3040
    private Rollbar roll;

    // Token: 0x04000BE1 RID: 3041
    private static string COPPA_URL = "http://coppa.zeptodev.com/?";

    // Token: 0x04000BE2 RID: 3042
    private static int speedAccelerator = 2;

    // Token: 0x04000BE3 RID: 3043
    private static int blankSpaceTop = 2;

    // Token: 0x04000BE4 RID: 3044
    private static int blankSpaceBottom = 1;

    // Token: 0x04000BE5 RID: 3045
    private static int minAge = 1;

    // Token: 0x04000BE6 RID: 3046
    private static int maxAge = 99;

    // Token: 0x04000BE7 RID: 3047
    private static int defaultIdx = maxAge / 4;

    // Token: 0x04000BE8 RID: 3048
    private static float friction = 5f;

    // Token: 0x04000BE9 RID: 3049
    private static float minFriction = 0.7f;

    // Token: 0x04000BEA RID: 3050
    private static float cellBounceSpeed = 3f;

    // Token: 0x04000BEB RID: 3051
    private static float boundReturnSpeed = 20f;

    // Token: 0x04000BEC RID: 3052
    private static int COPPA_VIEW_MAIN = 300;

    // Token: 0x04000BED RID: 3053
    private static int COPPA_BUTTON_OK = 0;

    // Token: 0x04000BEE RID: 3054
    private static int COPPA_BUTTON_PRIVACY = 1;
}
