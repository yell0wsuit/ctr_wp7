using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.sfe;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000112 RID: 274
    internal class Lantern : CTRGameObject
    {
        // Token: 0x06000853 RID: 2131 RVA: 0x0004A29C File Offset: 0x0004849C
        public Lantern initWithPosition(Vector position)
        {
            Texture2D texture = Application.getTexture(186);
            if (base.initWithTexture(texture) != null)
            {
                sharedCandyPoint = null;
                getAllLanterns().Add(this);
                NSREL(this);
                x = position.x;
                y = position.y;
                lanternState = 0;
                if (delayedDispatcher == null)
                {
                    delayedDispatcher = (DelayedDispatcher)new DelayedDispatcher().init();
                }
                fire = Image_createWithResIDQuad(186, 1);
                fire.anchor = fire.parentAnchor = 18;
                fire.color = RGBAColor.transparentRGBA;
                fire.doRestoreCutTransparency();
                _ = addChild(fire);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
                timeline.addKeyFrame(KeyFrame.makeScale(1.4, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.05, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.7, 0.7, 0.7, 0.7), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_PING_PONG);
                fire.addTimelinewithID(timeline, 2);
                idleForm = Image_createWithResIDQuad(186, 0);
                idleForm.anchor = idleForm.parentAnchor = 18;
                idleForm.doRestoreCutTransparency();
                _ = addChild(idleForm);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                idleForm.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                idleForm.addTimelinewithID(timeline, 1);
                activeForm = Image_createWithResIDQuad(186, 2);
                activeForm.anchor = activeForm.parentAnchor = 18;
                activeForm.color = RGBAColor.transparentRGBA;
                activeForm.y = 1f;
                activeForm.doRestoreCutTransparency();
                _ = addChild(activeForm);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                activeForm.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                activeForm.addTimelinewithID(timeline, 1);
                int num = 3;
                CTRPreferences ctrpreferences = Application.sharedPreferences();
                int intForKey = ctrpreferences.getIntForKey("PREFS_SELECTED_CANDY");
                num += intForKey;
                innerCandy = Image_createWithResIDQuad(186, num);
                innerCandy.anchor = innerCandy.parentAnchor = 18;
                innerCandy.color = RGBAColor.transparentRGBA;
                innerCandy.y = -4f;
                innerCandy.doRestoreCutTransparency();
                _ = addChild(innerCandy);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(4);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.07));
                timeline.addKeyFrame(KeyFrame.makeScale(0.85, 1.05, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, -4.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, -1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.05));
                innerCandy.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.6, 0.6, 0.6, 0.6), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.06));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.04));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.15, 0.8, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.06));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.04));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, -4.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.06));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, 4.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.04));
                innerCandy.addTimelinewithID(timeline, 1);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.93, 0.93, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.35));
                timeline.addKeyFrame(KeyFrame.makeScale(0.87, 0.87, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.35));
                timeline.addKeyFrame(KeyFrame.makeScale(0.93, 0.93, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.35));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.35));
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                innerCandy.addTimelinewithID(timeline, 2);
            }
            return this;
        }

        // Token: 0x06000854 RID: 2132 RVA: 0x0004AA4A File Offset: 0x00048C4A
        public void _captureCandy(NSObject obj)
        {
            captureCandy((ConstraintedPoint)obj);
        }

        // Token: 0x06000855 RID: 2133 RVA: 0x0004AA58 File Offset: 0x00048C58
        public void captureCandy(ConstraintedPoint candyPoint)
        {
            CTRSoundMgr._playSound(64);
            sharedCandyPoint = candyPoint;
            candyPoint.disableGravity = true;
            candyPoint.pos = candyPoint.prevPos = vect(x, y);
            List<Lantern> list = getAllLanterns();
            foreach (Lantern lantern in list)
            {
                lantern.lanternState = 1;
                lantern.idleForm.playTimeline(0);
                lantern.activeForm.playTimeline(0);
                lantern.innerCandy.playTimeline(0);
                lantern.fire.scaleX = 1.4f;
                lantern.fire.scaleY = 1f;
                lantern.fire.color = RGBAColor.MakeRGBA(0.7, 0.7, 0.7, 0.7);
                lantern.delayedDispatcher.cancelAllDispatches();
                lantern.delayedDispatcher.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(lantern.fire._playTimeline), NSInt.intWithInt(2), 0.4 * (double)RND_0_1);
                lantern.delayedDispatcher.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(lantern.innerCandy._playTimeline), NSInt.intWithInt(2), 0.2 + (0.2 * (double)RND_0_1));
            }
        }

        // Token: 0x06000856 RID: 2134 RVA: 0x0004ABE8 File Offset: 0x00048DE8
        public static List<Lantern> getAllLanterns()
        {
            if (allLanterns == null)
            {
                allLanterns = [];
            }
            return allLanterns;
        }

        // Token: 0x06000857 RID: 2135 RVA: 0x0004AC00 File Offset: 0x00048E00
        public static void removeAllLanterns()
        {
            getAllLanterns().Clear();
        }

        // Token: 0x06000858 RID: 2136 RVA: 0x0004AC0C File Offset: 0x00048E0C
        public override void update(float delta)
        {
            prevPos = vect(x, y);
            base.update(delta);
            delayedDispatcher.update(delta);
            if (sharedCandyPoint != null)
            {
                sharedCandyPoint.pos = sharedCandyPoint.prevPos = vect(x, y);
                if (lanternState != 1)
                {
                    lanternState = 1;
                }
            }
        }

        // Token: 0x06000859 RID: 2137 RVA: 0x0004AC84 File Offset: 0x00048E84
        public override bool onTouchDownXY(float tx, float ty)
        {
            float num = vectDistance(vect(tx, ty), vect(x, y));
            if (lanternState == 1 && num < LANTERN_TOUCH_RADIUS && sharedCandyPoint != null)
            {
                initiateReleasingCandy();
                return true;
            }
            return false;
        }

        // Token: 0x0600085A RID: 2138 RVA: 0x0004ACD0 File Offset: 0x00048ED0
        public override void draw()
        {
            base.preDraw();
            base.postDraw();
        }

        // Token: 0x0600085B RID: 2139 RVA: 0x0004ACDE File Offset: 0x00048EDE
        public override void dealloc()
        {
            idleForm = null;
            activeForm = null;
            innerCandy = null;
            fire = null;
            delayedDispatcher = null;
            base.dealloc();
        }

        // Token: 0x0600085C RID: 2140 RVA: 0x0004AD09 File Offset: 0x00048F09
        private void releaseCandy(NSObject obj)
        {
            sharedCandyPoint.disableGravity = false;
            sharedCandyPoint.pos = vect(x, y);
            sharedCandyPoint.prevPos = prevPos;
            sharedCandyPoint = null;
        }

        // Token: 0x0600085D RID: 2141 RVA: 0x0004AD47 File Offset: 0x00048F47
        private void becomeCandyAware(NSObject obj)
        {
            ((Lantern)obj).lanternState = 0;
        }

        // Token: 0x0600085E RID: 2142 RVA: 0x0004AD58 File Offset: 0x00048F58
        private void initiateReleasingCandy()
        {
            CTRSoundMgr._playSound(65);
            List<Lantern> list = getAllLanterns();
            foreach (Lantern lantern in list)
            {
                lantern.idleForm.playTimeline(1);
                lantern.activeForm.playTimeline(1);
                lantern.innerCandy.playTimeline(1);
                if (lantern.fire.getTimeline(2).state == Timeline.TimelineState.TIMELINE_PLAYING)
                {
                    lantern.fire.stopCurrentTimeline();
                }
                lantern.fire.color = RGBAColor.transparentRGBA;
                lantern.delayedDispatcher.cancelAllDispatches();
                lantern.delayedDispatcher.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(becomeCandyAware), lantern, LANTERN_INACTIVE_DELAY + 0.1f);
            }
            delayedDispatcher.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(releaseCandy), null, 0.01f);
        }

        // Token: 0x04000DEE RID: 3566
        public const float LANTERN_CANDY_REVEAL_TIME = 0.1f;

        // Token: 0x04000DEF RID: 3567
        private static ConstraintedPoint sharedCandyPoint;

        // Token: 0x04000DF0 RID: 3568
        private static float LANTERN_TOUCH_RADIUS = 35f;

        // Token: 0x04000DF1 RID: 3569
        private static float LANTERN_INACTIVE_DELAY = 0.4f;

        // Token: 0x04000DF2 RID: 3570
        public int lanternState;

        // Token: 0x04000DF3 RID: 3571
        private static List<Lantern> allLanterns;

        // Token: 0x04000DF4 RID: 3572
        public Vector prevPos;

        // Token: 0x04000DF5 RID: 3573
        private Image idleForm;

        // Token: 0x04000DF6 RID: 3574
        private Image activeForm;

        // Token: 0x04000DF7 RID: 3575
        private Image innerCandy;

        // Token: 0x04000DF8 RID: 3576
        private Image fire;

        // Token: 0x04000DF9 RID: 3577
        private DelayedDispatcher delayedDispatcher;

        // Token: 0x02000113 RID: 275
        private enum LANTERN_ACTIVATION
        {
            // Token: 0x04000DFB RID: 3579
            LANTERN_ACTIVATION_TL,
            // Token: 0x04000DFC RID: 3580
            LANTERN_DEACTIVATION_TL,
            // Token: 0x04000DFD RID: 3581
            LANTERN_FIRE_BOUNCING_TL
        }
    }
}
