using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000084 RID: 132
    internal class Star : CTRGameObject
    {
        // Token: 0x060003DE RID: 990 RVA: 0x00018A01 File Offset: 0x00016C01
        public static Star Star_create(Texture2D t)
        {
            return (Star)new Star().initWithTexture(t);
        }

        // Token: 0x060003DF RID: 991 RVA: 0x00018A13 File Offset: 0x00016C13
        public static Star Star_createWithResID(int r)
        {
            return Star_create(Application.getTexture(r));
        }

        // Token: 0x060003E0 RID: 992 RVA: 0x00018A20 File Offset: 0x00016C20
        public static Star Star_createWithResIDQuad(int r, int q)
        {
            Star star = Star_create(Application.getTexture(r));
            star.setDrawQuad(q);
            return star;
        }

        // Token: 0x060003E1 RID: 993 RVA: 0x00018A41 File Offset: 0x00016C41
        public override NSObject init()
        {
            if (base.init() != null)
            {
                timedAnim = null;
            }
            return this;
        }

        // Token: 0x060003E2 RID: 994 RVA: 0x00018A54 File Offset: 0x00016C54
        public override void update(float delta)
        {
            if ((double)timeout > 0.0 && (double)time > 0.0)
            {
                Mover.moveVariableToTarget(ref time, 0f, 1f, delta);
            }
            base.update(delta);
        }

        // Token: 0x060003E3 RID: 995 RVA: 0x00018AA3 File Offset: 0x00016CA3
        public override void draw()
        {
            if (timedAnim != null)
            {
                timedAnim.draw();
            }
            base.draw();
        }

        // Token: 0x060003E4 RID: 996 RVA: 0x00018AC0 File Offset: 0x00016CC0
        public virtual void createAnimations()
        {
            if ((double)timeout > 0.0)
            {
                timedAnim = Animation_createWithResID(127);
                timedAnim.anchor = (timedAnim.parentAnchor = 18);
                float num = timeout / 37f;
                timedAnim.addAnimationWithIDDelayLoopFirstLast(0, num, Timeline.LoopType.TIMELINE_NO_LOOP, 19, 55);
                timedAnim.playTimeline(0);
                time = timeout;
                timedAnim.visible = false;
                addChild(timedAnim);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timedAnim.addTimelinewithID(timeline, 1);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline2.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.25));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.25));
                addTimelinewithID(timeline2, 1);
            }
            bb = new Rectangle(22f, 20f, 30f, 30f);
            Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(5);
            timeline3.addKeyFrame(KeyFrame.makePos((int)x, (int)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0f));
            timeline3.addKeyFrame(KeyFrame.makePos((int)x, (int)y - 3, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
            timeline3.addKeyFrame(KeyFrame.makePos((int)x, (int)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
            timeline3.addKeyFrame(KeyFrame.makePos((int)x, (int)y + 3, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
            timeline3.addKeyFrame(KeyFrame.makePos((int)x, (int)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
            timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
            addTimelinewithID(timeline3, 0);
            playTimeline(0);
            Timeline.updateTimeline(timeline3, (float)((double)RND_RANGE(0, 20) / 10.0));
            Animation animation = Animation_createWithResID(127);
            animation.doRestoreCutTransparency();
            animation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 1, 18);
            animation.playTimeline(0);
            Timeline.updateTimeline(animation.getTimeline(0), (float)((double)RND_RANGE(0, 20) / 10.0));
            animation.anchor = (animation.parentAnchor = 18);
            addChild(animation);
        }

        // Token: 0x0400094E RID: 2382
        public float time;

        // Token: 0x0400094F RID: 2383
        public float timeout;

        // Token: 0x04000950 RID: 2384
        public Animation timedAnim;
    }
}
