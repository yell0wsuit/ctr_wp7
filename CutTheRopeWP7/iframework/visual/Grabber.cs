using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200004D RID: 77
    internal sealed class Grabber : NSObject
    {
        // Token: 0x0600026F RID: 623 RVA: 0x0000FD1A File Offset: 0x0000DF1A
        public override NSObject init()
        {
            _ = base.init();
            return this;
        }

        // Token: 0x06000270 RID: 624 RVA: 0x0000FD24 File Offset: 0x0000DF24
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x06000271 RID: 625 RVA: 0x0000FD2C File Offset: 0x0000DF2C
        public static Texture2D grab()
        {
            return (Texture2D)new Texture2D().initFromPixels(0, 0, (int)REAL_SCREEN_WIDTH, (int)REAL_SCREEN_HEIGHT);
        }

        // Token: 0x06000272 RID: 626 RVA: 0x0000FD58 File Offset: 0x0000DF58
        public static void drawGrabbedImage(Texture2D t, int x, int y)
        {
            if (t == null)
            {
                return;
            }
            float[] array =
            [
                default(float),
                default(float),
                t._maxS,
                default(float),
                default(float),
                t._maxT,
                t._maxS,
                t._maxT
            ];
            float[] array2 = new float[12];
            array2[0] = x;
            array2[1] = y;
            array2[3] = t._realWidth + x;
            array2[4] = y;
            array2[6] = x;
            array2[7] = t._realHeight + y;
            array2[9] = t._realWidth + x;
            array2[10] = t._realHeight + y;
            float[] array3 = array2;
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(t.name());
            OpenGL.glVertexPointer(3, 5, 0, array3);
            OpenGL.glTexCoordPointer(2, 5, 0, array);
            OpenGL.glDrawArrays(8, 0, 4);
        }
    }
}
