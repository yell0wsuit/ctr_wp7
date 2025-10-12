using System;
using System.Collections.Generic;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.sfe
{
	// Token: 0x020000DF RID: 223
	internal class ConstraintSystem : NSObject
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x00031D0F File Offset: 0x0002FF0F
		public override NSObject init()
		{
			if (base.init() != null)
			{
				this.relaxationTimes = 1;
				this.parts = new List<ConstraintedPoint>();
			}
			return this;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00031D2C File Offset: 0x0002FF2C
		public virtual void addPart(ConstraintedPoint cp)
		{
			this.parts.Add(cp);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00031D3A File Offset: 0x0002FF3A
		public virtual void addPartAt(ConstraintedPoint cp, int p)
		{
			this.parts.Insert(p, cp);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00031D4C File Offset: 0x0002FF4C
		public virtual void update(float delta)
		{
			int count = this.parts.Count;
			for (int i = 0; i < count; i++)
			{
				ConstraintedPoint constraintedPoint = this.parts[i];
				if (constraintedPoint != null)
				{
					constraintedPoint.update(delta);
				}
			}
			int count2 = this.parts.Count;
			for (int j = 0; j < this.relaxationTimes; j++)
			{
				for (int k = 0; k < count2; k++)
				{
					ConstraintedPoint constraintedPoint2 = this.parts[k];
					ConstraintedPoint.satisfyConstraints(constraintedPoint2);
				}
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00031DCF File Offset: 0x0002FFCF
		public virtual void draw()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00031DD6 File Offset: 0x0002FFD6
		public override void dealloc()
		{
			this.parts = null;
			base.dealloc();
		}

		// Token: 0x04000BF0 RID: 3056
		public List<ConstraintedPoint> parts;

		// Token: 0x04000BF1 RID: 3057
		public int relaxationTimes;
	}
}
