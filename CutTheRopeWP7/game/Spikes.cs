using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

using Microsoft.Xna.Framework.Audio;

namespace ctr_wp7.game
{
    // Token: 0x02000050 RID: 80
    internal sealed class Spikes : CTRGameObject, TimelineDelegate, ButtonDelegate
    {
        // Token: 0x06000277 RID: 631 RVA: 0x0000FE58 File Offset: 0x0000E058
        public NSObject initWithPosXYWidthAndAngleToggled(float px, float py, int w, double an, int t)
        {
            int num = -1;
            if (t != -1)
            {
                num = 136 + w - 1;
            }
            else
            {
                switch (w)
                {
                    case 1:
                        num = 131;
                        break;
                    case 2:
                        num = 130;
                        break;
                    case 3:
                        num = 129;
                        break;
                    case 4:
                        num = 123;
                        break;
                    case 5:
                        num = 135;
                        break;
                }
            }
            if (initWithTexture(Application.getTexture(num)) == null)
            {
                return null;
            }
            if (t > 0)
            {
                doRestoreCutTransparency();
                int num2 = (t - 1) * 2;
                int num3 = 1 + ((t - 1) * 2);
                Image image = Image_createWithResIDQuad(140, num2);
                Image image2 = Image_createWithResIDQuad(140, num3);
                image.doRestoreCutTransparency();
                image2.doRestoreCutTransparency();
                rotateButton = new Button().initWithUpElementDownElementandID(image, image2, 0);
                rotateButton.delegateButtonDelegate = this;
                rotateButton.anchor = rotateButton.parentAnchor = 18;
                _ = addChild(rotateButton);
                Vector quadOffset = getQuadOffset(140, num2);
                Vector quadSize = getQuadSize(140, num2);
                Vector vector = vect(image.texture.preCutSize.x, image.texture.preCutSize.y);
                Vector vector2 = vectSub(vector, vectAdd(quadSize, quadOffset));
                rotateButton.setTouchIncreaseLeftRightTopBottom(-quadOffset.x + (quadSize.x / 2f), -vector2.x + (quadSize.x / 2f), -quadOffset.y + (quadSize.y / 2f), -vector2.y + (quadSize.y / 2f));
            }
            passColorToChilds = false;
            spikesNormal = false;
            origRotation = rotation = (float)an;
            x = px;
            y = py;
            setToggled(t);
            updateRotation();
            if (w == 5)
            {
                addAnimationWithIDDelayLoopFirstLast(0, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 0);
                addAnimationWithIDDelayLoopFirstLast(1, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 1, 4);
                doRestoreCutTransparency();
            }
            touchIndex = -1;
            return this;
        }

        // Token: 0x06000278 RID: 632 RVA: 0x00010080 File Offset: 0x0000E280
        public void updateRotation()
        {
            float num;
            if (electro)
            {
                num = width - 130;
            }
            else
            {
                num = texture.quadRects[quadToDraw].w;
            }
            num /= 2f;
            t1.x = x - num;
            t2.x = x + num;
            t1.y = t2.y = y - 5f;
            b1.x = t1.x;
            b2.x = t2.x;
            b1.y = b2.y = y + 5f;
            angle = DEGREES_TO_RADIANS(rotation);
            t1 = vectRotateAround(t1, angle, x, y);
            t2 = vectRotateAround(t2, angle, x, y);
            b1 = vectRotateAround(b1, angle, x, y);
            b2 = vectRotateAround(b2, angle, x, y);
        }

        // Token: 0x06000279 RID: 633 RVA: 0x00010204 File Offset: 0x0000E404
        public void turnElectroOff()
        {
            electroOn = false;
            playTimeline(0);
            electroTimer = offTime;
            if (sndElectric != null)
            {
                sndElectric.Stop();
                Application.sharedSoundMgr().ClearLooped(sndElectric);
                sndElectric = null;
            }
        }

        // Token: 0x0600027A RID: 634 RVA: 0x00010255 File Offset: 0x0000E455
        public void turnElectroOn()
        {
            electroOn = true;
            playTimeline(1);
            electroTimer = onTime;
            sndElectric = CTRSoundMgr._playSoundLooped(38);
        }

        // Token: 0x0600027B RID: 635 RVA: 0x00010280 File Offset: 0x0000E480
        public void rotateSpikes()
        {
            spikesNormal = !spikesNormal;
            removeTimeline(2);
            float num = spikesNormal ? 90 : 0;
            float num2 = origRotation + num;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeRotation((int)rotation, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation((int)num2, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, Math.Abs(num2 - rotation) / 90f * 0.3f));
            timeline.delegateTimelineDelegate = this;
            addTimelinewithID(timeline, 2);
            playTimeline(2);
            updateRotationFlag = true;
            rotateButton.scaleX = -rotateButton.scaleX;
        }

