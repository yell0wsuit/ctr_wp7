using ctr_wp7.iframework;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.ctr_original
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
            duration = 2f;
            gravity.x = 0f;
            gravity.y = 500f;
            angle = -90f;
            angleVar = 50f;
            speed = 150f;
            speedVar = 70f;
            radialAccel = 0f;
            radialAccelVar = 1f;
            tangentialAccel = 0f;
            tangentialAccelVar = 1f;
            posVar.x = 0f;
            posVar.y = 0f;
            life = 2f;
            lifeVar = 0f;
            size = 0.1f;
            sizeVar = 0f;
            emissionRate = 100f;
            startColor.r = 1f;
            startColor.g = 1f;
            startColor.b = 1f;
            startColor.a = 1f;
            startColorVar.r = 0f;
            startColorVar.g = 0f;
            startColorVar.b = 0f;
            startColorVar.a = 0f;
            endColor.r = 1f;
            endColor.g = 1f;
            endColor.b = 1f;
            endColor.a = 1f;
            endColorVar.r = 0f;
            endColorVar.g = 0f;
            endColorVar.b = 0f;
            endColorVar.a = 0f;
            rotateSpeed = 0f;
            rotateSpeedVar = 600f;
            blendAdditive = false;
            return this;
        }

        // Token: 0x060007E2 RID: 2018 RVA: 0x0003DB44 File Offset: 0x0003BD44
        public override void initParticle(ref Particle particle)
        {
            base.initParticle(ref particle);
            int num = MathHelper.RND_RANGE(3, 7);
            Quad2D quad2D = imageGrid.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            particle.width = 16f;
            particle.height = 16f;
        }

        // Token: 0x060007E3 RID: 2019 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
        public override void draw()
        {
            preDraw();
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(drawer.image.texture.name());
            OpenGL.glVertexPointer(3, 5, 0, FrameworkTypes.toFloatArray(drawer.vertices));
            OpenGL.glTexCoordPointer(2, 5, 0, FrameworkTypes.toFloatArray(drawer.texCoordinates));
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glDrawElements(7, particleIdx * 6, drawer.indices);
            OpenGL.glBindBuffer(2, 0U);
            OpenGL.glDisableClientState(13);
            postDraw();
        }
    }
}
