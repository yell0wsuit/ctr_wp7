using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    internal sealed class BoxFabricOriginal : BoxFabric
    {
        public static BaseElement buildBox(int quad)
        {
            Image image = Image.Image_createWithResIDQuad(399, quad);
            image.doRestoreCutTransparency();
            image.anchor = image.parentAnchor = 9;
            Image image2 = Image.Image_createWithResIDQuad(399, quad);
            image2.doRestoreCutTransparency();
            image2.anchor = image2.parentAnchor = 9;
            image2.scaleX = -1f;
            image2.x += 1.33f;
            _ = image.addChild(image2);
            return image;
        }

        protected override BaseElement buildGameBox(int i, int n, BaseElement container, MenuController.TouchBaseElement tpack, ScrollableContainer c)
        {
            BaseElement baseElement = buildBox(n);
            BaseElement baseElement2 = buildText(1310785 + n, false);
            if (baseElement2.width > 200f)
            {
                baseElement2.scaleX = baseElement2.scaleY = 200f / baseElement2.width;
            }
            _ = baseElement.addChild(baseElement2);
            int unlockedForPackLevel = (int)CTRPreferences.getUnlockedForPackLevel(n, 0);
            if (unlockedForPackLevel >= 1)
            {
                tpack.bid = 2000 + n;
                MonsterSlot monsterSlot = MonsterSlot.createMonsterSlot();
                Image image = Image.Image_createWithResIDQuad(71, 0);
                monsterSlot.c = c;
                monsterSlot.anchor = monsterSlot.parentAnchor = 9;
                _ = container.addChild(monsterSlot);
                image.doRestoreCutTransparency();
                image.anchor = 18;
                image.parentAnchor = -1;
                monsterSlot.s = (250f * i) - 80f;
                monsterSlot.e = monsterSlot.s + 160f;
                image.x = 156f;
                image.y = 246f;
                monsterSlot.x = 80f;
                monsterSlot.y = 170f;
                _ = monsterSlot.addChild(image);
                if (CTRPreferences.isBannersMustBeShown())
                {
                    image.y -= 50f;
                }
            }
            if (unlockedForPackLevel != 1)
            {
                int num = CTRPreferences.packUnlockStars(n);
                BaseElement baseElement3 = buildLock();
                _ = baseElement.addChild(baseElement3);
                if (unlockedForPackLevel == 4)
                {
                    Button button = MenuController.createBigButtonWithTextIDDelegate(Application.getString(1310723), 29, buttonDelegate);
                    button.anchor = button.parentAnchor = 18;
                    button.y = 27f;
                    _ = baseElement3.addChild(button);
                }
                else if (unlockedForPackLevel == 0)
                {
                    tpack.bid = 2000 + n;
                    HBox hbox = MenuController.createTextWithStar(num.ToString());
                    hbox.anchor = hbox.parentAnchor = 18;
                    hbox.y = 40f;
                    hbox.x = 5f;
                    _ = baseElement3.addChild(hbox);
                    if (n >= CTRPreferences.getPacksCount())
                    {
                        Button button2 = MenuController.createBigButtonWithTextIDDelegate(Application.getString(1310723), 40, buttonDelegate);
                        button2.anchor = button2.parentAnchor = 18;
                        button2.y = 27f;
                        _ = baseElement3.addChild(button2);
                        hbox.visible = false;
                    }
                }
            }
            if (CTRPreferences.isPackPerfect(n))
            {
                _ = baseElement.addChild(buildPerfectMark());
            }
            return baseElement;
        }

        protected override BaseElement buildComingSoonBox()
        {
            BaseElement baseElement = buildBox(14);
            _ = baseElement.addChild(buildText(1310799, true));
            return baseElement;
        }

        protected override BaseElement buildVideoBox(ButtonDelegate buttonDelegate)
        {
            MenuController.TouchBaseElement touchBaseElement = (MenuController.TouchBaseElement)new MenuController.TouchBaseElement().init();
            touchBaseElement.height = touchBaseElement.width = 300;
            touchBaseElement.anchor = touchBaseElement.parentAnchor = 18;
            touchBaseElement.delegateButtonDelegate = buttonDelegate;
            touchBaseElement.bid = 53;
            touchBaseElement.bbc = MakeRectangle(60.0, 90.0, -60.0, -180.0);
            BaseElement baseElement = MenuController.createButtonCartoons(53, buttonDelegate, false);
            baseElement.anchor = baseElement.parentAnchor = 18;
            baseElement.x = 26f;
            _ = touchBaseElement.addChild(baseElement);
            return touchBaseElement;
        }

        public sealed class MonsterSlot : RectangleElement
        {
            public static MonsterSlot createMonsterSlot()
            {
                MonsterSlot monsterSlot = new();
                _ = monsterSlot.init();
                monsterSlot.color = RGBAColor.MakeRGBA(0.17647058823529413, 0.17647058823529413, 0.20784313725490197, 1.0);
                monsterSlot.height = 90;
                monsterSlot.width = 200;
                return monsterSlot;
            }

            public override void draw()
            {
                preDraw();
                OpenGL.glDisable(0);
                if (solid)
                {
                    GLDrawer.drawSolidRectWOBorder(drawX, drawY, width, height, color);
                }
                else
                {
                    GLDrawer.drawRect(drawX, drawY, width, height, color);
                }
                OpenGL.glEnable(0);
                OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
                float x = c.getScroll().x;
                if (x >= s && x < e)
                {
                    OpenGL.glEnable(4);
                    float num = x - ((s + e) / 2f);
                    OpenGL.setScissorRectangle(120.0 - (double)num, 0.0, 100.0, SCREEN_HEIGHT);
                    postDraw();
                    OpenGL.glDisable(4);
                }
            }

            public ScrollableContainer c;

            public float s;

            public float e;
        }
    }
}
