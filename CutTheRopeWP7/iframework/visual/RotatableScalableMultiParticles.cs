using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal sealed class RotatableScalableMultiParticles : ScalableMultiParticles
    {
        public override void initParticle(ref Particle particle)
        {
            base.initParticle(ref particle);
            particle.angle = initialAngle;
            particle.deltaAngle = DEGREES_TO_RADIANS(rotateSpeed + (rotateSpeedVar * RND_MINUS1_1));
            particle.deltaSize = (endSize - size) / particle.life;
        }

        public override void updateParticle(ref Particle p, float delta)
        {
            if (p.life > 0f)
            {
                Vector vector = vectZero;
                if (p.pos.x != 0f || p.pos.y != 0f)
                {
                    vector = vectNormalize(p.pos);
                }
                Vector vector2 = vector;
                vector = vectMult(vector, p.radialAccel);
                float x = vector2.x;
                vector2.x = -vector2.y;
                vector2.y = x;
                vector2 = vectMult(vector2, p.tangentialAccel);
                Vector vector3 = vectAdd(vectAdd(vector, vector2), gravity);
                vector3 = vectMult(vector3, delta);
                p.dir = vectAdd(p.dir, vector3);
                vector3 = vectMult(p.dir, delta);
                p.pos = vectAdd(p.pos, vector3);
                p.color.r += p.deltaColor.r * delta;
                p.color.g += p.deltaColor.g * delta;
                p.color.b += p.deltaColor.b * delta;
                p.color.a += p.deltaColor.a * delta;
                p.size += p.deltaSize * delta;
                p.life -= delta;
                float num = p.width * p.size;
                float num2 = p.height * p.size;
                float num3 = p.pos.x - (num / 2f);
                float num4 = p.pos.y - (num2 / 2f);
                float num5 = p.pos.x + (num / 2f);
                float num6 = p.pos.y - (num2 / 2f);
                float num7 = p.pos.x - (num / 2f);
                float num8 = p.pos.y + (num2 / 2f);
                float num9 = p.pos.x + (num / 2f);
                float num10 = p.pos.y + (num2 / 2f);
                float x2 = p.pos.x;
                float y = p.pos.y;
                Vector vector4 = vect(num3, num4);
                Vector vector5 = vect(num5, num6);
                Vector vector6 = vect(num7, num8);
                Vector vector7 = vect(num9, num10);
                p.angle += p.deltaAngle * delta;
                float num11 = cosf(p.angle);
                float num12 = sinf(p.angle);
                vector4 = rotatePreCalc(vector4, num11, num12, x2, y);
                vector5 = rotatePreCalc(vector5, num11, num12, x2, y);
                vector6 = rotatePreCalc(vector6, num11, num12, x2, y);
                vector7 = rotatePreCalc(vector7, num11, num12, x2, y);
                drawer.vertices[particleIdx] = Quad3D.MakeQuad3DEx(vector4.x, vector4.y, vector5.x, vector5.y, vector6.x, vector6.y, vector7.x, vector7.y);
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

        public float initialAngle;

        public float rotateSpeed;

        public float rotateSpeedVar;
    }
}
