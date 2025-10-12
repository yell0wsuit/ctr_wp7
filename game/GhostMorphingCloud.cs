using System;
using ctre_wp7.iframework;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x0200008F RID: 143
	internal class GhostMorphingCloud : MultiParticles
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public override NSObject init()
		{
			if (base.initWithTotalParticlesandImageGrid(5, Image.Image_createWithResID(180)) != null)
			{
				this.angle = (float)MathHelper.RND_RANGE(0, 360);
				this.size = 1.6f;
				this.angleVar = 360f;
				this.life = 0.5f;
				this.duration = 1.5f;
				this.speed = 30f;
				this.startColor = RGBAColor.solidOpaqueRGBA;
				this.endColor = RGBAColor.transparentRGBA;
			}
			return this;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001DE44 File Offset: 0x0001C044
		public override void initParticle(ref Particle particle)
		{
			this.angle += 360f / (float)this.totalParticles;
			base.initParticle(ref particle);
			int num = MathHelper.RND_RANGE(2, 4);
			Quad2D quad2D = this.imageGrid.texture.quads[num];
			Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.particleCount);
			Rectangle rectangle = this.imageGrid.texture.quadRects[num];
			particle.width = rectangle.w * this.size;
			particle.height = rectangle.h * this.size;
			particle.deltaColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0f);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001DF2C File Offset: 0x0001C12C
		public override void update(float delta)
		{
			base.update(delta);
			for (int i = 0; i < this.particleCount; i++)
			{
				Particle particle = this.particles[i];
				if (particle.life > 0f)
				{
					float num = 0.2f * this.life;
					if (particle.life > this.life - num)
					{
						float num2 = 1.025f;
						particle.width *= num2;
						particle.height *= num2;
					}
					else
					{
						particle.deltaColor.r = (this.endColor.r - this.startColor.r) / num;
						particle.deltaColor.g = (this.endColor.g - this.startColor.g) / num;
						particle.deltaColor.b = (this.endColor.b - this.startColor.b) / num;
						particle.deltaColor.a = (this.endColor.a - this.startColor.a) / num;
						float num3 = 0.98f;
						particle.width *= num3;
						particle.height *= num3;
					}
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001E07C File Offset: 0x0001C27C
		public virtual void startSystem()
		{
			base.startSystem(5);
		}

		// Token: 0x0400098F RID: 2447
		private const int CLOUD_PARTICLES = 5;
	}
}
