using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000C2 RID: 194
    internal class Grab : GameObject
    {
        // Token: 0x0600059E RID: 1438 RVA: 0x0002A728 File Offset: 0x00028928
        public override NSObject init()
        {
            if (base.init() != null)
            {
                rope = null;
                wheelOperating = -1;
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                baloon = ctrrootController.isSurvival();
            }
            return this;
        }

        // Token: 0x0600059F RID: 1439 RVA: 0x0002A764 File Offset: 0x00028964
        public override void update(float delta)
        {
            base.update(delta);
            if (launcher && rope != null)
            {
                rope.bungeeAnchor.pos = vect(x, y);
                rope.bungeeAnchor.pin = rope.bungeeAnchor.pos;
                if (launcherIncreaseSpeed)
                {
                    if (Mover.moveVariableToTarget(ref launcherSpeed, 200f, 30f, delta))
                    {
                        launcherIncreaseSpeed = false;
                    }
                }
                else if (Mover.moveVariableToTarget(ref launcherSpeed, 130f, 30f, delta))
                {
                    launcherIncreaseSpeed = true;
                }
                mover.setMoveSpeed(launcherSpeed);
            }
            if (hideRadius)
            {
                radiusAlpha -= 1.5f * delta;
                if ((double)radiusAlpha <= 0.0)
                {
                    radius = -1f;
                    hideRadius = false;
                }
            }
            if (wheel && wheelDirty)
            {
                float num;
                if (rope != null)
                {
                    num = (float)rope.getLength() * 0.7f;
                }
                else
                {
                    num = 0f;
                }
                if (num == 0f)
                {
                    wheelImage2.scaleX = (wheelImage2.scaleY = 0f);
                }
                else
                {
                    wheelImage2.scaleX = (wheelImage2.scaleY = (float)Math.Max(0.0, Math.Min(1.2, 1.0 - (double)num / 700.0)));
                }
            }
            if (bee != null)
            {
                Vector vector = mover.path[mover.targetPoint];
                Vector pos = mover.pos;
                Vector vector2 = vectSub(vector, pos);
                float num2 = 0f;
                if (Math.Abs(vector2.x) > 15f)
                {
                    num2 = ((vector2.x > 0f) ? 10f : (-10f));
                }
                Mover.moveVariableToTarget(ref bee.rotation, num2, 60f, delta);
            }
        }

        // Token: 0x060005A0 RID: 1440 RVA: 0x0002A9A8 File Offset: 0x00028BA8
        public override void draw()
        {
            base.preDraw();
            OpenGL.glEnable(0);
            Bungee bungee = rope;
            if (wheel)
            {
                wheelHighlight.visible = wheelOperating != -1;
                wheelImage3.visible = wheelOperating == -1;
                OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                wheelImage.draw();
            }
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glDisable(0);
            if (bungee != null)
            {
                bungee.draw();
            }
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            if ((double)moveLength <= 0.0)
            {
                front.draw();
            }
            else if (moverDragging != -1)
            {
                grabMoverHighlight.draw();
            }
            else
            {
                grabMover.draw();
            }
            if (wheel)
            {
                wheelImage2.draw();
            }
            base.postDraw();
        }

        // Token: 0x060005A1 RID: 1441 RVA: 0x0002AA93 File Offset: 0x00028C93
        public override void dealloc()
        {
            if (vertices != null)
            {
                vertices = null;
            }
            destroyRope();
            base.dealloc();
        }

        // Token: 0x060005A2 RID: 1442 RVA: 0x0002AAB0 File Offset: 0x00028CB0
        public virtual void setRope(Bungee r)
        {
            NSREL(rope);
            rope = r;
            radius = -1f;
            if (hasSpider)
            {
                shouldActivate = true;
            }
        }

        // Token: 0x060005A3 RID: 1443 RVA: 0x0002AAE0 File Offset: 0x00028CE0
        public virtual void setRadius(float r)
        {
            radius = r;
            if (radius == -1f)
            {
                int num = RND_RANGE(125, 126);
                back = Image_createWithResIDQuad(num, 0);
                back.doRestoreCutTransparency();
                back.anchor = (back.parentAnchor = 18);
                front = Image_createWithResIDQuad(num, 1);
                front.anchor = (front.parentAnchor = 18);
                addChild(back);
                addChild(front);
                back.visible = false;
                front.visible = false;
            }
            else
            {
                back = Image_createWithResIDQuad(122, 0);
                back.doRestoreCutTransparency();
                back.anchor = (back.parentAnchor = 18);
                front = Image_createWithResIDQuad(122, 1);
                front.anchor = (front.parentAnchor = 18);
                addChild(back);
                addChild(front);
                back.visible = false;
                front.visible = false;
                radiusAlpha = 1f;
                hideRadius = false;
                vertexCount = (int)Math.Max(16f, radius);
                if (vertexCount % 2 != 0)
                {
                    vertexCount++;
                }
                vertices = new float[vertexCount * 2];
                GLDrawer.calcCircle(x, y, radius, vertexCount, vertices);
            }
            if (wheel)
            {
                wheelImage = Image_createWithResIDQuad(134, 0);
                wheelImage.anchor = (wheelImage.parentAnchor = 18);
                addChild(wheelImage);
                wheelImage.visible = false;
                wheelImage2 = Image_createWithResIDQuad(134, 1);
                wheelImage2.passTransformationsToChilds = false;
                wheelHighlight = Image_createWithResIDQuad(134, 2);
                wheelHighlight.anchor = (wheelHighlight.parentAnchor = 18);
                wheelImage2.addChild(wheelHighlight);
                wheelImage3 = Image_createWithResIDQuad(134, 3);
                wheelImage3.anchor = (wheelImage3.parentAnchor = (wheelImage2.anchor = (wheelImage2.parentAnchor = 18)));
                wheelImage2.addChild(wheelImage3);
                addChild(wheelImage2);
                wheelImage2.visible = false;
                wheelDirty = true;
            }
        }

        // Token: 0x060005A4 RID: 1444 RVA: 0x0002ADD8 File Offset: 0x00028FD8
        public virtual void setMoveLengthVerticalOffset(float l, bool v, float o)
        {
            moveLength = l;
            moveVertical = v;
            moveOffset = o;
            if ((double)moveLength > 0.0)
            {
                moveBackground = HorizontallyTiledImage.HorizontallyTiledImage_createWithResID(142);
                moveBackground.setTileHorizontallyLeftCenterRight(0, 2, 1);
                moveBackground.width = (int)((double)l + 37.0);
                moveBackground.rotationCenterX = (float)(-(float)Math.Round((double)moveBackground.width / 2.0) + 17.0);
                moveBackground.x = -17f;
                grabMoverHighlight = Image_createWithResIDQuad(142, 3);
                grabMoverHighlight.visible = false;
                grabMoverHighlight.anchor = (grabMoverHighlight.parentAnchor = 18);
                addChild(grabMoverHighlight);
                grabMover = Image_createWithResIDQuad(142, 4);
                grabMover.visible = false;
                grabMover.anchor = (grabMover.parentAnchor = 18);
                addChild(grabMover);
                grabMover.addChild(moveBackground);
                if (moveVertical)
                {
                    moveBackground.rotation = 90f;
                    moveBackground.y = -moveOffset;
                    minMoveValue = y - moveOffset;
                    maxMoveValue = y + (moveLength - moveOffset);
                    grabMover.rotation = 90f;
                    grabMoverHighlight.rotation = 90f;
                }
                else
                {
                    minMoveValue = x - moveOffset;
                    maxMoveValue = x + (moveLength - moveOffset);
                    moveBackground.x += -moveOffset;
                }
                moveBackground.anchor = 19;
                moveBackground.x += x;
                moveBackground.y += y;
                moveBackground.visible = false;
            }
            moverDragging = -1;
        }

        // Token: 0x060005A5 RID: 1445 RVA: 0x0002B034 File Offset: 0x00029234
        public virtual void setBee()
        {
            bee = Image_createWithResIDQuad(148, 1);
            bee.blendingMode = 1;
            bee.doRestoreCutTransparency();
            bee.parentAnchor = 18;
            Animation animation = Animation_createWithResID(148);
            animation.parentAnchor = (animation.anchor = 9);
            animation.doRestoreCutTransparency();
            animation.addAnimationDelayLoopFirstLast(0.03f, Timeline.LoopType.TIMELINE_PING_PONG, 2, 4);
            animation.playTimeline(0);
            animation.jumpTo(RND_RANGE(0, 2));
            bee.addChild(animation);
            Vector quadOffset = getQuadOffset(148, 0);
            bee.x = -quadOffset.x;
            bee.y = -quadOffset.y;
            bee.rotationCenterX = quadOffset.x - (float)(bee.width / 2);
            bee.rotationCenterY = quadOffset.y - (float)(bee.height / 2);
            bee.scaleX = (bee.scaleY = 0.7692308f);
            addChild(bee);
        }

        // Token: 0x060005A6 RID: 1446 RVA: 0x0002B168 File Offset: 0x00029368
        public virtual void setSpider(bool s)
        {
            hasSpider = s;
            shouldActivate = false;
            spiderActive = false;
            spider = Animation_createWithResID(94);
            spider.doRestoreCutTransparency();
            spider.anchor = 18;
            spider.x = x;
            spider.y = y;
            spider.visible = false;
            spider.addAnimationWithIDDelayLoopFirstLast(0, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 6);
            spider.setDelayatIndexforAnimation(0.4f, 5, 0);
            spider.addAnimationWithIDDelayLoopFirstLast(1, 0.1f, Timeline.LoopType.TIMELINE_REPLAY, 7, 10);
            spider.switchToAnimationatEndOfAnimationDelay(1, 0, 0.05f);
            addChild(spider);
        }

        // Token: 0x060005A7 RID: 1447 RVA: 0x0002B238 File Offset: 0x00029438
        public virtual void setLauncher()
        {
            launcher = true;
            launcherIncreaseSpeed = true;
            launcherSpeed = 130f;
            Mover mover = new Mover().initWithPathCapacityMoveSpeedRotateSpeed(100, launcherSpeed, 0f);
            mover.setPathFromStringandStart(new NSString("RC30"), vect(x, y));
            setMover(mover);
            mover.start();
        }

        // Token: 0x060005A8 RID: 1448 RVA: 0x0002B2A4 File Offset: 0x000294A4
        public virtual void destroyRope()
        {
            NSREL(rope);
            rope = null;
        }

        // Token: 0x060005A9 RID: 1449 RVA: 0x0002B2B8 File Offset: 0x000294B8
        public virtual void reCalcCircle()
        {
            GLDrawer.calcCircle(x, y, radius, vertexCount, vertices);
        }

        // Token: 0x060005AA RID: 1450 RVA: 0x0002B2E0 File Offset: 0x000294E0
        public virtual void drawBack()
        {
            if ((double)moveLength > 0.0)
            {
                moveBackground.draw();
            }
            else
            {
                back.draw();
            }
            OpenGL.glDisable(0);
            if (radius != -1f || hideRadius)
            {
                RGBAColor rgbacolor = RGBAColor.MakeRGBA(0.2, 0.5, 0.9, (double)radiusAlpha);
                drawGrabCircle(this, x, y, radius, vertexCount, rgbacolor);
            }
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
        }

        // Token: 0x060005AB RID: 1451 RVA: 0x0002B385 File Offset: 0x00029585
        public virtual void drawSpider()
        {
            spider.draw();
        }

        // Token: 0x060005AC RID: 1452 RVA: 0x0002B394 File Offset: 0x00029594
        public virtual void updateSpider(float delta)
        {
            if (hasSpider && shouldActivate)
            {
                shouldActivate = false;
                spiderActive = true;
                CTRSoundMgr._playSound(43);
                spider.playTimeline(0);
            }
            if (hasSpider && spiderActive)
            {
                if (spider.getCurrentTimelineIndex() != 0)
                {
                    spiderPos += delta * 45f;
                }
                float num = 0f;
                bool flag = false;
                if (rope != null)
                {
                    int i = 0;
                    while (i < rope.drawPtsCount)
                    {
                        Vector vector = vect(rope.drawPts[i], rope.drawPts[i + 1]);
                        Vector vector2 = vect(rope.drawPts[i + 2], rope.drawPts[i + 3]);
                        float num2 = Math.Max(20f, vectDistance(vector, vector2));
                        if (spiderPos >= num && (spiderPos < num + num2 || i > rope.drawPtsCount - 3))
                        {
                            float num3 = spiderPos - num;
                            Vector vector3 = vectSub(vector2, vector);
                            vector3 = vectMult(vector3, num3 / num2);
                            spider.x = vector.x + vector3.x;
                            spider.y = vector.y + vector3.y;
                            if (i > rope.drawPtsCount - 3)
                            {
                                flag = true;
                            }
                            if (spider.getCurrentTimelineIndex() != 0)
                            {
                                spider.rotation = RADIANS_TO_DEGREES(vectAngleNormalized(vector3)) + 270f;
                                break;
                            }
                            break;
                        }
                        else
                        {
                            num += num2;
                            i += 2;
                        }
                    }
                }
                if (flag)
                {
                    spiderPos = -1f;
                }
            }
        }

        // Token: 0x060005AD RID: 1453 RVA: 0x0002B565 File Offset: 0x00029765
        public virtual void handleWheelTouch(Vector v)
        {
            lastWheelTouch = v;
        }

        // Token: 0x060005AE RID: 1454 RVA: 0x0002B570 File Offset: 0x00029770
        public virtual void handleWheelRotate(Vector v)
        {
            if (lastWheelTouch.x - v.x == 0f && lastWheelTouch.y - v.y == 0f)
            {
                return;
            }
            CTRSoundMgr._playSound(46);
            float num = getRotateAngleForStartEndCenter(lastWheelTouch, v, vect(x, y));
            wheelImage2.rotation += num;
            wheelImage3.rotation += num;
            wheelHighlight.rotation += num;
            num = ((num > 0f) ? MIN((double)MAX(1.0, (double)num), 2.0) : MAX((double)MIN(-1.0, (double)num), -2.0));
            if (rope != null)
            {
                float num2 = (float)rope.getLength();
                if (num > 0f)
                {
                    if ((double)num2 < 500.0)
                    {
                        rope.roll(num);
                    }
                }
                else if (num != 0f && rope.parts.Count > 3)
                {
                    rope.rollBack(-num);
                }
                wheelDirty = true;
            }
            lastWheelTouch = v;
        }

        // Token: 0x060005AF RID: 1455 RVA: 0x0002B6CC File Offset: 0x000298CC
        public virtual float getRotateAngleForStartEndCenter(Vector v1, Vector v2, Vector c)
        {
            Vector vector = vectSub(v1, c);
            Vector vector2 = vectSub(v2, c);
            float num = vectAngleNormalized(vector2) - vectAngleNormalized(vector);
            return RADIANS_TO_DEGREES(num);
        }

        // Token: 0x060005B0 RID: 1456 RVA: 0x0002B700 File Offset: 0x00029900
        protected void drawGrabCircle(Grab s, float x, float y, float radius, int vertexCount, RGBAColor color)
        {
            OpenGL.glColor4f(color.r, color.g, color.b, color.a);
            OpenGL.glDisableClientState(0);
            OpenGL.glEnableClientState(13);
            OpenGL.glColorPointer_setAdditive(s.vertexCount * 8);
            OpenGL.glVertexPointer_setAdditive(2, 5, 0, s.vertexCount * 16);
            for (int i = 0; i < s.vertexCount; i += 2)
            {
                GLDrawer.drawAntialiasedLine(s.vertices[i * 2], s.vertices[i * 2 + 1], s.vertices[i * 2 + 2], s.vertices[i * 2 + 3], 1f, color);
            }
            OpenGL.glDrawArrays(8, 0, 8);
            OpenGL.glEnableClientState(0);
            OpenGL.glDisableClientState(13);
        }

        // Token: 0x04000AF0 RID: 2800
        private const float SPIDER_SPEED = 45f;

        // Token: 0x04000AF1 RID: 2801
        public Image back;

        // Token: 0x04000AF2 RID: 2802
        public Image front;

        // Token: 0x04000AF3 RID: 2803
        public Image dot;

        // Token: 0x04000AF4 RID: 2804
        public Bungee rope;

        // Token: 0x04000AF5 RID: 2805
        public float radius;

        // Token: 0x04000AF6 RID: 2806
        public float radiusAlpha;

        // Token: 0x04000AF7 RID: 2807
        public bool hideRadius;

        // Token: 0x04000AF8 RID: 2808
        public float[] vertices;

        // Token: 0x04000AF9 RID: 2809
        public int vertexCount;

        // Token: 0x04000AFA RID: 2810
        public bool wheel;

        // Token: 0x04000AFB RID: 2811
        public Image wheelHighlight;

        // Token: 0x04000AFC RID: 2812
        public Image wheelImage;

        // Token: 0x04000AFD RID: 2813
        public Image wheelImage2;

        // Token: 0x04000AFE RID: 2814
        public Image wheelImage3;

        // Token: 0x04000AFF RID: 2815
        public int wheelOperating;

        // Token: 0x04000B00 RID: 2816
        public Vector lastWheelTouch;

        // Token: 0x04000B01 RID: 2817
        public float moveLength;

        // Token: 0x04000B02 RID: 2818
        public bool moveVertical;

        // Token: 0x04000B03 RID: 2819
        public float moveOffset;

        // Token: 0x04000B04 RID: 2820
        public HorizontallyTiledImage moveBackground;

        // Token: 0x04000B05 RID: 2821
        public Image grabMoverHighlight;

        // Token: 0x04000B06 RID: 2822
        public Image grabMover;

        // Token: 0x04000B07 RID: 2823
        public int moverDragging;

        // Token: 0x04000B08 RID: 2824
        public float minMoveValue;

        // Token: 0x04000B09 RID: 2825
        public float maxMoveValue;

        // Token: 0x04000B0A RID: 2826
        public bool hasSpider;

        // Token: 0x04000B0B RID: 2827
        public bool spiderActive;

        // Token: 0x04000B0C RID: 2828
        public Animation spider;

        // Token: 0x04000B0D RID: 2829
        public float spiderPos;

        // Token: 0x04000B0E RID: 2830
        public bool shouldActivate;

        // Token: 0x04000B0F RID: 2831
        public bool wheelDirty;

        // Token: 0x04000B10 RID: 2832
        public bool launcher;

        // Token: 0x04000B11 RID: 2833
        public float launcherSpeed;

        // Token: 0x04000B12 RID: 2834
        public bool launcherIncreaseSpeed;

        // Token: 0x04000B13 RID: 2835
        public bool baloon;

        // Token: 0x04000B14 RID: 2836
        public float initial_rotation;

        // Token: 0x04000B15 RID: 2837
        public float initial_x;

        // Token: 0x04000B16 RID: 2838
        public float initial_y;

        // Token: 0x04000B17 RID: 2839
        public RotatedCircle initial_rotatedCircle;

        // Token: 0x04000B18 RID: 2840
        public Image bee;

        // Token: 0x020000C3 RID: 195
        private enum SPIDER_ANI
        {
            // Token: 0x04000B1A RID: 2842
            SPIDER_START_ANI,
            // Token: 0x04000B1B RID: 2843
            SPIDER_WALK_ANI,
            // Token: 0x04000B1C RID: 2844
            SPIDER_BUSTED_ANI,
            // Token: 0x04000B1D RID: 2845
            SPIDER_CATCH_ANI
        }
    }
}
