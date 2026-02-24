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
            size = value;
            float num = size / (vinilTL.width + (vinilTR.width * (1f - vinilTL.scaleX)));
            vinilHighlightL.scaleX = vinilHighlightL.scaleY = vinilHighlightR.scaleY = num;
            vinilHighlightR.scaleX = -num;
            vinilBL.scaleX = vinilBL.scaleY = vinilBR.scaleY = num;
            vinilBR.scaleX = -num;
            vinilTL.scaleX = num;
            vinilTL.scaleY = -num;
            vinilTR.scaleX = vinilTR.scaleY = -num;
            float num2 = (num >= 0.4f) ? num : 0.4f;
            vinilStickerL.scaleX = vinilStickerL.scaleY = vinilStickerR.scaleY = num2;
            vinilStickerR.scaleX = -num2;
            float num3 = (num >= 0.75f) ? num : 0.75f;
            vinilControllerL.scaleX = vinilControllerL.scaleY = vinilControllerR.scaleX = vinilControllerR.scaleY = num3;
            vinilActiveControllerL.scaleX = vinilActiveControllerL.scaleY = vinilActiveControllerR.scaleX = vinilActiveControllerR.scaleY = num3;
            vinilCenter.scaleX = 1f - ((1f - vinilStickerL.scaleX) * 0.5f);
            vinilCenter.scaleY = vinilCenter.scaleX;
            sizeInPixels = vinilHighlightL.width * vinilHighlightL.scaleX;
            updateChildPositions();
        }

        // Token: 0x0600072A RID: 1834 RVA: 0x0003974E File Offset: 0x0003794E
        public virtual void setHasOneHandle(bool value)
        {
            vinilControllerL.visible = !value;
        }

        // Token: 0x0600072B RID: 1835 RVA: 0x0003975F File Offset: 0x0003795F
        public virtual bool hasOneHandle()
        {
            return !vinilControllerL.visible;
        }

        // Token: 0x0600072C RID: 1836 RVA: 0x0003976F File Offset: 0x0003796F
        public virtual void setIsLeftControllerActive(bool value)
        {
            vinilActiveControllerL.visible = value;
        }

        // Token: 0x0600072D RID: 1837 RVA: 0x0003977D File Offset: 0x0003797D
        public virtual bool isLeftControllerActive()
        {
            return vinilActiveControllerL.visible;
        }

        // Token: 0x0600072E RID: 1838 RVA: 0x0003978A File Offset: 0x0003798A
        public virtual void setIsRightControllerActive(bool value)
        {
            vinilActiveControllerR.visible = value;
        }

        // Token: 0x0600072F RID: 1839 RVA: 0x00039798 File Offset: 0x00037998
        public virtual bool isRightControllerActive()
        {
            return vinilActiveControllerR.visible;
        }

        // Token: 0x06000730 RID: 1840 RVA: 0x000397A8 File Offset: 0x000379A8
        public virtual bool containsSameObjectWithAnotherCircle()
        {
            for (int i = 0; i < circlesArray.Count; i++)
            {
                RotatedCircle rotatedCircle = circlesArray[i];
                if (rotatedCircle != this && containsSameObjectWithCircle(rotatedCircle))
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
                circlesArray = null;
                containedObjects = [];
                soundPlaying = -1;
                vinilStickerL = Image.Image_createWithResIDQuad(141, 2);
                vinilStickerL.anchor = 20;
                vinilStickerL.rotationCenterX = vinilStickerL.width / 2f;
                vinilStickerR = Image.Image_createWithResIDQuad(141, 2);
                vinilStickerR.scaleX = -1f;
                vinilStickerR.anchor = 20;
                vinilStickerR.rotationCenterX = vinilStickerR.width / 2f;
                vinilCenter = Image.Image_createWithResIDQuad(141, 3);
                vinilCenter.anchor = 18;
                vinilHighlightL = Image.Image_createWithResIDQuad(141, 1);
                vinilHighlightL.anchor = 12;
                vinilHighlightR = Image.Image_createWithResIDQuad(141, 1);
                vinilHighlightR.scaleX = -1f;
                vinilHighlightR.anchor = 9;
                vinilControllerL = Image.Image_createWithResIDQuad(141, 5);
                vinilControllerL.anchor = 18;
                vinilControllerL.rotation = 90f;
                vinilControllerR = Image.Image_createWithResIDQuad(141, 5);
                vinilControllerR.anchor = 18;
                vinilControllerR.rotation = -90f;
                vinilActiveControllerL = Image.Image_createWithResIDQuad(141, 4);
                vinilActiveControllerL.anchor = vinilControllerL.anchor;
                vinilActiveControllerL.rotation = vinilControllerL.rotation;
                vinilActiveControllerL.visible = false;
                vinilActiveControllerR = Image.Image_createWithResIDQuad(141, 4);
                vinilActiveControllerR.anchor = vinilControllerR.anchor;
                vinilActiveControllerR.rotation = vinilControllerR.rotation;
                vinilActiveControllerR.visible = false;
                vinilBL = Image.Image_createWithResIDQuad(141, 0);
                vinilBL.anchor = 12;
                vinilBR = Image.Image_createWithResIDQuad(141, 0);
                vinilBR.scaleX = -1f;
                vinilBR.anchor = 9;
                vinilTL = Image.Image_createWithResIDQuad(141, 0);
                vinilTL.scaleY = -1f;
                vinilTL.anchor = 36;
                vinilTR = Image.Image_createWithResIDQuad(141, 0);
                vinilTR.scaleX = vinilTR.scaleY = -1f;
                vinilTR.anchor = 33;
                passColorToChilds = false;
                _ = addChild(vinilActiveControllerL);
                _ = addChild(vinilActiveControllerR);
                _ = addChild(vinilControllerL);
                _ = addChild(vinilControllerR);
            }
            return this;
        }

        // Token: 0x06000732 RID: 1842 RVA: 0x00039AEC File Offset: 0x00037CEC
        public virtual NSObject copy()
        {
            RotatedCircle rotatedCircle = (RotatedCircle)new RotatedCircle().init();
            rotatedCircle.x = x;
            rotatedCircle.y = y;
            rotatedCircle.rotation = rotation;
            rotatedCircle.circlesArray = circlesArray;
            rotatedCircle.containedObjects = containedObjects;
            rotatedCircle.operating = -1;
            rotatedCircle.handle1 = new Vector(rotatedCircle.x - size, rotatedCircle.y);
            rotatedCircle.handle2 = new Vector(rotatedCircle.x + size, rotatedCircle.y);
            rotatedCircle.handle1 = vectRotateAround(rotatedCircle.handle1, (double)DEGREES_TO_RADIANS(rotatedCircle.rotation), rotatedCircle.x, rotatedCircle.y);
            rotatedCircle.handle2 = vectRotateAround(rotatedCircle.handle2, (double)DEGREES_TO_RADIANS(rotatedCircle.rotation), rotatedCircle.x, rotatedCircle.y);
            rotatedCircle.setSize(size);
            rotatedCircle.setHasOneHandle(hasOneHandle());
            rotatedCircle.vinilControllerL.visible = false;
            rotatedCircle.vinilControllerR.visible = false;
            return rotatedCircle;
        }

        // Token: 0x06000733 RID: 1843 RVA: 0x00039C0C File Offset: 0x00037E0C
        public override void draw()
        {
            if (isRightControllerActive() || isLeftControllerActive())
            {
                OpenGL.glDisable(0);
                OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                GLDrawer.drawAntialiasedCurve2(x, y, sizeInPixels + (3f * Math.Abs(vinilTR.scaleX)), 0f, 6.2831855f, 51, 2f, 1f * Math.Abs(vinilTR.scaleX), RGBAColor.whiteRGBA);
            }
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            vinilTL.color = vinilTR.color = vinilBL.color = vinilBR.color = RGBAColor.solidOpaqueRGBA;
            vinilTL.draw();
            vinilTR.draw();
            vinilBL.draw();
            vinilBR.draw();
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (isRightControllerActive() || isLeftControllerActive() || color.a < 1.0)
            {
                RGBAColor whiteRGBA = RGBAColor.whiteRGBA;
                whiteRGBA.a = 1f - color.a;
                GLDrawer.drawAntialiasedCurve2(x, y, sizeInPixels + 1f, 0f, 6.2831855f, 51, 2f, 1f * Math.Abs(vinilTR.scaleX), whiteRGBA);
            }
            for (int i = 0; i < circlesArray.Count; i++)
            {
                RotatedCircle rotatedCircle = circlesArray[i];
                if (rotatedCircle != this && rotatedCircle.containsSameObjectWithAnotherCircle() && circlesArray.IndexOf(rotatedCircle) < circlesArray.IndexOf(this))
                {
                    GLDrawer.drawCircleIntersection(x, y, sizeInPixels, rotatedCircle.x, rotatedCircle.y, rotatedCircle.sizeInPixels, 51, 7f * rotatedCircle.vinilHighlightL.scaleX * 0.5f, CONTOUR_COLOR);
                }
            }
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            vinilHighlightL.color = color;
            vinilHighlightR.color = color;
            vinilHighlightL.draw();
            vinilHighlightR.draw();
            vinilStickerL.x = vinilStickerR.x = x;
            vinilStickerL.y = vinilStickerR.y = y;
            vinilStickerL.rotation = vinilStickerR.rotation = rotation;
            vinilStickerL.draw();
            vinilStickerR.draw();
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            GLDrawer.drawAntialiasedCurve2(x, y, vinilStickerL.width * vinilStickerL.scaleX, 0f, 6.2831855f, 51, 1f, vinilStickerL.scaleX * 1.5f, INNER_CIRCLE_COLOR1);
            GLDrawer.drawAntialiasedCurve2(x, y, (vinilStickerL.width - 2) * vinilStickerL.scaleX, 0f, 6.2831855f, 51, 0f, vinilStickerL.scaleX * 1f, INNER_CIRCLE_COLOR2);
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            vinilControllerL.color = color;
            vinilControllerR.color = color;
            base.draw();
            vinilCenter.draw();
        }

        // Token: 0x06000734 RID: 1844 RVA: 0x0003A004 File Offset: 0x00038204
        public override void dealloc()
        {
            vinilCenter = null;
            vinilHighlightL = null;
            vinilHighlightR = null;
            vinilBL = null;
            vinilBR = null;
            vinilTL = null;
            vinilTR = null;
            vinilStickerL = null;
            vinilStickerR = null;
            containedObjects.Clear();
            containedObjects = null;
            base.dealloc();
        }

        // Token: 0x06000735 RID: 1845 RVA: 0x0003A068 File Offset: 0x00038268
        public virtual void updateChildPositions()
        {
            vinilCenter.x = x;
            vinilCenter.y = y;
            float num = vinilHighlightL.width / 2 * (1f - vinilHighlightL.scaleX);
            float num2 = vinilHighlightL.height / 2 * (1f - vinilHighlightL.scaleY);
            float num3 = (vinilBL.width + 4) / 2f * (1f - vinilBL.scaleX);
            float num4 = (vinilBL.height + 4) / 2f * (1f - vinilBL.scaleY);
            float num5 = (Math.Abs(vinilControllerR.scaleX) < 1f) ? ((1f - Math.Abs(vinilControllerR.scaleX)) * 10f) : 0f;
            float num6 = (Math.Abs(vinilTL.scaleX) < 0.45f) ? (((0.45f - Math.Abs(vinilTL.scaleX)) * 10f) + 1f) : 0f;
            float num7 = Math.Abs(vinilBL.height * vinilBL.scaleY) - Math.Abs(vinilControllerR.height * 0.58f * vinilControllerR.scaleY / 2f) - num5 - num6;
            vinilHighlightL.x = x + num;
            vinilHighlightR.x = x - num;
            vinilHighlightL.y = vinilHighlightR.y = y - num2;
            vinilBL.x = vinilTL.x = x + num3;
            vinilBL.y = vinilBR.y = y - num4;
            vinilBR.x = vinilTR.x = x - num3;
            vinilTL.y = vinilTR.y = y + num4;
            vinilControllerL.x = x - num7;
            vinilControllerR.x = x + num7;
            vinilControllerL.y = vinilControllerR.y = y;
            vinilActiveControllerL.x = vinilControllerL.x;
            vinilActiveControllerL.y = vinilControllerL.y;
            vinilActiveControllerR.x = vinilControllerR.x;
            vinilActiveControllerR.y = vinilControllerR.y;
        }

        // Token: 0x06000736 RID: 1846 RVA: 0x0003A36C File Offset: 0x0003856C
        public virtual bool containsSameObjectWithCircle(RotatedCircle anotherCircle)
        {
            if (x == anotherCircle.x && y == anotherCircle.y && size == anotherCircle.size)
            {
                return false;
            }
            for (int i = 0; i < containedObjects.Count; i++)
            {
                GameObject gameObject = (GameObject)containedObjects[i];
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
