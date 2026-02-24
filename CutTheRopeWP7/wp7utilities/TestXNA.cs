using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    internal sealed class TestXNA : BaseElement
    {
        private void cameraTestMove()
        {
            objects.Add(new TestCameraMove());
        }

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

        public TestXNA()
        {
            _ = init();
            scaleTestTop();
            scaleTestCenter();
            scaleTestBottom();
        }

        public override void update(float delta)
        {
            foreach (BaseElement baseElement in objects)
            {
                baseElement.update(delta);
            }
        }

        public override void draw()
        {
            foreach (BaseElement baseElement in objects)
            {
                baseElement.draw();
            }
        }

        private readonly List<BaseElement> objects = [];

        private sealed class TestCameraMove : BaseElement
        {
            public TestCameraMove()
            {
                speed_ = new Vector2(10f, 10f);
            }

            public override void update(float delta)
            {
                frame_++;
                if (frame_ > 1000)
                {
                    frame_ = 0;
                }
            }

            public override void draw()
            {
            }

            private Vector2 speed_;

            private int frame_;
        }

        private sealed class TestRotate(BaseElement testObject) : BaseElement
        {

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

            public override void draw()
            {
                testObject_?.draw();
            }

            private readonly BaseElement testObject_ = testObject;
        }
    }
}
