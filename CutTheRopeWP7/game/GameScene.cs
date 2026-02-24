using System;
using System.Collections.Generic;
using System.Linq;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.sfe;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

namespace ctr_wp7.game
{
    // Token: 0x02000108 RID: 264
    internal class GameScene : BaseElement, ButtonDelegate, TimelineDelegate
    {
        // Token: 0x060007FF RID: 2047 RVA: 0x0003F668 File Offset: 0x0003D868
        public static float FBOUND_PI(float a)
        {
            return (float)(((double)a > 3.141592653589793) ? ((double)a - 6.283185307179586) : (((double)a < -3.141592653589793) ? ((double)a + 6.283185307179586) : ((double)a)));
        }

        // Token: 0x06000800 RID: 2048 RVA: 0x0003F6A3 File Offset: 0x0003D8A3
        public virtual void animateLevelRestart()
        {
            restartState = 0;
            dimTime = 0.15f;
        }

        // Token: 0x06000801 RID: 2049 RVA: 0x0003F6B8 File Offset: 0x0003D8B8
        public override NSObject init()
        {
            if (base.init() != null)
            {
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                dd = (DelayedDispatcher)new DelayedDispatcher().init();
                initialCameraToStarDistance = -1f;
                restartState = -1;
                aniPool = new AnimationsPool();
                aniPool.visible = false;
                addChild(aniPool);
                staticAniPool = new AnimationsPool();
                staticAniPool.visible = false;
                addChild(staticAniPool);
                camera = new Camera2D().initWithSpeedandType(7f, CAMERA_TYPE.CAMERA_SPEED_DELAY);
                pack = ctrrootController.getPack();
                int num = 188 + pack * 2;
                back = new TileMap().initWithRowsColumns(1, 1);
                back.setRepeatHorizontally(TileMap.Repeat.REPEAT_ALL);
                back.setRepeatVertically(TileMap.Repeat.REPEAT_ALL);
                back.addTileQuadwithID(Application.getTexture(num), 0, 0);
                back.fillStartAtRowColumnRowsColumnswithTile(0, 0, 1, 1, 0);
                for (int i = 0; i < 3; i++)
                {
                    hudStar[i] = Animation.Animation_createWithResID(128);
                    hudStar[i].doRestoreCutTransparency();
                    hudStar[i].addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 10);
                    hudStar[i].setPauseAtIndexforAnimation(10, 0);
                    hudStar[i].x = (float)(hudStar[i].width * i);
                    hudStar[i].y = 0f;
                    hudStar[i].x -= FrameworkTypes.SCREEN_OFFSET_X;
                    hudStar[i].y -= FrameworkTypes.SCREEN_OFFSET_Y;
                    hudStar[i].x += 0.33f;
                    hudStar[i].y += 0.33f;
                    addChild(hudStar[i]);
                }
                for (int j = 0; j < 5; j++)
                {
                    fingerCuts[j] = new List<GameScene.FingerCut>();
                    NSObject.NSRET(fingerCuts[j]);
                }
            }
            return this;
        }

        // Token: 0x06000802 RID: 2050 RVA: 0x0003F8EC File Offset: 0x0003DAEC
        public virtual void xmlLoaderFinishedWithfromwithSuccess(XMLNode rootNode, NSString url, bool success)
        {
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            ctrrootController.setMap(rootNode);
            CTRRootController.checkMapIsValid(ContentHelper.OpenResourceAsString(url.ToString()).ToCharArray());
            if (animateRestartDim)
            {
                animateLevelRestart();
                return;
            }
            restart();
        }

