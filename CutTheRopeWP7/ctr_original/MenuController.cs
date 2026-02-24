using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using ctr_wp7.ctr_commons;
using ctr_wp7.game;
using ctr_wp7.game.remotedata;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.media;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.Specials;

namespace ctr_wp7.ctr_original
{
    // Token: 0x020000E3 RID: 227
    internal class MenuController : ViewController, ButtonDelegate, MovieMgrDelegate, ScrollableContainerProtocol, TimelineDelegate, LiftScrollbarDelegate
    {
        // Token: 0x06000693 RID: 1683 RVA: 0x00032E87 File Offset: 0x00031087
        public void selector_gotoNextBox(NSObject param)
        {
            gotoNextBox();
        }

        // Token: 0x06000694 RID: 1684 RVA: 0x00032E8F File Offset: 0x0003108F
        public void selector_playHandAnimation(NSObject param)
        {
            playHandAnimation();
        }

        // Token: 0x06000695 RID: 1685 RVA: 0x00032E98 File Offset: 0x00031098
        private void setElementPositionWithRelativeQuadOffset2(BaseElement e, int textureID, int quadToCountFrom, int textureID2, int quad)
        {
            Vector quadOffset = Image.getQuadOffset(textureID, quadToCountFrom);
            Vector quadOffset2 = Image.getQuadOffset(textureID2, quad);
            Vector vector = vectSub(quadOffset2, quadOffset);
            e.x = vector.x;
            e.y = vector.y;
        }

