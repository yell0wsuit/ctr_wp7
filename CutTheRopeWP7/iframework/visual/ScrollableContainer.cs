using System;
using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000CF RID: 207
    internal class ScrollableContainer : BaseElement
    {
        // Token: 0x06000603 RID: 1539 RVA: 0x0002D8B0 File Offset: 0x0002BAB0
        public void provideScrollPosMaxScrollPosScrollCoeff(ref Vector sp, ref Vector mp, ref Vector sc)
        {
            sp = getScroll();
            mp = getMaxScroll();
            float num = (float)container.width / (float)width;
            float num2 = (float)container.height / (float)height;
            sc = vect(num, num2);
        }

        // Token: 0x06000604 RID: 1540 RVA: 0x0002D90C File Offset: 0x0002BB0C
        public override int addChildwithID(BaseElement c, int i)
        {
            int num = container.addChildwithID(c, i);
            c.parentAnchor = 9;
            return num;
        }

        // Token: 0x06000605 RID: 1541 RVA: 0x0002D930 File Offset: 0x0002BB30
        public override int addChild(BaseElement c)
        {
            c.parentAnchor = 9;
            return container.addChild(c);
        }

        // Token: 0x06000606 RID: 1542 RVA: 0x0002D946 File Offset: 0x0002BB46
        public override void removeChildWithID(int i)
        {
            container.removeChildWithID(i);
        }

        // Token: 0x06000607 RID: 1543 RVA: 0x0002D954 File Offset: 0x0002BB54
        public override void removeChild(BaseElement c)
        {
            container.removeChild(c);
        }

        // Token: 0x06000608 RID: 1544 RVA: 0x0002D962 File Offset: 0x0002BB62
        public override BaseElement getChild(int i)
        {
            return container.getChild(i);
        }

        // Token: 0x06000609 RID: 1545 RVA: 0x0002D970 File Offset: 0x0002BB70
        public override int childsCount()
        {
            return container.childsCount();
        }

        // Token: 0x0600060A RID: 1546 RVA: 0x0002D980 File Offset: 0x0002BB80
        public override void draw()
        {
            float x = container.x;
            float y = container.y;
            container.x = (float)Math.Round((double)container.x);
            container.y = (float)Math.Round((double)container.y);
            base.preDraw();
            OpenGL.glEnable(4);
            OpenGL.setScissorRectangle(drawX, drawY, (float)width, (float)height);
            postDraw();
            OpenGL.glDisable(4);
            container.x = x;
            container.y = y;
        }

        // Token: 0x0600060B RID: 1547 RVA: 0x0002DA30 File Offset: 0x0002BC30
        public override void postDraw()
        {
            if (!passTransformationsToChilds)
            {
                restoreTransformations(this);
            }
            container.preDraw();
            if (!container.passTransformationsToChilds)
            {
                restoreTransformations(container);
            }
            Dictionary<int, BaseElement> childs = container.getChilds();
            int i = 0;
            int count = childs.Count;
            while (i < count)
            {
                BaseElement baseElement = childs[i];
                float drawX = baseElement.drawX;
                float drawY = baseElement.drawY;
                if (baseElement != null && baseElement.visible && rectInRect(drawX, drawY, drawX + (float)baseElement.width, drawY + (float)baseElement.height, this.drawX, this.drawY, this.drawX + (float)width, this.drawY + (float)height))
                {
                    baseElement.draw();
                }
                else
                {
                    calculateTopLeft(baseElement);
                }
                i++;
            }
            if (container.passTransformationsToChilds)
            {
                restoreTransformations(container);
            }
            if (passTransformationsToChilds)
            {
                restoreTransformations(this);
            }
        }

        // Token: 0x0600060C RID: 1548 RVA: 0x0002DB2C File Offset: 0x0002BD2C
        public override void update(float delta)
        {
            base.update(delta);
            delta = fixedDelta;
            targetPoint = vectZero;
            if ((double)touchTimer > 0.0)
            {
                touchTimer -= delta;
                if ((double)touchTimer <= 0.0)
                {
                    touchTimer = 0f;
                    passTouches = true;
                    if (!movingByInertion && !movingToSpoint && base.onTouchDownXY(savedTouch.x, savedTouch.y))
                    {
                        return;
                    }
                }
            }
            if ((double)touchReleaseTimer > 0.0)
            {
                touchReleaseTimer -= delta;
                if ((double)touchReleaseTimer <= 0.0)
                {
                    touchReleaseTimer = 0f;
                    if (base.onTouchUpXY(savedTouch.x, savedTouch.y))
                    {
                        return;
                    }
                }
            }
            if (touchState == TOUCH_STATE.TOUCH_STATE_UP)
            {
                if (shouldBounceHorizontally)
                {
                    if ((double)container.x > 0.0)
                    {
                        float num = (float)(50.0 + (double)Math.Abs(container.x) * 5.0);
                        moveToPointDeltaSpeed(vect(0f, container.y), delta, num);
                    }
                    else if (container.x < (float)(-(float)container.width + width) && (double)container.x < 0.0)
                    {
                        float num2 = (float)(50.0 + (double)Math.Abs((float)(-(float)container.width + width) - container.x) * 5.0);
                        moveToPointDeltaSpeed(vect((float)(-(float)container.width + width), container.y), delta, num2);
                    }
                }
                if (shouldBounceVertically)
                {
                    if ((double)container.y > 0.0)
                    {
                        moveToPointDeltaSpeed(vect(container.x, 0f), delta, (float)(50.0 + (double)Math.Abs(container.y) * 5.0));
                    }
                    else if (container.y < (float)(-(float)container.height + height) && (double)container.y < 0.0)
                    {
                        moveToPointDeltaSpeed(vect(container.x, (float)(-(float)container.height + height)), delta, (float)(50.0 + (double)Math.Abs((float)(-(float)container.height + height) - container.y) * 5.0));
                    }
                }
            }
            if (movingToSpoint)
            {
                Vector vector = spoints[targetSpoint];
                moveToPointDeltaSpeed(vector, delta, (float)Math.Max(100.0, (double)vectDistance(vector, vect(container.x, container.y)) * 4.0 * (double)spointMoveMultiplier));
                if (container.x == vector.x && container.y == vector.y)
                {
                    if (delegateScrollableContainerProtocol != null)
                    {
                        delegateScrollableContainerProtocol.scrollableContainerreachedScrollPoint(this, targetSpoint);
                    }
                    movingToSpoint = false;
                    targetSpoint = -1;
                    lastTargetSpoint = -1;
                    move = vectZero;
                }
            }
            else if (canSkipScrollPoints && spointsNum > 0 && !vectEqual(move, vectZero) && (double)vectLength(move) < 150.0 && targetSpoint == -1)
            {
                startMovingToSpointInDirection(move);
            }
            if (!vectEqual(move, vectZero))
            {
                vectEqual(targetPoint, vectZero);
                vect(container.x, container.y);
                Vector vector2 = vectMult(vectNeg(move), 2f);
                move = vectAdd(move, vectMult(vector2, delta));
                Vector vector3 = vectMult(move, delta);
                if ((double)Math.Abs(vector3.x) < 0.2)
                {
                    vector3.x = 0f;
                    move.x = 0f;
                }
                if ((double)Math.Abs(vector3.y) < 0.2)
                {
                    vector3.y = 0f;
                    move.y = 0f;
                }
                moveContainerBy(vector3);
            }
            if ((double)inertiaTimeoutLeft > 0.0)
            {
                inertiaTimeoutLeft -= delta;
            }
        }

        // Token: 0x0600060D RID: 1549 RVA: 0x0002E074 File Offset: 0x0002C274
        public override void show()
        {
            touchTimer = 0f;
            passTouches = false;
            touchReleaseTimer = 0f;
            move = vectZero;
            if (resetScrollOnShow)
            {
                setScroll(vectZero);
            }
        }

        // Token: 0x0600060E RID: 1550 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
        public override bool onTouchDownXY(float tx, float ty)
        {
            if (!pointInRect(tx, ty, drawX, drawY, (float)width, (float)height))
            {
                return false;
            }
            if (touchPassTimeout == 0f)
            {
                bool flag = base.onTouchDownXY(tx, ty);
                if (dontHandleTouchDownsHandledByChilds && flag)
                {
                    return true;
                }
            }
            else
            {
                touchTimer = touchPassTimeout;
                savedTouch = vect(tx, ty);
                totalDrag = vectZero;
                passTouches = false;
            }
            touchState = TOUCH_STATE.TOUCH_STATE_DOWN;
            movingByInertion = false;
            movingToSpoint = false;
            targetSpoint = -1;
            dragStart = vect(tx, ty);
            return true;
        }

        // Token: 0x0600060F RID: 1551 RVA: 0x0002E160 File Offset: 0x0002C360
        public override bool onTouchMoveXY(float tx, float ty)
        {
            if (MenuController.ep != null)
            {
                return false;
            }
            if (tx == -10000f && ty == -10000f)
            {
                return false;
            }
            if (touchPassTimeout == 0f || passTouches)
            {
                bool flag = base.onTouchMoveXY(tx, ty);
                if (dontHandleTouchMovesHandledByChilds && flag)
                {
                    return true;
                }
            }
            Vector vector = vect(tx, ty);
            if (vectEqualApproximately(dragStart, vector, 5f))
            {
                return false;
            }
            if (vectEqual(dragStart, impossibleTouch) && !pointInRect(tx, ty, drawX, drawY, (float)width, (float)height))
            {
                return false;
            }
            touchState = TOUCH_STATE.TOUCH_STATE_MOVING;
            if (!vectEqual(dragStart, impossibleTouch))
            {
                Vector vector2 = vectSub(vector, dragStart);
                dragStart = vector;
                vector2.x = FIT_TO_BOUNDARIES(vector2.x, -maxTouchMoveLength, maxTouchMoveLength);
                vector2.y = FIT_TO_BOUNDARIES(vector2.y, -maxTouchMoveLength, maxTouchMoveLength);
                totalDrag = vectAdd(totalDrag, vector2);
                if (((double)touchTimer > 0.0 || untouchChildsOnMove) && vectLength(totalDrag) > touchMoveIgnoreLength)
                {
                    touchTimer = 0f;
                    passTouches = false;
                    base.onTouchUpXY(-1f, -1f);
                }
                if (container.width <= width)
                {
                    vector2.x = 0f;
                }
                if (container.height <= height)
                {
                    vector2.y = 0f;
                }
                if (shouldBounceHorizontally && ((double)container.x > 0.0 || container.x < (float)(-(float)container.width + width)))
                {
                    vector2.x /= 2f;
                }
                if (shouldBounceVertically && ((double)container.y > 0.0 || container.y < (float)(-(float)container.height + height)))
                {
                    vector2.y /= 2f;
                }
                staticMove = moveContainerBy(vector2);
                move = vectZero;
                inertiaTimeoutLeft = inertiaTimeout;
                return true;
            }
            return false;
        }

        // Token: 0x06000610 RID: 1552 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
        public override bool onTouchUpXY(float tx, float ty)
        {
            if (touchPassTimeout == 0f || passTouches)
            {
                bool flag = base.onTouchUpXY(tx, ty);
                if (dontHandleTouchUpsHandledByChilds && flag)
                {
                    return true;
                }
            }
            if ((double)touchTimer > 0.0 && ((!movingByInertion && !movingToSpoint) || targetSpoint == spointsNum - 1 || CTRPreferences.isLiteVersion()))
            {
                bool flag2 = base.onTouchDownXY(savedTouch.x, savedTouch.y);
                touchReleaseTimer = 0.2f;
                touchTimer = 0f;
                if (dontHandleTouchDownsHandledByChilds && flag2)
                {
                    return true;
                }
            }
            if (touchState == TOUCH_STATE.TOUCH_STATE_UP)
            {
                return false;
            }
            touchState = TOUCH_STATE.TOUCH_STATE_UP;
            if ((double)inertiaTimeoutLeft > 0.0)
            {
                float num = inertiaTimeoutLeft / inertiaTimeout;
                move = vectMult(staticMove, (float)((double)num * 50.0));
                movingByInertion = true;
            }
            if (spointsNum > 0)
            {
                if (!canSkipScrollPoints)
                {
                    if (minAutoScrollToSpointLength != -1f && vectLength(move) > minAutoScrollToSpointLength)
                    {
                        startMovingToSpointInDirection(move);
                    }
                    else
                    {
                        startMovingToSpointInDirection(vectZero);
                    }
                }
                else if (vectEqual(move, vectZero))
                {
                    startMovingToSpointInDirection(vectZero);
                }
            }
            dragStart = impossibleTouch;
            return true;
        }

        // Token: 0x06000611 RID: 1553 RVA: 0x0002E560 File Offset: 0x0002C760
        public override void dealloc()
        {
            spoints = null;
            base.dealloc();
        }

        // Token: 0x06000612 RID: 1554 RVA: 0x0002E570 File Offset: 0x0002C770
        public virtual ScrollableContainer initWithWidthHeightContainer(float w, float h, BaseElement c)
        {
            if (base.init() != null)
            {
                float num = (float)Application.sharedAppSettings().getInt(5);
                fixedDelta = (float)(1.0 / (double)num);
                spoints = null;
                spointsNum = -1;
                spointsCapacity = -1;
                targetSpoint = -1;
                lastTargetSpoint = -1;
                deaccelerationSpeed = 3f;
                inertiaTimeout = 0.1f;
                scrollToPointDuration = 0.35f;
                canSkipScrollPoints = false;
                shouldBounceHorizontally = false;
                shouldBounceVertically = false;
                touchMoveIgnoreLength = 0f;
                maxTouchMoveLength = 40f;
                touchPassTimeout = 0.5f;
                minAutoScrollToSpointLength = -1f;
                resetScrollOnShow = true;
                untouchChildsOnMove = false;
                dontHandleTouchDownsHandledByChilds = false;
                dontHandleTouchMovesHandledByChilds = false;
                dontHandleTouchUpsHandledByChilds = false;
                touchTimer = 0f;
                passTouches = false;
                touchReleaseTimer = 0f;
                move = vectZero;
                container = c;
                width = (int)w;
                height = (int)h;
                container.parentAnchor = 9;
                container.parent = this;
                childs[0] = container;
                dragStart = impossibleTouch;
                touchState = TOUCH_STATE.TOUCH_STATE_UP;
            }
            return this;
        }

        // Token: 0x06000613 RID: 1555 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
        public virtual ScrollableContainer initWithWidthHeightContainerWidthHeight(float w, float h, float cw, float ch)
        {
            container = (BaseElement)new BaseElement().init();
            container.width = (int)cw;
            container.height = (int)ch;
            initWithWidthHeightContainer(w, h, container);
            return this;
        }

        // Token: 0x06000614 RID: 1556 RVA: 0x0002E71D File Offset: 0x0002C91D
        public virtual void turnScrollPointsOnWithCapacity(int n)
        {
            spointsCapacity = n;
            spoints = new Vector[spointsCapacity];
            spointsNum = 0;
        }

        // Token: 0x06000615 RID: 1557 RVA: 0x0002E73E File Offset: 0x0002C93E
        public virtual int addScrollPointAtXY(double sx, double sy)
        {
            return addScrollPointAtXY((float)sx, (float)sy);
        }

        // Token: 0x06000616 RID: 1558 RVA: 0x0002E74A File Offset: 0x0002C94A
        public virtual int addScrollPointAtXY(float sx, float sy)
        {
            addScrollPointAtXYwithID(sx, sy, spointsNum);
            return spointsNum - 1;
        }

        // Token: 0x06000617 RID: 1559 RVA: 0x0002E762 File Offset: 0x0002C962
        public virtual void addScrollPointAtXYwithID(float sx, float sy, int i)
        {
            spoints[i] = vect(-sx, -sy);
            if (i > spointsNum - 1)
            {
                spointsNum = i + 1;
            }
        }

        // Token: 0x06000618 RID: 1560 RVA: 0x0002E792 File Offset: 0x0002C992
        public virtual int getTotalScrollPoints()
        {
            return spointsNum;
        }

        // Token: 0x06000619 RID: 1561 RVA: 0x0002E79A File Offset: 0x0002C99A
        public virtual Vector getScrollPoint(int i)
        {
            return spoints[i];
        }

        // Token: 0x0600061A RID: 1562 RVA: 0x0002E7AD File Offset: 0x0002C9AD
        public virtual Vector getScroll()
        {
            return vect(-container.x, -container.y);
        }

        // Token: 0x0600061B RID: 1563 RVA: 0x0002E7CC File Offset: 0x0002C9CC
        public virtual Vector getMaxScroll()
        {
            return vect((float)(container.width - width), (float)(container.height - height));
        }

        // Token: 0x0600061C RID: 1564 RVA: 0x0002E7FC File Offset: 0x0002C9FC
        public virtual void setScroll(Vector s)
        {
            move = vectZero;
            container.x = -s.x;
            container.y = -s.y;
            movingToSpoint = false;
            targetSpoint = -1;
            lastTargetSpoint = -1;
        }

        // Token: 0x0600061D RID: 1565 RVA: 0x0002E850 File Offset: 0x0002CA50
        public virtual void placeToScrollPoint(int sp)
        {
            move = vectZero;
            container.x = spoints[sp].x;
            container.y = spoints[sp].y;
            movingToSpoint = false;
            targetSpoint = -1;
            lastTargetSpoint = sp;
            if (delegateScrollableContainerProtocol != null)
            {
                delegateScrollableContainerProtocol.scrollableContainerreachedScrollPoint(this, sp);
            }
        }

        // Token: 0x0600061E RID: 1566 RVA: 0x0002E8CA File Offset: 0x0002CACA
        public virtual void moveToScrollPointmoveMultiplier(int sp, double m)
        {
            moveToScrollPointmoveMultiplier(sp, (float)m);
        }

        // Token: 0x0600061F RID: 1567 RVA: 0x0002E8D5 File Offset: 0x0002CAD5
        public virtual void moveToScrollPointmoveMultiplier(int sp, float m)
        {
            movingToSpoint = true;
            movingByInertion = false;
            spointMoveMultiplier = m;
            targetSpoint = sp;
            lastTargetSpoint = targetSpoint;
        }

        // Token: 0x06000620 RID: 1568 RVA: 0x0002E900 File Offset: 0x0002CB00
        public virtual void calculateNearsetScrollPointInDirection(Vector d)
        {
            spointMoveDirection = d;
            int num = -1;
            float num2 = 9999999f;
            float num3 = angleTo0_360(RADIANS_TO_DEGREES(vectAngleNormalized(d)));
            Vector vector = vect(container.x, container.y);
            for (int i = 0; i < spointsNum; i++)
            {
                if ((double)spoints[i].x <= 0.0 && (spoints[i].x >= (float)(-(float)container.width + width) || (double)spoints[i].x >= 0.0) && (double)spoints[i].y <= 0.0 && (spoints[i].y >= (float)(-(float)container.height + height) || (double)spoints[i].y >= 0.0))
                {
                    float num4 = vectDistance(spoints[i], vector);
                    if (!vectEqual(d, vectZero))
                    {
                        float num5 = angleTo0_360(RADIANS_TO_DEGREES(vectAngleNormalized(vectSub(spoints[i], vector))));
                        if (Math.Abs(num5 - num3) > 90f)
                        {
                            goto IL_0187;
                        }
                    }
                    if (num4 < num2)
                    {
                        num = i;
                        num2 = num4;
                    }
                }
            IL_0187:;
            }
            if (num == -1 && !vectEqual(d, vectZero))
            {
                calculateNearsetScrollPointInDirection(vectZero);
                return;
            }
            targetSpoint = num;
            if (!canSkipScrollPoints && targetSpoint != lastTargetSpoint)
            {
                movingByInertion = false;
            }
            if (lastTargetSpoint != targetSpoint && targetSpoint != -1 && delegateScrollableContainerProtocol != null)
            {
                delegateScrollableContainerProtocol.scrollableContainerchangedTargetScrollPoint(this, targetSpoint);
            }
            float num6 = angleTo0_360(RADIANS_TO_DEGREES(vectAngleNormalized(move)));
            float num7 = angleTo0_360(RADIANS_TO_DEGREES(vectAngleNormalized(vectSub(spoints[targetSpoint], vector))));
            if (Math.Abs(angleTo0_360(num6 - num7)) < 90f)
            {
                spointMoveMultiplier = (float)Math.Max(1.0, (double)vectLength(move) / 500.0);
            }
            else
            {
                spointMoveMultiplier = 0.5f;
            }
            lastTargetSpoint = targetSpoint;
        }

        // Token: 0x06000621 RID: 1569 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
        public virtual Vector moveContainerBy(Vector off)
        {
            float num = container.x + off.x;
            float num2 = container.y + off.y;
            if (!shouldBounceHorizontally)
            {
                num = (float)Math.Min((double)Math.Max((float)(-(float)container.width + width), num), 0.0);
            }
            if (!shouldBounceVertically)
            {
                num2 = (float)Math.Min((double)Math.Max((float)(-(float)container.height + height), num2), 0.0);
            }
            Vector vector = vectSub(vect(num, num2), vect(container.x, container.y));
            container.x = num;
            container.y = num2;
            return vector;
        }

        // Token: 0x06000622 RID: 1570 RVA: 0x0002EC94 File Offset: 0x0002CE94
        public virtual void moveToPointDeltaSpeed(Vector tsp, float delta, float speed)
        {
            Vector vector = vectSub(tsp, vect(container.x, container.y));
            vector = vectNormalize(vector);
            vector = vectMult(vector, speed);
            Mover.moveVariableToTarget(ref container.x, tsp.x, Math.Abs(vector.x), delta);
            Mover.moveVariableToTarget(ref container.y, tsp.y, Math.Abs(vector.y), delta);
            targetPoint = tsp;
            move = vectZero;
        }

        // Token: 0x06000623 RID: 1571 RVA: 0x0002ED30 File Offset: 0x0002CF30
        public virtual void startMovingToSpointInDirection(Vector d)
        {
            movingToSpoint = true;
            targetSpoint = (lastTargetSpoint = -1);
            calculateNearsetScrollPointInDirection(d);
        }

        // Token: 0x04000B4D RID: 2893
        private const double DEFAULT_BOUNCE_MOVEMENT_DIVIDE = 2.0;

        // Token: 0x04000B4E RID: 2894
        private const double DEFAULT_BOUNCE_DURATION = 0.1;

        // Token: 0x04000B4F RID: 2895
        private const double DEFAULT_DEACCELERATION = 3.0;

        // Token: 0x04000B50 RID: 2896
        private const double DEFAULT_INERTIAL_TIMEOUT = 0.1;

        // Token: 0x04000B51 RID: 2897
        private const double DEFAULT_SCROLL_TO_POINT_DURATION = 0.35;

        // Token: 0x04000B52 RID: 2898
        private const double MIN_SCROLL_POINTS_MOVE = 50.0;

        // Token: 0x04000B53 RID: 2899
        private const double CALC_NEAREST_DEFAULT_TIMEOUT = 0.02;

        // Token: 0x04000B54 RID: 2900
        private const double DEFAULT_MAX_TOUCH_MOVE_LENGTH = 40.0;

        // Token: 0x04000B55 RID: 2901
        private const double DEFAULT_TOUCH_PASS_TIMEOUT = 0.5;

        // Token: 0x04000B56 RID: 2902
        private const double AUTO_RELEASE_TOUCH_TIMEOUT = 0.2;

        // Token: 0x04000B57 RID: 2903
        private const double MOVE_APPROXIMATION = 0.2;

        // Token: 0x04000B58 RID: 2904
        private const float MOVE_TOUCH_APPROXIMATION = 5f;

        // Token: 0x04000B59 RID: 2905
        public ScrollableContainerProtocol delegateScrollableContainerProtocol;

        // Token: 0x04000B5A RID: 2906
        private static readonly Vector impossibleTouch = new Vector(-1000f, -1000f);

        // Token: 0x04000B5B RID: 2907
        private BaseElement container;

        // Token: 0x04000B5C RID: 2908
        private Vector dragStart;

        // Token: 0x04000B5D RID: 2909
        private Vector staticMove;

        // Token: 0x04000B5E RID: 2910
        private Vector move;

        // Token: 0x04000B5F RID: 2911
        private bool movingByInertion;

        // Token: 0x04000B60 RID: 2912
        private float inertiaTimeoutLeft;

        // Token: 0x04000B61 RID: 2913
        private bool movingToSpoint;

        // Token: 0x04000B62 RID: 2914
        private int targetSpoint;

        // Token: 0x04000B63 RID: 2915
        private int lastTargetSpoint;

        // Token: 0x04000B64 RID: 2916
        private float spointMoveMultiplier;

        // Token: 0x04000B65 RID: 2917
        private Vector[] spoints;

        // Token: 0x04000B66 RID: 2918
        private int spointsNum;

        // Token: 0x04000B67 RID: 2919
        private int spointsCapacity;

        // Token: 0x04000B68 RID: 2920
        private Vector spointMoveDirection;

        // Token: 0x04000B69 RID: 2921
        private Vector targetPoint;

        // Token: 0x04000B6A RID: 2922
        private ScrollableContainer.TOUCH_STATE touchState;

        // Token: 0x04000B6B RID: 2923
        private float touchTimer;

        // Token: 0x04000B6C RID: 2924
        private float touchReleaseTimer;

        // Token: 0x04000B6D RID: 2925
        private Vector savedTouch;

        // Token: 0x04000B6E RID: 2926
        private Vector totalDrag;

        // Token: 0x04000B6F RID: 2927
        private bool passTouches;

        // Token: 0x04000B70 RID: 2928
        private float fixedDelta;

        // Token: 0x04000B71 RID: 2929
        private float deaccelerationSpeed;

        // Token: 0x04000B72 RID: 2930
        private float inertiaTimeout;

        // Token: 0x04000B73 RID: 2931
        private float scrollToPointDuration;

        // Token: 0x04000B74 RID: 2932
        public bool canSkipScrollPoints;

        // Token: 0x04000B75 RID: 2933
        public bool shouldBounceHorizontally;

        // Token: 0x04000B76 RID: 2934
        public bool shouldBounceVertically;

        // Token: 0x04000B77 RID: 2935
        public float touchMoveIgnoreLength;

        // Token: 0x04000B78 RID: 2936
        private float maxTouchMoveLength;

        // Token: 0x04000B79 RID: 2937
        private float touchPassTimeout;

        // Token: 0x04000B7A RID: 2938
        public bool resetScrollOnShow;

        // Token: 0x04000B7B RID: 2939
        private bool dontHandleTouchDownsHandledByChilds;

        // Token: 0x04000B7C RID: 2940
        private bool dontHandleTouchMovesHandledByChilds;

        // Token: 0x04000B7D RID: 2941
        private bool dontHandleTouchUpsHandledByChilds;

        // Token: 0x04000B7E RID: 2942
        public bool untouchChildsOnMove;

        // Token: 0x04000B7F RID: 2943
        public float minAutoScrollToSpointLength;

        // Token: 0x020000D0 RID: 208
        private enum TOUCH_STATE
        {
            // Token: 0x04000B81 RID: 2945
            TOUCH_STATE_UP,
            // Token: 0x04000B82 RID: 2946
            TOUCH_STATE_DOWN,
            // Token: 0x04000B83 RID: 2947
            TOUCH_STATE_MOVING
        }
    }
}
