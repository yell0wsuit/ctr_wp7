using System;
using System.Collections.Generic;

using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework
{
    // Token: 0x020000B3 RID: 179
    internal class OpenGL
    {
        // Token: 0x060004ED RID: 1261 RVA: 0x00025093 File Offset: 0x00023293
        public static void glGenTextures(int n, object textures)
        {
        }

        // Token: 0x060004EE RID: 1262 RVA: 0x00025095 File Offset: 0x00023295
        public static void glBindTexture(int target, uint texture)
        {
        }

        // Token: 0x060004EF RID: 1263 RVA: 0x00025097 File Offset: 0x00023297
        public static void glEnable(int cap)
        {
            if (cap == 1)
            {
                s_Blend.enable();
            }
            s_glServerSideFlags[cap] = true;
        }

        // Token: 0x060004F0 RID: 1264 RVA: 0x000250B8 File Offset: 0x000232B8
        public static void glDisable(int cap)
        {
            if (cap == 4)
            {
                glScissor(0.0, 0.0, (double)FrameworkTypes.REAL_SCREEN_WIDTH, (double)FrameworkTypes.REAL_SCREEN_HEIGHT);
            }
            if (cap == 1)
            {
                s_Blend.disable();
            }
            s_glServerSideFlags[cap] = false;
        }

        // Token: 0x060004F1 RID: 1265 RVA: 0x00025106 File Offset: 0x00023306
        public static void glEnableClientState(int cap)
        {
            s_glClientStateFlags[cap] = true;
        }

        // Token: 0x060004F2 RID: 1266 RVA: 0x00025114 File Offset: 0x00023314
        public static void glDisableClientState(int cap)
        {
            s_glClientStateFlags[cap] = false;
        }

        // Token: 0x060004F3 RID: 1267 RVA: 0x00025122 File Offset: 0x00023322
        public static void glViewport(double x, double y, double width, double height)
        {
            glViewport((int)x, (int)y, (int)width, (int)height);
        }

        // Token: 0x060004F4 RID: 1268 RVA: 0x00025131 File Offset: 0x00023331
        public static void glViewport(int x, int y, int width, int height)
        {
            s_Viewport.X = x;
            s_Viewport.Y = y;
            s_Viewport.Width = width;
            s_Viewport.Height = height;
        }

        // Token: 0x060004F5 RID: 1269 RVA: 0x0002515F File Offset: 0x0002335F
        public static void glMatrixMode(int mode)
        {
            s_glMatrixMode = mode;
        }

        // Token: 0x060004F6 RID: 1270 RVA: 0x00025168 File Offset: 0x00023368
        public static void glLoadIdentity()
        {
            if (s_glMatrixMode == 14)
            {
                s_matrixModelView = Matrix.Identity;
                return;
            }
            if (s_glMatrixMode == 15)
            {
                s_matrixProjection = Matrix.Identity;
                return;
            }
            if (s_glMatrixMode == 16)
            {
                throw new NotImplementedException();
            }
            if (s_glMatrixMode == 17)
            {
                throw new NotImplementedException();
            }
        }

        // Token: 0x060004F7 RID: 1271 RVA: 0x000251BB File Offset: 0x000233BB
        public static void glOrthof(double left, double right, double bottom, double top, double near, double far)
        {
            s_matrixProjection = Matrix.CreateOrthographicOffCenter((float)left, (float)right, (float)bottom, (float)top, (float)near, (float)far);
        }

        // Token: 0x060004F8 RID: 1272 RVA: 0x000251D8 File Offset: 0x000233D8
        public static void glPopMatrix()
        {
            if (s_matrixModelViewStack.Count > 0)
            {
                int num = s_matrixModelViewStack.Count - 1;
                s_matrixModelView = s_matrixModelViewStack[num];
                s_matrixModelViewStack.RemoveAt(num);
            }
        }

        // Token: 0x060004F9 RID: 1273 RVA: 0x0002521A File Offset: 0x0002341A
        public static void glPushMatrix()
        {
            s_matrixModelViewStack.Add(s_matrixModelView);
        }

        // Token: 0x060004FA RID: 1274 RVA: 0x0002522B File Offset: 0x0002342B
        public static void glScalef(float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateScale(x, y, z) * s_matrixModelView;
        }

        // Token: 0x060004FB RID: 1275 RVA: 0x00025244 File Offset: 0x00023444
        public static void glRotatef(double angle, double x, double y, double z)
        {
            glRotatef((float)angle, (float)x, (float)y, (float)z);
        }

        // Token: 0x060004FC RID: 1276 RVA: 0x00025253 File Offset: 0x00023453
        public static void glRotatef(float angle, float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateRotationZ(MathHelper.ToRadians(angle)) * s_matrixModelView;
        }

        // Token: 0x060004FD RID: 1277 RVA: 0x0002526F File Offset: 0x0002346F
        public static void glTranslatef(double x, double y, double z)
        {
            glTranslatef((float)x, (float)y, (float)z);
        }

        // Token: 0x060004FE RID: 1278 RVA: 0x0002527C File Offset: 0x0002347C
        public static void glTranslatef(float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateTranslation(x, y, 0f) * s_matrixModelView;
        }

        // Token: 0x060004FF RID: 1279 RVA: 0x00025299 File Offset: 0x00023499
        public static void glBindTexture(visual.Texture2D t)
        {
            s_Texture = t;
        }

        // Token: 0x06000500 RID: 1280 RVA: 0x000252A1 File Offset: 0x000234A1
        public static void glClearColor(double red, double green, double blue, double alpha)
        {
            s_glClearColor = new Color((float)red, (float)green, (float)blue, (float)alpha);
        }

        // Token: 0x06000501 RID: 1281 RVA: 0x000252B5 File Offset: 0x000234B5
        public static void glClear(int mask_NotUsedParam)
        {
            BlendParams.applyDefault();
            WP7Singletons.GraphicsDevice.Clear(s_glClearColor);
        }

        // Token: 0x06000502 RID: 1282 RVA: 0x000252CB File Offset: 0x000234CB
        public static void glColor4f(double red, double green, double blue, double alpha)
        {
            glColor4f((float)red, (float)green, (float)blue, (float)alpha);
        }

        // Token: 0x06000503 RID: 1283 RVA: 0x000252DA File Offset: 0x000234DA
        public static void glColor4f(float red, float green, float blue, float alpha)
        {
            s_Color = new Color(red, green, blue, alpha);
        }

        // Token: 0x06000504 RID: 1284 RVA: 0x000252EA File Offset: 0x000234EA
        public static void SetWhiteColor()
        {
            s_Color = Color.White;
        }

        // Token: 0x06000505 RID: 1285 RVA: 0x000252F6 File Offset: 0x000234F6
        public static void SetRopeColor()
        {
            s_Color = RopeColor;
        }

        // Token: 0x06000506 RID: 1286 RVA: 0x00025302 File Offset: 0x00023502
        public static void glBlendFunc(BlendingFactor sfactor, BlendingFactor dfactor)
        {
            s_Blend = new BlendParams(sfactor, dfactor);
        }

        // Token: 0x06000507 RID: 1287 RVA: 0x00025310 File Offset: 0x00023510
        public static void drawSegment(float x1, float y1, float x2, float y2, RGBAColor color)
        {
        }

        // Token: 0x06000508 RID: 1288 RVA: 0x00025312 File Offset: 0x00023512
        public static void glGenBuffers(int n, ref uint buffer)
        {
        }

        // Token: 0x06000509 RID: 1289 RVA: 0x00025314 File Offset: 0x00023514
        public static void glGenBuffers(int n, ref uint[] buffers)
        {
        }

        // Token: 0x0600050A RID: 1290 RVA: 0x00025316 File Offset: 0x00023516
        public static void glDeleteBuffers(int n, ref uint[] buffers)
        {
        }

        // Token: 0x0600050B RID: 1291 RVA: 0x00025318 File Offset: 0x00023518
        public static void glDeleteBuffers(int n, ref uint buffers)
        {
        }

        // Token: 0x0600050C RID: 1292 RVA: 0x0002531A File Offset: 0x0002351A
        public static void glBindBuffer(int target, uint buffer)
        {
        }

        // Token: 0x0600050D RID: 1293 RVA: 0x0002531C File Offset: 0x0002351C
        public static void glBufferData(int target, RGBAColor[] data, int usage)
        {
        }

        // Token: 0x0600050E RID: 1294 RVA: 0x0002531E File Offset: 0x0002351E
        public static void glBufferData(int target, PointSprite[] data, int usage)
        {
        }

        // Token: 0x0600050F RID: 1295 RVA: 0x00025320 File Offset: 0x00023520
        public static void glColorPointer(int size, int type, int stride, RGBAColor[] pointer)
        {
            s_GLColorPointer = pointer;
        }

        // Token: 0x06000510 RID: 1296 RVA: 0x00025328 File Offset: 0x00023528
        public static void glColorPointer_setAdditive(int size)
        {
            if (!RGBAColorArray.TryGetValue(size, out s_GLColorPointer))
            {
                s_GLColorPointer = new RGBAColor[size];
                RGBAColorArray.Add(size, s_GLColorPointer);
            }
            s_GLColorPointer_additive_position = 0;
        }

        // Token: 0x06000511 RID: 1297 RVA: 0x0002535D File Offset: 0x0002355D
        public static void glColorPointer_add(int size, int type, int stride, RGBAColor[] pointer)
        {
            pointer.CopyTo(s_GLColorPointer, s_GLColorPointer_additive_position);
            s_GLColorPointer_additive_position += pointer.Length;
        }

        // Token: 0x06000512 RID: 1298 RVA: 0x00025380 File Offset: 0x00023580
        public static void glVertexPointer_setAdditive(int size, int type, int stride, int length)
        {
            if (!FloatArray.TryGetValue(length, out float[] array))
            {
                array = new float[length];
                FloatArray.Add(length, array);
            }
            s_GLVertexPointer = new GLVertexPointer(size, type, stride, array);
            s_GLVertexPointer_additive_position = 0;
        }

        // Token: 0x06000513 RID: 1299 RVA: 0x000253C3 File Offset: 0x000235C3
        public static void glVertexPointer_add(int size, int type, int stride, float[] pointer)
        {
            pointer.CopyTo(s_GLVertexPointer.pointer_, s_GLVertexPointer_additive_position);
            s_GLVertexPointer_additive_position += pointer.Length;
        }

        // Token: 0x06000514 RID: 1300 RVA: 0x000253E8 File Offset: 0x000235E8
        public static void glVertexPointer(int size, int type, int stride, object pointer)
        {
            s_GLVertexPointer = new GLVertexPointer(size, type, stride, pointer);
        }

        // Token: 0x06000515 RID: 1301 RVA: 0x000253F8 File Offset: 0x000235F8
        public static void glTexCoordPointer(int size, int type, int stride, object pointer)
        {
            s_GLTexCoordPointer = new GLTexCoordPointer(size, type, stride, pointer);
        }

        // Token: 0x06000516 RID: 1302 RVA: 0x00025408 File Offset: 0x00023608
        public static void glDrawArrays(int mode, int first, int count)
        {
            if (mode == 8)
            {
                DrawTriangleStrip(first, count);
                return;
            }
            if (mode == 9)
            {
                return;
            }
            if (mode == 10)
            {
                return;
            }
            throw new NotImplementedException();
        }

        // Token: 0x06000517 RID: 1303 RVA: 0x00025428 File Offset: 0x00023628
        private static VertexPositionColor[] ConstructColorVertices()
        {
            if (!VertexPositionColorArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionColor[] array))
            {
                array = new VertexPositionColor[s_GLVertexPointer.Count];
                VertexPositionColorArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            Vector3 vector = default(Vector3);
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                if (s_GLVertexPointer.size_ == 2)
                {
                    vector.Z = 0f;
                }
                else
                {
                    vector.Z = s_GLVertexPointer.pointer_[num++];
                }
                Color color = s_GLColorPointer[i].toXNA();
                array[i] = new VertexPositionColor(vector, color);
            }
            return array;
        }

        // Token: 0x06000518 RID: 1304 RVA: 0x00025518 File Offset: 0x00023718
        private static VertexPositionColor[] ConstructCurrentColorVertices()
        {
            if (!VertexPositionColorArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionColor[] array))
            {
                array = new VertexPositionColor[s_GLVertexPointer.Count];
                VertexPositionColorArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            Vector3 vector = default(Vector3);
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                if (s_GLVertexPointer.size_ == 2)
                {
                    vector.Z = 0f;
                }
                else
                {
                    vector.Z = s_GLVertexPointer.pointer_[num++];
                }
                array[i] = new VertexPositionColor(vector, s_Color);
            }
            s_GLVertexPointer = null;
            return array;
        }

        // Token: 0x06000519 RID: 1305 RVA: 0x000255FC File Offset: 0x000237FC
        private static short[] InitializeTriangleStripIndices(int count)
        {
            short[] array = new short[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = (short)i;
            }
            return array;
        }

        // Token: 0x0600051A RID: 1306 RVA: 0x00025624 File Offset: 0x00023824
        private static VertexPositionNormalTexture[] ConstructTexturedVertices()
        {
            Vector3 vector = new Vector3(0f, 0f, 1f);
            if (!VertexPositionNormalTextureArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionNormalTexture[] array))
            {
                array = new VertexPositionNormalTexture[s_GLVertexPointer.Count];
                VertexPositionNormalTextureArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            int num2 = 0;
            Vector3 vector2 = default(Vector3);
            for (int i = 0; i < array.Length; i++)
            {
                vector2.X = s_GLVertexPointer.pointer_[num++];
                vector2.Y = s_GLVertexPointer.pointer_[num++];
                if (s_GLVertexPointer.size_ == 2)
                {
                    vector2.Z = 0f;
                }
                else
                {
                    vector2.Z = s_GLVertexPointer.pointer_[num++];
                }
                Vector2 vector3 = default(Vector2);
                vector3.X = s_GLTexCoordPointer.pointer_[num2++];
                vector3.Y = s_GLTexCoordPointer.pointer_[num2++];
                int j = 2;
                while (j < s_GLTexCoordPointer.size_)
                {
                    j++;
                    num2++;
                }
                array[i] = new VertexPositionNormalTexture(vector2, vector, vector3);
            }
            s_GLTexCoordPointer = null;
            s_GLVertexPointer = null;
            return array;
        }

        // Token: 0x0600051B RID: 1307 RVA: 0x00025780 File Offset: 0x00023980
        private static VertexPositionColorTexture[] ConstructTexturedColoredVertices(int VertexCount)
        {
            if (!VertexPositionColorTextureArray.TryGetValue(VertexCount, out VertexPositionColorTexture[] array))
            {
                array = new VertexPositionColorTexture[VertexCount];
                VertexPositionColorTextureArray.Add(VertexCount, array);
            }
            int num = 0;
            int num2 = 0;
            Vector3 vector = default(Vector3);
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                if (s_GLVertexPointer.size_ == 2)
                {
                    vector.Z = 0f;
                }
                else
                {
                    vector.Z = s_GLVertexPointer.pointer_[num++];
                }
                Vector2 vector2 = default(Vector2);
                vector2.X = s_GLTexCoordPointer.pointer_[num2++];
                vector2.Y = s_GLTexCoordPointer.pointer_[num2++];
                int j = 2;
                while (j < s_GLTexCoordPointer.size_)
                {
                    j++;
                    num2++;
                }
                Color color = s_GLColorPointer[i].toXNA();
                array[i] = new VertexPositionColorTexture(vector, color, vector2);
            }
            s_GLTexCoordPointer = null;
            s_GLVertexPointer = null;
            return array;
        }

        // Token: 0x0600051C RID: 1308 RVA: 0x000258C0 File Offset: 0x00023AC0
        public static void Init()
        {
            InitRasterizerState();
            s_glServerSideFlags[0] = true;
            s_glClientStateFlags[0] = true;
            s_effectTexture = new BasicEffect(WP7Singletons.GraphicsDevice);
            s_effectTexture.VertexColorEnabled = false;
            s_effectTexture.TextureEnabled = true;
            s_effectTexture.View = Matrix.Identity;
            s_effectTextureColor = new BasicEffect(WP7Singletons.GraphicsDevice);
            s_effectTextureColor.VertexColorEnabled = true;
            s_effectTextureColor.TextureEnabled = true;
            s_effectTextureColor.View = Matrix.Identity;
            s_effectColor = new BasicEffect(WP7Singletons.GraphicsDevice);
            s_effectColor.VertexColorEnabled = true;
            s_effectColor.TextureEnabled = false;
            s_effectColor.Alpha = 1f;
            s_effectColor.Texture = null;
            s_effectColor.View = Matrix.Identity;
        }

        // Token: 0x0600051D RID: 1309 RVA: 0x000259A0 File Offset: 0x00023BA0
        private static BasicEffect getEffect(bool useTexture, bool useColor)
        {
            BasicEffect basicEffect = useTexture ? (useColor ? s_effectTextureColor : s_effectTexture) : s_effectColor;
            if (useTexture)
            {
                basicEffect.Alpha = (float)s_Color.A / 255f;
                if (basicEffect.Alpha == 0f)
                {
                    return basicEffect;
                }
                basicEffect.Texture = s_Texture.xnaTexture_;
                basicEffect.DiffuseColor = s_Color.ToVector3();
                WP7Singletons.GraphicsDevice.RasterizerState = s_rasterizerStateTexture;
                WP7Singletons.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            }
            else
            {
                WP7Singletons.GraphicsDevice.RasterizerState = s_rasterizerStateSolidColor;
            }
            basicEffect.World = s_matrixModelView;
            basicEffect.Projection = s_matrixProjection;
            s_Blend.apply();
            return basicEffect;
        }

        // Token: 0x0600051E RID: 1310 RVA: 0x00025A68 File Offset: 0x00023C68
        private static void InitRasterizerState()
        {
            s_rasterizerStateSolidColor = new RasterizerState();
            s_rasterizerStateSolidColor.FillMode = FillMode.Solid;
            s_rasterizerStateSolidColor.CullMode = CullMode.None;
            s_rasterizerStateSolidColor.ScissorTestEnable = true;
            s_rasterizerStateTexture = new RasterizerState();
            s_rasterizerStateTexture.CullMode = CullMode.None;
            s_rasterizerStateTexture.ScissorTestEnable = true;
        }

        // Token: 0x0600051F RID: 1311 RVA: 0x00025AC0 File Offset: 0x00023CC0
        private static void DrawTriangleStrip(int first, int count)
        {
            _ = s_glServerSideFlags.TryGetValue(0, out bool flag);
            if (flag)
            {
                _ = s_glClientStateFlags.TryGetValue(0, out flag);
            }
            if (flag)
            {
                DrawTriangleStripTextured(first, count);
                return;
            }
            DrawTriangleStripColored(first, count);
        }

        // Token: 0x06000520 RID: 1312 RVA: 0x00025B00 File Offset: 0x00023D00
        private static void DrawTriangleStripColored(int first, int count)
        {
            BasicEffect effect = getEffect(false, true);
            if (effect.Alpha == 0f)
            {
                return;
            }
            _ = s_glClientStateFlags.TryGetValue(13, out bool flag);
            VertexPositionColor[] array = flag ? ConstructColorVertices() : ConstructCurrentColorVertices();
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                WP7Singletons.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, array, 0, array.Length - 2);
            }
        }

        // Token: 0x06000521 RID: 1313 RVA: 0x00025BA4 File Offset: 0x00023DA4
        private static void DrawTriangleStripTextured(int first, int count)
        {
            BasicEffect effect = getEffect(true, false);
            if (effect.Alpha == 0f)
            {
                return;
            }
            VertexPositionNormalTexture[] array = ConstructTexturedVertices();
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                WP7Singletons.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, array, 0, array.Length - 2);
            }
        }

        // Token: 0x06000522 RID: 1314 RVA: 0x00025C2C File Offset: 0x00023E2C
        public static VertexPositionNormalTexture[] GetLastVertices_PositionNormalTexture()
        {
            return s_LastVertices_PositionNormalTexture;
        }

        // Token: 0x06000523 RID: 1315 RVA: 0x00025C34 File Offset: 0x00023E34
        public static void Optimized_DrawTriangleList(VertexPositionNormalTexture[] vertices, short[] indices)
        {
            BasicEffect effect = getEffect(true, false);
            if (effect.Alpha == 0f)
            {
                return;
            }
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                WP7Singletons.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
            }
        }

        // Token: 0x06000524 RID: 1316 RVA: 0x00025CB8 File Offset: 0x00023EB8
        private static void DrawTriangleList(int first, int count, short[] indices)
        {
            _ = s_glClientStateFlags.TryGetValue(13, out bool flag);
            if (flag)
            {
                DrawTriangleListColored(first, count, indices);
                return;
            }
            BasicEffect effect = getEffect(true, false);
            if (effect.Alpha == 0f)
            {
                s_LastVertices_PositionNormalTexture = null;
                return;
            }
            VertexPositionNormalTexture[] array = ConstructTexturedVertices();
            s_LastVertices_PositionNormalTexture = array;
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                WP7Singletons.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, array, 0, array.Length, indices, 0, indices.Length / 3);
            }
        }

        // Token: 0x06000525 RID: 1317 RVA: 0x00025D6C File Offset: 0x00023F6C
        private static void DrawTriangleListColored(int first, int count, short[] indices)
        {
            if (count == 0)
            {
                return;
            }
            BasicEffect effect = getEffect(true, true);
            if (effect.Alpha == 0f)
            {
                return;
            }
            VertexPositionColorTexture[] array = ConstructTexturedColoredVertices(count / 3 * 2);
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                WP7Singletons.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, array, 0, count / 3 * 2, indices, 0, count / 3);
            }
        }

        // Token: 0x06000526 RID: 1318 RVA: 0x00025E00 File Offset: 0x00024000
        public static void glDrawElements(int mode, int count, short[] indices)
        {
            if (mode == 7)
            {
                DrawTriangleList(0, count, indices);
            }
        }

        // Token: 0x06000527 RID: 1319 RVA: 0x00025E0E File Offset: 0x0002400E
        public static void glScissor(double x, double y, double width, double height)
        {
            glScissor((int)x, (int)y, (int)width, (int)height);
        }

        // Token: 0x06000528 RID: 1320 RVA: 0x00025E20 File Offset: 0x00024020
        public static void glScissor(int x, int y, int width, int height)
        {
            try
            {
                Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(x, y, width, height);
                WP7Singletons.GraphicsDevice.ScissorRectangle = Microsoft.Xna.Framework.Rectangle.Intersect(rectangle, ScreenRect);
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000529 RID: 1321 RVA: 0x00025E64 File Offset: 0x00024064
        public static void setScissorRectangle(double x, double y, double w, double h)
        {
            setScissorRectangle((float)x, (float)y, (float)w, (float)h);
        }

        // Token: 0x0600052A RID: 1322 RVA: 0x00025E73 File Offset: 0x00024073
        public static void setScissorRectangle(float x, float y, float w, float h)
        {
            x = FrameworkTypes.transformToRealX(x);
            y = FrameworkTypes.transformToRealY(y);
            w = FrameworkTypes.transformToRealW(w);
            h = FrameworkTypes.transformToRealH(h);
            glScissor((double)x, (double)y, (double)w, (double)h);
        }

        // Token: 0x04000A40 RID: 2624
        public const int GL_TEXTURE_2D = 0;

        // Token: 0x04000A41 RID: 2625
        public const int GL_BLEND = 1;

        // Token: 0x04000A42 RID: 2626
        public const int GL_ARRAY_BUFFER = 2;

        // Token: 0x04000A43 RID: 2627
        public const int GL_DYNAMIC_DRAW = 3;

        // Token: 0x04000A44 RID: 2628
        public const int GL_SCISSOR_TEST = 4;

        // Token: 0x04000A45 RID: 2629
        public const int GL_FLOAT = 5;

        // Token: 0x04000A46 RID: 2630
        public const int GL_UNSIGNED_SHORT = 6;

        // Token: 0x04000A47 RID: 2631
        public const int GL_TRIANGLES = 7;

        // Token: 0x04000A48 RID: 2632
        public const int GL_TRIANGLE_STRIP = 8;

        // Token: 0x04000A49 RID: 2633
        public const int GL_LINE_STRIP = 9;

        // Token: 0x04000A4A RID: 2634
        public const int GL_POINTS = 10;

        // Token: 0x04000A4B RID: 2635
        public const int GL_VERTEX_ARRAY = 11;

        // Token: 0x04000A4C RID: 2636
        public const int GL_TEXTURE_COORD_ARRAY = 12;

        // Token: 0x04000A4D RID: 2637
        public const int GL_COLOR_ARRAY = 13;

        // Token: 0x04000A4E RID: 2638
        public const int GL_MODELVIEW = 14;

        // Token: 0x04000A4F RID: 2639
        public const int GL_PROJECTION = 15;

        // Token: 0x04000A50 RID: 2640
        public const int GL_TEXTURE = 16;

        // Token: 0x04000A51 RID: 2641
        public const int GL_COLOR = 17;

        // Token: 0x04000A52 RID: 2642
        private static Dictionary<int, bool> s_glServerSideFlags = [];

        // Token: 0x04000A53 RID: 2643
        private static Dictionary<int, bool> s_glClientStateFlags = [];

        // Token: 0x04000A54 RID: 2644
        private static Viewport s_Viewport;

        // Token: 0x04000A55 RID: 2645
        private static int s_glMatrixMode;

        // Token: 0x04000A56 RID: 2646
        private static List<Matrix> s_matrixModelViewStack = [];

        // Token: 0x04000A57 RID: 2647
        private static Matrix s_matrixModelView = Matrix.Identity;

        // Token: 0x04000A58 RID: 2648
        private static Matrix s_matrixProjection = Matrix.Identity;

        // Token: 0x04000A59 RID: 2649
        private static visual.Texture2D s_Texture;

        // Token: 0x04000A5A RID: 2650
        private static Color s_glClearColor = Color.White;

        // Token: 0x04000A5B RID: 2651
        private static Color s_Color = Color.White;

        // Token: 0x04000A5C RID: 2652
        private static Color RopeColor = new Color(0f, 0f, 0.4f, 1f);

        // Token: 0x04000A5D RID: 2653
        private static BlendParams s_Blend = new BlendParams();

        // Token: 0x04000A5E RID: 2654
        private static RGBAColor[] s_GLColorPointer;

        // Token: 0x04000A5F RID: 2655
        private static Dictionary<int, RGBAColor[]> RGBAColorArray = [];

        // Token: 0x04000A60 RID: 2656
        private static int s_GLColorPointer_additive_position;

        // Token: 0x04000A61 RID: 2657
        private static int s_GLVertexPointer_additive_position;

        // Token: 0x04000A62 RID: 2658
        private static Dictionary<int, float[]> FloatArray = [];

        // Token: 0x04000A63 RID: 2659
        private static GLVertexPointer s_GLVertexPointer;

        // Token: 0x04000A64 RID: 2660
        private static GLTexCoordPointer s_GLTexCoordPointer;

        // Token: 0x04000A65 RID: 2661
        private static Dictionary<int, VertexPositionColor[]> VertexPositionColorArray = [];

        // Token: 0x04000A66 RID: 2662
        private static Dictionary<int, VertexPositionNormalTexture[]> VertexPositionNormalTextureArray = [];

        // Token: 0x04000A67 RID: 2663
        private static Dictionary<int, VertexPositionColorTexture[]> VertexPositionColorTextureArray = [];

        // Token: 0x04000A68 RID: 2664
        private static BasicEffect s_effectTexture;

        // Token: 0x04000A69 RID: 2665
        private static BasicEffect s_effectColor;

        // Token: 0x04000A6A RID: 2666
        private static BasicEffect s_effectTextureColor;

        // Token: 0x04000A6B RID: 2667
        private static RasterizerState s_rasterizerStateSolidColor;

        // Token: 0x04000A6C RID: 2668
        private static RasterizerState s_rasterizerStateTexture;

        // Token: 0x04000A6D RID: 2669
        private static VertexPositionNormalTexture[] s_LastVertices_PositionNormalTexture;

        // Token: 0x04000A6E RID: 2670
        private static Microsoft.Xna.Framework.Rectangle ScreenRect = new Microsoft.Xna.Framework.Rectangle(0, 0, WP7Singletons.GraphicsDevice.Viewport.Width, WP7Singletons.GraphicsDevice.Viewport.Height);

        // Token: 0x020000B4 RID: 180
        private class GLVertexPointer
        {
            // Token: 0x0600052D RID: 1325 RVA: 0x00025F9A File Offset: 0x0002419A
            public GLVertexPointer(int size, int type, int stride, object pointer)
            {
                pointer_ = (pointer != null) ? ((float[])pointer) : null;
                size_ = size;
            }

            // Token: 0x17000012 RID: 18
            // (get) Token: 0x0600052E RID: 1326 RVA: 0x00025FBD File Offset: 0x000241BD
            public int Count
            {
                get
                {
                    if (pointer_ == null || size_ == 0)
                    {
                        return 0;
                    }
                    return pointer_.Length / size_;
                }
            }

            // Token: 0x04000A6F RID: 2671
            public int size_;

            // Token: 0x04000A70 RID: 2672
            public float[] pointer_;
        }

        // Token: 0x020000B5 RID: 181
        private class GLTexCoordPointer
        {
            // Token: 0x0600052F RID: 1327 RVA: 0x00025FE0 File Offset: 0x000241E0
            public GLTexCoordPointer(int size, int type, int stride, object pointer)
            {
                pointer_ = (pointer != null) ? ((float[])pointer) : null;
                size_ = size;
            }

            // Token: 0x17000013 RID: 19
            // (get) Token: 0x06000530 RID: 1328 RVA: 0x00026003 File Offset: 0x00024203
            public int Count
            {
                get
                {
                    if (pointer_ == null || size_ == 0)
                    {
                        return 0;
                    }
                    return pointer_.Length / size_;
                }
            }

            // Token: 0x04000A71 RID: 2673
            public int size_;

            // Token: 0x04000A72 RID: 2674
            public float[] pointer_;
        }
    }
}
