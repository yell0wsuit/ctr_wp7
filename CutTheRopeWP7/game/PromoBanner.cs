using System;
using System.Collections.Generic;
using System.Linq;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.sfe;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000D2 RID: 210
    internal class PromoBanner : BaseElement, ButtonDelegate, TimelineDelegate
    {
        // Token: 0x06000627 RID: 1575 RVA: 0x0002ED84 File Offset: 0x0002CF84
        public override NSObject init()
        {
            if (base.init() != null)
            {
                width = (int)SCREEN_WIDTH_EXPANDED;
                height = (int)SCREEN_HEIGHT_EXPANDED;
                scaleX = SCREEN_WIDTH_EXPANDED / SCREEN_WIDTH;
                x = SCREEN_OFFSET_X;
                parentAnchor = 18;
                anchor = 18;
                fadeElement = (Processing)new Processing().initWithLoading(false);
                _ = addChild(fadeElement);
                promoBanner = createMainBanner();
                checkSwitchButtons();
                _ = addChild(promoBanner);
                if (CTRPreferences.shouldShowPromo())
                {
                    CTRPreferences.disablePromoBanner();
                    openMainPromo();
                }
            }
            return this;
        }

        // Token: 0x06000628 RID: 1576 RVA: 0x0002EE35 File Offset: 0x0002D035
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x06000629 RID: 1577 RVA: 0x0002EE40 File Offset: 0x0002D040
        public virtual BaseElement createBanner()
        {
            BaseElement banner = Application.sharedPreferences().remoteDataManager.getBanner();
            if (banner != null)
            {
                banner.parentAnchor = 9;
                Image.setElementPositionWithQuadOffset(banner, 78, 0);
                banner.setName("banner");
                return banner;
            }
            Image image = Image.Image_createWithResIDQuad(78, 0);
            Image.setElementPositionWithQuadOffset(image, 78, 0);
            image.parentAnchor = 9;
            image.setName("banner");
            return image;
        }

        // Token: 0x0600062A RID: 1578 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
        public virtual BaseElement createMainBanner()
        {
            int banner_OFFSET = BANNER_OFFSET;
            setName("promoBanner");
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            baseElement.setName("container");
            baseElement.parentAnchor = (baseElement.anchor = 10);
            BaseElement baseElement2 = createBanner();
            _ = baseElement.addChild(baseElement2);
            RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
            rectangleElement.width = (int)SCREEN_WIDTH_EXPANDED;
            rectangleElement.height = 100;
            rectangleElement.parentAnchor = 10;
            rectangleElement.anchor = 34;
            rectangleElement.color = RGBAColor.blackRGBA;
            _ = baseElement.addChild(rectangleElement);
            Image image = Image.Image_createWithResIDQuad(77, 3);
            Image.setElementPositionWithQuadOffset(image, 77, 3);
            image.parentAnchor = 9;
            _ = baseElement.addChild(image);
            Text text = Text.createWithFontandString(5, Application.getString(1310733));
            text.parentAnchor = (text.anchor = 17);
            text.x = 10f;
            text.y = 2f;
            _ = image.addChild(text);
            Button button = MenuController.createButtonWithImageQuadIDDelegate(77, 4, 1, this);
            button.parentAnchor = (button.anchor = 9);
            Image.setElementPositionWithQuadCenter(button, 77, 4);
            _ = image.addChild(button);
            Image image2 = Image.Image_createWithResIDQuad(77, 2);
            image2.parentAnchor = 9;
            Image.setElementPositionWithQuadOffset(image2, 77, 2);
            image2.y -= 1f;
            image2.scaleX = 1.01f;
            _ = baseElement.addChild(image2);
            Button button2 = MenuController.createButtonWithImageQuadIDDelegate(77, 5, 3, this);
            button2.parentAnchor = (button2.anchor = 9);
            Image.setElementPositionWithQuadCenter(button2, 77, 5);
            button2.setName("promoSwitchRightButton");
            _ = baseElement.addChild(button2);
            Button button3 = MenuController.createButtonWithImageQuadIDDelegate(77, 5, 2, this);
            button3.parentAnchor = (button3.anchor = 9);
            button3.scaleX = -1f;
            Image.setElementPositionWithQuadCenter(button3, 77, 10);
            button3.setName("promoSwitchLeftButton");
            _ = baseElement.addChild(button3);
            Vector quadCenter = Image.getQuadCenter(77, 8);
            Vector quadCenter2 = Image.getQuadCenter(77, 9);
            quadCenter2.y += (float)banner_OFFSET;
            quadCenter.y += (float)banner_OFFSET;
            quadCenter2.x += 10f;
            hook = (ConstraintedPoint)new ConstraintedPoint().init();
            hook.pos.x = quadCenter2.x;
            hook.pos.y = quadCenter2.y;
            hookStart = (ConstraintedPoint)new ConstraintedPoint().init();
            hookStart.pos.x = quadCenter.x;
            hookStart.pos.y = quadCenter.y;
            bungee = (Bungee)new Bungee().initWithHeadAtXYTailAtTXTYandLength(hookStart, quadCenter.x, quadCenter.y, hook, quadCenter2.x, quadCenter2.y, 60f);
            bungee.bungeeAnchor.pin = bungee.bungeeAnchor.pos;
            bungee.dontDrawRedStretch = true;
            bungee.width++;
            bungee.alternateColors = true;
            hook.setWeight(1f);
            BungeeDrawer bungeeDrawer = (BungeeDrawer)new BungeeDrawer().init();
            bungeeDrawer.parentAnchor = (bungeeDrawer.anchor = 9);
            bungeeDrawer.bungee = bungee;
            bungeeDrawer.tailPos = hook.pos;
            bungeeDrawer.down = false;
            bungeeDrawer.delegateButtonDelegate = this;
            bungeeDrawer.bid = 1;
            bungeeDrawer.fadeElement = fadeElement;
            _ = baseElement.addChild(bungeeDrawer);
            hookButton = Image.Image_createWithResIDQuad(77, 1);
            hookButton.anchor = 10;
            hookButton.passTransformationsToChilds = false;
            _ = bungeeDrawer.addChild(hookButton);
            hookButton.x = hook.pos.x;
            hookButton.y = hook.pos.y - 5f;
            hookButton.rotationCenterY = (float)(-(float)hookButton.height / 2 + 5);
            promoMainHidden = true;
            baseElement.width = (int)SCREEN_WIDTH_EXPANDED;
            baseElement.height = image.height + baseElement2.height + image2.height;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makePos(0, banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, (double)banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
            _ = baseElement.addTimeline(timeline);
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makePos(0, 0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
            timeline2.addKeyFrame(KeyFrame.makePos(0.0, (double)banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = baseElement.addTimeline(timeline2);
            baseElement.y = (float)banner_OFFSET;
            arrowContainer = (BaseElement)new BaseElement().init();
            arrowContainer.parentAnchor = 34;
            arrowContainer.anchor = 10;
            _ = hookButton.addChild(arrowContainer);
            Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(6);
            _ = arrowContainer.addTimeline(timeline3);
            timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 6.3));
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 3.5));
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 2f));
            arrowContainer.playTimeline(0);
            Image image3 = Image.Image_createWithResIDQuad(77, 0);
            image3.parentAnchor = (image3.anchor = 10);
            _ = arrowContainer.addChild(image3);
            Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(5);
            _ = image3.addTimeline(timeline4);
            timeline4.addKeyFrame(KeyFrame.makePos((double)image3.x, (double)image3.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
            timeline4.addKeyFrame(KeyFrame.makePos((double)image3.x, (double)(image3.y + 25f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45));
            timeline4.addKeyFrame(KeyFrame.makePos((double)image3.x, (double)image3.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45));
            timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            image3.playTimeline(0);
            return baseElement;
        }

        // Token: 0x0600062B RID: 1579 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
        public virtual void openMainPromo()
        {
            checkSwitchButtons();
            promoMainHidden = false;
            arrowContainer.setEnabled(false);
            promoBanner.removeTimeline(0);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makePos(0.0, (double)promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, (double)promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.1));
            promoBanner.addTimelinewithID(timeline, 0);
            promoBanner.playTimeline(0);
        }

        // Token: 0x0600062C RID: 1580 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
        public virtual void closeMainPromo()
        {
            promoMainHidden = true;
            arrowContainer.setEnabled(true);
            promoBanner.removeTimeline(1);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makePos(0.0, (double)promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, (double)BANNER_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            timeline.delegateTimelineDelegate = this;
            promoBanner.addTimelinewithID(timeline, 1);
            promoBanner.playTimeline(1);
        }

        // Token: 0x0600062D RID: 1581 RVA: 0x0002F770 File Offset: 0x0002D970
        public virtual void onButtonPressed(int n)
        {
            switch (n)
            {
                case 0:
                    {
                        string text = "MMENU_BANNER_PRESSED";
                        List<string> list =
                        [
                            "banner_id",
                            "basic",
                            "language",
                            Application.sharedAppSettings().getString(8).ToString(),
                            "game_unlocked",
                            CTRPreferences.isLiteVersion() ? "0" : "1",
                        ];
                        FlurryAPI.logEvent(text, list);
                        AndroidAPI.openUrl("http://www.windowsphone.com/en-us/store/app/cut-the-rope-exp/d9f1608e-138a-4278-802f-25e32e44c068");
                        return;
                    }
                case 1:
                    if (promoMainHidden)
                    {
                        openMainPromo();
                        return;
                    }
                    closeMainPromo();
                    return;
                case 2:
                    Application.sharedPreferences().remoteDataManager.prevBanner();
                    changeBanner();
                    return;
                case 3:
                    Application.sharedPreferences().remoteDataManager.nextBanner();
                    changeBanner();
                    return;
                default:
                    return;
            }
        }

        // Token: 0x0600062E RID: 1582 RVA: 0x0002F850 File Offset: 0x0002DA50
        public override void update(float delta)
        {
            base.update(delta);
            if (hookButton != null)
            {
                hookButton.x = hook.pos.x;
                hookButton.y = hook.pos.y - 5f;
                ConstraintedPoint bungeeAnchor = bungee.bungeeAnchor;
                ConstraintedPoint constraintedPoint = bungee.parts[Enumerable.Count(bungee.parts) - 1];
                Vector vector = vectSub(bungeeAnchor.pos, constraintedPoint.pos);
                float num = RADIANS_TO_DEGREES(vectAngleNormalized(vector)) + 90f;
                hookButton.rotation = num;
            }
        }

        // Token: 0x0600062F RID: 1583 RVA: 0x0002F90A File Offset: 0x0002DB0A
        public virtual void timelineFinished(Timeline t)
        {
            Application.sharedPreferences().remoteDataManager.nextBanner();
            changeBanner();
        }

        // Token: 0x06000630 RID: 1584 RVA: 0x0002F921 File Offset: 0x0002DB21
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x06000631 RID: 1585 RVA: 0x0002F924 File Offset: 0x0002DB24
        public virtual void changeBanner()
        {
            BaseElement baseElement = createBanner();
            BaseElement childWithName = getChildWithName("container");
            BaseElement childWithName2 = childWithName.getChildWithName("banner");
            childWithName.removeChild(childWithName2);
            _ = childWithName.addChild(baseElement);
        }

        // Token: 0x06000632 RID: 1586 RVA: 0x0002F960 File Offset: 0x0002DB60
        public virtual void checkSwitchButtons()
        {
            bool flag = Application.sharedPreferences().remoteDataManager != null && Application.sharedPreferences().remoteDataManager.hasSenseToRotateBanners();
            promoBanner.getChildWithName("promoSwitchLeftButton").setEnabled(flag);
            promoBanner.getChildWithName("promoSwitchRightButton").setEnabled(flag);
        }

        // Token: 0x06000633 RID: 1587 RVA: 0x0002F9B8 File Offset: 0x0002DBB8
        public void reset()
        {
            _ = promoBanner.onTouchUpXY(-1000f, -1000f);
        }

        // Token: 0x04000B84 RID: 2948
        public const int BUNGEE_LENGTH = 60;

        // Token: 0x04000B85 RID: 2949
        private const int BUTTON_PROMO_MAIN = 0;

        // Token: 0x04000B86 RID: 2950
        private const int BUTTON_PROMO_MAIN_TOGGLE = 1;

        // Token: 0x04000B87 RID: 2951
        private const int BUTTON_PROMO_LEFT = 2;

        // Token: 0x04000B88 RID: 2952
        private const int BUTTON_PROMO_RIGHT = 3;

        // Token: 0x04000B89 RID: 2953
        public static int BANNER_OFFSET = (int)Math.Floor(-312.0);

        // Token: 0x04000B8A RID: 2954
        private Processing fadeElement;

        // Token: 0x04000B8B RID: 2955
        private BaseElement arrowContainer;

        // Token: 0x04000B8C RID: 2956
        private Bungee bungee;

        // Token: 0x04000B8D RID: 2957
        private ConstraintedPoint hook;

        // Token: 0x04000B8E RID: 2958
        private ConstraintedPoint hookStart;

        // Token: 0x04000B8F RID: 2959
        private Image hookButton;

        // Token: 0x04000B90 RID: 2960
        private BaseElement promoBanner;

        // Token: 0x04000B91 RID: 2961
        public bool promoMainHidden;
    }
}
