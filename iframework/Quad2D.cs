using System;

namespace ctr_wp7.iframework
{
	// Token: 0x02000024 RID: 36
	internal struct Quad2D
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public Quad2D(float x, float y, float w, float h)
		{
			this.tlX = x;
			this.tlY = y;
			this.trX = x + w;
			this.trY = y;
			this.blX = x;
			this.blY = y + h;
			this.brX = x + w;
			this.brY = y + h;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B048 File Offset: 0x00009248
		public float[] toFloatArray()
		{
			return new float[] { this.tlX, this.tlY, this.trX, this.trY, this.blX, this.blY, this.brX, this.brY };
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public static Quad2D MakeQuad2D(float x, float y, float w, float h)
		{
			Quad2D quad2D = new Quad2D(x, y, w, h);
			return quad2D;
		}

		// Token: 0x0400079F RID: 1951
		public float tlX;

		// Token: 0x040007A0 RID: 1952
		public float tlY;

		// Token: 0x040007A1 RID: 1953
		public float trX;

		// Token: 0x040007A2 RID: 1954
		public float trY;

		// Token: 0x040007A3 RID: 1955
		public float blX;

		// Token: 0x040007A4 RID: 1956
		public float blY;

		// Token: 0x040007A5 RID: 1957
		public float brX;

		// Token: 0x040007A6 RID: 1958
		public float brY;
	}
}
