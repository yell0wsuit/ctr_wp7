using System;
using System.Collections.Generic;

using ctr_wp7.Banner;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.sfe;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class PromoBanner : BaseElement, ButtonDelegate, TimelineDelegate
    {
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

        public override void dealloc()
        {
            base.dealloc();
        }

        public static BaseElement createBanner()
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

        public BaseElement createMainBanner()
        {
            int banner_OFFSET = BANNER_OFFSET;
            setName("promoBanner");
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            baseElement.setName("container");
            baseElement.parentAnchor = baseElement.anchor = 10;
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
            text.parentAnchor = text.anchor = 17;
            text.x = 10f;
            text.y = 2f;
            _ = image.addChild(text);
            Button button = MenuController.createButtonWithImageQuadIDDelegate(77, 4, 1, this);
            button.parentAnchor = button.anchor = 9;
            Image.setElementPositionWithQuadCenter(button, 77, 4);
            _ = image.addChild(button);
            Image image2 = Image.Image_createWithResIDQuad(77, 2);
            image2.parentAnchor = 9;
            Image.setElementPositionWithQuadOffset(image2, 77, 2);
            image2.y -= 1f;
            image2.scaleX = 1.01f;
            _ = baseElement.addChild(image2);
            Button button2 = MenuController.createButtonWithImageQuadIDDelegate(77, 5, 3, this);
            button2.parentAnchor = button2.anchor = 9;
            Image.setElementPositionWithQuadCenter(button2, 77, 5);
            button2.setName("promoSwitchRightButton");
            _ = baseElement.addChild(button2);
            Button button3 = MenuController.createButtonWithImageQuadIDDelegate(77, 5, 2, this);
            button3.parentAnchor = button3.anchor = 9;
            button3.scaleX = -1f;
            Image.setElementPositionWithQuadCenter(button3, 77, 10);
            button3.setName("promoSwitchLeftButton");
            _ = baseElement.addChild(button3);
            Vector quadCenter = Image.getQuadCenter(77, 8);
            Vector quadCenter2 = Image.getQuadCenter(77, 9);
            quadCenter2.y += banner_OFFSET;
            quadCenter.y += banner_OFFSET;
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
            bungeeDrawer.parentAnchor = bungeeDrawer.anchor = 9;
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
            hookButton.rotationCenterY = (-(float)hookButton.height / 2) + 5;
            promoMainHidden = true;
            baseElement.width = (int)SCREEN_WIDTH_EXPANDED;
            baseElement.height = image.height + baseElement2.height + image2.height;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makePos(0, banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
            _ = baseElement.addTimeline(timeline);
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makePos(0, 0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
            timeline2.addKeyFrame(KeyFrame.makePos(0.0, banner_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = baseElement.addTimeline(timeline2);
            baseElement.y = banner_OFFSET;
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
            image3.parentAnchor = image3.anchor = 10;
            _ = arrowContainer.addChild(image3);
            Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(5);
            _ = image3.addTimeline(timeline4);
            timeline4.addKeyFrame(KeyFrame.makePos(image3.x, image3.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
            timeline4.addKeyFrame(KeyFrame.makePos(image3.x, (double)(image3.y + 25f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45));
            timeline4.addKeyFrame(KeyFrame.makePos(image3.x, image3.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45));
            timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            image3.playTimeline(0);
            return baseElement;
        }

        public void openMainPromo()
        {
            checkSwitchButtons();
            promoMainHidden = false;
            arrowContainer.setEnabled(false);
            promoBanner.removeTimeline(0);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makePos(0.0, promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.1));
            promoBanner.addTimelinewithID(timeline, 0);
            promoBanner.playTimeline(0);
        }

        public void closeMainPromo()
        {
            promoMainHidden = true;
            arrowContainer.setEnabled(true);
            promoBanner.removeTimeline(1);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makePos(0.0, promoBanner.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(0.0, BANNER_OFFSET, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            timeline.delegateTimelineDelegate = this;
            promoBanner.addTimelinewithID(timeline, 1);
            promoBanner.playTimeline(1);
        }

        public void onButtonPressed(int n)
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
                            ApplicationSettings.getString(8).ToString(),
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
                    RemoteDataManager.prevBanner();
                    changeBanner();
                    return;
                case 3:
                    RemoteDataManager.nextBanner();
                    changeBanner();
                    return;
                default:
                    return;
            }
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (hookButton != null)
            {
                hookButton.x = hook.pos.x;
                hookButton.y = hook.pos.y - 5f;
                ConstraintedPoint bungeeAnchor = bungee.bungeeAnchor;
                ConstraintedPoint constraintedPoint = bungee.parts[^1];
                Vector vector = vectSub(bungeeAnchor.pos, constraintedPoint.pos);
                float num = RADIANS_TO_DEGREES(vectAngleNormalized(vector)) + 90f;
                hookButton.rotation = num;
            }
        }

        public void timelineFinished(Timeline t)
        {
            RemoteDataManager.nextBanner();
            changeBanner();
        }

        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public void changeBanner()
        {
            BaseElement baseElement = createBanner();
            BaseElement childWithName = getChildWithName("container");
            BaseElement childWithName2 = childWithName.getChildWithName("banner");
            childWithName.removeChild(childWithName2);
            _ = childWithName.addChild(baseElement);
        }

        public void checkSwitchButtons()
        {
            bool flag = RemoteDataManager.hasSenseToRotateBanners();
            promoBanner.getChildWithName("promoSwitchLeftButton").setEnabled(flag);
            promoBanner.getChildWithName("promoSwitchRightButton").setEnabled(flag);
        }

        public void reset()
        {
            _ = promoBanner.onTouchUpXY(-1000f, -1000f);
        }

        public const int BUNGEE_LENGTH = 60;

        private const int BUTTON_PROMO_MAIN = 0;

        private const int BUTTON_PROMO_MAIN_TOGGLE = 1;

        private const int BUTTON_PROMO_LEFT = 2;

        private const int BUTTON_PROMO_RIGHT = 3;

        public static int BANNER_OFFSET = (int)Math.Floor(-312.0);

        private Processing fadeElement;

        private BaseElement arrowContainer;

        private Bungee bungee;

        private ConstraintedPoint hook;

        private ConstraintedPoint hookStart;

        private Image hookButton;

        private BaseElement promoBanner;

        public bool promoMainHidden;
    }
}
