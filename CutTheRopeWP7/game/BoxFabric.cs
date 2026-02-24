using ctr_wp7.Banner;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal abstract class BoxFabric : NSObject
    {
        public virtual BaseElement createPackElementforContainer(int i, int pbox, ScrollableContainer c, ButtonDelegate d)
        {
            buttonDelegate = d;
            int saveIndex = getSaveIndex(pbox);
            _ = Application.sharedPreferences();
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            baseElement.setName(NSS("boxContainer"));
            baseElement.anchor = baseElement.parentAnchor = 9;
            BaseElement baseElement2 = null;
            BaseElement baseElement3 = null;
            if (isGameBox(pbox))
            {
                MenuController.TouchBaseElement touchBaseElement = (MenuController.TouchBaseElement)new MenuController.TouchBaseElement().init();
                baseElement2 = touchBaseElement;
                touchBaseElement.bid = -1;
                touchBaseElement.delegateButtonDelegate = buttonDelegate;
                touchBaseElement.bbc = MakeRectangle(70.0, 0.0, -70.0, 0.0);
                int totalStarsInDelivery = CTRPreferences.getTotalStarsInDelivery(-1);
                if (CTRPreferences.getUnlockedForPackLevel(saveIndex, 0) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED && totalStarsInDelivery >= CTRPreferences.packUnlockStars(saveIndex))
                {
                    CTRPreferences.setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_JUST_UNLOCKED, saveIndex, 0);
                }
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(4);
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.95, 1.05, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.15));
                timeline.addKeyFrame(KeyFrame.makeScale(1.05, 0.95, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.2));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.25));
                _ = baseElement.addTimeline(timeline);
                baseElement3 = buildGameBox(i, saveIndex, baseElement, touchBaseElement, c);
            }
            else if (pbox == 18)
            {
                baseElement2 = (BaseElement)new BaseElement().init();
                baseElement3 = buildComingSoonBox();
            }
            else if (pbox == 19)
            {
                baseElement2 = (BaseElement)new BaseElement().init();
                baseElement3 = buildNextDeliveryBox(buttonDelegate);
                baseElement3.anchor = baseElement3.parentAnchor = 18;
            }
            else if (pbox == 0)
            {
                baseElement2 = (BaseElement)new BaseElement().init();
                baseElement3 = buildZeroBox(buttonDelegate);
            }
            else if (pbox == 20)
            {
                baseElement2 = (BaseElement)new BaseElement().init();
                baseElement3 = buildLiteBox(buttonDelegate);
            }
            else if (pbox == 1)
            {
                baseElement2 = (BaseElement)new BaseElement().init();
                baseElement3 = buildVideoBox(buttonDelegate);
            }
            _ = baseElement2.addChild(baseElement);
            if (baseElement3.parent == null)
            {
                _ = baseElement.addChild(baseElement3);
            }
            if (isGameBox(pbox) || pbox == 18 || pbox == 19)
            {
                baseElement.height = baseElement2.height = 300;
                baseElement.width = baseElement2.width = 300;
            }
            else if (pbox == 20)
            {
                baseElement.height = baseElement2.height = 300;
                baseElement.width = baseElement2.width = 330;
            }
            else
            {
                baseElement.height = baseElement2.height = baseElement3.height;
                baseElement.width = baseElement2.width = baseElement3.width;
            }
            return baseElement2;
        }

        public static int getSaveIndex(int box)
        {
            return isGameBox(box) ? box - 2 - 1 : -1;
        }

        public static bool isGameBox(int box)
        {
            return box is > 2 and < 17;
        }

        protected virtual bool isZeroBoxLowerDefined()
        {
            return false;
        }

        protected virtual bool isZeroBoxUpperDefined()
        {
            return false;
        }

        protected virtual int getIcofor(int type)
        {
            switch (type)
            {
                case 0:
                    return 9;
                case 1:
                    return 10;
                case 2:
                    return 11;
                default:
                    return -1;
            }
        }

        protected virtual int getTextfor(int type)
        {
            switch (type)
            {
                case 0:
                    return 1310781;
                case 1:
                    return 1310818;
                case 2:
                    return 1310838;
                default:
                    return -1;
            }
        }

        protected virtual bool isAvailablefor(int type)
        {
            switch (type)
            {
                case 0:
                    return true;
                case 1:
                    return true;
                case 2:
                    return !RemoteDataManager.getHideSocialNetworks();
                default:
                    return false;
            }
        }

        public virtual void doActionfor(int type)
        {
            switch (type)
            {
                case 0:
                    AndroidAPI.openUrl("http://www.amazon.com/gp/mas/dl/android?p=com.zeptolab.ctrexperiments.hd.amazon.paid");
                    return;
                case 1:
                    AndroidAPI.openUrl("http://www.facebook.com/cuttherope");
                    break;
                case 2:
                    break;
                default:
                    return;
            }
        }

        public virtual void provideAnalyticsfor(int type)
        {
            switch (type)
            {
                default:
                    return;
            }
        }

        public virtual bool isZeroBoxDefined()
        {
            return isZeroBoxLowerDefined() || isZeroBoxUpperDefined();
        }

        protected abstract BaseElement buildGameBox(int i, int n, BaseElement container, MenuController.TouchBaseElement tpack, ScrollableContainer c);

        protected abstract BaseElement buildComingSoonBox();

        protected virtual BaseElement buildLock()
        {
            Image image = Image.Image_createWithResIDQuad(71, 1);
            image.setName(NSS("lockHideMe"));
            image.doRestoreCutTransparency();
            image.anchor = image.parentAnchor = 9;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.5));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeScale(2.0, 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.5));
            _ = image.addTimeline(timeline);
            return image;
        }

        protected virtual BaseElement buildText(int label, bool multistring)
        {
            NSString @string = Application.getString(label);
            Text text = new Text().initWithFont(Application.getFont(5));
            text.anchor = text.parentAnchor = 10;
            text.setAlignment(2);
            if (multistring)
            {
                text.setStringandWidth(@string, 215.0);
            }
            else
            {
                text.setString(@string);
            }
            text.y = 100f;
            return text;
        }

        protected virtual BaseElement buildPerfectMark()
        {
            Image image = Image.Image_createWithResIDQuad(71, 4);
            image.doRestoreCutTransparency();
            image.parentAnchor = image.anchor = 9;
            return image;
        }

        protected virtual BaseElement buildNextDeliveryBox(ButtonDelegate buttonDelegate)
        {
            Image image = Image.Image_createWithResIDQuad(71, 13);
            Image image2 = Image.Image_createWithResIDQuad(71, 13);
            MenuController.TouchBaseElement touchBaseElement = (MenuController.TouchBaseElement)new MenuController.TouchBaseElement().init();
            touchBaseElement.bid = 47;
            touchBaseElement.delegateButtonDelegate = buttonDelegate;
            touchBaseElement.anchor = touchBaseElement.parentAnchor = 18;
            touchBaseElement.height = image.height + image2.height;
            touchBaseElement.width = image.width;
            image.anchor = image.parentAnchor = 9;
            image2.anchor = image2.parentAnchor = 33;
            image2.scaleY = -1f;
            image2.y = -1f;
            touchBaseElement.rotation = 5f;
            _ = touchBaseElement.addChild(image);
            _ = touchBaseElement.addChild(image2);
            Text text = Text.createWithFontandString(5, Application.getString(1310837));
            text.anchor = text.parentAnchor = 18;
            text.y = -2f;
            text.x = -4f;
            if (text.width > 150f)
            {
                text.scaleX = text.scaleY = 150f / text.width;
            }
            text.setAlignment(2);
            _ = touchBaseElement.addChild(text);
            touchBaseElement.x = 40f;
            return touchBaseElement;
        }

        protected virtual VBox buildZeroBox(ButtonDelegate buttonDelegate)
        {
            BaseElement baseElement = null;
            BaseElement baseElement2 = null;
            float num = 25f;
            float num2 = 0f;
            _ = ApplicationSettings.getString(8).isEqualToString("zh");
            VBox vbox = new VBox().initWithOffsetAlignWidth(30.0, 2, (double)num2 + ((double)(SCREEN_WIDTH_EXPANDED - num2) / 2.0) + (double)num);
            vbox.x = num;
            vbox.parentAnchor = vbox.anchor = 10;
            if (baseElement != null)
            {
                _ = vbox.addChild(baseElement);
            }
            if (baseElement2 != null)
            {
                _ = vbox.addChild(baseElement2);
            }
            return vbox;
        }

        protected virtual BaseElement buildLiteBox(ButtonDelegate buttonDelegate)
        {
            return null;
        }

        protected abstract BaseElement buildVideoBox(ButtonDelegate buttonDelegate);

        public const int ZEROBOX_UPPER = 2;

        public const int ZEROBOX_LOWER = 1;

        protected ButtonDelegate buttonDelegate;

        public enum GAMEBOXES
        {
            _ZEROBOX,
            _VIDEOBOX,
            ___GAMEBOXES_START___,
            CARDBOARD,
            FABRIC,
            FOIL,
            MAGIC,
            VALENTINE,
            GIFT,
            COSMIC,
            TOY,
            TOOLBOX,
            BUZZ,
            DJ,
            SPOOKY,
            STEAM,
            LANTERN,
            ___GAMEBOXES_FINISH___,
            _COMING_SOON,
            _NEXT_DELIVERY,
            _LITE_GETFULL
        }
    }
}
