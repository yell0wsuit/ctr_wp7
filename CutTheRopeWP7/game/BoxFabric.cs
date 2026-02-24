using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000C5 RID: 197
    internal abstract class BoxFabric : NSObject
    {
        // Token: 0x060005B6 RID: 1462 RVA: 0x0002BF7C File Offset: 0x0002A17C
        public virtual BaseElement createPackElementforContainer(int i, int pbox, ScrollableContainer c, ButtonDelegate d)
        {
            buttonDelegate = d;
            int saveIndex = getSaveIndex(pbox);
            Application.sharedPreferences();
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            baseElement.setName(NSS("boxContainer"));
            baseElement.anchor = (baseElement.parentAnchor = 9);
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
                baseElement.addTimeline(timeline);
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
                baseElement3.anchor = (baseElement3.parentAnchor = 18);
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
            baseElement2.addChild(baseElement);
            if (baseElement3.parent == null)
            {
                baseElement.addChild(baseElement3);
            }
            if (isGameBox(pbox) || pbox == 18 || pbox == 19)
            {
                baseElement.height = (baseElement2.height = 300);
                baseElement.width = (baseElement2.width = 300);
            }
            else if (pbox == 20)
            {
                baseElement.height = (baseElement2.height = 300);
                baseElement.width = (baseElement2.width = 330);
            }
            else
            {
                baseElement.height = (baseElement2.height = baseElement3.height);
                baseElement.width = (baseElement2.width = baseElement3.width);
            }
            return baseElement2;
        }

        // Token: 0x060005B7 RID: 1463 RVA: 0x0002C29D File Offset: 0x0002A49D
        public static int getSaveIndex(int box)
        {
            if (isGameBox(box))
            {
                return box - 2 - 1;
            }
            return -1;
        }

        // Token: 0x060005B8 RID: 1464 RVA: 0x0002C2AE File Offset: 0x0002A4AE
        public static bool isGameBox(int box)
        {
            return box > 2 && box < 17;
        }

        // Token: 0x060005B9 RID: 1465 RVA: 0x0002C2BC File Offset: 0x0002A4BC
        protected virtual bool isZeroBoxLowerDefined()
        {
            return false;
        }

        // Token: 0x060005BA RID: 1466 RVA: 0x0002C2BF File Offset: 0x0002A4BF
        protected virtual bool isZeroBoxUpperDefined()
        {
            return false;
        }

        // Token: 0x060005BB RID: 1467 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
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

        // Token: 0x060005BC RID: 1468 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
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

        // Token: 0x060005BD RID: 1469 RVA: 0x0002C32C File Offset: 0x0002A52C
        protected virtual bool isAvailablefor(int type)
        {
            switch (type)
            {
                case 0:
                    return true;
                case 1:
                    return true;
                case 2:
                    return !Application.sharedPreferences().remoteDataManager.getHideSocialNetworks();
                default:
                    return false;
            }
        }

        // Token: 0x060005BE RID: 1470 RVA: 0x0002C368 File Offset: 0x0002A568
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

        // Token: 0x060005BF RID: 1471 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
        public virtual void provideAnalyticsfor(int type)
        {
            switch (type)
            {
                default:
                    return;
            }
        }

        // Token: 0x060005C0 RID: 1472 RVA: 0x0002C3C1 File Offset: 0x0002A5C1
        public virtual bool isZeroBoxDefined()
        {
            return isZeroBoxLowerDefined() || isZeroBoxUpperDefined();
        }

        // Token: 0x060005C1 RID: 1473
        protected abstract BaseElement buildGameBox(int i, int n, BaseElement container, MenuController.TouchBaseElement tpack, ScrollableContainer c);

        // Token: 0x060005C2 RID: 1474
        protected abstract BaseElement buildComingSoonBox();

        // Token: 0x060005C3 RID: 1475 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
        protected virtual BaseElement buildLock()
        {
            Image image = Image.Image_createWithResIDQuad(71, 1);
            image.setName(NSS("lockHideMe"));
            image.doRestoreCutTransparency();
            image.anchor = (image.parentAnchor = 9);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.5));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeScale(2.0, 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.5));
            image.addTimeline(timeline);
            return image;
        }

        // Token: 0x060005C4 RID: 1476 RVA: 0x0002C4A8 File Offset: 0x0002A6A8
        protected virtual BaseElement buildText(int label, bool multistring)
        {
            NSString @string = Application.getString(label);
            Text text = new Text().initWithFont(Application.getFont(5));
            text.anchor = (text.parentAnchor = 10);
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

        // Token: 0x060005C5 RID: 1477 RVA: 0x0002C510 File Offset: 0x0002A710
        protected virtual BaseElement buildPerfectMark()
        {
            Image image = Image.Image_createWithResIDQuad(71, 4);
            image.doRestoreCutTransparency();
            image.parentAnchor = (image.anchor = 9);
            return image;
        }

        // Token: 0x060005C6 RID: 1478 RVA: 0x0002C540 File Offset: 0x0002A740
        protected virtual BaseElement buildNextDeliveryBox(ButtonDelegate buttonDelegate)
        {
            Image image = Image.Image_createWithResIDQuad(71, 13);
            Image image2 = Image.Image_createWithResIDQuad(71, 13);
            MenuController.TouchBaseElement touchBaseElement = (MenuController.TouchBaseElement)new MenuController.TouchBaseElement().init();
            touchBaseElement.bid = 47;
            touchBaseElement.delegateButtonDelegate = buttonDelegate;
            touchBaseElement.anchor = (touchBaseElement.parentAnchor = 18);
            touchBaseElement.height = image.height + image2.height;
            touchBaseElement.width = image.width;
            image.anchor = (image.parentAnchor = 9);
            image2.anchor = (image2.parentAnchor = 33);
            image2.scaleY = -1f;
            image2.y = -1f;
            touchBaseElement.rotation = 5f;
            touchBaseElement.addChild(image);
            touchBaseElement.addChild(image2);
            Text text = Text.createWithFontandString(5, Application.getString(1310837));
            text.anchor = (text.parentAnchor = 18);
            text.y = -2f;
            text.x = -4f;
            if ((float)text.width > 150f)
            {
                text.scaleX = (text.scaleY = 150f / (float)text.width);
            }
            text.setAlignment(2);
            touchBaseElement.addChild(text);
            touchBaseElement.x = 40f;
            return touchBaseElement;
        }

        // Token: 0x060005C7 RID: 1479 RVA: 0x0002C68C File Offset: 0x0002A88C
        protected virtual VBox buildZeroBox(ButtonDelegate buttonDelegate)
        {
            BaseElement baseElement = null;
            BaseElement baseElement2 = null;
            float num = 25f;
            float num2 = 0f;
            Application.sharedAppSettings().getString(8).isEqualToString("zh");
            VBox vbox = new VBox().initWithOffsetAlignWidth(30.0, 2, (double)num2 + (double)(SCREEN_WIDTH_EXPANDED - num2) / 2.0 + (double)num);
            vbox.x = num;
            vbox.parentAnchor = (vbox.anchor = 10);
            if (baseElement != null)
            {
                vbox.addChild(baseElement);
            }
            if (baseElement2 != null)
            {
                vbox.addChild(baseElement2);
            }
            return vbox;
        }

        // Token: 0x060005C8 RID: 1480 RVA: 0x0002C724 File Offset: 0x0002A924
        protected virtual BaseElement buildLiteBox(ButtonDelegate buttonDelegate)
        {
            return null;
        }

        // Token: 0x060005C9 RID: 1481
        protected abstract BaseElement buildVideoBox(ButtonDelegate buttonDelegate);

        // Token: 0x04000B1E RID: 2846
        public const int ZEROBOX_UPPER = 2;

        // Token: 0x04000B1F RID: 2847
        public const int ZEROBOX_LOWER = 1;

        // Token: 0x04000B20 RID: 2848
        protected ButtonDelegate buttonDelegate;

        // Token: 0x020000C6 RID: 198
        public enum GAMEBOXES
        {
            // Token: 0x04000B22 RID: 2850
            _ZEROBOX,
            // Token: 0x04000B23 RID: 2851
            _VIDEOBOX,
            // Token: 0x04000B24 RID: 2852
            ___GAMEBOXES_START___,
            // Token: 0x04000B25 RID: 2853
            CARDBOARD,
            // Token: 0x04000B26 RID: 2854
            FABRIC,
            // Token: 0x04000B27 RID: 2855
            FOIL,
            // Token: 0x04000B28 RID: 2856
            MAGIC,
            // Token: 0x04000B29 RID: 2857
            VALENTINE,
            // Token: 0x04000B2A RID: 2858
            GIFT,
            // Token: 0x04000B2B RID: 2859
            COSMIC,
            // Token: 0x04000B2C RID: 2860
            TOY,
            // Token: 0x04000B2D RID: 2861
            TOOLBOX,
            // Token: 0x04000B2E RID: 2862
            BUZZ,
            // Token: 0x04000B2F RID: 2863
            DJ,
            // Token: 0x04000B30 RID: 2864
            SPOOKY,
            // Token: 0x04000B31 RID: 2865
            STEAM,
            // Token: 0x04000B32 RID: 2866
            LANTERN,
            // Token: 0x04000B33 RID: 2867
            ___GAMEBOXES_FINISH___,
            // Token: 0x04000B34 RID: 2868
            _COMING_SOON,
            // Token: 0x04000B35 RID: 2869
            _NEXT_DELIVERY,
            // Token: 0x04000B36 RID: 2870
            _LITE_GETFULL
        }
    }
}
