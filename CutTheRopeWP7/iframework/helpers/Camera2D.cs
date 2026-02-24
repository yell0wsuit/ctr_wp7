using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x0200003B RID: 59
    internal class Camera2D : NSObject
    {
        // Token: 0x0600020E RID: 526 RVA: 0x0000DB3C File Offset: 0x0000BD3C
        public virtual Camera2D initWithSpeedandType(float s, CAMERA_TYPE t)
        {
            if (base.init() != null)
            {
                speed = s;
                type = t;
            }
            return this;
        }

        // Token: 0x0600020F RID: 527 RVA: 0x0000DB58 File Offset: 0x0000BD58
        public virtual void moveToXYImmediate(float x, float y, bool immediate)
        {
            target.x = x;
            target.y = y;
            if (immediate)
            {
                pos = target;
                return;
            }
            if (type == CAMERA_TYPE.CAMERA_SPEED_DELAY)
            {
                offset = vectMult(vectSub(target, pos), speed);
                return;
            }
            if (type == CAMERA_TYPE.CAMERA_SPEED_PIXELS)
            {
                offset = vectMult(vectNormalize(vectSub(target, pos)), speed);
            }
        }

        // Token: 0x06000210 RID: 528 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
        public virtual void update(float delta)
        {
            if (!vectEqual(pos, target))
            {
                pos = vectAdd(pos, vectMult(offset, delta));
                pos = vect(round(pos.x), round(pos.y));
                if (!sameSign(offset.x, target.x - pos.x) || !sameSign(offset.y, target.y - pos.y))
                {
                    pos = target;
                }
            }
        }

        // Token: 0x06000211 RID: 529 RVA: 0x0000DCB3 File Offset: 0x0000BEB3
        public virtual void applyCameraTransformation()
        {
            OpenGL.glTranslatef((double)-(double)pos.x, (double)-(double)pos.y, 0.0);
        }

        // Token: 0x06000212 RID: 530 RVA: 0x0000DCDD File Offset: 0x0000BEDD
        public virtual void cancelCameraTransformation()
        {
            OpenGL.glTranslatef(pos.x, pos.y, 0.0);
        }

        // Token: 0x04000812 RID: 2066
        public CAMERA_TYPE type;

        // Token: 0x04000813 RID: 2067
        public float speed;

        // Token: 0x04000814 RID: 2068
        public Vector pos;

        // Token: 0x04000815 RID: 2069
        public Vector target;

        // Token: 0x04000816 RID: 2070
        public Vector offset;
    }
}
