using System;
using System.Collections.Generic;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000EB RID: 235
    internal class AnimationsPool : BaseElement, TimelineDelegate
    {
        // Token: 0x0600070C RID: 1804 RVA: 0x00038F4A File Offset: 0x0003714A
        public AnimationsPool()
        {
            base.init();
        }

        // Token: 0x0600070D RID: 1805 RVA: 0x00038F64 File Offset: 0x00037164
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x0600070E RID: 1806 RVA: 0x00038F66 File Offset: 0x00037166
        public virtual void timelineFinished(Timeline t)
        {
            if (this.getChildId(t.element) != -1)
            {
                this.removeList.Add(t.element);
            }
        }

        // Token: 0x0600070F RID: 1807 RVA: 0x00038F88 File Offset: 0x00037188
        public override void update(float delta)
        {
            int count = this.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                this.removeChild(this.removeList[i]);
            }
            this.removeList.Clear();
            base.update(delta);
        }

        // Token: 0x06000710 RID: 1808 RVA: 0x00038FD1 File Offset: 0x000371D1
        public virtual void particlesFinished(Particles p)
        {
            if (this.getChildId(p) != -1)
            {
                this.removeList.Add(p);
            }
        }

        // Token: 0x06000711 RID: 1809 RVA: 0x00038FE9 File Offset: 0x000371E9
        public override void dealloc()
        {
            this.removeList.Clear();
            this.removeList = null;
            base.dealloc();
        }

        // Token: 0x04000CA1 RID: 3233
        private List<BaseElement> removeList = new List<BaseElement>();
    }
}
