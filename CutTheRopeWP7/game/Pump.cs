using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x0200000F RID: 15
    internal class Pump : GameObject
    {
        // Token: 0x060000FB RID: 251 RVA: 0x000082F6 File Offset: 0x000064F6
        public static Pump Pump_create(Texture2D t)
        {
            return (Pump)new Pump().initWithTexture(t);
        }

        // Token: 0x060000FC RID: 252 RVA: 0x00008308 File Offset: 0x00006508
        public static Pump Pump_createWithResID(int r)
        {
            return Pump_create(Application.getTexture(r));
        }

        // Token: 0x060000FD RID: 253 RVA: 0x00008318 File Offset: 0x00006518
        public static Pump Pump_createWithResIDQuad(int r, int q)
        {
            Pump pump = Pump_create(Application.getTexture(r));
            pump.setDrawQuad(q);
            return pump;
        }

        // Token: 0x060000FE RID: 254 RVA: 0x0000833C File Offset: 0x0000653C
        public virtual void updateRotation()
        {
            t1.x = x - bb.w / 2f;
            t2.x = x + bb.w / 2f;
            t1.y = t2.y = y;
            angle = DEGREES_TO_RADIANS(rotation);
            t1 = vectRotateAround(t1, angle, x, y);
            t2 = vectRotateAround(t2, angle, x, y);
        }

        // Token: 0x04000732 RID: 1842
        public double angle;

        // Token: 0x04000733 RID: 1843
        public Vector t1;

        // Token: 0x04000734 RID: 1844
        public Vector t2;

        // Token: 0x04000735 RID: 1845
        public float pumpTouchTimer;

        // Token: 0x04000736 RID: 1846
        public int pumpTouch;

        // Token: 0x04000737 RID: 1847
        public float initial_rotation;

        // Token: 0x04000738 RID: 1848
        public float initial_x;

        // Token: 0x04000739 RID: 1849
        public float initial_y;

        // Token: 0x0400073A RID: 1850
        public RotatedCircle initial_rotatedCircle;
    }
}
