using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x0200000E RID: 14
    internal class GameObject : Animation
    {
        // Token: 0x060000E8 RID: 232 RVA: 0x00007628 File Offset: 0x00005828
        public static GameObject GameObject_createWithResID(int r)
        {
            return GameObject_create(Application.getTexture(r));
        }

        // Token: 0x060000E9 RID: 233 RVA: 0x00007638 File Offset: 0x00005838
        private static GameObject GameObject_create(Texture2D t)
        {
            GameObject gameObject = new();
            _ = gameObject.initWithTexture(t);
            return gameObject;
        }

        // Token: 0x060000EA RID: 234 RVA: 0x00007654 File Offset: 0x00005854
        public static GameObject GameObject_createWithResIDQuad(int r, int q)
        {
            GameObject gameObject = GameObject_create(Application.getTexture(r));
            gameObject.setDrawQuad(q);
            return gameObject;
        }

        // Token: 0x060000EB RID: 235 RVA: 0x00007678 File Offset: 0x00005878
        public override Image initWithTexture(Texture2D t)
        {
            if (base.initWithTexture(t) != null)
            {
                bb = new Rectangle(0f, 0f, (float)width, (float)height);
                rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
                anchor = 18;
                rotatedBB = false;
                topLeftCalculated = false;
            }
            return this;
        }

        // Token: 0x060000EC RID: 236 RVA: 0x00007700 File Offset: 0x00005900
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

        // Token: 0x060000ED RID: 237 RVA: 0x00007796 File Offset: 0x00005996
        public override void draw()
        {
            base.draw();
            if (isDrawBB)
            {
                drawBB();
            }
        }

        // Token: 0x060000EE RID: 238 RVA: 0x000077AC File Offset: 0x000059AC
        public override void dealloc()
        {
            NSREL(mover);
            base.dealloc();
        }

        // Token: 0x060000EF RID: 239 RVA: 0x000077C0 File Offset: 0x000059C0
        public virtual GameObject initWithTextureIDxOffyOffXML(int t, int tx, int ty, XMLNode xml)
        {
            if (base.initWithTexture(Application.getTexture(t)) != null)
            {
                float num = (float)xml["x"].intValue();
                float num2 = (float)xml["y"].intValue();
                x = (float)tx + num;
                y = (float)ty + num2;
                type = t;
                NSString nsstring = xml["bb"];
                if (nsstring != null)
                {
                    List<NSString> list = nsstring.componentsSeparatedByString(',');
                    bb = new Rectangle((float)list[0].intValue(), (float)list[1].intValue(), (float)list[2].intValue(), (float)list[3].intValue());
                }
                else
                {
                    bb = new Rectangle(0f, 0f, (float)width, (float)height);
                }
                rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
                parseMover(xml);
            }
            return this;
        }

        // Token: 0x060000F0 RID: 240 RVA: 0x000078DC File Offset: 0x00005ADC
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
                    num = num2 / 2 + 1;
                }
                float num3 = xml["moveSpeed"].floatValue();
                float num4 = xml["rotateSpeed"].floatValue();
                Mover mover = new Mover().initWithPathCapacityMoveSpeedRotateSpeed(num, num3, num4);
                mover.angle = (double)rotation;
                mover.setPathFromStringandStart(nsstring, vect(x, y));
                setMover(mover);
                mover.start();
            }
        }

        // Token: 0x060000F1 RID: 241 RVA: 0x000079AA File Offset: 0x00005BAA
        public virtual void setMover(Mover m)
        {
            mover = m;
        }

        // Token: 0x060000F2 RID: 242 RVA: 0x000079B4 File Offset: 0x00005BB4
        public virtual void setBBFromFirstQuad()
        {
            bb = new Rectangle((float)Math.Round((double)texture.quadOffsets[0].x), (float)Math.Round((double)texture.quadOffsets[0].y), texture.quadRects[0].w, texture.quadRects[0].h);
            rbb = new Quad2D(bb.x, bb.y, bb.w, bb.h);
        }

        // Token: 0x060000F3 RID: 243 RVA: 0x00007A6C File Offset: 0x00005C6C
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
            vector = vectRotateAround(vector, (double)DEGREES_TO_RADIANS(a), (float)((double)width / 2.0 + (double)rotationCenterX), (float)((double)height / 2.0 + (double)rotationCenterY));
            vector2 = vectRotateAround(vector2, (double)DEGREES_TO_RADIANS(a), (float)((double)width / 2.0 + (double)rotationCenterX), (float)((double)height / 2.0 + (double)rotationCenterY));
            vector3 = vectRotateAround(vector3, (double)DEGREES_TO_RADIANS(a), (float)((double)width / 2.0 + (double)rotationCenterX), (float)((double)height / 2.0 + (double)rotationCenterY));
            vector4 = vectRotateAround(vector4, (double)DEGREES_TO_RADIANS(a), (float)((double)width / 2.0 + (double)rotationCenterX), (float)((double)height / 2.0 + (double)rotationCenterY));
            rbb.tlX = vector.x;
            rbb.tlY = vector.y;
            rbb.trX = vector2.x;
            rbb.trY = vector2.y;
            rbb.brX = vector3.x;
            rbb.brY = vector3.y;
            rbb.blX = vector4.x;
            rbb.blY = vector4.y;
        }

        // Token: 0x060000F4 RID: 244 RVA: 0x00007CC8 File Offset: 0x00005EC8
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

        // Token: 0x060000F5 RID: 245 RVA: 0x00007E80 File Offset: 0x00006080
        public static bool objectsIntersect(GameObject o1, GameObject o2)
        {
            float num = o1.drawX + o1.bb.x;
            float num2 = o1.drawY + o1.bb.y;
            float num3 = o2.drawX + o2.bb.x;
            float num4 = o2.drawY + o2.bb.y;
            return rectInRect(num, num2, num + o1.bb.w, num2 + o1.bb.h, num3, num4, num3 + o2.bb.w, num4 + o2.bb.h);
        }

        // Token: 0x060000F6 RID: 246 RVA: 0x00007F18 File Offset: 0x00006118
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

        // Token: 0x060000F7 RID: 247 RVA: 0x0000808C File Offset: 0x0000628C
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

        // Token: 0x060000F8 RID: 248 RVA: 0x00008230 File Offset: 0x00006430
        public static bool pointInObject(Vector p, GameObject o)
        {
            float num = o.drawX + o.bb.x;
            float num2 = o.drawY + o.bb.y;
            return pointInRect(p.x, p.y, num, num2, o.bb.w, o.bb.h);
        }

        // Token: 0x060000F9 RID: 249 RVA: 0x00008290 File Offset: 0x00006490
        public static bool rectInObject(float r1x, float r1y, float r2x, float r2y, GameObject o)
        {
            float num = o.drawX + o.bb.x;
            float num2 = o.drawY + o.bb.y;
            return rectInRect(r1x, r1y, r2x, r2y, num, num2, num + o.bb.w, num2 + o.bb.h);
        }

        // Token: 0x04000729 RID: 1833
        public const int MAX_MOVER_CAPACITY = 100;

        // Token: 0x0400072A RID: 1834
        public int state;

        // Token: 0x0400072B RID: 1835
        public int type;

        // Token: 0x0400072C RID: 1836
        public Mover mover;

        // Token: 0x0400072D RID: 1837
        public Rectangle bb;

        // Token: 0x0400072E RID: 1838
        public Quad2D rbb;

        // Token: 0x0400072F RID: 1839
        public bool rotatedBB;

        // Token: 0x04000730 RID: 1840
        public bool isDrawBB;

        // Token: 0x04000731 RID: 1841
        public bool topLeftCalculated;
    }
}
