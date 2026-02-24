using System;
using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

// Token: 0x020000DC RID: 220
internal class Rollbar : BaseElement
{
    // Token: 0x06000660 RID: 1632 RVA: 0x00030A87 File Offset: 0x0002EC87
    public int getIndex()
    {
        return (int)(-Math.Round(offsetY / (double)centralCellHeight) - 2.0);
    }

    // Token: 0x06000661 RID: 1633 RVA: 0x00030AA8 File Offset: 0x0002ECA8
    public Rollbar Create()
    {
        _ = base.init();
        elements = new List<BaseElement>();
        BaseElement baseElement = (BaseElement)new BaseElement().init();
        baseElement.anchor = 9;
        baseElement.parentAnchor = 9;
        Image image = Image.Image_createWithResIDQuad(409, 0);
        image.anchor = (image.parentAnchor = 9);
        Image image2 = Image.Image_createWithResIDQuad(409, 0);
        image2.anchor = (image2.parentAnchor = 12);
        image2.scaleX = -1f;
        width = (baseElement.width = image.width * 2);
        height = (baseElement.height = image.height);
        _ = baseElement.addChild(image);
        _ = baseElement.addChild(image2);
        _ = addChild(baseElement);
        scrollTop = Image.Image_createWithResIDQuad(409, 4);
        scrollTop.anchor = (scrollTop.parentAnchor = 9);
        Image.setElementPositionWithRelativeQuadOffset(scrollTop, 409, 0, 4);
        _ = addChild(scrollTop);
        scrollTop.visible = false;
        centralCellTL = Image.getRelativeQuadOffset(409, 0, 3);
        Text text = Text.createWithFontandString(5, NSS(" "));
        text.visible = false;
        text.anchor = (text.parentAnchor = 18);
        elements.Add(text);
        _ = addChild(text);
        Image image3 = Image.Image_createWithResIDQuad(409, 2);
        image3.scaleY = -1f;
        image3.visible = false;
        image3.anchor = (image3.parentAnchor = 18);
        elements.Add(image3);
        _ = addChild(image3);
        Timeline timeline = new Timeline();
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
            text.anchor = (text.parentAnchor = 18);
            elements.Add(text);
            _ = addChild(text);
        }
        image3 = Image.Image_createWithResIDQuad(409, 2);
        image3.visible = false;
        image3.anchor = (image3.parentAnchor = 18);
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
        double num = (double)scrollTop.height / 2.0 / (double)centralCellHeight;
        halfVisibleCount = (int)Math.Ceiling(num);
        offsetY = (double)(-24f * centralCellHeight);
        lastTouchY = (double)(-(double)SCREEN_HEIGHT_EXPANDED);
        BaseElement baseElement2 = (BaseElement)new BaseElement().init();
        Image.setElementPositionWithQuadCenter(baseElement2, 409, 1);
        scissorTL = vect(baseElement2.x - 20f, baseElement2.y - 80f);
        scissorWH = Image.getQuadSize(409, 1);
        return this;
    }

    // Token: 0x06000662 RID: 1634 RVA: 0x00030F0C File Offset: 0x0002F10C
    public override bool onTouchDownXY(float x, float y)
    {
        if (x < this.x || x > this.x + (float)width || y < this.y || y > this.y + (float)height)
        {
            return false;
        }
        lastTouchY = (double)y;
        lastMoveSpeed = 0.0;
        speedY = 0.0;
        manualMode = true;
        return true;
    }

    // Token: 0x06000663 RID: 1635 RVA: 0x00030F80 File Offset: 0x0002F180
    public override bool onTouchMoveXY(float x, float y)
    {
        if (lastTouchY > (double)(-(double)SCREEN_HEIGHT_EXPANDED))
        {
            preLastTouchY = lastTouchY;
            float num = (float)((double)y - lastTouchY);
            lastTouchY = (double)y;
            oldOffsetY = offsetY;
            offsetY += (double)num;
            lastMoveSpeed = (double)num / lastTimeDelta;
            speedY = 0.0;
            return true;
        }
        return false;
    }

    // Token: 0x06000664 RID: 1636 RVA: 0x00030FF8 File Offset: 0x0002F1F8
    public override bool onTouchUpXY(float x, float y)
    {
        manualMode = false;
        if (lastTouchY <= (double)(-(double)SCREEN_HEIGHT_EXPANDED))
        {
            return false;
        }
        if (preLastTouchY == lastTouchY)
        {
            lastMoveSpeed = 0.0;
        }
        speedY = lastMoveSpeed * 2.0;
        lastTouchY = (double)(-(double)SCREEN_HEIGHT_EXPANDED);
        return true;
    }

    // Token: 0x06000665 RID: 1637 RVA: 0x0003105E File Offset: 0x0002F25E
    public void scrollWithSpeed(float speed)
    {
        speedY = (double)speed;
    }

    // Token: 0x06000666 RID: 1638 RVA: 0x00031068 File Offset: 0x0002F268
    private float getCurrentScrollSpeed()
    {
        return (float)speedY;
    }

    // Token: 0x06000667 RID: 1639 RVA: 0x00031074 File Offset: 0x0002F274
    public float getOffsetY()
    {
        float num = (float)(offsetY - Math.Floor(offsetY / (double)centralCellHeight) * (double)centralCellHeight);
        if (num > centralCellHeight / 2f)
        {
            num -= centralCellHeight;
        }
        return num;
    }

    // Token: 0x06000668 RID: 1640 RVA: 0x000310C0 File Offset: 0x0002F2C0
    public override void draw()
    {
        base.draw();
        OpenGL.glEnable(4);
        OpenGL.setScissorRectangle(scissorTL.x, scissorTL.y, scissorWH.x, scissorWH.y);
        for (int i = -halfVisibleCount - 1; i < halfVisibleCount + 1; i++)
        {
            int num = (int)(offsetY / (double)centralCellHeight);
            int num2 = -num + i;
            if (num2 >= 0 && num2 < elements.Count)
            {
                BaseElement baseElement = elements[num2];
                baseElement.y = (float)((double)((float)i * centralCellHeight) + (offsetY - (double)((float)num * centralCellHeight)));
                baseElement.draw();
            }
        }
        OpenGL.glDisable(4);
        scrollTop.draw();
    }

    // Token: 0x06000669 RID: 1641 RVA: 0x00031194 File Offset: 0x0002F394
    public override void update(float delta)
    {
        base.update(delta);
        lastTimeDelta = (double)delta;
        oldOffsetY = offsetY;
        offsetY += speedY * (double)delta;
        float num = (float)(offsetY - Math.Floor(offsetY / (double)centralCellHeight) * (double)centralCellHeight);
        if (num > centralCellHeight / 2f)
        {
            num -= centralCellHeight;
        }
        if (!manualMode)
        {
            speedY -= (double)(num / 3f);
        }
        speedY *= (double)MAX(0.7f, 1f - delta * 5f);
        float num2 = (float)(offsetY + (double)((float)halfVisibleCount * centralCellHeight));
        if (num2 > 0f && !manualMode)
        {
            offsetY -= (double)(num2 * 20f * delta);
        }
        num2 = (float)((double)((float)(-(float)(elements.Count - halfVisibleCount + 1)) * centralCellHeight) - offsetY);
        if (num2 > 0f && !manualMode)
        {
            offsetY += (double)(num2 * 20f * delta);
        }
    }

    // Token: 0x04000BC6 RID: 3014
    private const int speedAccelerator = 2;

    // Token: 0x04000BC7 RID: 3015
    private const int blankSpaceTop = 2;

    // Token: 0x04000BC8 RID: 3016
    private const int blankSpaceBottom = 1;

    // Token: 0x04000BC9 RID: 3017
    private const int minAge = 1;

    // Token: 0x04000BCA RID: 3018
    private const int maxAge = 99;

    // Token: 0x04000BCB RID: 3019
    private const int defaultIdx = 24;

    // Token: 0x04000BCC RID: 3020
    private const float friction = 5f;

    // Token: 0x04000BCD RID: 3021
    private const float minFriction = 0.7f;

    // Token: 0x04000BCE RID: 3022
    private const float cellBounceSpeed = 3f;

    // Token: 0x04000BCF RID: 3023
    private const float boundReturnSpeed = 20f;

    // Token: 0x04000BD0 RID: 3024
    private double offsetY;

    // Token: 0x04000BD1 RID: 3025
    private double oldOffsetY;

    // Token: 0x04000BD2 RID: 3026
    private double speedY;

    // Token: 0x04000BD3 RID: 3027
    private double lastTouchY;

    // Token: 0x04000BD4 RID: 3028
    private double preLastTouchY;

    // Token: 0x04000BD5 RID: 3029
    private double lastTimeDelta;

    // Token: 0x04000BD6 RID: 3030
    private double lastMoveSpeed;

    // Token: 0x04000BD7 RID: 3031
    private bool manualMode;

    // Token: 0x04000BD8 RID: 3032
    private Vector scissorTL;

    // Token: 0x04000BD9 RID: 3033
    private Vector scissorWH;

    // Token: 0x04000BDA RID: 3034
    private int halfVisibleCount;

    // Token: 0x04000BDB RID: 3035
    private Vector centralCellTL;

    // Token: 0x04000BDC RID: 3036
    private float centralCellHeight;

    // Token: 0x04000BDD RID: 3037
    private BaseElement scrollTop;

    // Token: 0x04000BDE RID: 3038
    private List<BaseElement> elements;
}
