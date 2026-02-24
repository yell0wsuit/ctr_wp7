using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    internal sealed class Dispatch : NSObject
    {
        public Dispatch initWithObjectSelectorParamafterDelay(DelayedDispatcher.DispatchFunc callThisFunc, NSObject p, float d)
        {
            callThis = callThisFunc;
            param = p;
            delay = d;
            return this;
        }

        public void dispatch()
        {
            if (callThis != null)
            {
                callThis(param);
            }
        }

        public float delay;

        public DelayedDispatcher.DispatchFunc callThis;

        public NSObject param;
    }
}
