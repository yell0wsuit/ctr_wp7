using System.Collections.Generic;

using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;

namespace ctre_wp7.wp7utilities
{
    internal sealed class FontWP7 : FontGeneric
    {
        public FontWP7(int fontId)
        {
            _font = (FontGeneric)Application.sharedResourceMgr().loadResource(fontId, ResourceMgr.ResourceType.FONT);
        }

        public override void setCharOffsetLineOffsetSpaceWidth(float co, float lo, float sw)
        {
            _font?.setCharOffsetLineOffsetSpaceWidth(co, lo, sw);
        }

        public override float fontHeight()
        {
            return _font?.fontHeight() ?? 0f;
        }

        public override bool canDraw(char c)
        {
            return _font?.canDraw(c) ?? false;
        }

        public override float getCharWidth(char c)
        {
            return _font?.getCharWidth(c) ?? 0f;
        }

        public override int getCharmapIndex(char c)
        {
            return _font?.getCharmapIndex(c) ?? 0;
        }

        public override int getCharQuad(char c)
        {
            return _font?.getCharQuad(c) ?? -1;
        }

        public override float getCharOffset(char[] s, int c, int len)
        {
            return _font?.getCharOffset(s, c, len) ?? 0f;
        }

        public override int totalCharmaps()
        {
            return _font?.totalCharmaps() ?? 0;
        }

        public override Image getCharmap(int i)
        {
            return _font?.getCharmap(i);
        }

        public void DrawString(ref List<CharPosition> charsToDraw)
        {
            if (_font == null || charsToDraw == null)
            {
                return;
            }

            foreach (CharPosition charPosition in charsToDraw)
            {
                int charQuad = _font.getCharQuad(charPosition.ch);
                if (charQuad < 0)
                {
                    continue;
                }

                Image charmap = _font.getCharmap(0);
                if (charmap != null)
                {
                    GLDrawer.drawImageQuad(charmap.texture, charQuad, charPosition.x, charPosition.y);
                }
            }
        }

        internal struct CharPosition
        {
            public char ch;
            public float x;
            public float y;
        }

        private readonly FontGeneric _font;
    }
}
