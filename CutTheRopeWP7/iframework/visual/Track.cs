using System.Collections.Generic;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000A9 RID: 169
    internal class Track : NSObject
    {
        // Token: 0x060004B7 RID: 1207 RVA: 0x000220BE File Offset: 0x000202BE
        public Track()
        {
            elementPrevState = new KeyFrame();
            currentStepPerSecond = new KeyFrame();
            currentStepAcceleration = new KeyFrame();
        }

        // Token: 0x060004B8 RID: 1208 RVA: 0x000220E8 File Offset: 0x000202E8
        public virtual Track initWithTimelineTypeandMaxKeyFrames(Timeline timeline, Track.TrackType trackType, int m)
        {
            t = timeline;
            type = trackType;
            state = TrackState.TRACK_NOT_ACTIVE;
            relative = false;
            nextKeyFrame = -1;
            keyFramesCount = 0;
            keyFramesCapacity = m;
            keyFrames = new KeyFrame[keyFramesCapacity];
            if (type == TrackType.TRACK_ACTION)
            {
                actionSets = new List<List<Action>>();
            }
            return this;
        }

        // Token: 0x060004B9 RID: 1209 RVA: 0x0002214C File Offset: 0x0002034C
        public virtual void initActionKeyFrameandTime(KeyFrame kf, float time)
        {
            keyFrameTimeLeft = time;
            setElementFromKeyFrame(kf);
            if (overrun > 0f)
            {
                updateActionTrack(this, overrun);
                overrun = 0f;
            }
        }

        // Token: 0x060004BA RID: 1210 RVA: 0x00022180 File Offset: 0x00020380
        public virtual void setKeyFrameAt(KeyFrame k, int i)
        {
            keyFrames[i] = k;
            if (i >= keyFramesCount)
            {
                keyFramesCount = i + 1;
            }
            if (type == TrackType.TRACK_ACTION)
            {
                actionSets.Add(k.value.action.actionSet);
            }
        }

        // Token: 0x060004BB RID: 1211 RVA: 0x000221CC File Offset: 0x000203CC
        public virtual float getFrameTime(int f)
        {
            float num = 0f;
            for (int i = 0; i <= f; i++)
            {
                num += keyFrames[i].timeOffset;
            }
            return num;
        }

        // Token: 0x060004BC RID: 1212 RVA: 0x000221FC File Offset: 0x000203FC
        public virtual void updateRange()
        {
            startTime = getFrameTime(0);
            endTime = getFrameTime(keyFramesCount - 1);
        }

        // Token: 0x060004BD RID: 1213 RVA: 0x00022220 File Offset: 0x00020420
        private void initKeyFrameStepFromTowithTime(KeyFrame src, KeyFrame dst, float time)
        {
            keyFrameTimeLeft = time;
            setKeyFrameFromElement(elementPrevState);
            if (!src.debugBreak)
            {
                bool debugBreak = dst.debugBreak;
            }
            setElementFromKeyFrame(src);
            switch (type)
            {
                case TrackType.TRACK_POSITION:
                    currentStepPerSecond.value.pos.x = (dst.value.pos.x - src.value.pos.x) / keyFrameTimeLeft;
                    currentStepPerSecond.value.pos.y = (dst.value.pos.y - src.value.pos.y) / keyFrameTimeLeft;
                    break;
                case TrackType.TRACK_SCALE:
                    currentStepPerSecond.value.scale.scaleX = (dst.value.scale.scaleX - src.value.scale.scaleX) / keyFrameTimeLeft;
                    currentStepPerSecond.value.scale.scaleY = (dst.value.scale.scaleY - src.value.scale.scaleY) / keyFrameTimeLeft;
                    break;
                case TrackType.TRACK_ROTATION:
                    currentStepPerSecond.value.rotation.angle = (dst.value.rotation.angle - src.value.rotation.angle) / keyFrameTimeLeft;
                    break;
                case TrackType.TRACK_COLOR:
                    currentStepPerSecond.value.color.rgba.r = (dst.value.color.rgba.r - src.value.color.rgba.r) / keyFrameTimeLeft;
                    currentStepPerSecond.value.color.rgba.g = (dst.value.color.rgba.g - src.value.color.rgba.g) / keyFrameTimeLeft;
                    currentStepPerSecond.value.color.rgba.b = (dst.value.color.rgba.b - src.value.color.rgba.b) / keyFrameTimeLeft;
                    currentStepPerSecond.value.color.rgba.a = (dst.value.color.rgba.a - src.value.color.rgba.a) / keyFrameTimeLeft;
                    break;
            }
            if (dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN || dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT)
            {
                switch (type)
                {
                    case TrackType.TRACK_POSITION:
                        currentStepPerSecond.value.pos.x *= 2f;
                        currentStepPerSecond.value.pos.y *= 2f;
                        currentStepAcceleration.value.pos.x = currentStepPerSecond.value.pos.x / keyFrameTimeLeft;
                        currentStepAcceleration.value.pos.y = currentStepPerSecond.value.pos.y / keyFrameTimeLeft;
                        if (dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN)
                        {
                            currentStepPerSecond.value.pos.x = 0f;
                            currentStepPerSecond.value.pos.y = 0f;
                        }
                        else
                        {
                            currentStepAcceleration.value.pos.x *= -1f;
                            currentStepAcceleration.value.pos.y *= -1f;
                        }
                        break;
                    case TrackType.TRACK_SCALE:
                        currentStepPerSecond.value.scale.scaleX *= 2f;
                        currentStepPerSecond.value.scale.scaleY *= 2f;
                        currentStepAcceleration.value.scale.scaleX = currentStepPerSecond.value.scale.scaleX / keyFrameTimeLeft;
                        currentStepAcceleration.value.scale.scaleY = currentStepPerSecond.value.scale.scaleY / keyFrameTimeLeft;
                        if (dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN)
                        {
                            currentStepPerSecond.value.scale.scaleX = 0f;
                            currentStepPerSecond.value.scale.scaleY = 0f;
                        }
                        else
                        {
                            currentStepAcceleration.value.scale.scaleX *= -1f;
                            currentStepAcceleration.value.scale.scaleY *= -1f;
                        }
                        break;
                    case TrackType.TRACK_ROTATION:
                        currentStepPerSecond.value.rotation.angle *= 2f;
                        currentStepAcceleration.value.rotation.angle = currentStepPerSecond.value.rotation.angle / keyFrameTimeLeft;
                        if (dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN)
                        {
                            currentStepPerSecond.value.rotation.angle = 0f;
                        }
                        else
                        {
                            currentStepAcceleration.value.rotation.angle *= -1f;
                        }
                        break;
                    case TrackType.TRACK_COLOR:
                        {
                            ColorParams color = currentStepPerSecond.value.color;
                            color.rgba.r = color.rgba.r * 2f;
                            ColorParams color2 = currentStepPerSecond.value.color;
                            color2.rgba.g = color2.rgba.g * 2f;
                            ColorParams color3 = currentStepPerSecond.value.color;
                            color3.rgba.b = color3.rgba.b * 2f;
                            ColorParams color4 = currentStepPerSecond.value.color;
                            color4.rgba.a = color4.rgba.a * 2f;
                            currentStepAcceleration.value.color.rgba.r = currentStepPerSecond.value.color.rgba.r / keyFrameTimeLeft;
                            currentStepAcceleration.value.color.rgba.g = currentStepPerSecond.value.color.rgba.g / keyFrameTimeLeft;
                            currentStepAcceleration.value.color.rgba.b = currentStepPerSecond.value.color.rgba.b / keyFrameTimeLeft;
                            currentStepAcceleration.value.color.rgba.a = currentStepPerSecond.value.color.rgba.a / keyFrameTimeLeft;
                            if (dst.transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN)
                            {
                                currentStepPerSecond.value.color.rgba.r = 0f;
                                currentStepPerSecond.value.color.rgba.g = 0f;
                                currentStepPerSecond.value.color.rgba.b = 0f;
                                currentStepPerSecond.value.color.rgba.a = 0f;
                            }
                            else
                            {
                                ColorParams color5 = currentStepAcceleration.value.color;
                                color5.rgba.r = color5.rgba.r * -1f;
                                ColorParams color6 = currentStepAcceleration.value.color;
                                color6.rgba.g = color6.rgba.g * -1f;
                                ColorParams color7 = currentStepAcceleration.value.color;
                                color7.rgba.b = color7.rgba.b * -1f;
                                ColorParams color8 = currentStepAcceleration.value.color;
                                color8.rgba.a = color8.rgba.a * -1f;
                            }
                            break;
                        }
                }
            }
            if (overrun > 0f)
            {
                updateTrack(this, overrun);
                overrun = 0f;
            }
        }

        // Token: 0x060004BE RID: 1214 RVA: 0x00022AF0 File Offset: 0x00020CF0
        public virtual void setElementFromKeyFrame(KeyFrame kf)
        {
            bool debugBreak = kf.debugBreak;
            switch (type)
            {
                case TrackType.TRACK_POSITION:
                    if (!relative)
                    {
                        t.element.x = kf.value.pos.x;
                        t.element.y = kf.value.pos.y;
                        return;
                    }
                    t.element.x = elementPrevState.value.pos.x + kf.value.pos.x;
                    t.element.y = elementPrevState.value.pos.y + kf.value.pos.y;
                    return;
                case TrackType.TRACK_SCALE:
                    if (!relative)
                    {
                        t.element.scaleX = kf.value.scale.scaleX;
                        t.element.scaleY = kf.value.scale.scaleY;
                        return;
                    }
                    t.element.scaleX = elementPrevState.value.scale.scaleX + kf.value.scale.scaleX;
                    t.element.scaleY = elementPrevState.value.scale.scaleY + kf.value.scale.scaleY;
                    return;
                case TrackType.TRACK_ROTATION:
                    if (!relative)
                    {
                        t.element.rotation = kf.value.rotation.angle;
                        return;
                    }
                    t.element.rotation = elementPrevState.value.rotation.angle + kf.value.rotation.angle;
                    return;
                case TrackType.TRACK_COLOR:
                    if (!relative)
                    {
                        t.element.color = kf.value.color.rgba;
                        return;
                    }
                    t.element.color.r = elementPrevState.value.color.rgba.r + kf.value.color.rgba.r;
                    t.element.color.g = elementPrevState.value.color.rgba.g + kf.value.color.rgba.g;
                    t.element.color.b = elementPrevState.value.color.rgba.b + kf.value.color.rgba.b;
                    t.element.color.a = elementPrevState.value.color.rgba.a + kf.value.color.rgba.a;
                    return;
                case TrackType.TRACK_ACTION:
                    {
                        for (int i = 0; i < kf.value.action.actionSet.Count; i++)
                        {
                            Action action = kf.value.action.actionSet[i];
                            _ = action.actionTarget.handleAction(action.data);
                        }
                        return;
                    }
                default:
                    return;
            }
        }

        // Token: 0x060004BF RID: 1215 RVA: 0x00022E7C File Offset: 0x0002107C
        private void setKeyFrameFromElement(KeyFrame kf)
        {
            bool debugBreak = kf.debugBreak;
            switch (type)
            {
                case TrackType.TRACK_POSITION:
                    kf.value.pos.x = t.element.x;
                    kf.value.pos.y = t.element.y;
                    return;
                case TrackType.TRACK_SCALE:
                    kf.value.scale.scaleX = t.element.scaleX;
                    kf.value.scale.scaleY = t.element.scaleY;
                    return;
                case TrackType.TRACK_ROTATION:
                    kf.value.rotation.angle = t.element.rotation;
                    return;
                case TrackType.TRACK_COLOR:
                    kf.value.color.rgba = t.element.color;
                    break;
                case TrackType.TRACK_ACTION:
                    break;
                default:
                    return;
            }
        }

        // Token: 0x060004C0 RID: 1216 RVA: 0x00022F78 File Offset: 0x00021178
        public static void updateActionTrack(Track thiss, float delta)
        {
            if (thiss == null)
            {
                return;
            }
            if (thiss.state == TrackState.TRACK_NOT_ACTIVE)
            {
                if (!thiss.t.timelineDirReverse)
                {
                    if (thiss.t.time - delta <= thiss.endTime && thiss.t.time >= thiss.startTime)
                    {
                        if (thiss.keyFramesCount > 1)
                        {
                            thiss.state = TrackState.TRACK_ACTIVE;
                            thiss.nextKeyFrame = 0;
                            thiss.overrun = thiss.t.time - thiss.startTime;
                            thiss.nextKeyFrame++;
                            thiss.initActionKeyFrameandTime(thiss.keyFrames[thiss.nextKeyFrame - 1], thiss.keyFrames[thiss.nextKeyFrame].timeOffset);
                            return;
                        }
                        thiss.initActionKeyFrameandTime(thiss.keyFrames[0], 0f);
                        return;
                    }
                }
                else if (thiss.t.time + delta >= thiss.startTime && thiss.t.time <= thiss.endTime)
                {
                    if (thiss.keyFramesCount > 1)
                    {
                        thiss.state = TrackState.TRACK_ACTIVE;
                        thiss.nextKeyFrame = thiss.keyFramesCount - 1;
                        thiss.overrun = thiss.endTime - thiss.t.time;
                        thiss.nextKeyFrame--;
                        thiss.initActionKeyFrameandTime(thiss.keyFrames[thiss.nextKeyFrame + 1], thiss.keyFrames[thiss.nextKeyFrame + 1].timeOffset);
                        return;
                    }
                    thiss.initActionKeyFrameandTime(thiss.keyFrames[0], 0f);
                }
                return;
            }
            thiss.keyFrameTimeLeft -= delta;
            if (thiss.keyFrameTimeLeft <= 1E-06f)
            {
                if (thiss.t != null && thiss.t.delegateTimelineDelegate != null)
                {
                    thiss.t.delegateTimelineDelegate.timelinereachedKeyFramewithIndex(thiss.t, thiss.keyFrames[thiss.nextKeyFrame], thiss.nextKeyFrame);
                }
                thiss.overrun = -thiss.keyFrameTimeLeft;
                if (thiss.nextKeyFrame == thiss.keyFramesCount - 1)
                {
                    thiss.setElementFromKeyFrame(thiss.keyFrames[thiss.nextKeyFrame]);
                    thiss.state = TrackState.TRACK_NOT_ACTIVE;
                    return;
                }
                if (thiss.nextKeyFrame == 0)
                {
                    thiss.setElementFromKeyFrame(thiss.keyFrames[thiss.nextKeyFrame]);
                    thiss.state = TrackState.TRACK_NOT_ACTIVE;
                    return;
                }
                if (!thiss.t.timelineDirReverse)
                {
                    thiss.nextKeyFrame++;
                    thiss.initActionKeyFrameandTime(thiss.keyFrames[thiss.nextKeyFrame - 1], thiss.keyFrames[thiss.nextKeyFrame].timeOffset);
                    return;
                }
                thiss.nextKeyFrame--;
                thiss.initActionKeyFrameandTime(thiss.keyFrames[thiss.nextKeyFrame + 1], thiss.keyFrames[thiss.nextKeyFrame + 1].timeOffset);
            }
        }

        // Token: 0x060004C1 RID: 1217 RVA: 0x0002322C File Offset: 0x0002142C
        public static void updateTrack(Track thiss, float delta)
        {
            Timeline timeline = thiss.t;
            if (thiss.state == TrackState.TRACK_NOT_ACTIVE)
            {
                if (timeline.time >= thiss.startTime && timeline.time <= thiss.endTime)
                {
                    thiss.state = TrackState.TRACK_ACTIVE;
                    if (!timeline.timelineDirReverse)
                    {
                        thiss.nextKeyFrame = 0;
                        thiss.overrun = timeline.time - thiss.startTime;
                        thiss.nextKeyFrame++;
                        thiss.initKeyFrameStepFromTowithTime(thiss.keyFrames[thiss.nextKeyFrame - 1], thiss.keyFrames[thiss.nextKeyFrame], thiss.keyFrames[thiss.nextKeyFrame].timeOffset);
                        return;
                    }
                    thiss.nextKeyFrame = thiss.keyFramesCount - 1;
                    thiss.overrun = thiss.endTime - timeline.time;
                    thiss.nextKeyFrame--;
                    thiss.initKeyFrameStepFromTowithTime(thiss.keyFrames[thiss.nextKeyFrame + 1], thiss.keyFrames[thiss.nextKeyFrame], thiss.keyFrames[thiss.nextKeyFrame + 1].timeOffset);
                }
                return;
            }
            thiss.keyFrameTimeLeft -= delta;
            if (thiss.keyFrames[thiss.nextKeyFrame].transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN || thiss.keyFrames[thiss.nextKeyFrame].transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT)
            {
                KeyFrame keyFrame = thiss.currentStepPerSecond;
                bool debugBreak = keyFrame.debugBreak;
                switch (thiss.type)
                {
                    case TrackType.TRACK_POSITION:
                        {
                            float num = thiss.currentStepAcceleration.value.pos.x * delta;
                            float num2 = thiss.currentStepAcceleration.value.pos.y * delta;
                            thiss.currentStepPerSecond.value.pos.x += num;
                            thiss.currentStepPerSecond.value.pos.y += num2;
                            timeline.element.x += (keyFrame.value.pos.x + num / 2f) * delta;
                            timeline.element.y += (keyFrame.value.pos.y + num2 / 2f) * delta;
                            break;
                        }
                    case TrackType.TRACK_SCALE:
                        {
                            float num3 = thiss.currentStepAcceleration.value.scale.scaleX * delta;
                            float num4 = thiss.currentStepAcceleration.value.scale.scaleY * delta;
                            thiss.currentStepPerSecond.value.scale.scaleX += num3;
                            thiss.currentStepPerSecond.value.scale.scaleY += num4;
                            timeline.element.scaleX += (keyFrame.value.scale.scaleX + num3 / 2f) * delta;
                            timeline.element.scaleY += (keyFrame.value.scale.scaleY + num4 / 2f) * delta;
                            break;
                        }
                    case TrackType.TRACK_ROTATION:
                        {
                            float num5 = thiss.currentStepAcceleration.value.rotation.angle * delta;
                            thiss.currentStepPerSecond.value.rotation.angle += num5;
                            timeline.element.rotation += (keyFrame.value.rotation.angle + num5 / 2f) * delta;
                            break;
                        }
                    case TrackType.TRACK_COLOR:
                        {
                            ColorParams color = thiss.currentStepPerSecond.value.color;
                            color.rgba.r = color.rgba.r + thiss.currentStepAcceleration.value.color.rgba.r * delta;
                            ColorParams color2 = thiss.currentStepPerSecond.value.color;
                            color2.rgba.g = color2.rgba.g + thiss.currentStepAcceleration.value.color.rgba.g * delta;
                            ColorParams color3 = thiss.currentStepPerSecond.value.color;
                            color3.rgba.b = color3.rgba.b + thiss.currentStepAcceleration.value.color.rgba.b * delta;
                            ColorParams color4 = thiss.currentStepPerSecond.value.color;
                            color4.rgba.a = color4.rgba.a + thiss.currentStepAcceleration.value.color.rgba.a * delta;
                            float num6 = thiss.currentStepAcceleration.value.color.rgba.r * delta;
                            float num7 = thiss.currentStepAcceleration.value.color.rgba.g * delta;
                            float num8 = thiss.currentStepAcceleration.value.color.rgba.b * delta;
                            float num9 = thiss.currentStepAcceleration.value.color.rgba.a * delta;
                            ColorParams color5 = thiss.currentStepPerSecond.value.color;
                            color5.rgba.r = color5.rgba.r + num6;
                            ColorParams color6 = thiss.currentStepPerSecond.value.color;
                            color6.rgba.g = color6.rgba.g + num7;
                            ColorParams color7 = thiss.currentStepPerSecond.value.color;
                            color7.rgba.b = color7.rgba.b + num8;
                            ColorParams color8 = thiss.currentStepPerSecond.value.color;
                            color8.rgba.a = color8.rgba.a + num9;
                            BaseElement element = timeline.element;
                            element.color.r = element.color.r + (keyFrame.value.color.rgba.r + num6 / 2f) * delta;
                            BaseElement element2 = timeline.element;
                            element2.color.g = element2.color.g + (keyFrame.value.color.rgba.g + num7 / 2f) * delta;
                            BaseElement element3 = timeline.element;
                            element3.color.b = element3.color.b + (keyFrame.value.color.rgba.b + num8 / 2f) * delta;
                            BaseElement element4 = timeline.element;
                            element4.color.a = element4.color.a + (keyFrame.value.color.rgba.a + num9 / 2f) * delta;
                            break;
                        }
                }
            }
            else if (thiss.keyFrames[thiss.nextKeyFrame].transitionType == KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR)
            {
                switch (thiss.type)
                {
                    case TrackType.TRACK_POSITION:
                        timeline.element.x += thiss.currentStepPerSecond.value.pos.x * delta;
                        timeline.element.y += thiss.currentStepPerSecond.value.pos.y * delta;
                        break;
                    case TrackType.TRACK_SCALE:
                        timeline.element.scaleX += thiss.currentStepPerSecond.value.scale.scaleX * delta;
                        timeline.element.scaleY += thiss.currentStepPerSecond.value.scale.scaleY * delta;
                        break;
                    case TrackType.TRACK_ROTATION:
                        timeline.element.rotation += thiss.currentStepPerSecond.value.rotation.angle * delta;
                        break;
                    case TrackType.TRACK_COLOR:
                        {
                            BaseElement element5 = timeline.element;
                            element5.color.r = element5.color.r + thiss.currentStepPerSecond.value.color.rgba.r * delta;
                            BaseElement element6 = timeline.element;
                            element6.color.g = element6.color.g + thiss.currentStepPerSecond.value.color.rgba.g * delta;
                            BaseElement element7 = timeline.element;
                            element7.color.b = element7.color.b + thiss.currentStepPerSecond.value.color.rgba.b * delta;
                            BaseElement element8 = timeline.element;
                            element8.color.a = element8.color.a + thiss.currentStepPerSecond.value.color.rgba.a * delta;
                            break;
                        }
                }
            }
            if (thiss.keyFrameTimeLeft <= 1E-06f)
            {
                if (timeline.delegateTimelineDelegate != null)
                {
                    timeline.delegateTimelineDelegate.timelinereachedKeyFramewithIndex(timeline, thiss.keyFrames[thiss.nextKeyFrame], thiss.nextKeyFrame);
                }
                thiss.overrun = -thiss.keyFrameTimeLeft;
                if (thiss.nextKeyFrame == thiss.keyFramesCount - 1)
                {
                    thiss.setElementFromKeyFrame(thiss.keyFrames[thiss.nextKeyFrame]);
                    thiss.state = TrackState.TRACK_NOT_ACTIVE;
                    return;
                }
                if (thiss.nextKeyFrame == 0)
                {
                    thiss.setElementFromKeyFrame(thiss.keyFrames[thiss.nextKeyFrame]);
                    thiss.state = TrackState.TRACK_NOT_ACTIVE;
                    return;
                }
                if (!timeline.timelineDirReverse)
                {
                    thiss.nextKeyFrame++;
                    thiss.initKeyFrameStepFromTowithTime(thiss.keyFrames[thiss.nextKeyFrame - 1], thiss.keyFrames[thiss.nextKeyFrame], thiss.keyFrames[thiss.nextKeyFrame].timeOffset);
                    return;
                }
                thiss.nextKeyFrame--;
                thiss.initKeyFrameStepFromTowithTime(thiss.keyFrames[thiss.nextKeyFrame + 1], thiss.keyFrames[thiss.nextKeyFrame], thiss.keyFrames[thiss.nextKeyFrame + 1].timeOffset);
            }
        }

        // Token: 0x040009F8 RID: 2552
        public Track.TrackType type;

        // Token: 0x040009F9 RID: 2553
        public Track.TrackState state;

        // Token: 0x040009FA RID: 2554
        public bool relative;

        // Token: 0x040009FB RID: 2555
        public float startTime;

        // Token: 0x040009FC RID: 2556
        public float endTime;

        // Token: 0x040009FD RID: 2557
        public int keyFramesCount;

        // Token: 0x040009FE RID: 2558
        public KeyFrame[] keyFrames;

        // Token: 0x040009FF RID: 2559
        public Timeline t;

        // Token: 0x04000A00 RID: 2560
        public int nextKeyFrame;

        // Token: 0x04000A01 RID: 2561
        public int keyFramesCapacity;

        // Token: 0x04000A02 RID: 2562
        public KeyFrame currentStepPerSecond;

        // Token: 0x04000A03 RID: 2563
        public KeyFrame currentStepAcceleration;

        // Token: 0x04000A04 RID: 2564
        public float keyFrameTimeLeft;

        // Token: 0x04000A05 RID: 2565
        public KeyFrame elementPrevState;

        // Token: 0x04000A06 RID: 2566
        public float overrun;

        // Token: 0x04000A07 RID: 2567
        public List<List<Action>> actionSets;

        // Token: 0x020000AA RID: 170
        public enum TrackType
        {
            // Token: 0x04000A09 RID: 2569
            TRACK_POSITION,
            // Token: 0x04000A0A RID: 2570
            TRACK_SCALE,
            // Token: 0x04000A0B RID: 2571
            TRACK_ROTATION,
            // Token: 0x04000A0C RID: 2572
            TRACK_COLOR,
            // Token: 0x04000A0D RID: 2573
            TRACK_ACTION,
            // Token: 0x04000A0E RID: 2574
            TRACKS_COUNT
        }

        // Token: 0x020000AB RID: 171
        public enum TrackState
        {
            // Token: 0x04000A10 RID: 2576
            TRACK_NOT_ACTIVE,
            // Token: 0x04000A11 RID: 2577
            TRACK_ACTIVE
        }
    }
}
