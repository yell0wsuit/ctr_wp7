using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200007B RID: 123
    internal class RotateableMultiParticles : MultiParticles
    {
        // Token: 0x060003A5 RID: 933 RVA: 0x00017392 File Offset: 0x00015592
        public override void initParticle(ref Particle particle)
        {
            base.initParticle(ref particle);
            particle.angle = 0f;
            particle.deltaAngle = MathHelper.DEGREES_TO_RADIANS(rotateSpeed + rotateSpeedVar * MathHelper.RND_MINUS1_1);
        }

        // Token: 0x060003A6 RID: 934 RVA: 0x000173C4 File Offset: 0x000155C4
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
                p.life -= delta;
                float num = p.pos.x - p.width / 2f;
                float num2 = p.pos.y - p.height / 2f;
                float num3 = p.pos.x + p.width / 2f;
                float num4 = p.pos.y - p.height / 2f;
                float num5 = p.pos.x - p.width / 2f;
                float num6 = p.pos.y + p.height / 2f;
                float num7 = p.pos.x + p.width / 2f;
                float num8 = p.pos.y + p.height / 2f;
                float x2 = p.pos.x;
                float y = p.pos.y;
                Vector vector4 = MathHelper.vect(num, num2);
                Vector vector5 = MathHelper.vect(num3, num4);
                Vector vector6 = MathHelper.vect(num5, num6);
                Vector vector7 = MathHelper.vect(num7, num8);
                p.angle += p.deltaAngle * delta;
                float num9 = MathHelper.cosf(p.angle);
                float num10 = MathHelper.sinf(p.angle);
                vector4 = Particles.rotatePreCalc(vector4, num9, num10, x2, y);
                vector5 = Particles.rotatePreCalc(vector5, num9, num10, x2, y);
                vector6 = Particles.rotatePreCalc(vector6, num9, num10, x2, y);
                vector7 = Particles.rotatePreCalc(vector7, num9, num10, x2, y);
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

        // Token: 0x060003A7 RID: 935 RVA: 0x00017808 File Offset: 0x00015A08
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

        // Token: 0x04000939 RID: 2361
        public float rotateSpeed;

        // Token: 0x0400093A RID: 2362
        public float rotateSpeedVar;
    }
}
