using System;

using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework
{
    // Token: 0x020000B1 RID: 177
    internal class BlendParams
    {
        // Token: 0x060004E5 RID: 1253 RVA: 0x00024DBC File Offset: 0x00022FBC
        public BlendParams()
        {
            this.defaultBlending = true;
        }

        // Token: 0x060004E6 RID: 1254 RVA: 0x00024DD2 File Offset: 0x00022FD2
        public BlendParams(BlendingFactor s, BlendingFactor d)
        {
            this.sfactor = s;
            this.dfactor = d;
            this.defaultBlending = false;
            this.enabled = true;
        }

        // Token: 0x060004E7 RID: 1255 RVA: 0x00024DFD File Offset: 0x00022FFD
        public void enable()
        {
            this.enabled = true;
        }

        // Token: 0x060004E8 RID: 1256 RVA: 0x00024E06 File Offset: 0x00023006
        public void disable()
        {
            this.enabled = false;
        }

        // Token: 0x060004E9 RID: 1257 RVA: 0x00024E0F File Offset: 0x0002300F
        public static void applyDefault()
        {
            if (BlendParams.states[0] == null)
            {
                BlendParams.states[0] = BlendState.Opaque;
            }
            WP7Singletons.GraphicsDevice.BlendState = BlendParams.states[0];
            WP7Singletons.GraphicsDevice.BlendFactor = Color.White;
        }

        // Token: 0x060004EA RID: 1258 RVA: 0x00024E48 File Offset: 0x00023048
        public void apply()
        {
            if (this.defaultBlending || !this.enabled)
            {
                if (this.lastBlend != BlendParams.BlendType.Default)
                {
                    this.lastBlend = BlendParams.BlendType.Default;
                    BlendParams.applyDefault();
                    return;
                }
            }
            else if (this.sfactor == BlendingFactor.GL_SRC_ALPHA && this.dfactor == BlendingFactor.GL_ONE_MINUS_SRC_ALPHA)
            {
                if (this.lastBlend != BlendParams.BlendType.SourceAlpha_InverseSourceAlpha)
                {
                    this.lastBlend = BlendParams.BlendType.SourceAlpha_InverseSourceAlpha;
                    if (BlendParams.states[(int)this.lastBlend] == null)
                    {
                        BlendState blendState = new BlendState();
                        blendState.AlphaSourceBlend = Blend.SourceAlpha;
                        blendState.AlphaDestinationBlend = Blend.InverseSourceAlpha;
                        blendState.ColorDestinationBlend = blendState.AlphaDestinationBlend;
                        blendState.ColorSourceBlend = blendState.AlphaSourceBlend;
                        BlendParams.states[(int)this.lastBlend] = blendState;
                    }
                    WP7Singletons.GraphicsDevice.BlendState = BlendParams.states[(int)this.lastBlend];
                    return;
                }
            }
            else if (this.sfactor == BlendingFactor.GL_ONE && this.dfactor == BlendingFactor.GL_ONE_MINUS_SRC_ALPHA)
            {
                if (this.lastBlend != BlendParams.BlendType.One_InverseSourceAlpha)
                {
                    this.lastBlend = BlendParams.BlendType.One_InverseSourceAlpha;
                    if (BlendParams.states[(int)this.lastBlend] == null)
                    {
                        BlendState blendState2 = new BlendState();
                        blendState2.AlphaSourceBlend = Blend.One;
                        blendState2.AlphaDestinationBlend = Blend.InverseSourceAlpha;
                        blendState2.ColorDestinationBlend = blendState2.AlphaDestinationBlend;
                        blendState2.ColorSourceBlend = blendState2.AlphaSourceBlend;
                        BlendParams.states[(int)this.lastBlend] = blendState2;
                    }
                    WP7Singletons.GraphicsDevice.BlendState = BlendParams.states[(int)this.lastBlend];
                    return;
                }
            }
            else if (this.sfactor == BlendingFactor.GL_SRC_ALPHA && this.dfactor == BlendingFactor.GL_ONE && this.lastBlend != BlendParams.BlendType.SourceAlpha_One)
            {
                this.lastBlend = BlendParams.BlendType.SourceAlpha_One;
                if (BlendParams.states[(int)this.lastBlend] == null)
                {
                    BlendState blendState3 = new BlendState();
                    blendState3.AlphaSourceBlend = Blend.SourceAlpha;
                    blendState3.AlphaDestinationBlend = Blend.One;
                    blendState3.ColorDestinationBlend = blendState3.AlphaDestinationBlend;
                    blendState3.ColorSourceBlend = blendState3.AlphaSourceBlend;
                    BlendParams.states[(int)this.lastBlend] = blendState3;
                }
                WP7Singletons.GraphicsDevice.BlendState = BlendParams.states[(int)this.lastBlend];
            }
        }

        // Token: 0x060004EB RID: 1259 RVA: 0x00025014 File Offset: 0x00023214
        public override string ToString()
        {
            if (!this.defaultBlending)
            {
                return string.Concat(new object[] { "BlendParams(src=", this.sfactor, ", dst=", this.dfactor, ", enabled=", this.enabled, ")" });
            }
            return "BlendParams(default)";
        }

        // Token: 0x04000A34 RID: 2612
        private static BlendState[] states = new BlendState[4];

        // Token: 0x04000A35 RID: 2613
        private BlendParams.BlendType lastBlend = BlendParams.BlendType.Unknown;

        // Token: 0x04000A36 RID: 2614
        private bool enabled;

        // Token: 0x04000A37 RID: 2615
        private bool defaultBlending;

        // Token: 0x04000A38 RID: 2616
        private BlendingFactor sfactor;

        // Token: 0x04000A39 RID: 2617
        private BlendingFactor dfactor;

        // Token: 0x020000B2 RID: 178
        private enum BlendType
        {
            // Token: 0x04000A3B RID: 2619
            Unknown = -1,
            // Token: 0x04000A3C RID: 2620
            Default,
            // Token: 0x04000A3D RID: 2621
            SourceAlpha_InverseSourceAlpha,
            // Token: 0x04000A3E RID: 2622
            One_InverseSourceAlpha,
            // Token: 0x04000A3F RID: 2623
            SourceAlpha_One
        }
    }
}
