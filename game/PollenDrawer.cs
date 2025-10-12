using System;
using System.Linq;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x020000D4 RID: 212
	internal class PollenDrawer : BaseElement
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		public override NSObject init()
		{
			if (base.init() != null)
			{
				Image image = Image.Image_createWithResID(149);
				this.qw = (float)image.width;
				this.qh = (float)image.height;
				this.totalCapacity = 200;
				this.drawer = new ImageMultiDrawer().initWithImageandCapacity(image, this.totalCapacity);
				this.pollens = new Pollen[this.totalCapacity];
				this.colors = new RGBAColor[4 * this.totalCapacity];
				OpenGL.glGenBuffers(1, ref this.colorsID);
			}
			return this;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002FA80 File Offset: 0x0002DC80
		public override void dealloc()
		{
			if (this.pollens != null)
			{
				this.pollens = null;
			}
			if (this.colors != null)
			{
				this.colors = null;
				OpenGL.glDeleteBuffers(1, ref this.colorsID);
			}
			if (this.vertices != null)
			{
				this.vertices = null;
				OpenGL.glDeleteBuffers(1, ref this.verticesID);
			}
			this.drawer = null;
			base.dealloc();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public virtual void addPollenAtparentIndex(Vector v, int pi)
		{
			float num = 1f;
			float num2 = 1f;
			float[] array = new float[] { 0.3f, 0.3f, 0.5f, 0.5f, 0.6f };
			int num3 = Enumerable.Count<float>(array);
			float num4 = array[MathHelper.RND_RANGE(0, num3 - 1)];
			float num5 = num4;
			if (MathHelper.RND(1) == 1)
			{
				num4 *= 1f + (float)MathHelper.RND(1) / 10f;
			}
			else
			{
				num5 *= 1f + (float)MathHelper.RND(1) / 10f;
			}
			num *= num4;
			num2 *= num5;
			int num6 = (int)this.qw;
			int num7 = (int)this.qh;
			num6 *= (int)num;
			num7 *= (int)num2;
			Pollen pollen;
			pollen.parentIndex = pi;
			pollen.x = v.x;
			pollen.y = v.y;
			float num8 = 1f;
			float num9 = Math.Min(num8 - num, num8 - num2);
			float rnd_0_ = MathHelper.RND_0_1;
			pollen.startScaleX = num9 + num;
			pollen.startScaleY = num9 + num2;
			pollen.scaleX = pollen.startScaleX * rnd_0_;
			pollen.scaleY = pollen.startScaleY * rnd_0_;
			pollen.endScaleX = num;
			pollen.endScaleY = num2;
			pollen.endAlpha = 0.3f;
			pollen.startAlpha = 1f;
			pollen.alpha = 0.7f * rnd_0_ + 0.3f;
			Quad2D quad2D = this.drawer.image.texture.quads[0];
			Quad3D quad3D = Quad3D.MakeQuad3D((double)(v.x - (float)(num6 / 2)), (double)(v.y - (float)(num7 / 2)), 0.0, (double)num6, (double)num7);
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.pollenCount);
			if (this.pollenCount >= this.totalCapacity)
			{
				this.totalCapacity = this.pollenCount;
				this.pollens = new Pollen[this.totalCapacity + 1];
				this.colors = new RGBAColor[4 * (this.totalCapacity + 1)];
			}
			for (int i = 0; i < 4; i++)
			{
				this.colors[this.pollenCount * 4 + i] = RGBAColor.whiteRGBA;
			}
			this.pollens[this.pollenCount] = pollen;
			this.pollenCount++;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0002FD53 File Offset: 0x0002DF53
		private int WVGAD(int V)
		{
			if (!FrameworkTypes.IS_WVGA)
			{
				return V;
			}
			return V * 2;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002FD64 File Offset: 0x0002DF64
		public virtual void fillWithPolenFromPathIndexToPathIndexGrab(int p1, int p2, Grab g)
		{
			int num = this.WVGAD(10);
			Vector vector = g.mover.path[p1];
			Vector vector2 = g.mover.path[p2];
			Vector vector3 = MathHelper.vectSub(vector2, vector);
			float num2 = MathHelper.vectLength(vector3);
			int num3 = (int)(num2 / (float)num);
			Vector vector4 = MathHelper.vectNormalize(vector3);
			for (int i = 0; i <= num3; i++)
			{
				Vector vector5 = MathHelper.vectAdd(vector, MathHelper.vectMult(vector4, (float)(i * num)));
				vector5.x += (float)MathHelper.RND_RANGE(this.WVGAD(-2), this.WVGAD(2));
				vector5.y += (float)MathHelper.RND_RANGE(this.WVGAD(-2), this.WVGAD(2));
				this.addPollenAtparentIndex(vector5, p1);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002FE3C File Offset: 0x0002E03C
		public override void update(float delta)
		{
			base.update(delta);
			this.drawer.update(delta);
			for (int i = 0; i < this.pollenCount; i++)
			{
				if (Mover.moveVariableToTarget(ref this.pollens[i].scaleX, this.pollens[i].endScaleX, 1f, delta))
				{
					float startScaleX = this.pollens[i].startScaleX;
					this.pollens[i].startScaleX = this.pollens[i].endScaleX;
					this.pollens[i].endScaleX = startScaleX;
				}
				if (Mover.moveVariableToTarget(ref this.pollens[i].scaleY, this.pollens[i].endScaleY, 1f, delta))
				{
					float startScaleY = this.pollens[i].startScaleY;
					this.pollens[i].startScaleY = this.pollens[i].endScaleY;
					this.pollens[i].endScaleY = startScaleY;
				}
				float num = this.qw * this.pollens[i].scaleX;
				float num2 = this.qh * this.pollens[i].scaleY;
				this.drawer.vertices[i] = Quad3D.MakeQuad3D((double)(this.pollens[i].x - num / 2f), (double)(this.pollens[i].y - num2 / 2f), 0.0, (double)num, (double)num2);
				if (Mover.moveVariableToTarget(ref this.pollens[i].alpha, this.pollens[i].endAlpha, 1f, delta))
				{
					float startAlpha = this.pollens[i].startAlpha;
					this.pollens[i].startAlpha = this.pollens[i].endAlpha;
					this.pollens[i].endAlpha = startAlpha;
				}
				float alpha = this.pollens[i].alpha;
				for (int j = 0; j < 4; j++)
				{
					this.colors[i * 4 + j] = RGBAColor.MakeRGBA(alpha, alpha, alpha, alpha);
				}
			}
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glBufferData(2, this.colors, 3);
			OpenGL.glBindBuffer(2, 0U);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000300CC File Offset: 0x0002E2CC
		public override void draw()
		{
			if (this.pollenCount < 2)
			{
				return;
			}
			this.preDraw();
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE);
			OpenGL.glEnable(0);
			OpenGL.glBindTexture(this.drawer.image.texture.name());
			OpenGL.glVertexPointer(3, 5, 0, FrameworkTypes.toFloatArray(this.drawer.vertices));
			OpenGL.glTexCoordPointer(2, 5, 0, FrameworkTypes.toFloatArray(this.drawer.texCoordinates));
			OpenGL.glEnableClientState(13);
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glBufferData(2, this.colors, 3);
			OpenGL.glColorPointer(4, 5, 0, this.colors);
			OpenGL.glDrawElements(7, (this.pollenCount - 1) * 6, this.drawer.indices);
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glBindBuffer(2, 0U);
			OpenGL.glDisableClientState(13);
			this.postDraw();
		}

		// Token: 0x04000B9E RID: 2974
		private ImageMultiDrawer drawer;

		// Token: 0x04000B9F RID: 2975
		private int pollenCount;

		// Token: 0x04000BA0 RID: 2976
		private int totalCapacity;

		// Token: 0x04000BA1 RID: 2977
		private Pollen[] pollens;

		// Token: 0x04000BA2 RID: 2978
		private float qw;

		// Token: 0x04000BA3 RID: 2979
		private float qh;

		// Token: 0x04000BA4 RID: 2980
		private RGBAColor[] colors;

		// Token: 0x04000BA5 RID: 2981
		private uint colorsID;

		// Token: 0x04000BA6 RID: 2982
		private PointSprite[] vertices;

		// Token: 0x04000BA7 RID: 2983
		private uint verticesID;
	}
}
