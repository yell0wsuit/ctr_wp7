using System.Collections.Generic;
using System.Linq;

using ctr_wp7.iframework.helpers;

namespace ctr_wp7.ios
{
    // Token: 0x02000046 RID: 70
    internal class NSTimer : NSObject
    {
        // Token: 0x06000244 RID: 580 RVA: 0x0000F02F File Offset: 0x0000D22F
        private static void Init()
        {
            Timers = new List<NSTimer.Entry>();
            dd = new DelayedDispatcher();
            is_init = true;
        }

        // Token: 0x06000245 RID: 581 RVA: 0x0000F04B File Offset: 0x0000D24B
        public static void registerDelayedObjectCall(DelayedDispatcher.DispatchFunc f, NSObject p, double interval)
        {
            if (!is_init)
            {
                Init();
            }
            dd.callObjectSelectorParamafterDelay(f, p, interval);
        }

        // Token: 0x06000246 RID: 582 RVA: 0x0000F068 File Offset: 0x0000D268
        public static int schedule(DelayedDispatcher.DispatchFunc f, NSObject p, float interval)
        {
            if (!is_init)
            {
                Init();
            }
            NSTimer.Entry entry = new NSTimer.Entry();
            entry.f = f;
            entry.p = p;
            entry.fireTime = 0f;
            entry.delay = interval;
            Timers.Add(entry);
            return Enumerable.Count<NSTimer.Entry>(Timers) - 1;
        }

        // Token: 0x06000247 RID: 583 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
        public static void fireTimers(float delta)
        {
            if (!is_init)
            {
                Init();
            }
            dd.update(delta);
            for (int i = 0; i < Timers.Count; i++)
            {
                NSTimer.Entry entry = Timers[i];
                entry.fireTime += delta;
                if (entry.fireTime >= entry.delay)
                {
                    entry.f(entry.p);
                    entry.fireTime -= entry.delay;
                }
            }
        }

        // Token: 0x06000248 RID: 584 RVA: 0x0000F145 File Offset: 0x0000D345
        public static void stopTimer(int Number)
        {
            Timers.RemoveAt(Number);
        }

        // Token: 0x0400082C RID: 2092
        private static List<NSTimer.Entry> Timers;

        // Token: 0x0400082D RID: 2093
        private static DelayedDispatcher dd;

        // Token: 0x0400082E RID: 2094
        private static bool is_init;

        // Token: 0x02000047 RID: 71
        private class Entry
        {
            // Token: 0x0400082F RID: 2095
            public DelayedDispatcher.DispatchFunc f;

            // Token: 0x04000830 RID: 2096
            public NSObject p;

            // Token: 0x04000831 RID: 2097
            public float fireTime;

            // Token: 0x04000832 RID: 2098
            public float delay;
        }
    }
}
