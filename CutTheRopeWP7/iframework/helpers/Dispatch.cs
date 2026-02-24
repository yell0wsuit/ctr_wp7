using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x0200007E RID: 126
    internal class Dispatch : NSObject
    {
        // Token: 0x060003AF RID: 943 RVA: 0x00017F63 File Offset: 0x00016163
        public virtual Dispatch initWithObjectSelectorParamafterDelay(DelayedDispatcher.DispatchFunc callThisFunc, NSObject p, float d)
        {
            this.callThis = callThisFunc;
            this.param = p;
            this.delay = d;
            return this;
        }

        // Token: 0x060003B0 RID: 944 RVA: 0x00017F7B File Offset: 0x0001617B
        public virtual void dispatch()
        {
            if (this.callThis != null)
            {
                this.callThis(this.param);
            }
        }

        // Token: 0x0400093E RID: 2366
        public float delay;

        // Token: 0x0400093F RID: 2367
        public DelayedDispatcher.DispatchFunc callThis;

        // Token: 0x04000940 RID: 2368
        public NSObject param;
    }
}
