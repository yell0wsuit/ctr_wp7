using System;

namespace ctr_wp7.iframework
{
	// Token: 0x02000025 RID: 37
	internal struct Quad3D
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000B0C1 File Offset: 0x000092C1
		public static Quad3D MakeQuad3D(double x, double y, double z, double w, double h)
		{
			return Quad3D.MakeQuad3D((float)x, (float)y, (float)z, (float)w, (float)h);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public static Quad3D MakeQuad3D(float x, float y, float z, float w, float h)
		{
			return new Quad3D
			{
				blX = x,
				blY = y,
				blZ = z,
				brX = x + w,
				brY = y,
				brZ = z,
				tlX = x,
				tlY = y + h,
				tlZ = z,
				trX = x + w,
				trY = y + h,
				trZ = z
			};
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B154 File Offset: 0x00009354
		public static Quad3D MakeQuad3DEx(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			return new Quad3D
			{
				blX = x1,
				blY = y1,
				blZ = 0f,
				brX = x2,
				brY = y2,
				brZ = 0f,
				tlX = x3,
				tlY = y3,
				tlZ = 0f,
				trX = x4,
				trY = y4,
				trZ = 0f
			};
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public float[] toFloatArray()
		{
			if (this._array == null)
			{
				this._array = new float[]
				{
					this.blX, this.blY, this.blZ, this.brX, this.brY, this.brZ, this.tlX, this.tlY, this.tlZ, this.trX,
					this.trY, this.trZ
				};
			}
			return this._array;
		}

		// Token: 0x040007A7 RID: 1959
		private float blX;

		// Token: 0x040007A8 RID: 1960
		private float blY;

		// Token: 0x040007A9 RID: 1961
		private float blZ;

		// Token: 0x040007AA RID: 1962
		private float brX;

		// Token: 0x040007AB RID: 1963
		private float brY;

		// Token: 0x040007AC RID: 1964
		private float brZ;

		// Token: 0x040007AD RID: 1965
		private float tlX;

		// Token: 0x040007AE RID: 1966
		private float tlY;

		// Token: 0x040007AF RID: 1967
		private float tlZ;

		// Token: 0x040007B0 RID: 1968
		private float trX;

		// Token: 0x040007B1 RID: 1969
		private float trY;

		// Token: 0x040007B2 RID: 1970
		private float trZ;

		// Token: 0x040007B3 RID: 1971
		private float[] _array;
	}
}
