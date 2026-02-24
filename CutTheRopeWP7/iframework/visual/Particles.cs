using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal class Particles : BaseElement
    {
        public static Vector rotatePreCalc(Vector v, float cosA, float sinA, float cx, float cy)
        {
            Vector vector = v;
            vector.x -= cx;
            vector.y -= cy;
            float num = (vector.x * cosA) - (vector.y * sinA);
            float num2 = (vector.x * sinA) + (vector.y * cosA);
            vector.x = num + cx;
            vector.y = num2 + cy;
            return vector;
        }

        public virtual void updateParticle(ref Particle p, float delta)
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
                p.life -= delta;
                vertices[particleIdx].x = p.pos.x;
                vertices[particleIdx].y = p.pos.y;
                vertices[particleIdx].size = p.size;
                colors[particleIdx] = p.color;
                particleIdx++;
                return;
            }
            if (particleIdx != particleCount - 1)
            {
                particles[particleIdx] = particles[particleCount - 1];
            }
            particleCount--;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (particlesDelegate != null && particleCount == 0 && !active)
            {
                particlesDelegate(this);
                return;
            }
            if (vertices == null)
            {
                return;
            }
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
            OpenGL.glBindBuffer(2, verticesID);
            OpenGL.glBufferData(2, vertices, 3);
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glBufferData(2, colors, 3);
            OpenGL.glBindBuffer(2, 0U);
        }

        public override void dealloc()
        {
            particles = null;
            vertices = null;
            colors = null;
            OpenGL.glDeleteBuffers(1, ref verticesID);
            OpenGL.glDeleteBuffers(1, ref colorsID);
            texture = null;
            base.dealloc();
        }

        public override void draw()
        {
            preDraw();
            postDraw();
        }

        public virtual Particles initWithTotalParticles(int numberOfParticles)
        {
            if (base.init() == null)
            {
                return null;
            }
            width = (int)SCREEN_WIDTH;
            height = (int)SCREEN_HEIGHT;
            totalParticles = numberOfParticles;
            particles = new Particle[totalParticles];
            vertices = new PointSprite[totalParticles];
            colors = new RGBAColor[totalParticles];
            if (particles == null || vertices == null || colors == null)
            {
                particles = null;
                vertices = null;
                colors = null;
                return null;
            }
            active = false;
            blendAdditive = false;
            OpenGL.glGenBuffers(1, ref verticesID);
            OpenGL.glGenBuffers(1, ref colorsID);
            return this;
        }

        public virtual bool addParticle()
        {
            if (isFull())
            {
                return false;
            }
            initParticle(ref particles[particleCount]);
            particleCount++;
            return true;
        }

        public virtual void initParticle(ref Particle particle)
        {
            particle.pos.x = x + (posVar.x * RND_MINUS1_1);
            particle.pos.y = y + (posVar.y * RND_MINUS1_1);
            particle.startPos = particle.pos;
            float num = DEGREES_TO_RADIANS(angle + (angleVar * RND_MINUS1_1));
            Vector vector;
            vector.y = sinf(num);
            vector.x = cosf(num);
            float num2 = speed + (speedVar * RND_MINUS1_1);
            particle.dir = vectMult(vector, num2);
            particle.radialAccel = radialAccel + (radialAccelVar * RND_MINUS1_1);
            particle.tangentialAccel = tangentialAccel + (tangentialAccelVar * RND_MINUS1_1);
            particle.life = life + (lifeVar * RND_MINUS1_1);
            RGBAColor rgbacolor = default;
            rgbacolor.r = startColor.r + (startColorVar.r * RND_MINUS1_1);
            rgbacolor.g = startColor.g + (startColorVar.g * RND_MINUS1_1);
            rgbacolor.b = startColor.b + (startColorVar.b * RND_MINUS1_1);
            rgbacolor.a = startColor.a + (startColorVar.a * RND_MINUS1_1);
            RGBAColor rgbacolor2 = default;
            rgbacolor2.r = endColor.r + (endColorVar.r * RND_MINUS1_1);
            rgbacolor2.g = endColor.g + (endColorVar.g * RND_MINUS1_1);
            rgbacolor2.b = endColor.b + (endColorVar.b * RND_MINUS1_1);
            rgbacolor2.a = endColor.a + (endColorVar.a * RND_MINUS1_1);
            particle.color = rgbacolor;
            particle.deltaColor.r = (rgbacolor2.r - rgbacolor.r) / particle.life;
            particle.deltaColor.g = (rgbacolor2.g - rgbacolor.g) / particle.life;
            particle.deltaColor.b = (rgbacolor2.b - rgbacolor.b) / particle.life;
            particle.deltaColor.a = (rgbacolor2.a - rgbacolor.a) / particle.life;
            particle.size = size + (sizeVar * RND_MINUS1_1);
        }

        public virtual void startSystem(int initialParticles)
        {
            particleCount = 0;
            while (particleCount < initialParticles)
            {
                _ = addParticle();
            }
            active = true;
        }

        public virtual void stopSystem()
        {
            active = false;
            elapsed = duration;
            emitCounter = 0f;
        }

        public virtual void resetSystem()
        {
            elapsed = 0f;
            emitCounter = 0f;
        }

        public virtual bool isFull()
        {
            return particleCount == totalParticles;
        }

        public virtual void setBlendAdditive(bool b)
        {
            blendAdditive = b;
        }

        public bool active;

        public float duration;

        public float elapsed;

        public Vector gravity;

        public Vector posVar;

        public float angle;

        public float angleVar;

        public float speed;

        public float speedVar;

        public float tangentialAccel;

        public float tangentialAccelVar;

        public float radialAccel;

        public float radialAccelVar;

        public float size;

        public float endSize;

        public float sizeVar;

        public float life;

        public float lifeVar;

        public RGBAColor startColor;

        public RGBAColor startColorVar;

        public RGBAColor endColor;

        public RGBAColor endColorVar;

        public Particle[] particles;

        public int totalParticles;

        public int particleCount;

        public bool blendAdditive;

        public bool colorModulate;

        public float emissionRate;

        public float emitCounter;

        public Texture2D texture;

        public PointSprite[] vertices;

        public RGBAColor[] colors;

        private uint verticesID;

        public uint colorsID;

        public int particleIdx;

        public ParticlesFinished particlesDelegate;

        // (Invoke) Token: 0x0600039B RID: 923
        public delegate void ParticlesFinished(Particles p);
    }
}
