using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class SteamTube : BaseElement, TimelineDelegate
    {
        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public SteamTube initWithPositionAngle(Vector position, float angle)
        {
            if (init() != null)
            {
                dd ??= (DelayedDispatcher)new DelayedDispatcher().init();
                x = position.x;
                y = position.y;
                rotation = angle;
                anchor = 18;
                steamBack = null;
                steamFront = null;
                phase = 0f;
                steamState = 0;
                tube = Image.Image_createWithResIDQuad(184, 0);
                tube.x = position.x;
                tube.y = position.y;
                tube.anchor = 10;
                _ = addChild(tube);
                valve = Image.Image_createWithResIDQuad(184, 1);
                valve.x = position.x;
                valve.y = position.y + 27f;
                valve.anchor = 18;
                _ = addChild(valve);
                steamBack = (BaseElement)new BaseElement().init();
                steamFront = (BaseElement)new BaseElement().init();
                _ = addChild(steamBack);
                _ = addChild(steamFront);
                adjustSteam();
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
                timeline.addKeyFrame(KeyFrame.makeRotation(180.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5499999999999999));
                valve.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
                timeline.addKeyFrame(KeyFrame.makeRotation(-180.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5499999999999999));
                valve.addTimelinewithID(timeline, 1);
            }
            return this;
        }

        public void drawBack()
        {
            preDraw();
            tube.draw();
            valve.draw();
            steamBack.draw();
            restoreTransformations(this);
        }

        public void drawFront()
        {
            preDraw();
            steamFront.draw();
            restoreTransformations(this);
        }

        public float getCurrentHeightModulated()
        {
            float currentHeight = getCurrentHeight();
            return currentHeight + (1f * sinf(6f * phase));
        }

        public override void update(float delta)
        {
            base.update(delta);
            dd.update(delta);
            phase += delta;
        }

        public override void dealloc()
        {
            tube = null;
            valve = null;
            steamBack = null;
            steamFront = null;
            dd = null;
            base.dealloc();
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            Vector vector = vectAdd(vect(x, y), vectRotate(vect(0f, 28f), (double)DEGREES_TO_RADIANS(rotation)));
            float num = vectLength(vectSub(vect(tx, ty), vector));
            if (num < 30f)
            {
                int num2 = 0;
                switch (steamState)
                {
                    case 0:
                        steamState++;
                        num2 = 0;
                        CTRSoundMgr._playSound(62);
                        break;
                    case 1:
                        steamState++;
                        num2 = 0;
                        CTRSoundMgr._playSound(61);
                        break;
                    case 2:
                        steamState = 0;
                        num2 = 1;
                        CTRSoundMgr._playSound(63);
                        break;
                }
                adjustSteam();
                if (valve.getTimeline(0).state != Timeline.TimelineState.TIMELINE_PLAYING && valve.getTimeline(1).state != Timeline.TimelineState.TIMELINE_PLAYING)
                {
                    valve.playTimeline(num2);
                }
                return true;
            }
            return false;
        }

        public void timelineFinished(Timeline t)
        {
            BaseElement element = t.element;
            element.parent.removeChild(element);
        }

        private float getCurrentHeight()
        {
            float num = 0f;
            switch (steamState)
            {
                case 0:
                    num = 32.9f;
                    break;
                case 1:
                    num = 94f;
                    break;
                case 2:
                    num = 141f;
                    break;
            }
            return num;
        }

        private void adjustSteam()
        {
            phase = 0f;
            if (steamBack != null)
            {
                Dictionary<int, BaseElement> childs = steamBack.getChilds();
                foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
                {
                    BaseElement value = keyValuePair.Value;
                    value?.getTimeline(0).setTimelineLoopType(Timeline.LoopType.TIMELINE_NO_LOOP);
                }
            }
            if (steamFront != null)
            {
                Dictionary<int, BaseElement> childs2 = steamFront.getChilds();
                foreach (KeyValuePair<int, BaseElement> keyValuePair2 in childs2)
                {
                    BaseElement value2 = keyValuePair2.Value;
                    value2?.getTimeline(0).setTimelineLoopType(Timeline.LoopType.TIMELINE_NO_LOOP);
                }
            }
            if (steamState != 3)
            {
                steamBack.anchor = steamBack.parentAnchor = 18;
                steamFront.anchor = steamFront.parentAnchor = 18;
                int num = 7;
                if (steamState == 1)
                {
                    num = 14;
                }
                if (steamState == 2)
                {
                    num = 20;
                }
                for (int i = 0; i < num; i++)
                {
                    int num2 = 0;
                    int num3 = 0;
                    switch (i % 3)
                    {
                        case 0:
                            num2 = 24;
                            num3 = 34;
                            break;
                        case 1:
                            num2 = 13;
                            num3 = 23;
                            break;
                        case 2:
                            num2 = 2;
                            num3 = 12;
                            break;
                    }
                    float num4 = 0.6f;
                    float num5 = num4 / (num3 - num2 + 1);
                    float num6 = -getCurrentHeight();
                    num6 *= 1f + (0.1f * RND_MINUS1_1);
                    if (steamState == 1 && (i % 3 == 1 || i % 3 == 2))
                    {
                        num6 *= 0.95f;
                    }
                    if (steamState == 2 && (i % 3 == 1 || i % 3 == 2))
                    {
                        num6 *= 0.94f;
                    }
                    float num7 = 1f;
                    if (i % 3 == 0)
                    {
                        num7 = 0f;
                    }
                    else if (i % 3 == 1)
                    {
                        num7 *= steamState;
                    }
                    else if (i % 3 == 2)
                    {
                        num7 *= (float)-(float)steamState;
                    }
                    Animation animation = Animation.Animation_createWithResID(184);
                    animation.doRestoreCutTransparency();
                    _ = animation.addAnimationDelayLoopFirstLast(num5, Timeline.LoopType.TIMELINE_REPLAY, num2, num3);
                    animation.anchor = animation.parentAnchor = 18;
                    Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                    timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                    timeline.addKeyFrame(KeyFrame.makePos((double)num7, (double)num6, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)num4));
                    timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                    timeline.addKeyFrame(KeyFrame.makeScale(1.5, 1.5, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num4));
                    timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                    timeline.delegateTimelineDelegate = this;
                    BaseElement baseElement = new();
                    _ = baseElement.init();
                    baseElement.addTimelinewithID(timeline, 0);
                    dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(startPuffFloatingAndAnimation), baseElement, num4 * i / num);
                    _ = baseElement.addChild(animation);
                    baseElement.anchor = baseElement.parentAnchor = 18;
                    baseElement.setEnabled(false);
                    _ = i % 3 == 0 ? steamBack.addChild(baseElement) : steamFront.addChild(baseElement);
                }
            }
        }

        private void startPuffFloatingAndAnimation(NSObject param)
        {
            BaseElement baseElement = (BaseElement)param;
            baseElement.setEnabled(true);
            baseElement.playTimeline(0);
            BaseElement child = baseElement.getChild(baseElement.childsCount() - 1);
            child.playTimeline(0);
        }

        private const int STEAM_TUBE_TOUCH_RADIUS = 30;

        private const double PUFF_LIFETIME = 0.6;

        public int steamState;

        private DelayedDispatcher dd;

        private Image tube;

        private Image valve;

        private BaseElement steamBack;

        private BaseElement steamFront;

        private float phase;

        private enum STEAM_TUBE_VALVE
        {
            STEAM_TUBE_VALVE_ROTATION_CW,
            STEAM_TUBE_VALVE_ROTATION_CCW
        }
    }
}
