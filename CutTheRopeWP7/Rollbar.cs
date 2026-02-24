using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

internal sealed class Rollbar : BaseElement
{
    public int getIndex()
    {
        return (int)(-Math.Round(offsetY / centralCellHeight) - 2.0);
    }

    public Rollbar Create()
    {
        _ = init();
        elements = [];
        BaseElement baseElement = (BaseElement)new BaseElement().init();
        baseElement.anchor = 9;
        baseElement.parentAnchor = 9;
        Image image = Image.Image_createWithResIDQuad(409, 0);
        image.anchor = image.parentAnchor = 9;
        Image image2 = Image.Image_createWithResIDQuad(409, 0);
        image2.anchor = image2.parentAnchor = 12;
        image2.scaleX = -1f;
        width = baseElement.width = image.width * 2;
        height = baseElement.height = image.height;
        _ = baseElement.addChild(image);
        _ = baseElement.addChild(image2);
        _ = addChild(baseElement);
        scrollTop = Image.Image_createWithResIDQuad(409, 4);
        scrollTop.anchor = scrollTop.parentAnchor = 9;
        Image.setElementPositionWithRelativeQuadOffset(scrollTop, 409, 0, 4);
        _ = addChild(scrollTop);
        scrollTop.visible = false;
        centralCellTL = Image.getRelativeQuadOffset(409, 0, 3);
        Text text = Text.createWithFontandString(5, NSS(" "));
        text.visible = false;
        text.anchor = text.parentAnchor = 18;
        elements.Add(text);
        _ = addChild(text);
        Image image3 = Image.Image_createWithResIDQuad(409, 2);
        image3.scaleY = -1f;
        image3.visible = false;
        image3.anchor = image3.parentAnchor = 18;
        elements.Add(image3);
        _ = addChild(image3);
        Timeline timeline = new();
        _ = timeline.initWithMaxKeyFramesOnTrack(2);
        timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.8, 0.8, 0.8, 0.8), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
        timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.3, 0.3, 0.3, 0.3), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
        timeline.timelineLoopType = Timeline.LoopType.TIMELINE_PING_PONG;
        _ = image3.addTimeline(timeline);
        image3.playTimeline(0);
        for (int i = 1; i <= 99; i++)
        {
            text = Text.createWithFontandString(5, new NSString(i.ToString()));
            text.visible = false;
            text.anchor = text.parentAnchor = 18;
            elements.Add(text);
            _ = addChild(text);
        }
        image3 = Image.Image_createWithResIDQuad(409, 2);
        image3.visible = false;
        image3.anchor = image3.parentAnchor = 18;
        elements.Add(image3);
        _ = addChild(image3);
        timeline = new Timeline();
        _ = timeline.initWithMaxKeyFramesOnTrack(2);
        timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.8, 0.8, 0.8, 0.8), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
        timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.3, 0.3, 0.3, 0.3), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
        timeline.timelineLoopType = Timeline.LoopType.TIMELINE_PING_PONG;
        _ = image3.addTimeline(timeline);
        image3.playTimeline(0);
        centralCellHeight = Image.getQuadSize(409, 3).y;
        double num = scrollTop.height / 2.0 / centralCellHeight;
        halfVisibleCount = (int)Math.Ceiling(num);
        offsetY = -24f * centralCellHeight;
        lastTouchY = -(double)SCREEN_HEIGHT_EXPANDED;
        BaseElement baseElement2 = (BaseElement)new BaseElement().init();
        Image.setElementPositionWithQuadCenter(baseElement2, 409, 1);
        scissorTL = vect(baseElement2.x - 20f, baseElement2.y - 80f);
        scissorWH = Image.getQuadSize(409, 1);
        return this;
    }

    public override bool onTouchDownXY(float x, float y)
    {
        if (x < this.x || x > this.x + width || y < this.y || y > this.y + height)
        {
            return false;
        }
        lastTouchY = y;
        lastMoveSpeed = 0.0;
        speedY = 0.0;
        manualMode = true;
        return true;
    }

    public override bool onTouchMoveXY(float x, float y)
    {
        if (lastTouchY > (double)-(double)SCREEN_HEIGHT_EXPANDED)
        {
            preLastTouchY = lastTouchY;
            float num = (float)((double)y - lastTouchY);
            lastTouchY = y;
            oldOffsetY = offsetY;
            offsetY += num;
            lastMoveSpeed = (double)num / lastTimeDelta;
            speedY = 0.0;
            return true;
        }
        return false;
    }

    public override bool onTouchUpXY(float x, float y)
    {
        manualMode = false;
        if (lastTouchY <= (double)-(double)SCREEN_HEIGHT_EXPANDED)
        {
            return false;
        }
        if (preLastTouchY == lastTouchY)
        {
            lastMoveSpeed = 0.0;
        }
        speedY = lastMoveSpeed * 2.0;
        lastTouchY = -(double)SCREEN_HEIGHT_EXPANDED;
        return true;
    }

    public void scrollWithSpeed(float speed)
    {
        speedY = speed;
    }

    private float getCurrentScrollSpeed()
    {
        return (float)speedY;
    }

    public float getOffsetY()
    {
        float num = (float)(offsetY - (Math.Floor(offsetY / centralCellHeight) * centralCellHeight));
        if (num > centralCellHeight / 2f)
        {
            num -= centralCellHeight;
        }
        return num;
    }

    public override void draw()
    {
        base.draw();
        OpenGL.glEnable(4);
        OpenGL.setScissorRectangle(scissorTL.x, scissorTL.y, scissorWH.x, scissorWH.y);
        for (int i = -halfVisibleCount - 1; i < halfVisibleCount + 1; i++)
        {
            int num = (int)(offsetY / centralCellHeight);
            int num2 = -num + i;
            if (num2 >= 0 && num2 < elements.Count)
            {
                BaseElement baseElement = elements[num2];
                baseElement.y = (float)((double)(i * centralCellHeight) + (offsetY - (double)(num * centralCellHeight)));
                baseElement.draw();
            }
        }
        OpenGL.glDisable(4);
        scrollTop.draw();
    }

    public override void update(float delta)
    {
        base.update(delta);
        lastTimeDelta = delta;
        oldOffsetY = offsetY;
        offsetY += speedY * (double)delta;
        float num = (float)(offsetY - (Math.Floor(offsetY / centralCellHeight) * centralCellHeight));
        if (num > centralCellHeight / 2f)
        {
            num -= centralCellHeight;
        }
        if (!manualMode)
        {
            speedY -= num / 3f;
        }
        speedY *= MAX(0.7f, 1f - (delta * 5f));
        float num2 = (float)(offsetY + (double)(halfVisibleCount * centralCellHeight));
        if (num2 > 0f && !manualMode)
        {
            offsetY -= num2 * 20f * delta;
        }
        num2 = (float)((double)((float)-(float)(elements.Count - halfVisibleCount + 1) * centralCellHeight) - offsetY);
        if (num2 > 0f && !manualMode)
        {
            offsetY += num2 * 20f * delta;
        }
    }

    private const int speedAccelerator = 2;

    private const int blankSpaceTop = 2;

    private const int blankSpaceBottom = 1;

    private const int minAge = 1;

    private const int maxAge = 99;

    private const int defaultIdx = 24;

    private const float friction = 5f;

    private const float minFriction = 0.7f;

    private const float cellBounceSpeed = 3f;

    private const float boundReturnSpeed = 20f;

    private double offsetY;

    private double oldOffsetY;

    private double speedY;

    private double lastTouchY;

    private double preLastTouchY;

    private double lastTimeDelta;

    private double lastMoveSpeed;

    private bool manualMode;

    private Vector scissorTL;

    private Vector scissorWH;

    private int halfVisibleCount;

    private Vector centralCellTL;

    private float centralCellHeight;

    private BaseElement scrollTop;

    private List<BaseElement> elements;
}
