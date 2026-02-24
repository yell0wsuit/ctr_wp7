using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    internal sealed class ScreenSizes
    {
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

        // (get) Token: 0x06000387 RID: 903 RVA: 0x000164A2 File Offset: 0x000146A2
        public static int Width2 => _width2;

        // (get) Token: 0x06000388 RID: 904 RVA: 0x000164A9 File Offset: 0x000146A9
        public static int Height2 => _height2;

        // (get) Token: 0x06000389 RID: 905 RVA: 0x000164B0 File Offset: 0x000146B0
        public static Rectangle FullScreen => _rectangle;

        private static Rectangle _rectangle;

        private static int _width;

        private static int _width2;

        private static int _height;

        private static int _height2;
    }
}
