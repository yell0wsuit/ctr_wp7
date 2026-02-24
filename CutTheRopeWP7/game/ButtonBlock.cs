using System;
using ctr_wp7.ctr_original;
using ctr_wp7.game.remotedata;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
	// Token: 0x02000059 RID: 89
	internal class ButtonBlock : Button
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		public virtual NSObject initWithIDDelegateBlock(int bid, ButtonDelegate d, BlockInterface pBlock)
		{
			this.delegateButtonDelegate = d;
			this.block = pBlock;
			this.recheckneeded = false;
			this.check = MathHelper.RND_0_1 * 3f + 1f;
			this.transition = 0.5f;
			NSString text = this.block.getText();
			NSString name = this.block.getName();
			Texture2D texture2D = null;
			if (name != null && this.block.isImageExists())
			{
				if (this.block.isImageReady())
				{
					texture2D = Application.sharedResourceMgr().loadTextureImageInfo(name.ToString());
				}
				else
				{
					this.recheckneeded = true;
				}
			}
			BaseElement baseElement = null;
			BaseElement baseElement2 = null;
			if (this.block.getType() == 0)
			{
				baseElement = this.buildAdBlock(texture2D, text);
				baseElement2 = this.buildAdBlock(texture2D, text);
			}
			else if (this.block.getType() == 1)
			{
				NSString number = this.block.getNumber();
				bool cartoonWatched = CTRPreferences.getCartoonWatched(this.block.getUrl());
				baseElement = this.buildEpisodeBlock(texture2D, text, number, this.recheckneeded, !cartoonWatched);
				baseElement2 = this.buildEpisodeBlock(texture2D, text, number, this.recheckneeded, !cartoonWatched);
			}
			baseElement2.color = RGBAColor.MakeRGBA(0.7f, 0.7f, 0.7f, 1f);
			this.initWithUpElementDownElementandID(baseElement, baseElement2, bid);
			return this;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00010E38 File Offset: 0x0000F038
		public static ButtonBlock createWithIDDelegateBlock(int bid, ButtonDelegate d, BlockInterface pBlock)
		{
			return (ButtonBlock)new ButtonBlock().initWithIDDelegateBlock(bid, d, pBlock);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00010E5C File Offset: 0x0000F05C
		protected virtual BaseElement buildAdBlock(Texture2D texture, NSString title)
		{
			int num = 403;
			int num2 = 10;
			int num3 = 9;
			int num4 = 6;
			int num5 = 7;
			BaseElement baseElement;
			if (texture == null)
			{
				baseElement = Image.createElementWithLeftPart(num, num4);
				Image image = Image.Image_createWithResIDQuad(num, num5);
				image.anchor = (image.parentAnchor = 9);
				Image.setElementPositionWithRelativeQuadOffset(image, num, num4, num5);
				Image image2 = Image.Image_createWithResIDQuad(num, num5);
				image2.anchor = (image2.parentAnchor = 12);
				image2.x = -image.x;
				image2.y = image.y;
				baseElement.addChild(image);
				baseElement.addChild(image2);
				BaseElement baseElement2 = (BaseElement)new BaseElement().init();
				baseElement2.anchor = (baseElement2.parentAnchor = 18);
				baseElement2.setName(NSObject.NSS("dummy"));
				baseElement2.width = baseElement.width;
				baseElement2.height = baseElement.height;
				baseElement.addChild(baseElement2);
			}
			else
			{
				baseElement = Image.Image_create(texture);
				Text text = new Text().initWithFont(Application.getFont(5));
				text.anchor = (text.parentAnchor = 18);
				text.setAlignment(2);
				text.setStringandWidth(title, (float)baseElement.width * 0.8f);
				baseElement.addChild(text);
			}
			MenuController.frameElement(baseElement, num, num2, num3);
			return baseElement;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00010FCC File Offset: 0x0000F1CC
		protected virtual BaseElement buildEpisodeBlock(Texture2D texture, NSString title, NSString number, bool processing, bool isNew)
		{
			int num = 403;
			int num2 = 2;
			int num3 = 1;
			int num4 = 0;
			int num5 = 3;
			BaseElement baseElement;
			if (texture == null)
			{
				baseElement = Image.Image_createWithResIDQuad(num, num4);
				BaseElement baseElement2 = (BaseElement)new BaseElement().init();
				baseElement2.anchor = (baseElement2.parentAnchor = 18);
				baseElement2.setName(NSObject.NSS("dummy"));
				baseElement2.width = baseElement.width;
				baseElement2.height = baseElement.height;
				baseElement.addChild(baseElement2);
				if (processing)
				{
					Image image = Image.Image_createWithResIDQuad(76, 0);
					Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
					timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline.addKeyFrame(KeyFrame.makeRotation(360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
					image.addTimeline(timeline);
					Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
					timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, this.transition));
					timeline2.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline2.addKeyFrame(KeyFrame.makeScale(0f, 0f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, this.transition));
					timeline2.addKeyFrame(KeyFrame.makeSingleAction(image, "ACTION_SET_VISIBLE", 0, 0, this.transition));
					image.addTimeline(timeline2);
					image.playTimeline(0);
					image.anchor = (image.parentAnchor = 18);
					image.setName(NSObject.NSS("progressbar"));
					baseElement.addChild(image);
				}
			}
			else
			{
				baseElement = Image.Image_create(texture);
				Text text = new Text().initWithFont(Application.getFont(5));
				text.anchor = (text.parentAnchor = 18);
				text.setAlignment(2);
				text.setStringandWidth(title, (float)baseElement.width * 0.9f);
				baseElement.addChild(text);
			}
			MenuController.frameElement(baseElement, num, num2, num3);
			BaseElement baseElement3 = baseElement.getChild(baseElement.childsCount() - 1);
			baseElement3 = baseElement3.getChild(baseElement3.childsCount() - 1);
			baseElement3.getChild(0).x -= 0.83f;
			baseElement3.getChild(1).x += 0.83f;
			if (number != null && number.length() != 0)
			{
				Image image2 = Image.Image_createWithResIDQuad(num, num5);
				image2.anchor = (image2.parentAnchor = 9);
				Image.setElementPositionWithRelativeQuadOffset(image2, num, num4, num5);
				baseElement.addChild(image2);
				if (isNew)
				{
					int num6 = 4;
					Image image3 = Image.Image_createWithResIDQuad(num, num6);
					image3.anchor = (image3.parentAnchor = 9);
					Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(11);
					timeline3.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline3.addKeyFrame(KeyFrame.makeRotation(-360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 8f));
					timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline3.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline3.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline3.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline3.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
					image3.addTimeline(timeline3);
					image3.playTimeline(0);
					Image.setElementPositionWithRelativeQuadOffset(image3, num, num5, num6);
					int num7 = 5;
					Image image4 = Image.Image_createWithResIDQuad(num, num7);
					image4.anchor = (image4.parentAnchor = 9);
					Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(11);
					timeline4.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline4.addKeyFrame(KeyFrame.makeRotation(360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 8f));
					timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
					timeline4.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline4.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline4.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline4.addKeyFrame(KeyFrame.makeScale(0.8, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
					timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
					timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
					image4.addTimeline(timeline4);
					image4.playTimeline(0);
					Image.setElementPositionWithRelativeQuadOffset(image4, num, num5, num7);
					BaseElement baseElement4 = (BaseElement)new BaseElement().init();
					baseElement4.width = image2.width;
					baseElement4.height = image2.height;
					baseElement4.anchor = (baseElement4.parentAnchor = 9);
					baseElement4.setName(NSObject.NSS("nimbus"));
					image2.addChild(baseElement4);
					baseElement4.addChild(image3);
					baseElement4.addChild(image4);
				}
				Text text2 = Text.createWithFontandString(5, number);
				text2.anchor = (text2.parentAnchor = 18);
				text2.x = -3f;
				text2.y = -2f;
				image2.addChild(text2);
			}
			return baseElement;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011694 File Offset: 0x0000F894
		public override void update(float delta)
		{
			base.update(delta);
			if (this.recheckneeded)
			{
				this.check -= delta;
				if (this.check <= 0f)
				{
					if (this.block.isImageReady())
					{
						this.recheckneeded = false;
						Texture2D texture2D = Application.sharedResourceMgr().loadTextureImageInfo(this.block.getName().ToString());
						BaseElement childWithName = this.getChild(0).getChildWithName(NSObject.NSS("dummy"));
						Image image = Image.Image_create(texture2D);
						Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(4);
						timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
						timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, this.transition));
						timeline.addKeyFrame(KeyFrame.makeScale(0f, 0f, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
						timeline.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, this.transition));
						image.addTimeline(timeline);
						image.playTimeline(0);
						image.anchor = (image.parentAnchor = 18);
						childWithName.addChild(image);
						BaseElement childWithName2 = this.getChild(0).getChildWithName(NSObject.NSS("progressbar"));
						if (childWithName2 != null)
						{
							childWithName2.playTimeline(1);
						}
						BaseElement childWithName3 = this.getChild(1).getChildWithName(NSObject.NSS("dummy"));
						Image image2 = Image.Image_create(texture2D);
						image2.anchor = (image2.parentAnchor = 18);
						childWithName3.addChild(image2);
						BaseElement childWithName4 = this.getChild(1).getChildWithName(NSObject.NSS("progressbar"));
						if (childWithName4 != null)
						{
							this.getChild(1).removeChild(childWithName4);
							return;
						}
					}
					else
					{
						this.check = MathHelper.RND_0_1 * 5f + 1f;
					}
				}
			}
		}

		// Token: 0x040008A5 RID: 2213
		protected BlockInterface block;

		// Token: 0x040008A6 RID: 2214
		protected bool recheckneeded;

		// Token: 0x040008A7 RID: 2215
		protected float check;

		// Token: 0x040008A8 RID: 2216
		protected float transition;
	}
}
