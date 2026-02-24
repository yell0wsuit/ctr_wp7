using System;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000014 RID: 20
    internal interface TimelineDelegate
    {
        // Token: 0x0600010E RID: 270
        void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i);

        // Token: 0x0600010F RID: 271
        void timelineFinished(Timeline t);
    }
}
