using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

using Microsoft.Xna.Framework.Audio;

namespace ctr_wp7.game
{
    internal sealed class Spikes : CTRGameObject, TimelineDelegate, ButtonDelegate
    {
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

        public void updateRotation()
        {
            float num = electro ? width - 130 : texture.quadRects[quadToDraw].w;
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

        public void turnElectroOn()
        {
            electroOn = true;
            playTimeline(1);
            electroTimer = onTime;
            sndElectric = CTRSoundMgr._playSoundLooped(38);
        }

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

        public void setToggled(int t)
        {
            toggled = t;
        }

        public int getToggled()
        {
            return toggled;
        }

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

        public static void timelineReachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public void timelineFinished(Timeline t)
        {
            updateRotationFlag = false;
        }

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

        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        private const float SPIKES_HEIGHT = 10f;

        private int toggled;

        public double angle;

        public Vector t1;

        public Vector t2;

        public Vector b1;

        public Vector b2;

        public bool electro;

        public float initialDelay;

        public float onTime;

        public float offTime;

        public bool electroOn;

        public float electroTimer;

        private bool updateRotationFlag;

        private bool spikesNormal;

        private float origRotation;

        public Button rotateButton;

        public int touchIndex;

        public rotateAllSpikesWithID delegateRotateAllSpikesWithID;

        private SoundEffectInstance sndElectric;

        private enum SPIKES_ANIM
        {
            SPIKES_ANIM_ELECTRODES_BASE,
            SPIKES_ANIM_ELECTRODES_ELECTRIC,
            SPIKES_ANIM_ROTATION_ADJUSTED
        }

        private enum SPIKES_ROTATION
        {
            SPIKES_ROTATION_BUTTON
        }

        // (Invoke) Token: 0x06000285 RID: 645
        public delegate void rotateAllSpikesWithID(int sid);
    }
}
