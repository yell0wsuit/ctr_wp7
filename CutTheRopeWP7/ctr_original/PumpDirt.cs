using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.ctr_original
{
    internal sealed class PumpDirt : MultiParticles
    {
        public PumpDirt initWithTotalParticlesAngleandImageGrid(int p, float a, Image grid)
        {
            if (initWithTotalParticlesandImageGrid(p, grid) == null)
            {
                return null;
            }
            duration = 0.6f;
            gravity.x = 0f;
            gravity.y = 0f;
            angle = a;
            angleVar = 10f;
            speed = 500f;
            speedVar = 100f;
            radialAccel = 0f;
            radialAccelVar = 0f;
            tangentialAccel = 0f;
            tangentialAccelVar = 0f;
            posVar.x = 0f;
            posVar.y = 0f;
            life = 0.6f;
            lifeVar = 0f;
            size = 0.002f;
            sizeVar = 0f;
            emissionRate = 100f;
            startColor.r = 1f;
            startColor.g = 1f;
            startColor.b = 1f;
            startColor.a = 0.6f;
            startColorVar.r = 0f;
            startColorVar.g = 0f;
            startColorVar.b = 0f;
            startColorVar.a = 0f;
            endColor.r = 1f;
            endColor.g = 1f;
            endColor.b = 1f;
            endColor.a = 0f;
            endColorVar.r = 0f;
            endColorVar.g = 0f;
            endColorVar.b = 0f;
            endColorVar.a = 0f;
            blendAdditive = true;
            return this;
        }

        public override void initParticle(ref Particle particle)
        {
            base.initParticle(ref particle);
            int num = RND_RANGE(6, 8);
            Quad2D quad2D = imageGrid.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            particle.width = 10f;
            particle.height = 10f;
        }

        public override void updateParticle(ref Particle p, float delta)
        {
            if (p.life > 0f)
            {
                p.dir = vectMult(p.dir, 0.9);
                Vector vector = vectMult(p.dir, delta);
                vector = vectAdd(vector, gravity);
                p.pos = vectAdd(p.pos, vector);
                p.color.r += p.deltaColor.r * delta;
                p.color.g += p.deltaColor.g * delta;
                p.color.b += p.deltaColor.b * delta;
                p.color.a += p.deltaColor.a * delta;
                p.life -= delta;
                drawer.vertices[particleIdx] = Quad3D.MakeQuad3D((double)(p.pos.x - (p.width / 2f)), (double)(p.pos.y - (p.height / 2f)), 0.0, p.width, p.height);
                for (int i = 0; i < 4; i++)
                {
                    colors[(particleIdx * 4) + i] = p.color;
                }
                particleIdx++;
                return;
            }
            if (particleIdx != particleCount - 1)
            {
                particles[particleIdx] = particles[particleCount - 1];
                drawer.vertices[particleIdx] = drawer.vertices[particleCount - 1];
                drawer.texCoordinates[particleIdx] = drawer.texCoordinates[particleCount - 1];
            }
            particleCount--;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (active && emissionRate != 0f)
            {
                float num = 1f / emissionRate;
                emitCounter += delta;
                while (particleCount < totalParticles && emitCounter > num)
                {
                    _ = addParticle();
                    emitCounter -= num;
                }
                elapsed += delta;
                if (duration != -1f && duration < elapsed)
                {
                    stopSystem();
                }
            }
            particleIdx = 0;
            while (particleIdx < particleCount)
            {
                updateParticle(ref particles[particleIdx], delta);
            }
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glBufferData(2, colors, 3);
            OpenGL.glBindBuffer(2, 0U);
        }
    }
}
