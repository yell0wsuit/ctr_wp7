using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x020000E9 RID: 233
    internal class CTRTouchState
    {
        // Token: 0x17000019 RID: 25
        // (get) Token: 0x060006E9 RID: 1769 RVA: 0x000384D9 File Offset: 0x000366D9
        // (set) Token: 0x060006EA RID: 1770 RVA: 0x000384E1 File Offset: 0x000366E1
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x060006EB RID: 1771 RVA: 0x000384EA File Offset: 0x000366EA
        // (set) Token: 0x060006EC RID: 1772 RVA: 0x000384F2 File Offset: 0x000366F2
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x060006ED RID: 1773 RVA: 0x000384FB File Offset: 0x000366FB
        // (set) Token: 0x060006EE RID: 1774 RVA: 0x00038503 File Offset: 0x00036703
        public TouchLocationState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x060006EF RID: 1775 RVA: 0x0003850C File Offset: 0x0003670C
        // (set) Token: 0x060006F0 RID: 1776 RVA: 0x00038514 File Offset: 0x00036714
        public bool Moved
        {
            get
            {
                return this.moved;
            }
            set
            {
                this.moved = value;
            }
        }

        // Token: 0x04000C83 RID: 3203
        private int id;

        // Token: 0x04000C84 RID: 3204
        private Vector2 position;

        // Token: 0x04000C85 RID: 3205
        private TouchLocationState state;

        // Token: 0x04000C86 RID: 3206
        private bool moved;
    }
}
