using System;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.sfe
{
	// Token: 0x0200001C RID: 28
	internal class Constraint : NSObject
	{
		// Token: 0x0400076E RID: 1902
		public ConstraintedPoint cp;

		// Token: 0x0400076F RID: 1903
		public float restLength;

		// Token: 0x04000770 RID: 1904
		public Constraint.CONSTRAINT type;

		// Token: 0x0200001D RID: 29
		public enum CONSTRAINT
		{
			// Token: 0x04000772 RID: 1906
			CONSTRAINT_DISTANCE,
			// Token: 0x04000773 RID: 1907
			CONSTRAINT_NOT_MORE_THAN,
			// Token: 0x04000774 RID: 1908
			CONSTRAINT_NOT_LESS_THAN
		}
	}
}
