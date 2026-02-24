using System;
using ctr_wp7.ctr_original;
using ctr_wp7.game.remotedata;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
	// Token: 0x02000018 RID: 24
	internal class CartoonsSelectView : MenuView
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00009D08 File Offset: 0x00007F08
		public virtual NSObject initFullscreenBackgroundDelegate(BaseElement background, ButtonDelegate d)
		{
			if (base.initFullscreen() != null)
			{
				this.buttonDelegate = d;
				float num = 20f;
				float num2 = 30f;
				this.sheight = FrameworkTypes.SCREEN_HEIGHT_EXPANDED - 60f - 2f * num;
				BaseElement baseElement = (BaseElement)new BaseElement().init();
				baseElement.x = -FrameworkTypes.SCREEN_OFFSET_X;
				baseElement.y = num - FrameworkTypes.SCREEN_OFFSET_Y;
				baseElement.width = (int)FrameworkTypes.SCREEN_WIDTH_EXPANDED;
				baseElement.height = (int)this.sheight;
				Image image = Image.Image_createWithResIDQuad(403, 11);
				image.anchor = 18;
				image.parentAnchor = 10;
				baseElement.addChild(image);
				Image image2 = Image.Image_createWithResIDQuad(403, 11);
				image2.scaleY = -1f;
				image2.anchor = 18;
				image2.parentAnchor = 34;
				baseElement.addChild(image2);
				this.box = new VBox().initWithOffsetAlignWidth(num2, 2, FrameworkTypes.SCREEN_WIDTH);
				this.buildBlocks();
				float num3 = 3f;
				ScrollableContainer scrollableContainer = new ScrollableContainer().initWithWidthHeightContainer((float)this.box.width, this.sheight + num3 * 2f, this.box);
				scrollableContainer.shouldBounceVertically = true;
				scrollableContainer.resetScrollOnShow = false;
				scrollableContainer.untouchChildsOnMove = true;
				scrollableContainer.anchor = (scrollableContainer.parentAnchor = 10);
				scrollableContainer.y = -num3;
				baseElement.addChild(scrollableContainer);
				Image image3 = Image.Image_createWithResIDQuad(403, 12);
				image3.anchor = 18;
				image3.parentAnchor = 10;
				baseElement.addChild(image3);
				Image image4 = Image.Image_createWithResIDQuad(403, 12);
				image4.anchor = 18;
				image4.parentAnchor = 34;
				image4.scaleY = -1f;
				baseElement.addChild(image4);
				int num4 = 48;
				Button button = MenuController.createBackButtonWithDelegateID(this.buttonDelegate, num4);
				background.addChild(button);
				if (ResDataPhoneFull.LANGUAGE != Language.LANG_ZH)
				{
					Application.sharedPreferences().remoteDataManager.getHideSocialNetworks();
				}
				background.addChild(baseElement);
				this.addChild(background);
				this.curtain = (RectangleElement)new RectangleElement().init();
				this.curtain.anchor = (this.curtain.parentAnchor = 9);
				this.curtain.x = -FrameworkTypes.SCREEN_OFFSET_X;
				this.curtain.y = -FrameworkTypes.SCREEN_OFFSET_Y;
				this.curtain.width = (int)FrameworkTypes.SCREEN_WIDTH_EXPANDED;
				this.curtain.height = (int)FrameworkTypes.SCREEN_HEIGHT_EXPANDED;
				this.curtain.color = RGBAColor.blackRGBA;
				this.curtain.setEnabled(false);
				this.addChild(this.curtain);
			}
			return this;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009FC4 File Offset: 0x000081C4
		public virtual void buildBlocks()
		{
			BaseElement baseElement = (BaseElement)new BaseElement().init();
			this.box.addChild(baseElement);
			BlockConfig blockConfig = VideoDataManager.getBlockConfig();
			int totalBlocks = blockConfig.getTotalBlocks();
			if (totalBlocks > 0)
			{
				for (int i = 0; i < totalBlocks; i++)
				{
					Button button = ButtonBlock.createWithIDDelegateBlock(4000 + i, this.buttonDelegate, blockConfig.getBlock(i));
					this.box.addChild(button);
				}
			}
			else
			{
				Button button2 = ButtonBlock.createWithIDDelegateBlock(4000, this.buttonDelegate, blockConfig.getBlock(-1));
				this.box.addChild(button2);
			}
			BaseElement baseElement2 = (BaseElement)new BaseElement().init();
			this.box.addChild(baseElement2);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A07C File Offset: 0x0000827C
		public virtual void rebuild()
		{
			CartoonsSelectView.needrebuild = false;
			this.box.removeAllChilds();
			((VBox)this.box).nextElementY = 0f;
			this.buildBlocks();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A0AA File Offset: 0x000082AA
		public virtual bool isRebuildNeeded()
		{
			return CartoonsSelectView.needrebuild;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public virtual void notifyBlockWatched(int blocknum)
		{
			BaseElement child = this.box.getChild(blocknum + 1);
			if (child != null)
			{
				BaseElement childWithName = child.getChildWithName(NSObject.NSS("nimbus"));
				if (childWithName != null)
				{
					childWithName.setEnabled(false);
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A0EE File Offset: 0x000082EE
		public virtual void openCurtain()
		{
			this.curtain.setEnabled(true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A0FC File Offset: 0x000082FC
		public virtual void closeCurtain()
		{
			this.curtain.setEnabled(false);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A10A File Offset: 0x0000830A
		public override void update(float delta)
		{
			if (CartoonsSelectView.needrebuild)
			{
				this.rebuild();
			}
			base.update(delta);
		}

		// Token: 0x0400075F RID: 1887
		protected ButtonDelegate buttonDelegate;

		// Token: 0x04000760 RID: 1888
		protected BaseElement box;

		// Token: 0x04000761 RID: 1889
		protected float sheight;

		// Token: 0x04000762 RID: 1890
		protected RectangleElement curtain;

		// Token: 0x04000763 RID: 1891
		public static bool needrebuild;
	}
}
