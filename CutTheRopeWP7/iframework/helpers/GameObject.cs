using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    internal class GameObject : Animation
    {
        public static GameObject GameObject_createWithResID(int r)
        {
            return GameObject_create(Application.getTexture(r));
        }

        private static GameObject GameObject_create(Texture2D t)
        {
            GameObject gameObject = new();
            _ = gameObject.initWithTexture(t);
            return gameObject;
        }

        public static GameObject GameObject_createWithResIDQuad(int r, int q)
        {
            GameObject gameObject = GameObject_create(Application.getTexture(r));
            gameObject.setDrawQuad(q);
            return gameObject;
        }

        public override Image initWithTexture(Texture2D t)
        {
            if (base.initWithTexture(t) != null)
            {
                bb = new Rectangle(0f, 0f, width, height);
                rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
                anchor = 18;
                rotatedBB = false;
                topLeftCalculated = false;
            }
            return this;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (!topLeftCalculated)
            {
                calculateTopLeft(this);
                topLeftCalculated = true;
            }
            if (mover != null)
            {
                mover.update(delta);
                x = mover.pos.x;
                y = mover.pos.y;
                if (rotatedBB)
                {
                    rotateWithBB((float)mover.angle);
                    return;
                }
                rotation = (float)mover.angle;
            }
        }

        public override void draw()
        {
            base.draw();
            if (isDrawBB)
            {
                drawBB();
            }
        }

        public override void dealloc()
        {
            NSREL(mover);
            base.dealloc();
        }

        public virtual GameObject initWithTextureIDxOffyOffXML(int t, int tx, int ty, XMLNode xml)
        {
            if (base.initWithTexture(Application.getTexture(t)) != null)
            {
                float num = xml["x"].intValue();
                float num2 = xml["y"].intValue();
                x = tx + num;
                y = ty + num2;
                type = t;
                NSString nsstring = xml["bb"];
                if (nsstring != null)
                {
                    List<NSString> list = nsstring.componentsSeparatedByString(',');
                    bb = new Rectangle(list[0].intValue(), list[1].intValue(), list[2].intValue(), list[3].intValue());
                }
                else
                {
                    bb = new Rectangle(0f, 0f, width, height);
                }
                rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
                parseMover(xml);
            }
            return this;
        }

        public virtual void parseMover(XMLNode xml)
        {
            rotation = xml["angle"].floatValue();
            NSString nsstring = xml["path"];
            if (nsstring != null && nsstring.length() != 0)
            {
                int num = 100;
                if (nsstring.characterAtIndex(0) == 'R')
                {
                    NSString nsstring2 = nsstring.substringFromIndex(2);
                    int num2 = nsstring2.intValue();
                    num = (num2 / 2) + 1;
                }
                float num3 = xml["moveSpeed"].floatValue();
                float num4 = xml["rotateSpeed"].floatValue();
                Mover mover = new Mover().initWithPathCapacityMoveSpeedRotateSpeed(num, num3, num4);
                mover.angle = rotation;
                mover.setPathFromStringandStart(nsstring, vect(x, y));
                setMover(mover);
                mover.start();
            }
        }

        public virtual void setMover(Mover m)
        {
            mover = m;
        }

        public virtual void setBBFromFirstQuad()
        {
            bb = new Rectangle((float)Math.Round(texture.quadOffsets[0].x), (float)Math.Round(texture.quadOffsets[0].y), texture.quadRects[0].w, texture.quadRects[0].h);
            rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
        }

        public virtual void rotateWithBB(float a)
        {
            if (!rotatedBB)
            {
                rotatedBB = true;
            }
            rotation = a;
            Vector vector = vect(bb.x, bb.y);
            Vector vector2 = vect(bb.x + bb.w, bb.y);
            Vector vector3 = vect(bb.x + bb.w, bb.y + bb.h);
            Vector vector4 = vect(bb.x, bb.y + bb.h);
            vector = vectRotateAround(vector, (double)DEGREES_TO_RADIANS(a), (float)((width / 2.0) + rotationCenterX), (float)((height / 2.0) + rotationCenterY));
            vector2 = vectRotateAround(vector2, (double)DEGREES_TO_RADIANS(a), (float)((width / 2.0) + rotationCenterX), (float)((height / 2.0) + rotationCenterY));
            vector3 = vectRotateAround(vector3, (double)DEGREES_TO_RADIANS(a), (float)((width / 2.0) + rotationCenterX), (float)((height / 2.0) + rotationCenterY));
            vector4 = vectRotateAround(vector4, (double)DEGREES_TO_RADIANS(a), (float)((width / 2.0) + rotationCenterX), (float)((height / 2.0) + rotationCenterY));
            rbb.tlX = vector.x;
            rbb.tlY = vector.y;
            rbb.trX = vector2.x;
            rbb.trY = vector2.y;
            rbb.brX = vector3.x;
            rbb.brY = vector3.y;
            rbb.blX = vector4.x;
            rbb.blY = vector4.y;
        }

        public virtual void drawBB()
        {
            OpenGL.glDisable(0);
            if (rotatedBB)
            {
                OpenGL.drawSegment(drawX + rbb.tlX, drawY + rbb.tlY, drawX + rbb.trX, drawY + rbb.trY, RGBAColor.redRGBA);
                OpenGL.drawSegment(drawX + rbb.trX, drawY + rbb.trY, drawX + rbb.brX, drawY + rbb.brY, RGBAColor.redRGBA);
                OpenGL.drawSegment(drawX + rbb.brX, drawY + rbb.brY, drawX + rbb.blX, drawY + rbb.blY, RGBAColor.redRGBA);
                OpenGL.drawSegment(drawX + rbb.blX, drawY + rbb.blY, drawX + rbb.tlX, drawY + rbb.tlY, RGBAColor.redRGBA);
            }
            else
            {
                GLDrawer.drawRect(drawX + bb.x, drawY + bb.y, bb.w, bb.h, RGBAColor.redRGBA);
            }
            OpenGL.glEnable(0);
            OpenGL.SetWhiteColor();
        }

        public static bool objectsIntersect(GameObject o1, GameObject o2)
        {
            float num = o1.drawX + o1.bb.x;
            float num2 = o1.drawY + o1.bb.y;
            float num3 = o2.drawX + o2.bb.x;
            float num4 = o2.drawY + o2.bb.y;
            return rectInRect(num, num2, num + o1.bb.w, num2 + o1.bb.h, num3, num4, num3 + o2.bb.w, num4 + o2.bb.h);
        }

        private static bool objectsIntersectRotated(GameObject o1, GameObject o2)
        {
            Vector vector = vect(o1.drawX + o1.rbb.tlX, o1.drawY + o1.rbb.tlY);
            Vector vector2 = vect(o1.drawX + o1.rbb.trX, o1.drawY + o1.rbb.trY);
            Vector vector3 = vect(o1.drawX + o1.rbb.brX, o1.drawY + o1.rbb.brY);
            Vector vector4 = vect(o1.drawX + o1.rbb.blX, o1.drawY + o1.rbb.blY);
            Vector vector5 = vect(o2.drawX + o2.rbb.tlX, o2.drawY + o2.rbb.tlY);
            Vector vector6 = vect(o2.drawX + o2.rbb.trX, o2.drawY + o2.rbb.trY);
            Vector vector7 = vect(o2.drawX + o2.rbb.brX, o2.drawY + o2.rbb.brY);
            Vector vector8 = vect(o2.drawX + o2.rbb.blX, o2.drawY + o2.rbb.blY);
            return obbInOBB(vector, vector2, vector3, vector4, vector5, vector6, vector7, vector8);
        }

        private static bool objectsIntersectRotatedWithUnrotated(GameObject o1, GameObject o2)
        {
            Vector vector = vect(o1.drawX + o1.rbb.tlX, o1.drawY + o1.rbb.tlY);
            Vector vector2 = vect(o1.drawX + o1.rbb.trX, o1.drawY + o1.rbb.trY);
            Vector vector3 = vect(o1.drawX + o1.rbb.brX, o1.drawY + o1.rbb.brY);
            Vector vector4 = vect(o1.drawX + o1.rbb.blX, o1.drawY + o1.rbb.blY);
            Vector vector5 = vect(o2.drawX + o2.bb.x, o2.drawY + o2.bb.y);
            Vector vector6 = vect(o2.drawX + o2.bb.x + o2.bb.w, o2.drawY + o2.bb.y);
            Vector vector7 = vect(o2.drawX + o2.bb.x + o2.bb.w, o2.drawY + o2.bb.y + o2.bb.h);
            Vector vector8 = vect(o2.drawX + o2.bb.x, o2.drawY + o2.bb.y + o2.bb.h);
            return obbInOBB(vector, vector2, vector3, vector4, vector5, vector6, vector7, vector8);
        }

        public static bool pointInObject(Vector p, GameObject o)
        {
            float num = o.drawX + o.bb.x;
            float num2 = o.drawY + o.bb.y;
            return pointInRect(p.x, p.y, num, num2, o.bb.w, o.bb.h);
        }

        public static bool rectInObject(float r1x, float r1y, float r2x, float r2y, GameObject o)
        {
            float num = o.drawX + o.bb.x;
            float num2 = o.drawY + o.bb.y;
            return rectInRect(r1x, r1y, r2x, r2y, num, num2, num + o.bb.w, num2 + o.bb.h);
        }

        public const int MAX_MOVER_CAPACITY = 100;

        public int state;

        public int type;

        public Mover mover;

        public Rectangle bb;

        public Quad2D rbb;

        public bool rotatedBB;

        public bool isDrawBB;

        public bool topLeftCalculated;
    }
}
