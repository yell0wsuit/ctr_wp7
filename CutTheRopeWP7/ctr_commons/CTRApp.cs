using System;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x0200001A RID: 26
    internal sealed class CTRApp : Application
    {
        // Token: 0x0600013C RID: 316 RVA: 0x0000A324 File Offset: 0x00008524
        public override void dealloc()
        {
            throw new NotImplementedException();
        }

        // Token: 0x0600013D RID: 317 RVA: 0x0000A32B File Offset: 0x0000852B
        public static void applicationWillTerminate(UIApplication application)
        {
            sharedPreferences().savePreferences();
        }

        // Token: 0x0600013E RID: 318 RVA: 0x0000A337 File Offset: 0x00008537
        public void applicationDidReceiveMemoryWarning(UIApplication application)
        {
            throw new NotImplementedException();
        }

        // Token: 0x0600013F RID: 319 RVA: 0x0000A33E File Offset: 0x0000853E
        public void challengeStartedWithGameConfig(NSString gameConfig)
        {
            throw new NotImplementedException();
        }

        // Token: 0x06000140 RID: 320 RVA: 0x0000A345 File Offset: 0x00008545
        public static void applicationWillResignActive(UIApplication application)
        {
            sharedPreferences().savePreferences();
            if (root != null && !root.isSuspended())
            {
                root.suspend();
            }
        }

        // Token: 0x06000141 RID: 321 RVA: 0x0000A36E File Offset: 0x0000856E
        public static void applicationDidBecomeActive(UIApplication application)
        {
            if (root != null && root.isSuspended())
            {
                root.resume();
            }
        }
    }
}
