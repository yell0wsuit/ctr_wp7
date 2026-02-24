using System;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200000A RID: 10
    internal class Image : BaseElement
    {
        // Token: 0x17000004 RID: 4
        // (get) Token: 0x060000A9 RID: 169 RVA: 0x00006A6B File Offset: 0x00004C6B
        public string _ResName
        {
            get
            {
                if (texture != null)
                {
                    return texture._resName;
                }
                return "ERROR: texture == null";
            }
        }

        // Token: 0x060000AA RID: 170 RVA: 0x00006A88 File Offset: 0x00004C88
        public static Vector getQuadSize(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return vect(texture2D.quadRects[quad].w, texture2D.quadRects[quad].h);
        }

        // Token: 0x060000AB RID: 171 RVA: 0x00006AC4 File Offset: 0x00004CC4
        public static Vector getQuadOffset(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return texture2D.quadOffsets[quad];
        }

        // Token: 0x060000AC RID: 172 RVA: 0x00006AEC File Offset: 0x00004CEC
        public static Vector getQuadCenter(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return vectAdd(texture2D.quadOffsets[quad], vect(ceil((double)texture2D.quadRects[quad].w / 2.0), ceil((double)texture2D.quadRects[quad].h / 2.0)));
        }

        // Token: 0x060000AD RID: 173 RVA: 0x00006B60 File Offset: 0x00004D60
        public static Vector getRelativeQuadOffset(int textureID, int quadToCountFrom, int quad)
        {
            Vector quadOffset = getQuadOffset(textureID, quadToCountFrom);
            Vector quadOffset2 = getQuadOffset(textureID, quad);
            return vectSub(quadOffset2, quadOffset);
        }

        // Token: 0x060000AE RID: 174 RVA: 0x00006B84 File Offset: 0x00004D84
        public static void setElementPositionWithQuadCenter(BaseElement e, int textureID, int quad)
        {
            Vector quadCenter = getQuadCenter(textureID, quad);
            e.x = quadCenter.x;
            e.y = quadCenter.y;
            e.anchor = 18;
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00006BBC File Offset: 0x00004DBC
        public static void setElementPositionWithQuadOffset(BaseElement e, int textureID, int quad)
        {
            Vector quadOffset = getQuadOffset(textureID, quad);
            e.x = quadOffset.x;
            e.y = quadOffset.y;
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x00006BEC File Offset: 0x00004DEC
        public static void setElementPositionWithRelativeQuadOffset(BaseElement e, int textureID, int quadToCountFrom, int quad)
        {
            Vector relativeQuadOffset = getRelativeQuadOffset(textureID, quadToCountFrom, quad);
            e.x = relativeQuadOffset.x;
            e.y = relativeQuadOffset.y;
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x00006C1C File Offset: 0x00004E1C
        public static BaseElement createElementWithLeftPart(int textureID, int quad)
        {
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            Image image = Image_createWithResIDQuad(textureID, quad);
            image.parentAnchor = 10;
            image.anchor = 12;
            Image image2 = Image_createWithResIDQuad(textureID, quad);
            image2.parentAnchor = 10;
            image2.anchor = 9;
            image2.scaleX = -1f;
            baseElement.width = image.width * 2;
            baseElement.height = image.height;
            _ = baseElement.addChild(image);
            _ = baseElement.addChild(image2);
            return baseElement;
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00006CA0 File Offset: 0x00004EA0
        public static BaseElement createElementWithBottomPart(int textureID, int quad)
        {
            BaseElement baseElement = (BaseElement)new BaseElement().init();
            Image image = Image_createWithResIDQuad(textureID, quad);
            image.parentAnchor = 10;
            image.anchor = 10;
            image.scaleY = -1f;
            Image image2 = Image_createWithResIDQuad(textureID, quad);
            image2.parentAnchor = 34;
            image2.anchor = 34;
            baseElement.width = image.width;
            baseElement.height = (int)((float)image.height * 2f);
            _ = baseElement.addChild(image);
            _ = baseElement.addChild(image2);
            return baseElement;
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x00006D29 File Offset: 0x00004F29
        public static Image Image_create(Texture2D t)
        {
            return new Image().initWithTexture(t);
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x00006D36 File Offset: 0x00004F36
        public static Image Image_createWithResID(int r)
        {
            return Image_create(Application.getTexture(r));
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x00006D44 File Offset: 0x00004F44
        public static Image Image_createWithResIDQuad(int r, int q)
        {
            Image image = Image_create(Application.getTexture(r));
            image.setDrawQuad(q);
            return image;
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00006D68 File Offset: 0x00004F68
        public virtual Image initWithTexture(Texture2D t)
        {
            if (base.init() != null)
            {
                texture = t;
                _ = NSRET(texture);
                restoreCutTransparency = false;
                if (texture.quadsCount > 0)
                {
                    setDrawQuad(0);
                }
                else
                {
                    setDrawFullImage();
                }
            }
            return this;
        }

        // Token: 0x060000B7 RID: 183 RVA: 0x00006DB5 File Offset: 0x00004FB5
        public virtual void setDrawFullImage()
        {
            quadToDraw = -1;
            width = texture._realWidth;
            height = texture._realHeight;
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x00006DE0 File Offset: 0x00004FE0
        public virtual void setDrawQuad(int n)
        {
            quadToDraw = n;
            if (!restoreCutTransparency)
            {
                width = (int)texture.quadRects[n].w;
                height = (int)texture.quadRects[n].h;
            }
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x00006E38 File Offset: 0x00005038
        public virtual void doRestoreCutTransparency()
        {
            if (texture.preCutSize.x != vectUndefined.x)
            {
                restoreCutTransparency = true;
                width = (int)texture.preCutSize.x;
                height = (int)texture.preCutSize.y;
            }
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00006E96 File Offset: 0x00005096
        public override void draw()
        {
            preDraw();
            if (quadToDraw == -1)
            {
                GLDrawer.drawImage(texture, drawX, drawY);
            }
            else
            {
                drawQuad(quadToDraw);
            }
            postDraw();
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00006ED4 File Offset: 0x000050D4
        public virtual void drawQuad(int n)
        {
            float w = texture.quadRects[n].w;
            float h = texture.quadRects[n].h;
            float num = drawX;
            float num2 = drawY;
            if (restoreCutTransparency)
            {
                num += texture.quadOffsets[n].x;
                num2 += texture.quadOffsets[n].y;
            }
            float[] array =
            [
                num,
                num2,
                num + w,
                num2,
                num,
                num2 + h,
                num + w,
                num2 + h
            ];
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(texture.name());
            OpenGL.glVertexPointer(2, 5, 0, array);
            OpenGL.glTexCoordPointer(2, 5, 0, texture.quads[n].toFloatArray());
            OpenGL.glDrawArrays(8, 0, 4);
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00006FD9 File Offset: 0x000051D9
        public override bool handleAction(ActionData a)
        {
            if (base.handleAction(a))
            {
                return true;
            }
            if (a.actionName == "ACTION_SET_DRAWQUAD")
            {
                setDrawQuad(a.actionParam);
                return true;
            }
            return false;
        }

        // Token: 0x060000BD RID: 189 RVA: 0x00007009 File Offset: 0x00005209
        public virtual BaseElement createFromXML(XMLNode xml)
        {
            throw new NotImplementedException();
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00007010 File Offset: 0x00005210
        public override void dealloc()
        {
            NSREL(texture);
            base.dealloc();
        }

        // Token: 0x0400071D RID: 1821
        public const string ACTION_SET_DRAWQUAD = "ACTION_SET_DRAWQUAD";

        // Token: 0x0400071E RID: 1822
        public Texture2D texture;

        // Token: 0x0400071F RID: 1823
        public bool restoreCutTransparency;

        // Token: 0x04000720 RID: 1824
        public int quadToDraw;
    }
}
