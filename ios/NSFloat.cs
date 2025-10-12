using System;

namespace ctre_wp7.ios
{
	// Token: 0x020000BD RID: 189
	internal class NSFloat : NSObject
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x00029EA8 File Offset: 0x000280A8
		public static NSFloat floatWithFloat(float v)
		{
			return new NSFloat
			{
				_value = v
			};
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00029EC3 File Offset: 0x000280C3
		public virtual float floatValue()
		{
			return this._value;
		}

		// Token: 0x04000ADD RID: 2781
		public float _value;
	}
}
