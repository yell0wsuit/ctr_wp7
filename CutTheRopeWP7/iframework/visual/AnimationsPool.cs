using System.Collections.Generic;

namespace ctr_wp7.iframework.visual
{
    internal sealed class AnimationsPool : BaseElement, TimelineDelegate
    {
        public AnimationsPool()
        {
            _ = init();
        }

        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public void timelineFinished(Timeline t)
        {
            if (getChildId(t.element) != -1)
            {
                removeList.Add(t.element);
            }
        }

        public override void update(float delta)
        {
            int count = removeList.Count;
            for (int i = 0; i < count; i++)
            {
                removeChild(removeList[i]);
            }
            removeList.Clear();
            base.update(delta);
        }

        public void particlesFinished(Particles p)
        {
            if (getChildId(p) != -1)
            {
                removeList.Add(p);
            }
        }

        public override void dealloc()
        {
            removeList.Clear();
            removeList = null;
            base.dealloc();
        }

        private List<BaseElement> removeList = [];
    }
}
