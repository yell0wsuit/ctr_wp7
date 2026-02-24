using System;

using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000EC RID: 236
    internal class ImageMultiDrawer : BaseElement
    {
        // Token: 0x06000712 RID: 1810 RVA: 0x00039004 File Offset: 0x00037204
        public virtual ImageMultiDrawer initWithImageandCapacity(Image i, int n)
        {
            if (base.init() == null)
            {
                return null;
            }
            image = (Image)NSRET(i);
            numberOfQuadsToDraw = -1;
            totalQuads = n;
            texCoordinates = new Quad2D[totalQuads];
            vertices = new Quad3D[totalQuads];
            indices = new short[totalQuads * 6];
            initIndices();
            return this;
        }

        // Token: 0x06000713 RID: 1811 RVA: 0x00039076 File Offset: 0x00037276
        private void freeWithCheck()
        {
            texCoordinates = null;
            vertices = null;
            indices = null;
        }

        // Token: 0x06000714 RID: 1812 RVA: 0x0003908D File Offset: 0x0003728D
        public override void dealloc()
        {
            freeWithCheck();
            image = null;
            base.dealloc();
        }

        // Token: 0x06000715 RID: 1813 RVA: 0x000390A4 File Offset: 0x000372A4
        private void initIndices()
        {
            for (int i = 0; i < totalQuads; i++)
            {
                indices[i * 6] = (short)(i * 4);
                indices[i * 6 + 1] = (short)(i * 4 + 1);
                indices[i * 6 + 2] = (short)(i * 4 + 2);
                indices[i * 6 + 3] = (short)(i * 4 + 3);
                indices[i * 6 + 4] = (short)(i * 4 + 2);
                indices[i * 6 + 5] = (short)(i * 4 + 1);
            }
        }

        // Token: 0x06000716 RID: 1814 RVA: 0x0003912A File Offset: 0x0003732A
        public void setTextureQuadatVertexQuadatIndex(Quad2D qt, Quad3D qv, int n)
        {
            if (n >= totalQuads)
            {
                resizeCapacity(n + 1);
            }
            texCoordinates[n] = qt;
            vertices[n] = qv;
        }

        // Token: 0x06000717 RID: 1815 RVA: 0x00039164 File Offset: 0x00037364
        public void mapTextureQuadAtXYatIndex(int q, float dx, float dy, int n)
        {
            if (n >= totalQuads)
            {
                resizeCapacity(n + 1);
            }
            texCoordinates[n] = image.texture.quads[q];
            vertices[n] = Quad3D.MakeQuad3D((double)(dx + image.texture.quadOffsets[q].x), (double)(dy + image.texture.quadOffsets[q].y), 0.0, (double)image.texture.quadRects[q].w, (double)image.texture.quadRects[q].h);
        }

        // Token: 0x06000718 RID: 1816 RVA: 0x00039248 File Offset: 0x00037448
        private void drawNumberOfQuads(int n)
        {
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(image.texture.name());
            OpenGL.glVertexPointer(3, 5, 0, toFloatArray(vertices));
            OpenGL.glTexCoordPointer(2, 5, 0, toFloatArray(texCoordinates));
            OpenGL.glDrawElements(7, n * 6, indices);
        }

        // Token: 0x06000719 RID: 1817 RVA: 0x000392A5 File Offset: 0x000374A5
        private void drawNumberOfQuadsStartingFrom(int n, int s)
        {
            throw new NotImplementedException();
        }

        // Token: 0x0600071A RID: 1818 RVA: 0x000392AC File Offset: 0x000374AC
        public void optimize(VertexPositionNormalTexture[] v)
        {
            if (v != null && verticesOptimized == null)
            {
                verticesOptimized = v;
            }
        }

        // Token: 0x0600071B RID: 1819 RVA: 0x000392C0 File Offset: 0x000374C0
        public void drawAllQuads()
        {
            if (verticesOptimized == null)
            {
                drawNumberOfQuads(totalQuads);
                return;
            }
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(image.texture.name());
            OpenGL.Optimized_DrawTriangleList(verticesOptimized, indices);
        }

        // Token: 0x0600071C RID: 1820 RVA: 0x00039310 File Offset: 0x00037510
        public override void draw()
        {
            preDraw();
            OpenGL.glTranslatef(drawX, drawY, 0f);
            if (numberOfQuadsToDraw == -1)
            {
                drawAllQuads();
            }
            else if (numberOfQuadsToDraw > 0)
            {
                drawNumberOfQuads(numberOfQuadsToDraw);
            }
            OpenGL.glTranslatef(-drawX, -drawY, 0f);
            postDraw();
        }

        // Token: 0x0600071D RID: 1821 RVA: 0x00039380 File Offset: 0x00037580
        private void resizeCapacity(int n)
        {
            if (n == totalQuads)
            {
                return;
            }
            totalQuads = n;
            texCoordinates = new Quad2D[totalQuads];
            vertices = new Quad3D[totalQuads];
            indices = new short[totalQuads * 6];
            if (texCoordinates == null || vertices == null || indices == null)
            {
                freeWithCheck();
            }
            initIndices();
        }

        // Token: 0x04000CA2 RID: 3234
        public Image image;

        // Token: 0x04000CA3 RID: 3235
        public int totalQuads;

        // Token: 0x04000CA4 RID: 3236
        public Quad2D[] texCoordinates;

        // Token: 0x04000CA5 RID: 3237
        public Quad3D[] vertices;

        // Token: 0x04000CA6 RID: 3238
        public short[] indices;

        // Token: 0x04000CA7 RID: 3239
        public int numberOfQuadsToDraw;

        // Token: 0x04000CA8 RID: 3240
        private VertexPositionNormalTexture[] verticesOptimized;
    }
}
