using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000095 RID: 149
    internal class BoxOpenClose : BaseElement, TimelineDelegate
    {
        // Token: 0x0600046D RID: 1133 RVA: 0x0001EF08 File Offset: 0x0001D108
        public virtual NSObject initWithButtonDelegate(ButtonDelegate b)
        {
            if (base.init() != null)
            {
                this.result = (BaseElement)new BaseElement().init();
                this.addChildwithID(this.result, 1);
                this.anchor = (this.parentAnchor = 18);
                this.result.anchor = (this.result.parentAnchor = 18);
                this.result.setEnabled(false);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                this.result.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                this.result.addTimelinewithID(timeline, 1);
                Image image = Image.Image_createWithResIDQuad(97, 14);
                image.anchor = 18;
                image.setName("star1");
                Image.setElementPositionWithQuadOffset(image, 97, 0);
                this.result.addChild(image);
                Image image2 = Image.Image_createWithResIDQuad(97, 14);
                image2.anchor = 18;
                image2.setName("star2");
                Image.setElementPositionWithQuadOffset(image2, 97, 1);
                this.result.addChild(image2);
                Image image3 = Image.Image_createWithResIDQuad(97, 14);
                image3.anchor = 18;
                image3.setName("star3");
                Image.setElementPositionWithQuadOffset(image3, 97, 2);
                this.result.addChild(image3);
                Text text = new Text().initWithFont(Application.getFont(5));
                text.setString(Application.getString(1310737));
                Image.setElementPositionWithQuadOffset(text, 97, 3);
                text.anchor = 18;
                text.setName("passText");
                this.result.addChild(text);
                Image image4 = Image.Image_createWithResIDQuad(97, 15);
                image4.anchor = 18;
                Image.setElementPositionWithQuadOffset(image4, 97, 4);
                this.result.addChild(image4);
                this.stamp = Image.Image_createWithResIDQuad(99, 0);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(7);
                timeline2.addKeyFrame(KeyFrame.makeScale(3.0, 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                this.stamp.addTimeline(timeline2);
                this.stamp.anchor = 18;
                this.stamp.setEnabled(false);
                Image.setElementPositionWithQuadOffset(this.stamp, 97, 12);
                this.result.addChild(this.stamp);
                Button button = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310749), 8, b);
                button.anchor = 18;
                button.setName("buttonWinRestart");
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                {
                    button.getChild(0).getChild(0).scaleX = 0.9f;
                    button.getChild(0).getChild(0).scaleY = 0.9f;
                    button.getChild(1).getChild(0).scaleX = 0.9f;
                    button.getChild(1).getChild(0).scaleY = 0.9f;
                }
                Image.setElementPositionWithQuadOffset(button, 97, 11);
                this.result.addChild(button);
                Button button2 = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310750), 9, b);
                button2.anchor = 18;
                button2.setName("buttonWinNextLevel");
                Image.setElementPositionWithQuadOffset(button2, 97, 10);
                this.result.addChild(button2);
                Button button3 = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310751), 5, b);
                button3.anchor = 18;
                button3.setName("buttonWinExit");
                Image.setElementPositionWithQuadOffset(button3, 97, 9);
                this.result.addChild(button3);
                Text text2 = new Text().initWithFont(Application.getFont(6));
                text2.setName("dataTitle");
                text2.anchor = 18;
                Image.setElementPositionWithQuadOffset(text2, 97, 5);
                this.result.addChild(text2);
                Text text3 = new Text().initWithFont(Application.getFont(6));
                text3.setName("dataValue");
                text3.anchor = 18;
                Image.setElementPositionWithQuadOffset(text3, 97, 6);
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                {
                    text3.x += 20f;
                }
                this.result.addChild(text3);
                Text text4 = new Text().initWithFont(Application.getFont(98));
                text4.setName("scoreValue");
                text4.anchor = 18;
                Image.setElementPositionWithQuadOffset(text4, 97, 8);
                this.result.addChild(text4);
                this.confettiAnims = (BaseElement)new BaseElement().init();
                this.result.addChild(this.confettiAnims);
                this.openCloseAnims = null;
                this.boxAnim = -1;
            }
            return this;
        }

        // Token: 0x0600046E RID: 1134 RVA: 0x0001F452 File Offset: 0x0001D652
        public virtual void levelFirstStart()
        {
            this.boxAnim = 0;
            this.removeOpenCloseAnims();
            this.showOpenAnim();
            if (this.result.isEnabled())
            {
                this.result.playTimeline(1);
            }
        }

        // Token: 0x0600046F RID: 1135 RVA: 0x0001F480 File Offset: 0x0001D680
        public virtual void levelStart()
        {
            this.boxAnim = 1;
            this.removeOpenCloseAnims();
            this.showOpenAnim();
            if (this.result.isEnabled())
            {
                this.result.playTimeline(1);
            }
        }

        // Token: 0x06000470 RID: 1136 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
        public virtual void levelWon()
        {
            this.boxAnim = 2;
            this.raState = -1;
            this.removeOpenCloseAnims();
            this.showCloseAnim();
            Text text = (Text)this.result.getChildWithName("scoreValue");
            text.setEnabled(false);
            Text text2 = (Text)this.result.getChildWithName("dataTitle");
            text2.setEnabled(false);
            Image.setElementPositionWithQuadOffset(text2, 97, 5);
            Text text3 = (Text)this.result.getChildWithName("dataValue");
            text3.setEnabled(false);
            this.result.playTimeline(0);
            this.result.setEnabled(true);
            this.stamp.setEnabled(false);
        }

        // Token: 0x06000471 RID: 1137 RVA: 0x0001F55B File Offset: 0x0001D75B
        public virtual void levelLost()
        {
            this.boxAnim = 3;
            this.removeOpenCloseAnims();
            this.showCloseAnim();
        }

        // Token: 0x06000472 RID: 1138 RVA: 0x0001F570 File Offset: 0x0001D770
        public virtual void levelQuit()
        {
            this.boxAnim = 4;
            this.result.setEnabled(false);
            this.removeOpenCloseAnims();
            this.showCloseAnim();
        }

        // Token: 0x06000473 RID: 1139 RVA: 0x0001F591 File Offset: 0x0001D791
        public virtual void postBoxClosed()
        {
            if (this.delegateboxClosed != null)
            {
                this.delegateboxClosed();
            }
            if (this.shouldShowConfetti)
            {
                this.showConfetti();
            }
        }

        // Token: 0x06000474 RID: 1140 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
        public virtual void showOpenAnim()
        {
            this.showOpenCloseAnim(true);
        }

        // Token: 0x06000475 RID: 1141 RVA: 0x0001F5BD File Offset: 0x0001D7BD
        public virtual void showCloseAnim()
        {
            this.showOpenCloseAnim(false);
        }

        // Token: 0x06000476 RID: 1142 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
        public virtual BaseElement createConfettiParticleNear(Vector p)
        {
            Confetti confetti = Confetti.Confetti_createWithResID(95);
            confetti.doRestoreCutTransparency();
            int num = MathHelper.RND_RANGE(0, 2);
            int num2 = 0;
            int num3 = 8;
            if (num == 1)
            {
                num2 = 9;
                num3 = 17;
            }
            else if (num == 2)
            {
                num2 = 18;
                num3 = 26;
            }
            float num4 = (float)MathHelper.RND_RANGE(-100, (int)FrameworkTypes.SCREEN_WIDTH);
            float num5 = (float)MathHelper.RND_RANGE(-20, 50);
            float num6 = MathHelper.FLOAT_RND_RANGE(2, 5);
            int num7 = confetti.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, num2, num3);
            confetti.ani = confetti.getTimeline(num7);
            confetti.ani.playTimeline();
            confetti.ani.jumpToTrackKeyFrame(4, MathHelper.RND_RANGE(0, num3 - num2 - 1));
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num6));
            timeline.addKeyFrame(KeyFrame.makePos((double)num4, (double)num5, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos((double)num4, (double)(num5 + MathHelper.FLOAT_RND_RANGE(150, 250)), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num6));
            timeline.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            timeline.addKeyFrame(KeyFrame.makeRotation(MathHelper.RND_RANGE(-360, 360), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation(MathHelper.RND_RANGE(-360, 360), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num6));
            confetti.addTimeline(timeline);
            confetti.playTimeline(1);
            return confetti;
        }

        // Token: 0x06000477 RID: 1143 RVA: 0x0001F788 File Offset: 0x0001D988
        public virtual void removeOpenCloseAnims()
        {
            if (this.getChild(0) != null)
            {
                this.removeChild(this.openCloseAnims);
                this.openCloseAnims = null;
            }
            Text text = (Text)this.result.getChildWithName("dataTitle");
            Text text2 = (Text)this.result.getChildWithName("dataValue");
            Text text3 = (Text)this.result.getChildWithName("scoreValue");
            text.color.a = (text2.color.a = (text3.color.a = 1f));
        }

        // Token: 0x06000478 RID: 1144 RVA: 0x0001F824 File Offset: 0x0001DA24
        public virtual void createOpenCloseAnims()
        {
            this.openCloseAnims = (BaseElement)new BaseElement().init();
            this.addChildwithID(this.openCloseAnims, 0);
            this.openCloseAnims.parentAnchor = (this.openCloseAnims.anchor = 18);
            this.openCloseAnims.scaleY = FrameworkTypes.SCREEN_BG_SCALE_Y;
            this.openCloseAnims.scaleX = FrameworkTypes.SCREEN_BG_SCALE_X;
            this.openCloseAnims.passTransformationsToChilds = true;
        }

        // Token: 0x06000479 RID: 1145 RVA: 0x0001F89C File Offset: 0x0001DA9C
        public virtual void showConfetti()
        {
            for (int i = 0; i < 70; i++)
            {
                this.confettiAnims.addChild(this.createConfettiParticleNear(MathHelper.vectZero));
            }
        }

        // Token: 0x0600047A RID: 1146 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
        public override void update(float delta)
        {
            base.update(delta);
            if (this.boxAnim == 2)
            {
                bool flag = Mover.moveVariableToTarget(ref this.raDelay, 0f, 1f, delta);
                switch (this.raState)
                {
                    case -1:
                        {
                            this.cscore = 0;
                            this.ctime = this.time;
                            this.cstarBonus = this.starBonus;
                            Text text = (Text)this.result.getChildWithName("scoreValue");
                            text.setString(NSObject.NSS(string.Concat(this.cscore)));
                            Text text2 = (Text)this.result.getChildWithName("dataTitle");
                            Image.setElementPositionWithQuadOffset(text2, 97, 5);
                            text2.setString(Application.getString(1310743));
                            Text text3 = (Text)this.result.getChildWithName("dataValue");
                            text3.setString(NSObject.NSS(string.Concat(this.cstarBonus)));
                            this.raState = 1;
                            this.raDelay = 1f;
                            return;
                        }
                    case 0:
                        if (flag)
                        {
                            this.raState = 1;
                            this.raDelay = 0.2f;
                            return;
                        }
                        break;
                    case 1:
                        {
                            Text text4 = (Text)this.result.getChildWithName("dataTitle");
                            text4.setEnabled(true);
                            Text text5 = (Text)this.result.getChildWithName("dataValue");
                            text5.setEnabled(true);
                            Text text6 = (Text)this.result.getChildWithName("scoreValue");
                            text4.color.a = (text5.color.a = (text6.color.a = 1f - this.raDelay / 0.2f));
                            if (flag)
                            {
                                this.raState = 2;
                                this.raDelay = 1f;
                                return;
                            }
                            break;
                        }
                    case 2:
                        {
                            this.cstarBonus = (int)((float)this.starBonus * this.raDelay);
                            this.cscore = (int)((1.0 - (double)this.raDelay) * (double)this.starBonus);
                            Text text7 = (Text)this.result.getChildWithName("dataValue");
                            text7.setString(NSObject.NSS(string.Concat(this.cstarBonus)));
                            Text text8 = (Text)this.result.getChildWithName("scoreValue");
                            text8.setEnabled(true);
                            text8.setString(NSObject.NSS(string.Concat(this.cscore)));
                            if (flag)
                            {
                                this.raState = 3;
                                this.raDelay = 0.2f;
                                return;
                            }
                            break;
                        }
                    case 3:
                        {
                            Text text9 = (Text)this.result.getChildWithName("dataTitle");
                            Text text10 = (Text)this.result.getChildWithName("dataValue");
                            text9.color.a = (text10.color.a = this.raDelay / 0.2f);
                            if (flag)
                            {
                                this.raState = 4;
                                this.raDelay = 0.2f;
                                int num = (int)Math.Floor(Math.Round((double)this.time) / 60.0);
                                int num2 = (int)(Math.Round((double)this.time) - (double)num * 60.0);
                                Text text11 = (Text)this.result.getChildWithName("dataTitle");
                                text11.setString(Application.getString(1310742));
                                Text text12 = (Text)this.result.getChildWithName("dataValue");
                                text12.setString(NSObject.NSS(num + ":" + num2.ToString("D2")));
                                return;
                            }
                            break;
                        }
                    case 4:
                        {
                            Text text13 = (Text)this.result.getChildWithName("dataTitle");
                            Text text14 = (Text)this.result.getChildWithName("dataValue");
                            text13.color.a = (text14.color.a = 1f - this.raDelay / 0.2f);
                            if (flag)
                            {
                                this.raState = 5;
                                this.raDelay = 1f;
                                return;
                            }
                            break;
                        }
                    case 5:
                        {
                            this.ctime = this.time * this.raDelay;
                            this.cscore = (int)((double)this.starBonus + (1.0 - (double)this.raDelay) * (double)this.timeBonus);
                            int num3 = (int)Math.Floor(Math.Round((double)this.ctime) / 60.0);
                            int num4 = (int)(Math.Round((double)this.ctime) - (double)num3 * 60.0);
                            Text text15 = (Text)this.result.getChildWithName("dataValue");
                            text15.setString(NSObject.NSS(num3 + ":" + num4.ToString("D2")));
                            Text text16 = (Text)this.result.getChildWithName("scoreValue");
                            text16.setString(NSObject.NSS(string.Concat(this.cscore)));
                            if (flag)
                            {
                                this.raState = 6;
                                this.raDelay = 0.2f;
                                return;
                            }
                            break;
                        }
                    case 6:
                        {
                            Text text17 = (Text)this.result.getChildWithName("dataTitle");
                            Text text18 = (Text)this.result.getChildWithName("dataValue");
                            text17.color.a = (text18.color.a = this.raDelay / 0.2f);
                            if (flag)
                            {
                                this.raState = 7;
                                this.raDelay = 0.2f;
                                Text text19 = (Text)this.result.getChildWithName("dataTitle");
                                Image.setElementPositionWithQuadOffset(text19, 97, 7);
                                text19.setString(Application.getString(1310744));
                                Text text20 = (Text)this.result.getChildWithName("dataValue");
                                text20.setString(NSObject.NSS(""));
                                return;
                            }
                            break;
                        }
                    case 7:
                        {
                            Text text21 = (Text)this.result.getChildWithName("dataTitle");
                            Text text22 = (Text)this.result.getChildWithName("dataValue");
                            text21.color.a = (text22.color.a = 1f - this.raDelay / 0.2f);
                            if (flag)
                            {
                                this.raState = 8;
                                if (this.shouldShowImprovedResult)
                                {
                                    this.stamp.setEnabled(true);
                                    this.stamp.playTimeline(0);
                                }
                            }
                            break;
                        }
                    default:
                        return;
                }
            }
        }

        // Token: 0x0600047B RID: 1147 RVA: 0x0001FF60 File Offset: 0x0001E160
        public virtual void showOpenCloseAnim(bool open)
        {
            this.createOpenCloseAnims();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int num = 216 + ctrrootController.getPack();
            Image image = Image.Image_createWithResIDQuad(97, 16);
            image.rotationCenterX = (float)(-(float)image.width) / 2f + 1f;
            image.rotationCenterY = (float)(-(float)image.height) / 2f + 1f;
            image.scaleX = (image.scaleY = 4f);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(-(double)image.width * 4), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(-(double)image.width * 4), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            image.addTimelinewithID(timeline, 0);
            image.playTimeline(0);
            this.openCloseAnims.addChild(image);
            Image image2 = Image.Image_createWithResIDQuad(num, 0);
            Image image3 = Image.Image_createWithResIDQuad(num, 0);
            image2.rotationCenterX = (float)(-(float)image2.width) / 2f;
            image3.rotationCenterX = image2.rotationCenterX;
            image3.y = -0.5f;
            image3.rotation = 180f;
            image3.x = FrameworkTypes.SCREEN_WIDTH;
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.85, 0.85, 0.85, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.whiteRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.85, 0.85, 0.85, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            image2.addTimelinewithID(timeline, 0);
            image2.playTimeline(0);
            timeline.delegateTimelineDelegate = this;
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.85, 0.85, 0.85, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.4, 0.4, 0.4, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.4, 0.4, 0.4, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.85, 0.85, 0.85, 1.0), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            }
            image3.addTimelinewithID(timeline, 0);
            image3.playTimeline(0);
            Image image4 = Image.Image_createWithResIDQuad(7, 0);
            Image image5 = Image.Image_createWithResIDQuad(7, 1);
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)((float)image2.width - 25f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos(-10.0, 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos(-15.0, 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)((float)image2.width - 25f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.49));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image4.addTimelinewithID(timeline, 0);
            image4.playTimeline(0);
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH - (float)image2.width + 3f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH + 6f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.56));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)FrameworkTypes.SCREEN_WIDTH - 9.0, 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH - (float)image2.width + 4f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.48));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image5.addTimelinewithID(timeline, 0);
            image5.playTimeline(0);
            Image image6 = Image.Image_createWithResIDQuad(num, 1);
            Image image7 = Image.Image_createWithResIDQuad(num, 1);
            image6.rotationCenterX = (float)(-(float)image6.width) / 2f;
            image7.rotationCenterX = image6.rotationCenterX;
            float num2 = -10f;
            float num3 = -5f;
            float num4 = 13f;
            float num5 = 3f;
            float num6 = 5f;
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)image2.width - 3.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)num2, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)num3, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)((float)image2.width - num5), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.49));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image6.addTimelinewithID(timeline, 0);
            image6.playTimeline(0);
            this.openCloseAnims.addChild(image6);
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH - (float)image2.width) + 3.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)FrameworkTypes.SCREEN_WIDTH, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.56));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH - num4), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(FrameworkTypes.SCREEN_WIDTH - (float)image2.width + num6), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.48));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image7.addTimelinewithID(timeline, 0);
            image7.playTimeline(0);
            this.openCloseAnims.addChild(image7);
            this.openCloseAnims.addChild(image2);
            this.openCloseAnims.addChild(image3);
            if (this.boxAnim == 0)
            {
                this.openCloseAnims.addChild(image4);
                this.openCloseAnims.addChild(image5);
            }
        }

        // Token: 0x0600047C RID: 1148 RVA: 0x00020AC6 File Offset: 0x0001ECC6
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x0600047D RID: 1149 RVA: 0x00020AC8 File Offset: 0x0001ECC8
        public virtual void timelineFinished(Timeline t)
        {
            switch (this.boxAnim)
            {
                case 0:
                case 1:
                    NSTimer.registerDelayedObjectCall(new DelayedDispatcher.DispatchFunc(BoxOpenClose.selector_removeOpenCloseAnims), this, 0.001);
                    if (this.result.isEnabled())
                    {
                        this.confettiAnims.removeAllChilds();
                        this.result.setEnabled(false);
                        return;
                    }
                    break;
                case 2:
                    NSTimer.registerDelayedObjectCall(new DelayedDispatcher.DispatchFunc(BoxOpenClose.selector_postBoxClosed), this, 0.001);
                    break;
                case 3:
                    break;
                case 4:
                    {
                        ViewController currentController = Application.sharedRootController().getCurrentController();
                        currentController.deactivate();
                        return;
                    }
                default:
                    return;
            }
        }

        // Token: 0x0600047E RID: 1150 RVA: 0x00020B64 File Offset: 0x0001ED64
        private static void selector_removeOpenCloseAnims(NSObject obj)
        {
            ((BoxOpenClose)obj).removeOpenCloseAnims();
        }

        // Token: 0x0600047F RID: 1151 RVA: 0x00020B71 File Offset: 0x0001ED71
        private static void selector_postBoxClosed(NSObject obj)
        {
            ((BoxOpenClose)obj).postBoxClosed();
        }

        // Token: 0x040009AD RID: 2477
        public const int BOX_ANIM_LEVEL_FIRST_START = 0;

        // Token: 0x040009AE RID: 2478
        public const int BOX_ANIM_LEVEL_START = 1;

        // Token: 0x040009AF RID: 2479
        public const int BOX_ANIM_LEVEL_WON = 2;

        // Token: 0x040009B0 RID: 2480
        public const int BOX_ANIM_LEVEL_LOST = 3;

        // Token: 0x040009B1 RID: 2481
        public const int BOX_ANIM_LEVEL_QUIT = 4;

        // Token: 0x040009B2 RID: 2482
        public const int RESULT_STATE_WAIT = 0;

        // Token: 0x040009B3 RID: 2483
        public const int RESULT_STATE_SHOW_STAR_BONUS = 1;

        // Token: 0x040009B4 RID: 2484
        public const int RESULT_STATE_COUNTDOWN_STAR_BONUS = 2;

        // Token: 0x040009B5 RID: 2485
        public const int RESULT_STATE_HIDE_STAR_BONUS = 3;

        // Token: 0x040009B6 RID: 2486
        public const int RESULT_STATE_SHOW_TIME_BONUS = 4;

        // Token: 0x040009B7 RID: 2487
        public const int RESULT_STATE_COUNTDOWN_TIME_BONUS = 5;

        // Token: 0x040009B8 RID: 2488
        public const int RESULT_STATE_HIDE_TIME_BONUS = 6;

        // Token: 0x040009B9 RID: 2489
        public const int RESULT_STATE_SHOW_FINAL_SCORE = 7;

        // Token: 0x040009BA RID: 2490
        public const int RESULTS_SHOW_ANIM = 0;

        // Token: 0x040009BB RID: 2491
        public const int RESULTS_HIDE_ANIM = 1;

        // Token: 0x040009BC RID: 2492
        public BaseElement openCloseAnims;

        // Token: 0x040009BD RID: 2493
        public BaseElement confettiAnims;

        // Token: 0x040009BE RID: 2494
        public BaseElement result;

        // Token: 0x040009BF RID: 2495
        public int boxAnim;

        // Token: 0x040009C0 RID: 2496
        public bool shouldShowConfetti;

        // Token: 0x040009C1 RID: 2497
        public bool shouldShowImprovedResult;

        // Token: 0x040009C2 RID: 2498
        public Image stamp;

        // Token: 0x040009C3 RID: 2499
        public int raState;

        // Token: 0x040009C4 RID: 2500
        public int timeBonus;

        // Token: 0x040009C5 RID: 2501
        public int starBonus;

        // Token: 0x040009C6 RID: 2502
        public int score;

        // Token: 0x040009C7 RID: 2503
        public float time;

        // Token: 0x040009C8 RID: 2504
        public float ctime;

        // Token: 0x040009C9 RID: 2505
        public int cstarBonus;

        // Token: 0x040009CA RID: 2506
        public int cscore;

        // Token: 0x040009CB RID: 2507
        public float raDelay;

        // Token: 0x040009CC RID: 2508
        public BoxOpenClose.boxClosed delegateboxClosed;

        // Token: 0x02000096 RID: 150
        // (Invoke) Token: 0x06000482 RID: 1154
        public delegate void boxClosed();
    }
}
