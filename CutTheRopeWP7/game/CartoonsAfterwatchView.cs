using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class CartoonsAfterwatchView : MenuView
    {
        public NSObject initFullscreenBackgroundDelegate(BaseElement background, ButtonDelegate d)
        {
            if (initFullscreen() != null)
            {
                texID = 402;
                shineID = 0;
                BaseElement baseElement = Image.createElementWithLeftPart(texID, shineID);
                baseElement.anchor = baseElement.parentAnchor = 10;
                baseElement.y = Image.getQuadOffset(texID, shineID).y;
                baseElement.getChild(1).x += 1.33f;
                BaseElement baseElement2 = Image.createElementWithLeftPart(texID, 1);
                baseElement2.anchor = baseElement2.parentAnchor = 10;
                baseElement2.y = Image.getRelativeQuadOffset(texID, shineID, 1).y;
                _ = baseElement.addChild(baseElement2);
                Button button = MenuController.buttonWithTextImageQuadHalfRescaledRecoloredIDDelegate(Application.getString(1310773), texID, 2, true, 0.95f, RGBAColor.MakeRGBA(0.85f, 0.85f, 0.85f, 1f), 50, d);
                button.anchor = button.parentAnchor = 10;
                button.y = Image.getRelativeQuadOffset(texID, shineID, 2).y;
                _ = baseElement.addChild(button);
                next = MenuController.createButton2WithImageQuad1Quad2IDDelegate(texID, 3, 4, 51, d);
                next.anchor = next.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(next, texID, shineID, 3);
                _ = baseElement.addChild(next);
                replay = MenuController.createButton2WithImageQuad1Quad2IDDelegate(texID, 5, 6, 49, d);
                replay.anchor = replay.parentAnchor = 9;
                _ = baseElement.addChild(replay);
                Image image = Image.Image_createWithResIDQuad(texID, 7);
                image.anchor = image.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(image, texID, shineID, 7);
                _ = baseElement.addChild(image);
                titletext = new Text().initWithFont(Application.getFont(5));
                titletext.anchor = 18;
                titletext.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(titletext, texID, shineID, 9);
                _ = baseElement.addChild(titletext);
                replaytext = Text.createWithFontandString(6, Application.getString(1310749));
                replaytext.anchor = 18;
                replaytext.parentAnchor = 9;
                _ = baseElement.addChild(replaytext);
                nexttext = Text.createWithFontandString(6, Application.getString(1310750));
                nexttext.anchor = 18;
                nexttext.parentAnchor = 9;
                Image.setElementPositionWithRelativeQuadOffset(nexttext, texID, shineID, 11);
                _ = baseElement.addChild(nexttext);
                _ = background.addChild(baseElement);
                Button button2 = MenuController.createBackButtonWithDelegateID(d, 52);
                _ = background.addChild(button2);
                _ = addChild(background);
                renumberEpisode(NSS(""));
                setLast(false);
            }
            return this;
        }

        public void renumberEpisode(NSString newnumber)
        {
            titletext.setString(Application.getString(1310836).ToString().Replace("%@", newnumber.ToString()));
        }

        public void setLast(bool last)
        {
            isLast = last;
            if (last)
            {
                next.setEnabled(false);
                nexttext.setEnabled(false);
                Image.setElementPositionWithRelativeQuadOffset(replaytext, texID, shineID, 12);
                Image.setElementPositionWithRelativeQuadOffset(replay, texID, shineID, 13);
                replay.x -= replay.width / 2f;
                replay.y -= replay.height / 2f;
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

        private int texID;

        private int shineID;

        private bool isLast;

        private Text titletext;

        private Text replaytext;

        private Button replay;

        private Text nexttext;

        private Button next;
    }
}
