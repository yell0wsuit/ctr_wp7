using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000F6 RID: 246
    internal sealed class Font : FontGeneric
    {
        // Token: 0x06000773 RID: 1907 RVA: 0x0003B6E8 File Offset: 0x000398E8
        public Font initWithVariableSizeCharscharMapFileKerning(NSString strParam, Texture2D charmapfile, object k)
        {
            if (init() != null)
            {
                _isWvga = charmapfile.isWvga();
                charmap = new Image().initWithTexture(charmapfile);
                quadsCount = charmapfile.quadsCount;
                height = charmapfile.quadRects[0].h;
                chars = strParam.copy();
                sortedChars = chars.getCharacters();
                Array.Sort(sortedChars);
                kerning = null;
                charOffset = 0f;
                lineOffset = 0f;
            }
            return this;
        }

        // Token: 0x06000774 RID: 1908 RVA: 0x0003B786 File Offset: 0x00039986
        public override void dealloc()
        {
            chars = null;
            sortedChars = null;
            charmap = null;
            kerning = null;
            base.dealloc();
        }

        // Token: 0x06000775 RID: 1909 RVA: 0x0003B7AC File Offset: 0x000399AC
        public override void setCharOffsetLineOffsetSpaceWidth(float co, float lo, float sw)
        {
            charOffset = co;
            lineOffset = lo;
            spaceWidth = sw;
            if (_isWvga)
            {
                charOffset = (int)(charOffset / 1.5);
                lineOffset = (int)(lineOffset / 1.5);
                spaceWidth = (int)(spaceWidth / 1.5);
            }
        }

        // Token: 0x06000776 RID: 1910 RVA: 0x0003B821 File Offset: 0x00039A21
        public override float fontHeight()
        {
            return height;
        }

        // Token: 0x06000777 RID: 1911 RVA: 0x0003B829 File Offset: 0x00039A29
        public override bool canDraw(char c)
        {
            return c == ' ' || Array.BinarySearch(sortedChars, c) >= 0;
        }

        // Token: 0x06000778 RID: 1912 RVA: 0x0003B844 File Offset: 0x00039A44
        public override float getCharWidth(char c)
        {
            if (c == '*')
            {
                return 0f;
            }
            if (c == ' ' || getCharQuad(c) == -1)
            {
                return spaceWidth;
            }
            return charmap.texture.quadRects[getCharQuad(c)].w;
        }

        // Token: 0x06000779 RID: 1913 RVA: 0x0003B893 File Offset: 0x00039A93
        public override int getCharmapIndex(char c)
        {
            return 0;
        }

        // Token: 0x0600077A RID: 1914 RVA: 0x0003B898 File Offset: 0x00039A98
        public override int getCharQuad(char c)
        {
            int num = chars.IndexOf(c);
            if (num >= 0)
            {
                return num;
            }
            return -1;
        }

        // Token: 0x0600077B RID: 1915 RVA: 0x0003B8B9 File Offset: 0x00039AB9
        public override float getCharOffset(char[] s, int c, int len)
        {
            if (c == len - 1)
            {
                return 0f;
            }
            return charOffset;
        }

        // Token: 0x0600077C RID: 1916 RVA: 0x0003B8CD File Offset: 0x00039ACD
        public override int totalCharmaps()
        {
            return 1;
        }

        // Token: 0x0600077D RID: 1917 RVA: 0x0003B8D0 File Offset: 0x00039AD0
        public override Image getCharmap(int i)
        {
            return charmap;
        }

        // Token: 0x04000CEE RID: 3310
        private NSString chars;

        // Token: 0x04000CEF RID: 3311
        private char[] sortedChars;

        // Token: 0x04000CF0 RID: 3312
        private bool _isWvga;

        // Token: 0x04000CF1 RID: 3313
        private object kerning;

        // Token: 0x04000CF2 RID: 3314
        private int quadsCount;

        // Token: 0x04000CF3 RID: 3315
        private float height;

        // Token: 0x04000CF4 RID: 3316
        private Image charmap;
    }
}
