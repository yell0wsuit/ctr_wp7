using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000036 RID: 54
    internal class TileMap : BaseElement
    {
        // Token: 0x060001FD RID: 509 RVA: 0x0000D13C File Offset: 0x0000B33C
        public override void draw()
        {
            int count = drawers.Count;
            for (int i = 0; i < count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = drawers[i];
                imageMultiDrawer?.draw();
            }
        }

        // Token: 0x060001FE RID: 510 RVA: 0x0000D177 File Offset: 0x0000B377
        public override void dealloc()
        {
            matrix = null;
            drawers.Clear();
            drawers = null;
            tiles.Clear();
            tiles = null;
            base.dealloc();
        }

        // Token: 0x060001FF RID: 511 RVA: 0x0000D1AC File Offset: 0x0000B3AC
        public virtual TileMap initWithRowsColumns(int r, int c)
        {
            if (base.init() != null)
            {
                rows = r;
                columns = c;
                cameraViewWidth = (int)SCREEN_WIDTH_EXPANDED;
                cameraViewHeight = (int)SCREEN_HEIGHT_EXPANDED;
                parallaxRatio = 1f;
                drawers = [];
                tiles = [];
                matrix = new int[columns, rows];
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        matrix[i, j] = -1;
                    }
                }
                repeatedVertically = Repeat.REPEAT_NONE;
                repeatedHorizontally = Repeat.REPEAT_NONE;
                horizontalRandom = false;
                verticalRandom = false;
                restoreTileTransparency = true;
                randomSeed = RND_RANGE(1000, 2000);
            }
            return this;
        }

        // Token: 0x06000200 RID: 512 RVA: 0x0000D28C File Offset: 0x0000B48C
        public virtual void addTileQuadwithID(Texture2D t, int q, int ti)
        {
            if (q == -1)
            {
                tileWidth = t._realWidth;
                tileHeight = t._realHeight;
            }
            else
            {
                tileWidth = (int)t.quadRects[q].w;
                tileHeight = (int)t.quadRects[q].h;
            }
            updateVars();
            int num = -1;
            for (int i = 0; i < drawers.Count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = drawers[i];
                if (imageMultiDrawer.image.texture == t)
                {
                    num = i;
                }
                if (imageMultiDrawer.image.texture._realWidth == tileWidth)
                {
                    _ = imageMultiDrawer.image.texture._realHeight;
                }
            }
            if (num == -1)
            {
                Image image = Image.Image_create(t);
                if (restoreTileTransparency)
                {
                    image.doRestoreCutTransparency();
                }
                ImageMultiDrawer imageMultiDrawer2 = new ImageMultiDrawer().initWithImageandCapacity(image, maxRowsOnScreen * maxColsOnScreen);
                num = drawers.Count;
                drawers.Add(imageMultiDrawer2);
            }
            TileEntry tileEntry = new()
            {
                drawerIndex = num,
                quad = q
            };
            tiles[ti] = tileEntry;
        }

        // Token: 0x06000201 RID: 513 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
        public virtual void fillStartAtRowColumnRowsColumnswithTile(int r, int c, int rs, int cs, int ti)
        {
            for (int i = c; i < c + cs; i++)
            {
                for (int j = r; j < r + rs; j++)
                {
                    matrix[i, j] = ti;
                }
            }
        }

        // Token: 0x06000202 RID: 514 RVA: 0x0000D3FD File Offset: 0x0000B5FD
        public virtual void setParallaxRatio(float r)
        {
            parallaxRatio = r;
        }

        // Token: 0x06000203 RID: 515 RVA: 0x0000D406 File Offset: 0x0000B606
        public virtual void setRepeatHorizontally(Repeat r)
        {
            repeatedHorizontally = r;
            updateVars();
        }

        // Token: 0x06000204 RID: 516 RVA: 0x0000D415 File Offset: 0x0000B615
        public virtual void setRepeatVertically(Repeat r)
        {
            repeatedVertically = r;
            updateVars();
        }

        // Token: 0x06000205 RID: 517 RVA: 0x0000D424 File Offset: 0x0000B624
        public virtual void updateWithCameraPos(Vector pos)
        {
            float num = (float)Math.Round((double)(pos.x / parallaxRatio));
            float num2 = (float)Math.Round((double)(pos.y / parallaxRatio));
            float num3 = x;
            float num4 = y;
            if (repeatedVertically != Repeat.REPEAT_NONE)
            {
                float num5 = num4 - num2;
                int num6 = (int)num5 % tileMapHeight;
                if (num5 < 0f)
                {
                    num4 = num6 + num2;
                }
                else
                {
                    num4 = num6 - tileMapHeight + num2;
                }
            }
            if (repeatedHorizontally != Repeat.REPEAT_NONE)
            {
                float num7 = num3 - num;
                int num8 = (int)num7 % tileMapWidth;
                if (num7 < 0f)
                {
                    num3 = num8 + num;
                }
                else
                {
                    num3 = num8 - tileMapWidth + num;
                }
            }
            if (!rectInRect(num, num2, num + cameraViewWidth, num2 + cameraViewHeight, num3, num4, num3 + tileMapWidth, num4 + tileMapHeight))
            {
                return;
            }
            Rectangle rectangle = rectInRectIntersection(new Rectangle(num3, num4, tileMapWidth, tileMapHeight), new Rectangle(num, num2, cameraViewWidth, cameraViewHeight));
            Vector vector = vect(Math.Max(0f, rectangle.x), Math.Max(0f, rectangle.y));
            Vector vector2 = vect((int)vector.x / tileWidth, (int)vector.y / tileHeight);
            float num9 = num4 + (vector2.y * tileHeight);
            Vector vector3 = vect(num3 + (vector2.x * tileWidth), num9);
            int count = drawers.Count;
            for (int i = 0; i < count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = drawers[i];
                imageMultiDrawer?.numberOfQuadsToDraw = 0;
            }
            int num10 = (int)(vector2.x + maxColsOnScreen - 1f);
            int num11 = (int)(vector2.y + maxRowsOnScreen - 1f);
            if (repeatedVertically == Repeat.REPEAT_NONE)
            {
                num11 = Math.Min(rows - 1, num11);
            }
            if (repeatedHorizontally == Repeat.REPEAT_NONE)
            {
                num10 = Math.Min(columns - 1, num10);
            }
            for (int j = (int)vector2.x; j <= num10; j++)
            {
                vector3.y = num9;
                int num12 = (int)vector2.y;
                while (num12 <= num11 && vector3.y < num2 + cameraViewHeight)
                {
                    Rectangle rectangle2 = rectInRectIntersection(new Rectangle(num, num2, cameraViewWidth, cameraViewHeight), new Rectangle(vector3.x, vector3.y, tileWidth, tileHeight));
                    Rectangle rectangle3 = new(num - vector3.x + rectangle2.x, num2 - vector3.y + rectangle2.y, rectangle2.w, rectangle2.h);
                    int num13 = j;
                    int num14 = num12;
                    if (repeatedVertically == Repeat.REPEAT_EDGES)
                    {
                        if (vector3.y < y)
                        {
                            num14 = 0;
                        }
                        else if (vector3.y >= y + tileMapHeight)
                        {
                            num14 = rows - 1;
                        }
                    }
                    if (repeatedHorizontally == Repeat.REPEAT_EDGES)
                    {
                        if (vector3.x < x)
                        {
                            num13 = 0;
                        }
                        else if (vector3.x >= x + tileMapWidth)
                        {
                            num13 = columns - 1;
                        }
                    }
                    if (horizontalRandom)
                    {
                        float num15 = fmSin(vector3.x) * randomSeed;
                        num13 = Math.Abs((int)num15 % columns);
                    }
                    if (verticalRandom)
                    {
                        float num16 = fmSin(vector3.y) * randomSeed;
                        num14 = Math.Abs((int)num16 % rows);
                    }
                    if (num13 >= columns)
                    {
                        num13 %= columns;
                    }
                    if (num14 >= rows)
                    {
                        num14 %= rows;
                    }
                    int num17 = matrix[num13, num14];
                    if (num17 >= 0)
                    {
                        TileEntry tileEntry = tiles[num17];
                        ImageMultiDrawer imageMultiDrawer2 = drawers[tileEntry.drawerIndex];
                        Texture2D texture = imageMultiDrawer2.image.texture;
                        if (tileEntry.quad != -1)
                        {
                            rectangle3.x += texture.quadRects[tileEntry.quad].x;
                            rectangle3.y += texture.quadRects[tileEntry.quad].y;
                        }
                        Quad2D textureCoordinates = GLDrawer.getTextureCoordinates(imageMultiDrawer2.image.texture, rectangle3);
                        Quad3D quad3D = Quad3D.MakeQuad3D((double)(pos.x + rectangle2.x), (double)(pos.y + rectangle2.y), 0.0, rectangle2.w, rectangle2.h);
                        imageMultiDrawer2.setTextureQuadatVertexQuadatIndex(textureCoordinates, quad3D, imageMultiDrawer2.numberOfQuadsToDraw++);
                    }
                    vector3.y += tileHeight;
                    num12++;
                }
                vector3.x += tileWidth;
                if (vector3.x >= num + cameraViewWidth)
                {
                    return;
                }
            }
        }

        // Token: 0x06000206 RID: 518 RVA: 0x0000D980 File Offset: 0x0000BB80
        public virtual void updateVars()
        {
            maxColsOnScreen = 2 + (int)Math.Floor((double)(cameraViewWidth / (tileWidth + 1)));
            maxRowsOnScreen = 2 + (int)Math.Floor((double)(cameraViewHeight / (tileHeight + 1)));
            if (repeatedVertically == Repeat.REPEAT_NONE)
            {
                maxRowsOnScreen = Math.Min(maxRowsOnScreen, rows);
            }
            if (repeatedHorizontally == Repeat.REPEAT_NONE)
            {
                maxColsOnScreen = Math.Min(maxColsOnScreen, columns);
            }
            width = tileMapWidth = columns * tileWidth;
            height = tileMapHeight = rows * tileHeight;
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
        private Repeat repeatedVertically;

        // Token: 0x04000801 RID: 2049
        private Repeat repeatedHorizontally;

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
