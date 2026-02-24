using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

namespace ctr_wp7.iframework.visual
{
    internal class Text : BaseElement
    {
        public static Text createWithFontandString(int i, NSString str)
        {
            Text text = new Text().initWithFont(Application.getFont(i));
            text.setString(str);
            return text;
        }

        public static Text createWithFontandStringEN(int i, NSString str)
        {
            Text text = new Text().initWithFont(Application.getFontEN(i));
            text.setString(str);
            return text;
        }

        public virtual Text initWithFont(FontGeneric i)
        {
            if (base.init() != null)
            {
                font = (FontGeneric)NSRET(i);
                formattedStrings = [];
                width = -1;
                height = -1;
                align = 1;
                multiDrawers = [];
                wrapLongWords = false;
                maxHeight = -1f;
            }
            font.notifyTextCreated(this);
            return this;
        }

        public virtual void setString(string newString)
        {
            setString(NSS(newString));
        }

        public virtual void setString(NSString newString)
        {
            setStringandWidth(newString, -1f);
        }

        public virtual void setStringandWidth(NSString newString, double w)
        {
            setStringandWidth(newString, (float)w);
        }

        public virtual void setStringandWidth(NSString newString, float w)
        {
            string_ = newString;
            string_ ??= new NSString("");
            font.notifyTextChanged(this);
            if (w == -1f)
            {
                float num = 0.1f;
                wrapWidth = font.stringWidth(string_) + num;
            }
            else
            {
                wrapWidth = w;
            }
            if (string_ != null)
            {
                formatText();
                updateDrawerValues();
                return;
            }
            stringLength = 0;
        }

        public void mapCharAtXYatIndex(char ch, float dx, float dy, int n)
        {
            FontWP7.CharPosition charPosition = new()
            {
                x = dx,
                y = dy,
                ch = ch
            };
            if (charsToDraw == null || n == 0)
            {
                charsToDraw = [];
            }
            charsToDraw.Add(charPosition);
        }

        private void updateSystemFontDrawerValues()
        {
            charsToDraw = [];
            _ = string_.length();
            _ = string_.getCharacters();
            float num = 0f;
            int num2 = (int)font.fontHeight();
            int num3 = 0;
            NSString nsstring = NSS("..");
            char[] characters = nsstring.getCharacters();
            int num4 = (int)font.getCharOffset(characters, 0, 2);
            int num5 = (int)((maxHeight == -1f) ? formattedStrings.Count : MIN(formattedStrings.Count, maxHeight / (num2 + font.getLineOffset())));
            bool flag = num5 != formattedStrings.Count;
            int num6 = 0;
            for (int i = 0; i < num5; i++)
            {
                FormattedString formattedString = formattedStrings[i];
                int num7 = formattedString.string_.length();
                char[] characters2 = formattedString.string_.getCharacters();
                float num8 = align != 1 ? align == 2 ? (wrapWidth - formattedString.width) / 2f : wrapWidth - formattedString.width : 0f;
                for (int j = 0; j < num7; j++)
                {
                    if (characters2[j] != '*')
                    {
                        if (characters2[j] == ' ')
                        {
                            num8 += font.getCharWidth(' ') + font.getCharOffset(characters2, j, num7);
                        }
                        else
                        {
                            mapCharAtXYatIndex(characters2[j], num8, num, num6++);
                            num3++;
                            num8 += font.getCharWidth(characters2[j]) + font.getCharOffset(characters2, j, num7);
                        }
                        if (flag && i == num5 - 1)
                        {
                            int num9 = (int)font.getCharWidth('.');
                            if (j == num7 - 1 || (j == num7 - 2 && num8 + (3 * (num9 + num4)) + font.getCharWidth(' ') > wrapWidth))
                            {
                                mapCharAtXYatIndex(characters2[j], num8, num, num3++);
                                num8 += num9 + num4;
                                mapCharAtXYatIndex(characters2[j], num8, num, num3++);
                                num8 += num9 + num4;
                                mapCharAtXYatIndex(characters2[j], num8, num, num3++);
                                break;
                            }
                        }
                    }
                }
                num += num2 + font.getLineOffset();
            }
            stringLength = num3;
            if (formattedStrings.Count <= 1)
            {
                height = (int)font.fontHeight();
                width = (int)wrapWidth;
            }
            else
            {
                height = (int)(((font.fontHeight() + font.getLineOffset()) * formattedStrings.Count) - font.getLineOffset());
                width = (int)wrapWidth;
            }
            if (maxHeight != -1f)
            {
                height = (int)MIN(height, maxHeight);
            }
        }

