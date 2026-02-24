using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000036 RID: 54
    internal class TileMap : BaseElement
    {
        // Token: 0x060001FD RID: 509 RVA: 0x0000D13C File Offset: 0x0000B33C
        public override void draw()
        {
            int count = this.drawers.Count;
            for (int i = 0; i < count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = this.drawers[i];
                if (imageMultiDrawer != null)
                {
                    imageMultiDrawer.draw();
                }
            }
        }

        // Token: 0x060001FE RID: 510 RVA: 0x0000D177 File Offset: 0x0000B377
        public override void dealloc()
        {
            this.matrix = null;
            this.drawers.Clear();
            this.drawers = null;
            this.tiles.Clear();
            this.tiles = null;
            base.dealloc();
        }

        // Token: 0x060001FF RID: 511 RVA: 0x0000D1AC File Offset: 0x0000B3AC
        public virtual TileMap initWithRowsColumns(int r, int c)
        {
            if (base.init() != null)
            {
                this.rows = r;
                this.columns = c;
                this.cameraViewWidth = (int)FrameworkTypes.SCREEN_WIDTH_EXPANDED;
                this.cameraViewHeight = (int)FrameworkTypes.SCREEN_HEIGHT_EXPANDED;
                this.parallaxRatio = 1f;
                this.drawers = new List<ImageMultiDrawer>();
                this.tiles = new Dictionary<int, TileEntry>();
                this.matrix = new int[this.columns, this.rows];
                for (int i = 0; i < this.columns; i++)
                {
                    for (int j = 0; j < this.rows; j++)
                    {
                        this.matrix[i, j] = -1;
                    }
                }
                this.repeatedVertically = TileMap.Repeat.REPEAT_NONE;
                this.repeatedHorizontally = TileMap.Repeat.REPEAT_NONE;
                this.horizontalRandom = false;
                this.verticalRandom = false;
                this.restoreTileTransparency = true;
                this.randomSeed = MathHelper.RND_RANGE(1000, 2000);
            }
            return this;
        }

        // Token: 0x06000200 RID: 512 RVA: 0x0000D28C File Offset: 0x0000B48C
        public virtual void addTileQuadwithID(Texture2D t, int q, int ti)
        {
            if (q == -1)
            {
                this.tileWidth = t._realWidth;
                this.tileHeight = t._realHeight;
            }
            else
            {
                this.tileWidth = (int)t.quadRects[q].w;
                this.tileHeight = (int)t.quadRects[q].h;
            }
            this.updateVars();
            int num = -1;
            for (int i = 0; i < this.drawers.Count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = this.drawers[i];
                if (imageMultiDrawer.image.texture == t)
                {
                    num = i;
                }
                if (imageMultiDrawer.image.texture._realWidth == this.tileWidth)
                {
                    int realHeight = imageMultiDrawer.image.texture._realHeight;
                    int num2 = this.tileHeight;
                }
            }
            if (num == -1)
            {
                Image image = Image.Image_create(t);
                if (this.restoreTileTransparency)
                {
                    image.doRestoreCutTransparency();
                }
                ImageMultiDrawer imageMultiDrawer2 = new ImageMultiDrawer().initWithImageandCapacity(image, this.maxRowsOnScreen * this.maxColsOnScreen);
                num = this.drawers.Count;
                this.drawers.Add(imageMultiDrawer2);
            }
            TileEntry tileEntry = new TileEntry();
            tileEntry.drawerIndex = num;
            tileEntry.quad = q;
            this.tiles[ti] = tileEntry;
        }

        // Token: 0x06000201 RID: 513 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
        public virtual void fillStartAtRowColumnRowsColumnswithTile(int r, int c, int rs, int cs, int ti)
        {
            for (int i = c; i < c + cs; i++)
            {
                for (int j = r; j < r + rs; j++)
                {
                    this.matrix[i, j] = ti;
                }
            }
        }

        // Token: 0x06000202 RID: 514 RVA: 0x0000D3FD File Offset: 0x0000B5FD
        public virtual void setParallaxRatio(float r)
        {
            this.parallaxRatio = r;
        }

        // Token: 0x06000203 RID: 515 RVA: 0x0000D406 File Offset: 0x0000B606
        public virtual void setRepeatHorizontally(TileMap.Repeat r)
        {
            this.repeatedHorizontally = r;
            this.updateVars();
        }

        // Token: 0x06000204 RID: 516 RVA: 0x0000D415 File Offset: 0x0000B615
        public virtual void setRepeatVertically(TileMap.Repeat r)
        {
            this.repeatedVertically = r;
            this.updateVars();
        }

        // Token: 0x06000205 RID: 517 RVA: 0x0000D424 File Offset: 0x0000B624
        public virtual void updateWithCameraPos(Vector pos)
        {
            float num = (float)Math.Round((double)(pos.x / this.parallaxRatio));
            float num2 = (float)Math.Round((double)(pos.y / this.parallaxRatio));
            float num3 = this.x;
            float num4 = this.y;
            if (this.repeatedVertically != TileMap.Repeat.REPEAT_NONE)
            {
                float num5 = num4 - num2;
                int num6 = (int)num5 % this.tileMapHeight;
                if (num5 < 0f)
                {
                    num4 = (float)num6 + num2;
                }
                else
                {
                    num4 = (float)(num6 - this.tileMapHeight) + num2;
                }
            }
            if (this.repeatedHorizontally != TileMap.Repeat.REPEAT_NONE)
            {
                float num7 = num3 - num;
                int num8 = (int)num7 % this.tileMapWidth;
                if (num7 < 0f)
                {
                    num3 = (float)num8 + num;
                }
                else
                {
                    num3 = (float)(num8 - this.tileMapWidth) + num;
                }
            }
            if (!MathHelper.rectInRect(num, num2, num + (float)this.cameraViewWidth, num2 + (float)this.cameraViewHeight, num3, num4, num3 + (float)this.tileMapWidth, num4 + (float)this.tileMapHeight))
            {
                return;
            }
            Rectangle rectangle = MathHelper.rectInRectIntersection(new Rectangle(num3, num4, (float)this.tileMapWidth, (float)this.tileMapHeight), new Rectangle(num, num2, (float)this.cameraViewWidth, (float)this.cameraViewHeight));
            Vector vector = MathHelper.vect(Math.Max(0f, rectangle.x), Math.Max(0f, rectangle.y));
            Vector vector2 = MathHelper.vect((float)((int)vector.x / this.tileWidth), (float)((int)vector.y / this.tileHeight));
            float num9 = num4 + vector2.y * (float)this.tileHeight;
            Vector vector3 = MathHelper.vect(num3 + vector2.x * (float)this.tileWidth, num9);
            int count = this.drawers.Count;
            for (int i = 0; i < count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = this.drawers[i];
                if (imageMultiDrawer != null)
                {
                    imageMultiDrawer.numberOfQuadsToDraw = 0;
                }
            }
            int num10 = (int)(vector2.x + (float)this.maxColsOnScreen - 1f);
            int num11 = (int)(vector2.y + (float)this.maxRowsOnScreen - 1f);
            if (this.repeatedVertically == TileMap.Repeat.REPEAT_NONE)
            {
                num11 = Math.Min(this.rows - 1, num11);
            }
            if (this.repeatedHorizontally == TileMap.Repeat.REPEAT_NONE)
            {
                num10 = Math.Min(this.columns - 1, num10);
            }
            for (int j = (int)vector2.x; j <= num10; j++)
            {
                vector3.y = num9;
                int num12 = (int)vector2.y;
                while (num12 <= num11 && vector3.y < num2 + (float)this.cameraViewHeight)
                {
                    Rectangle rectangle2 = MathHelper.rectInRectIntersection(new Rectangle(num, num2, (float)this.cameraViewWidth, (float)this.cameraViewHeight), new Rectangle(vector3.x, vector3.y, (float)this.tileWidth, (float)this.tileHeight));
                    Rectangle rectangle3 = new Rectangle(num - vector3.x + rectangle2.x, num2 - vector3.y + rectangle2.y, rectangle2.w, rectangle2.h);
                    int num13 = j;
                    int num14 = num12;
                    if (this.repeatedVertically == TileMap.Repeat.REPEAT_EDGES)
                    {
                        if (vector3.y < this.y)
                        {
                            num14 = 0;
                        }
                        else if (vector3.y >= this.y + (float)this.tileMapHeight)
                        {
                            num14 = this.rows - 1;
                        }
                    }
                    if (this.repeatedHorizontally == TileMap.Repeat.REPEAT_EDGES)
                    {
                        if (vector3.x < this.x)
                        {
                            num13 = 0;
                        }
                        else if (vector3.x >= this.x + (float)this.tileMapWidth)
                        {
                            num13 = this.columns - 1;
                        }
                    }
                    if (this.horizontalRandom)
                    {
                        float num15 = MathHelper.fmSin(vector3.x) * (float)this.randomSeed;
                        num13 = Math.Abs((int)num15 % this.columns);
                    }
                    if (this.verticalRandom)
                    {
                        float num16 = MathHelper.fmSin(vector3.y) * (float)this.randomSeed;
                        num14 = Math.Abs((int)num16 % this.rows);
                    }
                    if (num13 >= this.columns)
                    {
                        num13 %= this.columns;
                    }
                    if (num14 >= this.rows)
                    {
                        num14 %= this.rows;
                    }
                    int num17 = this.matrix[num13, num14];
                    if (num17 >= 0)
                    {
                        TileEntry tileEntry = this.tiles[num17];
                        ImageMultiDrawer imageMultiDrawer2 = this.drawers[tileEntry.drawerIndex];
                        Texture2D texture = imageMultiDrawer2.image.texture;
                        if (tileEntry.quad != -1)
                        {
                            rectangle3.x += texture.quadRects[tileEntry.quad].x;
                            rectangle3.y += texture.quadRects[tileEntry.quad].y;
                        }
                        Quad2D textureCoordinates = GLDrawer.getTextureCoordinates(imageMultiDrawer2.image.texture, rectangle3);
                        Quad3D quad3D = Quad3D.MakeQuad3D((double)(pos.x + rectangle2.x), (double)(pos.y + rectangle2.y), 0.0, (double)rectangle2.w, (double)rectangle2.h);
                        imageMultiDrawer2.setTextureQuadatVertexQuadatIndex(textureCoordinates, quad3D, imageMultiDrawer2.numberOfQuadsToDraw++);
                    }
                    vector3.y += (float)this.tileHeight;
                    num12++;
                }
                vector3.x += (float)this.tileWidth;
                if (vector3.x >= num + (float)this.cameraViewWidth)
                {
                    return;
                }
            }
        }

        // Token: 0x06000206 RID: 518 RVA: 0x0000D980 File Offset: 0x0000BB80
        public virtual void updateVars()
        {
            this.maxColsOnScreen = 2 + (int)Math.Floor((double)(this.cameraViewWidth / (this.tileWidth + 1)));
            this.maxRowsOnScreen = 2 + (int)Math.Floor((double)(this.cameraViewHeight / (this.tileHeight + 1)));
            if (this.repeatedVertically == TileMap.Repeat.REPEAT_NONE)
            {
                this.maxRowsOnScreen = Math.Min(this.maxRowsOnScreen, this.rows);
            }
            if (this.repeatedHorizontally == TileMap.Repeat.REPEAT_NONE)
            {
                this.maxColsOnScreen = Math.Min(this.maxColsOnScreen, this.columns);
            }
            this.width = (this.tileMapWidth = this.columns * this.tileWidth);
            this.height = (this.tileMapHeight = this.rows * this.tileHeight);
        }

        // Token: 0x040007F4 RID: 2036
        public int[,] matrix;

        // Token: 0x040007F5 RID: 2037
        private int rows;

        // Token: 0x040007F6 RID: 2038
        private int columns;

        // Token: 0x040007F7 RID: 2039
        private List<ImageMultiDrawer> drawers;

        // Token: 0x040007F8 RID: 2040
        private Dictionary<int, TileEntry> tiles;

        // Token: 0x040007F9 RID: 2041
        private int cameraViewWidth;

        // Token: 0x040007FA RID: 2042
        private int cameraViewHeight;

        // Token: 0x040007FB RID: 2043
        private int tileMapWidth;

        // Token: 0x040007FC RID: 2044
        private int tileMapHeight;

        // Token: 0x040007FD RID: 2045
        private int maxRowsOnScreen;

        // Token: 0x040007FE RID: 2046
        private int maxColsOnScreen;

        // Token: 0x040007FF RID: 2047
        private int randomSeed;

        // Token: 0x04000800 RID: 2048
        private TileMap.Repeat repeatedVertically;

        // Token: 0x04000801 RID: 2049
        private TileMap.Repeat repeatedHorizontally;

        // Token: 0x04000802 RID: 2050
        private float parallaxRatio;

        // Token: 0x04000803 RID: 2051
        private int tileWidth;

        // Token: 0x04000804 RID: 2052
        private int tileHeight;

        // Token: 0x04000805 RID: 2053
        private bool horizontalRandom;

        // Token: 0x04000806 RID: 2054
        private bool verticalRandom;

        // Token: 0x04000807 RID: 2055
        private bool restoreTileTransparency;

        // Token: 0x02000037 RID: 55
        public enum Repeat
        {
            // Token: 0x04000809 RID: 2057
            REPEAT_NONE,
            // Token: 0x0400080A RID: 2058
            REPEAT_ALL,
            // Token: 0x0400080B RID: 2059
            REPEAT_EDGES
        }
    }
}
