using ctr_wp7.ctr_original;
using ctr_wp7.game.remotedata;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000069 RID: 105
    internal class DeliverySelectView : MenuView
    {
        // Token: 0x06000327 RID: 807 RVA: 0x00014278 File Offset: 0x00012478
        public virtual NSObject initFullscreenBackgroundDelegate(BaseElement background, ButtonDelegate d)
        {
            if (base.initFullscreen() != null)
            {
                this.buttonDelegate = d;
                this.box = new VBox().initWithOffsetAlignWidth(6f, 2, FrameworkTypes.SCREEN_WIDTH_EXPANDED);
                BaseElement baseElement = (BaseElement)new BaseElement().init();
                baseElement.height = 18;
                this.box.addChild(baseElement);
                BaseElement baseElement2 = MenuController.createButtonDelivery(43, this.buttonDelegate, 0, 13, true);
                this.box.addChild(baseElement2);
                BaseElement baseElement3 = MenuController.createButtonDelivery(44, this.buttonDelegate, 1, 15, false);
                this.box.addChild(baseElement3);
                BaseElement baseElement4 = MenuController.createButtonDelivery(45, this.buttonDelegate, 2, 14, true);
                this.box.addChild(baseElement4);
                if (CTRPreferences.isLiteVersion())
                {
                    Button button = MenuController.createButtonWithTextIDDelegateAutoScale(Application.getString(1310723), 41, this.buttonDelegate);
                    button.anchor = (button.parentAnchor = 18);
                    button.y = 23f;
                    BaseElement baseElement5 = baseElement3.getChildWithName("del_button");
                    ((Button)baseElement5).touchable = false;
                    baseElement5.getChildWithName("del_text_up").color = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.5);
                    baseElement5.getChildWithName("del_text_down").color = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.5);
                    baseElement3.addChild(button);
                    button = MenuController.createButtonWithTextIDDelegateAutoScale(Application.getString(1310723), 41, this.buttonDelegate);
                    button.anchor = (button.parentAnchor = 18);
                    button.y = 23f;
                    baseElement5 = baseElement4.getChildWithName("del_button");
                    ((Button)baseElement5).touchable = false;
                    baseElement5.getChildWithName("del_text_up").color = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.5);
                    baseElement5.getChildWithName("del_text_down").color = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.5);
                    baseElement4.addChild(button);
                }
                this.cartoonsButton = MenuController.createButtonCartoons(46, this.buttonDelegate, true);
                this.box.addChild(this.cartoonsButton);
                BaseElement baseElement6 = (BaseElement)new BaseElement().init();
                baseElement6.height = 60;
                this.box.addChild(baseElement6);
                ScrollableContainer scrollableContainer = new ScrollableContainer().initWithWidthHeightContainer((float)this.box.width, FrameworkTypes.SCREEN_HEIGHT_EXPANDED + 3f, this.box);
                scrollableContainer.shouldBounceVertically = true;
                scrollableContainer.resetScrollOnShow = false;
                scrollableContainer.anchor = (scrollableContainer.parentAnchor = 10);
                scrollableContainer.y -= FrameworkTypes.SCREEN_OFFSET_Y;
                background.addChild(scrollableContainer);
                Button button2 = MenuController.createBackButtonWithDelegateID(this.buttonDelegate, 9);
                background.addChild(button2);
                this.addChild(background);
            }
            return this;
        }

        // Token: 0x06000328 RID: 808 RVA: 0x000145BC File Offset: 0x000127BC
        public virtual void checkCartoonsWatched()
        {
            int num = 0;
            int num2 = 0;
            BlockConfig blockConfig = VideoDataManager.getBlockConfig();
            int totalBlocks = blockConfig.getTotalBlocks();
            bool flag;
            if (totalBlocks == 0)
            {
                flag = !CTRPreferences.getCartoonWatched(blockConfig.getBlock(-1).getUrl());
            }
            else
            {
                for (int i = 0; i < totalBlocks; i++)
                {
                    BlockInterface block = blockConfig.getBlock(i);
                    if (block.getType() == 1 && block.getNumber() != null && block.getNumber().length() != 0)
                    {
                        num++;
                        if (CTRPreferences.getCartoonWatched(block.getUrl()))
                        {
                            num2++;
                        }
                    }
                }
                flag = num2 != num;
            }
            this.cartoonsButton.getChildWithName(NSObject.NSS("newCartoonsLabel")).setEnabled(flag);
        }

        // Token: 0x040008D5 RID: 2261
        protected ButtonDelegate buttonDelegate;

        // Token: 0x040008D6 RID: 2262
        protected BaseElement box;

        // Token: 0x040008D7 RID: 2263
        protected BaseElement cartoonsButton;
    }
}
