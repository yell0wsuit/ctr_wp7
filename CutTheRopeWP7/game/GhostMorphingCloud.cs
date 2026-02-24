using ctr_wp7.iframework;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x0200008F RID: 143
    internal class GhostMorphingCloud : MultiParticles
    {
        // Token: 0x06000446 RID: 1094 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
        public override NSObject init()
        {
            if (base.initWithTotalParticlesandImageGrid(5, Image.Image_createWithResID(180)) != null)
            {
                angle = (float)RND_RANGE(0, 360);
                size = 1.6f;
                angleVar = 360f;
                life = 0.5f;
                duration = 1.5f;
                speed = 30f;
                startColor = RGBAColor.solidOpaqueRGBA;
                endColor = RGBAColor.transparentRGBA;
            }
            return this;
        }

        // Token: 0x06000447 RID: 1095 RVA: 0x0001DE44 File Offset: 0x0001C044
        public override void initParticle(ref Particle particle)
        {
            angle += 360f / (float)totalParticles;
            base.initParticle(ref particle);
            int num = RND_RANGE(2, 4);
            Quad2D quad2D = imageGrid.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            Rectangle rectangle = imageGrid.texture.quadRects[num];
            particle.width = rectangle.w * size;
            particle.height = rectangle.h * size;
            particle.deltaColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0f);
        }

        // Token: 0x06000448 RID: 1096 RVA: 0x0001DF2C File Offset: 0x0001C12C
        public override void update(float delta)
        {
            base.update(delta);
            for (int i = 0; i < particleCount; i++)
            {
                Particle particle = particles[i];
                if (particle.life > 0f)
                {
                    float num = 0.2f * life;
                    if (particle.life > life - num)
                    {
                        float num2 = 1.025f;
                        particle.width *= num2;
                        particle.height *= num2;
                    }
                    else
                    {
                        particle.deltaColor.r = (endColor.r - startColor.r) / num;
                        particle.deltaColor.g = (endColor.g - startColor.g) / num;
                        particle.deltaColor.b = (endColor.b - startColor.b) / num;
                        particle.deltaColor.a = (endColor.a - startColor.a) / num;
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
