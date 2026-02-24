using System;
using ctr_wp7.iframework;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
	// Token: 0x0200008E RID: 142
	internal class GhostMorphingParticles : RotateableMultiParticles
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		public override void initParticle(ref Particle particle)
		{
			base.initParticle(ref particle);
			this.angle += (float)(360 / this.totalParticles);
			int num = MathHelper.RND_RANGE(2, 4);
			Quad2D quad2D = this.imageGrid.texture.quads[num];
			Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.particleCount);
			Rectangle rectangle = this.imageGrid.texture.quadRects[num];
			float num2 = this.size + MathHelper.FLOAT_RND_RANGE(-1, 1) * this.sizeVar;
			particle.width = rectangle.w * num2;
			particle.height = rectangle.h * num2;
			particle.deltaColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0f);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public new virtual NSObject initWithTotalParticles(int numberOfParticles)
		{
			if (base.initWithTotalParticlesandImageGrid(numberOfParticles, Image.Image_createWithResID(180)) != null)
			{
				this.size = 0.6f;
				this.sizeVar = 0.2f;
				this.angle = (float)MathHelper.RND_RANGE(0, 360);
				this.angleVar = 15f;
				this.rotateSpeedVar = 30f;
				this.life = 0.4f;
				this.lifeVar = 0.15f;
				this.duration = 1.5f;
				this.speed = 60f;
				this.speedVar = 15f;
				this.startColor = RGBAColor.solidOpaqueRGBA;
				this.endColor = RGBAColor.transparentRGBA;
			}
			return this;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public override void update(float delta)
		{
			base.update(delta);
			for (int i = 0; i < this.particleCount; i++)
			{
				Particle particle = this.particles[i];
				if (particle.life > 0f)
				{
					float num = 0.7f * this.life;
					if (particle.life < num)
					{
						particle.deltaColor.r = (this.endColor.r - this.startColor.r) / num;
						particle.deltaColor.g = (this.endColor.g - this.startColor.g) / num;
						particle.deltaColor.b = (this.endColor.b - this.startColor.b) / num;
						particle.deltaColor.a = (this.endColor.a - this.startColor.a) / num;
					}
					particle.dir = MathHelper.vectMult(particle.dir, 0.83);
					particle.width *= 1.015f;
					particle.height *= 1.015f;
				}
			}
		}
	}
}
