using System.Collections.Generic;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200000D RID: 13
    internal class Animation : Image
    {
        // Token: 0x060000D9 RID: 217 RVA: 0x00007303 File Offset: 0x00005503
        public static Animation Animation_create(Texture2D t)
        {
            return (Animation)new Animation().initWithTexture(t);
        }

        // Token: 0x060000DA RID: 218 RVA: 0x00007315 File Offset: 0x00005515
        public static Animation Animation_createWithResID(int r)
        {
            return Animation.Animation_create(Application.getTexture(r));
        }

        // Token: 0x060000DB RID: 219 RVA: 0x00007324 File Offset: 0x00005524
        public static Animation Animation_createWithResIDQuad(int r, int q)
        {
            Animation animation = Animation.Animation_createWithResID(r);
            if (animation != null)
            {
                animation.setDrawQuad(q);
            }
            return animation;
        }

        // Token: 0x060000DC RID: 220 RVA: 0x00007344 File Offset: 0x00005544
        public virtual void addAnimationWithIDDelayLoopFirstLast(int aid, float d, Timeline.LoopType l, int s, int e)
        {
            int num = e - s + 1;
            this.addAnimationWithIDDelayLoopCountFirstLastArgumentList(aid, d, l, num, s, e);
        }

        // Token: 0x060000DD RID: 221 RVA: 0x00007368 File Offset: 0x00005568
        public virtual void addAnimationWithIDDelayLoopCountFirstLastArgumentList(int aid, float d, Timeline.LoopType l, int c, int s, int e)
        {
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(c + 2);
            List<Action> list = new List<Action>();
            list.Add(Action.createAction(this, "ACTION_SET_DRAWQUAD", s, 0));
            timeline.addKeyFrame(KeyFrame.makeAction(list, 0f));
            int num = s;
            for (int i = 1; i < c; i++)
            {
                num++;
                list = new List<Action>();
                list.Add(Action.createAction(this, "ACTION_SET_DRAWQUAD", num, 0));
                timeline.addKeyFrame(KeyFrame.makeAction(list, d));
                if (i == c - 1 && l == Timeline.LoopType.TIMELINE_REPLAY)
                {
                    timeline.addKeyFrame(KeyFrame.makeAction(list, d));
                }
            }
            if (l != Timeline.LoopType.TIMELINE_NO_LOOP)
            {
                timeline.setTimelineLoopType(l);
            }
            this.addTimelinewithID(timeline, aid);
        }

        // Token: 0x060000DE RID: 222 RVA: 0x00007413 File Offset: 0x00005613
        public virtual void addAnimationWithIDDelayLoopCountSequence(int aid, float d, Timeline.LoopType l, int c, int s, List<int> al)
        {
            this.addAnimationWithIDDelayLoopCountFirstLastArgumentList(aid, d, l, c, s, -1, al);
        }

        // Token: 0x060000DF RID: 223 RVA: 0x00007428 File Offset: 0x00005628
        public virtual void addAnimationWithIDDelayLoopCountFirstLastArgumentList(int aid, float d, Timeline.LoopType l, int c, int s, int e, List<int> al)
        {
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(c + 2);
            List<Action> list = new List<Action>();
            list.Add(Action.createAction(this, "ACTION_SET_DRAWQUAD", s, 0));
            timeline.addKeyFrame(KeyFrame.makeAction(list, 0f));
            int num = 0;
            for (int i = 1; i < c; i++)
            {
                int num2 = al[num++];
                list = new List<Action>();
                list.Add(Action.createAction(this, "ACTION_SET_DRAWQUAD", num2, 0));
                timeline.addKeyFrame(KeyFrame.makeAction(list, d));
                if (i == c - 1 && l == Timeline.LoopType.TIMELINE_REPLAY)
                {
                    timeline.addKeyFrame(KeyFrame.makeAction(list, d));
                }
            }
            if (l != Timeline.LoopType.TIMELINE_NO_LOOP)
            {
                timeline.setTimelineLoopType(l);
            }
            this.addTimelinewithID(timeline, aid);
        }

        // Token: 0x060000E0 RID: 224 RVA: 0x000074E4 File Offset: 0x000056E4
        public virtual void switchToAnimationatEndOfAnimationDelay(int a2, int a1, float d)
        {
            Timeline timeline = this.getTimeline(a1);
            List<Action> list = new List<Action>();
            list.Add(Action.createAction(this, "ACTION_PLAY_TIMELINE", 0, a2));
            timeline.addKeyFrame(KeyFrame.makeAction(list, d));
        }

        // Token: 0x060000E1 RID: 225 RVA: 0x0000751F File Offset: 0x0000571F
        public virtual void setPauseAtIndexforAnimation(int i, int a)
        {
            this.setActionTargetParamSubParamAtIndexforAnimation("ACTION_PAUSE_TIMELINE", this, 0, 0, i, a);
        }

        // Token: 0x060000E2 RID: 226 RVA: 0x00007534 File Offset: 0x00005734
        public virtual void setActionTargetParamSubParamAtIndexforAnimation(string action, BaseElement target, int p, int sp, int i, int a)
        {
            Timeline timeline = this.getTimeline(a);
            Track track = timeline.getTrack(Track.TrackType.TRACK_ACTION);
            KeyFrame keyFrame = track.keyFrames[i];
            List<Action> actionSet = keyFrame.value.action.actionSet;
            actionSet.Add(Action.createAction(target, action, p, sp));
        }

        // Token: 0x060000E3 RID: 227 RVA: 0x00007580 File Offset: 0x00005780
        public virtual int addAnimationWithDelayLoopedCountSequence(float d, Timeline.LoopType l, int c, int s, List<int> al)
        {
            int count = this.timelines.Count;
            this.addAnimationWithIDDelayLoopCountFirstLastArgumentList(count, d, l, c, s, -1, al);
            return count;
        }

        // Token: 0x060000E4 RID: 228 RVA: 0x000075AC File Offset: 0x000057AC
        public void setDelayatIndexforAnimation(float d, int i, int a)
        {
            Timeline timeline = this.getTimeline(a);
            Track track = timeline.getTrack(Track.TrackType.TRACK_ACTION);
            KeyFrame keyFrame = track.keyFrames[i];
            keyFrame.timeOffset = d;
        }

        // Token: 0x060000E5 RID: 229 RVA: 0x000075DC File Offset: 0x000057DC
        public int addAnimationDelayLoopFirstLast(float d, Timeline.LoopType l, int s, int e)
        {
            int count = this.timelines.Count;
            this.addAnimationWithIDDelayLoopFirstLast(count, d, l, s, e);
            return count;
        }

        // Token: 0x060000E6 RID: 230 RVA: 0x00007604 File Offset: 0x00005804
        public void jumpTo(int i)
        {
            Timeline currentTimeline = this.getCurrentTimeline();
            currentTimeline.jumpToTrackKeyFrame(4, i);
        }
    }
}
