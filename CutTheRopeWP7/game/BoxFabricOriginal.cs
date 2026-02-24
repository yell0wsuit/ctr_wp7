using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x020000F0 RID: 240
    internal class BoxFabricOriginal : BoxFabric
    {
        // Token: 0x06000738 RID: 1848 RVA: 0x0003A510 File Offset: 0x00038710
        public virtual BaseElement buildBox(int quad)
        {
            Image image = Image.Image_createWithResIDQuad(399, quad);
            image.doRestoreCutTransparency();
            image.anchor = (image.parentAnchor = 9);
            Image image2 = Image.Image_createWithResIDQuad(399, quad);
            image2.doRestoreCutTransparency();
            image2.anchor = (image2.parentAnchor = 9);
            image2.scaleX = -1f;
            image2.x += 1.33f;
            image.addChild(image2);
            return image;
        }

        // Token: 0x06000739 RID: 1849 RVA: 0x0003A58C File Offset: 0x0003878C
        protected override BaseElement buildGameBox(int i, int n, BaseElement container, MenuController.TouchBaseElement tpack, ScrollableContainer c)
        {
            BaseElement baseElement = this.buildBox(n);
            BaseElement baseElement2 = this.buildText(1310785 + n, false);
            if ((float)baseElement2.width > 200f)
            {
                baseElement2.scaleX = (baseElement2.scaleY = 200f / (float)baseElement2.width);
            }
            baseElement.addChild(baseElement2);
            int unlockedForPackLevel = (int)CTRPreferences.getUnlockedForPackLevel(n, 0);
            if (unlockedForPackLevel >= 1)
            {
                tpack.bid = 2000 + n;
                BoxFabricOriginal.MonsterSlot monsterSlot = BoxFabricOriginal.MonsterSlot.createMonsterSlot();
                Image image = Image.Image_createWithResIDQuad(71, 0);
                monsterSlot.c = c;
                monsterSlot.anchor = (monsterSlot.parentAnchor = 9);
                container.addChild(monsterSlot);
                image.doRestoreCutTransparency();
                image.anchor = 18;
                image.parentAnchor = -1;
                monsterSlot.s = 250f * (float)i - 80f;
                monsterSlot.e = monsterSlot.s + 160f;
                image.x = 156f;
                image.y = 246f;
                monsterSlot.x = 80f;
                monsterSlot.y = 170f;
                monsterSlot.addChild(image);
                if (CTRPreferences.isBannersMustBeShown())
                {
                    image.y -= 50f;
                }
            }
            if (unlockedForPackLevel != 1)
            {
                int num = CTRPreferences.packUnlockStars(n);
                BaseElement baseElement3 = this.buildLock();
                baseElement.addChild(baseElement3);
                if (unlockedForPackLevel == 4)
                {
                    Button button = MenuController.createBigButtonWithTextIDDelegate(Application.getString(1310723), 29, this.buttonDelegate);
                    button.anchor = (button.parentAnchor = 18);
                    button.y = 27f;
                    baseElement3.addChild(button);
                }
                else if (unlockedForPackLevel == 0)
                {
                    tpack.bid = 2000 + n;
                    HBox hbox = MenuController.createTextWithStar(num.ToString());
                    hbox.anchor = (hbox.parentAnchor = 18);
                    hbox.y = 40f;
                    hbox.x = 5f;
                    baseElement3.addChild(hbox);
                    if (n >= CTRPreferences.getPacksCount())
                    {
                        Button button2 = MenuController.createBigButtonWithTextIDDelegate(Application.getString(1310723), 40, this.buttonDelegate);
                        button2.anchor = (button2.parentAnchor = 18);
                        button2.y = 27f;
                        baseElement3.addChild(button2);
                        hbox.visible = false;
                    }
                }
            }
            if (CTRPreferences.isPackPerfect(n))
            {
                baseElement.addChild(this.buildPerfectMark());
            }
            return baseElement;
        }

        // Token: 0x0600073A RID: 1850 RVA: 0x0003A7F4 File Offset: 0x000389F4
        protected override BaseElement buildComingSoonBox()
        {
            BaseElement baseElement = this.buildBox(14);
            baseElement.addChild(this.buildText(1310799, true));
            return baseElement;
        }

        // Token: 0x0600073B RID: 1851 RVA: 0x0003A820 File Offset: 0x00038A20
        protected override BaseElement buildVideoBox(ButtonDelegate buttonDelegate)
        {
            MenuController.TouchBaseElement touchBaseElement = (MenuController.TouchBaseElement)new MenuController.TouchBaseElement().init();
            touchBaseElement.height = (touchBaseElement.width = 300);
            touchBaseElement.anchor = (touchBaseElement.parentAnchor = 18);
            touchBaseElement.delegateButtonDelegate = buttonDelegate;
            touchBaseElement.bid = 53;
            touchBaseElement.bbc = FrameworkTypes.MakeRectangle(60.0, 90.0, -60.0, -180.0);
            BaseElement baseElement = MenuController.createButtonCartoons(53, buttonDelegate, false);
            baseElement.anchor = (baseElement.parentAnchor = 18);
            baseElement.x = 26f;
            touchBaseElement.addChild(baseElement);
            return touchBaseElement;
        }

        // Token: 0x020000F1 RID: 241
        public class MonsterSlot : RectangleElement
        {
            // Token: 0x0600073D RID: 1853 RVA: 0x0003A8DC File Offset: 0x00038ADC
            public static BoxFabricOriginal.MonsterSlot createMonsterSlot()
            {
                BoxFabricOriginal.MonsterSlot monsterSlot = new BoxFabricOriginal.MonsterSlot();
                monsterSlot.init();
                monsterSlot.color = RGBAColor.MakeRGBA(0.17647058823529413, 0.17647058823529413, 0.20784313725490197, 1.0);
                monsterSlot.height = 90;
                monsterSlot.width = 200;
                return monsterSlot;
            }

            // Token: 0x0600073E RID: 1854 RVA: 0x0003A93C File Offset: 0x00038B3C
            public override void draw()
            {
                base.preDraw();
                OpenGL.glDisable(0);
                if (this.solid)
                {
                    GLDrawer.drawSolidRectWOBorder(this.drawX, this.drawY, (float)this.width, (float)this.height, this.color);
                }
                else
                {
                    GLDrawer.drawRect(this.drawX, this.drawY, (float)this.width, (float)this.height, this.color);
                }
                OpenGL.glEnable(0);
                OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
                float x = this.c.getScroll().x;
                if (x >= this.s && x < this.e)
                {
                    OpenGL.glEnable(4);
                    float num = x - (this.s + this.e) / 2f;
                    OpenGL.setScissorRectangle(120.0 - (double)num, 0.0, 100.0, (double)FrameworkTypes.SCREEN_HEIGHT);
                    this.postDraw();
                    OpenGL.glDisable(4);
                }
            }

            // Token: 0x04000CD4 RID: 3284
            public ScrollableContainer c;

            // Token: 0x04000CD5 RID: 3285
            public float s;

            // Token: 0x04000CD6 RID: 3286
            public float e;
        }
    }
}
