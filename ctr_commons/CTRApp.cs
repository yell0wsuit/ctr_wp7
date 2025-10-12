using System;
using ctre_wp7.iframework.core;
using ctre_wp7.ios;

namespace ctre_wp7.ctr_commons
{
	// Token: 0x0200001A RID: 26
	internal class CTRApp : Application
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000A324 File Offset: 0x00008524
		public override void dealloc()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A32B File Offset: 0x0000852B
		public virtual void applicationWillTerminate(UIApplication application)
		{
			Application.sharedPreferences().savePreferences();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A337 File Offset: 0x00008537
		public virtual void applicationDidReceiveMemoryWarning(UIApplication application)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A33E File Offset: 0x0000853E
		public virtual void challengeStartedWithGameConfig(NSString gameConfig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A345 File Offset: 0x00008545
		public virtual void applicationWillResignActive(UIApplication application)
		{
			Application.sharedPreferences().savePreferences();
			if (Application.root != null && !Application.root.isSuspended())
			{
				Application.root.suspend();
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A36E File Offset: 0x0000856E
		public virtual void applicationDidBecomeActive(UIApplication application)
		{
			if (Application.root != null && Application.root.isSuspended())
			{
				Application.root.resume();
			}
		}
	}
}
