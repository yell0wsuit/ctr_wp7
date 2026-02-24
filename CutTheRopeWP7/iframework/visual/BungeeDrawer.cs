using System;

using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.sfe;

namespace ctr_wp7.iframework.visual
{
    internal sealed class BungeeDrawer : BaseElement
    {
        public override void draw()
        {
            preDraw();
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            bungee.draw();
            OpenGL.SetWhiteColor();
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glEnable(0);
            postDraw();
        }

        public override void update(float delta)
        {
            base.update(delta);
            float num = 0.025f;
            Vector quadCenter = Image.getQuadCenter(77, 8);
            bungee.bungeeAnchor.pos = vectAdd(vect(parent.drawX, parent.drawY), quadCenter);
            bungee.bungeeAnchor.pin = bungee.bungeeAnchor.pos;
            float num2 = 25f;
            bungee.tail.applyImpulseDelta(vect(-bungee.tail.v.x / num2, -bungee.tail.v.y / num2), num);
            bungee.update(num);
            bungee.tail.update(num);
            float num3 = 1f - (parent.y / PromoBanner.BANNER_OFFSET);
            fadeElement.color.a = 0.4f * num3;
            fadeElement.setEnabled(parent.y != PromoBanner.BANNER_OFFSET);
            if (down)
            {
                bungee.tail.pos = tailPos;
                if (bungee.relaxed != 0)
                {
                    ConstraintedPoint constraintedPoint = bungee.parts[0];
                    ConstraintedPoint constraintedPoint2 = bungee.parts[1];
                    float num4 = vectDistance(constraintedPoint.pos, constraintedPoint2.pos);
                    Vector vector = vectSub(bungee.tail.pos, bungee.bungeeAnchor.pos);
                    float num5 = 1f;
                    float num6 = (num4 - 30f) * num5;
                    if ((double)Math.Abs(vector.y) > 20.0)
                    {
                        if (tailPos.y > bungee.bungeeAnchor.pos.y)
                        {
                            parent.y += num6;
                        }
                        else
                        {
                            parent.y -= num6;
                        }
                        parent.y = FIT_TO_BOUNDARIES(parent.y, PromoBanner.BANNER_OFFSET, 100f);
                        return;
                    }
                    if ((double)num4 > 45.0)
                    {
                        down = false;
                        delegateButtonDelegate?.onButtonPressed(bid);
                    }
                }
            }
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            bool flag = base.onTouchDownXY(tx, ty);
            if (pointInRect(tx, ty, bungee.tail.pos.x - 35f, bungee.tail.pos.y - 15f, 70f, 70f))
            {
                fadeElement.setEnabled(true);
                down = true;
                dragStart = vect(tx, ty);
                tailStart = bungee.tail.pos;
                _ = onTouchMoveXY(tx, ty);
                return true;
            }
            return flag;
        }

        public override bool onTouchUpXY(float tx, float ty)
        {
            bool flag = base.onTouchUpXY(tx, ty);
            if (down)
            {
                down = false;
                delegateButtonDelegate?.onButtonPressed(bid);
                return true;
            }
            return flag;
        }

        public override bool onTouchMoveXY(float tx, float ty)
        {
            bool flag = base.onTouchMoveXY(tx, ty);
            if (down)
            {
                float num = 1f;
                if (ty > SCREEN_HEIGHT / 2f)
                {
                    num /= 1.5f;
                }
                Vector vector = vectSub(vect(tx, ty), dragStart);
                vector = vectMult(vector, num);
                tailPos = vectAdd(tailStart, vector);
                dragStart = vect(tx, ty);
                tailStart = tailPos;
            }
            return flag;
        }

        public ButtonDelegate delegateButtonDelegate;

        public int bid;

        public Bungee bungee;

        public Processing fadeElement;

        public bool down;

        public Vector dragStart;

        public Vector tailStart;

        public Vector tailPos;
    }
}
