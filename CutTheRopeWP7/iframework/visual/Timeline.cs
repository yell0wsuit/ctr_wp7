using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000AC RID: 172
    internal class Timeline : NSObject
    {
        // Token: 0x060004C2 RID: 1218 RVA: 0x00023B86 File Offset: 0x00021D86
        public virtual void stopTimeline()
        {
            this.state = Timeline.TimelineState.TIMELINE_STOPPED;
            this.deactivateTracks();
        }

        // Token: 0x060004C3 RID: 1219 RVA: 0x00023B98 File Offset: 0x00021D98
        public virtual void deactivateTracks()
        {
            for (int i = 0; i < this.tracks.Length; i++)
            {
                if (this.tracks[i] != null)
                {
                    this.tracks[i].state = Track.TrackState.TRACK_NOT_ACTIVE;
                }
            }
        }

        // Token: 0x060004C4 RID: 1220 RVA: 0x00023BD0 File Offset: 0x00021DD0
        public void jumpToTrackKeyFrame(int t, int k)
        {
            if (this.state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                this.state = Timeline.TimelineState.TIMELINE_PAUSED;
            }
            Timeline.updateTimeline(this, this.tracks[t].getFrameTime(k) - this.time);
        }

        // Token: 0x060004C5 RID: 1221 RVA: 0x00023BFC File Offset: 0x00021DFC
        public virtual void playTimeline()
        {
            if (this.state != Timeline.TimelineState.TIMELINE_PAUSED)
            {
                this.time = 0f;
                this.timelineDirReverse = false;
                this.length = 0f;
                for (int i = 0; i < 5; i++)
                {
                    if (this.tracks[i] != null)
                    {
                        this.tracks[i].updateRange();
                        if (this.tracks[i].endTime > this.length)
                        {
                            this.length = this.tracks[i].endTime;
                        }
                    }
                }
            }
            this.state = Timeline.TimelineState.TIMELINE_PLAYING;
            Timeline.updateTimeline(this, 0f);
        }

        // Token: 0x060004C6 RID: 1222 RVA: 0x00023C8C File Offset: 0x00021E8C
        public virtual void pauseTimeline()
        {
            this.state = Timeline.TimelineState.TIMELINE_PAUSED;
        }

        // Token: 0x060004C7 RID: 1223 RVA: 0x00023C98 File Offset: 0x00021E98
        public static void updateTimeline(Timeline thiss, float delta)
        {
            if (thiss.state != Timeline.TimelineState.TIMELINE_PLAYING)
            {
                return;
            }
            if (!thiss.timelineDirReverse)
            {
                thiss.time += delta;
            }
            else
            {
                thiss.time -= delta;
            }
            for (int i = 0; i < 5; i++)
            {
                if (thiss.tracks[i] != null)
                {
                    if (thiss.tracks[i].type == Track.TrackType.TRACK_ACTION)
                    {
                        Track.updateActionTrack(thiss.tracks[i], delta);
                    }
                    else
                    {
                        Track.updateTrack(thiss.tracks[i], delta);
                    }
                }
            }
            switch (thiss.timelineLoopType)
            {
                case Timeline.LoopType.TIMELINE_NO_LOOP:
                    if (thiss.time >= thiss.length - 1E-06f)
                    {
                        thiss.stopTimeline();
                        if (thiss != null && thiss.delegateTimelineDelegate != null)
                        {
                            thiss.delegateTimelineDelegate.timelineFinished(thiss);
                        }
                    }
                    break;
                case Timeline.LoopType.TIMELINE_REPLAY:
                    if (thiss.time >= thiss.length - 1E-06f)
                    {
                        if (thiss.loopsLimit > 0)
                        {
                            thiss.loopsLimit--;
                            if (thiss.loopsLimit == 0)
                            {
                                thiss.stopTimeline();
                                if (thiss.delegateTimelineDelegate != null)
                                {
                                    thiss.delegateTimelineDelegate.timelineFinished(thiss);
                                }
                            }
                        }
                        thiss.time = Math.Min(thiss.time - thiss.length, thiss.length);
                        return;
                    }
                    break;
                case Timeline.LoopType.TIMELINE_PING_PONG:
                    {
                        bool flag = !thiss.timelineDirReverse && thiss.time >= thiss.length - 1E-06f;
                        bool flag2 = thiss.timelineDirReverse && thiss.time <= 1E-06f;
                        if (flag)
                        {
                            thiss.time = Math.Max(0f, thiss.length - (thiss.time - thiss.length));
                            thiss.timelineDirReverse = true;
                            return;
                        }
                        if (flag2)
                        {
                            if (thiss.loopsLimit > 0)
                            {
                                thiss.loopsLimit--;
                                if (thiss.loopsLimit == 0)
                                {
                                    thiss.stopTimeline();
                                    if (thiss.delegateTimelineDelegate != null)
                                    {
                                        thiss.delegateTimelineDelegate.timelineFinished(thiss);
                                    }
                                }
                            }
                            thiss.time = Math.Min(-thiss.time, thiss.length);
                            thiss.timelineDirReverse = false;
                            return;
                        }
                        break;
                    }
                default:
                    return;
            }
        }

        // Token: 0x060004C8 RID: 1224 RVA: 0x00023EA0 File Offset: 0x000220A0
        public virtual Timeline initWithMaxKeyFramesOnTrack(int m)
        {
            if (base.init() != null)
            {
                this.maxKeyFrames = m;
                this.time = 0f;
                this.length = 0f;
                this.state = Timeline.TimelineState.TIMELINE_STOPPED;
                this.loopsLimit = -1;
                this.timelineLoopType = Timeline.LoopType.TIMELINE_NO_LOOP;
            }
            return this;
        }

        // Token: 0x060004C9 RID: 1225 RVA: 0x00023EE0 File Offset: 0x000220E0
        public virtual void addKeyFrame(KeyFrame k)
        {
            int num = ((this.tracks[(int)k.trackType] == null) ? 0 : this.tracks[(int)k.trackType].keyFramesCount);
            this.setKeyFrameAt(k, num);
        }

        // Token: 0x060004CA RID: 1226 RVA: 0x00023F1C File Offset: 0x0002211C
        public virtual void setKeyFrameAt(KeyFrame k, int i)
        {
            if (this.tracks[(int)k.trackType] == null)
            {
                this.tracks[(int)k.trackType] = new Track().initWithTimelineTypeandMaxKeyFrames(this, k.trackType, this.maxKeyFrames);
            }
            this.tracks[(int)k.trackType].setKeyFrameAt(k, i);
        }

        // Token: 0x060004CB RID: 1227 RVA: 0x00023F70 File Offset: 0x00022170
        public virtual void setTimelineLoopType(Timeline.LoopType l)
        {
            this.timelineLoopType = l;
        }

        // Token: 0x060004CC RID: 1228 RVA: 0x00023F7C File Offset: 0x0002217C
        public virtual Track getTrack(Track.TrackType tt)
        {
            return this.tracks[(int)tt];
        }

        // Token: 0x04000A12 RID: 2578
        private const int TRACKS_COUNT = 5;

        // Token: 0x04000A13 RID: 2579
        public TimelineDelegate delegateTimelineDelegate;

        // Token: 0x04000A14 RID: 2580
        public BaseElement element;

        // Token: 0x04000A15 RID: 2581
        public Timeline.TimelineState state;

        // Token: 0x04000A16 RID: 2582
        public float time;

        // Token: 0x04000A17 RID: 2583
        private float length;

        // Token: 0x04000A18 RID: 2584
        public bool timelineDirReverse;

        // Token: 0x04000A19 RID: 2585
        public int loopsLimit;

        // Token: 0x04000A1A RID: 2586
        private int maxKeyFrames;

        // Token: 0x04000A1B RID: 2587
        public Timeline.LoopType timelineLoopType;

        // Token: 0x04000A1C RID: 2588
        private Track[] tracks = new Track[5];

        // Token: 0x020000AD RID: 173
        public enum TimelineState
        {
            // Token: 0x04000A1E RID: 2590
            TIMELINE_STOPPED,
            // Token: 0x04000A1F RID: 2591
            TIMELINE_PLAYING,
            // Token: 0x04000A20 RID: 2592
            TIMELINE_PAUSED
        }

        // Token: 0x020000AE RID: 174
        public enum LoopType
        {
            // Token: 0x04000A22 RID: 2594
            TIMELINE_NO_LOOP,
            // Token: 0x04000A23 RID: 2595
            TIMELINE_REPLAY,
            // Token: 0x04000A24 RID: 2596
            TIMELINE_PING_PONG
        }
    }
}
