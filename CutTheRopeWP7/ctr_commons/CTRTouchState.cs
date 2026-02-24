using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.ctr_commons
{
    internal sealed class CTRTouchState
    {
        // (get) Token: 0x060006E9 RID: 1769 RVA: 0x000384D9 File Offset: 0x000366D9
        // (set) Token: 0x060006EA RID: 1770 RVA: 0x000384E1 File Offset: 0x000366E1
        public int Id
        {
            get => id; set => id = value;
        }

        // (get) Token: 0x060006EB RID: 1771 RVA: 0x000384EA File Offset: 0x000366EA
        // (set) Token: 0x060006EC RID: 1772 RVA: 0x000384F2 File Offset: 0x000366F2
        public Vector2 Position
        {
            get => position; set => position = value;
        }

        // (get) Token: 0x060006ED RID: 1773 RVA: 0x000384FB File Offset: 0x000366FB
        // (set) Token: 0x060006EE RID: 1774 RVA: 0x00038503 File Offset: 0x00036703
        public TouchLocationState State
        {
            get => state; set => state = value;
        }

        // (get) Token: 0x060006EF RID: 1775 RVA: 0x0003850C File Offset: 0x0003670C
        // (set) Token: 0x060006F0 RID: 1776 RVA: 0x00038514 File Offset: 0x00036714
        public bool Moved
        {
            get => moved; set => moved = value;
        }

        private int id;

        private Vector2 position;

        private TouchLocationState state;

        private bool moved;
    }
}
