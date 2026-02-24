using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal sealed class TileMap : BaseElement
    {
        public override void draw()
        {
            int count = drawers.Count;
            for (int i = 0; i < count; i++)
            {
                ImageMultiDrawer imageMultiDrawer = drawers[i];
                imageMultiDrawer?.draw();
            }
        }

        public override void dealloc()
        {
            matrix = null;
            drawers.Clear();
            drawers = null;
            tiles.Clear();
            tiles = null;
            base.dealloc();
        }

        public TileMap initWithRowsColumns(int r, int c)
        {
            if (init() != null)
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

        public void addTileQuadwithID(Texture2D t, int q, int ti)
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

        public void fillStartAtRowColumnRowsColumnswithTile(int r, int c, int rs, int cs, int ti)
        {
            for (int i = c; i < c + cs; i++)
            {
                for (int j = r; j < r + rs; j++)
                {
                    matrix[i, j] = ti;
                }
            }
        }

        public void setParallaxRatio(float r)
        {
            parallaxRatio = r;
        }

        public void setRepeatHorizontally(Repeat r)
        {
            repeatedHorizontally = r;
            updateVars();
        }

        public void setRepeatVertically(Repeat r)
        {
            repeatedVertically = r;
            updateVars();
        }

        public void updateWithCameraPos(Vector pos)
        {
            float num = (float)Math.Round((double)(pos.x / parallaxRatio));
            float num2 = (float)Math.Round((double)(pos.y / parallaxRatio));
            float num3 = x;
            float num4 = y;
            if (repeatedVertically != Repeat.REPEAT_NONE)
            {
                float num5 = num4 - num2;
                int num6 = (int)num5 % tileMapHeight;
                num4 = num5 < 0f ? num6 + num2 : num6 - tileMapHeight + num2;
            }
            if (repeatedHorizontally != Repeat.REPEAT_NONE)
            {
                float num7 = num3 - num;
                int num8 = (int)num7 % tileMapWidth;
                num3 = num7 < 0f ? num8 + num : num8 - tileMapWidth + num;
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
                _ = (imageMultiDrawer?.numberOfQuadsToDraw = 0);
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

        public void updateVars()
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

        public int[,] matrix;

        private int rows;

        private int columns;

        private List<ImageMultiDrawer> drawers;

        private Dictionary<int, TileEntry> tiles;

        private int cameraViewWidth;

        private int cameraViewHeight;

        private int tileMapWidth;

        private int tileMapHeight;

        private int maxRowsOnScreen;

        private int maxColsOnScreen;

        private int randomSeed;

        private Repeat repeatedVertically;

        private Repeat repeatedHorizontally;

        private float parallaxRatio;

        private int tileWidth;

        private int tileHeight;

        private bool horizontalRandom;

        private bool verticalRandom;

        private bool restoreTileTransparency;

        public enum Repeat
        {
            REPEAT_NONE,
            REPEAT_ALL,
            REPEAT_EDGES
        }
    }
}
