using System;

namespace ctr_wp7.iframework
{
	// Token: 0x02000022 RID: 34
	internal struct PointSprite
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000AF64 File Offset: 0x00009164
		public PointSprite(float xx, float yy, float s)
		{
			this.x = xx;
			this.y = yy;
			this.size = s;
		}

		// Token: 0x04000798 RID: 1944
		public float x;

		// Token: 0x04000799 RID: 1945
		public float y;

		// Token: 0x0400079A RID: 1946
		public float size;
	}
}
