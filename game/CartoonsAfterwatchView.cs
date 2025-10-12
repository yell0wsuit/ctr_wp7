using System;
using ctre_wp7.ctr_original;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x02000090 RID: 144
	internal class CartoonsAfterwatchView : MenuView
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001E090 File Offset: 0x0001C290
		public virtual NSObject initFullscreenBackgroundDelegate(BaseElement background, ButtonDelegate d)
		{
			if (base.initFullscreen() != null)
			{
				this.texID = 402;
				this.shineID = 0;
				BaseElement baseElement = Image.createElementWithLeftPart(this.texID, this.shineID);
				baseElement.anchor = (baseElement.parentAnchor = 10);
				baseElement.y = Image.getQuadOffset(this.texID, this.shineID).y;
				baseElement.getChild(1).x += 1.33f;
				BaseElement baseElement2 = Image.createElementWithLeftPart(this.texID, 1);
				baseElement2.anchor = (baseElement2.parentAnchor = 10);
				baseElement2.y = Image.getRelativeQuadOffset(this.texID, this.shineID, 1).y;
				baseElement.addChild(baseElement2);
				Button button = MenuController.buttonWithTextImageQuadHalfRescaledRecoloredIDDelegate(Application.getString(1310773), this.texID, 2, true, 0.95f, RGBAColor.MakeRGBA(0.85f, 0.85f, 0.85f, 1f), 50, d);
				button.anchor = (button.parentAnchor = 10);
				button.y = Image.getRelativeQuadOffset(this.texID, this.shineID, 2).y;
				baseElement.addChild(button);
				this.next = MenuController.createButton2WithImageQuad1Quad2IDDelegate(this.texID, 3, 4, 51, d);
				this.next.anchor = (this.next.parentAnchor = 9);
				Image.setElementPositionWithRelativeQuadOffset(this.next, this.texID, this.shineID, 3);
				baseElement.addChild(this.next);
				this.replay = MenuController.createButton2WithImageQuad1Quad2IDDelegate(this.texID, 5, 6, 49, d);
				this.replay.anchor = (this.replay.parentAnchor = 9);
				baseElement.addChild(this.replay);
				Image image = Image.Image_createWithResIDQuad(this.texID, 7);
				image.anchor = (image.parentAnchor = 9);
				Image.setElementPositionWithRelativeQuadOffset(image, this.texID, this.shineID, 7);
				baseElement.addChild(image);
				this.titletext = new Text().initWithFont(Application.getFont(5));
				this.titletext.anchor = 18;
				this.titletext.parentAnchor = 9;
				Image.setElementPositionWithRelativeQuadOffset(this.titletext, this.texID, this.shineID, 9);
				baseElement.addChild(this.titletext);
				this.replaytext = Text.createWithFontandString(6, Application.getString(1310749));
				this.replaytext.anchor = 18;
				this.replaytext.parentAnchor = 9;
				baseElement.addChild(this.replaytext);
				this.nexttext = Text.createWithFontandString(6, Application.getString(1310750));
				this.nexttext.anchor = 18;
				this.nexttext.parentAnchor = 9;
				Image.setElementPositionWithRelativeQuadOffset(this.nexttext, this.texID, this.shineID, 11);
				baseElement.addChild(this.nexttext);
				background.addChild(baseElement);
				Button button2 = MenuController.createBackButtonWithDelegateID(d, 52);
				background.addChild(button2);
				this.addChild(background);
				this.renumberEpisode(NSObject.NSS(""));
				this.setLast(false);
			}
			return this;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		public virtual void renumberEpisode(NSString newnumber)
		{
			this.titletext.setString(Application.getString(1310836).ToString().Replace("%@", newnumber.ToString()));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public virtual void setLast(bool last)
		{
			this.isLast = last;
			if (last)
			{
				this.next.setEnabled(false);
				this.nexttext.setEnabled(false);
				Image.setElementPositionWithRelativeQuadOffset(this.replaytext, this.texID, this.shineID, 12);
				Image.setElementPositionWithRelativeQuadOffset(this.replay, this.texID, this.shineID, 13);
				this.replay.x -= (float)this.replay.width / 2f;
				this.replay.y -= (float)this.replay.height / 2f;
				return;
			}
			if (!last)
			{
				this.nexttext.setEnabled(true);
				this.next.setEnabled(true);
				Image.setElementPositionWithRelativeQuadOffset(this.replaytext, this.texID, this.shineID, 10);
				Image.setElementPositionWithRelativeQuadOffset(this.replay, this.texID, this.shineID, 5);
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
