using System;
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
				Vector vector3 = MathHelper.vectAdd(MathHelper.vectAdd(vector, vector2), this.gravity);
				vector3 = MathHelper.vectMult(vector3, delta);
				p.dir = MathHelper.vectAdd(p.dir, vector3);
				vector3 = MathHelper.vectMult(p.dir, delta);
				p.pos = MathHelper.vectAdd(p.pos, vector3);
				p.color.r = p.color.r + p.deltaColor.r * delta;
				p.color.g = p.color.g + p.deltaColor.g * delta;
				p.color.b = p.color.b + p.deltaColor.b * delta;
				p.color.a = p.color.a + p.deltaColor.a * delta;
				p.life -= delta;
				this.vertices[this.particleIdx].x = p.pos.x;
				this.vertices[this.particleIdx].y = p.pos.y;
				this.vertices[this.particleIdx].size = p.size;
				this.colors[this.particleIdx] = p.color;
				this.particleIdx++;
				return;
			}
			if (this.particleIdx != this.particleCount - 1)
			{
				this.particles[this.particleIdx] = this.particles[this.particleCount - 1];
			}
			this.particleCount--;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00016774 File Offset: 0x00014974
		public override void update(float delta)
		{
			base.update(delta);
			if (this.particlesDelegate != null && this.particleCount == 0 && !this.active)
			{
				this.particlesDelegate(this);
				return;
			}
			if (this.vertices == null)
			{
				return;
			}
			if (this.active && this.emissionRate != 0f)
			{
				float num = 1f / this.emissionRate;
				this.emitCounter += delta;
				while (this.particleCount < this.totalParticles && this.emitCounter > num)
				{
					this.addParticle();
					this.emitCounter -= num;
				}
				this.elapsed += delta;
				if (this.duration != -1f && this.duration < this.elapsed)
				{
					this.stopSystem();
				}
			}
			this.particleIdx = 0;
			while (this.particleIdx < this.particleCount)
			{
				this.updateParticle(ref this.particles[this.particleIdx], delta);
			}
			OpenGL.glBindBuffer(2, this.verticesID);
			OpenGL.glBufferData(2, this.vertices, 3);
			OpenGL.glBindBuffer(2, this.colorsID);
			OpenGL.glBufferData(2, this.colors, 3);
			OpenGL.glBindBuffer(2, 0U);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000168AE File Offset: 0x00014AAE
		public override void dealloc()
		{
			this.particles = null;
			this.vertices = null;
			this.colors = null;
			OpenGL.glDeleteBuffers(1, ref this.verticesID);
			OpenGL.glDeleteBuffers(1, ref this.colorsID);
			this.texture = null;
			base.dealloc();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000168EA File Offset: 0x00014AEA
		public override void draw()
		{
			this.preDraw();
			this.postDraw();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000168F8 File Offset: 0x00014AF8
		public virtual Particles initWithTotalParticles(int numberOfParticles)
		{
			if (base.init() == null)
			{
				return null;
			}
			this.width = (int)FrameworkTypes.SCREEN_WIDTH;
			this.height = (int)FrameworkTypes.SCREEN_HEIGHT;
			this.totalParticles = numberOfParticles;
			this.particles = new Particle[this.totalParticles];
			this.vertices = new PointSprite[this.totalParticles];
			this.colors = new RGBAColor[this.totalParticles];
			if (this.particles == null || this.vertices == null || this.colors == null)
			{
				this.particles = null;
				this.vertices = null;
				this.colors = null;
				return null;
			}
			this.active = false;
			this.blendAdditive = false;
			OpenGL.glGenBuffers(1, ref this.verticesID);
			OpenGL.glGenBuffers(1, ref this.colorsID);
			return this;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000169B7 File Offset: 0x00014BB7
		public virtual bool addParticle()
		{
			if (this.isFull())
			{
				return false;
			}
			this.initParticle(ref this.particles[this.particleCount]);
			this.particleCount++;
			return true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000169EC File Offset: 0x00014BEC
		public virtual void initParticle(ref Particle particle)
		{
			particle.pos.x = this.x + this.posVar.x * MathHelper.RND_MINUS1_1;
			particle.pos.y = this.y + this.posVar.y * MathHelper.RND_MINUS1_1;
			particle.startPos = particle.pos;
			float num = MathHelper.DEGREES_TO_RADIANS(this.angle + this.angleVar * MathHelper.RND_MINUS1_1);
			Vector vector;
			vector.y = MathHelper.sinf(num);
			vector.x = MathHelper.cosf(num);
			float num2 = this.speed + this.speedVar * MathHelper.RND_MINUS1_1;
			particle.dir = MathHelper.vectMult(vector, num2);
			particle.radialAccel = this.radialAccel + this.radialAccelVar * MathHelper.RND_MINUS1_1;
			particle.tangentialAccel = this.tangentialAccel + this.tangentialAccelVar * MathHelper.RND_MINUS1_1;
			particle.life = this.life + this.lifeVar * MathHelper.RND_MINUS1_1;
			RGBAColor rgbacolor = default(RGBAColor);
			rgbacolor.r = this.startColor.r + this.startColorVar.r * MathHelper.RND_MINUS1_1;
			rgbacolor.g = this.startColor.g + this.startColorVar.g * MathHelper.RND_MINUS1_1;
			rgbacolor.b = this.startColor.b + this.startColorVar.b * MathHelper.RND_MINUS1_1;
			rgbacolor.a = this.startColor.a + this.startColorVar.a * MathHelper.RND_MINUS1_1;
			RGBAColor rgbacolor2 = default(RGBAColor);
			rgbacolor2.r = this.endColor.r + this.endColorVar.r * MathHelper.RND_MINUS1_1;
			rgbacolor2.g = this.endColor.g + this.endColorVar.g * MathHelper.RND_MINUS1_1;
			rgbacolor2.b = this.endColor.b + this.endColorVar.b * MathHelper.RND_MINUS1_1;
			rgbacolor2.a = this.endColor.a + this.endColorVar.a * MathHelper.RND_MINUS1_1;
			particle.color = rgbacolor;
			particle.deltaColor.r = (rgbacolor2.r - rgbacolor.r) / particle.life;
			particle.deltaColor.g = (rgbacolor2.g - rgbacolor.g) / particle.life;
			particle.deltaColor.b = (rgbacolor2.b - rgbacolor.b) / particle.life;
			particle.deltaColor.a = (rgbacolor2.a - rgbacolor.a) / particle.life;
			particle.size = this.size + this.sizeVar * MathHelper.RND_MINUS1_1;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00016CBF File Offset: 0x00014EBF
		public virtual void startSystem(int initialParticles)
		{
			this.particleCount = 0;
			while (this.particleCount < initialParticles)
			{
				this.addParticle();
			}
			this.active = true;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00016CE1 File Offset: 0x00014EE1
		public virtual void stopSystem()
		{
			this.active = false;
			this.elapsed = this.duration;
			this.emitCounter = 0f;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00016D01 File Offset: 0x00014F01
		public virtual void resetSystem()
		{
			this.elapsed = 0f;
			this.emitCounter = 0f;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00016D19 File Offset: 0x00014F19
		public virtual bool isFull()
		{
			return this.particleCount == this.totalParticles;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00016D29 File Offset: 0x00014F29
		public virtual void setBlendAdditive(bool b)
		{
			this.blendAdditive = b;
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
