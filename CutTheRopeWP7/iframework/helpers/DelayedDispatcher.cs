using System;
using System.Collections.Generic;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    internal sealed class DelayedDispatcher : NSObject
    {
        public DelayedDispatcher()
        {
            dispatchers = [];
        }

        public override void dealloc()
        {
            dispatchers.Clear();
            dispatchers = null;
            base.dealloc();
        }

        public void callObjectSelectorParamafterDelay(DispatchFunc s, NSObject p, double d)
        {
            callObjectSelectorParamafterDelay(s, p, (float)d);
        }

        public void callObjectSelectorParamafterDelay(DispatchFunc s, NSObject p, float d)
        {
            Dispatch dispatch = new Dispatch().initWithObjectSelectorParamafterDelay(s, p, d);
            dispatchers.Add(dispatch);
        }

        public void update(float d)
        {
            int num = dispatchers.Count;
            for (int i = 0; i < num; i++)
            {
                Dispatch dispatch = dispatchers[i];
                dispatch.delay -= d;
                if (dispatch.delay <= 0.0)
                {
                    dispatch.dispatch();
                    _ = dispatchers.Remove(dispatch);
                    i--;
                    num--;
                }
            }
        }

        public void cancelAllDispatches()
        {
            dispatchers.Clear();
        }

        public void cancelDispatchWithObjectSelectorParam(DispatchFunc s, NSObject p)
        {
            throw new NotImplementedException();
        }

        private List<Dispatch> dispatchers;

        // (Invoke) Token: 0x060003BA RID: 954
        public delegate void DispatchFunc(NSObject param);
    }
}
