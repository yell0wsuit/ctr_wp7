using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    // Token: 0x02000076 RID: 118
    internal sealed class ScreenSizes
    {
        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000383 RID: 899 RVA: 0x0001644E File Offset: 0x0001464E
        // (set) Token: 0x06000384 RID: 900 RVA: 0x00016455 File Offset: 0x00014655
        public static int Width
        {
            get => _width;
            set
            {
                _width = value;
                _width2 = _width / 2;
                _rectangle.Width = _width;
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000385 RID: 901 RVA: 0x00016478 File Offset: 0x00014678
        // (set) Token: 0x06000386 RID: 902 RVA: 0x0001647F File Offset: 0x0001467F
        public static int Height
        {
            get => _height;
            set
            {
                _height = value;
                _height2 = _height / 2;
                _rectangle.Height = _height;
            }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000387 RID: 903 RVA: 0x000164A2 File Offset: 0x000146A2
        public static int Width2 => _width2;

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x06000388 RID: 904 RVA: 0x000164A9 File Offset: 0x000146A9
        public static int Height2 => _height2;

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000389 RID: 905 RVA: 0x000164B0 File Offset: 0x000146B0
        public static Rectangle FullScreen => _rectangle;

        // Token: 0x04000900 RID: 2304
        private static Rectangle _rectangle;

        // Token: 0x04000901 RID: 2305
        private static int _width;

        // Token: 0x04000902 RID: 2306
        private static int _width2;

        // Token: 0x04000903 RID: 2307
        private static int _height;

        // Token: 0x04000904 RID: 2308
        private static int _height2;
    }
}