        public virtual void updateDrawerValues()
        {
            multiDrawers.Clear();
            int num = font.totalCharmaps();
            if (num == 0)
            {
                useSystemFont = true;
                updateSystemFontDrawerValues();
                return;
            }
            int num2 = string_.length();
            char[] characters = string_.getCharacters();
            int[] array = new int[num];
            for (int i = 0; i < num2; i++)
            {
                if (characters[i] is not ' ' and not '*' and not '\n')
                {
                    array[font.getCharmapIndex(characters[i])]++;
                }
            }
            for (int j = 0; j < num; j++)
            {
                int num3 = array[j];
                if (num3 > 0)
                {
                    ImageMultiDrawer imageMultiDrawer = new ImageMultiDrawer().initWithImageandCapacity(font.getCharmap(j), num3);
                    multiDrawers.Add(imageMultiDrawer);
                }
            }
            float num4 = 0f;
            int num5 = (int)font.fontHeight();
            int num6 = 0;
            NSString nsstring = NSS("..");
            char[] characters2 = nsstring.getCharacters();
            int num7 = (int)font.getCharOffset(characters2, 0, 2);
            int num8 = (int)((maxHeight == -1f) ? formattedStrings.Count : MIN(formattedStrings.Count, maxHeight / (num5 + font.getLineOffset())));
            bool flag = num8 != formattedStrings.Count;
            int[] array2 = new int[num];
            for (int k = 0; k < num8; k++)
            {
                FormattedString formattedString = formattedStrings[k];
                int num9 = formattedString.string_.length();
                char[] characters3 = formattedString.string_.getCharacters();
                float num10 = align != 1 ? align == 2 ? (wrapWidth - formattedString.width) / 2f : wrapWidth - formattedString.width : 0f;
                for (int l = 0; l < num9; l++)
                {
                    if (characters3[l] != '*')
                    {
                        if (characters3[l] == ' ' || font.getCharQuad(characters3[l]) == -1)
                        {
                            num10 += font.getCharWidth(' ') + font.getCharOffset(characters3, l, num9);
                        }
                        else
                        {
                            int charmapIndex = font.getCharmapIndex(characters3[l]);
                            int charQuad = font.getCharQuad(characters3[l]);
                            ImageMultiDrawer imageMultiDrawer2 = multiDrawers[charmapIndex];
                            imageMultiDrawer2.mapTextureQuadAtXYatIndex(charQuad, num10, num4, array2[charmapIndex]++);
                            num6++;
                            num10 += font.getCharWidth(characters3[l]) + font.getCharOffset(characters3, l, num9);
                        }
                        if (flag && k == num8 - 1)
                        {
                            int charmapIndex2 = font.getCharmapIndex('.');
                            int charQuad2 = font.getCharQuad('.');
                            ImageMultiDrawer imageMultiDrawer3 = multiDrawers[charmapIndex2];
                            int num11 = (int)font.getCharWidth('.');
                            if (l == num9 - 1 || (l == num9 - 2 && num10 + (3 * (num11 + num7)) + font.getCharWidth(' ') > wrapWidth))
                            {
                                imageMultiDrawer3.mapTextureQuadAtXYatIndex(charQuad2, num10, num4, num6++);
                                num10 += num11 + num7;
                                imageMultiDrawer3.mapTextureQuadAtXYatIndex(charQuad2, num10, num4, num6++);
                                num10 += num11 + num7;
                                imageMultiDrawer3.mapTextureQuadAtXYatIndex(charQuad2, num10, num4, num6++);
                                break;
                            }
                        }
                    }
                }
                num4 += num5 + font.getLineOffset();
            }
            stringLength = num6;
            if (formattedStrings.Count <= 1)
            {
                height = (int)font.fontHeight();
                width = (int)wrapWidth;
            }
            else
            {
                height = (int)(((font.fontHeight() + font.getLineOffset()) * formattedStrings.Count) - font.getLineOffset());
                width = (int)wrapWidth;
            }
            if (maxHeight != -1f)
            {
                height = (int)MIN(height, maxHeight);
            }
        }