        // Token: 0x0600027C RID: 636 RVA: 0x0001033A File Offset: 0x0000E53A
        public void setToggled(int t)
        {
            toggled = t;
        }

        // Token: 0x0600027D RID: 637 RVA: 0x00010343 File Offset: 0x0000E543
        public int getToggled()
        {
            return toggled;
        }

        // Token: 0x0600027E RID: 638 RVA: 0x0001034C File Offset: 0x0000E54C
        public override void update(float delta)
        {
            base.update(delta);
            if (mover != null || updateRotationFlag)
            {
                updateRotation();
            }
            if (electro)
            {
                if (electroOn)
                {
                    _ = Mover.moveVariableToTarget(ref electroTimer, 0f, 1f, delta);
                    if (electroTimer == 0.0)
                    {
                        turnElectroOff();
                        return;
                    }
                }
                else
                {
                    _ = Mover.moveVariableToTarget(ref electroTimer, 0f, 1f, delta);
                    if (electroTimer == 0.0)
                    {
                        turnElectroOn();
                    }
                }
            }
        }

        // Token: 0x0600027F RID: 639 RVA: 0x000103E5 File Offset: 0x0000E5E5
        public static void timelineReachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x06000280 RID: 640 RVA: 0x000103E7 File Offset: 0x0000E5E7
        public void timelineFinished(Timeline t)
        {
            updateRotationFlag = false;
        }

        // Token: 0x06000281 RID: 641 RVA: 0x000103F0 File Offset: 0x0000E5F0
        public void onButtonPressed(int n)
        {
            if (n != 0)
            {
                return;
            }
            delegateRotateAllSpikesWithID(toggled);
            if (spikesNormal)
            {
                CTRSoundMgr._playSound(53);
                return;
            }
            CTRSoundMgr._playSound(54);
        }

        // Token: 0x06000282 RID: 642 RVA: 0x0001042C File Offset: 0x0000E62C
        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x0400086A RID: 2154
        private const float SPIKES_HEIGHT = 10f;

        // Token: 0x0400086B RID: 2155
        private int toggled;

        // Token: 0x0400086C RID: 2156
        public double angle;

        // Token: 0x0400086D RID: 2157
        public Vector t1;

        // Token: 0x0400086E RID: 2158
        public Vector t2;

        // Token: 0x0400086F RID: 2159
        public Vector b1;

        // Token: 0x04000870 RID: 2160
        public Vector b2;

        // Token: 0x04000871 RID: 2161
        public bool electro;

        // Token: 0x04000872 RID: 2162
        public float initialDelay;

        // Token: 0x04000873 RID: 2163
        public float onTime;

        // Token: 0x04000874 RID: 2164
        public float offTime;

        // Token: 0x04000875 RID: 2165
        public bool electroOn;

        // Token: 0x04000876 RID: 2166
        public float electroTimer;

        // Token: 0x04000877 RID: 2167
        private bool updateRotationFlag;

        // Token: 0x04000878 RID: 2168
        private bool spikesNormal;

        // Token: 0x04000879 RID: 2169
        private float origRotation;

        // Token: 0x0400087A RID: 2170
        public Button rotateButton;

        // Token: 0x0400087B RID: 2171
        public int touchIndex;

        // Token: 0x0400087C RID: 2172
        public rotateAllSpikesWithID delegateRotateAllSpikesWithID;

        // Token: 0x0400087D RID: 2173
        private SoundEffectInstance sndElectric;

        // Token: 0x02000051 RID: 81
        private enum SPIKES_ANIM
        {
            // Token: 0x0400087F RID: 2175
            SPIKES_ANIM_ELECTRODES_BASE,
            // Token: 0x04000880 RID: 2176
            SPIKES_ANIM_ELECTRODES_ELECTRIC,
            // Token: 0x04000881 RID: 2177
            SPIKES_ANIM_ROTATION_ADJUSTED
        }

        // Token: 0x02000052 RID: 82
        private enum SPIKES_ROTATION
        {
            // Token: 0x04000883 RID: 2179
            SPIKES_ROTATION_BUTTON
        }

        // Token: 0x02000053 RID: 83
        // (Invoke) Token: 0x06000285 RID: 645
        public delegate void rotateAllSpikesWithID(int sid);
    }
}
