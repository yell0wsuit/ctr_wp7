using System;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    internal class Image : BaseElement
    {
        // (get) Token: 0x060000A9 RID: 169 RVA: 0x00006A6B File Offset: 0x00004C6B
        public string _ResName
        {
            get
            {
                return texture != null ? texture._resName : "ERROR: texture == null";
            }
        }

        public static Vector getQuadSize(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return vect(texture2D.quadRects[quad].w, texture2D.quadRects[quad].h);
        }

        public static Vector getQuadOffset(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return texture2D.quadOffsets[quad];
        }

        public static Vector getQuadCenter(int textureID, int quad)
        {
            Texture2D texture2D = Application.getTexture(textureID);
            return vectAdd(texture2D.quadOffsets[quad], vect(ceil(texture2D.quadRects[quad].w / 2.0), ceil(texture2D.quadRects[quad].h / 2.0)));
        }

        public static Vector getRelativeQuadOffset(int textureID, int quadToCountFrom, int quad)
        {
            Vector quadOffset = getQuadOffset(textureID, quadToCountFrom);
            Vector quadOffset2 = getQuadOffset(textureID, quad);
            return vectSub(quadOffset2, quadOffset);
        }

        public static void setElementPositionWithQuadCenter(BaseElement e, int textureID, int quad)
        {
            Vector quadCenter = getQuadCenter(textureID, quad);
            e.x = quadCenter.x;
            e.y = quadCenter.y;
            e.anchor = 18;
        }

        public static void setElementPositionWithQuadOffset(BaseElement e, int textureID, int quad)
        {
            Vector quadOffset = getQuadOffset(textureID, quad);
            e.x = quadOffset.x;
            e.y = quadOffset.y;
        }

        public static void setElementPositionWithRelativeQuadOffset(BaseElement e, int textureID, int quadToCountFrom, int quad)
        {
            Vector relativeQuadOffset = getRelativeQuadOffset(textureID, quadToCountFrom, quad);
            e.x = relativeQuadOffset.x;
            e.y = relativeQuadOffset.y;
        }

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
            baseElement.height = (int)(image.height * 2f);
            _ = baseElement.addChild(image);
            _ = baseElement.addChild(image2);
            return baseElement;
        }

        public static Image Image_create(Texture2D t)
        {
            return new Image().initWithTexture(t);
        }

        public static Image Image_createWithResID(int r)
        {
            return Image_create(Application.getTexture(r));
        }

        public static Image Image_createWithResIDQuad(int r, int q)
        {
            Image image = Image_create(Application.getTexture(r));
            image.setDrawQuad(q);
            return image;
        }

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

        public virtual void setDrawFullImage()
        {
            quadToDraw = -1;
            width = texture._realWidth;
            height = texture._realHeight;
        }

        public virtual void setDrawQuad(int n)
        {
            quadToDraw = n;
            if (!restoreCutTransparency)
            {
                width = (int)texture.quadRects[n].w;
                height = (int)texture.quadRects[n].h;
            }
        }

        public virtual void doRestoreCutTransparency()
        {
            if (texture.preCutSize.x != vectUndefined.x)
            {
                restoreCutTransparency = true;
                width = (int)texture.preCutSize.x;
                height = (int)texture.preCutSize.y;
            }
        }

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

        public virtual BaseElement createFromXML(XMLNode xml)
        {
            throw new NotImplementedException();
        }

        public override void dealloc()
        {
            NSREL(texture);
            base.dealloc();
        }

        public const string ACTION_SET_DRAWQUAD = "ACTION_SET_DRAWQUAD";

        public Texture2D texture;

        public bool restoreCutTransparency;

        public int quadToDraw;
    }
}
