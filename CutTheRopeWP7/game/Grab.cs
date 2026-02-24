using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal class Grab : GameObject
    {
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
                if (radiusAlpha <= 0.0)
                {
                    radius = -1f;
                    hideRadius = false;
                }
            }
            if (wheel && wheelDirty)
            {
                float num = rope != null ? rope.getLength() * 0.7f : 0f;
                wheelImage2.scaleX = num == 0f ? (wheelImage2.scaleY = 0f) : (wheelImage2.scaleY = (float)Math.Max(0.0, Math.Min(1.2, 1.0 - ((double)num / 700.0))));
            }
            if (bee != null)
            {
                Vector vector = mover.path[mover.targetPoint];
                Vector pos = mover.pos;
                Vector vector2 = vectSub(vector, pos);
                float num2 = 0f;
                if (Math.Abs(vector2.x) > 15f)
                {
                    num2 = (vector2.x > 0f) ? 10f : (-10f);
                }
                _ = Mover.moveVariableToTarget(ref bee.rotation, num2, 60f, delta);
            }
        }

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
            bungee?.draw();
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            if (moveLength <= 0.0)
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

        public override void dealloc()
        {
            if (vertices != null)
            {
                vertices = null;
            }
            destroyRope();
            base.dealloc();
        }

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

        public virtual void setRadius(float r)
        {
            radius = r;
            if (radius == -1f)
            {
                int num = RND_RANGE(125, 126);
                back = Image_createWithResIDQuad(num, 0);
                back.doRestoreCutTransparency();
                back.anchor = back.parentAnchor = 18;
                front = Image_createWithResIDQuad(num, 1);
                front.anchor = front.parentAnchor = 18;
                _ = addChild(back);
                _ = addChild(front);
                back.visible = false;
                front.visible = false;
            }
            else
            {
                back = Image_createWithResIDQuad(122, 0);
                back.doRestoreCutTransparency();
                back.anchor = back.parentAnchor = 18;
                front = Image_createWithResIDQuad(122, 1);
                front.anchor = front.parentAnchor = 18;
                _ = addChild(back);
                _ = addChild(front);
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
                wheelImage.anchor = wheelImage.parentAnchor = 18;
                _ = addChild(wheelImage);
                wheelImage.visible = false;
                wheelImage2 = Image_createWithResIDQuad(134, 1);
                wheelImage2.passTransformationsToChilds = false;
                wheelHighlight = Image_createWithResIDQuad(134, 2);
                wheelHighlight.anchor = wheelHighlight.parentAnchor = 18;
                _ = wheelImage2.addChild(wheelHighlight);
                wheelImage3 = Image_createWithResIDQuad(134, 3);
                wheelImage3.anchor = wheelImage3.parentAnchor = wheelImage2.anchor = wheelImage2.parentAnchor = 18;
                _ = wheelImage2.addChild(wheelImage3);
                _ = addChild(wheelImage2);
                wheelImage2.visible = false;
                wheelDirty = true;
            }
        }

        public virtual void setMoveLengthVerticalOffset(float l, bool v, float o)
        {
            moveLength = l;
            moveVertical = v;
            moveOffset = o;
            if (moveLength > 0.0)
            {
                moveBackground = HorizontallyTiledImage.HorizontallyTiledImage_createWithResID(142);
                moveBackground.setTileHorizontallyLeftCenterRight(0, 2, 1);
                moveBackground.width = (int)((double)l + 37.0);
                moveBackground.rotationCenterX = (float)(-(float)Math.Round(moveBackground.width / 2.0) + 17.0);
                moveBackground.x = -17f;
                grabMoverHighlight = Image_createWithResIDQuad(142, 3);
                grabMoverHighlight.visible = false;
                grabMoverHighlight.anchor = grabMoverHighlight.parentAnchor = 18;
                _ = addChild(grabMoverHighlight);
                grabMover = Image_createWithResIDQuad(142, 4);
                grabMover.visible = false;
                grabMover.anchor = grabMover.parentAnchor = 18;
                _ = addChild(grabMover);
                _ = grabMover.addChild(moveBackground);
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

        public virtual void setBee()
        {
            bee = Image_createWithResIDQuad(148, 1);
            bee.blendingMode = 1;
            bee.doRestoreCutTransparency();
            bee.parentAnchor = 18;
            Animation animation = Animation_createWithResID(148);
            animation.parentAnchor = animation.anchor = 9;
            animation.doRestoreCutTransparency();
            _ = animation.addAnimationDelayLoopFirstLast(0.03f, Timeline.LoopType.TIMELINE_PING_PONG, 2, 4);
            animation.playTimeline(0);
            animation.jumpTo(RND_RANGE(0, 2));
            _ = bee.addChild(animation);
            Vector quadOffset = getQuadOffset(148, 0);
            bee.x = -quadOffset.x;
            bee.y = -quadOffset.y;
            bee.rotationCenterX = quadOffset.x - (bee.width / 2);
            bee.rotationCenterY = quadOffset.y - (bee.height / 2);
            bee.scaleX = bee.scaleY = 0.7692308f;
            _ = addChild(bee);
        }

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
            _ = addChild(spider);
        }

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

        public virtual void destroyRope()
        {
            NSREL(rope);
            rope = null;
        }

        public virtual void reCalcCircle()
        {
            GLDrawer.calcCircle(x, y, radius, vertexCount, vertices);
        }

        public virtual void drawBack()
        {
            if (moveLength > 0.0)
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
                RGBAColor rgbacolor = RGBAColor.MakeRGBA(0.2, 0.5, 0.9, radiusAlpha);
                drawGrabCircle(this, x, y, radius, vertexCount, rgbacolor);
            }
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
        }

        public virtual void drawSpider()
        {
            spider.draw();
        }

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

        public virtual void handleWheelTouch(Vector v)
        {
            lastWheelTouch = v;
        }

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
            num = (num > 0f) ? MIN((double)MAX(1.0, (double)num), 2.0) : MAX((double)MIN(-1.0, (double)num), -2.0);
            if (rope != null)
            {
                float num2 = rope.getLength();
                if (num > 0f)
                {
                    if ((double)num2 < 500.0)
                    {
                        rope.roll(num);
                    }
                }
                else if (num != 0f && rope.parts.Count > 3)
                {
                    _ = rope.rollBack(-num);
                }
                wheelDirty = true;
            }
            lastWheelTouch = v;
        }

        public virtual float getRotateAngleForStartEndCenter(Vector v1, Vector v2, Vector c)
        {
            Vector vector = vectSub(v1, c);
            Vector vector2 = vectSub(v2, c);
            float num = vectAngleNormalized(vector2) - vectAngleNormalized(vector);
            return RADIANS_TO_DEGREES(num);
        }

        protected static void drawGrabCircle(Grab s, float x, float y, float radius, int vertexCount, RGBAColor color)
        {
            OpenGL.glColor4f(color.r, color.g, color.b, color.a);
            OpenGL.glDisableClientState(0);
            OpenGL.glEnableClientState(13);
            OpenGL.glColorPointer_setAdditive(s.vertexCount * 8);
            OpenGL.glVertexPointer_setAdditive(2, 5, 0, s.vertexCount * 16);
            for (int i = 0; i < s.vertexCount; i += 2)
            {
                GLDrawer.drawAntialiasedLine(s.vertices[i * 2], s.vertices[(i * 2) + 1], s.vertices[(i * 2) + 2], s.vertices[(i * 2) + 3], 1f, color);
            }
            OpenGL.glDrawArrays(8, 0, 8);
            OpenGL.glEnableClientState(0);
            OpenGL.glDisableClientState(13);
        }

        private const float SPIDER_SPEED = 45f;

        public Image back;

        public Image front;

        public Image dot;

        public Bungee rope;

        public float radius;

        public float radiusAlpha;

        public bool hideRadius;

        public float[] vertices;

        public int vertexCount;

        public bool wheel;

        public Image wheelHighlight;

        public Image wheelImage;

        public Image wheelImage2;

        public Image wheelImage3;

        public int wheelOperating;

        public Vector lastWheelTouch;

        public float moveLength;

        public bool moveVertical;

        public float moveOffset;

        public HorizontallyTiledImage moveBackground;

        public Image grabMoverHighlight;

        public Image grabMover;

        public int moverDragging;

        public float minMoveValue;

        public float maxMoveValue;

        public bool hasSpider;

        public bool spiderActive;

        public Animation spider;

        public float spiderPos;

        public bool shouldActivate;

        public bool wheelDirty;

        public bool launcher;

        public float launcherSpeed;

        public bool launcherIncreaseSpeed;

        public bool baloon;

        public float initial_rotation;

        public float initial_x;

        public float initial_y;

        public RotatedCircle initial_rotatedCircle;

        public Image bee;

        private enum SPIDER_ANI
        {
            SPIDER_START_ANI,
            SPIDER_WALK_ANI,
            SPIDER_BUSTED_ANI,
            SPIDER_CATCH_ANI
        }
    }
}
