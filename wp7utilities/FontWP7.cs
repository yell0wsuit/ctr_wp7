using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7.wp7utilities
{
	// Token: 0x02000033 RID: 51
	internal class FontWP7 : FontGeneric
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		private FontGeneric fontOriginal
		{
			get
			{
				FontWP7.initOriginalFonts();
				if (this.fontId == 6)
				{
					return FontWP7.fontSmallOriginal;
				}
				if (this.fontId == 5)
				{
					return FontWP7.fontBigOriginal;
				}
				return null;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public FontWP7(int _fontId)
		{
			if (_fontId == 6)
			{
				this.font = FontWP7.fontSmall;
				this.fontId = _fontId;
			}
			if (_fontId == 5)
			{
				this.font = FontWP7.fontBig;
				this.fontId = _fontId;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000CA4D File Offset: 0x0000AC4D
		public override void setCharOffsetLineOffsetSpaceWidth(float co, float lo, float sw)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public override float fontHeight()
		{
			if (this.height == -1f && this.font != null && this.font.Count > 0)
			{
				foreach (KeyValuePair<char, Image> keyValuePair in this.font)
				{
					this.height = (float)keyValuePair.Value.height + 10f;
				}
			}
			return this.height;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public override bool canDraw(char c)
		{
			FontWP7.PrepareToDrawChar(c, this.fontId);
			return true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public override float getCharWidth(char c)
		{
			if (this.fontOriginal.canDraw(c))
			{
				return this.fontOriginal.getCharWidth(c);
			}
			if (this.font != null)
			{
				FontWP7.PrepareToDrawChar(c, this.fontId);
				Image image = null;
				if (this.font.TryGetValue(c, ref image))
				{
					return (float)image.width;
				}
			}
			return 0f;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000CB4B File Offset: 0x0000AD4B
		public override int getCharmapIndex(char c)
		{
			return 0;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000CB4E File Offset: 0x0000AD4E
		public override int getCharQuad(char c)
		{
			return 0;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000CB51 File Offset: 0x0000AD51
		public override float getCharOffset(char[] s, int c, int len)
		{
			return 0f;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override int totalCharmaps()
		{
			return 0;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CB5B File Offset: 0x0000AD5B
		public override Image getCharmap(int i)
		{
			return null;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CB5E File Offset: 0x0000AD5E
		private static void initOriginalFonts()
		{
			if (FontWP7.fontSmallOriginal == null)
			{
				FontWP7.fontSmallOriginal = (FontGeneric)Application.sharedResourceMgr().loadResource(6, ResourceMgr.ResourceType.FONT);
			}
			if (FontWP7.fontBigOriginal == null)
			{
				FontWP7.fontBigOriginal = (FontGeneric)Application.sharedResourceMgr().loadResource(5, ResourceMgr.ResourceType.FONT);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public void DrawString(ref List<FontWP7.CharPosition> charsToDraw)
		{
			FontGeneric fontOriginal = this.fontOriginal;
			float num = (fontOriginal.fontHeight() - this.fontHeight()) / 2f;
			float num2 = 5f;
			foreach (FontWP7.CharPosition charPosition in charsToDraw)
			{
				if (fontOriginal.canDraw(charPosition.ch))
				{
					int charQuad = fontOriginal.getCharQuad(charPosition.ch);
					Image charmap = fontOriginal.getCharmap(0);
					GLDrawer.drawImageQuad(charmap.texture, charQuad, charPosition.x, charPosition.y - num);
				}
				else
				{
					Image image = null;
					bool flag = this.font.TryGetValue(charPosition.ch, ref image);
					if (flag)
					{
						image.x = charPosition.x;
						image.y = charPosition.y + num2;
						image.draw();
					}
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public static Microsoft.Xna.Framework.Graphics.Texture2D WriteableBitmaptoTexture2D(WriteableBitmap bitmap)
		{
			Microsoft.Xna.Framework.Graphics.Texture2D texture2D = new Microsoft.Xna.Framework.Graphics.Texture2D(WP7Singletons.GraphicsDevice, bitmap.PixelWidth, bitmap.PixelHeight);
			int num = bitmap.PixelWidth * bitmap.PixelHeight;
			int[] array = new int[num];
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = bitmap.Pixels[i];
				byte b = (byte)(num2 >> 24);
				byte b2 = (byte)(num2 >> 16);
				byte b3 = (byte)(num2 >> 8);
				byte b4 = (byte)num2;
				int num3 = ((int)b << 24) | ((int)b4 << 16) | ((int)b3 << 8) | (int)b2;
				array[i] = num3;
			}
			texture2D.SetData<int>(array);
			return texture2D;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CD20 File Offset: 0x0000AF20
		private static Image Texture2DToImage(Microsoft.Xna.Framework.Graphics.Texture2D t)
		{
			ctre_wp7.iframework.visual.Texture2D texture2D = new ctre_wp7.iframework.visual.Texture2D().initWithTexture(t);
			return Image.Image_create(texture2D);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CD40 File Offset: 0x0000AF40
		private static bool hasTexture(ref Dictionary<char, Image> array, char ch)
		{
			Image image = null;
			bool flag = array.TryGetValue(ch, ref image);
			image = null;
			return flag;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public static void PrepareToDrawChar(char ch, int fontId)
		{
			FontWP7.initOriginalFonts();
			if (fontId == 6)
			{
				if (FontWP7.fontSmallOriginal.canDraw(ch))
				{
					return;
				}
				if (!FontWP7.hasTexture(ref FontWP7.fontSmall, ch))
				{
					double num = 19.0;
					TextBlock textBlock = new TextBlock
					{
						Text = ch.ToString(),
						FontSize = num,
						Foreground = FontWP7.blackBrush
					};
					Canvas canvas = new Canvas();
					canvas.Children.Add(textBlock);
					Canvas.SetLeft(textBlock, 0.0);
					Canvas.SetTop(textBlock, 0.0);
					WriteableBitmap writeableBitmap = new WriteableBitmap((int)textBlock.ActualWidth, (int)textBlock.ActualHeight);
					writeableBitmap.Render(canvas, null);
					writeableBitmap.Invalidate();
					FontWP7.fontSmall.Add(ch, FontWP7.Texture2DToImage(FontWP7.WriteableBitmaptoTexture2D(writeableBitmap)));
					return;
				}
			}
			else if (fontId == 5)
			{
				if (FontWP7.fontBigOriginal.canDraw(ch))
				{
					return;
				}
				if (!FontWP7.hasTexture(ref FontWP7.fontBig, ch))
				{
					double num2 = 26.0;
					TextBlock textBlock2 = new TextBlock
					{
						Text = ch.ToString(),
						FontSize = num2,
						Foreground = FontWP7.whiteBrush
					};
					TextBlock textBlock3 = new TextBlock
					{
						Text = ch.ToString(),
						FontSize = num2,
						Foreground = FontWP7.blackBrush
					};
					Canvas canvas2 = new Canvas();
					canvas2.Children.Add(textBlock3);
					WriteableBitmap writeableBitmap2 = new WriteableBitmap((int)textBlock3.ActualWidth + 3, (int)textBlock3.ActualHeight + 3);
					Canvas.SetLeft(textBlock3, 1.0);
					Canvas.SetTop(textBlock3, 0.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 1.0);
					Canvas.SetTop(textBlock3, 2.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 0.0);
					Canvas.SetTop(textBlock3, 1.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 2.0);
					Canvas.SetTop(textBlock3, 1.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 0.0);
					Canvas.SetTop(textBlock3, 0.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 0.0);
					Canvas.SetTop(textBlock3, 2.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 2.0);
					Canvas.SetTop(textBlock3, 0.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas.SetLeft(textBlock3, 1.0);
					Canvas.SetTop(textBlock3, 3.0);
					writeableBitmap2.Render(canvas2, null);
					Canvas canvas3 = new Canvas();
					canvas3.Children.Add(textBlock2);
					Canvas.SetLeft(textBlock2, 1.0);
					Canvas.SetTop(textBlock2, 1.0);
					writeableBitmap2.Render(canvas3, null);
					writeableBitmap2.Invalidate();
					FontWP7.fontBig.Add(ch, FontWP7.Texture2DToImage(FontWP7.WriteableBitmaptoTexture2D(writeableBitmap2)));
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public static void PrepareToDrawString(string text, int fontId)
		{
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				FontWP7.PrepareToDrawChar(c, fontId);
			}
		}

		// Token: 0x040007E5 RID: 2021
		private const float ADD_TO_HEIGHT = 10f;

		// Token: 0x040007E6 RID: 2022
		private float height = -1f;

		// Token: 0x040007E7 RID: 2023
		private int fontId = -1;

		// Token: 0x040007E8 RID: 2024
		private Dictionary<char, Image> font;

		// Token: 0x040007E9 RID: 2025
		private static Dictionary<char, Image> fontSmall = new Dictionary<char, Image>();

		// Token: 0x040007EA RID: 2026
		private static Dictionary<char, Image> fontBig = new Dictionary<char, Image>();

		// Token: 0x040007EB RID: 2027
		private static FontGeneric fontSmallOriginal;

		// Token: 0x040007EC RID: 2028
		private static FontGeneric fontBigOriginal;

		// Token: 0x040007ED RID: 2029
		private static SolidColorBrush whiteBrush = new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));

		// Token: 0x040007EE RID: 2030
		private static SolidColorBrush blackBrush = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 0, 0));

		// Token: 0x02000034 RID: 52
		internal class CharPosition
		{
			// Token: 0x040007EF RID: 2031
			public float x;

			// Token: 0x040007F0 RID: 2032
			public float y;

			// Token: 0x040007F1 RID: 2033
			public char ch;
		}
	}
}