        public virtual NSString getString()
        {
            return string_;
        }

        public virtual void setAlignment(int a)
        {
            align = a;
        }

        public override void draw()
        {
            preDraw();
            if (useSystemFont)
            {
                OpenGL.glTranslatef(drawX, drawY, 0f);
                FontWP7 fontWP = (FontWP7)font;
                fontWP.DrawString(ref charsToDraw);
                OpenGL.glTranslatef(-drawX, -drawY, 0f);
            }
            else if (stringLength > 0)
            {
                OpenGL.glTranslatef(drawX, drawY, 0f);
                int i = 0;
                int count = multiDrawers.Count;
                while (i < count)
                {
                    ImageMultiDrawer imageMultiDrawer = multiDrawers[i];
                    imageMultiDrawer?.drawAllQuads();
                    i++;
                }
                OpenGL.glTranslatef(-drawX, -drawY, 0f);
            }
            postDraw();
        }

        public virtual void formatText()
        {
            short[] array = new short[512];
            char[] characters = string_.getCharacters();
            int num = string_.length();
            int num2 = 0;
            int num3 = 0;
            float num4 = 0f;
            int num5 = 0;
            int num6 = 0;
            float num7 = 0f;
            int i = 0;
            while (i < num)
            {
                char c = characters[i++];
                if (c is ' ' or '\n' or '*')
                {
                    num7 += num4;
                    num6 = i - 1;
                    num4 = 0f;
                    num3 = i;
                    if (c == ' ')
                    {
                        num3--;
                        num4 = font.getCharWidth(' ') + font.getCharOffset(characters, i - 1, num);
                    }
                }
                else
                {
                    num4 += font.getCharWidth(c) + font.getCharOffset(characters, i - 1, num);
                }
                bool flag = num7 + num4 > wrapWidth;
                if (wrapLongWords && flag && num6 == num5)
                {
                    num7 += num4;
                    num6 = i;
                    num4 = 0f;
                    num3 = i;
                }
                if ((num7 + num4 > wrapWidth && num6 != num5) || c == '\n')
                {
                    array[num2++] = (short)num5;
                    array[num2++] = (short)num6;
                    while (num3 < num && characters[num3] == ' ')
                    {
                        num3++;
                        num4 -= font.getCharWidth(' ');
                    }
                    num5 = num3;
                    num6 = num5;
                    num7 = 0f;
                }
            }
            if (num4 != 0f)
            {
                array[num2++] = (short)num5;
                array[num2++] = (short)i;
            }
            int num8 = num2 >> 1;
            formattedStrings.Clear();
            for (int j = 0; j < num8; j++)
            {
                int num9 = array[j << 1];
                int num10 = array[(j << 1) + 1];
                NSRange nsrange;
                nsrange.location = (uint)num9;
                nsrange.length = (uint)(num10 - num9);
                NSString nsstring = string_.substringWithRange(nsrange);
                float num11 = font.stringWidth(nsstring);
                FormattedString formattedString = new FormattedString().initWithStringAndWidth(nsstring, num11);
                formattedStrings.Add(formattedString);
            }
        }

        private static BaseElement createFromXML(XMLNode xml)
        {
            return null;
        }

        public override void dealloc()
        {
            font.notifyTextDeleted(this);
            string_ = null;
            font = null;
            formattedStrings = null;
            multiDrawers.Clear();
            multiDrawers = null;
            base.dealloc();
        }

        public bool useSystemFont;

        public int align;

        public NSString string_;

        public int stringLength;

        public FontGeneric font;

        public float wrapWidth;

        public List<FormattedString> formattedStrings;

        private List<ImageMultiDrawer> multiDrawers;

        public float maxHeight;

        public bool wrapLongWords;

        private List<FontWP7.CharPosition> charsToDraw;
    }
}
