using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000090 RID: 144
    internal class CartoonsAfterwatchView : MenuView
    {
        // Token: 0x0600044B RID: 1099 RVA: 0x0001E090 File Offset: 0x0001C290
        public virtual NSObject initFullscreenBackgroundDelegate(BaseElement background, ButtonDelegate d)
        {
            if (base.initFullscreen() != null)
            {
                texID = 402;
                shineID = 0;
                BaseElement baseElement = Image.createElementWithLeftPart(texID, shineID);
                baseElement.anchor = (baseElement.parentAnchor = 10);
                baseElement.y = Image.getQuadOffset(texID, shineID).y;
                baseElement.getChild(1).x += 1.33f;
                BaseElement baseElement2 = Image.createElementWithLeftPart(texID, 1);
                baseElement2.anchor = (baseElement2.parentAnchor = 10);
                baseElement2.y = Image.getRelativeQuadOffset(texID, shineID, 1).y;
                baseElement.addChild(baseElement2);
                Button button = MenuController.buttonWithTextImageQuadHalfRescaledRecoloredIDDelegate(Application.getString(1310773), texID, 2, true, 0.95f, RGBAColor.MakeRGBA(0.85f, 0.85f, 0.85f, 1f), 50, d);
                button.anchor = (button.parentAnchor = 10);
                button.y = Image.getRelativeQuadOffset(texID, shineID, 2).y;
                baseElement.addChild(button);
                next = MenuController.createButton2WithImageQuad1Quad2IDDelegate(texID, 3, 4, 51, d);
                next.anchor = (next.parentAnchor = 9);
                Image.setElementPositionWithRelativeQuadOffset(next, texID, shineID, 3);
                baseElement.addChild(next);
                replay = MenuController.createButton2WithImageQuad1Quad2IDDelegate(texID, 5, 6, 49, d);
                replay.anchor = (replay.parentAnchor = 9);
                baseElement.addChild(replay);
                Image image = Image.Image_createWithResIDQuad(texID, 7);
                image.anchor = (image.parentAnchor = 9);
                Image.setElementPositionWithRelativeQuadOffset(image, texID, shineID, 7);
                baseElement.addChild(image);
                titletext = new Text().initWithFont(Application.getFont(5));
                titletext.anchor = 18;
                titletext.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(titletext, texID, shineID, 9);
                baseElement.addChild(titletext);
                replaytext = Text.createWithFontandString(6, Application.getString(1310749));
                replaytext.anchor = 18;
                replaytext.parentAnchor = 9;
                baseElement.addChild(replaytext);
                nexttext = Text.createWithFontandString(6, Application.getString(1310750));
                nexttext.anchor = 18;
                nexttext.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(nexttext, texID, shineID, 11);
                baseElement.addChild(nexttext);
                background.addChild(baseElement);
                Button button2 = MenuController.createBackButtonWithDelegateID(d, 52);
                background.addChild(button2);
                addChild(background);
                renumberEpisode(NSObject.NSS(""));
                setLast(false);
            }
            return this;
        }

        // Token: 0x0600044C RID: 1100 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
        public virtual void renumberEpisode(NSString newnumber)
        {
            titletext.setString(Application.getString(1310836).ToString().Replace("%@", newnumber.ToString()));
        }

        // Token: 0x0600044D RID: 1101 RVA: 0x0001E3EC File Offset: 0x0001C5EC
        public virtual void setLast(bool last)
        {
            isLast = last;
            if (last)
            {
                next.setEnabled(false);
                nexttext.setEnabled(false);
                Image.setElementPositionWithRelativeQuadOffset(replaytext, texID, shineID, 12);
                Image.setElementPositionWithRelativeQuadOffset(replay, texID, shineID, 13);
                replay.x -= (float)replay.width / 2f;
                replay.y -= (float)replay.height / 2f;
                return;
            }
            if (!last)
            {
                nexttext.setEnabled(true);
                next.setEnabled(true);
                Image.setElementPositionWithRelativeQuadOffset(replaytext, texID, shineID, 10);
                Image.setElementPositionWithRelativeQuadOffset(replay, texID, shineID, 5);
            }
        }

        // Token: 0x04000990 RID: 2448
        protected int texID;

        // Token: 0x04000991 RID: 2449
        protected int shineID;

        // Token: 0x04000992 RID: 2450
        protected bool isLast;

        // Token: 0x04000993 RID: 2451
        protected Text titletext;

        // Token: 0x04000994 RID: 2452
        protected Text replaytext;

        // Token: 0x04000995 RID: 2453
        protected Button replay;

        // Token: 0x04000996 RID: 2454
        protected Text nexttext;

        // Token: 0x04000997 RID: 2455
        protected Button next;
    }
}
