using System;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x0200007A RID: 122
	internal class MultiParticles : Particles
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00016D6C File Offset: 0x00014F6C
		public virtual Particles initWithTotalParticlesandImageGrid(int numberOfParticles, Image image)
		{
			if (base.init() == null)
			{
				return null;
			}
			this.imageGrid = image;
			this.drawer = new ImageMultiDrawer().initWithImageandCapacity(this.imageGrid, numberOfParticles);
			this.width = (int)FrameworkTypes.SCREEN_WIDTH;
			this.height = (int)FrameworkTypes.SCREEN_HEIGHT;
			this.totalParticles = numberOfParticles;
			this.particles = new Particle[this.totalParticles];
			this.colors = new RGBAColor[4 * this.totalParticles];
			if (this.particles == null || this.colors == null)
			{
				this.particles = null;
				this.colors = null;
				return null;
			}
			this.active = false;
			this.blendAdditive = false;
			OpenGL.glGenBuffers(1, ref this.colorsID);
			return this;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00016E20 File Offset: 0x00015020
		public override void initParticle(ref Particle particle)
		{
			Image image = this.imageGrid;
			int num = MathHelper.RND(image.texture.quadsCount - 1);
			Quad2D quad2D = image.texture.quads[num];
			Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
			Rectangle rectangle = image.texture.quadRects[num];
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.particleCount);
			base.initParticle(ref particle);
			particle.width = rectangle.w * particle.size;
			particle.height = rectangle.h * particle.size;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00016ED8 File Offset: 0x000150D8
		public override void updateParticle(ref Particle p, float delta)
		{
			if (p.life > 0f)
			{
				Vector vector = MathHelper.vectZero;
				if (p.pos.x != 0f || p.pos.y != 0f)
				{
					vector = MathHelper.vectNormalize(p.pos);
				}
				Vector vector2 = vector;
				vector = MathHelper.vectMult(vector, p.radialAccel);
				float x = vector2.x;
				vector2.x = -vector2.y;
				vector2.y = x;
				vector2 = MathHelper.vectMult(vector2, p.tangentialAccel);
				Vector vector3 = MathHelper.vectAdd(MathHelper.vectAdd(vector, vector2), this.gravity);
				vector3 = MathHelper.vectMult(vector3, delta);
				p.dir = MathHelper.vectAdd(p.dir, vector3);
				vector3 = MathHelper.vectMult(p.dir, delta);
				p.pos = MathHelper.vectAdd(p.pos, vector3);
				p.color.r = p.color.r + p.deltaColor.r * delta;
				p.color.g = p.color.g + p.deltaColor.g * delta;
				p.color.b = p.color.b + p.deltaColor.b * delta;
				p.color.a = p.color.a + p.deltaColor.a * delta;
				p.life -= delta;
				this.drawer.vertices[this.particleIdx] = Quad3D.MakeQuad3D((double)(p.pos.x - p.width / 2f), (double)(p.pos.y - p.height / 2f), 0.0, (double)p.width, (double)p.height);
				for (int i = 0; i < 4; i++)
				{
					this.colors[this.particleIdx * 4 + i] = p.color;
				}
				this.particleIdx++;
				return;
			}
			if (this.particleIdx != this.particleCount - 1)
			{
				this.particles[this.particleIdx] = this.particles[this.particleCount - 1];
				this.drawer.vertices[this.particleIdx] = this.drawer.vertices[this.particleCount - 1];
				this.drawer.texCoordinates[this.particleIdx] = this.drawer.texCoordinates[this.particleCount - 1];
			}
			this.particleCount--;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000171A4 File Offset: 0x000153A4
		public override void update(float delta)
		{
			base.update(delta);
			if (this.active && this.emissionRate != 0f)
			{
				float num = 1f / this.emissionRate;
				this.emitCounter += delta;
				while (this.particleCount < this.totalParticles && this.emitCounter > num)
				{
					this.addParticle();
					this.emitCounter -= num;
				}
				this.elapsed += delta;
				if (this.duration != -1f && this.duration < this.elapsed)
				{
					this.stopSystem();
				}
			}
			this.particleIdx = 0;
			while (this.particleIdx < this.particleCount)
			{
				this.updateParticle(ref this.particles[this.particleIdx], delta);
			}
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glBufferData(2, this.colors, 3);
			OpenGL.glBindBuffer(2, 0U);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00017298 File Offset: 0x00015498
		public override void draw()
		{
			this.preDraw();
			if (this.blendAdditive)
			{
				OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE);
			}
			else
			{
				OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			}
			OpenGL.glEnable(0);
			OpenGL.glBindTexture(this.drawer.image.texture.name());
			OpenGL.glVertexPointer(3, 5, 0, FrameworkTypes.toFloatArray(this.drawer.vertices));
			OpenGL.glTexCoordPointer(2, 5, 0, FrameworkTypes.toFloatArray(this.drawer.texCoordinates));
			OpenGL.glEnableClientState(13);
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glColorPointer(4, 5, 0, this.colors);
			OpenGL.glDrawElements(7, this.particleIdx * 6, this.drawer.indices);
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glBindBuffer(2, 0U);
			OpenGL.glDisableClientState(13);
			this.postDraw();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00017374 File Offset: 0x00015574
		public override void dealloc()
		{
			this.drawer = null;
			this.imageGrid = null;
			base.dealloc();
		}

		// Token: 0x04000937 RID: 2359
		public ImageMultiDrawer drawer;

		// Token: 0x04000938 RID: 2360
		public Image imageGrid;
	}
}
