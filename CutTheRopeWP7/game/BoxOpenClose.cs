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
                result = (BaseElement)new BaseElement().init();
                _ = addChildwithID(result, 1);
                anchor = parentAnchor = 18;
                result.anchor = result.parentAnchor = 18;
                result.setEnabled(false);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                result.addTimelinewithID(timeline, 0);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                result.addTimelinewithID(timeline, 1);
                Image image = Image.Image_createWithResIDQuad(97, 14);
                image.anchor = 18;
                image.setName("star1");
                Image.setElementPositionWithQuadOffset(image, 97, 0);
                _ = result.addChild(image);
                Image image2 = Image.Image_createWithResIDQuad(97, 14);
                image2.anchor = 18;
                image2.setName("star2");
                Image.setElementPositionWithQuadOffset(image2, 97, 1);
                _ = result.addChild(image2);
                Image image3 = Image.Image_createWithResIDQuad(97, 14);
                image3.anchor = 18;
                image3.setName("star3");
                Image.setElementPositionWithQuadOffset(image3, 97, 2);
                _ = result.addChild(image3);
                Text text = new Text().initWithFont(Application.getFont(5));
                text.setString(Application.getString(1310737));
                Image.setElementPositionWithQuadOffset(text, 97, 3);
                text.anchor = 18;
                text.setName("passText");
                _ = result.addChild(text);
                Image image4 = Image.Image_createWithResIDQuad(97, 15);
                image4.anchor = 18;
                Image.setElementPositionWithQuadOffset(image4, 97, 4);
                _ = result.addChild(image4);
                stamp = Image.Image_createWithResIDQuad(99, 0);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(7);
                timeline2.addKeyFrame(KeyFrame.makeScale(3.0, 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                _ = stamp.addTimeline(timeline2);
                stamp.anchor = 18;
                stamp.setEnabled(false);
                Image.setElementPositionWithQuadOffset(stamp, 97, 12);
                _ = result.addChild(stamp);
                Button button = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310749), 8, b);
                button.anchor = 18;
                button.setName("buttonWinRestart");
                if (LANGUAGE == Language.LANG_KO)
                {
                    button.getChild(0).getChild(0).scaleX = 0.9f;
                    button.getChild(0).getChild(0).scaleY = 0.9f;
                    button.getChild(1).getChild(0).scaleX = 0.9f;
                    button.getChild(1).getChild(0).scaleY = 0.9f;
                }
                Image.setElementPositionWithQuadOffset(button, 97, 11);
                _ = result.addChild(button);
                Button button2 = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310750), 9, b);
                button2.anchor = 18;
                button2.setName("buttonWinNextLevel");
                Image.setElementPositionWithQuadOffset(button2, 97, 10);
                _ = result.addChild(button2);
                Button button3 = MenuController.createShortButtonWithTextIDDelegate(Application.getString(1310751), 5, b);
                button3.anchor = 18;
                button3.setName("buttonWinExit");
                Image.setElementPositionWithQuadOffset(button3, 97, 9);
                _ = result.addChild(button3);
                Text text2 = new Text().initWithFont(Application.getFont(6));
                text2.setName("dataTitle");
                text2.anchor = 18;
                Image.setElementPositionWithQuadOffset(text2, 97, 5);
                _ = result.addChild(text2);
                Text text3 = new Text().initWithFont(Application.getFont(6));
                text3.setName("dataValue");
                text3.anchor = 18;
                Image.setElementPositionWithQuadOffset(text3, 97, 6);
                if (LANGUAGE == Language.LANG_ES)
                {
                    text3.x += 20f;
                }
                _ = result.addChild(text3);
                Text text4 = new Text().initWithFont(Application.getFont(98));
                text4.setName("scoreValue");
                text4.anchor = 18;
                Image.setElementPositionWithQuadOffset(text4, 97, 8);
                _ = result.addChild(text4);
                confettiAnims = (BaseElement)new BaseElement().init();
                _ = result.addChild(confettiAnims);
                openCloseAnims = null;
                boxAnim = -1;
            }
            return this;
        }

        // Token: 0x0600046E RID: 1134 RVA: 0x0001F452 File Offset: 0x0001D652
        public virtual void levelFirstStart()
        {
            boxAnim = 0;
            removeOpenCloseAnims();
            showOpenAnim();
            if (result.isEnabled())
            {
                result.playTimeline(1);
            }
        }

        // Token: 0x0600046F RID: 1135 RVA: 0x0001F480 File Offset: 0x0001D680
        public virtual void levelStart()
        {
            boxAnim = 1;
            removeOpenCloseAnims();
            showOpenAnim();
            if (result.isEnabled())
            {
                result.playTimeline(1);
            }
        }

        // Token: 0x06000470 RID: 1136 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
        public virtual void levelWon()
        {
            boxAnim = 2;
            raState = -1;
            removeOpenCloseAnims();
            showCloseAnim();
            Text text = (Text)result.getChildWithName("scoreValue");
            text.setEnabled(false);
            Text text2 = (Text)result.getChildWithName("dataTitle");
            text2.setEnabled(false);
            Image.setElementPositionWithQuadOffset(text2, 97, 5);
            Text text3 = (Text)result.getChildWithName("dataValue");
            text3.setEnabled(false);
            result.playTimeline(0);
            result.setEnabled(true);
            stamp.setEnabled(false);
        }

        // Token: 0x06000471 RID: 1137 RVA: 0x0001F55B File Offset: 0x0001D75B
        public virtual void levelLost()
        {
            boxAnim = 3;
            removeOpenCloseAnims();
            showCloseAnim();
        }

        // Token: 0x06000472 RID: 1138 RVA: 0x0001F570 File Offset: 0x0001D770
        public virtual void levelQuit()
        {
            boxAnim = 4;
            result.setEnabled(false);
            removeOpenCloseAnims();
            showCloseAnim();
        }

        // Token: 0x06000473 RID: 1139 RVA: 0x0001F591 File Offset: 0x0001D791
        public virtual void postBoxClosed()
        {
            if (delegateboxClosed != null)
            {
                delegateboxClosed();
            }
            if (shouldShowConfetti)
            {
                showConfetti();
            }
        }

        // Token: 0x06000474 RID: 1140 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
        public virtual void showOpenAnim()
        {
            showOpenCloseAnim(true);
        }

        // Token: 0x06000475 RID: 1141 RVA: 0x0001F5BD File Offset: 0x0001D7BD
        public virtual void showCloseAnim()
        {
            showOpenCloseAnim(false);
        }

        // Token: 0x06000476 RID: 1142 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
        public virtual BaseElement createConfettiParticleNear(Vector p)
        {
            Confetti confetti = Confetti.Confetti_createWithResID(95);
            confetti.doRestoreCutTransparency();
            int num = RND_RANGE(0, 2);
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
            float num4 = (float)RND_RANGE(-100, (int)SCREEN_WIDTH);
            float num5 = (float)RND_RANGE(-20, 50);
            float num6 = FLOAT_RND_RANGE(2, 5);
            int num7 = confetti.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, num2, num3);
            confetti.ani = confetti.getTimeline(num7);
            confetti.ani.playTimeline();
            confetti.ani.jumpToTrackKeyFrame(4, RND_RANGE(0, num3 - num2 - 1));
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num6));
            timeline.addKeyFrame(KeyFrame.makePos((double)num4, (double)num5, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos((double)num4, (double)(num5 + FLOAT_RND_RANGE(150, 250)), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num6));
            timeline.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
            timeline.addKeyFrame(KeyFrame.makeRotation(RND_RANGE(-360, 360), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation(RND_RANGE(-360, 360), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, num6));
            _ = confetti.addTimeline(timeline);
            confetti.playTimeline(1);
            return confetti;
        }

        // Token: 0x06000477 RID: 1143 RVA: 0x0001F788 File Offset: 0x0001D988
        public virtual void removeOpenCloseAnims()
        {
            if (getChild(0) != null)
            {
                removeChild(openCloseAnims);
                openCloseAnims = null;
            }
            Text text = (Text)result.getChildWithName("dataTitle");
            Text text2 = (Text)result.getChildWithName("dataValue");
            Text text3 = (Text)result.getChildWithName("scoreValue");
            text.color.a = text2.color.a = text3.color.a = 1f;
        }

        // Token: 0x06000478 RID: 1144 RVA: 0x0001F824 File Offset: 0x0001DA24
        public virtual void createOpenCloseAnims()
        {
            openCloseAnims = (BaseElement)new BaseElement().init();
            _ = addChildwithID(openCloseAnims, 0);
            openCloseAnims.parentAnchor = openCloseAnims.anchor = 18;
            openCloseAnims.scaleY = SCREEN_BG_SCALE_Y;
            openCloseAnims.scaleX = SCREEN_BG_SCALE_X;
            openCloseAnims.passTransformationsToChilds = true;
        }

        // Token: 0x06000479 RID: 1145 RVA: 0x0001F89C File Offset: 0x0001DA9C
        public virtual void showConfetti()
        {
            for (int i = 0; i < 70; i++)
            {
                _ = confettiAnims.addChild(createConfettiParticleNear(vectZero));
            }
        }

        // Token: 0x0600047A RID: 1146 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
        public override void update(float delta)
        {
            base.update(delta);
            if (boxAnim == 2)
            {
                bool flag = Mover.moveVariableToTarget(ref raDelay, 0f, 1f, delta);
                switch (raState)
                {
                    case -1:
                        {
                            cscore = 0;
                            ctime = time;
                            cstarBonus = starBonus;
                            Text text = (Text)result.getChildWithName("scoreValue");
                            text.setString(NSS(string.Concat(cscore)));
                            Text text2 = (Text)result.getChildWithName("dataTitle");
                            Image.setElementPositionWithQuadOffset(text2, 97, 5);
                            text2.setString(Application.getString(1310743));
                            Text text3 = (Text)result.getChildWithName("dataValue");
                            text3.setString(NSS(string.Concat(cstarBonus)));
                            raState = 1;
                            raDelay = 1f;
                            return;
                        }
                    case 0:
                        if (flag)
                        {
                            raState = 1;
                            raDelay = 0.2f;
                            return;
                        }
                        break;
                    case 1:
                        {
                            Text text4 = (Text)result.getChildWithName("dataTitle");
                            text4.setEnabled(true);
                            Text text5 = (Text)result.getChildWithName("dataValue");
                            text5.setEnabled(true);
                            Text text6 = (Text)result.getChildWithName("scoreValue");
                            text4.color.a = text5.color.a = text6.color.a = 1f - raDelay / 0.2f;
                            if (flag)
                            {
                                raState = 2;
                                raDelay = 1f;
                                return;
                            }
                            break;
                        }
                    case 2:
                        {
                            cstarBonus = (int)((float)starBonus * raDelay);
                            cscore = (int)((1.0 - (double)raDelay) * (double)starBonus);
                            Text text7 = (Text)result.getChildWithName("dataValue");
                            text7.setString(NSS(string.Concat(cstarBonus)));
                            Text text8 = (Text)result.getChildWithName("scoreValue");
                            text8.setEnabled(true);
                            text8.setString(NSS(string.Concat(cscore)));
                            if (flag)
                            {
                                raState = 3;
                                raDelay = 0.2f;
                                return;
                            }
                            break;
                        }
                    case 3:
                        {
                            Text text9 = (Text)result.getChildWithName("dataTitle");
                            Text text10 = (Text)result.getChildWithName("dataValue");
                            text9.color.a = text10.color.a = raDelay / 0.2f;
                            if (flag)
                            {
                                raState = 4;
                                raDelay = 0.2f;
                                int num = (int)Math.Floor(Math.Round((double)time) / 60.0);
                                int num2 = (int)(Math.Round((double)time) - (double)num * 60.0);
                                Text text11 = (Text)result.getChildWithName("dataTitle");
                                text11.setString(Application.getString(1310742));
                                Text text12 = (Text)result.getChildWithName("dataValue");
                                text12.setString(NSS(num + ":" + num2.ToString("D2")));
                                return;
                            }
                            break;
                        }
                    case 4:
                        {
                            Text text13 = (Text)result.getChildWithName("dataTitle");
                            Text text14 = (Text)result.getChildWithName("dataValue");
                            text13.color.a = text14.color.a = 1f - raDelay / 0.2f;
                            if (flag)
                            {
                                raState = 5;
                                raDelay = 1f;
                                return;
                            }
                            break;
                        }
                    case 5:
                        {
                            ctime = time * raDelay;
                            cscore = (int)((double)starBonus + (1.0 - (double)raDelay) * (double)timeBonus);
                            int num3 = (int)Math.Floor(Math.Round((double)ctime) / 60.0);
                            int num4 = (int)(Math.Round((double)ctime) - (double)num3 * 60.0);
                            Text text15 = (Text)result.getChildWithName("dataValue");
                            text15.setString(NSS(num3 + ":" + num4.ToString("D2")));
                            Text text16 = (Text)result.getChildWithName("scoreValue");
                            text16.setString(NSS(string.Concat(cscore)));
                            if (flag)
                            {
                                raState = 6;
                                raDelay = 0.2f;
                                return;
                            }
                            break;
                        }
                    case 6:
                        {
                            Text text17 = (Text)result.getChildWithName("dataTitle");
                            Text text18 = (Text)result.getChildWithName("dataValue");
                            text17.color.a = text18.color.a = raDelay / 0.2f;
                            if (flag)
                            {
                                raState = 7;
                                raDelay = 0.2f;
                                Text text19 = (Text)result.getChildWithName("dataTitle");
                                Image.setElementPositionWithQuadOffset(text19, 97, 7);
                                text19.setString(Application.getString(1310744));
                                Text text20 = (Text)result.getChildWithName("dataValue");
                                text20.setString(NSS(""));
                                return;
                            }
                            break;
                        }
                    case 7:
                        {
                            Text text21 = (Text)result.getChildWithName("dataTitle");
                            Text text22 = (Text)result.getChildWithName("dataValue");
                            text21.color.a = text22.color.a = 1f - raDelay / 0.2f;
                            if (flag)
                            {
                                raState = 8;
                                if (shouldShowImprovedResult)
                                {
                                    stamp.setEnabled(true);
                                    stamp.playTimeline(0);
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
            createOpenCloseAnims();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int num = 216 + ctrrootController.getPack();
            Image image = Image.Image_createWithResIDQuad(97, 16);
            image.rotationCenterX = (float)-(float)image.width / 2f + 1f;
            image.rotationCenterY = (float)-(float)image.height / 2f + 1f;
            image.scaleX = image.scaleY = 4f;
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
            _ = openCloseAnims.addChild(image);
            Image image2 = Image.Image_createWithResIDQuad(num, 0);
            Image image3 = Image.Image_createWithResIDQuad(num, 0);
            image2.rotationCenterX = (float)-(float)image2.width / 2f;
            image3.rotationCenterX = image2.rotationCenterX;
            image3.y = -0.5f;
            image3.rotation = 180f;
            image3.x = SCREEN_WIDTH;
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
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH - (float)image2.width + 3f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH + 6f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.56));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)SCREEN_WIDTH - 9.0, 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH - (float)image2.width + 4f), 25.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.48));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image5.addTimelinewithID(timeline, 0);
            image5.playTimeline(0);
            Image image6 = Image.Image_createWithResIDQuad(num, 1);
            Image image7 = Image.Image_createWithResIDQuad(num, 1);
            image6.rotationCenterX = (float)-(float)image6.width / 2f;
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
            _ = openCloseAnims.addChild(image6);
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            if (open)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH - (float)image2.width) + 3.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)SCREEN_WIDTH, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.56));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH - num4), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(SCREEN_WIDTH - (float)image2.width + num6), 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.48));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 1.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.53));
            }
            image7.addTimelinewithID(timeline, 0);
            image7.playTimeline(0);
            _ = openCloseAnims.addChild(image7);
            _ = openCloseAnims.addChild(image2);
            _ = openCloseAnims.addChild(image3);
            if (boxAnim == 0)
            {
                _ = openCloseAnims.addChild(image4);
                _ = openCloseAnims.addChild(image5);
            }
        }

        // Token: 0x0600047C RID: 1148 RVA: 0x00020AC6 File Offset: 0x0001ECC6
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x0600047D RID: 1149 RVA: 0x00020AC8 File Offset: 0x0001ECC8
        public virtual void timelineFinished(Timeline t)
        {
            switch (boxAnim)
            {
                case 0:
                case 1:
                    NSTimer.registerDelayedObjectCall(new DelayedDispatcher.DispatchFunc(selector_removeOpenCloseAnims), this, 0.001);
                    if (result.isEnabled())
                    {
                        confettiAnims.removeAllChilds();
                        result.setEnabled(false);
                        return;
                    }
                    break;
                case 2:
                    NSTimer.registerDelayedObjectCall(new DelayedDispatcher.DispatchFunc(selector_postBoxClosed), this, 0.001);
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
        public boxClosed delegateboxClosed;

        // Token: 0x02000096 RID: 150
        // (Invoke) Token: 0x06000482 RID: 1154
        public delegate void boxClosed();
    }
}
