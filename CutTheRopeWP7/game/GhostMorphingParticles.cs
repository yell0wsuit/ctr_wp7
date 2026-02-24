using ctr_wp7.iframework;
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
            angle += (float)(360 / totalParticles);
            int num = RND_RANGE(2, 4);
            Quad2D quad2D = imageGrid.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            Rectangle rectangle = imageGrid.texture.quadRects[num];
            float num2 = size + FLOAT_RND_RANGE(-1, 1) * sizeVar;
            particle.width = rectangle.w * num2;
            particle.height = rectangle.h * num2;
            particle.deltaColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0f);
        }

        // Token: 0x06000443 RID: 1091 RVA: 0x0001DBCC File Offset: 0x0001BDCC
        public new virtual NSObject initWithTotalParticles(int numberOfParticles)
        {
            if (base.initWithTotalParticlesandImageGrid(numberOfParticles, Image.Image_createWithResID(180)) != null)
            {
                size = 0.6f;
                sizeVar = 0.2f;
                angle = (float)RND_RANGE(0, 360);
                angleVar = 15f;
                rotateSpeedVar = 30f;
                life = 0.4f;
                lifeVar = 0.15f;
                duration = 1.5f;
                speed = 60f;
                speedVar = 15f;
                startColor = RGBAColor.solidOpaqueRGBA;
                endColor = RGBAColor.transparentRGBA;
            }
            return this;
        }

        // Token: 0x06000444 RID: 1092 RVA: 0x0001DC7C File Offset: 0x0001BE7C
        public override void update(float delta)
        {
            base.update(delta);
            for (int i = 0; i < particleCount; i++)
            {
                Particle particle = particles[i];
                if (particle.life > 0f)
                {
                    float num = 0.7f * life;
                    if (particle.life < num)
                    {
                        particle.deltaColor.r = (endColor.r - startColor.r) / num;
                        particle.deltaColor.g = (endColor.g - startColor.g) / num;
                        particle.deltaColor.b = (endColor.b - startColor.b) / num;
                        particle.deltaColor.a = (endColor.a - startColor.a) / num;
                    }
                    particle.dir = vectMult(particle.dir, 0.83);
                    particle.width *= 1.015f;
                    particle.height *= 1.015f;
                }
            }
        }
    }
}
