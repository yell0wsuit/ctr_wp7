using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200007D RID: 125
    internal class RotatableScalableMultiParticles : ScalableMultiParticles
    {
        // Token: 0x060003AB RID: 939 RVA: 0x000179B8 File Offset: 0x00015BB8
        public override void initParticle(ref Particle particle)
        {
            base.initParticle(ref particle);
            particle.angle = initialAngle;
            particle.deltaAngle = MathHelper.DEGREES_TO_RADIANS(rotateSpeed + rotateSpeedVar * MathHelper.RND_MINUS1_1);
            particle.deltaSize = (endSize - size) / particle.life;
        }

        // Token: 0x060003AC RID: 940 RVA: 0x00017A10 File Offset: 0x00015C10
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
                Vector vector3 = MathHelper.vectAdd(MathHelper.vectAdd(vector, vector2), gravity);
                vector3 = MathHelper.vectMult(vector3, delta);
                p.dir = MathHelper.vectAdd(p.dir, vector3);
                vector3 = MathHelper.vectMult(p.dir, delta);
                p.pos = MathHelper.vectAdd(p.pos, vector3);
                p.color.r = p.color.r + p.deltaColor.r * delta;
                p.color.g = p.color.g + p.deltaColor.g * delta;
                p.color.b = p.color.b + p.deltaColor.b * delta;
                p.color.a = p.color.a + p.deltaColor.a * delta;
                p.size += p.deltaSize * delta;
                p.life -= delta;
                float num = p.width * p.size;
                float num2 = p.height * p.size;
                float num3 = p.pos.x - num / 2f;
                float num4 = p.pos.y - num2 / 2f;
                float num5 = p.pos.x + num / 2f;
                float num6 = p.pos.y - num2 / 2f;
                float num7 = p.pos.x - num / 2f;
                float num8 = p.pos.y + num2 / 2f;
                float num9 = p.pos.x + num / 2f;
                float num10 = p.pos.y + num2 / 2f;
                float x2 = p.pos.x;
                float y = p.pos.y;
                Vector vector4 = MathHelper.vect(num3, num4);
                Vector vector5 = MathHelper.vect(num5, num6);
                Vector vector6 = MathHelper.vect(num7, num8);
                Vector vector7 = MathHelper.vect(num9, num10);
                p.angle += p.deltaAngle * delta;
                float num11 = MathHelper.cosf(p.angle);
                float num12 = MathHelper.sinf(p.angle);
                vector4 = Particles.rotatePreCalc(vector4, num11, num12, x2, y);
                vector5 = Particles.rotatePreCalc(vector5, num11, num12, x2, y);
                vector6 = Particles.rotatePreCalc(vector6, num11, num12, x2, y);
                vector7 = Particles.rotatePreCalc(vector7, num11, num12, x2, y);
                drawer.vertices[particleIdx] = Quad3D.MakeQuad3DEx(vector4.x, vector4.y, vector5.x, vector5.y, vector6.x, vector6.y, vector7.x, vector7.y);
                for (int i = 0; i < 4; i++)
                {
                    colors[particleIdx * 4 + i] = p.color;
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

        // Token: 0x060003AD RID: 941 RVA: 0x00017E68 File Offset: 0x00016068
        public override void update(float delta)
        {
            base.update(delta);
            if (active && emissionRate != 0f)
            {
                float num = 1f / emissionRate;
                emitCounter += delta;
                while (particleCount < totalParticles && emitCounter > num)
                {
                    addParticle();
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

        // Token: 0x0400093B RID: 2363
        public float initialAngle;

        // Token: 0x0400093C RID: 2364
        public float rotateSpeed;

        // Token: 0x0400093D RID: 2365
        public float rotateSpeedVar;
    }
}
