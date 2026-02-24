namespace ctr_wp7.iframework.visual
{
    internal class ScalableMultiParticles : MultiParticles
    {
        public override void initParticle(ref Particle particle)
        {
            Image imageGrid = this.imageGrid;
            int num = RND(imageGrid.texture.quadsCount - 1);
            Quad2D quad2D = imageGrid.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            Rectangle rectangle = imageGrid.texture.quadRects[num];
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            base.initParticle(ref particle);
            particle.width = rectangle.w;
            particle.height = rectangle.h;
        }
    }
}
