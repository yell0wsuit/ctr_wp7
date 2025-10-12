using System;
using ctre_wp7.iframework;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;

namespace ctre_wp7.ctr_original
{
	// Token: 0x02000103 RID: 259
	internal class CandyBreak : RotateableMultiParticles
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x0003D930 File Offset: 0x0003BB30
		public override Particles initWithTotalParticlesandImageGrid(int p, Image grid)
		{
			if (base.initWithTotalParticlesandImageGrid(p, grid) == null)
			{
				return null;
			}
			this.duration = 2f;
			this.gravity.x = 0f;
			this.gravity.y = 500f;
			this.angle = -90f;
			this.angleVar = 50f;
			this.speed = 150f;
			this.speedVar = 70f;
			this.radialAccel = 0f;
			this.radialAccelVar = 1f;
			this.tangentialAccel = 0f;
			this.tangentialAccelVar = 1f;
			this.posVar.x = 0f;
			this.posVar.y = 0f;
			this.life = 2f;
			this.lifeVar = 0f;
			this.size = 0.1f;
			this.sizeVar = 0f;
			this.emissionRate = 100f;
			this.startColor.r = 1f;
			this.startColor.g = 1f;
			this.startColor.b = 1f;
			this.startColor.a = 1f;
			this.startColorVar.r = 0f;
			this.startColorVar.g = 0f;
			this.startColorVar.b = 0f;
			this.startColorVar.a = 0f;
			this.endColor.r = 1f;
			this.endColor.g = 1f;
			this.endColor.b = 1f;
			this.endColor.a = 1f;
			this.endColorVar.r = 0f;
			this.endColorVar.g = 0f;
			this.endColorVar.b = 0f;
			this.endColorVar.a = 0f;
			this.rotateSpeed = 0f;
			this.rotateSpeedVar = 600f;
			this.blendAdditive = false;
			return this;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003DB44 File Offset: 0x0003BD44
		public override void initParticle(ref Particle particle)
		{
			base.initParticle(ref particle);
			int num = MathHelper.RND_RANGE(3, 7);
			Quad2D quad2D = this.imageGrid.texture.quads[num];
			Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.particleCount);
			particle.width = 16f;
			particle.height = 16f;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		public override void draw()
		{
			this.preDraw();
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glEnable(0);
			OpenGL.glBindTexture(this.drawer.image.texture.name());
			OpenGL.glVertexPointer(3, 5, 0, FrameworkTypes.toFloatArray(this.drawer.vertices));
			OpenGL.glTexCoordPointer(2, 5, 0, FrameworkTypes.toFloatArray(this.drawer.texCoordinates));
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glDrawElements(7, this.particleIdx * 6, this.drawer.indices);
			OpenGL.glBindBuffer(2, 0U);
			OpenGL.glDisableClientState(13);
			this.postDraw();
		}
	}
}
