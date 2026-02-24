using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000CD RID: 205
    internal class Text : BaseElement
    {
        // Token: 0x060005F0 RID: 1520 RVA: 0x0002CBFC File Offset: 0x0002ADFC
        public static Text createWithFontandString(int i, NSString str)
        {
            Text text = new Text().initWithFont(Application.getFont(i));
            text.setString(str);
            return text;
        }

        // Token: 0x060005F1 RID: 1521 RVA: 0x0002CC24 File Offset: 0x0002AE24
        public static Text createWithFontandStringEN(int i, NSString str)
        {
            Text text = new Text().initWithFont(Application.getFontEN(i));
            text.setString(str);
            return text;
        }

        // Token: 0x060005F2 RID: 1522 RVA: 0x0002CC4C File Offset: 0x0002AE4C
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

        // Token: 0x060005F3 RID: 1523 RVA: 0x0002CCBC File Offset: 0x0002AEBC
        public virtual void setString(string newString)
        {
            setString(NSS(newString));
        }

        // Token: 0x060005F4 RID: 1524 RVA: 0x0002CCCA File Offset: 0x0002AECA
        public virtual void setString(NSString newString)
        {
            setStringandWidth(newString, -1f);
        }

        // Token: 0x060005F5 RID: 1525 RVA: 0x0002CCD8 File Offset: 0x0002AED8
        public virtual void setStringandWidth(NSString newString, double w)
        {
            setStringandWidth(newString, (float)w);
        }

        // Token: 0x060005F6 RID: 1526 RVA: 0x0002CCE4 File Offset: 0x0002AEE4
        public virtual void setStringandWidth(NSString newString, float w)
        {
            string_ = newString;
            if (string_ == null)
            {
                string_ = new NSString("");
            }
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

        // Token: 0x060005F7 RID: 1527 RVA: 0x0002CD68 File Offset: 0x0002AF68
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

        // Token: 0x060005F8 RID: 1528 RVA: 0x0002CDB4 File Offset: 0x0002AFB4
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
                float num8;
                if (align != 1)
                {
                    if (align == 2)
                    {
                        num8 = (wrapWidth - formattedString.width) / 2f;
                    }
                    else
                    {
                        num8 = wrapWidth - formattedString.width;
                    }
                }
                else
                {
                    num8 = 0f;
                }
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

        // Token: 0x060005F9 RID: 1529 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
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
                if (characters[i] != ' ' && characters[i] != '*' && characters[i] != '\n')
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
                float num10;
                if (align != 1)
                {
                    if (align == 2)
                    {
                        num10 = (wrapWidth - formattedString.width) / 2f;
                    }
                    else
                    {
                        num10 = wrapWidth - formattedString.width;
                    }
                }
                else
                {
                    num10 = 0f;
                }
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

        // Token: 0x060005FA RID: 1530 RVA: 0x0002D56B File Offset: 0x0002B76B
        public virtual NSString getString()
        {
            return string_;
        }

        // Token: 0x060005FB RID: 1531 RVA: 0x0002D573 File Offset: 0x0002B773
        public virtual void setAlignment(int a)
        {
            align = a;
        }

        // Token: 0x060005FC RID: 1532 RVA: 0x0002D57C File Offset: 0x0002B77C
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

        // Token: 0x060005FD RID: 1533 RVA: 0x0002D64C File Offset: 0x0002B84C
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
                if (c == ' ' || c == '\n' || c == '*')
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

        // Token: 0x060005FE RID: 1534 RVA: 0x0002D867 File Offset: 0x0002BA67
        private BaseElement createFromXML(XMLNode xml)
        {
            return null;
        }

        // Token: 0x060005FF RID: 1535 RVA: 0x0002D86A File Offset: 0x0002BA6A
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

        // Token: 0x04000B42 RID: 2882
        public bool useSystemFont;

        // Token: 0x04000B43 RID: 2883
        public int align;

        // Token: 0x04000B44 RID: 2884
        public NSString string_;

        // Token: 0x04000B45 RID: 2885
        public int stringLength;

        // Token: 0x04000B46 RID: 2886
        public FontGeneric font;

        // Token: 0x04000B47 RID: 2887
        public float wrapWidth;

        // Token: 0x04000B48 RID: 2888
        public List<FormattedString> formattedStrings;

        // Token: 0x04000B49 RID: 2889
        private List<ImageMultiDrawer> multiDrawers;

        // Token: 0x04000B4A RID: 2890
        public float maxHeight;

        // Token: 0x04000B4B RID: 2891
        public bool wrapLongWords;

        // Token: 0x04000B4C RID: 2892
        private List<FontWP7.CharPosition> charsToDraw;
    }
}
