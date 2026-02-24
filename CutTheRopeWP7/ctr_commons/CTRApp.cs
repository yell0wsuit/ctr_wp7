using System;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_commons
{
    internal sealed class CTRApp : Application
    {
        public override void dealloc()
        {
            throw new NotImplementedException();
        }

        public static void applicationWillTerminate(UIApplication application)
        {
            sharedPreferences().savePreferences();
        }

        public void applicationDidReceiveMemoryWarning(UIApplication application)
        {
            throw new NotImplementedException();
        }

        public void challengeStartedWithGameConfig(NSString gameConfig)
        {
            throw new NotImplementedException();
        }

        public static void applicationWillResignActive(UIApplication application)
        {
            sharedPreferences().savePreferences();
            if (root != null && !root.isSuspended())
            {
                root.suspend();
            }
        }

        public static void applicationDidBecomeActive(UIApplication application)
        {
            if (root != null && root.isSuspended())
            {
                root.resume();
            }
        }
    }
}
