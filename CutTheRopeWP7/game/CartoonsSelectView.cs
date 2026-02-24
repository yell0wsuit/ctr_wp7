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
                buttonDelegate = d;
                float num = 20f;
                float num2 = 30f;
                sheight = SCREEN_HEIGHT_EXPANDED - 60f - 2f * num;
                BaseElement baseElement = (BaseElement)new BaseElement().init();
                baseElement.x = -SCREEN_OFFSET_X;
                baseElement.y = num - SCREEN_OFFSET_Y;
                baseElement.width = (int)SCREEN_WIDTH_EXPANDED;
                baseElement.height = (int)sheight;
                Image image = Image.Image_createWithResIDQuad(403, 11);
                image.anchor = 18;
                image.parentAnchor = 10;
                _ = baseElement.addChild(image);
                Image image2 = Image.Image_createWithResIDQuad(403, 11);
                image2.scaleY = -1f;
                image2.anchor = 18;
                image2.parentAnchor = 34;
                _ = baseElement.addChild(image2);
                box = new VBox().initWithOffsetAlignWidth(num2, 2, SCREEN_WIDTH);
                buildBlocks();
                float num3 = 3f;
                ScrollableContainer scrollableContainer = new ScrollableContainer().initWithWidthHeightContainer((float)box.width, sheight + num3 * 2f, box);
                scrollableContainer.shouldBounceVertically = true;
                scrollableContainer.resetScrollOnShow = false;
                scrollableContainer.untouchChildsOnMove = true;
                scrollableContainer.anchor = (scrollableContainer.parentAnchor = 10);
                scrollableContainer.y = -num3;
                _ = baseElement.addChild(scrollableContainer);
                Image image3 = Image.Image_createWithResIDQuad(403, 12);
                image3.anchor = 18;
                image3.parentAnchor = 10;
                _ = baseElement.addChild(image3);
                Image image4 = Image.Image_createWithResIDQuad(403, 12);
                image4.anchor = 18;
                image4.parentAnchor = 34;
                image4.scaleY = -1f;
                _ = baseElement.addChild(image4);
                int num4 = 48;
                Button button = MenuController.createBackButtonWithDelegateID(buttonDelegate, num4);
                _ = background.addChild(button);
                if (LANGUAGE != Language.LANG_ZH)
                {
                    _ = Application.sharedPreferences().remoteDataManager.getHideSocialNetworks();
                }
                _ = background.addChild(baseElement);
                _ = addChild(background);
                curtain = (RectangleElement)new RectangleElement().init();
                curtain.anchor = (curtain.parentAnchor = 9);
                curtain.x = -SCREEN_OFFSET_X;
                curtain.y = -SCREEN_OFFSET_Y;
                curtain.width = (int)SCREEN_WIDTH_EXPANDED;
                curtain.height = (int)SCREEN_HEIGHT_EXPANDED;
                curtain.color = RGBAColor.blackRGBA;
                curtain.setEnabled(false);
                _ = addChild(curtain);
            }
            return this;
        }

        // Token: 0x06000120 RID: 288 RVA: 0x00009FC4 File Offset: 0x000081C4
        public virtual void buildBlocks()
        {
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            _ = box.addChild(baseElement);
            BlockConfig blockConfig = VideoDataManager.getBlockConfig();
            int totalBlocks = blockConfig.getTotalBlocks();
            if (totalBlocks > 0)
            {
                for (int i = 0; i < totalBlocks; i++)
                {
                    Button button = ButtonBlock.createWithIDDelegateBlock(4000 + i, buttonDelegate, blockConfig.getBlock(i));
                    _ = box.addChild(button);
                }
            }
            else
            {
                Button button2 = ButtonBlock.createWithIDDelegateBlock(4000, buttonDelegate, blockConfig.getBlock(-1));
                _ = box.addChild(button2);
            }
            BaseElement baseElement2 = (BaseElement)new BaseElement().init();
            _ = box.addChild(baseElement2);
        }

        // Token: 0x06000121 RID: 289 RVA: 0x0000A07C File Offset: 0x0000827C
        public virtual void rebuild()
        {
            needrebuild = false;
            box.removeAllChilds();
            ((VBox)box).nextElementY = 0f;
            buildBlocks();
        }

        // Token: 0x06000122 RID: 290 RVA: 0x0000A0AA File Offset: 0x000082AA
        public virtual bool isRebuildNeeded()
        {
            return needrebuild;
        }

        // Token: 0x06000123 RID: 291 RVA: 0x0000A0B4 File Offset: 0x000082B4
        public virtual void notifyBlockWatched(int blocknum)
        {
            BaseElement child = box.getChild(blocknum + 1);
            if (child != null)
            {
                BaseElement childWithName = child.getChildWithName(NSS("nimbus"));
                if (childWithName != null)
                {
                    childWithName.setEnabled(false);
                }
            }
        }

        // Token: 0x06000124 RID: 292 RVA: 0x0000A0EE File Offset: 0x000082EE
        public virtual void openCurtain()
        {
            curtain.setEnabled(true);
        }

        // Token: 0x06000125 RID: 293 RVA: 0x0000A0FC File Offset: 0x000082FC
        public virtual void closeCurtain()
        {
            curtain.setEnabled(false);
        }

        // Token: 0x06000126 RID: 294 RVA: 0x0000A10A File Offset: 0x0000830A
        public override void update(float delta)
        {
            if (needrebuild)
            {
                rebuild();
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
