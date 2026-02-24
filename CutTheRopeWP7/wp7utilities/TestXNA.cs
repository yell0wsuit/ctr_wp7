using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    // Token: 0x02000073 RID: 115
    internal class TestXNA : BaseElement
    {
        // Token: 0x06000376 RID: 886 RVA: 0x00016016 File Offset: 0x00014216
        private void cameraTestMove()
        {
            objects.Add(new TestCameraMove());
        }

        // Token: 0x06000377 RID: 887 RVA: 0x00016028 File Offset: 0x00014228
        private void scaleTestTop()
        {
            int num = 1310747;
            Button button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            float num2 = 0f;
            float num3 = 0f;
            button.x = num2;
            button.y = num3;
            objects.Add(button);
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            button.scaleX = 0.5f;
            button.scaleY = 0.5f;
            objects.Add(new TestRotate(button));
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            button.scaleX = 0.5f;
            button.scaleY = 0.5f;
            button.rotation = 33f;
            objects.Add(button);
        }

        // Token: 0x06000378 RID: 888 RVA: 0x000160FC File Offset: 0x000142FC
        private void scaleTestCenter()
        {
            int num = 1310721;
            Button button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            float num2 = (SCREEN_WIDTH - button.width) / 2f;
            float num3 = (SCREEN_HEIGHT - button.height) / 2f;
            button.x = num2;
            button.y = num3;
            objects.Add(button);
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            objects.Add(new TestRotate(button));
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            button.scaleX = 0.5f;
            button.scaleY = 0.5f;
            objects.Add(button);
        }

        // Token: 0x06000379 RID: 889 RVA: 0x000161CC File Offset: 0x000143CC
        private void scaleTestBottom()
        {
            int num = 1310748;
            Button button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            float num2 = SCREEN_WIDTH - button.width;
            float num3 = SCREEN_HEIGHT - button.height;
            button.x = num2;
            button.y = num3;
            objects.Add(button);
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            button.scaleX = 1.5f;
            button.scaleY = 1.5f;
            objects.Add(new TestRotate(button));
            button = MenuController.createButtonWithTextIDDelegate(Application.getString(num), 0, null);
            button.x = num2;
            button.y = num3;
            button.scaleX = 0.5f;
            button.scaleY = 0.5f;
            button.rotation = 45f;
            objects.Add(button);
        }

        // Token: 0x0600037A RID: 890 RVA: 0x000162AF File Offset: 0x000144AF
        public TestXNA()
        {
            _ = base.init();
            scaleTestTop();
            scaleTestCenter();
            scaleTestBottom();
        }

        // Token: 0x0600037B RID: 891 RVA: 0x000162DC File Offset: 0x000144DC
        public override void update(float delta)
        {
            foreach (BaseElement baseElement in objects)
            {
                baseElement.update(delta);
            }
        }

        // Token: 0x0600037C RID: 892 RVA: 0x00016330 File Offset: 0x00014530
        public override void draw()
        {
            foreach (BaseElement baseElement in objects)
            {
                baseElement.draw();
            }
        }

        // Token: 0x040008FC RID: 2300
        private List<BaseElement> objects = [];

        // Token: 0x02000074 RID: 116
        private class TestCameraMove : BaseElement
        {
            // Token: 0x0600037D RID: 893 RVA: 0x00016384 File Offset: 0x00014584
            public TestCameraMove()
            {
                speed_ = new Vector2(10f, 10f);
            }

            // Token: 0x0600037E RID: 894 RVA: 0x000163A1 File Offset: 0x000145A1
            public override void update(float delta)
            {
                frame_++;
                if (frame_ > 1000)
                {
                    frame_ = 0;
                }
            }

            // Token: 0x0600037F RID: 895 RVA: 0x000163D2 File Offset: 0x000145D2
            public override void draw()
            {
            }

            // Token: 0x040008FD RID: 2301
            private Vector2 speed_;

            // Token: 0x040008FE RID: 2302
            private int frame_;
        }

        // Token: 0x02000075 RID: 117
        private class TestRotate(BaseElement testObject) : BaseElement
        {

            // Token: 0x06000381 RID: 897 RVA: 0x000163E4 File Offset: 0x000145E4
            public override void update(float delta)
            {
                if (testObject_ != null)
                {
                    testObject_.rotation += 0.3f;
                    if (testObject_.rotation > 360f)
                    {
                        testObject_.rotation -= 360f;
                    }
                }
            }

            // Token: 0x06000382 RID: 898 RVA: 0x00016439 File Offset: 0x00014639
            public override void draw()
            {
                testObject_?.draw();
            }

            // Token: 0x040008FF RID: 2303
            private BaseElement testObject_ = testObject;
        }
    }
}
