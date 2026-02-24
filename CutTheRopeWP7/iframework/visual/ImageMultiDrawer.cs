using System;

using ctr_wp7.ios;

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
            this.image = (Image)NSObject.NSRET(i);
            this.numberOfQuadsToDraw = -1;
            this.totalQuads = n;
            this.texCoordinates = new Quad2D[this.totalQuads];
            this.vertices = new Quad3D[this.totalQuads];
            this.indices = new short[this.totalQuads * 6];
            this.initIndices();
            return this;
        }

        // Token: 0x06000713 RID: 1811 RVA: 0x00039076 File Offset: 0x00037276
        private void freeWithCheck()
        {
            this.texCoordinates = null;
            this.vertices = null;
            this.indices = null;
        }

        // Token: 0x06000714 RID: 1812 RVA: 0x0003908D File Offset: 0x0003728D
        public override void dealloc()
        {
            this.freeWithCheck();
            this.image = null;
            base.dealloc();
        }

        // Token: 0x06000715 RID: 1813 RVA: 0x000390A4 File Offset: 0x000372A4
        private void initIndices()
        {
            for (int i = 0; i < this.totalQuads; i++)
            {
                this.indices[i * 6] = (short)(i * 4);
                this.indices[i * 6 + 1] = (short)(i * 4 + 1);
                this.indices[i * 6 + 2] = (short)(i * 4 + 2);
                this.indices[i * 6 + 3] = (short)(i * 4 + 3);
                this.indices[i * 6 + 4] = (short)(i * 4 + 2);
                this.indices[i * 6 + 5] = (short)(i * 4 + 1);
            }
        }

        // Token: 0x06000716 RID: 1814 RVA: 0x0003912A File Offset: 0x0003732A
        public void setTextureQuadatVertexQuadatIndex(Quad2D qt, Quad3D qv, int n)
        {
            if (n >= this.totalQuads)
            {
                this.resizeCapacity(n + 1);
            }
            this.texCoordinates[n] = qt;
            this.vertices[n] = qv;
        }

        // Token: 0x06000717 RID: 1815 RVA: 0x00039164 File Offset: 0x00037364
        public void mapTextureQuadAtXYatIndex(int q, float dx, float dy, int n)
        {
            if (n >= this.totalQuads)
            {
                this.resizeCapacity(n + 1);
            }
            this.texCoordinates[n] = this.image.texture.quads[q];
            this.vertices[n] = Quad3D.MakeQuad3D((double)(dx + this.image.texture.quadOffsets[q].x), (double)(dy + this.image.texture.quadOffsets[q].y), 0.0, (double)this.image.texture.quadRects[q].w, (double)this.image.texture.quadRects[q].h);
        }

        // Token: 0x06000718 RID: 1816 RVA: 0x00039248 File Offset: 0x00037448
        private void drawNumberOfQuads(int n)
        {
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(this.image.texture.name());
            OpenGL.glVertexPointer(3, 5, 0, FrameworkTypes.toFloatArray(this.vertices));
            OpenGL.glTexCoordPointer(2, 5, 0, FrameworkTypes.toFloatArray(this.texCoordinates));
            OpenGL.glDrawElements(7, n * 6, this.indices);
        }

        // Token: 0x06000719 RID: 1817 RVA: 0x000392A5 File Offset: 0x000374A5
        private void drawNumberOfQuadsStartingFrom(int n, int s)
        {
            throw new NotImplementedException();
        }

        // Token: 0x0600071A RID: 1818 RVA: 0x000392AC File Offset: 0x000374AC
        public void optimize(VertexPositionNormalTexture[] v)
        {
            if (v != null && this.verticesOptimized == null)
            {
                this.verticesOptimized = v;
            }
        }

        // Token: 0x0600071B RID: 1819 RVA: 0x000392C0 File Offset: 0x000374C0
        public void drawAllQuads()
        {
            if (this.verticesOptimized == null)
            {
                this.drawNumberOfQuads(this.totalQuads);
                return;
            }
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(this.image.texture.name());
            OpenGL.Optimized_DrawTriangleList(this.verticesOptimized, this.indices);
        }

        // Token: 0x0600071C RID: 1820 RVA: 0x00039310 File Offset: 0x00037510
        public override void draw()
        {
            this.preDraw();
            OpenGL.glTranslatef(this.drawX, this.drawY, 0f);
            if (this.numberOfQuadsToDraw == -1)
            {
                this.drawAllQuads();
            }
            else if (this.numberOfQuadsToDraw > 0)
            {
                this.drawNumberOfQuads(this.numberOfQuadsToDraw);
            }
            OpenGL.glTranslatef(-this.drawX, -this.drawY, 0f);
            this.postDraw();
        }

        // Token: 0x0600071D RID: 1821 RVA: 0x00039380 File Offset: 0x00037580
        private void resizeCapacity(int n)
        {
            if (n == this.totalQuads)
            {
                return;
            }
            this.totalQuads = n;
            this.texCoordinates = new Quad2D[this.totalQuads];
            this.vertices = new Quad3D[this.totalQuads];
            this.indices = new short[this.totalQuads * 6];
            if (this.texCoordinates == null || this.vertices == null || this.indices == null)
            {
                this.freeWithCheck();
            }
            this.initIndices();
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
