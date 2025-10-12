using System;
using System.Collections.Generic;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x02000094 RID: 148
	internal class CTRMover : Mover
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		public override void setPathFromStringandStart(NSString p, Vector s)
		{
			if (p.characterAtIndex(0) == 'R')
			{
				bool flag = p.characterAtIndex(1) == 'C';
				NSString nsstring = p.substringFromIndex(2);
				int num = nsstring.intValue();
				int num2 = num / 2;
				float num3 = (float)(6.283185307179586 / (double)num2);
				if (!flag)
				{
					num3 = -num3;
				}
				float num4 = 0f;
				for (int i = 0; i < num2; i++)
				{
					float num5 = (float)((double)s.x + (double)num * Math.Cos((double)num4));
					float num6 = (float)((double)s.y + (double)num * Math.Sin((double)num4));
					this.addPathPoint(new Vector(num5, num6));
					num4 += num3;
				}
				return;
			}
			this.addPathPoint(s);
			if (p.characterAtIndex(p.length() - 1) == ',')
			{
				p = p.substringToIndex(p.length() - 1);
			}
			List<NSString> list = p.componentsSeparatedByString(',');
			for (int j = 0; j < list.Count; j += 2)
			{
				NSString nsstring2 = list[j];
				NSString nsstring3 = list[j + 1];
				this.addPathPoint(new Vector(s.x + nsstring2.floatValue(), s.y + nsstring3.floatValue()));
			}
		}
	}
}
