using System;
using System.Net;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

internal sealed class CoppaController : ViewController, ButtonDelegate, TimelineDelegate
{
    private new void addViewwithID(View view, int n)
    {
        base.addViewwithID(view, n);
    }

    public override NSObject initWithParent(ViewController p)
    {
        _ = base.initWithParent(p);
        float num = SCREEN_HEIGHT * 0.03f;
        CoppaView coppaView = (CoppaView)new CoppaView().initFullscreen();
        addViewwithID(coppaView, COPPA_VIEW_MAIN);
        coppaView.blendingMode = 1;
        Image image = Image.Image_createWithResIDQuad(407, 0);
        image.parentAnchor = image.anchor = 18;
        image.passTransformationsToChilds = false;
        image.scaleY = SCREEN_BG_SCALE_Y;
        image.scaleX = SCREEN_BG_SCALE_X;
        _ = coppaView.addChild(image);
        okb = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310754), COPPA_BUTTON_OK, this);
        Image.setElementPositionWithQuadCenter(okb, 409, 8);
        _ = coppaView.addChild(okb);
        Vector vector = vectMult(Image.getQuadSize(409, 10), 0.2f);
        Text text = Text.createWithFontandString(5, Application.getString(3276952));
        text.setAlignment(2);
        text.scaleX /= 2.2f;
        text.scaleY /= 2.2f;
        Image.setElementPositionWithQuadCenter(text, 409, 10);
        text.y += (float)(vector.y * 1.1 * 0.5);
        text.anchor = 18;
        _ = coppaView.addChild(text);
        Image image2 = Image.Image_createWithResIDQuad(409, 5);
        image2.setName("baloon");
        Image.setElementPositionWithQuadCenter(image2, 409, 5);
        _ = coppaView.addChild(image2);
        string text2 = Application.getString(3276953).ToString();
        string[] array = text2.Split(['\n']);
        Text[] array2 = new Text[array.Length];
        BaseElement baseElement = (BaseElement)new BaseElement().init();
        image2.y = (float)(SCREEN_HEIGHT - (0.75 * SCREEN_HEIGHT)) + num;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Replace('\n', ' ');
            array2[i] = new Text().initWithFont(Application.getFont(6));
            array2[i].setString(array[i]);
            array2[i].y = image2.y - 40f + (i * 20);
            array2[i].x = image2.x - 10f;
            array2[i].color = RGBAColor.blackRGBA;
            array2[i].setAlignment(2);
            array2[i].anchor = 18;
            array2[i].scaleX /= 1.5f;
            array2[i].scaleY /= 1.5f;
            _ = baseElement.addChild(array2[i]);
        }
        baseElement.setName(NSS("baloonText"));
        _ = coppaView.addChild(baseElement);
        Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(7);
        timeline.addKeyFrame(KeyFrame.makePos(image2.x, image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 3f), image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x - 2f), image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 2f), image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + -3f), image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos((double)(image2.x + 3f), image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        timeline.addKeyFrame(KeyFrame.makePos(image2.x, image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
        _ = image2.addTimeline(timeline);
        roll = new Rollbar().Create();
        roll.setName("agePicker");
        Image.setElementPositionWithQuadOffset(roll, 409, 0);
        roll.x -= SCREEN_OFFSET_X;
        roll.y -= SCREEN_OFFSET_Y;
        roll.scrollWithSpeed(-16300f);
        _ = coppaView.addChild(roll);
        okb.x = SCREEN_WIDTH / 2f;
        Button button = new Button().initWithUpElementDownElementandID(Image.Image_createWithResIDQuad(409, 6), Image.Image_createWithResIDQuad(409, 7), COPPA_BUTTON_PRIVACY);
        button.delegateButtonDelegate = this;
        button.y = SCREEN_HEIGHT - 40f;
        button.x += 20f;
        _ = coppaView.addChild(button);
        return this;
    }

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

    public void timelineFinished(Timeline tl)
    {
    }

    public void timelinereachedKeyFramewithIndex(Timeline tl, KeyFrame kf, int a)
    {
    }

    public override void showView(int n)
    {
        base.showView(n);
    }

    private bool ageValid()
    {
        return getSelectedAge() >= minAge && getSelectedAge() <= maxAge;
    }

    private int getSelectedAge()
    {
        return roll.getIndex() + 1;
    }

    public override void activate()
    {
        base.activate();
        showView(COPPA_VIEW_MAIN);
    }

    public override void update(float delta)
    {
        base.update(delta);
        okb.color = new RGBAColor(1f, 1f, 1f, ((ageValid() ? 1 : 0) + 1) * 0.5f);
    }

    public static void trackCoppaParams(int age)
    {
        string text = string.Format("{0}age={1}&app={2}", COPPA_URL, age, getAppName());
        _ = WebRequest.Create(text);
    }

    public static NSString getAppName()
    {
        return NSS("ctr");
    }

    private Button okb;

    private Rollbar roll;

    private static readonly string COPPA_URL = "http://coppa.zeptodev.com/?";

    private static readonly int speedAccelerator = 2;

    private static readonly int blankSpaceTop = 2;

    private static readonly int blankSpaceBottom = 1;

    private static readonly int minAge = 1;

    private static readonly int maxAge = 99;

    private static readonly int defaultIdx = maxAge / 4;

    private static readonly float friction = 5f;

    private static readonly float minFriction = 0.7f;

    private static readonly float cellBounceSpeed = 3f;

    private static readonly float boundReturnSpeed = 20f;

    private static readonly int COPPA_VIEW_MAIN = 300;

    private static readonly int COPPA_BUTTON_OK;

    private static readonly int COPPA_BUTTON_PRIVACY = 1;
}
