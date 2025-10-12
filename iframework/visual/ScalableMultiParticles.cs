using System;
using ctre_wp7.iframework.helpers;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x0200007C RID: 124
	internal class ScalableMultiParticles : MultiParticles
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00017904 File Offset: 0x00015B04
		public override void initParticle(ref Particle particle)
		{
			Image imageGrid = this.imageGrid;
			int num = MathHelper.RND(imageGrid.texture.quadsCount - 1);
			Quad2D quad2D = imageGrid.texture.quads[num];
			Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
			Rectangle rectangle = imageGrid.texture.quadRects[num];
			this.drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, this.particleCount);
			base.initParticle(ref particle);
			particle.width = rectangle.w;
			particle.height = rectangle.h;
		}
	}
}
