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
            defaultBlending = true;
        }

        // Token: 0x060004E6 RID: 1254 RVA: 0x00024DD2 File Offset: 0x00022FD2
        public BlendParams(BlendingFactor s, BlendingFactor d)
        {
            sfactor = s;
            dfactor = d;
            defaultBlending = false;
            enabled = true;
        }

        // Token: 0x060004E7 RID: 1255 RVA: 0x00024DFD File Offset: 0x00022FFD
        public void enable()
        {
            enabled = true;
        }

        // Token: 0x060004E8 RID: 1256 RVA: 0x00024E06 File Offset: 0x00023006
        public void disable()
        {
            enabled = false;
        }

        // Token: 0x060004E9 RID: 1257 RVA: 0x00024E0F File Offset: 0x0002300F
        public static void applyDefault()
        {
            if (states[0] == null)
            {
                states[0] = BlendState.Opaque;
            }
            WP7Singletons.GraphicsDevice.BlendState = states[0];
            WP7Singletons.GraphicsDevice.BlendFactor = Color.White;
        }

        // Token: 0x060004EA RID: 1258 RVA: 0x00024E48 File Offset: 0x00023048
        public void apply()
        {
            if (defaultBlending || !enabled)
            {
                if (lastBlend != BlendType.Default)
                {
                    lastBlend = BlendType.Default;
                    applyDefault();
                    return;
                }
            }
            else if (sfactor == BlendingFactor.GL_SRC_ALPHA && dfactor == BlendingFactor.GL_ONE_MINUS_SRC_ALPHA)
            {
                if (lastBlend != BlendType.SourceAlpha_InverseSourceAlpha)
                {
                    lastBlend = BlendType.SourceAlpha_InverseSourceAlpha;
                    if (states[(int)lastBlend] == null)
                    {
                        BlendState blendState = new();
                        blendState.AlphaSourceBlend = Blend.SourceAlpha;
                        blendState.AlphaDestinationBlend = Blend.InverseSourceAlpha;
                        blendState.ColorDestinationBlend = blendState.AlphaDestinationBlend;
                        blendState.ColorSourceBlend = blendState.AlphaSourceBlend;
                        states[(int)lastBlend] = blendState;
                    }
                    WP7Singletons.GraphicsDevice.BlendState = states[(int)lastBlend];
                    return;
                }
            }
            else if (sfactor == BlendingFactor.GL_ONE && dfactor == BlendingFactor.GL_ONE_MINUS_SRC_ALPHA)
            {
                if (lastBlend != BlendType.One_InverseSourceAlpha)
                {
                    lastBlend = BlendType.One_InverseSourceAlpha;
                    if (states[(int)lastBlend] == null)
                    {
                        BlendState blendState2 = new();
                        blendState2.AlphaSourceBlend = Blend.One;
                        blendState2.AlphaDestinationBlend = Blend.InverseSourceAlpha;
                        blendState2.ColorDestinationBlend = blendState2.AlphaDestinationBlend;
                        blendState2.ColorSourceBlend = blendState2.AlphaSourceBlend;
                        states[(int)lastBlend] = blendState2;
                    }
                    WP7Singletons.GraphicsDevice.BlendState = states[(int)lastBlend];
                    return;
                }
            }
            else if (sfactor == BlendingFactor.GL_SRC_ALPHA && dfactor == BlendingFactor.GL_ONE && lastBlend != BlendType.SourceAlpha_One)
            {
                lastBlend = BlendType.SourceAlpha_One;
                if (states[(int)lastBlend] == null)
                {
                    BlendState blendState3 = new();
                    blendState3.AlphaSourceBlend = Blend.SourceAlpha;
                    blendState3.AlphaDestinationBlend = Blend.One;
                    blendState3.ColorDestinationBlend = blendState3.AlphaDestinationBlend;
                    blendState3.ColorSourceBlend = blendState3.AlphaSourceBlend;
                    states[(int)lastBlend] = blendState3;
                }
                WP7Singletons.GraphicsDevice.BlendState = states[(int)lastBlend];
            }
        }

        // Token: 0x060004EB RID: 1259 RVA: 0x00025014 File Offset: 0x00023214
        public override string ToString()
        {
            if (!defaultBlending)
            {
                return string.Concat(new object[] { "BlendParams(src=", sfactor, ", dst=", dfactor, ", enabled=", enabled, ")" });
            }
            return "BlendParams(default)";
        }

        // Token: 0x04000A34 RID: 2612
        private static BlendState[] states = new BlendState[4];

        // Token: 0x04000A35 RID: 2613
        private BlendType lastBlend = BlendType.Unknown;

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
