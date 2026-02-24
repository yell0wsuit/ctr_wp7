using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000078 RID: 120
    internal class Particles : BaseElement
    {
        // Token: 0x0600038C RID: 908 RVA: 0x000164CC File Offset: 0x000146CC
        public static Vector rotatePreCalc(Vector v, float cosA, float sinA, float cx, float cy)
        {
            Vector vector = v;
            vector.x -= cx;
            vector.y -= cy;
            float num = vector.x * cosA - vector.y * sinA;
            float num2 = vector.x * sinA + vector.y * cosA;
            vector.x = num + cx;
            vector.y = num2 + cy;
            return vector;
        }

        // Token: 0x0600038D RID: 909 RVA: 0x00016538 File Offset: 0x00014738
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
                p.color.r = p.color.r + p.deltaColor.r * delta;
                p.color.g = p.color.g + p.deltaColor.g * delta;
                p.color.b = p.color.b + p.deltaColor.b * delta;
                p.color.a = p.color.a + p.deltaColor.a * delta;
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

        // Token: 0x0600038E RID: 910 RVA: 0x00016774 File Offset: 0x00014974
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
            OpenGL.glBindBuffer(2, verticesID);
            OpenGL.glBufferData(2, vertices, 3);
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glBufferData(2, colors, 3);
            OpenGL.glBindBuffer(2, 0U);
        }

        // Token: 0x0600038F RID: 911 RVA: 0x000168AE File Offset: 0x00014AAE
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

        // Token: 0x06000390 RID: 912 RVA: 0x000168EA File Offset: 0x00014AEA
        public override void draw()
        {
            preDraw();
            postDraw();
        }

        // Token: 0x06000391 RID: 913 RVA: 0x000168F8 File Offset: 0x00014AF8
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

        // Token: 0x06000392 RID: 914 RVA: 0x000169B7 File Offset: 0x00014BB7
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

        // Token: 0x06000393 RID: 915 RVA: 0x000169EC File Offset: 0x00014BEC
        public virtual void initParticle(ref Particle particle)
        {
            particle.pos.x = x + posVar.x * RND_MINUS1_1;
            particle.pos.y = y + posVar.y * RND_MINUS1_1;
            particle.startPos = particle.pos;
            float num = DEGREES_TO_RADIANS(angle + angleVar * RND_MINUS1_1);
            Vector vector;
            vector.y = sinf(num);
            vector.x = cosf(num);
            float num2 = speed + speedVar * RND_MINUS1_1;
            particle.dir = vectMult(vector, num2);
            particle.radialAccel = radialAccel + radialAccelVar * RND_MINUS1_1;
            particle.tangentialAccel = tangentialAccel + tangentialAccelVar * RND_MINUS1_1;
            particle.life = life + lifeVar * RND_MINUS1_1;
            RGBAColor rgbacolor = default(RGBAColor);
            rgbacolor.r = startColor.r + startColorVar.r * RND_MINUS1_1;
            rgbacolor.g = startColor.g + startColorVar.g * RND_MINUS1_1;
            rgbacolor.b = startColor.b + startColorVar.b * RND_MINUS1_1;
            rgbacolor.a = startColor.a + startColorVar.a * RND_MINUS1_1;
            RGBAColor rgbacolor2 = default(RGBAColor);
            rgbacolor2.r = endColor.r + endColorVar.r * RND_MINUS1_1;
            rgbacolor2.g = endColor.g + endColorVar.g * RND_MINUS1_1;
            rgbacolor2.b = endColor.b + endColorVar.b * RND_MINUS1_1;
            rgbacolor2.a = endColor.a + endColorVar.a * RND_MINUS1_1;
            particle.color = rgbacolor;
            particle.deltaColor.r = (rgbacolor2.r - rgbacolor.r) / particle.life;
            particle.deltaColor.g = (rgbacolor2.g - rgbacolor.g) / particle.life;
            particle.deltaColor.b = (rgbacolor2.b - rgbacolor.b) / particle.life;
            particle.deltaColor.a = (rgbacolor2.a - rgbacolor.a) / particle.life;
            particle.size = size + sizeVar * RND_MINUS1_1;
        }

        // Token: 0x06000394 RID: 916 RVA: 0x00016CBF File Offset: 0x00014EBF
        public virtual void startSystem(int initialParticles)
        {
            particleCount = 0;
            while (particleCount < initialParticles)
            {
                addParticle();
            }
            active = true;
        }

        // Token: 0x06000395 RID: 917 RVA: 0x00016CE1 File Offset: 0x00014EE1
        public virtual void stopSystem()
        {
            active = false;
            elapsed = duration;
            emitCounter = 0f;
        }

        // Token: 0x06000396 RID: 918 RVA: 0x00016D01 File Offset: 0x00014F01
        public virtual void resetSystem()
        {
            elapsed = 0f;
            emitCounter = 0f;
        }

        // Token: 0x06000397 RID: 919 RVA: 0x00016D19 File Offset: 0x00014F19
        public virtual bool isFull()
        {
            return particleCount == totalParticles;
        }

        // Token: 0x06000398 RID: 920 RVA: 0x00016D29 File Offset: 0x00014F29
        public virtual void setBlendAdditive(bool b)
        {
            blendAdditive = b;
        }

        // Token: 0x04000913 RID: 2323
        public bool active;

        // Token: 0x04000914 RID: 2324
        public float duration;

        // Token: 0x04000915 RID: 2325
        public float elapsed;

        // Token: 0x04000916 RID: 2326
        public Vector gravity;

        // Token: 0x04000917 RID: 2327
        public Vector posVar;

        // Token: 0x04000918 RID: 2328
        public float angle;

        // Token: 0x04000919 RID: 2329
        public float angleVar;

        // Token: 0x0400091A RID: 2330
        public float speed;

        // Token: 0x0400091B RID: 2331
        public float speedVar;

        // Token: 0x0400091C RID: 2332
        public float tangentialAccel;

        // Token: 0x0400091D RID: 2333
        public float tangentialAccelVar;

        // Token: 0x0400091E RID: 2334
        public float radialAccel;

        // Token: 0x0400091F RID: 2335
        public float radialAccelVar;

        // Token: 0x04000920 RID: 2336
        public float size;

        // Token: 0x04000921 RID: 2337
        public float endSize;

        // Token: 0x04000922 RID: 2338
        public float sizeVar;

        // Token: 0x04000923 RID: 2339
        public float life;

        // Token: 0x04000924 RID: 2340
        public float lifeVar;

        // Token: 0x04000925 RID: 2341
        public RGBAColor startColor = default(RGBAColor);

        // Token: 0x04000926 RID: 2342
        public RGBAColor startColorVar = default(RGBAColor);

        // Token: 0x04000927 RID: 2343
        public RGBAColor endColor = default(RGBAColor);

        // Token: 0x04000928 RID: 2344
        public RGBAColor endColorVar = default(RGBAColor);

        // Token: 0x04000929 RID: 2345
        public Particle[] particles;

        // Token: 0x0400092A RID: 2346
        public int totalParticles;

        // Token: 0x0400092B RID: 2347
        public int particleCount;

        // Token: 0x0400092C RID: 2348
        public bool blendAdditive;

        // Token: 0x0400092D RID: 2349
        public bool colorModulate;

        // Token: 0x0400092E RID: 2350
        public float emissionRate;

        // Token: 0x0400092F RID: 2351
        public float emitCounter;

        // Token: 0x04000930 RID: 2352
        public Texture2D texture;

        // Token: 0x04000931 RID: 2353
        public PointSprite[] vertices;

        // Token: 0x04000932 RID: 2354
        public RGBAColor[] colors;

        // Token: 0x04000933 RID: 2355
        private uint verticesID;

        // Token: 0x04000934 RID: 2356
        public uint colorsID;

        // Token: 0x04000935 RID: 2357
        public int particleIdx;

        // Token: 0x04000936 RID: 2358
        public Particles.ParticlesFinished particlesDelegate;

        // Token: 0x02000079 RID: 121
        // (Invoke) Token: 0x0600039B RID: 923
        public delegate void ParticlesFinished(Particles p);
    }
}
