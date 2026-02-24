using System;
using System.Collections.Generic;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x0200007F RID: 127
    internal class DelayedDispatcher : NSObject
    {
        // Token: 0x060003B2 RID: 946 RVA: 0x00017F9E File Offset: 0x0001619E
        public DelayedDispatcher()
        {
            dispatchers = new List<Dispatch>();
        }

        // Token: 0x060003B3 RID: 947 RVA: 0x00017FB1 File Offset: 0x000161B1
        public override void dealloc()
        {
            dispatchers.Clear();
            dispatchers = null;
            base.dealloc();
        }

        // Token: 0x060003B4 RID: 948 RVA: 0x00017FCB File Offset: 0x000161CB
        public virtual void callObjectSelectorParamafterDelay(DispatchFunc s, NSObject p, double d)
        {
            callObjectSelectorParamafterDelay(s, p, (float)d);
        }

        // Token: 0x060003B5 RID: 949 RVA: 0x00017FD8 File Offset: 0x000161D8
        public virtual void callObjectSelectorParamafterDelay(DispatchFunc s, NSObject p, float d)
        {
            Dispatch dispatch = new Dispatch().initWithObjectSelectorParamafterDelay(s, p, d);
            dispatchers.Add(dispatch);
        }

        // Token: 0x060003B6 RID: 950 RVA: 0x00018000 File Offset: 0x00016200
        public virtual void update(float d)
        {
            int num = dispatchers.Count;
            for (int i = 0; i < num; i++)
            {
                Dispatch dispatch = dispatchers[i];
                dispatch.delay -= d;
                if ((double)dispatch.delay <= 0.0)
                {
                    dispatch.dispatch();
                    _ = dispatchers.Remove(dispatch);
                    i--;
                    num--;
                }
            }
        }

        // Token: 0x060003B7 RID: 951 RVA: 0x0001806D File Offset: 0x0001626D
        public virtual void cancelAllDispatches()
        {
            dispatchers.Clear();
        }

        // Token: 0x060003B8 RID: 952 RVA: 0x0001807A File Offset: 0x0001627A
        public virtual void cancelDispatchWithObjectSelectorParam(DispatchFunc s, NSObject p)
        {
            throw new NotImplementedException();
        }

        // Token: 0x04000941 RID: 2369
        private List<Dispatch> dispatchers;

        // Token: 0x02000080 RID: 128
        // (Invoke) Token: 0x060003BA RID: 954
        public delegate void DispatchFunc(NSObject param);
    }
}
