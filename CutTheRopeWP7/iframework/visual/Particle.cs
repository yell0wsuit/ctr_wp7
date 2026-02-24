using System;
using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x02000077 RID: 119
	internal struct Particle
	{
		// Token: 0x04000905 RID: 2309
		public Vector startPos;

		// Token: 0x04000906 RID: 2310
		public Vector pos;

		// Token: 0x04000907 RID: 2311
		public Vector dir;

		// Token: 0x04000908 RID: 2312
		public float radialAccel;

		// Token: 0x04000909 RID: 2313
		public float tangentialAccel;

		// Token: 0x0400090A RID: 2314
		public RGBAColor color;

		// Token: 0x0400090B RID: 2315
		public RGBAColor deltaColor;

		// Token: 0x0400090C RID: 2316
		public float size;

		// Token: 0x0400090D RID: 2317
		public float deltaSize;

		// Token: 0x0400090E RID: 2318
		public float life;

		// Token: 0x0400090F RID: 2319
		public float deltaAngle;

		// Token: 0x04000910 RID: 2320
		public float angle;

		// Token: 0x04000911 RID: 2321
		public float width;

		// Token: 0x04000912 RID: 2322
		public float height;
	}
}