        // Token: 0x06000696 RID: 1686 RVA: 0x00032EDC File Offset: 0x000310DC
        public static Button createButtonWithTextscaleTextIDDelegate(NSString str, float scale, int bid, ButtonDelegate d, int img, int idle, int pressed)
        {
            Image image = Image.Image_createWithResIDQuad(img, idle);
            Image image2 = Image.Image_createWithResIDQuad(img, pressed);
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Text text2 = new Text().initWithFont(font);
            text2.setString(str);
            text.anchor = text.parentAnchor = 18;
            text2.anchor = text2.parentAnchor = 18;
            BaseElement baseElement = text;
            BaseElement baseElement2 = text;
            BaseElement baseElement3 = text2;
            text2.scaleY = scale;
            baseElement3.scaleX = scale;
            baseElement2.scaleY = scale;
            baseElement.scaleX = scale;
            _ = image.addChild(text);
            _ = image2.addChild(text2);
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.setTouchIncreaseLeftRightTopBottom(15.0, 15.0, 15.0, 15.0);
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x06000697 RID: 1687 RVA: 0x00032FD3 File Offset: 0x000311D3
        public static Button createButtonWithTextIDDelegate(NSString str, int bid, ButtonDelegate d, int img, int idle, int pressed)
        {
            return createButtonWithTextscaleTextIDDelegate(str, 1f, bid, d, img, idle, pressed);
        }

        // Token: 0x06000698 RID: 1688 RVA: 0x00032FE7 File Offset: 0x000311E7
        public static Button createButtonWithTextIDDelegate(NSString str, float scale, int bid, ButtonDelegate d)
        {
            return createButtonWithTextscaleTextIDDelegate(str, scale, bid, d, 4, 0, 1);
        }

        // Token: 0x06000699 RID: 1689 RVA: 0x00032FF5 File Offset: 0x000311F5
        public static Button createButtonWithTextIDDelegate(NSString str, int bid, ButtonDelegate d)
        {
            return createButtonWithTextIDDelegate(str, bid, d, 4, 0, 1);
        }

        // Token: 0x0600069A RID: 1690 RVA: 0x00033004 File Offset: 0x00031204
        public static Button createBigButtonWithTextIDDelegate(NSString str, int bid, ButtonDelegate d)
        {
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Image image = Image.Image_createWithResIDQuad(72, 6);
            float num = image.width * 0.9f / text.width;
            if (num > 1f)
            {
                num = 1f;
            }
            return createButtonWithTextscaleTextIDDelegate(str, num, bid, d, 72, 6, 7);
        }

        // Token: 0x0600069B RID: 1691 RVA: 0x00033068 File Offset: 0x00031268
        public static Button createButtonWithTextIDDelegateAutoScale(NSString str, int bid, ButtonDelegate d)
        {
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Image image = Image.Image_createWithResIDQuad(4, 0);
            float num = image.width * 0.9f / text.width;
            if (num > 1f)
            {
                num = 1f;
            }
            return createButtonWithTextscaleTextIDDelegate(str, num, bid, d, 4, 0, 1);
        }

        // Token: 0x0600069C RID: 1692 RVA: 0x000330C8 File Offset: 0x000312C8
        public static TimedButton createTimedButtonWithTextIDDelegateTimer(NSString str, int bid, ButtonDelegate d, float time)
        {
            Image image = Image.Image_createWithResIDQuad(4, 0);
            Image image2 = Image.Image_createWithResIDQuad(4, 1);
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Text text2 = new Text().initWithFont(font);
            text2.setString(str);
            text.anchor = text.parentAnchor = 18;
            text2.anchor = text2.parentAnchor = 18;
            _ = image.addChild(text);
            _ = image2.addChild(text2);
            TimedButton timedButton = new TimedButton().initWithUpElementDownElementandID(image, image2, bid);
            timedButton.setTouchIncreaseLeftRightTopBottom(15.0, 15.0, 15.0, 15.0);
            timedButton.timer = time;
            timedButton.delegateButtonDelegate = d;
            return timedButton;
        }

        // Token: 0x0600069D RID: 1693 RVA: 0x0003319C File Offset: 0x0003139C
        public static Button createPromoBanner(ButtonDelegate d)
        {
            Button button = createButton2WithImageQuad1Quad2IDDelegate(77, 6, 6, 27, d);
            button.parentAnchor = button.anchor = 36;
            float screen_OFFSET_Y = SCREEN_OFFSET_Y;
            float num = screen_OFFSET_Y + button.height;
            button.y = num;
            button.x -= SCREEN_OFFSET_X;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makePos(button.x, (double)num, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(button.x, (double)screen_OFFSET_Y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
            _ = button.addTimeline(timeline);
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makePos(button.x, (double)screen_OFFSET_Y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline2.addKeyFrame(KeyFrame.makePos(button.x, (double)num, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = button.addTimeline(timeline2);
            return button;
        }

        // Token: 0x0600069E RID: 1694 RVA: 0x00033298 File Offset: 0x00031498
        public static BaseElement frameElement(BaseElement block, int res, int frameid, int shadowid)
        {
            BaseElement baseElement = Image.createElementWithLeftPart(res, frameid);
            baseElement.anchor = baseElement.parentAnchor = 18;
            BaseElement baseElement2 = Image.createElementWithLeftPart(res, shadowid);
            baseElement2.anchor = 10;
            baseElement2.parentAnchor = 34;
            _ = baseElement.addChild(baseElement2);
            _ = block.addChild(baseElement);
            return block;
        }

        // Token: 0x0600069F RID: 1695 RVA: 0x000332E8 File Offset: 0x000314E8
        public static Button createShortButtonWithTextIDDelegate(NSString str, int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(10, 0);
            Image image2 = Image.Image_createWithResIDQuad(10, 1);
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Text text2 = new Text().initWithFont(font);
            text2.setString(str);
            text.anchor = text.parentAnchor = 18;
            text2.anchor = text2.parentAnchor = 18;
            _ = image.addChild(text);
            _ = image2.addChild(text2);
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.setTouchIncreaseLeftRightTopBottom(15.0, 15.0, 15.0, 15.0);
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x060006A0 RID: 1696 RVA: 0x000333B4 File Offset: 0x000315B4
        public static ToggleButton createToggleButtonWithText1Text2IDDelegate(NSString str1, NSString str2, int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(4, 0);
            Image image2 = Image.Image_createWithResIDQuad(4, 1);
            Image image3 = Image.Image_createWithResIDQuad(4, 0);
            Image image4 = Image.Image_createWithResIDQuad(4, 1);
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str1);
            Text text2 = new Text().initWithFont(font);
            text2.setString(str1);
            Text text3 = new Text().initWithFont(font);
            text3.setString(str2);
            Text text4 = new Text().initWithFont(font);
            text4.setString(str2);
            text.anchor = text.parentAnchor = 18;
            text2.anchor = text2.parentAnchor = 18;
            text3.anchor = text3.parentAnchor = 18;
            text4.anchor = text4.parentAnchor = 18;
            _ = image.addChild(text);
            _ = image2.addChild(text2);
            _ = image3.addChild(text3);
            _ = image4.addChild(text4);
            ToggleButton toggleButton = new ToggleButton().initWithUpElement1DownElement1UpElement2DownElement2andID(image, image2, image3, image4, bid);
            toggleButton.setTouchIncreaseLeftRightTopBottom(10.0, 10.0, 10.0, 10.0);
            toggleButton.delegateButtonDelegate = d;
            return toggleButton;
        }

        // Token: 0x060006A1 RID: 1697 RVA: 0x00033500 File Offset: 0x00031700
        public static Button createBackButtonWithDelegateID(ButtonDelegate d, int bid)
        {
            Button button = createButtonWithImageQuad1Quad2IDDelegate(72, 0, 1, bid, d);
            button.anchor = button.parentAnchor = 33;
            button.y += SCREEN_OFFSET_Y;
            button.x -= SCREEN_OFFSET_X;
            return button;
        }

        // Token: 0x060006A2 RID: 1698 RVA: 0x00033550 File Offset: 0x00031750
        public static BaseElement packOurNews(int bidtwit, int bidface, ButtonDelegate d)
        {
            Texture2D texture = Application.getTexture(72);
            Button button = createButton2WithImageQuad1Quad2IDDelegate(72, 3, 3, bidtwit, d);
            button.anchor = 9;
            button.parentAnchor = 36;
            Image.setElementPositionWithQuadOffset(button, 72, 3);
            button.x -= texture.preCutSize.x;
            button.y -= texture.preCutSize.y;
            button.x += SCREEN_OFFSET_X;
            button.y += SCREEN_OFFSET_Y;
            Button button2 = createButton2WithImageQuad1Quad2IDDelegate(72, 2, 2, bidface, d);
            button2.anchor = button2.parentAnchor = 9;
            Image.setElementPositionWithRelativeQuadOffset(button2, 72, 3, 2);
            _ = button.addChild(button2);
            Image image = Image.Image_createWithResIDQuad(73, 0);
            image.anchor = 12;
            image.parentAnchor = 33;
            image.x = -7f;
            image.y = 4f;
            _ = button2.addChild(image);
            return button;
        }

        // Token: 0x060006A3 RID: 1699 RVA: 0x00033648 File Offset: 0x00031848
        public static Button createButtonWithImageIDDelegate(int resID, int bid, ButtonDelegate d)
        {
            Texture2D texture = Application.getTexture(resID);
            Image image = Image.Image_create(texture);
            Image image2 = Image.Image_create(texture);
            image2.scaleX = 1.2f;
            image2.scaleY = 1.2f;
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.setTouchIncreaseLeftRightTopBottom(10.0, 10.0, 10.0, 10.0);
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x060006A4 RID: 1700 RVA: 0x000336C0 File Offset: 0x000318C0
        public static Button createButtonWithImageQuadIDDelegate(int resID, int quad, int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(resID, quad);
            Image image2 = Image.Image_createWithResIDQuad(resID, quad);
            image2.scaleX = 1.2f;
            image2.scaleY = 1.2f;
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.setTouchIncreaseLeftRightTopBottom(10.0, 10.0, 10.0, 10.0);
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x060006A5 RID: 1701 RVA: 0x00033734 File Offset: 0x00031934
        public static Button createButton2WithImageQuad1Quad2IDDelegate(int res, int q1, int q2, int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(res, q1);
            Image image2 = Image.Image_createWithResIDQuad(res, q2);
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.delegateButtonDelegate = d;
            _ = Application.getTexture(res);
            return button;
        }

        // Token: 0x060006A6 RID: 1702 RVA: 0x00033770 File Offset: 0x00031970
        public static Button buttonWithTextImageQuadHalfRescaledRecoloredIDDelegate(NSString str, int img, int quad, bool half, float scale, RGBAColor color, int bid, ButtonDelegate d)
        {
            BaseElement baseElement;
            BaseElement baseElement2;
            if (half)
            {
                baseElement = Image.createElementWithLeftPart(img, quad);
                baseElement2 = Image.createElementWithLeftPart(img, quad);
            }
            else
            {
                baseElement = Image.Image_createWithResIDQuad(img, quad);
                baseElement2 = Image.Image_createWithResIDQuad(img, quad);
            }
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.setString(str);
            Text text2 = new Text().initWithFont(font);
            text2.setString(str);
            text.anchor = text.parentAnchor = 18;
            text2.anchor = text2.parentAnchor = 18;
            _ = baseElement.addChild(text);
            _ = baseElement2.addChild(text2);
            baseElement2.color = color;
            BaseElement baseElement3 = baseElement2;
            baseElement2.scaleY = scale;
            baseElement3.scaleX = scale;
            Button button = new Button().initWithUpElementDownElementandID(baseElement, baseElement2, bid);
            button.setTouchIncreaseLeftRightTopBottom(15.0, 15.0, 15.0, 15.0);
            if (text.width > button.width * 0.8f)
            {
                text.scaleX = text.scaleY = text2.scaleX = text2.scaleY = button.width * 0.8f / text.width;
            }
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x060006A7 RID: 1703 RVA: 0x000338C4 File Offset: 0x00031AC4
        public static Button createMenuButtonWithImgQuadTextDelegateID(int q, NSString str, ButtonDelegate d, int bid)
        {
            BaseElement baseElement = createMenuElementQuadText(q, str);
            BaseElement baseElement2 = createMenuElementQuadText(q, str);
            baseElement2.scaleX = baseElement2.scaleY = 1.2f;
            Button button = new Button().initWithUpElementDownElementandID(baseElement, baseElement2, bid);
            button.delegateButtonDelegate = d;
            return button;
        }

        // Token: 0x060006A8 RID: 1704 RVA: 0x0003390C File Offset: 0x00031B0C
        public static BaseElement createMenuElementQuadText(int q, NSString str)
        {
            Image image = Image.Image_createWithResIDQuad(72, 8);
            Image image2 = Image.Image_createWithResIDQuad(72, q);
            image2.parentAnchor = 9;
            Image.setElementPositionWithRelativeQuadOffset(image2, 72, 8, q);
            _ = image.addChild(image2);
            FontGeneric font = Application.getFont(5);
            Text text = new Text().initWithFont(font);
            text.scaleX = text.scaleY = 0.7f;
            text.setAlignment(2);
            text.setStringandWidth(str, image.width);
            text.parentAnchor = text.anchor = 18;
            _ = image.addChild(text);
            return image;
        }

        // Token: 0x060006A9 RID: 1705 RVA: 0x000339A0 File Offset: 0x00031BA0
        public static Button createButtonWithImageQuad1Quad2IDDelegate(int res, int q1, int q2, int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(res, q1);
            Image image2 = Image.Image_createWithResIDQuad(res, q2);
            image.doRestoreCutTransparency();
            image2.doRestoreCutTransparency();
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.delegateButtonDelegate = d;
            Texture2D texture = Application.getTexture(res);
            button.forceTouchRect(MakeRectangle(texture.quadOffsets[q1].x, texture.quadOffsets[q1].y, texture.quadRects[q1].w, texture.quadRects[q1].h));
            return button;
        }

        // Token: 0x060006AA RID: 1706 RVA: 0x00033A38 File Offset: 0x00031C38
        public static Button createStarkeyButtonWithDelegateID(int bid, ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(72, 4);
            Image image2 = Image.Image_createWithResIDQuad(72, 5);
            FontGeneric font = Application.getFont(5);
            NSString @string = Application.getString(1310803);
            Text text = new Text().initWithFont(font);
            text.setString(@string);
            text.anchor = text.parentAnchor = 20;
            text.x = -65f;
            _ = image.addChild(text);
            Text text2 = new Text().initWithFont(font);
            text2.setString(@string);
            text2.anchor = text2.parentAnchor = 20;
            text2.x = text.x;
            _ = image2.addChild(text2);
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
            button.parentAnchor = button.anchor = 36;
            button.delegateButtonDelegate = d;
            float num = SCREEN_OFFSET_Y;
            if (CTRPreferences.isBannersMustBeShown())
            {
                num -= 50f;
            }
            float num2 = SCREEN_OFFSET_Y + button.height;
            button.y = num2;
            button.x -= SCREEN_OFFSET_X;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makePos(button.x, (double)num2, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(button.x, (double)num2, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            timeline.addKeyFrame(KeyFrame.makePos(button.x, (double)num, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
            _ = button.addTimeline(timeline);
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makePos(button.x, (double)num, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline2.addKeyFrame(KeyFrame.makePos(button.x, (double)num2, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = button.addTimeline(timeline2);
            return button;
        }

        // Token: 0x060006AB RID: 1707 RVA: 0x00033C30 File Offset: 0x00031E30
        private static Image createAudioElementForQuadwithCrosspressed(int q, bool b, bool p)
        {
            int num = p ? 1 : 0;
            Image image = Image.Image_createWithResIDQuad(397, num);
            Image image2 = Image.Image_createWithResIDQuad(397, q);
            image2.parentAnchor = image2.anchor = 18;
            _ = image.addChild(image2);
            if (b)
            {
                image2.color = RGBAColor.MakeRGBA(0.5f, 0.5f, 0.5f, 0.5f);
                Image image3 = Image.Image_createWithResIDQuad(397, 4);
                image3.parentAnchor = image3.anchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(image3, 397, num, 4);
                _ = image.addChild(image3);
            }
            return image;
        }

        // Token: 0x060006AC RID: 1708 RVA: 0x00033CD0 File Offset: 0x00031ED0
        private static ToggleButton createAudioButtonWithQuadDelegateID(int q, ButtonDelegate d, int bid)
        {
            Image image = createAudioElementForQuadwithCrosspressed(q, false, false);
            Image image2 = createAudioElementForQuadwithCrosspressed(q, false, true);
            Image image3 = createAudioElementForQuadwithCrosspressed(q, true, false);
            Image image4 = createAudioElementForQuadwithCrosspressed(q, true, true);
            ToggleButton toggleButton = new ToggleButton().initWithUpElement1DownElement1UpElement2DownElement2andID(image, image2, image3, image4, bid);
            toggleButton.delegateButtonDelegate = d;
            return toggleButton;
        }

        // Token: 0x060006AD RID: 1709 RVA: 0x00033D1C File Offset: 0x00031F1C
        public static BaseElement createButtonDelivery(int bid, ButtonDelegate d, int framenum, int heroid, bool under)
        {
            int num = 401;
            int num2 = 0;
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            BaseElement baseElement2 = Image.createElementWithLeftPart(num, num2);
            baseElement2.anchor = baseElement2.parentAnchor = 33;
            _ = baseElement.addChild(baseElement2);
            Image image = Image.Image_createWithResIDQuad(num, heroid);
            image.anchor = image.parentAnchor = 9;
            Image.setElementPositionWithRelativeQuadOffset(image, num, num2, heroid);
            if (under)
            {
                _ = baseElement2.addChild(image);
            }
            int num3 = 7 + framenum;
            BaseElement baseElement3 = Image.createElementWithLeftPart(num, num3);
            BaseElement baseElement4 = Image.createElementWithLeftPart(num, num3);
            baseElement4.color = RGBAColor.MakeRGBA(0.8f, 0.8f, 0.8f, 1f);
            baseElement4.scaleX = baseElement4.scaleY = 0.95f;
            string text = Application.getString(1310834).ToString().Replace("%d", (framenum + 1).ToString());
            NSString nsstring = new(text);
            Text text2 = Text.createWithFontandString(5, nsstring);
            text2.setName("del_text_up");
            text2.anchor = text2.parentAnchor = 18;
            text2.y = 4f;
            _ = baseElement3.addChild(text2);
            Text text3 = Text.createWithFontandString(5, nsstring);
            text3.setName("del_text_down");
            text3.anchor = text3.parentAnchor = 18;
            text3.y = 4f;
            _ = baseElement4.addChild(text3);
            Button button = new Button().initWithUpElementDownElementandID(baseElement3, baseElement4, bid);
            button.delegateButtonDelegate = d;
            button.anchor = button.parentAnchor = 9;
            button.setName("del_button");
            Image.setElementPositionWithRelativeQuadOffset(button, num, num2, num3);
            _ = baseElement2.addChild(button);
            int num4 = 2 + (framenum * 2);
            int num5 = 1 + (framenum * 2);
            Vector relativeQuadOffset = Image.getRelativeQuadOffset(num, num2, num4);
            Image image2 = Image.Image_createWithResIDQuad(num, num4);
            image2.anchor = image2.parentAnchor = 9;
            image2.y = relativeQuadOffset.y;
            _ = baseElement2.addChild(image2);
            Image image3 = Image.Image_createWithResIDQuad(num, num5);
            image3.anchor = image3.parentAnchor = 12;
            image3.scaleX = -1f;
            image3.y = relativeQuadOffset.y;
            _ = baseElement2.addChild(image3);
            if (!under)
            {
                _ = baseElement2.addChild(image);
            }
            baseElement.width = baseElement2.width;
            baseElement.height = baseElement2.height + button.height;
            baseElement.scaleX = SCREEN_BG_SCALE_X;
            return baseElement;
        }

        // Token: 0x060006AE RID: 1710 RVA: 0x00033FB4 File Offset: 0x000321B4
        public static BaseElement createButtonCartoons(int bid, ButtonDelegate d, bool newmark = true)
        {
            int num = 401;
            int num2 = 10;
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            BaseElement baseElement2 = Image.createElementWithLeftPart(num, num2);
            baseElement2.getChild(1).x += 1.33f;
            BaseElement baseElement3 = Image.createElementWithLeftPart(num, num2);
            baseElement3.color = RGBAColor.MakeRGBA(0.8f, 0.8f, 0.8f, 1f);
            baseElement3.scaleX = baseElement3.scaleY = 0.95f;
            baseElement3.getChild(1).x += 1.33f;
            NSString @string = Application.getString(1310835);
            Text text = Text.createWithFontandString(5, @string);
            text.anchor = text.parentAnchor = 18;
            _ = baseElement2.addChild(text);
            Text text2 = Text.createWithFontandString(5, @string);
            text2.anchor = text2.parentAnchor = 18;
            _ = baseElement3.addChild(text2);
            Button button = new Button().initWithUpElementDownElementandID(baseElement2, baseElement3, bid);
            button.delegateButtonDelegate = d;
            button.anchor = button.parentAnchor = 9;
            _ = baseElement.addChild(button);
            if (text.width > 0.8f * button.width)
            {
                text.scaleX = text.scaleY = text2.scaleX = text2.scaleY = 0.8f * button.width / text.width;
            }
            if (newmark)
            {
                Image image = Image.Image_createWithResIDQuad(num, 11);
                image.anchor = image.parentAnchor = 12;
                image.passTransformationsToChilds = false;
                image.x = -15f;
                image.setName(NSS("newCartoonsLabel"));
                Image image2 = Image.Image_createWithResIDQuad(num, 12);
                image2.anchor = image2.parentAnchor = 18;
                _ = image.addChild(image2);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
                timeline.addKeyFrame(KeyFrame.makeRotation(360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 4f));
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                _ = image.addTimeline(timeline);
                image.playTimeline(0);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(3);
                timeline2.addKeyFrame(KeyFrame.makePos(image2.x, image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos(image2.x, (double)(image2.y - 5f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                timeline2.addKeyFrame(KeyFrame.makePos(image2.x, image2.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                _ = image2.addTimeline(timeline2);
                image2.playTimeline(0);
                _ = baseElement.addChild(image);
            }
            baseElement.width = button.width;
            baseElement.height = button.height;
            return baseElement;
        }

        // Token: 0x060006AF RID: 1711 RVA: 0x000342C8 File Offset: 0x000324C8
        private Image createBackgroundWithLogo(bool l, bool mm = false)
        {
            Image image = Image.Image_createWithResIDQuad(66, 0);
            image.anchor = image.parentAnchor = 18;
            image.passTransformationsToChilds = false;
            image.scaleY = SCREEN_BG_SCALE_Y;
            image.scaleX = SCREEN_BG_SCALE_X;
            if (l || mm)
            {
                Image image2 = Image.Image_createWithResIDQuad(66, 1);
                image2.anchor = image2.parentAnchor = 34;
                image2.scaleY = SCREEN_BG_SCALE_Y * CHOOSE3(1.02, 1.18, 1.02);
                image2.scaleX = SCREEN_BG_SCALE_X;
                image2.y += SCREEN_OFFSET_Y;
                _ = image.addChild(image2);
            }
            if (l)
            {
                Image image3 = Image.Image_createWithResIDQuad(69, 0);
                image3.anchor = 10;
                image3.parentAnchor = 10;
                image3.y = 5f;
                image3.x += 0.33f;
                _ = image.addChild(image3);
                int num = 1 + Preferences._getIntForKey("PREFS_SELECTED_CANDY");
                TouchImage touchImage = TouchImage.TouchImage_createWithResIDQuad(69, num);
                touchImage.bid = 23;
                touchImage.delegateButtonDelegate = this;
                touchImage.setName(NSS("logoCandy"));
                Image.setElementPositionWithRelativeQuadOffset(touchImage, 69, 0, num);
                touchImage.anchor = touchImage.parentAnchor = 9;
                _ = image3.addChild(touchImage);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(4);
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.15, 1.15, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.1));
                timeline.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.08));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.25));
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_NO_LOOP);
                _ = touchImage.addTimeline(timeline);
                glowAnimation = Image.Image_createWithResIDQuad(75, 0);
                glowAnimation.parentAnchor = 9;
                glowAnimation.anchor = 18;
                glowAnimation.setEnabled(false);
                setElementPositionWithRelativeQuadOffset2(glowAnimation, 69, 0, 69, 4);
                _ = image3.addChild(glowAnimation);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(23);
                float num2 = 0.15f;
                timeline2.addKeyFrame(KeyFrame.makeScale(0.7, 0.7, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                for (int i = 0; i < 10; i++)
                {
                    timeline2.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num2));
                    timeline2.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num2));
                }
                timeline2.addKeyFrame(KeyFrame.makeScale(0.7, 0.7, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 3f));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                timeline2.addKeyFrame(KeyFrame.makeSingleAction(glowAnimation, "ACTION_SET_VISIBLE", 0, 0, 3.6));
                _ = glowAnimation.addTimeline(timeline2);
            }
            Image image4 = Image.Image_createWithResID(74);
            image4.anchor = image4.parentAnchor = 18;
            image4.scaleX = image4.scaleY = 2.3f;
            Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline3.addKeyFrame(KeyFrame.makeRotation(45.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline3.addKeyFrame(KeyFrame.makeRotation(405.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 75.0));
            timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            _ = image4.addTimeline(timeline3);
            image4.playTimeline(0);
            _ = image.addChild(image4);
            return image;
        }

        // Token: 0x060006B0 RID: 1712 RVA: 0x0003474C File Offset: 0x0003294C
        private void createMainMenu()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(true, false);
            VBox vbox = new VBox().initWithOffsetAlignWidth(5f, 2, SCREEN_WIDTH);
            vbox.anchor = vbox.parentAnchor = 10;
            vbox.y = 265f;
            Button button = createButtonWithTextIDDelegate(Application.getString(1310720), 0, this);
            _ = vbox.addChild(button);
            Button button2 = createButtonWithTextIDDelegate(Application.getString(1310721), 1, this);
            _ = vbox.addChild(button2);
            if (CTRPreferences.isLiteVersion())
            {
                Button button3 = createButtonWithTextIDDelegateAutoScale(Application.getString(1310723), 42, this);
                _ = vbox.addChild(button3);
            }
            _ = Application.getTexture(72);
            if (LANGUAGE != Language.LANG_ZH)
            {
                _ = Application.sharedPreferences().remoteDataManager.getHideSocialNetworks();
            }
            _ = CTRPreferences.isBannersMustBeShown();
            _ = image.addChild(vbox);
            BaseElement childWithName = image.getChildWithName(NSS("logoCandy"));
            BaseElement.calculateTopLeft(childWithName.parent);
            BaseElement.calculateTopLeft(childWithName);
            _ = Image.getQuadCenter(69, 1);
            Vector vector = vect(childWithName.drawX + (childWithName.width / 2) - 5f, childWithName.drawY + (childWithName.height / 2) - 5f);
            handAnimation = Image.Image_createWithResIDQuad(75, 1);
            handAnimation.rotationCenterX = -(float)handAnimation.width / 2;
            handAnimation.rotationCenterY = -(float)handAnimation.height / 2;
            handAnimation.setEnabled(false);
            _ = image.addChild(handAnimation);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(8);
            float num = 0.5f;
            timeline.addKeyFrame(KeyFrame.makePos(SCREEN_WIDTH, (double)(SCREEN_HEIGHT * 2f / 3f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos(SCREEN_WIDTH, (double)(SCREEN_HEIGHT * 2f / 3f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num));
            timeline.addKeyFrame(KeyFrame.makePos(vector.x, vector.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
            timeline.addKeyFrame(KeyFrame.makePos(vector.x, vector.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 2.0));
            timeline.addKeyFrame(KeyFrame.makePos(SCREEN_WIDTH, (double)(SCREEN_HEIGHT * 2f / 3f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 2.4));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            timeline.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.8 + (double)num));
            timeline.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            timeline.addKeyFrame(KeyFrame.makeSingleAction(handAnimation, "ACTION_SET_VISIBLE", 0, 0, 3.5));
            _ = handAnimation.addTimeline(timeline);
            timeline.delegateTimelineDelegate = this;
            ddMainMenu.cancelAllDispatches();
            ddMainMenu.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_playHandAnimation), null, 3f);
            _ = menuView.addChild(image);
            if (!Application.sharedPreferences().remoteDataManager.getHideMainPromo())
            {
                PromoBanner promoBanner = (PromoBanner)new PromoBanner().init();
                promoBanner.setName("promoBanner");
                _ = menuView.addChild(promoBanner);
            }
            addViewwithID(menuView, ViewID.VIEW_MAIN_MENU);
        }

        // Token: 0x060006B1 RID: 1713 RVA: 0x00034C08 File Offset: 0x00032E08
        private void createOptions()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(false, false);
            VBox vbox = new VBox().initWithOffsetAlignWidth(10.0, 2, SCREEN_WIDTH);
            vbox.anchor = vbox.parentAnchor = 18;
            vbox.y += 0.33f;
            _ = Application.getString(1310730);
            _ = Application.getString(1310728);
            _ = Application.getString(1310731);
            _ = Application.getString(1310729);
            ToggleButton toggleButton = createAudioButtonWithQuadDelegateID(2, this, 5);
            ToggleButton toggleButton2 = createAudioButtonWithQuadDelegateID(3, this, 6);
            HBox hbox = new HBox().initWithOffsetAlignHeight(14f, 16, toggleButton2.height);
            _ = hbox.addChild(toggleButton);
            _ = hbox.addChild(toggleButton2);
            _ = vbox.addChild(hbox);
            Button button = createButtonWithTextIDDelegateAutoScale(Application.getString(1310830), 39, this);
            Button button2 = createButtonWithTextIDDelegateAutoScale(Application.getString(1310829), 37, this);
            _ = vbox.addChild(button);
            _ = vbox.addChild(button2);
            bool flag = Preferences._getBooleanForKey("SOUND_ON");
            bool flag2 = Preferences._getBooleanForKey("MUSIC_ON");
            if (!flag)
            {
                toggleButton.toggle();
            }
            if (!flag2)
            {
                toggleButton2.toggle();
            }
            Button button3 = createButtonWithTextIDDelegate(Application.getString(1310725), 7, this);
            _ = vbox.addChild(button3);
            Button button4 = createButtonWithTextIDDelegate(Application.getString(1310724), 8, this);
            _ = vbox.addChild(button4);
            float num = 1f;
            switch (LANGUAGE)
            {
                case Language.LANG_JA:
                    num = 0.8f;
                    break;
                case Language.LANG_KO:
                    num = 0.8f;
                    break;
                case Language.LANG_ES:
                    num = 0.8f;
                    break;
                case Language.LANG_BR:
                    num = 0.8f;
                    break;
            }
            Button button5 = createButtonWithTextscaleTextIDDelegate(Application.getString(1310821), num, 54, this, 4, 0, 1);
            _ = vbox.addChild(button5);
            Button button6 = createBackButtonWithDelegateID(this, 9);
            _ = image.addChild(button6);
            if (CTRPreferences.isBannersMustBeShown())
            {
                button6.y -= 50f;
                vbox.y -= 30f;
            }
            _ = image.addChild(vbox);
            _ = menuView.addChild(image);
            addViewwithID(menuView, ViewID.VIEW_OPTIONS);
        }

        // Token: 0x060006B2 RID: 1714 RVA: 0x00034E50 File Offset: 0x00033050
        private void createReset()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(false, false);
            VBox vbox = new VBox().initWithOffsetAlignWidth(10.0, 2, SCREEN_WIDTH - 40.0);
            vbox.parentAnchor = vbox.anchor = 18;
            Text text = new Text().initWithFont(Application.getFont(5));
            text.setAlignment(2);
            text.setStringandWidth(Application.getString(1310735), vbox.width);
            _ = vbox.addChild(text);
            Text text2 = new Text().initWithFont(Application.getFont(6));
            text2.setAlignment(2);
            text2.setStringandWidth(Application.getString(1310777), vbox.width);
            _ = vbox.addChild(text2);
            _ = image.addChild(vbox);
            TimedButton timedButton = createTimedButtonWithTextIDDelegateTimer(Application.getString(1310747), 12, this, 3f);
            timedButton.anchor = timedButton.parentAnchor = 34;
            timedButton.y = -130f;
            Button button = createButtonWithTextIDDelegate(Application.getString(1310748), 13, this);
            button.anchor = button.parentAnchor = 34;
            button.y = -70f;
            vbox.y = -65f;
            _ = image.addChild(timedButton);
            _ = image.addChild(button);
            Button button2 = createBackButtonWithDelegateID(this, 10);
            _ = image.addChild(button2);
            _ = menuView.addChild(image);
            addViewwithID(menuView, ViewID.VIEW_RESET);
        }

        // Token: 0x060006B3 RID: 1715 RVA: 0x00034FD8 File Offset: 0x000331D8
        private void createMovieView()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
            rectangleElement.width = (int)SCREEN_WIDTH;
            rectangleElement.height = (int)SCREEN_HEIGHT;
            rectangleElement.color = RGBAColor.blackRGBA;
            _ = menuView.addChild(rectangleElement);
            addViewwithID(menuView, ViewID.VIEW_MOVIE);
        }

        // Token: 0x060006B4 RID: 1716 RVA: 0x00035038 File Offset: 0x00033238
        private void createTerms()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(false, false);
            VBox vbox = new VBox().initWithOffsetAlignWidth(5f, 2, SCREEN_WIDTH_EXPANDED - 20f);
            vbox.parentAnchor = vbox.anchor = 18;
            vbox.y = -20f;
            _ = image.addChild(vbox);
            Text text = new Text().initWithFont(Application.getFont(6));
            text.setAlignment(2);
            text.setStringandWidth(Application.getString(1310820), vbox.width);
            text.height -= 20;
            _ = vbox.addChild(text);
            Button button = createButtonWithTextIDDelegate(Application.getString(1310821), 0.8f, 35, this);
            _ = vbox.addChild(button);
            Button button2 = createButtonWithTextIDDelegate(Application.getString(1310822), 0.8f, 34, this);
            _ = vbox.addChild(button2);
            Button button3 = createBackButtonWithDelegateID(this, 32);
            _ = image.addChild(button3);
            _ = menuView.addChild(image);
            addViewwithID(menuView, ViewID.VIEW_TERMS);
        }

        // Token: 0x060006B5 RID: 1717 RVA: 0x00035150 File Offset: 0x00033350
        private void createAbout()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(false, false);
            _ = NSS("undefined version");
            string text;
            if (CTRPreferences.isLiteVersion())
            {
                text = " Free";
            }
            else
            {
                text = "";
            }
            VBox vbox = new VBox().initWithOffsetAlignWidth(0f, 2, 310f);
            Image image2 = Image.Image_createWithResIDQuad(69, 5);
            _ = vbox.addChild(image2);
            string text2 = Application.getString(1310784).ToString();
            string[] array = ["%@"];
            string[] array2 = text2.Split(array, 0);
            for (int i = 0; i < array2.Length; i++)
            {
                if (i == 0)
                {
                    text2 = text;
                }
                if (i == 2)
                {
                    string fullName = Assembly.GetExecutingAssembly().FullName;
                    text2 += fullName.Split(['='])[1].Split([','])[0];
                    text2 += " ";
                }
                text2 += array2[i];
            }
            Text text3 = new Text().initWithFont(Application.getFont(6));
            text3.setAlignment(2);
            text3.setStringandWidth(NSS(text2), 310f);
            _ = vbox.addChild(text3);
            aboutContainer = new ScrollableContainer().initWithWidthHeightContainer(310f, 350f, vbox);
            aboutContainer.anchor = aboutContainer.parentAnchor = 18;
            _ = image.addChild(aboutContainer);
            Button button = createBackButtonWithDelegateID(this, 10);
            _ = image.addChild(button);
            _ = menuView.addChild(image);
            addViewwithID(menuView, ViewID.VIEW_ABOUT);
        }

        // Token: 0x060006B6 RID: 1718 RVA: 0x00035310 File Offset: 0x00033510
        public static HBox createTextWithStar(string t)
        {
            HBox hbox = new HBox().initWithOffsetAlignHeight(0.0, 16, 50.0);
            Text text = new Text().initWithFont(Application.getFont(5));
            text.setString(NSS(t));
            _ = hbox.addChild(text);
            Image image = Image.Image_createWithResIDQuad(71, 3);
            _ = hbox.addChild(image);
            return hbox;
        }

        // Token: 0x060006B7 RID: 1719 RVA: 0x00035374 File Offset: 0x00033574
        private void createPackSelect()
        {
            _ = boxFabric.isZeroBoxDefined();
            NSREL(packSelect);
            packSelect = CTRPreferences.getPackSelectInfo(boxFabric.isZeroBoxDefined(), -1);
            _ = NSRET(packSelect);
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            Image image = createBackgroundWithLogo(false, false);
            HBox hbox = createTextWithStar(Application.getString(1310753).ToString().Replace("%d", CTRPreferences.getTotalStarsInDelivery(-1).ToString()));
            hbox.x = -10f;
            hbox.x += SCREEN_OFFSET_X;
            hbox.y -= SCREEN_OFFSET_Y;
            HBox hbox2 = new HBox().initWithOffsetAlignHeight(-50f, 16, SCREEN_HEIGHT);
            packContainer = new ScrollableContainer().initWithWidthHeightContainer(SCREEN_WIDTH_EXPANDED, SCREEN_HEIGHT, hbox2);
            packContainer.minAutoScrollToSpointLength = 5f;
            packContainer.shouldBounceHorizontally = true;
            packContainer.resetScrollOnShow = false;
            packContainer.touchMoveIgnoreLength = 15f;
            packContainer.x = -SCREEN_OFFSET_X;
            packContainer.turnScrollPointsOnWithCapacity(packSelect.size);
            packContainer.delegateScrollableContainerProtocol = this;
            hbox.anchor = hbox.parentAnchor = 12;
            _ = image.addChild(hbox);
            float num = 0f;
            for (int i = 0; i < packSelect.size; i++)
            {
                BaseElement baseElement = boxFabric.createPackElementforContainer(i, packSelect.content[i], packContainer, this);
                packSelect.elements[i] = baseElement;
                _ = hbox2.addChild(baseElement);
                baseElement.x -= 25f - SCREEN_OFFSET_X;
                baseElement.y -= 20f;
                num += (i != 0) ? (packSelect.elements[i - 1].width + -50f) : 0f;
                _ = packContainer.addScrollPointAtXY((double)num, 0.0);
            }
            hbox2.width += Math.Max(50, (int)(packSelect.size * SCREEN_OFFSET_X));
            _ = image.addChild(packContainer);
            int num2 = 48;
            Button button = createBackButtonWithDelegateID(this, num2);
            _ = image.addChild(button);
            unlockb = null;
            AnimationsPool animationsPool = (AnimationsPool)new AnimationsPool().init();
            Image image2 = Image.Image_createWithResID(11);
            image2.doRestoreCutTransparency();
            breakParticles = (StarsBreak)new StarsBreak().initWithTotalParticlesandImageGrid(10, image2);
            breakParticles.particlesDelegate = new Particles.ParticlesFinished(animationsPool.particlesFinished);
            breakParticles.posVar.x = 0f;
            breakParticles.posVar.y = 0f;
            breakParticles.y += 50f;
            _ = image.addChild(breakParticles);
            _ = image.addChild(animationsPool);
            _ = menuView.addChild(image);
            addViewwithID(menuView, 4);
            ddPackSelect.cancelAllDispatches();
            if (CTRPreferences.isBannersMustBeShown())
            {
                packContainer.y -= 50f;
                button.y -= 40f;
            }
            int num3 = MIN(packSelect.size - 1, CTRPreferences.getLastPack());
            packContainer.placeToScrollPoint((num3 != -1) ? num3 : packSelect.getFirstGameBox());
            if (packSelect.size >= 4)
            {
                liftScrollbar = HLiftScrollbar.createWithResIDBackQuadLiftQuadLiftQuadPressed(71, 5, 6, 7);
                liftScrollbar.anchor = liftScrollbar.parentAnchor = 34;
                liftScrollbar.delegateLiftScrollbarDelegate = this;
                liftScrollbar.setContainer(packContainer);
                liftScrollbar.y = -75f;
                bulletContainer = (BaseElement)new BaseElement().init();
                bulletContainer.width = liftScrollbar.width;
                bulletContainer.height = liftScrollbar.height;
                bulletContainer.anchor = bulletContainer.parentAnchor = 34;
                bulletContainer.blendingMode = 1;
                bulletContainer.y = liftScrollbar.y - 14f;
                _ = menuView.addChild(bulletContainer);
                int totalScrollPoints = liftScrollbar.getTotalScrollPoints();
                for (int j = 0; j < totalScrollPoints; j++)
                {
                    Vector scrollPoint = liftScrollbar.getScrollPoint(j);
                    TouchImage touchImage = TouchImage.TouchImage_createWithResIDQuad(71, 8);
                    touchImage.delegateButtonDelegate = this;
                    touchImage.bid = 3000 + j;
                    touchImage.x = scrollPoint.x - 2f;
                    touchImage.y = scrollPoint.y;
                    touchImage.parentAnchor = 9;
                    touchImage.anchor = 18;
                    _ = bulletContainer.addChild(touchImage);
                }
                _ = menuView.addChild(liftScrollbar);
                liftScrollbar.updateActiveSpoint();
                if (CTRPreferences.isBannersMustBeShown())
                {
                    bulletContainer.y -= 50f;
                    liftScrollbar.y -= 50f;
                }
            }
        }

        // Token: 0x060006B8 RID: 1720 RVA: 0x00035918 File Offset: 0x00033B18
        public virtual void scrollableContainerreachedScrollPoint(ScrollableContainer e, int i)
        {
            if (i > packSelect.size)
            {
                return;
            }
            currentPackIndex = i;
            int num = packSelect.content[i];
            if (BoxFabric.isGameBox(num))
            {
                int saveIndex = BoxFabric.getSaveIndex(num);
                BaseElement baseElement = packSelect.elements[i];
                baseElement.getChildWithName(NSS("boxContainer")).playTimeline(0);
                int unlockedForPackLevel = (int)CTRPreferences.getUnlockedForPackLevel(saveIndex, 0);
                BaseElement childWithName = baseElement.getChildWithName(NSS("lockHideMe"));
                if (childWithName != null && (unlockedForPackLevel == 2 || unlockedForPackLevel == 3))
                {
                    CTRPreferences.setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_UNLOCKED, saveIndex, 0);
                    childWithName.playTimeline(0);
                    if (unlockedForPackLevel == 3)
                    {
                        breakParticles.stopSystem();
                        breakParticles.startSystem(10);
                        CTRSoundMgr._playSound(37);
                    }
                }
                if (packSelect.nextpack == i)
                {
                    packSelect.nextpack = -1;
                    if (unlockedForPackLevel == 0)
                    {
                        if (CTRPreferences.isLiteVersion())
                        {
                            showBuyFullPopup();
                        }
                        else
                        {
                            showCantUnlockPopupForPack(activeView(), saveIndex);
                        }
                    }
                }
                if (!promobHidden && promob != null)
                {
                    promob.playTimeline(1);
                    promobHidden = true;
                    return;
                }
            }
            else if (promobHidden && promob != null && !CTRPreferences.isBannersMustBeShown())
            {
                promob.playTimeline(0);
                promobHidden = false;
            }
        }

        // Token: 0x060006B9 RID: 1721 RVA: 0x00035A5F File Offset: 0x00033C5F
        public virtual void scrollableContainerchangedTargetScrollPoint(ScrollableContainer e, int i)
        {
            CTRPreferences.setLastPack(i);
        }

        // Token: 0x060006BA RID: 1722 RVA: 0x00035A68 File Offset: 0x00033C68
        private BaseElement createButtonForLevelPack(int l, int p)
        {
            bool flag = l >= CTRPreferences.getLevelsInPackCount() && CTRPreferences.isLiteVersion();
            int num = 2 * (flag ? 1 : 0);
            if (num == 0)
            {
                num = (CTRPreferences.getUnlockedForPackLevel(p, l) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED) ? 1 : 0;
            }
            int starsForPackLevel = CTRPreferences.getStarsForPackLevel(p, l);
            TouchBaseElement touchBaseElement = (TouchBaseElement)new TouchBaseElement().init();
            touchBaseElement.bbc = MakeRectangle(5.0, 0.0, -10.0, 0.0);
            touchBaseElement.delegateButtonDelegate = this;
            Image image;
            if (num == 2)
            {
                touchBaseElement.bid = 40;
                image = Image.Image_createWithResIDQuad(70, 1);
                image.doRestoreCutTransparency();
            }
            else if (num == 1)
            {
                touchBaseElement.bid = -1;
                image = Image.Image_createWithResIDQuad(70, 0);
                image.doRestoreCutTransparency();
            }
            else
            {
                touchBaseElement.bid = 1000 + l;
                image = Image.Image_createWithResIDQuad(70, 2);
                image.doRestoreCutTransparency();
                Text text = new Text().initWithFont(Application.getFont(5));
                NSString nsstring = NSS((l + 1).ToString());
                text.setString(nsstring);
                text.anchor = text.parentAnchor = 18;
                text.y -= 5f;
                _ = image.addChild(text);
                Image image2 = Image.Image_createWithResIDQuad(70, 3 + starsForPackLevel);
                image2.doRestoreCutTransparency();
                image2.anchor = image2.parentAnchor = 9;
                _ = image.addChild(image2);
            }
            image.anchor = image.parentAnchor = 18;
            _ = touchBaseElement.addChild(image);
            touchBaseElement.setSizeToChildsBounds();
            return touchBaseElement;
        }

        // Token: 0x060006BB RID: 1723 RVA: 0x00035C18 File Offset: 0x00033E18
        private void createLevelSelect()
        {
            MenuView menuView = (MenuView)new MenuView().initFullscreen();
            int num = 216 + pack;
            Image image = Image.Image_createWithResIDQuad(num, 0);
            Image image2 = Image.Image_createWithResIDQuad(num, 0);
            image.rotationCenterX = -(float)image.width / 2;
            image.x += 0.33f;
            image2.rotationCenterX = image.rotationCenterX;
            image2.rotation = 180f;
            image2.x = SCREEN_WIDTH;
            image2.y -= 0.5f;
            image2.x -= 0.33f;
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            baseElement.parentAnchor = baseElement.anchor = 18;
            baseElement.scaleY = SCREEN_BG_SCALE_Y;
            baseElement.scaleX = SCREEN_BG_SCALE_X;
            baseElement.passTransformationsToChilds = true;
            _ = baseElement.addChild(image);
            _ = baseElement.addChild(image2);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.85, 0.85, 0.85, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            _ = baseElement.addTimeline(timeline);
            baseElement.setName("levelsBack");
            _ = menuView.addChild(baseElement);
            Image image3 = Image.Image_createWithResIDQuad(7, 0);
            Image image4 = Image.Image_createWithResIDQuad(7, 1);
            image3.x = 141f;
            image3.y = 25f;
            image4.x = 160f;
            image4.y = 25f;
            _ = baseElement.addChild(image3);
            _ = baseElement.addChild(image4);
            Image image5 = Image.Image_createWithResID(74);
            image5.setName("shadow");
            image5.anchor = image5.parentAnchor = 18;
            image5.scaleX = image5.scaleY = 2.3f;
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makeScale(2.0, 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline2.addKeyFrame(KeyFrame.makeScale(5.0, 5.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            timeline2.delegateTimelineDelegate = this;
            _ = image5.addTimeline(timeline2);
            Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline3.addKeyFrame(KeyFrame.makeRotation(45.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline3.addKeyFrame(KeyFrame.makeRotation(405.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 75.0));
            timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            _ = image5.addTimeline(timeline3);
            image5.playTimeline(1);
            _ = menuView.addChild(image5);
            HBox hbox = createTextWithStar(CTRPreferences.getTotalStarsInPack(pack).ToString() + "/" + (CTRPreferences.getLevelsInPackCount() * 3).ToString());
            hbox.x = -10f;
            hbox.y -= SCREEN_OFFSET_Y;
            hbox.x += SCREEN_OFFSET_X;
            float num2 = 8f;
            float num3 = -9f;
            float num4 = 63f;
            VBox vbox = new VBox().initWithOffsetAlignWidth(num2, 2, SCREEN_WIDTH);
            vbox.setName("levelsBox");
            vbox.x = 0f;
            vbox.y = 50f;
            Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline4.addKeyFrame(KeyFrame.makePos(vbox.x, vbox.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline4.addKeyFrame(KeyFrame.makePos(vbox.x, (double)(vbox.y - WVGAD(25.0)), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = vbox.addTimeline(timeline4);
            Timeline timeline5 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline5.addKeyFrame(KeyFrame.makePos(vbox.x, (double)(vbox.y - WVGAD(25.0)), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline5.addKeyFrame(KeyFrame.makePos(vbox.x, vbox.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = vbox.addTimeline(timeline5);
            int num5 = 5;
            int num6 = 0;
            for (int i = 0; i < num5; i++)
            {
                HBox hbox2 = new HBox().initWithOffsetAlignHeight(num3, 16, num4);
                for (int j = 0; j < num5; j++)
                {
                    _ = hbox2.addChild(createButtonForLevelPack(num6++, pack));
                }
                _ = vbox.addChild(hbox2);
            }
            Timeline timeline6 = new Timeline().initWithMaxKeyFramesOnTrack(3);
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            _ = vbox.addTimeline(timeline6);
            hbox.anchor = hbox.parentAnchor = 12;
            hbox.setName("starText");
            Timeline timeline7 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline7.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline7.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            _ = hbox.addTimeline(timeline7);
            _ = menuView.addChild(hbox);
            _ = menuView.addChild(vbox);
            Button button = createBackButtonWithDelegateID(this, 11);
            _ = menuView.addChild(button);
            button.setName("backButton");
            Timeline timeline8 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline8.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline8.addKeyFrame(KeyFrame.makePos(0.0, (double)-(double)WVGAD(50.0), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = button.addTimeline(timeline8);
            Timeline timeline9 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline9.addKeyFrame(KeyFrame.makePos(0.0, (double)-(double)WVGAD(50.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline9.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3));
            _ = button.addTimeline(timeline9);
            Timeline timeline10 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline10.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline10.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            _ = button.addTimeline(timeline10);
            addViewwithID(menuView, ViewID.VIEW_LEVEL_SELECT);
        }

        // Token: 0x060006BC RID: 1724 RVA: 0x00036314 File Offset: 0x00034514
        private void createDeliverySelect()
        {
            DeliverySelectView deliverySelectView = (DeliverySelectView)new DeliverySelectView().initFullscreenBackgroundDelegate(createBackgroundWithLogo(false, false), this);
            addViewwithID(deliverySelectView, ViewID.VIEW_DELIVERY_SELECT);
        }

        // Token: 0x060006BD RID: 1725 RVA: 0x00036344 File Offset: 0x00034544
        private void createCartoonsSelect()
        {
            CartoonsSelectView cartoonsSelectView = (CartoonsSelectView)new CartoonsSelectView().initFullscreenBackgroundDelegate(createBackgroundWithLogo(false, true), this);
            addViewwithID(cartoonsSelectView, ViewID.VIEW_CARTOONS_SELECT);
        }

        // Token: 0x060006BE RID: 1726 RVA: 0x00036374 File Offset: 0x00034574
        private void createCartoonsAfterwatch()
        {
            CartoonsAfterwatchView cartoonsAfterwatchView = (CartoonsAfterwatchView)new CartoonsAfterwatchView().initFullscreenBackgroundDelegate(createBackgroundWithLogo(false, false), this);
            addViewwithID(cartoonsAfterwatchView, ViewID.VIEW_CARTOONS_AFTERWATCH);
        }

        // Token: 0x060006BF RID: 1727 RVA: 0x000363A4 File Offset: 0x000345A4
        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                boxFabric = (BoxFabric)new BoxFabricOriginal().init();
                needRecreate = false;
                needUnlock = false;
                statusBackupRestore = 0;
                animationStartPackIndex = 0;
                currentPackIndex = 0;
                unlockAnimation = false;
                ddMainMenu = (DelayedDispatcher)new DelayedDispatcher().init();
                ddPackSelect = (DelayedDispatcher)new DelayedDispatcher().init();
                ep = null;
                createMainMenu();
                createOptions();
                createReset();
                createAbout();
                createTerms();
                createMovieView();
                createPackSelect();
                createDeliverySelect();
                createAchievements();
                createLeaderboards();
                createCartoonsSelect();
                createCartoonsAfterwatch();
                MapPickerController mapPickerController = (MapPickerController)new MapPickerController().initWithParent(this);
                addChildwithID(mapPickerController, 0);
            }
            CtrRenderer.gUseFingerDelta = true;
            return this;
        }

        // Token: 0x060006C0 RID: 1728 RVA: 0x00036494 File Offset: 0x00034694
        public override void dealloc()
        {
            ddMainMenu.cancelAllDispatches();
            ddMainMenu.dealloc();
            ddMainMenu = null;
            ddPackSelect.cancelAllDispatches();
            ddPackSelect.dealloc();
            ddPackSelect = null;
            base.dealloc();
        }

        // Token: 0x060006C1 RID: 1729 RVA: 0x000364E4 File Offset: 0x000346E4
        public override void activate()
        {
            packSelect.nextpack = -1;
            showNextPackStatus = false;
            base.activate();
            if (viewToShow == ViewID.VIEW_LEVEL_SELECT)
            {
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                pack = ctrrootController.getPack();
                preLevelSelect();
            }
            else
            {
                AndroidAPI.showBanner();
            }
            showView(viewToShow);
            CTRSoundMgr._stopMusic();
            CTRSoundMgr._playMusic(58);
        }

        // Token: 0x060006C2 RID: 1730 RVA: 0x00036550 File Offset: 0x00034750
        public virtual void showNextPack()
        {
            bool flag = false;
            for (int i = currentPackIndex + 1; i < packSelect.size; i++)
            {
                if (BoxFabric.isGameBox(packSelect.content[i]))
                {
                    flag = true;
                }
            }
            if (!flag && CTRPreferences.isInLastDelivery())
            {
                replayingIntroMovie = false;
                CTRSoundMgr._stopMusic();
                NSString nsstring = NSS("outro.wmv");
                Application.sharedMovieMgr().delegateMovieMgrDelegate = this;
                Application.sharedMovieMgr().playURL(nsstring, !Preferences._getBooleanForKey("SOUND_ON"));
                return;
            }
            if (currentPackIndex <= packSelect.size - 2)
            {
                packSelect.nextpack = currentPackIndex + 1;
                packContainer.moveToScrollPointmoveMultiplier(packSelect.nextpack, 0.8);
            }
        }

        // Token: 0x060006C3 RID: 1731 RVA: 0x00036620 File Offset: 0x00034820
        public override void onChildDeactivated(int n)
        {
            base.onChildDeactivated(n);
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            ctrrootController.setSurvival(false);
            base.deactivate();
        }

        // Token: 0x060006C4 RID: 1732 RVA: 0x0003664C File Offset: 0x0003484C
        public virtual void moviePlaybackFinished(NSString url)
        {
            if (replayingIntroMovie)
            {
                hideActiveView();
                replayingIntroMovie = false;
                activateChild(CHILD_TYPE.CHILD_PICKER);
                return;
            }
            CTRSoundMgr._playMusic(58);
            AndroidAPI.showBanner();
            if (url != null && url.rangeOfString(NSS("outro.wmv")).length > 0U)
            {
                showView(ViewID.VIEW_PACK_SELECT);
                packContainer.moveToScrollPointmoveMultiplier(packSelect.size - 1, 0.8);
                if (!CTRPreferences.isLiteVersion())
                {
                    showGameFinishedPopup(getView(ViewID.VIEW_PACK_SELECT));
                }
                return;
            }
            deleteView(ViewID.VIEW_PACK_SELECT);
            createPackSelect();
            showView(ViewID.VIEW_PACK_SELECT);
        }

        // Token: 0x060006C5 RID: 1733 RVA: 0x000366F0 File Offset: 0x000348F0
        private void preLevelSelect()
        {
            ResourceMgr resourceMgr = Application.sharedResourceMgr();
            int[] array = null;
            switch (pack)
            {
                case 0:
                    array = PACK_GAME_COVER_01;
                    break;
                case 1:
                    array = PACK_GAME_COVER_02;
                    break;
                case 2:
                    array = PACK_GAME_COVER_03;
                    break;
                case 3:
                    array = PACK_GAME_COVER_04;
                    break;
                case 4:
                    array = PACK_GAME_COVER_05;
                    break;
                case 5:
                    array = PACK_GAME_COVER_06;
                    break;
                case 6:
                    array = PACK_GAME_COVER_07;
                    break;
                case 7:
                    array = PACK_GAME_COVER_08;
                    break;
                case 8:
                    array = PACK_GAME_COVER_09;
                    break;
                case 9:
                    array = PACK_GAME_COVER_10;
                    break;
                case 10:
                    array = PACK_GAME_COVER_11;
                    break;
                case 11:
                    array = PACK_GAME_COVER_12;
                    break;
                case 12:
                    array = PACK_GAME_COVER_13;
                    break;
                case 13:
                    array = PACK_GAME_COVER_14;
                    break;
            }
            resourceMgr.initLoading();
            resourceMgr.loadPack(array);
            resourceMgr.loadImmediately();
            if (getView(ViewID.VIEW_LEVEL_SELECT) != null)
            {
                deleteView(ViewID.VIEW_LEVEL_SELECT);
            }
            createLevelSelect();
        }

        // Token: 0x060006C6 RID: 1734 RVA: 0x000367E4 File Offset: 0x000349E4
        private void showIntroIfNeeded()
        {
            int i = 0;
            int packsCount = CTRPreferences.getPacksCount();
            while (i < packsCount)
            {
                GameController.checkForBoxPerfect(i);
                i++;
            }
            replayingIntroMovie = false;
            if (CTRPreferences.getUnlockedForPackLevel(0, 1) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
            {
                showView(ViewID.VIEW_MOVIE);
                CTRSoundMgr._stopMusic();
                NSString nsstring = NSS("intro.wmv");
                Application.sharedMovieMgr().delegateMovieMgrDelegate = this;
                Application.sharedMovieMgr().playURL(nsstring, !Preferences._getBooleanForKey("SOUND_ON"));
                return;
            }
            moviePlaybackFinished(null);
        }

        // Token: 0x060006C7 RID: 1735 RVA: 0x0003685C File Offset: 0x00034A5C
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
            if (t.element == handAnimation && k.trackType == Track.TrackType.TRACK_SCALE && (i == 2 || i == 5))
            {
                CTRPreferences ctrpreferences = Application.sharedPreferences();
                int intForKey = ctrpreferences.getIntForKey("PREFS_SELECTED_CANDY");
                Image image = (Image)activeView().getChildWithName(NSS("logoCandy"));
                int num = 1 + intForKey;
                int num2 = (num != 3) ? 3 : 1;
                if (i == 2)
                {
                    image.setDrawQuad(num2);
                    setElementPositionWithRelativeQuadOffset2(image, 69, 0, 69, num2);
                }
                else
                {
                    image.setDrawQuad(num);
                    setElementPositionWithRelativeQuadOffset2(image, 69, 0, 69, num);
                }
                image.playTimeline(0);
            }
        }

        // Token: 0x060006C8 RID: 1736 RVA: 0x00036900 File Offset: 0x00034B00
        public virtual void timelineFinished(Timeline t)
        {
            if (t.element != handAnimation)
            {
                CTRSoundMgr._stopMusic();
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                ctrrootController.setPack(pack);
                ctrrootController.setLevel(level);
                Application.sharedRootController().setViewTransition(-1);
                MapPickerController mapPickerController = (MapPickerController)getChild(0);
                mapPickerController.setAutoLoadMap(LevelsList.LEVEL_NAMES[pack, level]);
                activateChild(CHILD_TYPE.CHILD_PICKER);
                AndroidAPI.hideBanner();
            }
        }

        // Token: 0x060006C9 RID: 1737 RVA: 0x00036984 File Offset: 0x00034B84
        public virtual void onButtonPressed(int n)
        {
            _ = activeView().onTouchMoveXY(-10000f, -10000f);
            if (n is not -1 and not 14 and not 40)
            {
                CTRSoundMgr._playSound(21);
            }
            if (n is >= 1000 and < 2000)
            {
                level = n - 1000;
                int starsForPackLevel = CTRPreferences.getStarsForPackLevel(pack, level);
                string text = "LEVSEL_LEVEL_PRESSED";
                List<string> list =
                [
                    "already_won",
                    (starsForPackLevel > 0).ToString(),
                    "box_id",
                    pack.ToString(),
                    "level_id",
                    level.ToString(),
                ];
                FlurryAPI.logEventwithParams(text, list, true, false, false);
                BaseElement childWithName = activeView().getChildWithName("levelsBox");
                childWithName.playTimeline(2);
                BaseElement childWithName2 = activeView().getChildWithName("shadow");
                childWithName2.playTimeline(0);
                BaseElement childWithName3 = activeView().getChildWithName("levelsBack");
                childWithName3.playTimeline(0);
                BaseElement childWithName4 = activeView().getChildWithName("starText");
                childWithName4.playTimeline(0);
                BaseElement childWithName5 = activeView().getChildWithName("backButton");
                childWithName5.touchable = false;
                childWithName5.playTimeline(2);
                return;
            }
            if (n is >= 2000 and < 3000)
            {
                AndroidAPI.hideBanner();
                pack = n - 2000;
                if (pack >= CTRPreferences.getPacksCount())
                {
                    if (CTRPreferences.isLiteVersion() && pack < CTRPreferences.getPacksCount(false))
                    {
                        showBuyFullPopup();
                    }
                    return;
                }
                bool flag = CTRPreferences.getUnlockedForPackLevel(pack, 0) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED;
                string text2 = "BOXSEL_BOX_PRESSED";
                List<string> list2 = ["box_id", pack.ToString(), "box_unlocked", (!flag).ToString()];
                FlurryAPI.logEventwithParams(text2, list2, true, false, false);
                if (flag)
                {
                    showCantUnlockPopupForPack(activeView(), pack);
                    return;
                }
                preLevelSelect();
                showView(ViewID.VIEW_LEVEL_SELECT);
                return;
            }
            else
            {
                if (n is >= 3000 and < 4000)
                {
                    packContainer.moveToScrollPointmoveMultiplier(n - 3000, 1f);
                    return;
                }
                if (n >= 4000)
                {
                    int num = n - 4000;
                    BlockConfig blockConfig = VideoDataManager.getBlockConfig();
                    BlockInterface blockInterface;
                    if (blockConfig.getTotalBlocks() != 0)
                    {
                        blockInterface = blockConfig.getBlock(num);
                        VideoDataManager.setLastActivated(num);
                    }
                    else
                    {
                        blockInterface = blockConfig.getBlock(-1);
                        VideoDataManager.setLastActivated(-1);
                    }
                    int type = blockInterface.getType();
                    NSString id = blockInterface.getId();
                    if (type == 1)
                    {
                        string text3 = "CARTOONSEL_CARTOON_PRESSED";
                        List<string> list3 = ["cartoon_id", id.ToString()];
                        FlurryAPI.logEventwithParams(text3, list3, true, true, false);
                    }
                    else
                    {
                        string text4 = "CARTOONSEL_BANNER_PRESSED";
                        List<string> list4 = ["banner_id", id.ToString()];
                        FlurryAPI.logEventwithParams(text4, list4, true, true, false);
                    }
                    CartoonsAfterwatchView cartoonsAfterwatchView = (CartoonsAfterwatchView)getView(ViewID.VIEW_CARTOONS_AFTERWATCH);
                    cartoonsAfterwatchView.renumberEpisode(blockInterface.getNumber());
                    cartoonsAfterwatchView.setLast(blockConfig.getNextSameType(blockInterface) == -1);
                    CartoonsSelectView cartoonsSelectView = (CartoonsSelectView)getView(ViewID.VIEW_CARTOONS_SELECT);
                    cartoonsSelectView.notifyBlockWatched(num);
                    if (type == 1 && activeViewID != 12)
                    {
                        cartoonsSelectView.openCurtain();
                        showView(ViewID.VIEW_CARTOONS_AFTERWATCH);
                    }
                    NSString url = blockInterface.getUrl();
                    if (url != null && url.length() != 0)
                    {
                        if (type == 1)
                        {
                            CTRPreferences.setCartoonWatched(url);
                        }
                        AndroidAPI.openUrl(url);
                    }
                    return;
                }
                switch (n)
                {
                    case 0:
                        FlurryAPI.logEvent("MMENU_PLAYBT_PRESSED", null);
                        if (CTRPreferences.getGameSessionsCount() == 1 && CTRPreferences.getAttemptsForPackLevel(0, 0) == 0)
                        {
                            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                            ctrrootController.setPack(0);
                            ctrrootController.setLevel(0);
                            preLevelSelect();
                            ctrrootController.setViewTransition(-1);
                            MapPickerController mapPickerController = (MapPickerController)getChild(0);
                            mapPickerController.setAutoLoadMap(MapPickerController.getLevelNameForPackLevel(0, 0));
                            replayingIntroMovie = true;
                            showView(ViewID.VIEW_MOVIE);
                            CTRSoundMgr._stopMusic();
                            NSString nsstring = NSS("intro.wmv");
                            Application.sharedMovieMgr().delegateMovieMgrDelegate = this;
                            Application.sharedMovieMgr().playURL(nsstring, !Preferences._getBooleanForKey("SOUND_ON"));
                            return;
                        }
                        CTRSoundMgr._playMusic(58);
                        AndroidAPI.showBanner();
                        showView(ViewID.VIEW_DELIVERY_SELECT);
                        return;
                    case 1:
                        FlurryAPI.logEvent("MMENU_SETBT_PRESSED", null);
                        showView(ViewID.VIEW_OPTIONS);
                        return;
                    case 2:
                        {
                            pack = ((CTRRootController)Application.sharedRootController()).getPack();
                            preLevelSelect();
                            Application.sharedRootController().setViewTransition(-1);
                            MapPickerController mapPickerController2 = (MapPickerController)getChild(0);
                            mapPickerController2.setNormalMode();
                            activateChild(CHILD_TYPE.CHILD_PICKER);
                            return;
                        }
                    case 3:
                        Scorer.activateScorerUIAtProfile();
                        return;
                    case 4:
                        {
                            string text5 = "BUYFULL_PRESSED";
                            List<string> list5 = ["stars_collected", CTRPreferences.getTotalStars().ToString()];
                            FlurryAPI.logEvent(text5, list5);
                            CTRRootController.openFullVersionPage();
                            ep.hidePopup();
                            ep = null;
                            return;
                        }
                    case 5:
                        {
                            bool flag2 = !Preferences._getBooleanForKey("SOUND_ON");
                            Preferences._setBooleanforKey(flag2, "SOUND_ON", true);
                            string text6 = "SETSCR_SOUNDBT_PRESSED";
                            List<string> list6 = ["sound_on", flag2.ToString()];
                            FlurryAPI.logEventwithParams(text6, list6, true, false, false);
                            return;
                        }
                    case 6:
                        {
                            Thread.Sleep(100);
                            bool flag3 = !Preferences._getBooleanForKey("MUSIC_ON");
                            Preferences._setBooleanforKey(flag3, "MUSIC_ON", true);
                            string text7 = "SETSCR_MUSICBT_PRESSED";
                            List<string> list7 = ["music_on", flag3.ToString()];
                            FlurryAPI.logEventwithParams(text7, list7, true, false, false);
                            if (flag3)
                            {
                                CTRSoundMgr._playMusic(58);
                                return;
                            }
                            CTRSoundMgr._stopMusic();
                            return;
                        }
                    case 7:
                        AndroidAPI.hideBanner();
                        aboutContainer.setScroll(vect(0.0, 0.0));
                        aboutAutoScroll = true;
                        FlurryAPI.logEvent("SETSCR_ABOUTBT_PRESSED", null);
                        showView(ViewID.VIEW_ABOUT);
                        return;
                    case 8:
                        AndroidAPI.hideBanner();
                        showView(ViewID.VIEW_RESET);
                        FlurryAPI.logEvent("SETSCR_PROGRESSBT_PRESSED", null);
                        return;
                    case 9:
                        AndroidAPI.showBanner();
                        showView(ViewID.VIEW_MAIN_MENU);
                        return;
                    case 10:
                    case 36:
                    case 38:
                        AndroidAPI.showBanner();
                        showView(ViewID.VIEW_OPTIONS);
                        return;
                    case 11:
                        {
                            FlurryAPI.logEvent("LEVSEL_BACKBT_PRESSED", null);
                            AndroidAPI.showBanner();
                            Application.sharedRootController().setViewTransition(4);
                            Application.sharedRootController().setTransitionTime();
                            Application.sharedRootController().onControllerViewHide(getView(ViewID.VIEW_LEVEL_SELECT));
                            deleteView(ViewID.VIEW_LEVEL_SELECT);
                            ResourceMgr resourceMgr = Application.sharedResourceMgr();
                            resourceMgr.freePack(PACK_GAME_COVER_01);
                            resourceMgr.freePack(PACK_GAME_COVER_02);
                            if (!CTRPreferences.isLiteVersion())
                            {
                                resourceMgr.freePack(PACK_GAME_COVER_03);
                                resourceMgr.freePack(PACK_GAME_COVER_04);
                                resourceMgr.freePack(PACK_GAME_COVER_05);
                                resourceMgr.freePack(PACK_GAME_COVER_06);
                                resourceMgr.freePack(PACK_GAME_COVER_07);
                                resourceMgr.freePack(PACK_GAME_COVER_08);
                                resourceMgr.freePack(PACK_GAME_COVER_09);
                                resourceMgr.freePack(PACK_GAME_COVER_10);
                                resourceMgr.freePack(PACK_GAME_COVER_11);
                                resourceMgr.freePack(PACK_GAME_COVER_12);
                                resourceMgr.freePack(PACK_GAME_COVER_13);
                            }
                            showView(ViewID.VIEW_PACK_SELECT);
                            AndroidAPI.showBanner();
                            GC.Collect();
                            return;
                        }
                    case 12:
                        {
                            FlurryAPI.logEvent("SETSCR_RESET_APPLIED", null);
                            CTRPreferences ctrpreferences = Application.sharedPreferences();
                            ctrpreferences.resetToDefaults();
                            ctrpreferences.savePreferences();
                            deleteView(ViewID.VIEW_PACK_SELECT);
                            pack = 0;
                            AndroidAPI.showBanner();
                            showView(ViewID.VIEW_OPTIONS);
                            return;
                        }
                    case 13:
                        AndroidAPI.showBanner();
                        showView(ViewID.VIEW_OPTIONS);
                        return;
                    case 14:
                    case 15:
                    case 24:
                    case 25:
                    case 26:
                    case 28:
                    case 29:
                        return;
                    case 16:
                        if (activeViewID == 11)
                        {
                            FlurryAPI.logEvent("CARTOONSEL_TWITTER_PRESSED", null);
                        }
                        else
                        {
                            FlurryAPI.logEvent("MMENU_TWITTER_PRESSED", null);
                        }
                        AndroidAPI.openUrl("https://mobile.twitter.com/zeptolab");
                        return;
                    case 17:
                        if (activeViewID == 11)
                        {
                            FlurryAPI.logEvent("CARTOONSEL_FACEBOOK_PRESSED", null);
                        }
                        else
                        {
                            FlurryAPI.logEvent("MMENU_FACEBOOK_PRESSED", null);
                        }
                        AndroidAPI.openUrl("http://www.facebook.com/cuttherope");
                        return;
                    case 18:
                        AndroidAPI.exitApp();
                        return;
                    case 19:
                        throw new NotImplementedException();
                    case 20:
                        ep.hidePopup();
                        ep = null;
                        FLAG_RESTORING = false;
                        return;
                    case 21:
                        break;
                    case 22:
                        {
                            string text8 = "BOXSEL_MISSING-OK_PRESSED";
                            List<string> list8 = ["box_id", currentPackIndex.ToString()];
                            FlurryAPI.logEventwithParams(text8, list8, true, true, false);
                            break;
                        }
                    case 23:
                        {
                            FlurryAPI.logEvent("MMENU_CANDY_PRESSED", null);
                            CTRPreferences ctrpreferences2 = Application.sharedPreferences();
                            int num2 = ctrpreferences2.getIntForKey("PREFS_SELECTED_CANDY") + 1;
                            if (num2 >= 3)
                            {
                                num2 = 0;
                            }
                            ctrpreferences2.setIntforKey(num2, "PREFS_SELECTED_CANDY", true);
                            Image image = (Image)activeView().getChildWithName(NSS("logoCandy"));
                            int num3 = 1 + num2;
                            image.setDrawQuad(num3);
                            setElementPositionWithRelativeQuadOffset2(image, 69, 0, 69, num3);
                            image.playTimeline(0);
                            ctrpreferences2.setBooleanforKey(true, "PREFS_CANDY_WAS_CHANGED", true);
                            return;
                        }
                    case 27:
                        AndroidAPI.openUrl("http://www.amazon.com/gp/mas/dl/android?p=com.zeptolab.ctrexperiments.hd.amazon.paid");
                        return;
                    case 30:
                        AndroidAPI.openUrl("http://www.amazon.com/gp/mas/dl/android?p=com.zeptolab.ctrexperiments.hd.amazon.paid");
                        return;
                    case 31:
                        AndroidAPI.openUrl("http://www.facebook.com/cuttherope");
                        return;
                    case 32:
                        showView(ViewID.VIEW_ABOUT);
                        return;
                    case 33:
                        showView(ViewID.VIEW_TERMS);
                        return;
                    case 34:
                        AndroidAPI.openUrl(Application.getString(1310824));
                        return;
                    case 35:
                        AndroidAPI.openUrl(Application.getString(1310823));
                        return;
                    case 37:
                        FlurryAPI.logEvent("SETSCR_ACHIEVEMENTS_PRESSED", null);
                        if (AchievementsView.Init)
                        {
                            ((AchievementsView)views[8]).resetScroll();
                            showView(ViewID.VIEW_ACHIEVEMENTS);
                            return;
                        }
                        return;
                    case 39:
                        FlurryAPI.logEvent("SETSCR_LEADERBOARDS_PRESSED", null);
                        showView(ViewID.VIEW_LEADERBOARDS);
                        return;
                    case 40:
                    case 41:
                        if (4 != activeViewID)
                        {
                            showBuyFullPopup();
                            return;
                        }
                        return;
                    case 42:
                        showBuyFullPopup();
                        return;
                    case 43:
                    case 44:
                    case 45:
                        {
                            int num4 = n - 43;
                            string text9 = "SEASONSEL_SEASON_PRESSED";
                            List<string> list9 = ["season_id", num4.ToString()];
                            FlurryAPI.logEvent(text9, list9);
                            CTRPreferences.setLastDelivery(num4);
                            deleteView(ViewID.VIEW_PACK_SELECT);
                            createPackSelect();
                            if (num4 == 0)
                            {
                                showIntroIfNeeded();
                                return;
                            }
                            showView(ViewID.VIEW_PACK_SELECT);
                            return;
                        }
                    case 46:
                        {
                            if (n == 46)
                            {
                                FlurryAPI.logEvent("SEASONSEL_CARTOONS_PRESSED", null);
                            }
                            CartoonsSelectView cartoonsSelectView2 = (CartoonsSelectView)getView(ViewID.VIEW_CARTOONS_SELECT);
                            if (cartoonsSelectView2.isRebuildNeeded())
                            {
                                cartoonsSelectView2.rebuild();
                            }
                            showView(ViewID.VIEW_CARTOONS_SELECT);
                            return;
                        }
                    case 47:
                        CTRPreferences.setLastDelivery(CTRPreferences.getLastDelivery() + 1);
                        needRecreate = true;
                        return;
                    case 48:
                        showView(ViewID.VIEW_DELIVERY_SELECT);
                        return;
                    case 49:
                        {
                            int lastActivated = VideoDataManager.getLastActivated();
                            BlockInterface block = VideoDataManager.getBlockConfig().getBlock(lastActivated);
                            string text10 = "CARTOONSCR_REPLAY_PRESSED";
                            List<string> list10 = ["cartoon_id", block.getId().ToString()];
                            FlurryAPI.logEvent(text10, list10);
                            if (lastActivated == -1)
                            {
                                onButtonPressed(4000);
                                return;
                            }
                            onButtonPressed(4000 + lastActivated);
                            return;
                        }
                    case 50:
                        {
                            BlockInterface block2 = VideoDataManager.getBlockConfig().getBlock(VideoDataManager.getLastActivated());
                            string text11 = "CARTOONSCR_SHARE_PRESSED";
                            List<string> list11 = ["cartoon_id", block2.getId().ToString()];
                            FlurryAPI.logEvent(text11, list11);
                            AndroidAPI.share(Application.getString(1310773), NSS("Cut the Rope Cartoons: Episode " + block2.getNumber()), block2.getUrl(), false);
                            return;
                        }
                    case 51:
                        {
                            int lastActivated2 = VideoDataManager.getLastActivated();
                            BlockConfig blockConfig2 = VideoDataManager.getBlockConfig();
                            BlockInterface block3 = blockConfig2.getBlock(lastActivated2);
                            int nextSameType = blockConfig2.getNextSameType(block3);
                            if (nextSameType != -1)
                            {
                                BlockInterface block4 = blockConfig2.getBlock(nextSameType);
                                string text12 = "CARTOONSCR_NEXT_PRESSED";
                                List<string> list12 =
                                [
                                    "cartoon_id",
                                    block3.getId().ToString(),
                                    "next_cartoon_id",
                                    block4.getId().ToString(),
                                ];
                                FlurryAPI.logEvent(text12, list12);
                                onButtonPressed(4000 + nextSameType);
                                return;
                            }
                            return;
                        }
                    case 52:
                        {
                            BlockInterface block5 = VideoDataManager.getBlockConfig().getBlock(VideoDataManager.getLastActivated());
                            string text13 = "CARTOONSCR_BACKBT_PRESSED";
                            List<string> list13 = ["cartoon_id", block5.getId().ToString()];
                            FlurryAPI.logEvent(text13, list13);
                            showView(ViewID.VIEW_CARTOONS_SELECT);
                            return;
                        }
                    case 53:
                        FlurryAPI.logEvent("BOXSEL_CARTOONS_PRESSED", null);
                        return;
                    case 54:
                        AndroidAPI.openUrl(Application.getString(1310823));
                        return;
                    default:
                        return;
                }
                ep.hidePopup();
                ep = null;
                return;
            }
        }

        // Token: 0x060006CA RID: 1738 RVA: 0x0003767C File Offset: 0x0003587C
        private void gotoNextBox()
        {
            _ = Application.sharedPreferences();
            for (int i = 0; i < packSelect.size; i++)
            {
                int num = packSelect.content[i];
                if (BoxFabric.isGameBox(num))
                {
                    int saveIndex = BoxFabric.getSaveIndex(num);
                    if (CTRPreferences.getUnlockedForPackLevel(saveIndex, 0) == UNLOCKED_STATE.UNLOCKED_STATE_JUST_UNLOCKED_WITH_CHEAT)
                    {
                        int firstContainerForPack = getFirstContainerForPack(saveIndex);
                        if (firstContainerForPack != -1)
                        {
                            packContainer.moveToScrollPointmoveMultiplier(firstContainerForPack, 0.8);
                            ddPackSelect.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_gotoNextBox), null, 0.7);
                        }
                        return;
                    }
                }
            }
            packContainer.moveToScrollPointmoveMultiplier(animationStartPackIndex, 0.3);
            unlockAnimation = false;
        }

        // Token: 0x060006CB RID: 1739 RVA: 0x00037730 File Offset: 0x00035930
        private void unlockBoxes()
        {
            needUnlock = false;
            _ = Application.sharedPreferences();
            unlockAnimation = true;
            animationStartPackIndex = currentPackIndex;
            int i = 0;
            int packsCount = CTRPreferences.getPacksCount();
            while (i < packsCount)
            {
                if (CTRPreferences.getUnlockedForPackLevel(i, 0) == UNLOCKED_STATE.UNLOCKED_STATE_LOCKED)
                {
                    CTRPreferences.setUnlockedForPackLevel(UNLOCKED_STATE.UNLOCKED_STATE_JUST_UNLOCKED_WITH_CHEAT, i, 0);
                }
                i++;
            }
            deleteView(ViewID.VIEW_PACK_SELECT);
            createPackSelect();
            ddPackSelect.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_gotoNextBox), null, 0.5);
        }

        // Token: 0x060006CC RID: 1740 RVA: 0x000377B0 File Offset: 0x000359B0
        public override void update(float delta)
        {
            base.update(delta);
            if (App.NeedsUpdate)
            {
                UpdatePopup.showUpdatePopup();
            }
            if (activeViewID == 2 && aboutAutoScroll)
            {
                Vector scroll = aboutContainer.getScroll();
                Vector maxScroll = aboutContainer.getMaxScroll();
                scroll.y += 0.5f;
                scroll.y = FIT_TO_BOUNDARIES(scroll.y, 0.0, maxScroll.y);
                aboutContainer.setScroll(scroll);
            }
            else if (activeViewID == 4 && ddPackSelect != null)
            {
                ddPackSelect.update(delta);
            }
            else if (activeViewID == 0 && ddMainMenu != null)
            {
                ddMainMenu.update(delta);
            }
            if (needRecreate)
            {
                needRecreate = false;
                if (activeViewID == 4)
                {
                    Application.sharedRootController().setViewTransition(4);
                    Application.sharedRootController().onControllerViewHide(getView(ViewID.VIEW_PACK_SELECT));
                }
                deleteView(ViewID.VIEW_PACK_SELECT);
                createPackSelect();
                if (activeViewID == 4)
                {
                    Application.sharedRootController().onControllerViewShow(getView(ViewID.VIEW_PACK_SELECT));
                    packContainer.moveToScrollPointmoveMultiplier(boxFabric.isZeroBoxDefined() ? 1 : 0, 0.8);
                }
            }
            if (needUnlock)
            {
                unlockBoxes();
            }
            if (statusBackupRestore > 0)
            {
                ep?.hidePopup();
                ep = null;
                switch (statusBackupRestore)
                {
                    case 1:
                        showStatusPopup(activeView(), Application.getString(1310780));
                        break;
                    case 2:
                        showStatusPopup(activeView(), Application.getString(1310779));
                        break;
                }
                statusBackupRestore = 0;
            }
        }

        // Token: 0x060006CD RID: 1741 RVA: 0x00037976 File Offset: 0x00035B76
        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            if (unlockAnimation)
            {
                return true;
            }
            _ = base.touchesBeganwithEvent(touches);
            if (activeViewID == 2 && aboutAutoScroll)
            {
                aboutAutoScroll = false;
            }
            return true;
        }

        // Token: 0x060006CE RID: 1742 RVA: 0x000379A4 File Offset: 0x00035BA4
        public override bool backButtonPressed()
        {
            if (FLAG_RESTORING || unlockAnimation)
            {
                return true;
            }
            if (ep != null)
            {
                ep.hidePopup();
                ep = null;
                return true;
            }
            int activeViewID = this.activeViewID;
            if (activeViewID == 0)
            {
                PromoBanner promoBanner = (PromoBanner)activeView().getChildWithName("promoBanner");
                if (promoBanner != null && !promoBanner.promoMainHidden)
                {
                    promoBanner.reset();
                    promoBanner.closeMainPromo();
                    return true;
                }
                promoBanner?.reset();
                showYesNoPopup(activeView(), Application.getString(1310736), 18, 21);
            }
            if (activeViewID == 1)
            {
                onButtonPressed(9);
            }
            else if (activeViewID is 2 or 3)
            {
                onButtonPressed(10);
            }
            else if (activeViewID is 4 or 11)
            {
                onButtonPressed(48);
            }
            else if (activeViewID == 5)
            {
                onButtonPressed(11);
            }
            else if (activeViewID == 8)
            {
                onButtonPressed(10);
            }
            else if (activeViewID == 9)
            {
                onButtonPressed(10);
            }
            else if (activeViewID == 10)
            {
                onButtonPressed(9);
            }
            else if (activeViewID == 12)
            {
                onButtonPressed(52);
            }
            return true;
        }

        // Token: 0x060006CF RID: 1743 RVA: 0x00037AB3 File Offset: 0x00035CB3
        private void updateNewDrawingsCounter()
        {
        }

        // Token: 0x060006D0 RID: 1744 RVA: 0x00037AB8 File Offset: 0x00035CB8
        private void playHandAnimation()
        {
            bool flag = Preferences._getBooleanForKey("PREFS_CANDY_WAS_CHANGED");
            if (flag)
            {
                return;
            }
            handAnimation.setEnabled(true);
            glowAnimation.setEnabled(true);
            handAnimation.playTimeline(0);
            glowAnimation.playTimeline(0);
            ddMainMenu.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_playHandAnimation), null, 14f);
        }

        // Token: 0x060006D1 RID: 1745 RVA: 0x00037B21 File Offset: 0x00035D21
        private void restoreSuccess()
        {
            FLAG_RESTORING = false;
            needRecreate = true;
            statusBackupRestore = 2;
        }

        // Token: 0x060006D2 RID: 1746 RVA: 0x00037B38 File Offset: 0x00035D38
        private void setProblem(int problemID)
        {
            FLAG_RESTORING = false;
            statusBackupRestore = problemID;
        }

        // Token: 0x060006D3 RID: 1747 RVA: 0x00037B48 File Offset: 0x00035D48
        private int getFirstContainerForPack(int ppack)
        {
            int num = (ppack == -1) ? pack : ppack;
            for (int i = 0; i < packSelect.size; i++)
            {
                if (BoxFabric.getSaveIndex(packSelect.content[i]) == num)
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x060006D4 RID: 1748 RVA: 0x00037B94 File Offset: 0x00035D94
        private void showStatusPopup(BaseElement parent, NSString statusText)
        {
            FontGeneric font = Application.getFont(5);
            float num = 250f;
            Text text = new Text().initWithFont(font);
            text.setAlignment(2);
            text.setStringandWidth(statusText, num);
            Button button = createButtonWithTextIDDelegate(Application.getString(1310754), 20, this);
            showPopup(parent, text, button);
        }

        // Token: 0x060006D5 RID: 1749 RVA: 0x00037BE8 File Offset: 0x00035DE8
        private void showPopup(BaseElement parent, BaseElement message, BaseElement buttons)
        {
            Popup popup = (Popup)new Popup().init();
            popup.setName(NSS("popup"));
            Image image = Image.Image_createWithResIDQuad(68, 0);
            image.anchor = image.parentAnchor = 18;
            _ = popup.addChild(image);
            message.anchor = message.parentAnchor = 18;
            _ = image.addChild(message);
            buttons.y += -14f;
            buttons.anchor = 18;
            buttons.parentAnchor = 34;
            _ = image.addChild(buttons);
            popup.showPopup();
            ep = popup;
            _ = parent.addChild(popup);
        }

        // Token: 0x060006D6 RID: 1750 RVA: 0x00037C90 File Offset: 0x00035E90
        public virtual void showYesNoPopup(BaseElement parent, NSString statusText, int buttonYes, int buttonNo)
        {
            Button button = createButtonWithTextIDDelegate(Application.getString(1310748), buttonNo, this);
            button.anchor = button.parentAnchor = 18;
            button.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            Button button2 = createButtonWithTextIDDelegate(Application.getString(1310747), buttonYes, this);
            button2.anchor = 33;
            button2.parentAnchor = 9;
            button2.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            _ = button.addChild(button2);
            VBox vbox = new VBox().initWithOffsetAlignWidth(0.0, 2, SCREEN_WIDTH);
            _ = vbox.addChild(button2);
            _ = vbox.addChild(button);
            vbox.y = -34f;
            FontGeneric font = Application.getFont(5);
            float num = 250f;
            Text text = new Text().initWithFont(font);
            text.setAlignment(2);
            text.setStringandWidth(statusText, num);
            text.y = -34f;
            showPopup(parent, text, vbox);
        }

        // Token: 0x060006D7 RID: 1751 RVA: 0x00037D9C File Offset: 0x00035F9C
        public virtual void showBuyFullPopup()
        {
            Button button = createButtonWithTextIDDelegate(Application.getString(1310833), 21, this);
            button.anchor = button.parentAnchor = 18;
            button.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            Button button2 = createButtonWithTextIDDelegateAutoScale(Application.getString(1310723), 4, this);
            button2.anchor = 33;
            button2.parentAnchor = 9;
            button2.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            _ = button.addChild(button2);
            FontGeneric font = Application.getFont(5);
            float num = 250f;
            Text text = new Text().initWithFont(font);
            text.setAlignment(2);
            text.setStringandWidth(Application.getString(1310832), num);
            text.y = -34f;
            showPopup(activeView(), text, button);
        }

        // Token: 0x060006D8 RID: 1752 RVA: 0x00037E7C File Offset: 0x0003607C
        public virtual void showUnlockShareware()
        {
            showYesNoPopup(activeView(), Application.getString(1310802), 29, 21);
        }

        // Token: 0x060006D9 RID: 1753 RVA: 0x00037E98 File Offset: 0x00036098
        public virtual void showCantUnlockPopupForPack(BaseElement parent, int pack)
        {
            float num = 280f;
            VBox vbox = new VBox().initWithOffsetAlignWidth(-10.0, 2, SCREEN_WIDTH);
            vbox.anchor = 18;
            Text text = new Text().initWithFont(Application.getFont(5));
            text.setAlignment(2);
            text.setString(Application.getString(1310756));
            _ = vbox.addChild(text);
            int totalStarsInDelivery = CTRPreferences.getTotalStarsInDelivery(-1);
            BaseElement baseElement = createTextWithStar((CTRPreferences.packUnlockStars(pack) - totalStarsInDelivery).ToString());
            _ = vbox.addChild(baseElement);
            Text text2 = new Text().initWithFont(Application.getFont(5));
            text2.setAlignment(2);
            text2.setStringandWidth(Application.getString(1310757), num);
            _ = vbox.addChild(text2);
            Text text3 = new Text().initWithFont(Application.getFont(6));
            text3.setAlignment(2);
            text3.setStringandWidth(Application.getString(1310758), num);
            _ = vbox.addChild(text3);
            Button button = createButtonWithTextIDDelegate(Application.getString(1310754), 22, this);
            showPopup(parent, vbox, button);
        }

        // Token: 0x060006DA RID: 1754 RVA: 0x00037FB0 File Offset: 0x000361B0
        private void showGameFinishedPopup(BaseElement parent)
        {
            float num = 250f;
            VBox vbox = new VBox().initWithOffsetAlignWidth(40.0, 2, SCREEN_WIDTH);
            vbox.anchor = 18;
            Text text = new Text().initWithFont(Application.getFont(5));
            text.setAlignment(2);
            text.setStringandWidth(Application.getString(1310759), num);
            _ = vbox.addChild(text);
            Text text2 = new Text().initWithFont(Application.getFont(6));
            text2.setAlignment(2);
            text2.setStringandWidth(Application.getString(1310760), num);
            _ = vbox.addChild(text2);
            Button button = createButtonWithTextIDDelegate(Application.getString(1310754), 21, this);
            showPopup(parent, vbox, button);
        }

        // Token: 0x060006DB RID: 1755 RVA: 0x00038068 File Offset: 0x00036268
        public virtual void changedActiveSpointFromTo(int pp, int cp)
        {
            _ = liftScrollbar.getTotalScrollPoints();
            TouchImage touchImage = (TouchImage)bulletContainer.getChild(pp);
            TouchImage touchImage2 = (TouchImage)bulletContainer.getChild(cp);
            touchImage.setDrawQuad(8);
            touchImage2.setDrawQuad(9);
        }

        // Token: 0x060006DC RID: 1756 RVA: 0x000380B4 File Offset: 0x000362B4
        private void showView(ViewID n)
        {
            if (n is ViewID.VIEW_OPTIONS or ViewID.VIEW_RESET or ViewID.VIEW_PACK_SELECT)
            {
                AndroidAPI.showBanner();
            }
            else
            {
                AndroidAPI.hideBanner();
            }
            if (n == ViewID.VIEW_DELIVERY_SELECT)
            {
                FlurryAPI.logEvent("SEASONSEL_SCREEN_SHOWN", null);
                VideoDataManager.init();
                DeliverySelectView deliverySelectView = (DeliverySelectView)getView(ViewID.VIEW_DELIVERY_SELECT);
                deliverySelectView.checkCartoonsWatched();
            }
            else if (n == ViewID.VIEW_CARTOONS_SELECT)
            {
                CartoonsSelectView cartoonsSelectView = (CartoonsSelectView)getView(ViewID.VIEW_CARTOONS_SELECT);
                cartoonsSelectView.closeCurtain();
                FlurryAPI.logEvent("CARTOONSEL_SCREEN_SHOWN", null);
            }
            else if (n == ViewID.VIEW_CARTOONS_AFTERWATCH)
            {
                FlurryAPI.logEvent("CARTOONSCR_SCREEN_SHOWN", null);
            }
            else if (n == ViewID.VIEW_PACK_SELECT)
            {
                string text = "BOXSEL_SCREEN_SHOWN";
                List<string> list = ["box_id", currentPackIndex.ToString()];
                FlurryAPI.logEvent(text, list);
                int num = Math.Min(packSelect.size - 1, CTRPreferences.getLastPack());
                packContainer.placeToScrollPoint((num != -1) ? num : packSelect.getFirstGameBox());
            }
            else if (n == ViewID.VIEW_LEVEL_SELECT)
            {
                string text2 = "LEVSEL_SCREEN_SHOWN";
                List<string> list2 = ["box_id", currentPackIndex.ToString()];
                FlurryAPI.logEvent(text2, list2);
            }
            else if (n == ViewID.VIEW_MAIN_MENU)
            {
                if (!Preferences.firstStart)
                {
                    FirstTime = false;
                }

                _ = Assembly.GetExecutingAssembly().FullName.Split(['='])[1].Split([','])[0];
                string text4 = "MMENU_SCREEN_SHOWN";
                List<string> list3 = ["first_time", FirstTime.ToString()];
                FlurryAPI.logEventwithParams(text4, list3, true, true, false);
                FirstTime = false;
            }
            base.showView((int)n);
        }

        // Token: 0x060006DD RID: 1757 RVA: 0x00038273 File Offset: 0x00036473
        private View getView(ViewID n)
        {
            return base.getView((int)n);
        }

        // Token: 0x060006DE RID: 1758 RVA: 0x0003827C File Offset: 0x0003647C
        private void deleteView(ViewID n)
        {
            base.deleteView((int)n);
        }

        // Token: 0x060006DF RID: 1759 RVA: 0x00038285 File Offset: 0x00036485
        private void addViewwithID(View view, ViewID n)
        {
            base.addViewwithID(view, (int)n);
        }

        // Token: 0x060006E0 RID: 1760 RVA: 0x0003828F File Offset: 0x0003648F
        private void activateChild(CHILD_TYPE c)
        {
            base.activateChild((int)c);
        }

        // Token: 0x060006E1 RID: 1761 RVA: 0x00038298 File Offset: 0x00036498
        public void createAchievements()
        {
            AchievementsView achievementsView = (AchievementsView)new AchievementsView().init();
            addViewwithID(achievementsView, ViewID.VIEW_ACHIEVEMENTS);
            Button button = createBackButtonWithDelegateID(this, 36);
            button.setName("backb");
            button.x = 0f;
            _ = achievementsView.addChild(button);
        }

        // Token: 0x060006E2 RID: 1762 RVA: 0x000382E4 File Offset: 0x000364E4
        public virtual void createLeaderboards()
        {
            LeaderboardsView leaderboardsView = (LeaderboardsView)new LeaderboardsView().init();
            addViewwithID(leaderboardsView, ViewID.VIEW_LEADERBOARDS);
            Button button = createBackButtonWithDelegateID(this, 38);
            button.setName("backb");
            button.x = 0f;
            _ = leaderboardsView.addChildwithID(button, leaderboardsView.childsCount());
        }

        // Token: 0x060006E3 RID: 1763 RVA: 0x00038338 File Offset: 0x00036538
        public static Image createBlankScoresButtonWithIconpressed(int quad, bool pressed)
        {
            Image image = Image.Image_createWithResIDQuad(389, pressed ? 1 : 0);
            Image image2 = Image.Image_createWithResIDQuad(389, quad);
            _ = image.addChild(image2);
            image2.parentAnchor = 9;
            Image.setElementPositionWithRelativeQuadOffset(image2, 389, 0, quad);
            return image;
        }

        // Token: 0x060006E4 RID: 1764 RVA: 0x00038384 File Offset: 0x00036584
        public static Button createScoresButtonWithIconbuttonIDdelegate(int quad, int bId, ButtonDelegate delegateValue)
        {
            Image image = createBlankScoresButtonWithIconpressed(quad, false);
            Image image2 = createBlankScoresButtonWithIconpressed(quad, true);
            Image.setElementPositionWithRelativeQuadOffset(image2, 389, 0, 1);
            Button button = new Button().initWithUpElementDownElementandID(image, image2, bId);
            button.delegateButtonDelegate = delegateValue;
            return button;
        }

        // Token: 0x04000C11 RID: 3089
        public ScrollableContainer aboutContainer;

        // Token: 0x04000C12 RID: 3090
        public ScrollableContainer packContainer;

        // Token: 0x04000C13 RID: 3091
        public BaseElement[] boxes = new BaseElement[15];

        // Token: 0x04000C14 RID: 3092
        public StarsBreak breakParticles;

        // Token: 0x04000C15 RID: 3093
        public Button unlockb;

        // Token: 0x04000C16 RID: 3094
        public bool unlockbHidden;

        // Token: 0x04000C17 RID: 3095
        public Image handAnimation;

        // Token: 0x04000C18 RID: 3096
        public Image glowAnimation;

        // Token: 0x04000C19 RID: 3097
        public static Popup ep;

        // Token: 0x04000C1A RID: 3098
        public bool showNextPackStatus;

        // Token: 0x04000C1B RID: 3099
        public bool aboutAutoScroll;

        // Token: 0x04000C1C RID: 3100
        public bool replayingIntroMovie;

        // Token: 0x04000C1D RID: 3101
        public bool needRecreate;

        // Token: 0x04000C1E RID: 3102
        public bool needUnlock;

        // Token: 0x04000C1F RID: 3103
        public int statusBackupRestore;

        // Token: 0x04000C20 RID: 3104
        public int pack;

        // Token: 0x04000C21 RID: 3105
        public int level;

        // Token: 0x04000C22 RID: 3106
        public ViewID viewToShow;

        // Token: 0x04000C23 RID: 3107
        public int animationStartPackIndex;

        // Token: 0x04000C24 RID: 3108
        public int currentPackIndex;

        // Token: 0x04000C25 RID: 3109
        public bool unlockAnimation;

        // Token: 0x04000C26 RID: 3110
        public DelayedDispatcher ddMainMenu;

        // Token: 0x04000C27 RID: 3111
        public DelayedDispatcher ddPackSelect;

        // Token: 0x04000C28 RID: 3112
        public Button promob;

        // Token: 0x04000C29 RID: 3113
        public bool promobHidden;

        // Token: 0x04000C2A RID: 3114
        public BaseElement bulletContainer;

        // Token: 0x04000C2B RID: 3115
        public HLiftScrollbar liftScrollbar;

        // Token: 0x04000C2C RID: 3116
        protected PackSelectInfo packSelect;

        // Token: 0x04000C2D RID: 3117
        protected BoxFabric boxFabric;

        // Token: 0x04000C2E RID: 3118
        private bool FLAG_RESTORING;

        // Token: 0x04000C2F RID: 3119
        private static bool FirstTime = true;

        // Token: 0x020000E4 RID: 228
        private enum CHILD_TYPE
        {
            // Token: 0x04000C31 RID: 3121
            CHILD_PICKER
        }

        // Token: 0x020000E5 RID: 229
        public enum ViewID
        {
            // Token: 0x04000C33 RID: 3123
            VIEW_MAIN_MENU,
            // Token: 0x04000C34 RID: 3124
            VIEW_OPTIONS,
            // Token: 0x04000C35 RID: 3125
            VIEW_ABOUT,
            // Token: 0x04000C36 RID: 3126
            VIEW_RESET,
            // Token: 0x04000C37 RID: 3127
            VIEW_PACK_SELECT,
            // Token: 0x04000C38 RID: 3128
            VIEW_LEVEL_SELECT,
            // Token: 0x04000C39 RID: 3129
            VIEW_MOVIE,
            // Token: 0x04000C3A RID: 3130
            VIEW_TERMS,
            // Token: 0x04000C3B RID: 3131
            VIEW_ACHIEVEMENTS,
            // Token: 0x04000C3C RID: 3132
            VIEW_LEADERBOARDS,
            // Token: 0x04000C3D RID: 3133
            VIEW_DELIVERY_SELECT,
            // Token: 0x04000C3E RID: 3134
            VIEW_CARTOONS_SELECT,
            // Token: 0x04000C3F RID: 3135
            VIEW_CARTOONS_AFTERWATCH
        }

        // Token: 0x020000E6 RID: 230
        public enum ButtonID
        {
            // Token: 0x04000C41 RID: 3137
            BUTTON_PLAY,
            // Token: 0x04000C42 RID: 3138
            BUTTON_OPTIONS,
            // Token: 0x04000C43 RID: 3139
            BUTTON_EXTRAS,
            // Token: 0x04000C44 RID: 3140
            BUTTON_CRYSTAL,
            // Token: 0x04000C45 RID: 3141
            BUTTON_BUYGAME,
            // Token: 0x04000C46 RID: 3142
            BUTTON_SOUND_ONOFF,
            // Token: 0x04000C47 RID: 3143
            BUTTON_MUSIC_ONOFF,
            // Token: 0x04000C48 RID: 3144
            BUTTON_ABOUT,
            // Token: 0x04000C49 RID: 3145
            BUTTON_RESET,
            // Token: 0x04000C4A RID: 3146
            BUTTON_BACK_TO_MAIN_MENU,
            // Token: 0x04000C4B RID: 3147
            BUTTON_BACK_TO_OPTIONS,
            // Token: 0x04000C4C RID: 3148
            BUTTON_BACK_TO_PACK_SELECT,
            // Token: 0x04000C4D RID: 3149
            BUTTON_RESET_YES,
            // Token: 0x04000C4E RID: 3150
            BUTTON_RESET_NO,
            // Token: 0x04000C4F RID: 3151
            BUTTON_PACK_SOON,
            // Token: 0x04000C50 RID: 3152
            BUTTON_GAMECENTER_ONOFF,
            // Token: 0x04000C51 RID: 3153
            BUTTON_TWITTER,
            // Token: 0x04000C52 RID: 3154
            BUTTON_FACEBOOK,
            // Token: 0x04000C53 RID: 3155
            BUTTON_EXIT_YES,
            // Token: 0x04000C54 RID: 3156
            BUTTON_RESTORE,
            // Token: 0x04000C55 RID: 3157
            BUTTON_POPUP_OK,
            // Token: 0x04000C56 RID: 3158
            BUTTON_POPUP_HIDE,
            // Token: 0x04000C57 RID: 3159
            BUTTON_POPUP_HIDE_FROM_CANTUNLOCK,
            // Token: 0x04000C58 RID: 3160
            BUTTON_CANDY,
            // Token: 0x04000C59 RID: 3161
            BUTTON_DRAWINGS,
            // Token: 0x04000C5A RID: 3162
            BUTTON_UNLOCK,
            // Token: 0x04000C5B RID: 3163
            BUTTON_DISABLEBANNERS,
            // Token: 0x04000C5C RID: 3164
            BUTTON_PROMO,
            // Token: 0x04000C5D RID: 3165
            BUTTON_TOYS,
            // Token: 0x04000C5E RID: 3166
            BUTTON_UNLOCK_SHAREWARE,
            // Token: 0x04000C5F RID: 3167
            BUTTON_LEVEL_PROMO_1,
            // Token: 0x04000C60 RID: 3168
            BUTTON_LEVEL_PROMO_2,
            // Token: 0x04000C61 RID: 3169
            BUTTON_BACK_TO_ABOUT,
            // Token: 0x04000C62 RID: 3170
            BUTTON_VIEW_TERMS,
            // Token: 0x04000C63 RID: 3171
            BUTTON_TERMS,
            // Token: 0x04000C64 RID: 3172
            BUTTON_PRIVACY,
            // Token: 0x04000C65 RID: 3173
            BUTTON_ACHIEVEMENTS_BACK,
            // Token: 0x04000C66 RID: 3174
            BUTTON_ACHIEVEMENTS,
            // Token: 0x04000C67 RID: 3175
            BUTTON_LEADERBOARDS_BACK,
            // Token: 0x04000C68 RID: 3176
            BUTTON_LEADERBOARDS,
            // Token: 0x04000C69 RID: 3177
            BUTTON_BUY_FULL_FROM_LEVEL_SELECT,
            // Token: 0x04000C6A RID: 3178
            BUTTON_BUY_FULL_FROM_DELIVERY_SELECT,
            // Token: 0x04000C6B RID: 3179
            BUTTON_BUYGAME_FROM_MAINMENU,
            // Token: 0x04000C6C RID: 3180
            BUTTON_DELIVERY_1,
            // Token: 0x04000C6D RID: 3181
            BUTTON_DELIVERY_2,
            // Token: 0x04000C6E RID: 3182
            BUTTON_DELIVERY_3,
            // Token: 0x04000C6F RID: 3183
            BUTTON_DELIVERY_CARTOONS,
            // Token: 0x04000C70 RID: 3184
            BUTTON_DELIVERY_NEXT,
            // Token: 0x04000C71 RID: 3185
            BUTTON_BACK_TO_DELIVERY_SELECT,
            // Token: 0x04000C72 RID: 3186
            BUTTON_CARTOON_REPLAY,
            // Token: 0x04000C73 RID: 3187
            BUTTON_CARTOON_SHARE,
            // Token: 0x04000C74 RID: 3188
            BUTTON_CARTOON_NEXT,
            // Token: 0x04000C75 RID: 3189
            BUTTON_BACK_TO_CARTOON_SELECT,
            // Token: 0x04000C76 RID: 3190
            BUTTON_VIDEO_BOX,
            // Token: 0x04000C77 RID: 3191
            BUTTON_PRIVACY_P,
            // Token: 0x04000C78 RID: 3192
            BUTTON_LEVEL_1 = 1000,
            // Token: 0x04000C79 RID: 3193
            BUTTON_PACK_1 = 2000,
            // Token: 0x04000C7A RID: 3194
            BUTTON_BULLET_1 = 3000,
            // Token: 0x04000C7B RID: 3195
            BUTTON_BLOCK_1 = 4000
        }

        // Token: 0x020000E7 RID: 231
        private enum Status
        {
            // Token: 0x04000C7D RID: 3197
            STATUS,
            // Token: 0x04000C7E RID: 3198
            STATUS_RESTORE_BROKEN,
            // Token: 0x04000C7F RID: 3199
            STATUS_RESTORE_OK
        }

        // Token: 0x020000E8 RID: 232
        public class TouchBaseElement : BaseElement
        {
            // Token: 0x060006E7 RID: 1767 RVA: 0x000383E4 File Offset: 0x000365E4
            public override bool onTouchDownXY(float tx, float ty)
            {
                _ = base.onTouchDownXY(tx, ty);
                Rectangle rectangle = MakeRectangle(drawX + bbc.x, drawY + bbc.y, width + bbc.w, height + bbc.h);
                Rectangle rectangle2 = rectInRectIntersection(MakeRectangle(0.0, 0.0, SCREEN_WIDTH, SCREEN_HEIGHT), rectangle);
                if (pointInRect(tx, ty, rectangle.x, rectangle.y, rectangle.w, rectangle.h) && rectangle2.w > rectangle.w / 2.0)
                {
                    delegateButtonDelegate?.onButtonPressed(bid);
                    return true;
                }
                return false;
            }

            // Token: 0x04000C80 RID: 3200
            public int bid;

            // Token: 0x04000C81 RID: 3201
            public Rectangle bbc;

            // Token: 0x04000C82 RID: 3202
            public ButtonDelegate delegateButtonDelegate;
        }
    }
}
