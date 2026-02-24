using System;
using ctr_wp7.iframework;

namespace ctr_wp7.ios
{
	// Token: 0x02000008 RID: 8
	internal class NSObject : FrameworkTypes
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00005A36 File Offset: 0x00003C36
		public static void NSREL(NSObject obj)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005A3A File Offset: 0x00003C3A
		public static object NSRET(object obj)
		{
			return obj;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005A3D File Offset: 0x00003C3D
		public static NSString NSS(string s)
		{
			return new NSString(s);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005A45 File Offset: 0x00003C45
		public virtual NSObject init()
		{
			return this;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005A48 File Offset: 0x00003C48
		public virtual void dealloc()
		{
		}
	}
}
