using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000EF RID: 239
    internal class RotatedCircle : BaseElement
    {
        // Token: 0x06000729 RID: 1833 RVA: 0x0003952C File Offset: 0x0003772C
        public virtual void setSize(float value)
        {
            this.size = value;
            float num = this.size / ((float)this.vinilTL.width + (float)this.vinilTR.width * (1f - this.vinilTL.scaleX));
            this.vinilHighlightL.scaleX = (this.vinilHighlightL.scaleY = (this.vinilHighlightR.scaleY = num));
            this.vinilHighlightR.scaleX = -num;
            this.vinilBL.scaleX = (this.vinilBL.scaleY = (this.vinilBR.scaleY = num));
            this.vinilBR.scaleX = -num;
            this.vinilTL.scaleX = num;
            this.vinilTL.scaleY = -num;
            this.vinilTR.scaleX = (this.vinilTR.scaleY = -num);
            float num2 = ((num >= 0.4f) ? num : 0.4f);
            this.vinilStickerL.scaleX = (this.vinilStickerL.scaleY = (this.vinilStickerR.scaleY = num2));
            this.vinilStickerR.scaleX = -num2;
            float num3 = ((num >= 0.75f) ? num : 0.75f);
            this.vinilControllerL.scaleX = (this.vinilControllerL.scaleY = (this.vinilControllerR.scaleX = (this.vinilControllerR.scaleY = num3)));
            this.vinilActiveControllerL.scaleX = (this.vinilActiveControllerL.scaleY = (this.vinilActiveControllerR.scaleX = (this.vinilActiveControllerR.scaleY = num3)));
            this.vinilCenter.scaleX = 1f - (1f - this.vinilStickerL.scaleX) * 0.5f;
            this.vinilCenter.scaleY = this.vinilCenter.scaleX;
            this.sizeInPixels = (float)this.vinilHighlightL.width * this.vinilHighlightL.scaleX;
            this.updateChildPositions();
        }

        // Token: 0x0600072A RID: 1834 RVA: 0x0003974E File Offset: 0x0003794E
        public virtual void setHasOneHandle(bool value)
        {
            this.vinilControllerL.visible = !value;
        }

        // Token: 0x0600072B RID: 1835 RVA: 0x0003975F File Offset: 0x0003795F
        public virtual bool hasOneHandle()
        {
            return !this.vinilControllerL.visible;
        }

        // Token: 0x0600072C RID: 1836 RVA: 0x0003976F File Offset: 0x0003796F
        public virtual void setIsLeftControllerActive(bool value)
        {
            this.vinilActiveControllerL.visible = value;
        }

        // Token: 0x0600072D RID: 1837 RVA: 0x0003977D File Offset: 0x0003797D
        public virtual bool isLeftControllerActive()
        {
            return this.vinilActiveControllerL.visible;
        }

        // Token: 0x0600072E RID: 1838 RVA: 0x0003978A File Offset: 0x0003798A
        public virtual void setIsRightControllerActive(bool value)
        {
            this.vinilActiveControllerR.visible = value;
        }

        // Token: 0x0600072F RID: 1839 RVA: 0x00039798 File Offset: 0x00037998
        public virtual bool isRightControllerActive()
        {
            return this.vinilActiveControllerR.visible;
        }

        // Token: 0x06000730 RID: 1840 RVA: 0x000397A8 File Offset: 0x000379A8
        public virtual bool containsSameObjectWithAnotherCircle()
        {
            for (int i = 0; i < this.circlesArray.Count; i++)
            {
                RotatedCircle rotatedCircle = this.circlesArray[i];
                if (rotatedCircle != this && this.containsSameObjectWithCircle(rotatedCircle))
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000731 RID: 1841 RVA: 0x000397E8 File Offset: 0x000379E8
        public override NSObject init()
        {
            if (base.init() != null)
            {
                this.circlesArray = null;
                this.containedObjects = new List<BaseElement>();
                this.soundPlaying = -1;
                this.vinilStickerL = Image.Image_createWithResIDQuad(141, 2);
                this.vinilStickerL.anchor = 20;
                this.vinilStickerL.rotationCenterX = (float)this.vinilStickerL.width / 2f;
                this.vinilStickerR = Image.Image_createWithResIDQuad(141, 2);
                this.vinilStickerR.scaleX = -1f;
                this.vinilStickerR.anchor = 20;
                this.vinilStickerR.rotationCenterX = (float)this.vinilStickerR.width / 2f;
                this.vinilCenter = Image.Image_createWithResIDQuad(141, 3);
                this.vinilCenter.anchor = 18;
                this.vinilHighlightL = Image.Image_createWithResIDQuad(141, 1);
                this.vinilHighlightL.anchor = 12;
                this.vinilHighlightR = Image.Image_createWithResIDQuad(141, 1);
                this.vinilHighlightR.scaleX = -1f;
                this.vinilHighlightR.anchor = 9;
                this.vinilControllerL = Image.Image_createWithResIDQuad(141, 5);
                this.vinilControllerL.anchor = 18;
                this.vinilControllerL.rotation = 90f;
                this.vinilControllerR = Image.Image_createWithResIDQuad(141, 5);
                this.vinilControllerR.anchor = 18;
                this.vinilControllerR.rotation = -90f;
                this.vinilActiveControllerL = Image.Image_createWithResIDQuad(141, 4);
                this.vinilActiveControllerL.anchor = this.vinilControllerL.anchor;
                this.vinilActiveControllerL.rotation = this.vinilControllerL.rotation;
                this.vinilActiveControllerL.visible = false;
                this.vinilActiveControllerR = Image.Image_createWithResIDQuad(141, 4);
                this.vinilActiveControllerR.anchor = this.vinilControllerR.anchor;
                this.vinilActiveControllerR.rotation = this.vinilControllerR.rotation;
                this.vinilActiveControllerR.visible = false;
                this.vinilBL = Image.Image_createWithResIDQuad(141, 0);
                this.vinilBL.anchor = 12;
                this.vinilBR = Image.Image_createWithResIDQuad(141, 0);
                this.vinilBR.scaleX = -1f;
                this.vinilBR.anchor = 9;
                this.vinilTL = Image.Image_createWithResIDQuad(141, 0);
                this.vinilTL.scaleY = -1f;
                this.vinilTL.anchor = 36;
                this.vinilTR = Image.Image_createWithResIDQuad(141, 0);
                this.vinilTR.scaleX = (this.vinilTR.scaleY = -1f);
                this.vinilTR.anchor = 33;
                this.passColorToChilds = false;
                this.addChild(this.vinilActiveControllerL);
                this.addChild(this.vinilActiveControllerR);
                this.addChild(this.vinilControllerL);
                this.addChild(this.vinilControllerR);
            }
            return this;
        }

        // Token: 0x06000732 RID: 1842 RVA: 0x00039AEC File Offset: 0x00037CEC
        public virtual NSObject copy()
        {
            RotatedCircle rotatedCircle = (RotatedCircle)new RotatedCircle().init();
            rotatedCircle.x = this.x;
            rotatedCircle.y = this.y;
            rotatedCircle.rotation = this.rotation;
            rotatedCircle.circlesArray = this.circlesArray;
            rotatedCircle.containedObjects = this.containedObjects;
            rotatedCircle.operating = -1;
            rotatedCircle.handle1 = new Vector(rotatedCircle.x - this.size, rotatedCircle.y);
            rotatedCircle.handle2 = new Vector(rotatedCircle.x + this.size, rotatedCircle.y);
            rotatedCircle.handle1 = MathHelper.vectRotateAround(rotatedCircle.handle1, (double)MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation), rotatedCircle.x, rotatedCircle.y);
            rotatedCircle.handle2 = MathHelper.vectRotateAround(rotatedCircle.handle2, (double)MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation), rotatedCircle.x, rotatedCircle.y);
            rotatedCircle.setSize(this.size);
            rotatedCircle.setHasOneHandle(this.hasOneHandle());
            rotatedCircle.vinilControllerL.visible = false;
            rotatedCircle.vinilControllerR.visible = false;
            return rotatedCircle;
        }

        // Token: 0x06000733 RID: 1843 RVA: 0x00039C0C File Offset: 0x00037E0C
        public override void draw()
        {
            if (this.isRightControllerActive() || this.isLeftControllerActive())
            {
                OpenGL.glDisable(0);
                OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                GLDrawer.drawAntialiasedCurve2(this.x, this.y, this.sizeInPixels + 3f * Math.Abs(this.vinilTR.scaleX), 0f, 6.2831855f, 51, 2f, 1f * Math.Abs(this.vinilTR.scaleX), RGBAColor.whiteRGBA);
            }
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            this.vinilTL.color = (this.vinilTR.color = (this.vinilBL.color = (this.vinilBR.color = RGBAColor.solidOpaqueRGBA)));
            this.vinilTL.draw();
            this.vinilTR.draw();
            this.vinilBL.draw();
            this.vinilBR.draw();
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (this.isRightControllerActive() || this.isLeftControllerActive() || (double)this.color.a < 1.0)
            {
                RGBAColor whiteRGBA = RGBAColor.whiteRGBA;
                whiteRGBA.a = 1f - this.color.a;
                GLDrawer.drawAntialiasedCurve2(this.x, this.y, this.sizeInPixels + 1f, 0f, 6.2831855f, 51, 2f, 1f * Math.Abs(this.vinilTR.scaleX), whiteRGBA);
            }
            for (int i = 0; i < this.circlesArray.Count; i++)
            {
                RotatedCircle rotatedCircle = this.circlesArray[i];
                if (rotatedCircle != this && rotatedCircle.containsSameObjectWithAnotherCircle() && this.circlesArray.IndexOf(rotatedCircle) < this.circlesArray.IndexOf(this))
                {
                    GLDrawer.drawCircleIntersection(this.x, this.y, this.sizeInPixels, rotatedCircle.x, rotatedCircle.y, rotatedCircle.sizeInPixels, 51, 7f * rotatedCircle.vinilHighlightL.scaleX * 0.5f, this.CONTOUR_COLOR);
                }
            }
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            this.vinilHighlightL.color = this.color;
            this.vinilHighlightR.color = this.color;
            this.vinilHighlightL.draw();
            this.vinilHighlightR.draw();
            this.vinilStickerL.x = (this.vinilStickerR.x = this.x);
            this.vinilStickerL.y = (this.vinilStickerR.y = this.y);
            this.vinilStickerL.rotation = (this.vinilStickerR.rotation = this.rotation);
            this.vinilStickerL.draw();
            this.vinilStickerR.draw();
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            GLDrawer.drawAntialiasedCurve2(this.x, this.y, (float)this.vinilStickerL.width * this.vinilStickerL.scaleX, 0f, 6.2831855f, 51, 1f, this.vinilStickerL.scaleX * 1.5f, this.INNER_CIRCLE_COLOR1);
            GLDrawer.drawAntialiasedCurve2(this.x, this.y, (float)(this.vinilStickerL.width - 2) * this.vinilStickerL.scaleX, 0f, 6.2831855f, 51, 0f, this.vinilStickerL.scaleX * 1f, this.INNER_CIRCLE_COLOR2);
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            this.vinilControllerL.color = this.color;
            this.vinilControllerR.color = this.color;
            base.draw();
            this.vinilCenter.draw();
        }

        // Token: 0x06000734 RID: 1844 RVA: 0x0003A004 File Offset: 0x00038204
        public override void dealloc()
        {
            this.vinilCenter = null;
            this.vinilHighlightL = null;
            this.vinilHighlightR = null;
            this.vinilBL = null;
            this.vinilBR = null;
            this.vinilTL = null;
            this.vinilTR = null;
            this.vinilStickerL = null;
            this.vinilStickerR = null;
            this.containedObjects.Clear();
            this.containedObjects = null;
            base.dealloc();
        }

        // Token: 0x06000735 RID: 1845 RVA: 0x0003A068 File Offset: 0x00038268
        public virtual void updateChildPositions()
        {
            this.vinilCenter.x = this.x;
            this.vinilCenter.y = this.y;
            float num = (float)(this.vinilHighlightL.width / 2) * (1f - this.vinilHighlightL.scaleX);
            float num2 = (float)(this.vinilHighlightL.height / 2) * (1f - this.vinilHighlightL.scaleY);
            float num3 = (float)(this.vinilBL.width + 4) / 2f * (1f - this.vinilBL.scaleX);
            float num4 = (float)(this.vinilBL.height + 4) / 2f * (1f - this.vinilBL.scaleY);
            float num5 = ((Math.Abs(this.vinilControllerR.scaleX) < 1f) ? ((1f - Math.Abs(this.vinilControllerR.scaleX)) * 10f) : 0f);
            float num6 = ((Math.Abs(this.vinilTL.scaleX) < 0.45f) ? ((0.45f - Math.Abs(this.vinilTL.scaleX)) * 10f + 1f) : 0f);
            float num7 = Math.Abs((float)this.vinilBL.height * this.vinilBL.scaleY) - Math.Abs((float)this.vinilControllerR.height * 0.58f * this.vinilControllerR.scaleY / 2f) - num5 - num6;
            this.vinilHighlightL.x = this.x + num;
            this.vinilHighlightR.x = this.x - num;
            this.vinilHighlightL.y = (this.vinilHighlightR.y = this.y - num2);
            this.vinilBL.x = (this.vinilTL.x = this.x + num3);
            this.vinilBL.y = (this.vinilBR.y = this.y - num4);
            this.vinilBR.x = (this.vinilTR.x = this.x - num3);
            this.vinilTL.y = (this.vinilTR.y = this.y + num4);
            this.vinilControllerL.x = this.x - num7;
            this.vinilControllerR.x = this.x + num7;
            this.vinilControllerL.y = (this.vinilControllerR.y = this.y);
            this.vinilActiveControllerL.x = this.vinilControllerL.x;
            this.vinilActiveControllerL.y = this.vinilControllerL.y;
            this.vinilActiveControllerR.x = this.vinilControllerR.x;
            this.vinilActiveControllerR.y = this.vinilControllerR.y;
        }

        // Token: 0x06000736 RID: 1846 RVA: 0x0003A36C File Offset: 0x0003856C
        public virtual bool containsSameObjectWithCircle(RotatedCircle anotherCircle)
        {
            if (this.x == anotherCircle.x && this.y == anotherCircle.y && this.size == anotherCircle.size)
            {
                return false;
            }
            for (int i = 0; i < this.containedObjects.Count; i++)
            {
                GameObject gameObject = (GameObject)this.containedObjects[i];
                if (anotherCircle.containedObjects.IndexOf(gameObject) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x04000CAB RID: 3243
        private const float CONTROLLER_MIN_SCALE = 0.75f;

        // Token: 0x04000CAC RID: 3244
        private const float STICKER_MIN_SCALE = 0.4f;

        // Token: 0x04000CAD RID: 3245
        private const float CENTER_SCALE_FACTOR = 0.5f;

        // Token: 0x04000CAE RID: 3246
        private const float HUNDRED_PERCENT_SCALE_SIZE = 160f;

        // Token: 0x04000CAF RID: 3247
        private const int CIRCLE_VERTEX_COUNT = 50;

        // Token: 0x04000CB0 RID: 3248
        private const float INNER_CIRCLE_WIDTH = 15f;

        // Token: 0x04000CB1 RID: 3249
        private const float OUTER_CIRCLE_WIDTH = 7f;

        // Token: 0x04000CB2 RID: 3250
        private const float ACTIVE_CIRCLE_WIDTH = 2f;

        // Token: 0x04000CB3 RID: 3251
        private const float CONTROLLER_RATIO_PARAM = 0.58f;

        // Token: 0x04000CB4 RID: 3252
        private const float CIRCLE_CONTROLLER_OFFS = 10f;

        // Token: 0x04000CB5 RID: 3253
        private RGBAColor CIRCLE_COLOR1 = RGBAColor.MakeRGBA(0.306, 0.298, 0.454, 1.0);

        // Token: 0x04000CB6 RID: 3254
        private RGBAColor CIRCLE_COLOR2 = RGBAColor.MakeRGBA(0.239, 0.231, 0.356, 1.0);

        // Token: 0x04000CB7 RID: 3255
        private RGBAColor CIRCLE_COLOR3 = RGBAColor.MakeRGBA(0.29, 0.286, 0.419, 1.0);

        // Token: 0x04000CB8 RID: 3256
        private RGBAColor INNER_CIRCLE_COLOR1 = RGBAColor.MakeRGBA(0.6901960784313725, 0.4196078431372549, 0.07450980392156863, 1.0);

        // Token: 0x04000CB9 RID: 3257
        private RGBAColor INNER_CIRCLE_COLOR2 = RGBAColor.MakeRGBA(0.9294117647058824, 0.611764705882353, 0.07450980392156863, 1.0);

        // Token: 0x04000CBA RID: 3258
        private RGBAColor CONTOUR_COLOR = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.2);

        // Token: 0x04000CBB RID: 3259
        public float size;

        // Token: 0x04000CBC RID: 3260
        public float sizeInPixels;

        // Token: 0x04000CBD RID: 3261
        public int operating;

        // Token: 0x04000CBE RID: 3262
        public int soundPlaying;

        // Token: 0x04000CBF RID: 3263
        public Vector lastTouch;

        // Token: 0x04000CC0 RID: 3264
        public Vector handle1;

        // Token: 0x04000CC1 RID: 3265
        public Vector handle2;

        // Token: 0x04000CC2 RID: 3266
        public Vector inithanlde1;

        // Token: 0x04000CC3 RID: 3267
        public Vector inithanlde2;

        // Token: 0x04000CC4 RID: 3268
        public List<RotatedCircle> circlesArray;

        // Token: 0x04000CC5 RID: 3269
        public List<BaseElement> containedObjects;

        // Token: 0x04000CC6 RID: 3270
        public bool removeOnNextUpdate;

        // Token: 0x04000CC7 RID: 3271
        private Image vinilStickerL;

        // Token: 0x04000CC8 RID: 3272
        private Image vinilStickerR;

        // Token: 0x04000CC9 RID: 3273
        private Image vinilHighlightL;

        // Token: 0x04000CCA RID: 3274
        private Image vinilHighlightR;

        // Token: 0x04000CCB RID: 3275
        private Image vinilControllerL;

        // Token: 0x04000CCC RID: 3276
        private Image vinilControllerR;

        // Token: 0x04000CCD RID: 3277
        private Image vinilActiveControllerL;

        // Token: 0x04000CCE RID: 3278
        private Image vinilActiveControllerR;

        // Token: 0x04000CCF RID: 3279
        private Image vinilCenter;

        // Token: 0x04000CD0 RID: 3280
        private Image vinilTL;

        // Token: 0x04000CD1 RID: 3281
        private Image vinilTR;

        // Token: 0x04000CD2 RID: 3282
        private Image vinilBL;

        // Token: 0x04000CD3 RID: 3283
        private Image vinilBR;
    }
}