        // Token: 0x06000803 RID: 2051 RVA: 0x0003F938 File Offset: 0x0003DB38
        public override void show()
        {
            isCandyInLantern = false;
            isCandyInGhostBubbleAnimationLoaded = false;
            isCandyInGhostBubbleAnimationLeftLoaded = false;
            isCandyInGhostBubbleAnimationRightLoaded = false;
            CTRSoundMgr.EnableLoopedSounds(true);
            aniPool.removeAllChilds();
            staticAniPool.removeAllChilds();
            gravityButton = null;
            gravityTouchDown = -1;
            twoParts = 2;
            partsDist = 0f;
            targetSock = null;
            CTRSoundMgr._stopLoopedSounds();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            XMLNode map = ctrrootController.getMap();
            bungees = new List<Grab>();
            razors = new List<Razor>();
            spikes = new List<Spikes>();
            stars = new List<Star>();
            bubbles = new List<Bubble>();
            pumps = new List<Pump>();
            socks = new List<Sock>();
            bouncers = new List<Bouncer>();
            tutorialImages = new List<GameScene.GameObjectSpecial>();
            tutorials = new List<GameScene.TutorialText>();
            rotatedCircles = new List<RotatedCircle>();
            ghosts = new List<Ghost>();
            tubes = new List<SteamTube>();
            pollenDrawer = (PollenDrawer)new PollenDrawer().init();
            star = (ConstraintedPoint)new ConstraintedPoint().init();
            star.setWeight(1f);
            starL = (ConstraintedPoint)new ConstraintedPoint().init();
            starL.setWeight(1f);
            starR = (ConstraintedPoint)new ConstraintedPoint().init();
            starR.setWeight(1f);
            int num = Preferences._getIntForKey("PREFS_SELECTED_CANDY");
            candy = GameObject.GameObject_createWithResIDQuad(GameScene.CANDIES[num], 0);
            candy.doRestoreCutTransparency();
            NSObject.NSRET(candy);
            candy.anchor = 18;
            candy.bb = new Rectangle(46.0, 49.0, 35.0, 35.0);
            candy.passTransformationsToChilds = false;
            candy.scaleX = (candy.scaleY = 0.71f);
            candyMain = GameObject.GameObject_createWithResIDQuad(GameScene.CANDIES[num], 1);
            candyMain.doRestoreCutTransparency();
            candyMain.anchor = (candyMain.parentAnchor = 18);
            candy.addChild(candyMain);
            candyMain.scaleX = (candyMain.scaleY = 0.71f);
            candyTop = GameObject.GameObject_createWithResIDQuad(GameScene.CANDIES[num], 2);
            candyTop.doRestoreCutTransparency();
            candyTop.anchor = (candyTop.parentAnchor = 18);
            candy.addChild(candyTop);
            candyTop.scaleX = (candyTop.scaleY = 0.71f);
            candyBlink = Animation.Animation_createWithResID(103);
            candyBlink.addAnimationWithIDDelayLoopFirstLast(0, 0.07f, Timeline.LoopType.TIMELINE_NO_LOOP, 5, 14);
            Animation animation = candyBlink;
            int num2 = 1;
            float num3 = 0.3f;
            Timeline.LoopType loopType = Timeline.LoopType.TIMELINE_NO_LOOP;
            int num4 = 2;
            int num5 = 15;
            List<int> list = new List<int>();
            list.Add(15);
            animation.addAnimationWithIDDelayLoopCountSequence(num2, num3, loopType, num4, num5, list);
            Timeline timeline = candyBlink.getTimeline(1);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
            candyBlink.visible = false;
            candyBlink.anchor = (candyBlink.parentAnchor = 18);
            candyBlink.scaleX = (candyBlink.scaleY = 0.71f);
            candy.addChild(candyBlink);
            candyBubbleAnimation = Animation.Animation_createWithResID(120);
            candyBubbleAnimation.x = candy.x;
            candyBubbleAnimation.y = candy.y;
            candyBubbleAnimation.parentAnchor = (candyBubbleAnimation.anchor = 18);
            candyBubbleAnimation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
            candyBubbleAnimation.playTimeline(0);
            candy.addChild(candyBubbleAnimation);
            candyBubbleAnimation.visible = false;
            for (int i = 0; i < 3; i++)
            {
                if (hudStar[i] != null)
                {
                    if (hudStar[i].getCurrentTimeline() != null)
                    {
                        hudStar[i].getCurrentTimeline().stopTimeline();
                    }
                    hudStar[i].setDrawQuad(0);
                }
            }
            int num6 = map.childs().Count;
            for (int j = 0; j < num6; j++)
            {
                XMLNode xmlnode = map.childs()[j];
                int count = xmlnode.childs().Count;
                for (int k = 0; k < count; k++)
                {
                    XMLNode xmlnode2 = xmlnode.childs()[k];
                    if (xmlnode2.Name == "map")
                    {
                        mapWidth = xmlnode2["width"].floatValue();
                        mapHeight = xmlnode2["height"].floatValue();
                        if (ctrrootController.getPack() == 6)
                        {
                            earthAnims = new List<Image>();
                            if (mapWidth > FrameworkTypes.SCREEN_WIDTH)
                            {
                                createEarthImageWithOffsetXY(FrameworkTypes.SCREEN_WIDTH, 0f);
                            }
                            if (mapHeight > FrameworkTypes.SCREEN_HEIGHT)
                            {
                                createEarthImageWithOffsetXY(0f, FrameworkTypes.SCREEN_HEIGHT);
                            }
                            createEarthImageWithOffsetXY(0f, 0f);
                        }
                    }
                    else if (xmlnode2.Name == "gameDesign")
                    {
                        special = xmlnode2["special"].intValue();
                        ropePhysicsSpeed = xmlnode2["ropePhysicsSpeed"].floatValue();
                        nightLevel = xmlnode2["nightLevel"].isEqualToString(NSObject.NSS("true"));
                        twoParts = (xmlnode2["twoParts"].isEqualToString(NSObject.NSS("true")) ? 0 : 2);
                    }
                    else if (xmlnode2.Name == "candyL")
                    {
                        starL.pos.x = (float)xmlnode2["x"].intValue() * 1f + 0f;
                        starL.pos.y = (float)xmlnode2["y"].intValue() * 1f + 0f;
                        candyL = GameObject.GameObject_createWithResIDQuad(GameScene.CANDIES[num], 8);
                        candyL.passTransformationsToChilds = false;
                        candyL.doRestoreCutTransparency();
                        candyL.anchor = 18;
                        NSObject.NSRET(candyL);
                        candyL.scaleX = (candyL.scaleY = 0.71f);
                        candyL.x = starL.pos.x;
                        candyL.y = starL.pos.y;
                        candyL.bb = new Rectangle(52.0, 56.0, 23.0, 24.0);
                    }
                    else if (xmlnode2.Name == "candyR")
                    {
                        starR.pos.x = (float)xmlnode2["x"].intValue() * 1f + 0f;
                        starR.pos.y = (float)xmlnode2["y"].intValue() * 1f + 0f;
                        candyR = GameObject.GameObject_createWithResIDQuad(GameScene.CANDIES[num], 9);
                        candyR.passTransformationsToChilds = false;
                        candyR.doRestoreCutTransparency();
                        candyR.anchor = 18;
                        NSObject.NSRET(candyR);
                        candyR.scaleX = (candyR.scaleY = 0.71f);
                        candyR.x = starR.pos.x;
                        candyR.y = starR.pos.y;
                        candyR.bb = new Rectangle(52.0, 56.0, 23.0, 24.0);
                    }
                    else if (xmlnode2.Name == "candy")
                    {
                        star.pos.x = (float)xmlnode2["x"].intValue() * 1f + 0f;
                        star.pos.y = (float)xmlnode2["y"].intValue() * 1f + 0f;
                    }
                }
            }
            num6 = map.childs().Count;
            for (int l = 0; l < num6; l++)
            {
                XMLNode xmlnode3 = map.childs()[l];
                int count2 = xmlnode3.childs().Count;
                for (int m = 0; m < count2; m++)
                {
                    XMLNode xmlnode4 = xmlnode3.childs()[m];
                    if (xmlnode4.Name == "gravitySwitch")
                    {
                        gravityButton = GameScene.createGravityButtonWithDelegate(this);
                        gravityButton.visible = false;
                        gravityButton.touchable = false;
                        addChild(gravityButton);
                        gravityButton.x = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        gravityButton.y = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        gravityButton.anchor = 18;
                    }
                    else if (xmlnode4.Name == "star")
                    {
                        Star star = Star.Star_createWithResID(127);
                        star.x = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        star.y = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        star.timeout = xmlnode4["timeout"].floatValue();
                        star.createAnimations();
                        star.parseMover(xmlnode4);
                        star.update(0f);
                        stars.Add(star);
                    }
                    else if (xmlnode4.Name == "tutorialText")
                    {
                        if (!GameScene.shouldSkipTutorialElement(xmlnode4))
                        {
                            NSString nsstring = xmlnode4["text"];
                            if (nsstring != null)
                            {
                                GameScene.TutorialText tutorialText = (GameScene.TutorialText)new GameScene.TutorialText().initWithFont(Application.getFont(6));
                                tutorialText.color = RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 0.9);
                                tutorialText.x = (float)xmlnode4["x"].intValue() * 1f + 0f;
                                tutorialText.y = (float)xmlnode4["y"].intValue() * 1f + 0f;
                                tutorialText.special = xmlnode4["special"].intValue();
                                tutorialText.setAlignment(2);
                                tutorialText.setStringandWidth(nsstring, (float)xmlnode4["width"].intValue() * 1f);
                                tutorialText.color = RGBAColor.transparentRGBA;
                                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(4);
                                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
                                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 5.0));
                                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                                tutorialText.addTimelinewithID(timeline2, 0);
                                if (tutorialText.special == 0)
                                {
                                    tutorialText.playTimeline(0);
                                }
                                tutorials.Add(tutorialText);
                            }
                        }
                    }
                    else if (xmlnode4.Name == "tutorial01" || xmlnode4.Name == "tutorial02" || xmlnode4.Name == "tutorial03" || xmlnode4.Name == "tutorial04" || xmlnode4.Name == "tutorial05" || xmlnode4.Name == "tutorial06" || xmlnode4.Name == "tutorial07" || xmlnode4.Name == "tutorial08" || xmlnode4.Name == "tutorial09" || xmlnode4.Name == "tutorial10")
                    {
                        if (!GameScene.shouldSkipTutorialElement(xmlnode4))
                        {
                            NSString nsstring2 = new NSString(xmlnode4.Name.Substring(8));
                            int num7 = nsstring2.intValue() - 1;
                            GameScene.GameObjectSpecial gameObjectSpecial = GameScene.GameObjectSpecial.GameObjectSpecial_createWithResIDQuad(144, num7);
                            gameObjectSpecial.color = RGBAColor.transparentRGBA;
                            gameObjectSpecial.x = (float)xmlnode4["x"].intValue() * 1f + 0f;
                            gameObjectSpecial.y = (float)xmlnode4["y"].intValue() * 1f + 0f;
                            gameObjectSpecial.rotation = (float)xmlnode4["angle"].intValue();
                            gameObjectSpecial.special = xmlnode4["special"].intValue();
                            gameObjectSpecial.parseMover(xmlnode4);
                            Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(4);
                            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
                            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 5.2));
                            timeline3.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                            gameObjectSpecial.addTimelinewithID(timeline3, 0);
                            if (gameObjectSpecial.special == 0)
                            {
                                gameObjectSpecial.playTimeline(0);
                            }
                            if (gameObjectSpecial.special == 2)
                            {
                                Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                                timeline4.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                                timeline4.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                                timeline4.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
                                timeline4.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.1));
                                timeline4.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                                timeline4.addKeyFrame(KeyFrame.makePos((double)gameObjectSpecial.x, (double)gameObjectSpecial.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                                timeline4.addKeyFrame(KeyFrame.makePos((double)gameObjectSpecial.x, (double)gameObjectSpecial.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
                                timeline4.addKeyFrame(KeyFrame.makePos((double)gameObjectSpecial.x, (double)gameObjectSpecial.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
                                timeline4.addKeyFrame(KeyFrame.makePos((double)gameObjectSpecial.x + 115.0, (double)gameObjectSpecial.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                                timeline4.addKeyFrame(KeyFrame.makePos((double)gameObjectSpecial.x + 220.0, (double)gameObjectSpecial.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                                timeline4.loopsLimit = 2;
                                timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                                gameObjectSpecial.addTimelinewithID(timeline4, 1);
                                gameObjectSpecial.playTimeline(1);
                                gameObjectSpecial.rotation = 10f;
                            }
                            tutorialImages.Add(gameObjectSpecial);
                        }
                    }
                    else if (xmlnode4.Name == "bubble")
                    {
                        int num8 = MathHelper.RND_RANGE(1, 3);
                        Bubble bubble = Bubble.Bubble_createWithResIDQuad(124, num8);
                        bubble.doRestoreCutTransparency();
                        bubble.bb = new Rectangle(0.0, 0.0, 57.0, 57.0);
                        bubble.initial_x = (bubble.x = (float)xmlnode4["x"].intValue() * 1f + 0f);
                        bubble.initial_y = (bubble.y = (float)xmlnode4["y"].intValue() * 1f + 0f);
                        bubble.initial_rotation = 0f;
                        bubble.initial_rotatedCircle = null;
                        bubble.anchor = 18;
                        bubble.popped = false;
                        Image image = Image.Image_createWithResIDQuad(124, 0);
                        image.doRestoreCutTransparency();
                        image.parentAnchor = (image.anchor = 18);
                        bubble.addChild(image);
                        bubbles.Add(bubble);
                    }
                    else if (xmlnode4.Name == "pump")
                    {
                        Pump pump = Pump.Pump_createWithResID(143);
                        pump.doRestoreCutTransparency();
                        Animation animation2 = pump;
                        float num9 = 0.05f;
                        Timeline.LoopType loopType2 = Timeline.LoopType.TIMELINE_NO_LOOP;
                        int num10 = 4;
                        int num11 = 1;
                        List<int> list2 = new List<int>();
                        list2.Add(2);
                        list2.Add(3);
                        list2.Add(0);
                        animation2.addAnimationWithDelayLoopedCountSequence(num9, loopType2, num10, num11, list2);
                        pump.bb = new Rectangle(94.0, 95.0, 57.0, 57.0);
                        pump.initial_x = (pump.x = (float)xmlnode4["x"].intValue() * 1f + 0f);
                        pump.initial_y = (pump.y = (float)xmlnode4["y"].intValue() * 1f + 0f);
                        pump.initial_rotation = 0f;
                        pump.initial_rotatedCircle = null;
                        pump.rotation = xmlnode4["angle"].floatValue() + 90f;
                        pump.updateRotation();
                        pump.anchor = 18;
                        pumps.Add(pump);
                    }
                    else if (xmlnode4.Name == "sock")
                    {
                        Sock sock = Sock.Sock_createWithResID(145);
                        sock.createAnimations();
                        sock.scaleX = (sock.scaleY = 0.7f);
                        sock.doRestoreCutTransparency();
                        sock.x = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        sock.y = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        sock.group = xmlnode4["group"].intValue();
                        sock.anchor = 10;
                        sock.rotationCenterY -= (float)sock.height / 2f - 25f;
                        if (sock.group == 0)
                        {
                            sock.setDrawQuad(0);
                        }
                        else
                        {
                            sock.setDrawQuad(1);
                        }
                        sock.state = Sock.SOCK_IDLE;
                        sock.parseMover(xmlnode4);
                        sock.rotation += 90f;
                        if (sock.mover != null)
                        {
                            sock.mover.angle += 90.0;
                        }
                        sock.updateRotation();
                        socks.Add(sock);
                    }
                    else if (xmlnode4.Name == "spike1" || xmlnode4.Name == "spike2" || xmlnode4.Name == "spike3" || xmlnode4.Name == "spike4" || xmlnode4.Name == "electro")
                    {
                        float num12 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num13 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        int num14 = xmlnode4["size"].intValue();
                        double num15 = (double)xmlnode4["angle"].intValue();
                        NSString nsstring3 = xmlnode4["toggled"];
                        int num16 = -1;
                        if (nsstring3 != null && nsstring3.length() > 0)
                        {
                            num16 = (nsstring3.isEqualToString(NSObject.NSS("false")) ? (-1) : nsstring3.intValue());
                        }
                        Spikes spikes = (Spikes)new Spikes().initWithPosXYWidthAndAngleToggled(num12, num13, num14, num15, num16);
                        spikes.parseMover(xmlnode4);
                        if (num16 != 0)
                        {
                            spikes.delegateRotateAllSpikesWithID = new Spikes.rotateAllSpikesWithID(rotateAllSpikesWithID);
                        }
                        if (xmlnode4.Name == "electro")
                        {
                            spikes.electro = true;
                            spikes.initialDelay = xmlnode4["initialDelay"].floatValue();
                            spikes.onTime = xmlnode4["onTime"].floatValue();
                            spikes.offTime = xmlnode4["offTime"].floatValue();
                            spikes.electroTimer = 0f;
                            spikes.turnElectroOff();
                            spikes.electroTimer += spikes.initialDelay;
                            spikes.updateRotation();
                        }
                        else
                        {
                            spikes.electro = false;
                        }
                        this.spikes.Add(spikes);
                    }
                    else if (xmlnode4.Name == "rotatedCircle")
                    {
                        float num17 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num18 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        float num19 = (float)xmlnode4["size"].intValue();
                        float num20 = (float)xmlnode4["handleAngle"].intValue();
                        bool flag = xmlnode4["oneHandle"].boolValue();
                        if (num19 == 60f)
                        {
                            num19 = 59.9f;
                        }
                        RotatedCircle rotatedCircle = (RotatedCircle)new RotatedCircle().init();
                        rotatedCircle.anchor = 18;
                        rotatedCircle.x = num17;
                        rotatedCircle.y = num18;
                        rotatedCircle.rotation = num20;
                        rotatedCircle.inithanlde1 = (rotatedCircle.handle1 = MathHelper.vect(rotatedCircle.x - num19, rotatedCircle.y));
                        rotatedCircle.inithanlde2 = (rotatedCircle.handle2 = MathHelper.vect(rotatedCircle.x + num19, rotatedCircle.y));
                        rotatedCircle.handle1 = MathHelper.vectRotateAround(rotatedCircle.handle1, (double)MathHelper.DEGREES_TO_RADIANS(num20), rotatedCircle.x, rotatedCircle.y);
                        rotatedCircle.handle2 = MathHelper.vectRotateAround(rotatedCircle.handle2, (double)MathHelper.DEGREES_TO_RADIANS(num20), rotatedCircle.x, rotatedCircle.y);
                        rotatedCircle.setSize(num19);
                        rotatedCircle.setHasOneHandle(flag);
                        rotatedCircles.Add(rotatedCircle);
                    }
                    else if (xmlnode4.Name == "ghost")
                    {
                        float num21 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num22 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        float num23 = xmlnode4["radius"].floatValue();
                        float num24 = xmlnode4["angle"].floatValue();
                        bool flag2 = xmlnode4["grab"].boolValue();
                        bool flag3 = xmlnode4["bubble"].boolValue();
                        int num25 = (xmlnode4["bouncer"].boolValue() ? 8 : 0) | (flag3 ? 2 : 0) | (flag2 ? 4 : 0);
                        Ghost ghost = new Ghost().initWithPositionPossibleStatesMaskGrabRadiusBouncerAngleBubblesBungeesBouncers(MathHelper.vect(num21, num22), num25, num23, num24, bubbles, bungees, bouncers);
                        ghosts.Add(ghost);
                        if (!isCandyInGhostBubbleAnimationLoaded)
                        {
                            candyGhostBubbleAnimation = CandyInGhostBubbleAnimation.CIGBAnimation_createWithResID(120);
                            candyGhostBubbleAnimation.parentAnchor = (candyGhostBubbleAnimation.anchor = 18);
                            candyGhostBubbleAnimation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
                            candyGhostBubbleAnimation.playTimeline(0);
                            candy.addChild(candyGhostBubbleAnimation);
                            candyGhostBubbleAnimation.visible = false;
                            candyGhostBubbleAnimation.addSupportingCloudsTimelines();
                            isCandyInGhostBubbleAnimationLoaded = true;
                        }
                        if (twoParts != 2)
                        {
                            if (!isCandyInGhostBubbleAnimationLeftLoaded)
                            {
                                candyGhostBubbleAnimationL = CandyInGhostBubbleAnimation.CIGBAnimation_createWithResID(120);
                                candyGhostBubbleAnimationL.parentAnchor = (candyGhostBubbleAnimationL.anchor = 18);
                                candyGhostBubbleAnimationL.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
                                candyGhostBubbleAnimationL.playTimeline(0);
                                candyL.addChild(candyGhostBubbleAnimationL);
                                candyGhostBubbleAnimationL.visible = false;
                                candyGhostBubbleAnimationL.addSupportingCloudsTimelines();
                                isCandyInGhostBubbleAnimationLeftLoaded = true;
                            }
                            if (!isCandyInGhostBubbleAnimationRightLoaded)
                            {
                                candyGhostBubbleAnimationR = CandyInGhostBubbleAnimation.CIGBAnimation_createWithResID(120);
                                candyGhostBubbleAnimationR.parentAnchor = (candyGhostBubbleAnimationR.anchor = 18);
                                candyGhostBubbleAnimationR.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
                                candyGhostBubbleAnimationR.playTimeline(0);
                                candyR.addChild(candyGhostBubbleAnimationR);
                                candyGhostBubbleAnimationR.visible = false;
                                candyGhostBubbleAnimationR.addSupportingCloudsTimelines();
                                isCandyInGhostBubbleAnimationRightLoaded = true;
                            }
                        }
                    }
                    else if (xmlnode4.Name == "bouncer1" || xmlnode4.Name == "bouncer2")
                    {
                        float num26 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num27 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        int num28 = xmlnode4["size"].intValue();
                        double num29 = (double)xmlnode4["angle"].intValue();
                        Bouncer bouncer = (Bouncer)new Bouncer().initWithPosXYWidthAndAngle(num26, num27, num28, num29);
                        bouncer.parseMover(xmlnode4);
                        bouncers.Add(bouncer);
                    }
                    else if (xmlnode4.Name == "grab")
                    {
                        float num30 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num31 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        float num32 = (float)xmlnode4["length"].intValue() * 1f;
                        float num33 = xmlnode4["radius"].floatValue();
                        bool flag4 = xmlnode4["wheel"].isEqualToString(NSObject.NSS("true"));
                        float num34 = xmlnode4["moveLength"].floatValue() * 1f;
                        bool flag5 = xmlnode4["moveVertical"].isEqualToString(NSObject.NSS("true"));
                        float num35 = xmlnode4["moveOffset"].floatValue() * 1f;
                        bool flag6 = xmlnode4["spider"].isEqualToString(NSObject.NSS("true"));
                        bool flag7 = xmlnode4["part"].isEqualToString(NSObject.NSS("L"));
                        bool flag8 = xmlnode4["hidePath"].isEqualToString(NSObject.NSS("true"));
                        Grab grab = (Grab)new Grab().init();
                        grab.initial_x = (grab.x = num30);
                        grab.initial_y = (grab.y = num31);
                        grab.initial_rotation = 0f;
                        grab.wheel = flag4;
                        grab.setSpider(flag6);
                        grab.parseMover(xmlnode4);
                        if (grab.mover != null)
                        {
                            grab.setBee();
                            if (!flag8)
                            {
                                int num36 = 2;
                                bool flag9 = xmlnode4["path"].hasPrefix(NSObject.NSS("R"));
                                for (int n = 0; n < grab.mover.pathLen - 1; n++)
                                {
                                    if (!flag9 || n % num36 == 0)
                                    {
                                        pollenDrawer.fillWithPolenFromPathIndexToPathIndexGrab(n, n + 1, grab);
                                    }
                                }
                                if (grab.mover.pathLen > 2)
                                {
                                    pollenDrawer.fillWithPolenFromPathIndexToPathIndexGrab(0, grab.mover.pathLen - 1, grab);
                                }
                            }
                        }
                        if (num33 != -1f)
                        {
                            num33 *= 1f;
                        }
                        if (num33 == -1f)
                        {
                            ConstraintedPoint constraintedPoint = star;
                            if (twoParts != 2)
                            {
                                constraintedPoint = (flag7 ? starL : starR);
                            }
                            Bungee bungee = (Bungee)new Bungee().initWithHeadAtXYTailAtTXTYandLength(null, num30, num31, constraintedPoint, constraintedPoint.pos.x, constraintedPoint.pos.y, num32);
                            bungee.bungeeAnchor.pin = bungee.bungeeAnchor.pos;
                            grab.setRope(bungee);
                        }
                        grab.setRadius(num33);
                        grab.setMoveLengthVerticalOffset(num34, flag5, num35);
                        bungees.Add(grab);
                    }
                    else if (xmlnode4.Name == "target")
                    {
                        target = GameScene.CharAnim.CharAnim_createWithResID(132);
                        target.doRestoreCutTransparency();
                        target.bb = new Rectangle(90.0, 110.0, 25.0, 1.0);
                        targetIdle = GameScene.CharAnim.CharAnim_createWithResID(133);
                        targetIdle.doRestoreCutTransparency();
                        target.o = targetIdle;
                        targetIdle.o = target;
                        targetIdle.bb = new Rectangle(90.0, 110.0, 25.0, 1.0);
                        target.addAnimationWithIDDelayLoopFirstLast(11, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 84, 112);
                        targetIdle.addAnimationWithIDDelayLoopFirstLast(0, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 18);
                        targetIdle.addAnimationWithIDDelayLoopFirstLast(1, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 19, 43);
                        Animation animation3 = targetIdle;
                        int num37 = 3;
                        float num38 = 0.05f;
                        Timeline.LoopType loopType3 = Timeline.LoopType.TIMELINE_NO_LOOP;
                        int num39 = 32;
                        int num40 = 44;
                        List<int> list3 = new List<int>();
                        list3.Add(45);
                        list3.Add(46);
                        list3.Add(47);
                        list3.Add(48);
                        list3.Add(49);
                        list3.Add(50);
                        list3.Add(51);
                        list3.Add(52);
                        list3.Add(53);
                        list3.Add(54);
                        list3.Add(55);
                        list3.Add(56);
                        list3.Add(57);
                        list3.Add(58);
                        list3.Add(59);
                        list3.Add(44);
                        list3.Add(45);
                        list3.Add(46);
                        list3.Add(47);
                        list3.Add(48);
                        list3.Add(49);
                        list3.Add(50);
                        list3.Add(51);
                        list3.Add(52);
                        list3.Add(53);
                        list3.Add(54);
                        list3.Add(55);
                        list3.Add(56);
                        list3.Add(57);
                        list3.Add(58);
                        list3.Add(59);
                        animation3.addAnimationWithIDDelayLoopCountSequence(num37, num38, loopType3, num39, num40, list3);
                        target.addAnimationWithIDDelayLoopFirstLast(4, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 37, 56);
                        target.addAnimationWithIDDelayLoopFirstLast(5, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 57, 83);
                        target.addAnimationWithIDDelayLoopFirstLast(6, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 12);
                        target.addAnimationWithIDDelayLoopFirstLast(7, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 22, 25);
                        target.addAnimationWithIDDelayLoopFirstLast(8, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 13, 21);
                        target.addAnimationWithIDDelayLoopFirstLast(9, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 22, 25);
                        target.addAnimationWithIDDelayLoopFirstLast(10, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 26, 34);
                        target.switchToAnimationatEndOfAnimationDelay(10, 7, 0.05f);
                        target.switchToAnimationatEndOfAnimationDelay(5, 9, 0.05f);
                        target.switchToAnimationatEndOfAnimationDelay(0, 11, 0.05f);
                        targetIdle.switchToAnimationatEndOfAnimationDelay(0, 1, 0.05f);
                        targetIdle.switchToAnimationatEndOfAnimationDelay(0, 3, 0.05f);
                        target.switchToAnimationatEndOfAnimationDelay(0, 4, 0.05f);
                        target.switchToAnimationatEndOfAnimationDelay(0, 5, 0.05f);
                        NSObject.NSRET(target);
                        NSObject.NSRET(targetIdle);
                        if (CTRRootController.isShowGreeting())
                        {
                            dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_showGreeting), null, 1.3f);
                            CTRRootController.setShowGreeting(false);
                        }
                        targetIdle.playTimeline(0);
                        Timeline timeline5 = targetIdle.getTimeline(0);
                        timeline5.delegateTimelineDelegate = this;
                        target.isDrawBB = true;
                        target.setPauseAtIndexforAnimation(8, 8);
                        blink = Animation.Animation_createWithResID(132);
                        blink.parentAnchor = 9;
                        blink.visible = false;
                        Animation animation4 = blink;
                        int num41 = 0;
                        float num42 = 0.05f;
                        Timeline.LoopType loopType4 = Timeline.LoopType.TIMELINE_NO_LOOP;
                        int num43 = 4;
                        int num44 = 35;
                        List<int> list4 = new List<int>();
                        list4.Add(36);
                        list4.Add(36);
                        list4.Add(36);
                        animation4.addAnimationWithIDDelayLoopCountSequence(num41, num42, loopType4, num43, num44, list4);
                        blink.setActionTargetParamSubParamAtIndexforAnimation("ACTION_SET_VISIBLE", blink, 0, 0, 2, 0);
                        blinkTimer = 3;
                        blink.doRestoreCutTransparency();
                        targetIdle.addChild(blink);
                        CTRRootController ctrrootController2 = (CTRRootController)Application.sharedRootController();
                        int num45 = 189 + ctrrootController2.getPack() * 2;
                        support = Image.Image_createWithResID(num45);
                        NSObject.NSRET(support);
                        support.doRestoreCutTransparency();
                        support.anchor = 18;
                        NSString nsstring4 = xmlnode4["x"];
                        float num46 = (float)(1 - nsstring4.intValue() % 2) * 0.33f;
                        targetIdle.x = (target.x = (support.x = (float)nsstring4.intValue() * 1f + 0f + num46));
                        NSString nsstring5 = xmlnode4["y"];
                        float num47 = (float)(1 - nsstring5.intValue() % 2) * 0.33f;
                        targetIdle.y = (target.y = (support.y = (float)nsstring5.intValue() * 1f + 0f + num47));
                        idlesTimer = MathHelper.RND_RANGE(5, 20);
                    }
                    else if (xmlnode4.Name == "steamTube")
                    {
                        float num48 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num49 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        double num50 = (double)xmlnode4["angle"].intValue();
                        SteamTube steamTube = new SteamTube().initWithPositionAngle(MathHelper.vect(num48, num49), (float)num50);
                        tubes.Add(steamTube);
                    }
                    else if (xmlnode4.Name == "lantern")
                    {
                        float num51 = (float)xmlnode4["x"].intValue() * 1f + 0f;
                        float num52 = (float)xmlnode4["y"].intValue() * 1f + 0f;
                        bool flag10 = xmlnode4["candyCaptured"].boolValue();
                        Lantern lantern = new Lantern().initWithPosition(MathHelper.vect(num51, num52));
                        lantern.parseMover(xmlnode4);
                        if (flag10)
                        {
                            isCandyInLantern = true;
                            lantern.captureCandy(star);
                            candy.x = star.pos.x;
                            candy.y = star.pos.y;
                            candy.color = RGBAColor.transparentRGBA;
                        }
                    }
                }
            }
            if (twoParts != 2)
            {
                candyBubbleAnimationL = Animation.Animation_createWithResID(120);
                candyBubbleAnimationL.parentAnchor = (candyBubbleAnimationL.anchor = 18);
                candyBubbleAnimationL.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
                candyBubbleAnimationL.playTimeline(0);
                candyL.addChild(candyBubbleAnimationL);
                candyBubbleAnimationL.visible = false;
                candyBubbleAnimationR = Animation.Animation_createWithResID(120);
                candyBubbleAnimationR.parentAnchor = (candyBubbleAnimationR.anchor = 18);
                candyBubbleAnimationR.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 12);
                candyBubbleAnimationR.playTimeline(0);
                candyR.addChild(candyBubbleAnimationR);
                candyBubbleAnimationR.visible = false;
            }
            for (int num53 = 0; num53 < rotatedCircles.Count; num53++)
            {
                RotatedCircle rotatedCircle2 = rotatedCircles[num53];
                rotatedCircle2.operating = -1;
                rotatedCircle2.circlesArray = rotatedCircles;
            }
            startCamera();
            tummyTeasers = 0;
            starsCollected = 0;
            candyBubble = null;
            candyBubbleL = null;
            candyBubbleR = null;
            mouthOpen = false;
            noCandy = twoParts != 2;
            noCandyL = false;
            noCandyR = false;
            blink.playTimeline(0);
            spiderTookCandy = false;
            time = 0f;
            score = 0;
            gravityNormal = true;
            MaterialPoint.globalGravity = MathHelper.vect(0f, 784f);
            dimTime = 0f;
            ropesCutAtOnce = 0;
            ropeAtOnceTimer = 0f;
            dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_doCandyBlink), null, 1f);
            Text text = Text.createWithFontandString(5, NSObject.NSS((ctrrootController.getPack() + 1).ToString() + " - " + (ctrrootController.getLevel() + 1).ToString()));
            text.anchor = 33;
            Text text2 = Text.createWithFontandString(5, Application.getString(1310741));
            text2.anchor = 33;
            text2.parentAnchor = 9;
            text.x = 5f;
            text.y = FrameworkTypes.SCREEN_HEIGHT + 10f;
            text2.y = 30f;
            text2.rotationCenterX -= (float)text2.width / 2f;
            text2.scaleX = (text2.scaleY = 0.7f);
            if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH || ResDataPhoneFull.LANGUAGE == Language.LANG_KO || ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
            {
                text2.y -= 7f;
            }
            text.addChild(text2);
            Timeline timeline6 = new Timeline().initWithMaxKeyFramesOnTrack(5);
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.0));
            timeline6.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
            text.addTimelinewithID(timeline6, 0);
            text.playTimeline(0);
            timeline6.delegateTimelineDelegate = staticAniPool;
            staticAniPool.addChild(text);
            text.y += FrameworkTypes.SCREEN_OFFSET_Y;
            text.x -= FrameworkTypes.SCREEN_OFFSET_X;
            int attemptsForPackLevel = CTRPreferences.getAttemptsForPackLevel(ctrrootController.getPack(), ctrrootController.getLevel());
            CTRPreferences.setAttemptsForPackLevel(attemptsForPackLevel + 1, ctrrootController.getPack(), ctrrootController.getLevel());
            ropesCutFromLevelStart = 0;
        }

        // Token: 0x06000804 RID: 2052 RVA: 0x000422E4 File Offset: 0x000404E4
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
            if (t.element is RotatedCircle && rotatedCircles.IndexOf((RotatedCircle)t.element) >= 0)
            {
                return;
            }
            if (i == 1 && targetIdle.visible)
            {
                blinkTimer--;
                if (blinkTimer == 0)
                {
                    blink.visible = true;
                    blink.playTimeline(0);
                    blinkTimer = 3;
                }
                idlesTimer--;
                if (idlesTimer == 0)
                {
                    if (MathHelper.RND_RANGE(0, 1) == 1)
                    {
                        targetIdle.playTimeline(1);
                    }
                    else
                    {
                        targetIdle.playTimeline(3);
                    }
                    idlesTimer = MathHelper.RND_RANGE(5, 20);
                }
            }
        }

        // Token: 0x06000805 RID: 2053 RVA: 0x000423AC File Offset: 0x000405AC
        public virtual void timelineFinished(Timeline t)
        {
            if (t.element is RotatedCircle && rotatedCircles.IndexOf((RotatedCircle)t.element) >= 0)
            {
                RotatedCircle rotatedCircle = (RotatedCircle)t.element;
                rotatedCircle.removeOnNextUpdate = true;
            }
        }

        // Token: 0x06000806 RID: 2054 RVA: 0x000423F4 File Offset: 0x000405F4
        public override void hide()
        {
            if (gravityButton != null)
            {
                removeChild(gravityButton);
            }
            if (ropesCutFromLevelStart > 0)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary["ropes_cut"] = ropesCutFromLevelStart.ToString();
                postGameEvent("LEVSCR_ROPES_CUT", dictionary, false);
            }
            if (starsCollected > 0)
            {
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                dictionary2["stars_collected_on_level"] = starsCollected.ToString();
                postGameEvent("LEVSCR_STARS_COLLECTED", dictionary2, false);
            }
            Lantern.removeAllLanterns();
        }

        // Token: 0x06000807 RID: 2055 RVA: 0x00042480 File Offset: 0x00040680
        public override void update(float delta)
        {
            base.update(delta);
            dd.update(delta);
            pollenDrawer.update(delta);
            bool flag = true;
            if (!noCandy)
            {
                if (candyBubble == null)
                {
                    for (int i = 0; i < bungees.Count; i++)
                    {
                        if (bungees[i].rope != null && bungees[i].rope.tail == star && bungees[i].rope.cut == -1)
                        {
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            if (!flag)
            {
                JugglingTime = 0f;
            }
            else
            {
                JugglingTime += delta;
                if (JugglingTime >= 30f)
                {
                    CTRRootController.postAchievementName(NSObject.NSS("acCandyJuggler"));
                }
            }
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < fingerCuts[j].Count; k++)
                {
                    GameScene.FingerCut fingerCut = fingerCuts[j][k];
                    if (Mover.moveVariableToTarget(ref fingerCut.c.a, 0f, 10f, delta))
                    {
                        fingerCuts[j].Remove(fingerCut);
                        k--;
                    }
                }
            }
            int num = 0;
            if (earthAnims != null)
            {
                num = earthAnims.Count;
                for (int l = 0; l < num; l++)
                {
                    Image image = earthAnims[l];
                    if (image != null)
                    {
                        image.update(delta);
                    }
                }
            }
            Mover.moveVariableToTarget(ref ropeAtOnceTimer, 0f, 1f, delta);
            ConstraintedPoint constraintedPoint = ((twoParts != 2) ? starL : star);
            float num2 = constraintedPoint.pos.x - FrameworkTypes.SCREEN_WIDTH / 2f;
            float num3 = constraintedPoint.pos.y - FrameworkTypes.SCREEN_HEIGHT / 2f;
            float num4 = MathHelper.FIT_TO_BOUNDARIES(num2, 0f, mapWidth - FrameworkTypes.SCREEN_WIDTH);
            float num5 = MathHelper.FIT_TO_BOUNDARIES(num3, 0f, mapHeight - FrameworkTypes.SCREEN_HEIGHT);
            camera.moveToXYImmediate(num4, num5, false);
            if (!freezeCamera || camera.type != CAMERA_TYPE.CAMERA_SPEED_DELAY)
            {
                camera.update(delta);
            }
            if (camera.type == CAMERA_TYPE.CAMERA_SPEED_PIXELS)
            {
                float num6 = MathHelper.vectDistance(camera.pos, MathHelper.vect(num4, num5));
                if (num6 < 50f)
                {
                    ignoreTouches = false;
                }
                if (fastenCamera)
                {
                    if (camera.speed < 2700f)
                    {
                        camera.speed *= 1.5f;
                    }
                }
                else if ((double)num6 > (double)initialCameraToStarDistance / 2.0)
                {
                    camera.speed += delta * 200f;
                    camera.speed = Math.Min(300f, camera.speed);
                }
                else
                {
                    camera.speed -= delta * 200f;
                    camera.speed = Math.Max(50f, camera.speed);
                }
                if (((double)Math.Abs(camera.pos.x - num4) < 1.0 && (double)Math.Abs(camera.pos.y - num5) < 1.0) || (!ignoreTouches && num6 > 50f))
                {
                    camera.type = CAMERA_TYPE.CAMERA_SPEED_DELAY;
                    camera.speed = 7f;
                }
            }
            else
            {
                time += delta;
            }
            if (bungees.Count > 0)
            {
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                int count = bungees.Count;
                int m = 0;
                while (m < count)
                {
                    Grab grab = bungees[m];
                    grab.update(delta);
                    Bungee rope = grab.rope;
                    if (grab.mover != null)
                    {
                        if (grab.rope != null)
                        {
                            grab.rope.bungeeAnchor.pos = MathHelper.vect(grab.x, grab.y);
                            grab.rope.bungeeAnchor.pin = grab.rope.bungeeAnchor.pos;
                        }
                        if (grab.radius != -1f)
                        {
                            grab.reCalcCircle();
                        }
                    }
                    if (rope == null)
                    {
                        goto IL_0506;
                    }
                    if (rope.cut == -1 || (double)rope.cutTime != 0.0)
                    {
                        if (rope != null)
                        {
                            rope.update(delta * ropePhysicsSpeed, ropePhysicsSpeed);
                        }
                        if (!grab.hasSpider)
                        {
                            goto IL_0506;
                        }
                        if (camera.type != CAMERA_TYPE.CAMERA_SPEED_PIXELS || !ignoreTouches)
                        {
                            grab.updateSpider(delta);
                        }
                        if (grab.spiderPos == -1f)
                        {
                            spiderWon(grab);
                            break;
                        }
                        goto IL_0506;
                    }
                IL_0982:
                    m++;
                    continue;
                IL_0506:
                    if (grab.radius != -1f && grab.rope == null && !grab.hasTimeline(11))
                    {
                        if (twoParts != 2)
                        {
                            if (!noCandyL)
                            {
                                float num7 = MathHelper.vectDistance(MathHelper.vect(grab.x, grab.y), starL.pos);
                                if (num7 <= grab.radius + 15f)
                                {
                                    Bungee bungee = (Bungee)new Bungee().initWithHeadAtXYTailAtTXTYandLength(null, grab.x, grab.y, starL, starL.pos.x, starL.pos.y, grab.radius + 15f);
                                    bungee.bungeeAnchor.pin = bungee.bungeeAnchor.pos;
                                    grab.hideRadius = true;
                                    grab.setRope(bungee);
                                    CTRSoundMgr._playSound(33);
                                    if (grab.mover != null)
                                    {
                                        CTRSoundMgr._playSound(55);
                                    }
                                }
                            }
                            if (!noCandyR && grab.rope == null)
                            {
                                float num8 = MathHelper.vectDistance(MathHelper.vect(grab.x, grab.y), starR.pos);
                                if (num8 <= grab.radius + 15f)
                                {
                                    Bungee bungee2 = (Bungee)new Bungee().initWithHeadAtXYTailAtTXTYandLength(null, grab.x, grab.y, starR, starR.pos.x, starR.pos.y, grab.radius + 15f);
                                    bungee2.bungeeAnchor.pin = bungee2.bungeeAnchor.pos;
                                    grab.hideRadius = true;
                                    grab.setRope(bungee2);
                                    CTRSoundMgr._playSound(33);
                                    if (grab.mover != null)
                                    {
                                        CTRSoundMgr._playSound(55);
                                    }
                                }
                            }
                        }
                        else
                        {
                            float num9 = MathHelper.vectDistance(MathHelper.vect(grab.x, grab.y), star.pos);
                            if (num9 <= grab.radius + 15f)
                            {
                                Bungee bungee3 = (Bungee)new Bungee().initWithHeadAtXYTailAtTXTYandLength(null, grab.x, grab.y, star, star.pos.x, star.pos.y, grab.radius + 15f);
                                bungee3.bungeeAnchor.pin = bungee3.bungeeAnchor.pos;
                                grab.hideRadius = true;
                                grab.setRope(bungee3);
                                CTRSoundMgr._playSound(33);
                                if (grab.mover != null)
                                {
                                    CTRSoundMgr._playSound(55);
                                }
                            }
                        }
                    }
                    if (rope == null)
                    {
                        goto IL_0982;
                    }
                    ConstraintedPoint bungeeAnchor = rope.bungeeAnchor;
                    ConstraintedPoint constraintedPoint2 = rope.parts[rope.parts.Count - 1];
                    Vector vector = MathHelper.vectSub(bungeeAnchor.pos, constraintedPoint2.pos);
                    bool flag5 = false;
                    if (twoParts != 2)
                    {
                        if (constraintedPoint2 == starL && !noCandyL && !flag3)
                        {
                            flag5 = true;
                        }
                        if (constraintedPoint2 == starR && !noCandyR && !flag4)
                        {
                            flag5 = true;
                        }
                    }
                    else if (!noCandy && !flag2)
                    {
                        flag5 = true;
                    }
                    if (rope.relaxed != 0 && rope.cut == -1 && flag5)
                    {
                        float num10 = MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(vector));
                        if (twoParts != 2)
                        {
                            GameObject gameObject = ((constraintedPoint2 == starL) ? candyL : candyR);
                            if (!rope.chosenOne)
                            {
                                rope.initialCandleAngle = gameObject.rotation - num10;
                            }
                            if (constraintedPoint2 == starL)
                            {
                                lastCandyRotateDeltaL = num10 + rope.initialCandleAngle - gameObject.rotation;
                                flag3 = true;
                            }
                            else
                            {
                                lastCandyRotateDeltaR = num10 + rope.initialCandleAngle - gameObject.rotation;
                                flag4 = true;
                            }
                            gameObject.rotation = num10 + rope.initialCandleAngle;
                        }
                        else
                        {
                            if (!rope.chosenOne)
                            {
                                rope.initialCandleAngle = candyMain.rotation - num10;
                            }
                            lastCandyRotateDelta = num10 + rope.initialCandleAngle - candyMain.rotation;
                            candyMain.rotation = num10 + rope.initialCandleAngle;
                            flag2 = true;
                        }
                        rope.chosenOne = true;
                        goto IL_0982;
                    }
                    rope.chosenOne = false;
                    goto IL_0982;
                }
                if (twoParts != 2)
                {
                    if (!flag3 && !noCandyL)
                    {
                        candyL.rotation += Math.Min(5f, lastCandyRotateDeltaL);
                        lastCandyRotateDeltaL *= 0.98f;
                    }
                    if (!flag4 && !noCandyR)
                    {
                        candyR.rotation += Math.Min(5f, lastCandyRotateDeltaR);
                        lastCandyRotateDeltaR *= 0.98f;
                    }
                }
                else if (!flag2 && !noCandy)
                {
                    candyMain.rotation += Math.Min(5f, lastCandyRotateDelta);
                    lastCandyRotateDelta *= 0.98f;
                }
            }
            if (!noCandy)
            {
                star.update(delta * ropePhysicsSpeed);
                candy.x = star.pos.x;
                candy.y = star.pos.y;
                candy.update(delta);
                BaseElement.calculateTopLeft(candy);
            }
            if (twoParts != 2)
            {
                candyL.update(delta);
                starL.update(delta * ropePhysicsSpeed);
                candyR.update(delta);
                starR.update(delta * ropePhysicsSpeed);
                if (twoParts == 1)
                {
                    for (int n = 0; n < 30; n++)
                    {
                        ConstraintedPoint.satisfyConstraints(starL);
                        ConstraintedPoint.satisfyConstraints(starR);
                    }
                }
                if ((double)partsDist > 0.0)
                {
                    if (Mover.moveVariableToTarget(ref partsDist, 0f, 200f, delta))
                    {
                        CTRSoundMgr._playSound(50);
                        twoParts = 2;
                        noCandy = false;
                        noCandyL = true;
                        noCandyR = true;
                        int num11 = Preferences._getIntForKey("PREFS_CANDIES_UNITED") + 1;
                        Preferences._setIntforKey(num11, "PREFS_CANDIES_UNITED", false);
                        if (num11 >= 100)
                        {
                            CTRRootController.postAchievementName(NSObject.NSS("acRomanticSoul"));
                        }
                        bool flag6 = false;
                        bool flag7 = false;
                        if (ghosts != null)
                        {
                            foreach (Ghost ghost in ghosts)
                            {
                                if (ghost != null)
                                {
                                    if (candyBubbleL != null && ghost.bubble == candyBubbleL)
                                    {
                                        flag6 = true;
                                    }
                                    if (candyBubbleR != null && ghost.bubble == candyBubbleR)
                                    {
                                        flag7 = true;
                                    }
                                }
                            }
                        }
                        if (candyBubbleL != null && candyBubbleR != null && flag6 && flag7)
                        {
                            candyGhostBubbleAnimation.visible = true;
                            candyBubble = candyBubbleL;
                            shouldRestoreSecondGhost = true;
                        }
                        else if (candyBubbleL != null && flag6)
                        {
                            candyGhostBubbleAnimation.visible = true;
                            candyBubble = candyBubbleL;
                        }
                        else if (candyBubbleR != null && flag7)
                        {
                            candyGhostBubbleAnimation.visible = true;
                            candyBubble = candyBubbleR;
                        }
                        else if (candyBubbleL != null || candyBubbleR != null)
                        {
                            candyBubble = ((candyBubbleL != null) ? candyBubbleL : candyBubbleR);
                            candyBubbleAnimation.visible = true;
                            if (ghosts != null)
                            {
                                foreach (Ghost ghost2 in ghosts)
                                {
                                    if (ghost2 != null)
                                    {
                                        if (candyBubbleL != null && ghost2.bubble == candyBubbleL)
                                        {
                                            ghost2.cyclingEnabled = true;
                                            ghost2.resetToState(1);
                                        }
                                        if (candyBubbleR != null && ghost2.bubble == candyBubbleR)
                                        {
                                            ghost2.cyclingEnabled = true;
                                            ghost2.resetToState(1);
                                        }
                                    }
                                }
                            }
                        }
                        lastCandyRotateDelta = 0f;
                        lastCandyRotateDeltaL = 0f;
                        lastCandyRotateDeltaR = 0f;
                        star.pos.x = starL.pos.x;
                        star.pos.y = starL.pos.y;
                        candy.x = star.pos.x;
                        candy.y = star.pos.y;
                        BaseElement.calculateTopLeft(candy);
                        Vector vector2 = MathHelper.vectSub(starL.pos, starL.prevPos);
                        Vector vector3 = MathHelper.vectSub(starR.pos, starR.prevPos);
                        Vector vector4 = MathHelper.vect((vector2.x + vector3.x) / 2f, (vector2.y + vector3.y) / 2f);
                        star.prevPos = MathHelper.vectSub(star.pos, vector4);
                        int count2 = bungees.Count;
                        for (int num12 = 0; num12 < count2; num12++)
                        {
                            Grab grab2 = bungees[num12];
                            Bungee rope2 = grab2.rope;
                            if (rope2 != null && rope2.cut != rope2.parts.Count - 3 && (rope2.tail == starL || rope2.tail == starR))
                            {
                                ConstraintedPoint constraintedPoint3 = rope2.parts[rope2.parts.Count - 2];
                                int num13 = (int)rope2.tail.restLengthFor(constraintedPoint3);
                                star.addConstraintwithRestLengthofType(constraintedPoint3, (float)num13, Constraint.CONSTRAINT.CONSTRAINT_DISTANCE);
                                rope2.tail = star;
                                rope2.parts[rope2.parts.Count - 1] = star;
                                rope2.initialCandleAngle = 0f;
                                rope2.chosenOne = false;
                            }
                        }
                        Animation animation = Animation.Animation_createWithResID(103);
                        animation.doRestoreCutTransparency();
                        animation.x = candy.x;
                        animation.y = candy.y;
                        animation.anchor = 18;
                        int num14 = animation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 4);
                        animation.getTimeline(num14).delegateTimelineDelegate = aniPool;
                        animation.playTimeline(0);
                        aniPool.addChild(animation);
                    }
                    else
                    {
                        starL.changeRestLengthToFor(partsDist, starR);
                        starR.changeRestLengthToFor(partsDist, starL);
                    }
                }
                if (!noCandyL && !noCandyR && GameObject.objectsIntersect(candyL, candyR) && twoParts == 0)
                {
                    twoParts = 1;
                    partsDist = MathHelper.vectDistance(starL.pos, starR.pos);
                    starL.addConstraintwithRestLengthofType(starR, partsDist, Constraint.CONSTRAINT.CONSTRAINT_NOT_MORE_THAN);
                    starR.addConstraintwithRestLengthofType(starL, partsDist, Constraint.CONSTRAINT.CONSTRAINT_NOT_MORE_THAN);
                }
            }
            target.update(delta);
            targetIdle.update(delta);
            if (camera.type != CAMERA_TYPE.CAMERA_SPEED_PIXELS || !ignoreTouches)
            {
                int count3 = stars.Count;
                for (int num15 = 0; num15 < count3; num15++)
                {
                    Star star = stars[num15];
                    if (star != null)
                    {
                        star.update(delta);
                        if ((double)star.timeout > 0.0 && (double)star.time == 0.0)
                        {
                            star.getTimeline(1).delegateTimelineDelegate = aniPool;
                            aniPool.addChild(star);
                            stars.Remove(star);
                            star.timedAnim.playTimeline(1);
                            star.playTimeline(1);
                            break;
                        }
                        bool flag8;
                        if (twoParts != 2)
                        {
                            flag8 = (GameObject.objectsIntersect(candyL, star) && !noCandyL) || (GameObject.objectsIntersect(candyR, star) && !noCandyR);
                        }
                        else
                        {
                            flag8 = GameObject.objectsIntersect(candy, star) && !noCandy;
                        }
                        if (flag8)
                        {
                            candyBlink.playTimeline(1);
                            starsCollected++;
                            hudStar[starsCollected - 1].playTimeline(0);
                            Animation animation2 = Animation.Animation_createWithResID(119);
                            animation2.doRestoreCutTransparency();
                            animation2.x = star.x;
                            animation2.y = star.y;
                            stars.Remove(star);
                            animation2.anchor = 18;
                            int num16 = animation2.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 12);
                            animation2.getTimeline(num16).delegateTimelineDelegate = aniPool;
                            animation2.playTimeline(0);
                            aniPool.addChild(animation2);
                            CTRSoundMgr._playSound(35 + starsCollected - 1);
                            if (targetIdle.visible && targetIdle.getCurrentTimelineIndex() == 0)
                            {
                                target.playTimeline(4);
                                break;
                            }
                            break;
                        }
                    }
                }
            }
            num = bubbles.Count;
            for (int num17 = 0; num17 < num; num17++)
            {
                Bubble bubble = bubbles[num17];
                if (bubble != null)
                {
                    bubble.update(delta);
                    if (twoParts != 2)
                    {
                        if (!noCandyL && !bubble.popped && MathHelper.pointInRect(candyL.x, candyL.y, bubble.x - 30f, bubble.y - 30f, 60f, 60f))
                        {
                            if (candyBubbleL != null)
                            {
                                popBubbleAtXY(bubble.x, bubble.y);
                                if (ghosts != null)
                                {
                                    foreach (Ghost ghost3 in ghosts)
                                    {
                                        if (ghost3 != null && ghost3.bubble == candyBubbleL)
                                        {
                                            ghost3.cyclingEnabled = true;
                                            ghost3.resetToState(1);
                                        }
                                    }
                                }
                            }
                            bool flag9 = false;
                            if (ghosts != null)
                            {
                                foreach (Ghost ghost4 in ghosts)
                                {
                                    if (ghost4 != null && ghost4.bubble == bubble)
                                    {
                                        ghost4.cyclingEnabled = false;
                                        flag9 = true;
                                    }
                                }
                            }
                            candyBubbleL = bubble;
                            if (flag9)
                            {
                                if (isCandyInGhostBubbleAnimationLeftLoaded)
                                {
                                    candyGhostBubbleAnimationL.visible = true;
                                }
                                candyBubbleAnimationL.visible = false;
                            }
                            else
                            {
                                candyBubbleAnimationL.visible = true;
                                if (isCandyInGhostBubbleAnimationLeftLoaded)
                                {
                                    candyGhostBubbleAnimationL.visible = false;
                                }
                            }
                            CTRSoundMgr._playSound(23);
                            bubble.popped = true;
                            bubble.removeChildWithID(0);
                            break;
                        }
                        if (!noCandyR && !bubble.popped && MathHelper.pointInRect(candyR.x, candyR.y, bubble.x - 30f, bubble.y - 30f, 60f, 60f))
                        {
                            if (candyBubbleR != null)
                            {
                                popBubbleAtXY(bubble.x, bubble.y);
                                if (ghosts != null)
                                {
                                    foreach (Ghost ghost5 in ghosts)
                                    {
                                        if (ghost5 != null && ghost5.bubble == candyBubbleR)
                                        {
                                            ghost5.cyclingEnabled = true;
                                            ghost5.resetToState(1);
                                        }
                                    }
                                }
                            }
                            bool flag10 = false;
                            if (ghosts != null)
                            {
                                foreach (Ghost ghost6 in ghosts)
                                {
                                    if (ghost6 != null && ghost6.bubble == bubble)
                                    {
                                        ghost6.cyclingEnabled = false;
                                        flag10 = true;
                                    }
                                }
                            }
                            candyBubbleR = bubble;
                            if (flag10)
                            {
                                if (isCandyInGhostBubbleAnimationRightLoaded)
                                {
                                    candyGhostBubbleAnimationR.visible = true;
                                }
                                candyBubbleAnimationR.visible = false;
                            }
                            else
                            {
                                candyBubbleAnimationR.visible = true;
                                if (isCandyInGhostBubbleAnimationRightLoaded)
                                {
                                    candyGhostBubbleAnimationR.visible = false;
                                }
                            }
                            CTRSoundMgr._playSound(23);
                            bubble.popped = true;
                            bubble.removeChildWithID(0);
                            break;
                        }
                    }
                    else if (!noCandy && !bubble.popped && MathHelper.pointInRect(candy.x, candy.y, bubble.x - 30f, bubble.y - 30f, 60f, 60f))
                    {
                        if (candyBubble != null)
                        {
                            popBubbleAtXY(bubble.x, bubble.y);
                            if (ghosts != null)
                            {
                                foreach (Ghost ghost7 in ghosts)
                                {
                                    if (ghost7 != null && ghost7.bubble == candyBubble)
                                    {
                                        ghost7.cyclingEnabled = true;
                                        ghost7.resetToState(1);
                                    }
                                }
                            }
                        }
                        bool flag11 = false;
                        if (ghosts != null)
                        {
                            foreach (Ghost ghost8 in ghosts)
                            {
                                if (ghost8 != null && ghost8.bubble == bubble)
                                {
                                    ghost8.cyclingEnabled = false;
                                    flag11 = true;
                                }
                            }
                        }
                        candyBubble = bubble;
                        if (flag11)
                        {
                            if (isCandyInGhostBubbleAnimationLoaded)
                            {
                                candyGhostBubbleAnimation.visible = true;
                            }
                            candyBubbleAnimation.visible = false;
                        }
                        else
                        {
                            candyBubbleAnimation.visible = true;
                            if (isCandyInGhostBubbleAnimationLoaded)
                            {
                                candyGhostBubbleAnimation.visible = false;
                            }
                        }
                        CTRSoundMgr._playSound(23);
                        bubble.popped = true;
                        bubble.removeChildWithID(0);
                        break;
                    }
                    if (!bubble.withoutShadow)
                    {
                        for (int num18 = 0; num18 < rotatedCircles.Count; num18++)
                        {
                            RotatedCircle rotatedCircle = rotatedCircles[num18];
                            float num19 = MathHelper.vectDistance(MathHelper.vect(bubble.x, bubble.y), MathHelper.vect(rotatedCircle.x, rotatedCircle.y));
                            if (num19 < rotatedCircle.sizeInPixels)
                            {
                                bubble.withoutShadow = true;
                            }
                        }
                    }
                }
            }
            if (ghosts != null)
            {
                foreach (Ghost ghost9 in ghosts)
                {
                    if (ghost9 != null)
                    {
                        ghost9.update(delta);
                    }
                }
            }
            num = tutorials.Count;
            for (int num20 = 0; num20 < num; num20++)
            {
                Text text = tutorials[num20];
                if (text != null)
                {
                    text.update(delta);
                }
            }
            num = tutorialImages.Count;
            for (int num21 = 0; num21 < num; num21++)
            {
                GameObject gameObject2 = tutorialImages[num21];
                if (gameObject2 != null)
                {
                    gameObject2.update(delta);
                }
            }
            num = pumps.Count;
            for (int num22 = 0; num22 < num; num22++)
            {
                Pump pump = pumps[num22];
                if (pump != null)
                {
                    pump.update(delta);
                    if (Mover.moveVariableToTarget(ref pump.pumpTouchTimer, 0f, 1f, delta))
                    {
                        operatePump(pump);
                    }
                }
            }
            if (tubes != null)
            {
                foreach (SteamTube steamTube in tubes)
                {
                    if (steamTube != null)
                    {
                        steamTube.update(delta);
                        if (steamTube.steamState != 3)
                        {
                            operateSteamTube(steamTube);
                        }
                    }
                }
            }
            List<Lantern> allLanterns = Lantern.getAllLanterns();
            foreach (Lantern lantern in allLanterns)
            {
                lantern.update(delta);
                if (!isCandyInLantern && lantern.lanternState == 0 && MathHelper.vectDistance(star.pos, MathHelper.vect(lantern.x, lantern.y)) < 32f)
                {
                    isCandyInLantern = true;
                    candy.passTransformationsToChilds = true;
                    candyMain.scaleX = (candyMain.scaleY = 1f);
                    candyTop.scaleX = (candyTop.scaleY = 1f);
                    Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                    timeline.addKeyFrame(KeyFrame.makePos((double)candy.x, (double)candy.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                    timeline.addKeyFrame(KeyFrame.makePos((double)lantern.x, (double)lantern.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
                    timeline.addKeyFrame(KeyFrame.makeScale(0.71, 0.71, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                    timeline.addKeyFrame(KeyFrame.makeScale(0.3, 0.3, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
                    timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                    timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
                    if (candy.hasTimeline(0))
                    {
                        candy.removeTimeline(0);
                    }
                    candy.addTimelinewithID(timeline, 0);
                    candy.playTimeline(0);
                    releaseAllRopes(false);
                    if (candyBubble != null)
                    {
                        popCandyBubble(false);
                    }
                    dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(lantern._captureCandy), star, 0.05f);
                    if (special == 3)
                    {
                        special = 0;
                        foreach (GameScene.TutorialText tutorialText in tutorials)
                        {
                            if (tutorialText.special == 3)
                            {
                                tutorialText.playTimeline(0);
                            }
                            else
                            {
                                tutorialText.getCurrentTimeline().jumpToTrackKeyFrame(3, 2);
                            }
                        }
                        foreach (GameScene.GameObjectSpecial gameObjectSpecial in tutorialImages)
                        {
                            if (gameObjectSpecial.special == 3)
                            {
                                gameObjectSpecial.playTimeline(0);
                            }
                            else
                            {
                                gameObjectSpecial.getCurrentTimeline().jumpToTrackKeyFrame(3, 2);
                            }
                        }
                    }
                }
            }
            RotatedCircle rotatedCircle2 = null;
            for (int num23 = 0; num23 < rotatedCircles.Count; num23++)
            {
                RotatedCircle rotatedCircle3 = rotatedCircles[num23];
                for (int num24 = 0; num24 < bungees.Count; num24++)
                {
                    Grab grab3 = bungees[num24];
                    if (MathHelper.vectDistance(MathHelper.vect(grab3.x, grab3.y), MathHelper.vect(rotatedCircle3.x, rotatedCircle3.y)) <= rotatedCircle3.sizeInPixels + 5f)
                    {
                        if (rotatedCircle3.containedObjects.IndexOf(grab3) < 0)
                        {
                            rotatedCircle3.containedObjects.Add(grab3);
                        }
                    }
                    else if (rotatedCircle3.containedObjects.IndexOf(grab3) >= 0)
                    {
                        rotatedCircle3.containedObjects.Remove(grab3);
                    }
                }
                for (int num25 = 0; num25 < bubbles.Count; num25++)
                {
                    Bubble bubble2 = bubbles[num25];
                    if (MathHelper.vectDistance(MathHelper.vect(bubble2.x, bubble2.y), MathHelper.vect(rotatedCircle3.x, rotatedCircle3.y)) <= rotatedCircle3.sizeInPixels + 10f)
                    {
                        if (rotatedCircle3.containedObjects.IndexOf(bubble2) < 0)
                        {
                            rotatedCircle3.containedObjects.Add(bubble2);
                        }
                    }
                    else if (rotatedCircle3.containedObjects.IndexOf(bubble2) >= 0)
                    {
                        rotatedCircle3.containedObjects.Remove(bubble2);
                    }
                }
                for (int num26 = 0; num26 < pumps.Count; num26++)
                {
                    Pump pump2 = pumps[num26];
                    if (MathHelper.vectDistance(MathHelper.vect(pump2.x, pump2.y), MathHelper.vect(rotatedCircle3.x, rotatedCircle3.y)) <= rotatedCircle3.sizeInPixels + 10f)
                    {
                        if (rotatedCircle3.containedObjects.IndexOf(pump2) < 0)
                        {
                            rotatedCircle3.containedObjects.Add(pump2);
                        }
                    }
                    else if (rotatedCircle3.containedObjects.IndexOf(pump2) >= 0)
                    {
                        rotatedCircle3.containedObjects.Remove(pump2);
                    }
                }
                if (rotatedCircle3.removeOnNextUpdate)
                {
                    rotatedCircle2 = rotatedCircle3;
                }
                rotatedCircle3.update(delta);
            }
            if (rotatedCircle2 != null)
            {
                rotatedCircles.Remove(rotatedCircle2);
            }
            num = socks.Count;
            for (int num27 = 0; num27 < num; num27++)
            {
                Sock sock = socks[num27];
                sock.update(delta);
                if (Mover.moveVariableToTarget(ref sock.idleTimeout, 0f, 1f, delta))
                {
                    sock.state = Sock.SOCK_IDLE;
                }
                float rotation = sock.rotation;
                sock.rotation = 0f;
                sock.updateRotation();
                Vector vector5 = MathHelper.vectRotate(star.posDelta, (double)MathHelper.DEGREES_TO_RADIANS(-rotation));
                sock.rotation = rotation;
                sock.updateRotation();
                if ((double)vector5.y >= 0.0 && (MathHelper.lineInRect(sock.t1.x, sock.t1.y, sock.t2.x, sock.t2.y, star.pos.x - 10f, star.pos.y - 10f, 20f, 20f) || MathHelper.lineInRect(sock.b1.x, sock.b1.y, sock.b2.x, sock.b2.y, star.pos.x - 10f, star.pos.y - 10f, 20f, 20f)))
                {
                    if (sock.state == Sock.SOCK_IDLE)
                    {
                        for (int num28 = 0; num28 < num; num28++)
                        {
                            Sock sock2 = socks[num28];
                            if (sock2 != sock && sock2.group == sock.group)
                            {
                                sock.state = Sock.SOCK_RECEIVING;
                                sock2.state = Sock.SOCK_THROWING;
                                releaseAllRopes(false);
                                int num29 = Preferences._getIntForKey("PREFS_SOCKS_USED") + 1;
                                Preferences._setIntforKey(num29, "PREFS_SOCKS_USED", false);
                                if (num29 >= 200)
                                {
                                    CTRRootController.postAchievementName(NSObject.NSS("acMagician"));
                                }
                                savedSockSpeed = 0.9f * MathHelper.vectLength(star.v);
                                targetSock = sock2;
                                sock.light.playTimeline(0);
                                sock.light.visible = true;
                                CTRSoundMgr._playSound(51);
                                dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_teleport), null, 0.1f);
                                break;
                            }
                        }
                        break;
                    }
                }
                else if (sock.state != Sock.SOCK_IDLE && sock.idleTimeout == 0f)
                {
                    sock.idleTimeout = 0.8f;
                }
            }
            num = razors.Count;
            for (int num30 = 0; num30 < num; num30++)
            {
                Razor razor = razors[num30];
                if (razor != null)
                {
                    razor.update(delta);
                    cutWithRazorOrLine1Line2Immediate(razor, MathHelper.vectZero, MathHelper.vectZero, false);
                }
            }
            num = spikes.Count;
            foreach (Spikes spikes in spikes)
            {
                spikes.update(delta);
            }
            if (!isCandyInLantern)
            {
                for (int num31 = 0; num31 < num; num31++)
                {
                    Spikes spikes2 = spikes[num31];
                    if (spikes2 != null && (!spikes2.electro || (spikes2.electro && spikes2.electroOn)))
                    {
                        bool flag12 = false;
                        bool flag13;
                        if (twoParts != 2)
                        {
                            flag13 = MathHelper.lineInRect(spikes2.t1.x, spikes2.t1.y, spikes2.t2.x, spikes2.t2.y, starL.pos.x - 5f, starL.pos.y - 5f, 10f, 10f) || MathHelper.lineInRect(spikes2.b1.x, spikes2.b1.y, spikes2.b2.x, spikes2.b2.y, starL.pos.x - 5f, starL.pos.y - 5f, 10f, 10f);
                            flag13 = flag13 && !noCandyL;
                            if (flag13)
                            {
                                flag12 = true;
                            }
                            else
                            {
                                flag13 = MathHelper.lineInRect(spikes2.t1.x, spikes2.t1.y, spikes2.t2.x, spikes2.t2.y, starR.pos.x - 5f, starR.pos.y - 5f, 10f, 10f) || MathHelper.lineInRect(spikes2.b1.x, spikes2.b1.y, spikes2.b2.x, spikes2.b2.y, starR.pos.x - 5f, starR.pos.y - 5f, 10f, 10f);
                                flag13 = flag13 && !noCandyR;
                            }
                        }
                        else
                        {
                            flag13 = MathHelper.lineInRect(spikes2.t1.x, spikes2.t1.y, spikes2.t2.x, spikes2.t2.y, star.pos.x - 5f, star.pos.y - 5f, 10f, 10f) || MathHelper.lineInRect(spikes2.b1.x, spikes2.b1.y, spikes2.b2.x, spikes2.b2.y, star.pos.x - 5f, star.pos.y - 5f, 10f, 10f);
                            flag13 = flag13 && !noCandy;
                        }
                        if (flag13)
                        {
                            if (twoParts != 2)
                            {
                                if (flag12)
                                {
                                    if (candyBubbleL != null)
                                    {
                                        popCandyBubble(true);
                                    }
                                }
                                else if (candyBubbleR != null)
                                {
                                    popCandyBubble(false);
                                }
                            }
                            else if (candyBubble != null)
                            {
                                popCandyBubble(false);
                            }
                            CTRPreferences ctrpreferences = Application.sharedPreferences();
                            int intForKey = ctrpreferences.getIntForKey("PREFS_SELECTED_CANDY");
                            Image image2 = Image.Image_createWithResID(GameScene.CANDIES[intForKey]);
                            image2.doRestoreCutTransparency();
                            CandyBreak candyBreak = (CandyBreak)new CandyBreak().initWithTotalParticlesandImageGrid(5, image2);
                            if (gravityButton != null && !gravityNormal)
                            {
                                candyBreak.gravity.y = -500f;
                                candyBreak.angle = 90f;
                            }
                            candyBreak.particlesDelegate = new Particles.ParticlesFinished(aniPool.particlesFinished);
                            if (twoParts != 2)
                            {
                                if (flag12)
                                {
                                    candyBreak.x = candyL.x;
                                    candyBreak.y = candyL.y;
                                    noCandyL = true;
                                }
                                else
                                {
                                    candyBreak.x = candyR.x;
                                    candyBreak.y = candyR.y;
                                    noCandyR = true;
                                }
                            }
                            else
                            {
                                candyBreak.x = candy.x;
                                candyBreak.y = candy.y;
                                noCandy = true;
                            }
                            candyBreak.startSystem(5);
                            aniPool.addChild(candyBreak);
                            CTRSoundMgr._playSound(24);
                            releaseAllRopes(flag12);
                            if (restartState != 0 && (twoParts == 2 || !noCandyL || !noCandyR))
                            {
                                dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_gameLost), null, 0.3f);
                            }
                            if (ghosts != null)
                            {
                                foreach (Ghost ghost10 in ghosts)
                                {
                                    if (ghost10 != null)
                                    {
                                        ghost10.candyBreak = true;
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
            }
            num = bouncers.Count;
            for (int num32 = 0; num32 < num; num32++)
            {
                Bouncer bouncer = bouncers[num32];
                if (bouncer != null)
                {
                    bouncer.update(delta);
                    bool flag14 = false;
                    bool flag15;
                    if (twoParts != 2)
                    {
                        flag15 = MathHelper.lineInRect(bouncer.t1.x, bouncer.t1.y, bouncer.t2.x, bouncer.t2.y, starL.pos.x - 20f, starL.pos.y - 20f, 40f, 40f) || MathHelper.lineInRect(bouncer.b1.x, bouncer.b1.y, bouncer.b2.x, bouncer.b2.y, starL.pos.x - 20f, starL.pos.y - 20f, 40f, 40f);
                        flag15 = flag15 && !noCandyL;
                        if (flag15)
                        {
                            flag14 = true;
                        }
                        else
                        {
                            flag15 = MathHelper.lineInRect(bouncer.t1.x, bouncer.t1.y, bouncer.t2.x, bouncer.t2.y, starR.pos.x - 20f, starR.pos.y - 20f, 40f, 40f) || MathHelper.lineInRect(bouncer.b1.x, bouncer.b1.y, bouncer.b2.x, bouncer.b2.y, starR.pos.x - 20f, starR.pos.y - 20f, 40f, 40f);
                            flag15 = flag15 && !noCandyR;
                        }
                    }
                    else
                    {
                        flag15 = MathHelper.lineInRect(bouncer.t1.x, bouncer.t1.y, bouncer.t2.x, bouncer.t2.y, star.pos.x - 20f, star.pos.y - 20f, 40f, 40f) || MathHelper.lineInRect(bouncer.b1.x, bouncer.b1.y, bouncer.b2.x, bouncer.b2.y, star.pos.x - 20f, star.pos.y - 20f, 40f, 40f);
                        flag15 = flag15 && !noCandy;
                    }
                    if (flag15)
                    {
                        if (twoParts != 2)
                        {
                            if (flag14)
                            {
                                handleBouncePtDelta(bouncer, starL, delta);
                            }
                            else
                            {
                                handleBouncePtDelta(bouncer, starR, delta);
                            }
                        }
                        else
                        {
                            handleBouncePtDelta(bouncer, star, delta);
                        }
                    }
                    else
                    {
                        bouncer.skip = false;
                    }
                }
            }
            float num33 = -18f;
            float num34 = 20f;
            if (twoParts == 0)
            {
                if (candyBubbleL != null)
                {
                    if (gravityButton != null && !gravityNormal)
                    {
                        starL.applyImpulseDelta(MathHelper.vect(-starL.v.x / num34, -starL.v.y / num34 - num33), delta);
                    }
                    else
                    {
                        starL.applyImpulseDelta(MathHelper.vect(-starL.v.x / num34, -starL.v.y / num34 + num33), delta);
                    }
                }
                if (candyBubbleR != null)
                {
                    if (gravityButton != null && !gravityNormal)
                    {
                        starR.applyImpulseDelta(MathHelper.vect(-starR.v.x / num34, -starR.v.y / num34 - num33), delta);
                    }
                    else
                    {
                        starR.applyImpulseDelta(MathHelper.vect(-starR.v.x / num34, -starR.v.y / num34 + num33), delta);
                    }
                }
            }
            if (twoParts == 1)
            {
                if (candyBubbleR != null || candyBubbleL != null)
                {
                    if (gravityButton != null && !gravityNormal)
                    {
                        starL.applyImpulseDelta(MathHelper.vect(-starL.v.x / num34, -starL.v.y / num34 - num33), delta);
                        starR.applyImpulseDelta(MathHelper.vect(-starR.v.x / num34, -starR.v.y / num34 - num33), delta);
                    }
                    else
                    {
                        starL.applyImpulseDelta(MathHelper.vect(-starL.v.x / num34, -starL.v.y / num34 + num33), delta);
                        starR.applyImpulseDelta(MathHelper.vect(-starR.v.x / num34, -starR.v.y / num34 + num33), delta);
                    }
                }
            }
            else if (candyBubble != null)
            {
                if (gravityButton != null && !gravityNormal)
                {
                    star.applyImpulseDelta(MathHelper.vect(-star.v.x / num34, -star.v.y / num34 - num33), delta);
                }
                else
                {
                    star.applyImpulseDelta(MathHelper.vect(-star.v.x / num34, -star.v.y / num34 + num33), delta);
                }
            }
            if (!noCandy)
            {
                if (!mouthOpen)
                {
                    if (!isCandyInLantern && MathHelper.vectDistance(star.pos, MathHelper.vect(target.x, target.y)) < 100f)
                    {
                        mouthOpen = true;
                        target.playTimeline(8);
                        CTRSoundMgr._playSound(27);
                        mouthCloseTimer = 1f;
                    }
                }
                else if ((double)mouthCloseTimer > 0.0)
                {
                    Mover.moveVariableToTarget(ref mouthCloseTimer, 0f, 1f, delta);
                    if ((double)mouthCloseTimer <= 0.0)
                    {
                        if (isCandyInLantern || MathHelper.vectDistance(star.pos, MathHelper.vect(target.x, target.y)) > 100f)
                        {
                            mouthOpen = false;
                            target.playTimeline(9);
                            CTRSoundMgr._playSound(26);
                            tummyTeasers++;
                            if (tummyTeasers >= 10)
                            {
                                CTRRootController.postAchievementName(NSObject.NSS("acTummyTeaser"));
                            }
                        }
                        else
                        {
                            mouthCloseTimer = 1f;
                        }
                    }
                }
                if (restartState != 0 && GameObject.objectsIntersect(candy, target))
                {
                    postGameEvent("LEVSCR_CANDY_EATEN", null, false);
                    gameWon();
                    return;
                }
            }
            bool flag16 = twoParts == 2 && pointOutOfScreen(star) && !noCandy;
            bool flag17 = twoParts != 2 && pointOutOfScreen(starL) && !noCandyL;
            bool flag18 = twoParts != 2 && pointOutOfScreen(starR) && !noCandyR;
            if (flag17 || flag18 || flag16)
            {
                if (twoParts == 2)
                {
                    releaseAllRopes(false);
                }
                bool flag19 = false;
                if (twoParts != 2 && (noCandyL || noCandyR))
                {
                    flag19 = true;
                }
                if (flag16)
                {
                    noCandy = true;
                }
                if (flag17)
                {
                    noCandyL = true;
                }
                if (flag18)
                {
                    noCandyR = true;
                }
                if (restartState != 0)
                {
                    int num35 = Preferences._getIntForKey("PREFS_CANDIES_LOST") + 1;
                    Preferences._setIntforKey(num35, "PREFS_CANDIES_LOST", false);
                    if (num35 >= 50)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acWeightLoser"));
                    }
                    if (num35 >= 200)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acCalorieMinimizer"));
                    }
                    if (!flag19)
                    {
                        gameLost();
                    }
                    return;
                }
            }
            if (special != 0 && special == 1 && !noCandy && candyBubble != null && candy.y < 100f && candy.x > 200f)
            {
                special = 0;
                int num36 = tutorials.Count;
                for (int num37 = 0; num37 < num36; num37++)
                {
                    GameScene.TutorialText tutorialText2 = tutorials[num37];
                    if (tutorialText2 != null && tutorialText2.special == 1)
                    {
                        tutorialText2.playTimeline(0);
                    }
                }
                num36 = tutorialImages.Count;
                for (int num38 = 0; num38 < num36; num38++)
                {
                    GameScene.GameObjectSpecial gameObjectSpecial2 = tutorialImages[num38];
                    if (gameObjectSpecial2.special == 1)
                    {
                        gameObjectSpecial2.playTimeline(0);
                    }
                }
            }
            if (Mover.moveVariableToTarget(ref dimTime, 0f, 1f, delta))
            {
                if (restartState == 0)
                {
                    restartState = 1;
                    hide();
                    show();
                    dimTime = 0.15f;
                    return;
                }
                restartState = -1;
            }
        }

        // Token: 0x06000808 RID: 2056 RVA: 0x00045820 File Offset: 0x00043A20
        public override void draw()
        {
            base.preDraw();
            OpenGL.glPushMatrix();
            OpenGL.glTranslatef(-FrameworkTypes.SCREEN_OFFSET_X, -FrameworkTypes.SCREEN_OFFSET_Y, 0f);
            float num = FrameworkTypes.SCREEN_BG_SCALE_X;
            float num2 = FrameworkTypes.SCREEN_BG_SCALE_Y;
            if (mapHeight > FrameworkTypes.SCREEN_HEIGHT)
            {
                num2 = (FrameworkTypes.SCREEN_HEIGHT_EXPANDED - FrameworkTypes.SCREEN_OFFSET_Y) / FrameworkTypes.SCREEN_HEIGHT;
            }
            if (mapWidth > FrameworkTypes.SCREEN_WIDTH)
            {
                num = (FrameworkTypes.SCREEN_WIDTH_EXPANDED - FrameworkTypes.SCREEN_OFFSET_X) / FrameworkTypes.SCREEN_WIDTH;
            }
            OpenGL.glScalef(num, num2, 1f);
            OpenGL.glEnable(0);
            OpenGL.glDisable(1);
            Vector pos = camera.pos;
            pos.x /= num;
            pos.y /= num2;
            OpenGL.glTranslatef(-pos.x, -pos.y, 0f);
            back.updateWithCameraPos(pos);
            back.draw();
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (mapHeight > FrameworkTypes.SCREEN_HEIGHT)
            {
                float num3 = 2f;
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                int num4 = ctrrootController.getPack();
                Texture2D texture = Application.getTexture(188 + num4 * 2);
                int num5 = ((num4 == 0 || num4 == 9) ? 1 : 2);
                float y = texture.quadOffsets[num5].y;
                Rectangle rectangle = texture.quadRects[num5];
                rectangle.y += num3;
                rectangle.h -= num3 * 2f;
                GLDrawer.drawImagePart(texture, rectangle, 0f, y + num3);
            }
            if (mapWidth > FrameworkTypes.SCREEN_WIDTH)
            {
                float num6 = 2f;
                CTRRootController ctrrootController2 = (CTRRootController)Application.sharedRootController();
                Texture2D texture2 = Application.getTexture(188 + ctrrootController2.getPack() * 2);
                float x = texture2.quadOffsets[1].x;
                Rectangle rectangle2 = texture2.quadRects[1];
                rectangle2.x += num6;
                rectangle2.w -= num6 * 2f;
                GLDrawer.drawImagePart(texture2, rectangle2, x + num6, 0f);
            }
            if (earthAnims != null)
            {
                for (int i = 0; i < earthAnims.Count; i++)
                {
                    Image image = earthAnims[i];
                    if (image != null)
                    {
                        image.draw();
                    }
                }
            }
            OpenGL.glPopMatrix();
            camera.applyCameraTransformation();
            pollenDrawer.draw();
            if (gravityButton != null)
            {
                gravityButton.draw();
            }
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            support.draw();
            if (target.visible)
            {
                target.draw();
            }
            if (targetIdle.visible)
            {
                targetIdle.draw();
            }
            if (tutorials != null)
            {
                for (int j = 0; j < tutorials.Count; j++)
                {
                    Text text = tutorials[j];
                    if (text != null)
                    {
                        text.draw();
                    }
                }
            }
            if (tutorialImages != null)
            {
                for (int k = 0; k < tutorialImages.Count; k++)
                {
                    GameObject gameObject = tutorialImages[k];
                    if (gameObject != null)
                    {
                        gameObject.draw();
                    }
                }
            }
            if (razors != null)
            {
                for (int l = 0; l < razors.Count; l++)
                {
                    Razor razor = razors[l];
                    if (razor != null)
                    {
                        razor.draw();
                    }
                }
            }
            if (rotatedCircles != null)
            {
                for (int m = 0; m < rotatedCircles.Count; m++)
                {
                    if (rotatedCircles[m] != null)
                    {
                        rotatedCircles[m].draw();
                    }
                }
            }
            if (bubbles != null)
            {
                for (int n = 0; n < bubbles.Count; n++)
                {
                    GameObject gameObject2 = bubbles[n];
                    if (gameObject2 != null)
                    {
                        gameObject2.draw();
                    }
                }
            }
            if (pumps != null)
            {
                for (int num7 = 0; num7 < pumps.Count; num7++)
                {
                    GameObject gameObject3 = pumps[num7];
                    if (gameObject3 != null)
                    {
                        gameObject3.draw();
                    }
                }
            }
            if (spikes != null)
            {
                for (int num8 = 0; num8 < spikes.Count; num8++)
                {
                    Spikes spikes = this.spikes[num8];
                    if (spikes != null)
                    {
                        spikes.draw();
                    }
                }
            }
            if (bouncers != null)
            {
                for (int num9 = 0; num9 < bouncers.Count; num9++)
                {
                    Bouncer bouncer = bouncers[num9];
                    if (bouncer != null)
                    {
                        bouncer.draw();
                    }
                }
            }
            if (socks != null)
            {
                for (int num10 = 0; num10 < socks.Count; num10++)
                {
                    Sock sock = socks[num10];
                    if (sock != null)
                    {
                        sock.y -= 25f;
                    }
                    sock.draw();
                    sock.y += 25f;
                }
            }
            if (tubes != null)
            {
                foreach (SteamTube steamTube in tubes)
                {
                    if (steamTube != null)
                    {
                        steamTube.drawBack();
                    }
                }
            }
            List<Lantern> allLanterns = Lantern.getAllLanterns();
            foreach (Lantern lantern in allLanterns)
            {
                lantern.draw();
            }
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (ghosts != null)
            {
                foreach (Ghost ghost in ghosts)
                {
                    if (ghost != null)
                    {
                        ghost.draw();
                    }
                }
            }
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (bungees != null)
            {
                for (int num11 = 0; num11 < bungees.Count; num11++)
                {
                    Grab grab = bungees[num11];
                    if (grab != null)
                    {
                        grab.drawBack();
                    }
                }
            }
            if (bungees != null)
            {
                for (int num12 = 0; num12 < bungees.Count; num12++)
                {
                    Grab grab2 = bungees[num12];
                    if (grab2 != null)
                    {
                        grab2.draw();
                    }
                }
            }
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            if (stars != null)
            {
                for (int num13 = 0; num13 < stars.Count; num13++)
                {
                    GameObject gameObject4 = stars[num13];
                    if (gameObject4 != null)
                    {
                        gameObject4.draw();
                    }
                }
            }
            if (!noCandy && targetSock == null)
            {
                if (!isCandyInLantern)
                {
                    candy.x = star.pos.x;
                    candy.y = star.pos.y;
                }
                candy.draw();
                if (candyBlink.getCurrentTimeline() != null && !isCandyInLantern)
                {
                    OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE);
                    candyBlink.draw();
                    OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                }
            }
            if (twoParts != 2)
            {
                if (!noCandyL)
                {
                    candyL.x = starL.pos.x;
                    candyL.y = starL.pos.y;
                    candyL.draw();
                }
                if (!noCandyR)
                {
                    candyR.x = starR.pos.x;
                    candyR.y = starR.pos.y;
                    candyR.draw();
                }
            }
            if (tubes != null)
            {
                foreach (SteamTube steamTube2 in tubes)
                {
                    if (steamTube2 != null)
                    {
                        steamTube2.drawFront();
                    }
                }
            }
            if (bungees != null)
            {
                for (int num14 = 0; num14 < bungees.Count; num14++)
                {
                    Grab grab3 = bungees[num14];
                    if (grab3 != null && grab3.hasSpider)
                    {
                        grab3.drawSpider();
                    }
                }
            }
            aniPool.draw();
            bool flag = nightLevel;
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glDisable(0);
            OpenGL.SetWhiteColor();
            drawCuts();
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            camera.cancelCameraTransformation();
            staticAniPool.draw();
            if (nightLevel)
            {
                OpenGL.glDisable(4);
            }
            base.postDraw();
        }

        // Token: 0x06000809 RID: 2057 RVA: 0x0004615C File Offset: 0x0004435C
        public override void dealloc()
        {
            for (int i = 0; i < 5; i++)
            {
                fingerCuts[i].Clear();
            }
            NSObject.NSREL(dd);
            NSObject.NSREL(camera);
            NSObject.NSREL(back);
            base.dealloc();
        }

        // Token: 0x0600080A RID: 2058 RVA: 0x000461AC File Offset: 0x000443AC
        public virtual void popBubbleAtXY(float bx, float by)
        {
            CTRSoundMgr._playSound(22);
            Animation animation = Animation.Animation_createWithResID(121);
            animation.doRestoreCutTransparency();
            animation.x = bx;
            animation.y = by;
            animation.anchor = 18;
            int num = animation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 11);
            animation.getTimeline(num).delegateTimelineDelegate = aniPool;
            animation.playTimeline(0);
            aniPool.addChild(animation);
        }

        // Token: 0x0600080B RID: 2059 RVA: 0x0004621C File Offset: 0x0004441C
        public virtual void popCandyBubble(bool left)
        {
            if (twoParts == 2)
            {
                if (ghosts != null)
                {
                    foreach (Ghost ghost in ghosts)
                    {
                        if (ghost != null)
                        {
                            if (ghost.bubble == candyBubble)
                            {
                                ghost.cyclingEnabled = true;
                                ghost.resetToState(1);
                            }
                            if (shouldRestoreSecondGhost && ghost.bubble == candyBubbleR)
                            {
                                ghost.cyclingEnabled = true;
                                ghost.resetToState(1);
                                candyBubbleR = null;
                                shouldRestoreSecondGhost = false;
                            }
                        }
                    }
                }
                candyBubble = null;
                candyBubbleAnimation.visible = false;
                if (isCandyInGhostBubbleAnimationLoaded)
                {
                    candyGhostBubbleAnimation.visible = false;
                }
                popBubbleAtXY(candy.x, candy.y);
                return;
            }
            if (left)
            {
                if (ghosts != null)
                {
                    foreach (Ghost ghost2 in ghosts)
                    {
                        if (ghost2 != null && ghost2.bubble == candyBubbleL)
                        {
                            ghost2.cyclingEnabled = true;
                            ghost2.resetToState(1);
                        }
                    }
                }
                candyBubbleL = null;
                candyBubbleAnimationL.visible = false;
                if (isCandyInGhostBubbleAnimationLeftLoaded)
                {
                    candyGhostBubbleAnimationL.visible = false;
                }
                popBubbleAtXY(candyL.x, candyL.y);
                return;
            }
            if (ghosts != null)
            {
                foreach (Ghost ghost3 in ghosts)
                {
                    if (ghost3 != null && ghost3.bubble == candyBubbleR)
                    {
                        ghost3.cyclingEnabled = true;
                        ghost3.resetToState(1);
                    }
                }
            }
            candyBubbleR = null;
            candyBubbleAnimationR.visible = false;
            if (isCandyInGhostBubbleAnimationRightLoaded)
            {
                candyGhostBubbleAnimationR.visible = false;
            }
            popBubbleAtXY(candyR.x, candyR.y);
        }

        // Token: 0x0600080C RID: 2060 RVA: 0x00046468 File Offset: 0x00044668
        public virtual void loadNextMap()
        {
            dd.cancelAllDispatches();
            initialCameraToStarDistance = -1f;
            animateRestartDim = false;
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            if (ctrrootController.isPicker())
            {
                xmlLoaderFinishedWithfromwithSuccess(XMLNode.parseXML("mappicker://next"), NSObject.NSS("mappicker://next"), true);
                return;
            }
            int num = ctrrootController.getPack();
            int num2 = ctrrootController.getLevel();
            if (num2 < CTRPreferences.getLevelsInPackCount() - 1)
            {
                ctrrootController.setLevel(++num2);
                ctrrootController.setMapName(LevelsList.LEVEL_NAMES[num, num2]);
                xmlLoaderFinishedWithfromwithSuccess(XMLNode.parseXML("maps/" + LevelsList.LEVEL_NAMES[num, num2].ToString()), NSObject.NSS("maps/" + LevelsList.LEVEL_NAMES[num, num2].ToString()), true);
            }
        }

        // Token: 0x0600080D RID: 2061 RVA: 0x00046540 File Offset: 0x00044740
        public virtual void reload()
        {
            dd.cancelAllDispatches();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            if (ctrrootController.isPicker())
            {
                xmlLoaderFinishedWithfromwithSuccess(XMLNode.parseXML("mappicker://reload"), NSObject.NSS("mappicker://reload"), true);
                return;
            }
            int num = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            xmlLoaderFinishedWithfromwithSuccess(XMLNode.parseXML("maps/" + LevelsList.LEVEL_NAMES[num, level].ToString()), NSObject.NSS("maps/" + LevelsList.LEVEL_NAMES[num, level].ToString()), true);
        }

        // Token: 0x0600080E RID: 2062 RVA: 0x000465DC File Offset: 0x000447DC
        public virtual bool touchDownXYIndex(float tx, float ty, int ti)
        {
            if (ignoreTouches)
            {
                if (camera.type == CAMERA_TYPE.CAMERA_SPEED_PIXELS)
                {
                    fastenCamera = true;
                }
                return true;
            }
            if (ti >= 5)
            {
                return true;
            }
            if (gravityButton != null)
            {
                Button button = (Button)gravityButton.getChild(gravityButton.on() ? 1 : 0);
                if (button.isInTouchZoneXYforTouchDown(tx + camera.pos.x, ty + camera.pos.y, true))
                {
                    gravityTouchDown = ti;
                }
            }
            Vector vector = MathHelper.vect(tx, ty);
            prevStartPos[ti] = (startPos[ti] = vector);
            if (candyBubble != null && handleBubbleTouchXY(star, tx, ty))
            {
                return true;
            }
            if (twoParts != 2)
            {
                if (candyBubbleL != null && handleBubbleTouchXY(starL, tx, ty))
                {
                    return true;
                }
                if (candyBubbleR != null && handleBubbleTouchXY(starR, tx, ty))
                {
                    return true;
                }
            }
            int i = 0;
            int count = spikes.Count;
            while (i < count)
            {
                Spikes spikes = this.spikes[i];
                if (spikes.rotateButton != null && spikes.touchIndex == -1 && spikes.rotateButton.onTouchDownXY(tx + camera.pos.x, ty + camera.pos.y))
                {
                    spikes.touchIndex = ti;
                    return true;
                }
                i++;
            }
            bool flag = false;
            int count2 = pumps.Count;
            for (int j = 0; j < count2; j++)
            {
                Pump pump = pumps[j];
                if (GameObject.pointInObject(MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y), pump))
                {
                    pump.pumpTouchTimer = 0.05f;
                    pump.pumpTouch = ti;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return true;
            }
            if (!dragging[ti])
            {
                dragging[ti] = true;
            }
            RotatedCircle rotatedCircle = null;
            bool flag2 = false;
            bool flag3 = false;
            for (int k = 0; k < rotatedCircles.Count; k++)
            {
                RotatedCircle rotatedCircle2 = rotatedCircles[k];
                float num = MathHelper.vectDistance(MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y), rotatedCircle2.handle1);
                float num2 = MathHelper.vectDistance(MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y), rotatedCircle2.handle2);
                if ((num < 30f && !rotatedCircle2.hasOneHandle()) || num2 < 30f)
                {
                    for (int l = 0; l < rotatedCircles.Count; l++)
                    {
                        RotatedCircle rotatedCircle3 = rotatedCircles[l];
                        if (rotatedCircles.IndexOf(rotatedCircle3) > rotatedCircles.IndexOf(rotatedCircle2))
                        {
                            float num3 = MathHelper.vectDistance(MathHelper.vect(rotatedCircle3.x, rotatedCircle3.y), MathHelper.vect(rotatedCircle2.x, rotatedCircle2.y));
                            if (num3 + rotatedCircle3.sizeInPixels <= rotatedCircle2.sizeInPixels)
                            {
                                flag2 = true;
                            }
                            if (num3 <= rotatedCircle2.sizeInPixels + rotatedCircle3.sizeInPixels)
                            {
                                flag3 = true;
                            }
                        }
                    }
                    rotatedCircle2.lastTouch = MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y);
                    rotatedCircle2.operating = ti;
                    if (num < 30f)
                    {
                        rotatedCircle2.setIsLeftControllerActive(true);
                    }
                    if (num2 < 30f)
                    {
                        rotatedCircle2.setIsRightControllerActive(true);
                    }
                    rotatedCircle = rotatedCircle2;
                    break;
                }
            }
            if (rotatedCircles.IndexOf(rotatedCircle) != rotatedCircles.Count - 1 && flag3 && !flag2)
            {
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(1);
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                timeline2.delegateTimelineDelegate = this;
                RotatedCircle rotatedCircle4 = (RotatedCircle)rotatedCircle.copy();
                rotatedCircle4.addTimeline(timeline2);
                rotatedCircle4.playTimeline(0);
                rotatedCircle.addTimeline(timeline);
                rotatedCircle.playTimeline(0);
                rotatedCircles[rotatedCircles.IndexOf(rotatedCircle)] = rotatedCircle4;
                rotatedCircles.Add(rotatedCircle);
            }
            if (bungees != null)
            {
                for (int m = 0; m < bungees.Count; m++)
                {
                    Grab grab = bungees[m];
                    if (grab != null && grab.wheel && MathHelper.pointInRect(tx + camera.pos.x, ty + camera.pos.y, grab.x - 45f, grab.y - 45f, 90f, 90f))
                    {
                        grab.handleWheelTouch(MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y));
                        grab.wheelOperating = ti;
                    }
                    if ((double)grab.moveLength > 0.0 && MathHelper.pointInRect(tx + camera.pos.x, ty + camera.pos.y, grab.x - 40f, grab.y - 40f, 80f, 80f))
                    {
                        grab.moverDragging = ti;
                    }
                }
            }
            if (ghosts != null)
            {
                foreach (Ghost ghost in ghosts)
                {
                    if (ghost != null && ghost.onTouchDownXY(tx + camera.pos.x, ty + camera.pos.y))
                    {
                        return true;
                    }
                }
            }
            if (tubes != null)
            {
                foreach (SteamTube steamTube in tubes)
                {
                    if (steamTube != null && steamTube.onTouchDownXY(tx + camera.pos.x, ty + camera.pos.y))
                    {
                        return true;
                    }
                }
            }
            List<Lantern> allLanterns = Lantern.getAllLanterns();
            foreach (Lantern lantern in allLanterns)
            {
                if (restartState == -1 && lantern != null && lantern.onTouchDownXY(tx + camera.pos.x, ty + camera.pos.y))
                {
                    dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_revealCandyFromLantern), null, 0.1);
                    return true;
                }
            }
            return true;
        }

        // Token: 0x0600080F RID: 2063 RVA: 0x00046DA4 File Offset: 0x00044FA4
        public virtual bool touchUpXYIndex(float tx, float ty, int ti)
        {
            if (ignoreTouches)
            {
                return true;
            }
            dragging[ti] = false;
            if (ti >= 5)
            {
                return true;
            }
            if (gravityButton != null && gravityTouchDown == ti)
            {
                Button button = (Button)gravityButton.getChild(gravityButton.on() ? 1 : 0);
                if (button.isInTouchZoneXYforTouchDown(tx + camera.pos.x, ty + camera.pos.y, true))
                {
                    gravityButton.toggle();
                    onButtonPressed(0);
                }
                gravityTouchDown = -1;
            }
            int i = 0;
            int count = spikes.Count;
            while (i < count)
            {
                Spikes spikes = this.spikes[i];
                if (spikes.rotateButton != null && spikes.touchIndex == ti)
                {
                    spikes.touchIndex = -1;
                    if (spikes.rotateButton.onTouchUpXY(tx + camera.pos.x, ty + camera.pos.y))
                    {
                        return true;
                    }
                }
                i++;
            }
            if (rotatedCircles != null)
            {
                for (int j = 0; j < rotatedCircles.Count; j++)
                {
                    RotatedCircle rotatedCircle = rotatedCircles[j];
                    if (rotatedCircle != null && rotatedCircle.operating == ti)
                    {
                        rotatedCircle.operating = -1;
                        rotatedCircle.soundPlaying = -1;
                        rotatedCircle.setIsLeftControllerActive(false);
                        rotatedCircle.setIsRightControllerActive(false);
                    }
                }
            }
            if (bungees != null)
            {
                for (int k = 0; k < bungees.Count; k++)
                {
                    Grab grab = bungees[k];
                    if (grab != null && grab.wheel && grab.wheelOperating == ti)
                    {
                        grab.wheelOperating = -1;
                    }
                    if ((double)grab.moveLength > 0.0 && grab.moverDragging == ti)
                    {
                        grab.moverDragging = -1;
                    }
                }
            }
            return true;
        }

        // Token: 0x06000810 RID: 2064 RVA: 0x00046F88 File Offset: 0x00045188
        public virtual bool touchMoveXYIndex(float tx, float ty, int ti)
        {
            if (ignoreTouches)
            {
                return true;
            }
            Vector vector = MathHelper.vect(tx, ty);
            if (ti >= 5)
            {
                return true;
            }
            slastTouch = vector;
            int i = 0;
            int count = spikes.Count;
            while (i < count)
            {
                Spikes spikes = this.spikes[i];
                if (spikes.rotateButton != null && spikes.touchIndex == ti)
                {
                    if (spikes.rotateButton.onTouchMoveXY(tx + camera.pos.x, ty + camera.pos.y))
                    {
                        return true;
                    }
                    spikes.touchIndex = -1;
                }
                i++;
            }
            if (pumps != null)
            {
                for (int j = 0; j < pumps.Count; j++)
                {
                    Pump pump = pumps[j];
                    if (pump != null && pump.pumpTouch == ti && (double)pump.pumpTouchTimer != 0.0 && (double)MathHelper.vectDistance(startPos[ti], vector) > 10.0)
                    {
                        pump.pumpTouchTimer = 0f;
                    }
                }
            }
            if (rotatedCircles != null)
            {
                for (int k = 0; k < rotatedCircles.Count; k++)
                {
                    RotatedCircle rotatedCircle = rotatedCircles[k];
                    if (rotatedCircle != null && rotatedCircle.operating == ti)
                    {
                        Vector vector2 = MathHelper.vect(rotatedCircle.x, rotatedCircle.y);
                        Vector vector3 = MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y);
                        Vector vector4 = MathHelper.vectSub(rotatedCircle.lastTouch, vector2);
                        Vector vector5 = MathHelper.vectSub(vector3, vector2);
                        float num = MathHelper.vectAngleNormalized(vector5) - MathHelper.vectAngleNormalized(vector4);
                        float num2 = MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation);
                        rotatedCircle.rotation += MathHelper.RADIANS_TO_DEGREES(num);
                        float num3 = MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation);
                        num3 = GameScene.FBOUND_PI(num3);
                        rotatedCircle.handle1 = MathHelper.vectRotateAround(rotatedCircle.inithanlde1, (double)num3, rotatedCircle.x, rotatedCircle.y);
                        rotatedCircle.handle2 = MathHelper.vectRotateAround(rotatedCircle.inithanlde2, (double)num3, rotatedCircle.x, rotatedCircle.y);
                        int num4 = ((num > 0f) ? 56 : 57);
                        if ((double)Math.Abs(num) < 0.07)
                        {
                            num4 = -1;
                        }
                        if (rotatedCircle.soundPlaying != num4 && num4 != -1)
                        {
                            CTRSoundMgr._playSound(num4);
                            rotatedCircle.soundPlaying = num4;
                        }
                        for (int l = 0; l < bungees.Count; l++)
                        {
                            Grab grab = bungees[l];
                            if (MathHelper.vectDistance(MathHelper.vect(grab.x, grab.y), MathHelper.vect(rotatedCircle.x, rotatedCircle.y)) <= rotatedCircle.sizeInPixels + 5f)
                            {
                                if (grab.initial_rotatedCircle != rotatedCircle)
                                {
                                    grab.initial_x = grab.x;
                                    grab.initial_y = grab.y;
                                    grab.initial_rotatedCircle = rotatedCircle;
                                    grab.initial_rotation = num2;
                                }
                                float num5 = MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation) - grab.initial_rotation;
                                num5 = GameScene.FBOUND_PI(num5);
                                Vector vector6 = MathHelper.vectRotateAround(MathHelper.vect(grab.initial_x, grab.initial_y), (double)num5, rotatedCircle.x, rotatedCircle.y);
                                grab.x = vector6.x;
                                grab.y = vector6.y;
                                if (grab.rope != null)
                                {
                                    grab.rope.bungeeAnchor.pos = MathHelper.vect(grab.x, grab.y);
                                    grab.rope.bungeeAnchor.pin = grab.rope.bungeeAnchor.pos;
                                }
                                if (grab.radius != -1f)
                                {
                                    grab.reCalcCircle();
                                }
                            }
                        }
                        for (int m = 0; m < pumps.Count; m++)
                        {
                            Pump pump2 = pumps[m];
                            if (MathHelper.vectDistance(MathHelper.vect(pump2.x, pump2.y), MathHelper.vect(rotatedCircle.x, rotatedCircle.y)) <= rotatedCircle.sizeInPixels + 5f)
                            {
                                if (pump2.initial_rotatedCircle != rotatedCircle)
                                {
                                    pump2.initial_x = pump2.x;
                                    pump2.initial_y = pump2.y;
                                    pump2.initial_rotatedCircle = rotatedCircle;
                                    pump2.initial_rotation = num2;
                                }
                                float num6 = MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation) - pump2.initial_rotation;
                                num6 = GameScene.FBOUND_PI(num6);
                                Vector vector7 = MathHelper.vectRotateAround(MathHelper.vect(pump2.initial_x, pump2.initial_y), (double)num6, rotatedCircle.x, rotatedCircle.y);
                                pump2.x = vector7.x;
                                pump2.y = vector7.y;
                                pump2.rotation += MathHelper.RADIANS_TO_DEGREES(num);
                                pump2.updateRotation();
                            }
                        }
                        for (int n = 0; n < bubbles.Count; n++)
                        {
                            Bubble bubble = bubbles[n];
                            if (MathHelper.vectDistance(MathHelper.vect(bubble.x, bubble.y), MathHelper.vect(rotatedCircle.x, rotatedCircle.y)) <= rotatedCircle.sizeInPixels + 10f && bubble != candyBubble && bubble != candyBubbleR && bubble != candyBubbleL)
                            {
                                if (bubble.initial_rotatedCircle != rotatedCircle)
                                {
                                    bubble.initial_x = bubble.x;
                                    bubble.initial_y = bubble.y;
                                    bubble.initial_rotatedCircle = rotatedCircle;
                                    bubble.initial_rotation = num2;
                                }
                                float num7 = MathHelper.DEGREES_TO_RADIANS(rotatedCircle.rotation) - bubble.initial_rotation;
                                num7 = GameScene.FBOUND_PI(num7);
                                Vector vector8 = MathHelper.vectRotateAround(MathHelper.vect(bubble.initial_x, bubble.initial_y), (double)num7, rotatedCircle.x, rotatedCircle.y);
                                bubble.x = vector8.x;
                                bubble.y = vector8.y;
                            }
                        }
                        if (MathHelper.pointInRect(target.x, target.y, rotatedCircle.x - rotatedCircle.size, rotatedCircle.y - rotatedCircle.size, 2f * rotatedCircle.size, 2f * rotatedCircle.size))
                        {
                            Vector vector9 = MathHelper.vectRotateAround(MathHelper.vect(target.x, target.y), (double)num, rotatedCircle.x, rotatedCircle.y);
                            target.x = vector9.x;
                            target.y = vector9.y;
                        }
                        rotatedCircle.lastTouch = vector3;
                        return true;
                    }
                }
            }
            int count2 = bungees.Count;
            for (int num8 = 0; num8 < count2; num8++)
            {
                Grab grab2 = bungees[num8];
                if (grab2 != null)
                {
                    if (grab2.wheel && grab2.wheelOperating == ti)
                    {
                        grab2.handleWheelRotate(MathHelper.vect(tx + camera.pos.x, ty + camera.pos.y));
                        return true;
                    }
                    if ((double)grab2.moveLength > 0.0 && grab2.moverDragging == ti)
                    {
                        if (grab2.moveVertical)
                        {
                            grab2.y = MathHelper.FIT_TO_BOUNDARIES(ty + camera.pos.y, grab2.minMoveValue, grab2.maxMoveValue);
                        }
                        else
                        {
                            grab2.x = MathHelper.FIT_TO_BOUNDARIES(tx + camera.pos.x, grab2.minMoveValue, grab2.maxMoveValue);
                        }
                        if (grab2.rope != null)
                        {
                            grab2.rope.bungeeAnchor.pos = MathHelper.vect(grab2.x, grab2.y);
                            grab2.rope.bungeeAnchor.pin = grab2.rope.bungeeAnchor.pos;
                        }
                        if (grab2.radius != -1f)
                        {
                            grab2.reCalcCircle();
                        }
                        return true;
                    }
                }
            }
            if (dragging[ti])
            {
                MathHelper.vectAdd(prevStartPos[ti], camera.pos);
                Vector vector10 = MathHelper.vectAdd(startPos[ti], camera.pos);
                Vector vector11 = MathHelper.vectAdd(MathHelper.vect(tx, ty), camera.pos);
                float num9 = MathHelper.vectDistance(vector10, vector11);
                GameScene.FingerCut fingerCut = new GameScene.FingerCut();
                fingerCut.start = vector10;
                fingerCut.end = vector11;
                fingerCut.startSize = 5f;
                fingerCut.endSize = 5f;
                fingerCut.c = RGBAColor.whiteRGBA;
                fingerCuts[ti].Add(fingerCut);
                int num10 = 0;
                List<GameScene.FingerCut> list = fingerCuts[ti];
                if (num9 > 0f && list != null)
                {
                    for (int num11 = 0; num11 < list.Count; num11++)
                    {
                        GameScene.FingerCut fingerCut2 = list[num11];
                        if (fingerCut2 != null)
                        {
                            num10 += cutWithRazorOrLine1Line2Immediate(null, fingerCut2.start, fingerCut2.end, false);
                        }
                    }
                }
                if (num10 > 0)
                {
                    freezeCamera = false;
                    if (ropesCutAtOnce > 0 && (double)ropeAtOnceTimer > 0.0)
                    {
                        ropesCutAtOnce += num10;
                    }
                    else
                    {
                        ropesCutAtOnce = num10;
                    }
                    ropeAtOnceTimer = 0.05f;
                    ropesCutFromLevelStart += num10;
                    int num12 = Preferences._getIntForKey("PREFS_ROPES_CUT") + 1;
                    Preferences._setIntforKey(num12, "PREFS_ROPES_CUT", false);
                    if (num12 >= 100)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acRopeCutter"));
                    }
                    if (ropesCutAtOnce >= 3 && ropesCutAtOnce < 5)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acQuickFinger"));
                    }
                    else if (ropesCutAtOnce >= 5)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acMasterFinger"));
                    }
                    if (num12 >= 800)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acRopeCutterManiac"));
                    }
                    if (num12 >= 2000)
                    {
                        CTRRootController.postAchievementName(NSObject.NSS("acUltimateRopeCutter"));
                    }
                }
                prevStartPos[ti] = startPos[ti];
                startPos[ti] = vector;
            }
            return true;
        }

        // Token: 0x06000811 RID: 2065 RVA: 0x00047A5B File Offset: 0x00045C5B
        public virtual void restart()
        {
            hide();
            show();
        }

        // Token: 0x06000812 RID: 2066 RVA: 0x00047A6C File Offset: 0x00045C6C
        public virtual void spiderBusted(Grab g)
        {
            int num = Preferences._getIntForKey("PREFS_SPIDERS_BUSTED") + 1;
            Preferences._setIntforKey(num, "PREFS_SPIDERS_BUSTED", false);
            if (num >= 40)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acSpiderBuster"));
            }
            if (num >= 200)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acSpiderTamer"));
            }
            CTRSoundMgr._playSound(44);
            g.hasSpider = false;
            Image image = Image.Image_createWithResIDQuad(94, 11);
            image.doRestoreCutTransparency();
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            if (gravityButton != null && !gravityNormal)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)g.spider.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)g.spider.y + 50.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)(g.spider.y - FrameworkTypes.SCREEN_HEIGHT), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 1.0));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)g.spider.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)g.spider.y - 50.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
                timeline.addKeyFrame(KeyFrame.makePos((double)g.spider.x, (double)(g.spider.y + FrameworkTypes.SCREEN_HEIGHT), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 1.0));
            }
            timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation(MathHelper.RND_RANGE(-120, 120), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
            image.addTimelinewithID(timeline, 0);
            image.playTimeline(0);
            image.x = g.spider.x;
            image.y = g.spider.y;
            image.anchor = 18;
            timeline.delegateTimelineDelegate = aniPool;
            aniPool.addChild(image);
        }

        // Token: 0x06000813 RID: 2067 RVA: 0x00047CB0 File Offset: 0x00045EB0
        public virtual void spiderWon(Grab sg)
        {
            CTRSoundMgr._playSound(45);
            int count = bungees.Count;
            for (int i = 0; i < count; i++)
            {
                Grab grab = bungees[i];
                Bungee rope = grab.rope;
                if (rope != null && rope.tail == star)
                {
                    if (rope.cut == -1)
                    {
                        rope.setCut(rope.parts.Count - 2);
                        rope.forceWhite = false;
                    }
                    if (grab.hasSpider && grab.spiderActive && sg != grab)
                    {
                        spiderBusted(grab);
                    }
                }
            }
            int num = Preferences._getIntForKey("PREFS_SPIDERS_WON") + 1;
            Preferences._setIntforKey(num, "PREFS_SPIDERS_WON", false);
            if (num >= 100)
            {
                CTRRootController.postAchievementName(NSObject.NSS("acSpiderLover"));
            }
            sg.hasSpider = false;
            spiderTookCandy = true;
            noCandy = true;
            Image image = Image.Image_createWithResIDQuad(94, 12);
            image.doRestoreCutTransparency();
            candy.anchor = (candy.parentAnchor = 18);
            candy.x = 0f;
            candy.y = -5f;
            image.addChild(candy);
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
            if (gravityButton != null && !gravityNormal)
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)sg.spider.y - 10.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)sg.spider.y + 70.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)(sg.spider.y - FrameworkTypes.SCREEN_HEIGHT), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 1.0));
            }
            else
            {
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)sg.spider.y - 10.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)sg.spider.y - 70.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3));
                timeline.addKeyFrame(KeyFrame.makePos((double)sg.spider.x, (double)(sg.spider.y + FrameworkTypes.SCREEN_HEIGHT), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 1.0));
            }
            image.addTimelinewithID(timeline, 0);
            image.playTimeline(0);
            image.x = sg.spider.x;
            image.y = sg.spider.y - 10f;
            image.anchor = 18;
            timeline.delegateTimelineDelegate = aniPool;
            aniPool.addChild(image);
            if (restartState != 0)
            {
                dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_gameLost), null, 2f);
            }
        }

        // Token: 0x06000814 RID: 2068 RVA: 0x00047FD8 File Offset: 0x000461D8
        public virtual void operatePump(Pump p)
        {
            p.playTimeline(0);
            CTRSoundMgr._playSound(MathHelper.RND_RANGE(39, 42));
            Image image = Image.Image_createWithResID(143);
            PumpDirt pumpDirt = new PumpDirt().initWithTotalParticlesAngleandImageGrid(5, MathHelper.RADIANS_TO_DEGREES((float)p.angle) - 90f, image);
            pumpDirt.particlesDelegate = new Particles.ParticlesFinished(aniPool.particlesFinished);
            Vector vector = MathHelper.vect(p.x + 25f, p.y);
            vector = MathHelper.vectRotateAround(vector, p.angle - 1.5707963267948966, p.x, p.y);
            pumpDirt.x = vector.x;
            pumpDirt.y = vector.y;
            pumpDirt.startSystem(5);
            aniPool.addChild(pumpDirt);
            if (!noCandy)
            {
                handlePumpFlowPtSkin(p, star, candy);
            }
            if (twoParts != 2)
            {
                if (!noCandyL)
                {
                    handlePumpFlowPtSkin(p, starL, candyL);
                }
                if (!noCandyR)
                {
                    handlePumpFlowPtSkin(p, starR, candyR);
                }
            }
        }

        // Token: 0x06000815 RID: 2069 RVA: 0x00048100 File Offset: 0x00046300
        public virtual int cutWithRazorOrLine1Line2Immediate(Razor r, Vector v1, Vector v2, bool im)
        {
            int num = 0;
            for (int i = 0; i < bungees.Count; i++)
            {
                Grab grab = bungees[i];
                Bungee rope = grab.rope;
                if (rope != null && rope.cut == -1)
                {
                    int j = 0;
                    while (j < rope.parts.Count - 1)
                    {
                        ConstraintedPoint constraintedPoint = rope.parts[j];
                        ConstraintedPoint constraintedPoint2 = rope.parts[j + 1];
                        bool flag = false;
                        if (r != null)
                        {
                            if (constraintedPoint.prevPos.x != 2.1474836E+09f)
                            {
                                float num2 = minOf4(constraintedPoint.pos.x, constraintedPoint.prevPos.x, constraintedPoint2.pos.x, constraintedPoint2.prevPos.x);
                                float num3 = minOf4(constraintedPoint.pos.y, constraintedPoint.prevPos.y, constraintedPoint2.pos.y, constraintedPoint2.prevPos.y);
                                float num4 = maxOf4(constraintedPoint.pos.x, constraintedPoint.prevPos.x, constraintedPoint2.pos.x, constraintedPoint2.prevPos.x);
                                float num5 = maxOf4(constraintedPoint.pos.y, constraintedPoint.prevPos.y, constraintedPoint2.pos.y, constraintedPoint2.prevPos.y);
                                flag = MathHelper.rectInRect(num2, num3, num4, num5, r.drawX, r.drawY, r.drawX + (float)r.width, r.drawY + (float)r.height);
                            }
                        }
                        else
                        {
                            flag = (!grab.wheel || !MathHelper.lineInRect(v1.x, v1.y, v2.x, v2.y, grab.x - 30f, grab.y - 30f, 60f, 60f)) && base.lineInLine(v1.x, v1.y, v2.x, v2.y, constraintedPoint.pos.x, constraintedPoint.pos.y, constraintedPoint2.pos.x, constraintedPoint2.pos.y);
                        }
                        if (flag)
                        {
                            num++;
                            if (grab.hasSpider && grab.spiderActive)
                            {
                                spiderBusted(grab);
                            }
                            CTRSoundMgr._playSound(29 + rope.relaxed);
                            rope.setCut(j);
                            if (im)
                            {
                                rope.cutTime = 0f;
                                rope.removePart(j);
                                break;
                            }
                            break;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            return num;
        }

        // Token: 0x06000816 RID: 2070 RVA: 0x000483C4 File Offset: 0x000465C4
        public virtual void gameWon()
        {
            dd.cancelAllDispatches();
            targetIdle.disableAnimations = true;
            target.playTimeline(7);
            CTRSoundMgr._playSound(25);
            if (candyBubble != null)
            {
                popCandyBubble(false);
            }
            noCandy = true;
            candy.passTransformationsToChilds = true;
            candyMain.scaleX = (candyMain.scaleY = 1f);
            candyTop.scaleX = (candyTop.scaleY = 1f);
            if (candy.hasTimeline(0))
            {
                candy.removeTimeline(0);
            }
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makePos((double)candy.x, (double)candy.y, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makePos((double)target.x, (double)target.y + 10.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makeScale(0.71, 0.71, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
            candy.addTimelinewithID(timeline, 0);
            candy.playTimeline(0);
            timeline.delegateTimelineDelegate = aniPool;
            aniPool.addChild(candy);
            dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_gameWonDelegate), null, 2f);
            calculateScore();
            releaseAllRopes(false);
        }

        // Token: 0x06000817 RID: 2071 RVA: 0x000485C8 File Offset: 0x000467C8
        public virtual void gameLost()
        {
            dd.cancelAllDispatches();
            targetIdle.disableAnimations = true;
            target.playTimeline(6);
            CTRSoundMgr._playSound(28);
            dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(selector_animateLevelRestart), null, 1f);
            gameSceneDelegate_gameLost();
        }

        // Token: 0x06000818 RID: 2072 RVA: 0x00048628 File Offset: 0x00046828
        public virtual void releaseAllRopes(bool left)
        {
            int count = bungees.Count;
            for (int i = 0; i < count; i++)
            {
                Grab grab = bungees[i];
                if (grab != null)
                {
                    Bungee rope = grab.rope;
                    if (rope != null && (rope.tail == star || (rope.tail == starL && left) || (rope.tail == starR && !left)))
                    {
                        if (rope.cut == -1)
                        {
                            rope.setCut(rope.parts.Count - 2);
                        }
                        else
                        {
                            rope.hideTailParts = true;
                        }
                        if (grab.hasSpider && grab.spiderActive)
                        {
                            spiderBusted(grab);
                        }
                    }
                }
            }
        }

        // Token: 0x06000819 RID: 2073 RVA: 0x000486DC File Offset: 0x000468DC
        public virtual void calculateScore()
        {
            timeBonus = (int)Math.Max(0f, 30f - time) * 100;
            timeBonus /= 10;
            timeBonus *= 10;
            starBonus = 1000 * starsCollected;
            score = timeBonus + starBonus;
        }

        // Token: 0x0600081A RID: 2074 RVA: 0x0004874C File Offset: 0x0004694C
        public virtual void doCandyBlink()
        {
            candyBlink.playTimeline(0);
        }

        // Token: 0x0600081B RID: 2075 RVA: 0x0004875C File Offset: 0x0004695C
        public virtual void startCamera()
        {
            if (mapWidth > FrameworkTypes.SCREEN_WIDTH || mapHeight > FrameworkTypes.SCREEN_HEIGHT)
            {
                ignoreTouches = true;
                fastenCamera = false;
                camera.type = CAMERA_TYPE.CAMERA_SPEED_PIXELS;
                camera.speed = 10f;
                cameraMoveMode = 0;
                ConstraintedPoint constraintedPoint = ((twoParts != 2) ? starL : star);
                float num;
                float num2;
                if (mapWidth > FrameworkTypes.SCREEN_WIDTH)
                {
                    if ((double)star.pos.x > (double)mapWidth / 2.0)
                    {
                        num = 0f;
                        num2 = 0f;
                    }
                    else
                    {
                        num = mapWidth - FrameworkTypes.SCREEN_WIDTH;
                        num2 = 0f;
                    }
                }
                else if ((double)constraintedPoint.pos.y > (double)mapHeight / 2.0)
                {
                    num = 0f;
                    num2 = 0f;
                }
                else
                {
                    num = 0f;
                    num2 = mapHeight - FrameworkTypes.SCREEN_HEIGHT;
                }
                float num3 = constraintedPoint.pos.x - FrameworkTypes.SCREEN_WIDTH / 2f;
                float num4 = constraintedPoint.pos.y - FrameworkTypes.SCREEN_HEIGHT / 2f;
                float num5 = MathHelper.FIT_TO_BOUNDARIES(num3, 0f, mapWidth - FrameworkTypes.SCREEN_WIDTH);
                float num6 = MathHelper.FIT_TO_BOUNDARIES(num4, 0f, mapHeight - FrameworkTypes.SCREEN_HEIGHT);
                camera.moveToXYImmediate(num, num2, true);
                initialCameraToStarDistance = MathHelper.vectDistance(camera.pos, MathHelper.vect(num5, num6));
                return;
            }
            ignoreTouches = false;
            camera.moveToXYImmediate(0f, 0f, true);
        }

        // Token: 0x0600081C RID: 2076 RVA: 0x00048914 File Offset: 0x00046B14
        public virtual void drawCuts()
        {
            for (int i = 0; i < 5; i++)
            {
                int num = fingerCuts[i].Count;
                if (num > 0)
                {
                    float num2 = 6f;
                    float num3 = 1f;
                    int num4 = 0;
                    int j = 0;
                    Vector[] array = new Vector[num + 1];
                    int num5 = 0;
                    while (j < num)
                    {
                        GameScene.FingerCut fingerCut = fingerCuts[i][j];
                        if (j == 0)
                        {
                            array[num5++] = fingerCut.start;
                        }
                        array[num5++] = fingerCut.end;
                        j++;
                    }
                    List<Vector> list = new List<Vector>();
                    Vector vector = default(Vector);
                    bool flag = true;
                    for (int k = 0; k < Enumerable.Count<Vector>(array); k++)
                    {
                        if (k == 0)
                        {
                            list.Add(array[k]);
                        }
                        else if (array[k].x != vector.x || array[k].y != vector.y)
                        {
                            list.Add(array[k]);
                            flag = false;
                        }
                        vector = array[k];
                    }
                    if (!flag)
                    {
                        array = list.ToArray();
                        num = Enumerable.Count<Vector>(array) - 1;
                        int num6 = num * 2;
                        float[] array2 = new float[num6 * 2];
                        float num7 = 1f / (float)num6;
                        float num8 = 0f;
                        int num9 = 0;
                        for (; ; )
                        {
                            if ((double)num8 > 1.0)
                            {
                                num8 = 1f;
                            }
                            Vector vector2 = GLDrawer.calcPathBezier(array, num + 1, num8);
                            if (num9 > Enumerable.Count<float>(array2) - 2)
                            {
                                break;
                            }
                            array2[num9++] = vector2.x;
                            array2[num9++] = vector2.y;
                            if ((double)num8 == 1.0)
                            {
                                break;
                            }
                            num8 += num7;
                        }
                        float num10 = num2 / (float)num6;
                        float[] array3 = new float[num6 * 4];
                        for (int l = 0; l < num6 - 1; l++)
                        {
                            float num11 = num3;
                            float num12 = ((l == num6 - 2) ? 1f : (num3 + num10));
                            Vector vector3 = MathHelper.vect(array2[l * 2], array2[l * 2 + 1]);
                            Vector vector4 = MathHelper.vect(array2[(l + 1) * 2], array2[(l + 1) * 2 + 1]);
                            Vector vector5 = MathHelper.vectSub(vector4, vector3);
                            Vector vector6 = MathHelper.vectNormalize(vector5);
                            Vector vector7 = MathHelper.vectRperp(vector6);
                            Vector vector8 = MathHelper.vectPerp(vector6);
                            if (num4 == 0)
                            {
                                Vector vector9 = MathHelper.vectAdd(vector3, MathHelper.vectMult(vector7, num11));
                                Vector vector10 = MathHelper.vectAdd(vector3, MathHelper.vectMult(vector8, num11));
                                array3[num4++] = vector10.x;
                                array3[num4++] = vector10.y;
                                array3[num4++] = vector9.x;
                                array3[num4++] = vector9.y;
                            }
                            Vector vector11 = MathHelper.vectAdd(vector4, MathHelper.vectMult(vector7, num12));
                            Vector vector12 = MathHelper.vectAdd(vector4, MathHelper.vectMult(vector8, num12));
                            array3[num4++] = vector12.x;
                            array3[num4++] = vector12.y;
                            array3[num4++] = vector11.x;
                            array3[num4++] = vector11.y;
                            num3 += num10;
                        }
                        OpenGL.SetWhiteColor();
                        OpenGL.glVertexPointer(2, 5, 0, array3);
                        OpenGL.glDrawArrays(8, 0, num4 / 2);
                    }
                }
            }
        }

        // Token: 0x0600081D RID: 2077 RVA: 0x00048C94 File Offset: 0x00046E94
        private static ToggleButton createGravityButtonWithDelegate(ButtonDelegate d)
        {
            Image image = Image.Image_createWithResIDQuad(127, 56);
            Image image2 = Image.Image_createWithResIDQuad(127, 56);
            Image image3 = Image.Image_createWithResIDQuad(127, 57);
            Image image4 = Image.Image_createWithResIDQuad(127, 57);
            ToggleButton toggleButton = new ToggleButton().initWithUpElement1DownElement1UpElement2DownElement2andID(image, image2, image3, image4, 0);
            toggleButton.setTouchIncreaseLeftRightTopBottom(10f, 10f, 10f, 10f);
            toggleButton.delegateButtonDelegate = d;
            return toggleButton;
        }

        // Token: 0x0600081E RID: 2078 RVA: 0x00048D00 File Offset: 0x00046F00
        public virtual void createEarthImageWithOffsetXY(float xs, float ys)
        {
            Image image = Image.Image_createWithResIDQuad(127, 58);
            image.anchor = 18;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation(180, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3f));
            image.addTimelinewithID(timeline, 1);
            timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeRotation(180, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
            timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3f));
            image.addTimelinewithID(timeline, 0);
            Image.setElementPositionWithQuadOffset(image, 200, 3);
            image.x += xs;
            image.y += ys;
            earthAnims.Add(image);
        }

        // Token: 0x0600081F RID: 2079 RVA: 0x00048DCB File Offset: 0x00046FCB
        public virtual void showGreeting()
        {
            target.playTimeline(11);
        }

        // Token: 0x06000820 RID: 2080 RVA: 0x00048DDC File Offset: 0x00046FDC
        public static bool shouldSkipTutorialElement(XMLNode c)
        {
            NSString @string = Application.sharedAppSettings().getString(8);
            NSString nsstring = c["locale"];
            if (@string.isEqualToString(NSObject.NSS("en")) || @string.isEqualToString(NSObject.NSS("ru")) || @string.isEqualToString(NSObject.NSS("de")) || @string.isEqualToString(NSObject.NSS("fr")) || @string.isEqualToString(NSObject.NSS("zh")) || @string.isEqualToString(NSObject.NSS("ja")) || @string.isEqualToString(NSObject.NSS("ko")) || @string.isEqualToString(NSObject.NSS("es")) || @string.isEqualToString(NSObject.NSS("it")) || @string.isEqualToString(NSObject.NSS("br")) || @string.isEqualToString(NSObject.NSS("nl")))
            {
                if (!nsstring.isEqualToString(@string))
                {
                    return true;
                }
            }
            else if (!nsstring.isEqualToString(NSObject.NSS("en")))
            {
                return true;
            }
            return false;
        }

        // Token: 0x06000821 RID: 2081 RVA: 0x00048EF0 File Offset: 0x000470F0
        public virtual void onButtonPressed(int n)
        {
            if ((double)MaterialPoint.globalGravity.y == 784.0)
            {
                MaterialPoint.globalGravity.y = -784f;
                gravityNormal = false;
                CTRSoundMgr._playSound(49);
            }
            else
            {
                MaterialPoint.globalGravity.y = 784f;
                gravityNormal = true;
                CTRSoundMgr._playSound(48);
            }
            if (earthAnims != null)
            {
                for (int i = 0; i < earthAnims.Count; i++)
                {
                    Image image = earthAnims[i];
                    if (image != null)
                    {
                        if (gravityNormal)
                        {
                            image.playTimeline(0);
                        }
                        else
                        {
                            image.playTimeline(1);
                        }
                    }
                }
            }
        }

        // Token: 0x06000822 RID: 2082 RVA: 0x00048F98 File Offset: 0x00047198
        private void operateSteamTube(SteamTube tube)
        {
            float num = 5f;
            float num2 = MathHelper.DEGREES_TO_RADIANS(tube.rotation);
            float num3 = 10f;
            float currentHeightModulated = tube.getCurrentHeightModulated();
            float num4 = 1f;
            float num5 = 17.5f;
            Vector vector = MathHelper.vect(tube.x - num3 / 2f, tube.y - currentHeightModulated - num4);
            Vector vector2 = MathHelper.vect(tube.x + num3 / 2f, tube.y - num5);
            if (twoParts == 2)
            {
                Vector vector3 = MathHelper.vect(star.pos.x, star.pos.y);
                Vector vector4 = MathHelper.vect(star.v.x, star.v.y);
                vector3 = MathHelper.vectRotateAround(vector3, (double)(-(double)num2), tube.x, tube.y);
                vector4 = MathHelper.vectRotate(vector4, (double)(-(double)num2));
                if (MathHelper.rectInRect(vector3.x - num5, vector3.y - num5 / 2f, vector3.x + num5, vector3.y + num5, vector.x, vector.y, vector2.x, vector2.y))
                {
                    foreach (Bouncer bouncer in bouncers)
                    {
                        bouncer.skip = false;
                    }
                    float num6 = 0f;
                    if (tube.rotation == 0f)
                    {
                        float num7 = tube.x - vector3.x;
                        if (Math.Abs(num7) > num3 / 4f)
                        {
                            num6 = -vector4.x / num + 0.25f * num7;
                        }
                        else if (Math.Abs(vector4.x) < 1f)
                        {
                            num6 = -vector4.x;
                        }
                        else
                        {
                            num6 = -vector4.x / num;
                        }
                    }
                    float num8 = -32f / star.weight;
                    if (tube.rotation != 0f)
                    {
                        num *= 15f;
                        if (tube.rotation == 180f)
                        {
                            num8 /= 2f;
                        }
                        else
                        {
                            num8 /= 4f;
                        }
                    }
                    Vector vector5 = MathHelper.vect(num6, -vector4.y / num + num8);
                    float num9 = tube.y - vector3.y;
                    if (num9 > currentHeightModulated + num5)
                    {
                        vector5 = MathHelper.vectMult(vector5, Math.Exp((double)(-2f * (num9 - (currentHeightModulated + num5)))));
                    }
                    vector5 = MathHelper.vectRotate(vector5, (double)num2);
                    star.applyImpulseDelta(vector5, 0.016f);
                    return;
                }
            }
            else
            {
                Vector vector6 = MathHelper.vect(starL.pos.x, starL.pos.y);
                Vector vector7 = MathHelper.vect(starL.v.x, starL.v.y);
                vector6 = MathHelper.vectRotateAround(vector6, (double)(-(double)num2), tube.x, tube.y);
                vector7 = MathHelper.vectRotate(vector7, (double)(-(double)num2));
                if (MathHelper.rectInRect(vector6.x - num5, vector6.y - num5 / 2f, vector6.x + num5, vector6.y + num5, vector.x, vector.y, vector2.x, vector2.y))
                {
                    foreach (Bouncer bouncer2 in bouncers)
                    {
                        bouncer2.skip = false;
                    }
                    float num10 = 0f;
                    if (tube.rotation == 0f)
                    {
                        float num11 = tube.x - vector6.x;
                        if (Math.Abs(num11) > num3 / 4f)
                        {
                            num10 = -vector7.x / num + 0.25f * num11;
                        }
                        else if (Math.Abs(vector7.x) < 1f)
                        {
                            num10 = -vector7.x;
                        }
                        else
                        {
                            num10 = -vector7.x / num;
                        }
                    }
                    float num12 = -32f / starL.weight;
                    if (tube.rotation != 0f)
                    {
                        num *= 15f;
                        if (tube.rotation == 180f)
                        {
                            num12 /= 2f;
                        }
                        else
                        {
                            num12 /= 4f;
                        }
                    }
                    Vector vector8 = MathHelper.vect(num10, -vector7.y / num + num12);
                    float num13 = tube.y - vector6.y;
                    if (num13 > currentHeightModulated + num5)
                    {
                        vector8 = MathHelper.vectMult(vector8, Math.Exp((double)(-2f * (num13 - (currentHeightModulated + num5)))));
                    }
                    vector8 = MathHelper.vectRotate(vector8, (double)num2);
                    starL.applyImpulseDelta(vector8, 0.016f);
                }
                vector6 = MathHelper.vect(starR.pos.x, starR.pos.y);
                vector7 = MathHelper.vect(starR.v.x, starR.v.y);
                vector6 = MathHelper.vectRotateAround(vector6, (double)(-(double)num2), tube.x, tube.y);
                vector7 = MathHelper.vectRotate(vector7, (double)(-(double)num2));
                if (MathHelper.rectInRect(vector6.x - num5, vector6.y - num5 / 2f, vector6.x + num5, vector6.y + num5, vector.x, vector.y, vector2.x, vector2.y))
                {
                    foreach (Bouncer bouncer3 in bouncers)
                    {
                        bouncer3.skip = false;
                    }
                    float num14 = 0f;
                    if (tube.rotation == 0f)
                    {
                        float num15 = tube.x - vector6.x;
                        if (Math.Abs(num15) > num3 / 4f)
                        {
                            num14 = -vector7.x / num + 0.25f * num15;
                        }
                        else if (Math.Abs(vector7.x) < 1f)
                        {
                            num14 = -vector7.x;
                        }
                        else
                        {
                            num14 = -vector7.x / num;
                        }
                    }
                    float num16 = -32f / starR.weight;
                    if (tube.rotation != 0f)
                    {
                        num *= 15f;
                        if (tube.rotation == 180f)
                        {
                            num16 /= 2f;
                        }
                        else
                        {
                            num16 /= 4f;
                        }
                    }
                    Vector vector9 = MathHelper.vect(num14, -vector7.y / num + num16);
                    float num17 = tube.y - vector6.y;
                    if (num17 > currentHeightModulated + num5)
                    {
                        vector9 = MathHelper.vectMult(vector9, Math.Exp((double)(-2f * (num17 - (currentHeightModulated + num5)))));
                    }
                    vector9 = MathHelper.vectRotate(vector9, (double)num2);
                    starR.applyImpulseDelta(vector9, 0.016f);
                }
            }
        }

        // Token: 0x06000823 RID: 2083 RVA: 0x000496BC File Offset: 0x000478BC
        public virtual void handlePumpFlowPtSkin(Pump p, ConstraintedPoint s, GameObject c)
        {
            float num = 200f;
            if (GameObject.rectInObject(p.x - num, p.y - num, p.x + num, p.y + num, c))
            {
                Vector vector = MathHelper.vect(c.x, c.y);
                Vector vector2;
                vector2.x = p.x - p.bb.w / 2f;
                Vector vector3;
                vector3.x = p.x + p.bb.w / 2f;
                vector2.y = (vector3.y = p.y);
                if (p.angle != 0.0)
                {
                    vector = MathHelper.vectRotateAround(vector, -p.angle, p.x, p.y);
                }
                if (vector.y < vector2.y && MathHelper.rectInRect(vector.x - c.bb.w / 2f, vector.y - c.bb.h / 2f, vector.x + c.bb.w / 2f, vector.y + c.bb.h / 2f, vector2.x, vector2.y - num, vector3.x, vector3.y))
                {
                    float num2 = num * 2f;
                    float num3 = num2 * (num - (vector2.y - vector.y)) / num;
                    Vector vector4 = MathHelper.vect(0f, -num3);
                    vector4 = MathHelper.vectRotate(vector4, p.angle);
                    s.applyImpulseDelta(vector4, 0.016f);
                }
            }
        }

        // Token: 0x06000824 RID: 2084 RVA: 0x00049878 File Offset: 0x00047A78
        public virtual void handleBouncePtDelta(Bouncer b, ConstraintedPoint s, float delta)
        {
            if (b.skip)
            {
                return;
            }
            b.skip = true;
            Vector vector = MathHelper.vectSub(s.prevPos, s.pos);
            int num = ((MathHelper.vectRotateAround(s.prevPos, (double)(-(double)b.angle), b.x, b.y).y < b.y) ? (-1) : 1);
            float num2 = Math.Max(MathHelper.vectLength(vector) * 40f, 300f) * (float)num;
            Vector vector2 = MathHelper.vectForAngle(b.angle);
            Vector vector3 = MathHelper.vectMult(MathHelper.vectPerp(vector2), num2);
            s.pos = MathHelper.vectRotateAround(s.pos, (double)(-(double)b.angle), b.x, b.y);
            s.prevPos = MathHelper.vectRotateAround(s.prevPos, (double)(-(double)b.angle), b.x, b.y);
            s.prevPos.y = s.pos.y;
            s.pos = MathHelper.vectRotateAround(s.pos, (double)b.angle, b.x, b.y);
            s.prevPos = MathHelper.vectRotateAround(s.prevPos, (double)b.angle, b.x, b.y);
            s.applyImpulseDelta(vector3, delta);
            b.playTimeline(0);
            CTRSoundMgr._playSound(52);
        }

        // Token: 0x06000825 RID: 2085 RVA: 0x000499D8 File Offset: 0x00047BD8
        public virtual bool handleBubbleTouchXY(ConstraintedPoint s, float tx, float ty)
        {
            if (MathHelper.pointInRect(tx + camera.pos.x, ty + camera.pos.y, s.pos.x - 30f, s.pos.y - 30f, 60f, 60f))
            {
                popCandyBubble(s == starL);
                int num = Preferences._getIntForKey("PREFS_BUBBLES_POPPED") + 1;
                Preferences._setIntforKey(num, "PREFS_BUBBLES_POPPED", false);
                if (num >= 50)
                {
                    CTRRootController.postAchievementName(NSObject.NSS("acBubblePopper"));
                }
                if (num >= 300)
                {
                    CTRRootController.postAchievementName(NSObject.NSS("acBubbleMaster"));
                }
                return true;
            }
            return false;
        }

        // Token: 0x06000826 RID: 2086 RVA: 0x00049A94 File Offset: 0x00047C94
        public virtual void teleport()
        {
            if (targetSock == null)
            {
                return;
            }
            targetSock.light.playTimeline(0);
            targetSock.light.visible = true;
            Vector vector = MathHelper.vect(0f, -8f);
            vector = MathHelper.vectRotate(vector, (double)MathHelper.DEGREES_TO_RADIANS(targetSock.rotation));
            star.pos.x = targetSock.x;
            star.pos.y = targetSock.y;
            star.pos = MathHelper.vectAdd(star.pos, vector);
            star.prevPos.x = star.pos.x;
            star.prevPos.y = star.pos.y;
            star.v = MathHelper.vectMult(MathHelper.vectRotate(MathHelper.vect(0f, -1f), (double)MathHelper.DEGREES_TO_RADIANS(targetSock.rotation)), savedSockSpeed);
            star.posDelta = MathHelper.vectDiv(star.v, 60f);
            star.prevPos = MathHelper.vectSub(star.pos, star.posDelta);
            targetSock = null;
        }

        // Token: 0x06000827 RID: 2087 RVA: 0x00049C10 File Offset: 0x00047E10
        public virtual bool pointOutOfScreen(ConstraintedPoint p)
        {
            if (pack == 10)
            {
                return (double)p.pos.y > (double)mapHeight + 200.0 || (double)p.pos.y < -200.0;
            }
            return (double)p.pos.y > (double)mapHeight + 100.0 || (double)p.pos.y < -50.0;
        }

        // Token: 0x06000828 RID: 2088 RVA: 0x00049C98 File Offset: 0x00047E98
        public virtual void rotateAllSpikesWithID(int sid)
        {
            int i = 0;
            int count = spikes.Count;
            while (i < count)
            {
                Spikes spikes = this.spikes[i];
                if (spikes.getToggled() == sid)
                {
                    spikes.rotateSpikes();
                }
                i++;
            }
        }

        // Token: 0x06000829 RID: 2089 RVA: 0x00049CD9 File Offset: 0x00047ED9
        public virtual void drawDrawing()
        {
        }

        // Token: 0x0600082A RID: 2090 RVA: 0x00049CDB File Offset: 0x00047EDB
        private void selector_gameLost(NSObject param)
        {
            gameLost();
        }

        // Token: 0x0600082B RID: 2091 RVA: 0x00049CE3 File Offset: 0x00047EE3
        private void selector_gameWonDelegate(NSObject param)
        {
            CTRSoundMgr.EnableLoopedSounds(false);
            gameSceneDelegate_gameWon();
        }

        // Token: 0x0600082C RID: 2092 RVA: 0x00049CF6 File Offset: 0x00047EF6
        private void selector_animateLevelRestart(NSObject param)
        {
            animateLevelRestart();
        }

        // Token: 0x0600082D RID: 2093 RVA: 0x00049CFE File Offset: 0x00047EFE
        private void selector_showGreeting(NSObject param)
        {
            showGreeting();
        }

        // Token: 0x0600082E RID: 2094 RVA: 0x00049D06 File Offset: 0x00047F06
        private void selector_doCandyBlink(NSObject param)
        {
            doCandyBlink();
        }

        // Token: 0x0600082F RID: 2095 RVA: 0x00049D0E File Offset: 0x00047F0E
        private void selector_teleport(NSObject param)
        {
            teleport();
        }

        // Token: 0x06000830 RID: 2096 RVA: 0x00049D18 File Offset: 0x00047F18
        private void drawCut(Vector fls, Vector frs, Vector start, Vector end, float startSize, float endSize, RGBAColor c, ref Vector le, ref Vector re)
        {
            Vector vector = MathHelper.vectSub(end, start);
            Vector vector2 = MathHelper.vectNormalize(vector);
            Vector vector3 = MathHelper.vectRperp(vector2);
            Vector vector4 = MathHelper.vectPerp(vector2);
            Vector vector5 = (MathHelper.vectEqual(frs, MathHelper.vectUndefined) ? MathHelper.vectAdd(start, MathHelper.vectMult(vector3, startSize)) : frs);
            Vector vector6 = (MathHelper.vectEqual(fls, MathHelper.vectUndefined) ? MathHelper.vectAdd(start, MathHelper.vectMult(vector4, startSize)) : fls);
            Vector vector7 = MathHelper.vectAdd(end, MathHelper.vectMult(vector3, endSize));
            Vector vector8 = MathHelper.vectAdd(end, MathHelper.vectMult(vector4, endSize));
            float[] array = new float[] { vector6.x, vector6.y, vector5.x, vector5.y, vector7.x, vector7.y, vector8.x, vector8.y };
            GLDrawer.drawSolidPolygonWOBorder(array, 4, c);
            le = vector8;
            re = vector7;
        }

        // Token: 0x06000831 RID: 2097 RVA: 0x00049E28 File Offset: 0x00048028
        private float maxOf4(float v1, float v2, float v3, float v4)
        {
            if (v1 >= v2 && v1 >= v3 && v1 >= v4)
            {
                return v1;
            }
            if (v2 >= v1 && v2 >= v3 && v2 >= v4)
            {
                return v2;
            }
            if (v3 >= v2 && v3 >= v1 && v3 >= v4)
            {
                return v3;
            }
            if (v4 >= v2 && v4 >= v3 && v4 >= v1)
            {
                return v4;
            }
            return -1f;
        }

        // Token: 0x06000832 RID: 2098 RVA: 0x00049E7C File Offset: 0x0004807C
        private float minOf4(float v1, float v2, float v3, float v4)
        {
            if (v1 <= v2 && v1 <= v3 && v1 <= v4)
            {
                return v1;
            }
            if (v2 <= v1 && v2 <= v3 && v2 <= v4)
            {
                return v2;
            }
            if (v3 <= v2 && v3 <= v1 && v3 <= v4)
            {
                return v3;
            }
            if (v4 <= v2 && v4 <= v3 && v4 <= v1)
            {
                return v4;
            }
            return -1f;
        }

        // Token: 0x06000833 RID: 2099 RVA: 0x00049ED0 File Offset: 0x000480D0
        private void selector_revealCandyFromLantern(NSObject obj)
        {
            isCandyInLantern = false;
            candy.color = RGBAColor.solidOpaqueRGBA;
            candy.passTransformationsToChilds = false;
            candy.scaleX = (candy.scaleY = 0.71f);
            candyMain.scaleX = (candyMain.scaleY = 0.71f);
            candyTop.scaleX = (candyTop.scaleY = 0.71f);
        }

        // Token: 0x06000834 RID: 2100 RVA: 0x00049F5C File Offset: 0x0004815C
        private void postGameEvent(string _string, Dictionary<string, string> dict = null, bool mixpanel = false)
        {
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int num = ctrrootController.getPack();
            int level = ctrrootController.getLevel();
            if (dict == null)
            {
                dict = new Dictionary<string, string>();
            }
            dict["level"] = num.ToString() + "-" + level.ToString();
            FrameworkTypes.FlurryAPI.logEventwithParams(_string, dict, true, mixpanel, false);
        }

        // Token: 0x04000D49 RID: 3401
        public const int MAX_TOUCHES = 5;

        // Token: 0x04000D4A RID: 3402
        public const float DIM_TIMEOUT = 0.15f;

        // Token: 0x04000D4B RID: 3403
        public const int HUD_STARS_COUNT = 3;

        // Token: 0x04000D4C RID: 3404
        public const float SCOMBO_TIMEOUT = 0.2f;

        // Token: 0x04000D4D RID: 3405
        public const int SCUT_SCORE = 10;

        // Token: 0x04000D4E RID: 3406
        public const int MAX_LOST_CANDIES = 3;

        // Token: 0x04000D4F RID: 3407
        public const float ROPE_CUT_AT_ONCE_TIMEOUT = 0.05f;

        // Token: 0x04000D50 RID: 3408
        public const float STAR_RADIUS = 15f;

        // Token: 0x04000D51 RID: 3409
        public const float MOUTH_OPEN_RADIUS = 100f;

        // Token: 0x04000D52 RID: 3410
        public const int BLINK_SKIP = 3;

        // Token: 0x04000D53 RID: 3411
        public const float MOUTH_OPEN_TIME = 1f;

        // Token: 0x04000D54 RID: 3412
        public const float PUMP_TIMEOUT = 0.05f;

        // Token: 0x04000D55 RID: 3413
        public const float CAMERA_SPEED = 7f;

        // Token: 0x04000D56 RID: 3414
        public const float SOCK_SPEED_K = 0.9f;

        // Token: 0x04000D57 RID: 3415
        public const float SOCK_COLLISION_Y_OFFSET = 25f;

        // Token: 0x04000D58 RID: 3416
        public const int RESTART_STATE_FADE_IN = 0;

        // Token: 0x04000D59 RID: 3417
        public const int RESTART_STATE_FADE_OUT = 1;

        // Token: 0x04000D5A RID: 3418
        private const int S_MOVE_DOWN = 0;

        // Token: 0x04000D5B RID: 3419
        private const int S_WAIT = 1;

        // Token: 0x04000D5C RID: 3420
        private const int S_MOVE_UP = 2;

        // Token: 0x04000D5D RID: 3421
        private const int CAMERA_MOVE_TO_CANDY_PART = 0;

        // Token: 0x04000D5E RID: 3422
        private const int CAMERA_MOVE_TO_CANDY = 1;

        // Token: 0x04000D5F RID: 3423
        private const int BUTTON_GRAVITY = 0;

        // Token: 0x04000D60 RID: 3424
        private const int BUTTON_SPIKES = 1;

        // Token: 0x04000D61 RID: 3425
        private const int PARTS_SEPARATE = 0;

        // Token: 0x04000D62 RID: 3426
        private const int PARTS_DIST = 1;

        // Token: 0x04000D63 RID: 3427
        private const int PARTS_NONE = 2;

        // Token: 0x04000D64 RID: 3428
        private const int CANDY_BLINK_INITIAL = 0;

        // Token: 0x04000D65 RID: 3429
        private const int CANDY_BLINK_STAR = 1;

        // Token: 0x04000D66 RID: 3430
        private const int TUTORIAL_SHOW_ANIM = 0;

        // Token: 0x04000D67 RID: 3431
        private const int TUTORIAL_HIDE_ANIM = 1;

        // Token: 0x04000D68 RID: 3432
        private const int EARTH_NORMAL_ANIM = 0;

        // Token: 0x04000D69 RID: 3433
        private const int EARTH_UPSIDEDOWN_ANIM = 1;

        // Token: 0x04000D6A RID: 3434
        private const int CHAR_ANIMATION_IDLE = 0;

        // Token: 0x04000D6B RID: 3435
        private const int CHAR_ANIMATION_IDLE2 = 1;

        // Token: 0x04000D6C RID: 3436
        private const int CHAR_ANIMATION_IDLE3 = 3;

        // Token: 0x04000D6D RID: 3437
        private const int CHAR_ANIMATION_EXCITED = 4;

        // Token: 0x04000D6E RID: 3438
        private const int CHAR_ANIMATION_PUZZLED = 5;

        // Token: 0x04000D6F RID: 3439
        private const int CHAR_ANIMATION_FAIL = 6;

        // Token: 0x04000D70 RID: 3440
        private const int CHAR_ANIMATION_WIN = 7;

        // Token: 0x04000D71 RID: 3441
        private const int CHAR_ANIMATION_MOUTH_OPEN = 8;

        // Token: 0x04000D72 RID: 3442
        private const int CHAR_ANIMATION_MOUTH_CLOSE = 9;

        // Token: 0x04000D73 RID: 3443
        private const int CHAR_ANIMATION_CHEW = 10;

        // Token: 0x04000D74 RID: 3444
        private const int CHAR_ANIMATION_GREETING = 11;

        // Token: 0x04000D75 RID: 3445
        private const float GD_PRIMARY_FORCE = 32f;

        // Token: 0x04000D76 RID: 3446
        private const float GD_SLOWING_FACTOR = 5f;

        // Token: 0x04000D77 RID: 3447
        private static int[] CANDIES = new int[] { 93, 101, 102 };

        // Token: 0x04000D78 RID: 3448
        private DelayedDispatcher dd;

        // Token: 0x04000D79 RID: 3449
        public GameScene.gameWonDelegate gameSceneDelegate_gameWon;

        // Token: 0x04000D7A RID: 3450
        public GameScene.gameLostDelegate gameSceneDelegate_gameLost;

        // Token: 0x04000D7B RID: 3451
        private AnimationsPool aniPool;

        // Token: 0x04000D7C RID: 3452
        private AnimationsPool staticAniPool;

        // Token: 0x04000D7D RID: 3453
        private PollenDrawer pollenDrawer;

        // Token: 0x04000D7E RID: 3454
        private TileMap back;

        // Token: 0x04000D7F RID: 3455
        private GameScene.CharAnim target;

        // Token: 0x04000D80 RID: 3456
        private GameScene.CharAnim targetIdle;

        // Token: 0x04000D81 RID: 3457
        private Image support;

        // Token: 0x04000D82 RID: 3458
        private GameObject candy;

        // Token: 0x04000D83 RID: 3459
        private Image candyMain;

        // Token: 0x04000D84 RID: 3460
        private Image candyTop;

        // Token: 0x04000D85 RID: 3461
        private Animation candyBlink;

        // Token: 0x04000D86 RID: 3462
        private Animation candyBubbleAnimation;

        // Token: 0x04000D87 RID: 3463
        private Animation candyBubbleAnimationL;

        // Token: 0x04000D88 RID: 3464
        private Animation candyBubbleAnimationR;

        // Token: 0x04000D89 RID: 3465
        private bool isCandyInGhostBubbleAnimationLoaded;

        // Token: 0x04000D8A RID: 3466
        private bool isCandyInGhostBubbleAnimationLeftLoaded;

        // Token: 0x04000D8B RID: 3467
        private bool isCandyInGhostBubbleAnimationRightLoaded;

        // Token: 0x04000D8C RID: 3468
        private CandyInGhostBubbleAnimation candyGhostBubbleAnimation;

        // Token: 0x04000D8D RID: 3469
        private CandyInGhostBubbleAnimation candyGhostBubbleAnimationL;

        // Token: 0x04000D8E RID: 3470
        private CandyInGhostBubbleAnimation candyGhostBubbleAnimationR;

        // Token: 0x04000D8F RID: 3471
        private bool shouldRestoreSecondGhost;

        // Token: 0x04000D90 RID: 3472
        private ConstraintedPoint star;

        // Token: 0x04000D91 RID: 3473
        private List<Grab> bungees;

        // Token: 0x04000D92 RID: 3474
        private List<Razor> razors;

        // Token: 0x04000D93 RID: 3475
        private List<Spikes> spikes;

        // Token: 0x04000D94 RID: 3476
        private List<Star> stars;

        // Token: 0x04000D95 RID: 3477
        private List<Bubble> bubbles;

        // Token: 0x04000D96 RID: 3478
        private List<Pump> pumps;

        // Token: 0x04000D97 RID: 3479
        private List<Sock> socks;

        // Token: 0x04000D98 RID: 3480
        private List<Bouncer> bouncers;

        // Token: 0x04000D99 RID: 3481
        private List<RotatedCircle> rotatedCircles;

        // Token: 0x04000D9A RID: 3482
        private List<GameScene.GameObjectSpecial> tutorialImages;

        // Token: 0x04000D9B RID: 3483
        private List<GameScene.TutorialText> tutorials;

        // Token: 0x04000D9C RID: 3484
        private List<Ghost> ghosts;

        // Token: 0x04000D9D RID: 3485
        private List<SteamTube> tubes;

        // Token: 0x04000D9E RID: 3486
        private GameObject candyL;

        // Token: 0x04000D9F RID: 3487
        private GameObject candyR;

        // Token: 0x04000DA0 RID: 3488
        private ConstraintedPoint starL;

        // Token: 0x04000DA1 RID: 3489
        private ConstraintedPoint starR;

        // Token: 0x04000DA2 RID: 3490
        private Animation blink;

        // Token: 0x04000DA3 RID: 3491
        private bool[] dragging = new bool[5];

        // Token: 0x04000DA4 RID: 3492
        private Vector[] startPos = new Vector[5];

        // Token: 0x04000DA5 RID: 3493
        private Vector[] prevStartPos = new Vector[5];

        // Token: 0x04000DA6 RID: 3494
        private float ropePhysicsSpeed;

        // Token: 0x04000DA7 RID: 3495
        private GameObject candyBubble;

        // Token: 0x04000DA8 RID: 3496
        private GameObject candyBubbleL;

        // Token: 0x04000DA9 RID: 3497
        private GameObject candyBubbleR;

        // Token: 0x04000DAA RID: 3498
        private Animation[] hudStar = new Animation[3];

        // Token: 0x04000DAB RID: 3499
        private Camera2D camera;

        // Token: 0x04000DAC RID: 3500
        private float mapWidth;

        // Token: 0x04000DAD RID: 3501
        private float mapHeight;

        // Token: 0x04000DAE RID: 3502
        private bool mouthOpen;

        // Token: 0x04000DAF RID: 3503
        private bool noCandy;

        // Token: 0x04000DB0 RID: 3504
        private int blinkTimer;

        // Token: 0x04000DB1 RID: 3505
        private int idlesTimer;

        // Token: 0x04000DB2 RID: 3506
        private float mouthCloseTimer;

        // Token: 0x04000DB3 RID: 3507
        private float lastCandyRotateDelta;

        // Token: 0x04000DB4 RID: 3508
        private float lastCandyRotateDeltaL;

        // Token: 0x04000DB5 RID: 3509
        private float lastCandyRotateDeltaR;

        // Token: 0x04000DB6 RID: 3510
        private bool spiderTookCandy;

        // Token: 0x04000DB7 RID: 3511
        private int special;

        // Token: 0x04000DB8 RID: 3512
        private bool fastenCamera;

        // Token: 0x04000DB9 RID: 3513
        private float savedSockSpeed;

        // Token: 0x04000DBA RID: 3514
        private Sock targetSock;

        // Token: 0x04000DBB RID: 3515
        private int ropesCutAtOnce;

        // Token: 0x04000DBC RID: 3516
        private int ropesCutFromLevelStart;

        // Token: 0x04000DBD RID: 3517
        private float ropeAtOnceTimer;

        // Token: 0x04000DBE RID: 3518
        private int pack;

        // Token: 0x04000DBF RID: 3519
        public int starsCollected;

        // Token: 0x04000DC0 RID: 3520
        public int starBonus;

        // Token: 0x04000DC1 RID: 3521
        public int timeBonus;

        // Token: 0x04000DC2 RID: 3522
        public int score;

        // Token: 0x04000DC3 RID: 3523
        public float time;

        // Token: 0x04000DC4 RID: 3524
        private float initialCameraToStarDistance;

        // Token: 0x04000DC5 RID: 3525
        public float dimTime;

        // Token: 0x04000DC6 RID: 3526
        public int restartState;

        // Token: 0x04000DC7 RID: 3527
        public bool animateRestartDim;

        // Token: 0x04000DC8 RID: 3528
        private bool freezeCamera;

        // Token: 0x04000DC9 RID: 3529
        private int cameraMoveMode;

        // Token: 0x04000DCA RID: 3530
        private bool ignoreTouches;

        // Token: 0x04000DCB RID: 3531
        private bool nightLevel;

        // Token: 0x04000DCC RID: 3532
        private bool gravityNormal;

        // Token: 0x04000DCD RID: 3533
        public ToggleButton gravityButton;

        // Token: 0x04000DCE RID: 3534
        private int gravityTouchDown;

        // Token: 0x04000DCF RID: 3535
        private bool isCandyInLantern;

        // Token: 0x04000DD0 RID: 3536
        private int twoParts;

        // Token: 0x04000DD1 RID: 3537
        private bool noCandyL;

        // Token: 0x04000DD2 RID: 3538
        private bool noCandyR;

        // Token: 0x04000DD3 RID: 3539
        private float partsDist;

        // Token: 0x04000DD4 RID: 3540
        public List<Image> earthAnims;

        // Token: 0x04000DD5 RID: 3541
        private int tummyTeasers;

        // Token: 0x04000DD6 RID: 3542
        private Vector slastTouch;

        // Token: 0x04000DD7 RID: 3543
        private List<GameScene.FingerCut>[] fingerCuts = new List<GameScene.FingerCut>[5];

        // Token: 0x04000DD8 RID: 3544
        private float JugglingTime;

        // Token: 0x02000109 RID: 265
        private class FingerCut : NSObject
        {
            // Token: 0x04000DD9 RID: 3545
            public Vector start;

            // Token: 0x04000DDA RID: 3546
            public Vector end;

            // Token: 0x04000DDB RID: 3547
            public float startSize;

            // Token: 0x04000DDC RID: 3548
            public float endSize;

            // Token: 0x04000DDD RID: 3549
            public RGBAColor c;
        }

        // Token: 0x0200010A RID: 266
        private class CharAnim : GameObject
        {
            // Token: 0x06000838 RID: 2104 RVA: 0x0004A03B File Offset: 0x0004823B
            public static GameScene.CharAnim CharAnim_create(Texture2D t)
            {
                return (GameScene.CharAnim)new GameScene.CharAnim().initWithTexture(t);
            }

            // Token: 0x06000839 RID: 2105 RVA: 0x0004A04D File Offset: 0x0004824D
            public static GameScene.CharAnim CharAnim_createWithResID(int r)
            {
                return GameScene.CharAnim.CharAnim_create(Application.getTexture(r));
            }

            // Token: 0x0600083A RID: 2106 RVA: 0x0004A05C File Offset: 0x0004825C
            public static GameScene.CharAnim CharAnim_createWithResIDQuad(int r, int q)
            {
                GameScene.CharAnim charAnim = GameScene.CharAnim.CharAnim_create(Application.getTexture(r));
                charAnim.setDrawQuad(q);
                return charAnim;
            }

            // Token: 0x0600083B RID: 2107 RVA: 0x0004A080 File Offset: 0x00048280
            public override void playTimeline(int t)
            {
                if (getTimeline(t) == null)
                {
                    if (!o.disableAnimations)
                    {
                        o.visible = true;
                        visible = false;
                        o.playTimeline(t);
                        return;
                    }
                }
                else if (!disableAnimations)
                {
                    o.visible = false;
                    visible = true;
                    base.playTimeline(t);
                }
            }

            // Token: 0x0600083C RID: 2108 RVA: 0x0004A0E8 File Offset: 0x000482E8
            public override void switchToAnimationatEndOfAnimationDelay(int a2, int a1, float d)
            {
                Timeline timeline = getTimeline(a1);
                List<ctr_wp7.iframework.visual.Action> list = new List<ctr_wp7.iframework.visual.Action>();
                list.Add(ctr_wp7.iframework.visual.Action.createAction(this, "ACTION_PLAY_TIMELINE", 0, a2));
                timeline.addKeyFrame(KeyFrame.makeAction(list, d));
            }

            // Token: 0x04000DDE RID: 3550
            public GameScene.CharAnim o;

            // Token: 0x04000DDF RID: 3551
            public bool disableAnimations;
        }

        // Token: 0x0200010B RID: 267
        private class GameObjectSpecial : CTRGameObject
        {
            // Token: 0x0600083E RID: 2110 RVA: 0x0004A12B File Offset: 0x0004832B
            public static GameScene.GameObjectSpecial GameObjectSpecial_create(Texture2D t)
            {
                return (GameScene.GameObjectSpecial)new GameScene.GameObjectSpecial().initWithTexture(t);
            }

            // Token: 0x0600083F RID: 2111 RVA: 0x0004A13D File Offset: 0x0004833D
            public static GameScene.GameObjectSpecial GameObjectSpecial_createWithResID(int r)
            {
                return GameScene.GameObjectSpecial.GameObjectSpecial_create(Application.getTexture(r));
            }

            // Token: 0x06000840 RID: 2112 RVA: 0x0004A14C File Offset: 0x0004834C
            public static GameScene.GameObjectSpecial GameObjectSpecial_createWithResIDQuad(int r, int q)
            {
                GameScene.GameObjectSpecial gameObjectSpecial = GameScene.GameObjectSpecial.GameObjectSpecial_create(Application.getTexture(r));
                gameObjectSpecial.setDrawQuad(q);
                return gameObjectSpecial;
            }

            // Token: 0x04000DE0 RID: 3552
            public int special;
        }

        // Token: 0x0200010C RID: 268
        private class SCandy : ConstraintedPoint
        {
            // Token: 0x04000DE1 RID: 3553
            public bool good;

            // Token: 0x04000DE2 RID: 3554
            public float speed;

            // Token: 0x04000DE3 RID: 3555
            public float angle;

            // Token: 0x04000DE4 RID: 3556
            public float lastAngleChange;
        }

        // Token: 0x0200010D RID: 269
        private class TutorialText : Text
        {
            // Token: 0x04000DE5 RID: 3557
            public int special;
        }

        // Token: 0x0200010E RID: 270
        // (Invoke) Token: 0x06000845 RID: 2117
        public delegate void gameWonDelegate();

        // Token: 0x0200010F RID: 271
        // (Invoke) Token: 0x06000849 RID: 2121
        public delegate void gameLostDelegate();
    }
}
