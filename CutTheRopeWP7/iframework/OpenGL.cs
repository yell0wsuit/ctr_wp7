using System;
using System.Collections.Generic;

using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework
{
    internal sealed class OpenGL
    {
        public static void glGenTextures(int n, object textures)
        {
        }

        public static void glBindTexture(int target, uint texture)
        {
        }

        public static void glEnable(int cap)
        {
            if (cap == 1)
            {
                s_Blend.enable();
            }
            s_glServerSideFlags[cap] = true;
        }

        public static void glDisable(int cap)
        {
            if (cap == 4)
            {
                glScissor(0.0, 0.0, FrameworkTypes.REAL_SCREEN_WIDTH, FrameworkTypes.REAL_SCREEN_HEIGHT);
            }
            if (cap == 1)
            {
                s_Blend.disable();
            }
            s_glServerSideFlags[cap] = false;
        }

        public static void glEnableClientState(int cap)
        {
            s_glClientStateFlags[cap] = true;
        }

        public static void glDisableClientState(int cap)
        {
            s_glClientStateFlags[cap] = false;
        }

        public static void glViewport(double x, double y, double width, double height)
        {
            glViewport((int)x, (int)y, (int)width, (int)height);
        }

        public static void glViewport(int x, int y, int width, int height)
        {
            s_Viewport.X = x;
            s_Viewport.Y = y;
            s_Viewport.Width = width;
            s_Viewport.Height = height;
        }

        public static void glMatrixMode(int mode)
        {
            s_glMatrixMode = mode;
        }

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

        public static void glOrthof(double left, double right, double bottom, double top, double near, double far)
        {
            s_matrixProjection = Matrix.CreateOrthographicOffCenter((float)left, (float)right, (float)bottom, (float)top, (float)near, (float)far);
        }

        public static void glPopMatrix()
        {
            if (s_matrixModelViewStack.Count > 0)
            {
                int num = s_matrixModelViewStack.Count - 1;
                s_matrixModelView = s_matrixModelViewStack[num];
                s_matrixModelViewStack.RemoveAt(num);
            }
        }

        public static void glPushMatrix()
        {
            s_matrixModelViewStack.Add(s_matrixModelView);
        }

        public static void glScalef(float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateScale(x, y, z) * s_matrixModelView;
        }

        public static void glRotatef(double angle, double x, double y, double z)
        {
            glRotatef((float)angle, (float)x, (float)y, (float)z);
        }

        public static void glRotatef(float angle, float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateRotationZ(MathHelper.ToRadians(angle)) * s_matrixModelView;
        }

        public static void glTranslatef(double x, double y, double z)
        {
            glTranslatef((float)x, (float)y, (float)z);
        }

        public static void glTranslatef(float x, float y, float z)
        {
            s_matrixModelView = Matrix.CreateTranslation(x, y, 0f) * s_matrixModelView;
        }

        public static void glBindTexture(visual.Texture2D t)
        {
            s_Texture = t;
        }

        public static void glClearColor(double red, double green, double blue, double alpha)
        {
            s_glClearColor = new Color((float)red, (float)green, (float)blue, (float)alpha);
        }

        public static void glClear(int mask_NotUsedParam)
        {
            BlendParams.applyDefault();
            WP7Singletons.GraphicsDevice.Clear(s_glClearColor);
        }

        public static void glColor4f(double red, double green, double blue, double alpha)
        {
            glColor4f((float)red, (float)green, (float)blue, (float)alpha);
        }

        public static void glColor4f(float red, float green, float blue, float alpha)
        {
            s_Color = new Color(red, green, blue, alpha);
        }

        public static void SetWhiteColor()
        {
            s_Color = Color.White;
        }

        public static void SetRopeColor()
        {
            s_Color = RopeColor;
        }

        public static void glBlendFunc(BlendingFactor sfactor, BlendingFactor dfactor)
        {
            s_Blend = new BlendParams(sfactor, dfactor);
        }

        public static void drawSegment(float x1, float y1, float x2, float y2, RGBAColor color)
        {
        }

        public static void glGenBuffers(int n, ref uint buffer)
        {
        }

        public static void glGenBuffers(int n, ref uint[] buffers)
        {
        }

        public static void glDeleteBuffers(int n, ref uint[] buffers)
        {
        }

        public static void glDeleteBuffers(int n, ref uint buffers)
        {
        }

        public static void glBindBuffer(int target, uint buffer)
        {
        }

        public static void glBufferData(int target, RGBAColor[] data, int usage)
        {
        }

        public static void glBufferData(int target, PointSprite[] data, int usage)
        {
        }

        public static void glColorPointer(int size, int type, int stride, RGBAColor[] pointer)
        {
            s_GLColorPointer = pointer;
        }

        public static void glColorPointer_setAdditive(int size)
        {
            if (!RGBAColorArray.TryGetValue(size, out s_GLColorPointer))
            {
                s_GLColorPointer = new RGBAColor[size];
                RGBAColorArray.Add(size, s_GLColorPointer);
            }
            s_GLColorPointer_additive_position = 0;
        }

        public static void glColorPointer_add(int size, int type, int stride, RGBAColor[] pointer)
        {
            pointer.CopyTo(s_GLColorPointer, s_GLColorPointer_additive_position);
            s_GLColorPointer_additive_position += pointer.Length;
        }

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

        public static void glVertexPointer_add(int size, int type, int stride, float[] pointer)
        {
            pointer.CopyTo(s_GLVertexPointer.pointer_, s_GLVertexPointer_additive_position);
            s_GLVertexPointer_additive_position += pointer.Length;
        }

        public static void glVertexPointer(int size, int type, int stride, object pointer)
        {
            s_GLVertexPointer = new GLVertexPointer(size, type, stride, pointer);
        }

        public static void glTexCoordPointer(int size, int type, int stride, object pointer)
        {
            s_GLTexCoordPointer = new GLTexCoordPointer(size, type, stride, pointer);
        }

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

        private static VertexPositionColor[] ConstructColorVertices()
        {
            if (!VertexPositionColorArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionColor[] array))
            {
                array = new VertexPositionColor[s_GLVertexPointer.Count];
                VertexPositionColorArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            Vector3 vector = default;
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                vector.Z = s_GLVertexPointer.size_ == 2 ? 0f : s_GLVertexPointer.pointer_[num++];
                Color color = s_GLColorPointer[i].toXNA();
                array[i] = new VertexPositionColor(vector, color);
            }
            return array;
        }

        private static VertexPositionColor[] ConstructCurrentColorVertices()
        {
            if (!VertexPositionColorArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionColor[] array))
            {
                array = new VertexPositionColor[s_GLVertexPointer.Count];
                VertexPositionColorArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            Vector3 vector = default;
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                vector.Z = s_GLVertexPointer.size_ == 2 ? 0f : s_GLVertexPointer.pointer_[num++];
                array[i] = new VertexPositionColor(vector, s_Color);
            }
            s_GLVertexPointer = null;
            return array;
        }

        private static short[] InitializeTriangleStripIndices(int count)
        {
            short[] array = new short[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = (short)i;
            }
            return array;
        }

        private static VertexPositionNormalTexture[] ConstructTexturedVertices()
        {
            Vector3 vector = new(0f, 0f, 1f);
            if (!VertexPositionNormalTextureArray.TryGetValue(s_GLVertexPointer.Count, out VertexPositionNormalTexture[] array))
            {
                array = new VertexPositionNormalTexture[s_GLVertexPointer.Count];
                VertexPositionNormalTextureArray.Add(s_GLVertexPointer.Count, array);
            }
            int num = 0;
            int num2 = 0;
            Vector3 vector2 = default;
            for (int i = 0; i < array.Length; i++)
            {
                vector2.X = s_GLVertexPointer.pointer_[num++];
                vector2.Y = s_GLVertexPointer.pointer_[num++];
                vector2.Z = s_GLVertexPointer.size_ == 2 ? 0f : s_GLVertexPointer.pointer_[num++];
                Vector2 vector3 = default;
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

        private static VertexPositionColorTexture[] ConstructTexturedColoredVertices(int VertexCount)
        {
            if (!VertexPositionColorTextureArray.TryGetValue(VertexCount, out VertexPositionColorTexture[] array))
            {
                array = new VertexPositionColorTexture[VertexCount];
                VertexPositionColorTextureArray.Add(VertexCount, array);
            }
            int num = 0;
            int num2 = 0;
            Vector3 vector = default;
            for (int i = 0; i < array.Length; i++)
            {
                vector.X = s_GLVertexPointer.pointer_[num++];
                vector.Y = s_GLVertexPointer.pointer_[num++];
                vector.Z = s_GLVertexPointer.size_ == 2 ? 0f : s_GLVertexPointer.pointer_[num++];
                Vector2 vector2 = default;
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

        public static void Init()
        {
            InitRasterizerState();
            s_glServerSideFlags[0] = true;
            s_glClientStateFlags[0] = true;
            s_effectTexture = new BasicEffect(WP7Singletons.GraphicsDevice)
            {
                VertexColorEnabled = false,
                TextureEnabled = true,
                View = Matrix.Identity
            };
            s_effectTextureColor = new BasicEffect(WP7Singletons.GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = true,
                View = Matrix.Identity
            };
            s_effectColor = new BasicEffect(WP7Singletons.GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = false,
                Alpha = 1f,
                Texture = null,
                View = Matrix.Identity
            };
        }

        private static BasicEffect getEffect(bool useTexture, bool useColor)
        {
            BasicEffect basicEffect = useTexture ? (useColor ? s_effectTextureColor : s_effectTexture) : s_effectColor;
            if (useTexture)
            {
                basicEffect.Alpha = s_Color.A / 255f;
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

        private static void InitRasterizerState()
        {
            s_rasterizerStateSolidColor = new RasterizerState
            {
                FillMode = FillMode.Solid,
                CullMode = CullMode.None,
                ScissorTestEnable = true
            };
            s_rasterizerStateTexture = new RasterizerState
            {
                CullMode = CullMode.None,
                ScissorTestEnable = true
            };
        }

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

        public static VertexPositionNormalTexture[] GetLastVertices_PositionNormalTexture()
        {
            return s_LastVertices_PositionNormalTexture;
        }

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

        public static void glDrawElements(int mode, int count, short[] indices)
        {
            if (mode == 7)
            {
                DrawTriangleList(0, count, indices);
            }
        }

        public static void glScissor(double x, double y, double width, double height)
        {
            glScissor((int)x, (int)y, (int)width, (int)height);
        }

        public static void glScissor(int x, int y, int width, int height)
        {
            try
            {
                Microsoft.Xna.Framework.Rectangle rectangle = new(x, y, width, height);
                WP7Singletons.GraphicsDevice.ScissorRectangle = Microsoft.Xna.Framework.Rectangle.Intersect(rectangle, ScreenRect);
            }
            catch (Exception)
            {
            }
        }

        public static void setScissorRectangle(double x, double y, double w, double h)
        {
            setScissorRectangle((float)x, (float)y, (float)w, (float)h);
        }

        public static void setScissorRectangle(float x, float y, float w, float h)
        {
            x = FrameworkTypes.transformToRealX(x);
            y = FrameworkTypes.transformToRealY(y);
            w = FrameworkTypes.transformToRealW(w);
            h = FrameworkTypes.transformToRealH(h);
            glScissor((double)x, (double)y, (double)w, (double)h);
        }

        public const int GL_TEXTURE_2D = 0;

        public const int GL_BLEND = 1;

        public const int GL_ARRAY_BUFFER = 2;

        public const int GL_DYNAMIC_DRAW = 3;

        public const int GL_SCISSOR_TEST = 4;

        public const int GL_FLOAT = 5;

        public const int GL_UNSIGNED_SHORT = 6;

        public const int GL_TRIANGLES = 7;

        public const int GL_TRIANGLE_STRIP = 8;

        public const int GL_LINE_STRIP = 9;

        public const int GL_POINTS = 10;

        public const int GL_VERTEX_ARRAY = 11;

        public const int GL_TEXTURE_COORD_ARRAY = 12;

        public const int GL_COLOR_ARRAY = 13;

        public const int GL_MODELVIEW = 14;

        public const int GL_PROJECTION = 15;

        public const int GL_TEXTURE = 16;

        public const int GL_COLOR = 17;

        private static readonly Dictionary<int, bool> s_glServerSideFlags = [];

        private static readonly Dictionary<int, bool> s_glClientStateFlags = [];

        private static Viewport s_Viewport;

        private static int s_glMatrixMode;

        private static readonly List<Matrix> s_matrixModelViewStack = [];

        private static Matrix s_matrixModelView = Matrix.Identity;

        private static Matrix s_matrixProjection = Matrix.Identity;

        private static visual.Texture2D s_Texture;

        private static Color s_glClearColor = Color.White;

        private static Color s_Color = Color.White;

        private static Color RopeColor = new(0f, 0f, 0.4f, 1f);

        private static BlendParams s_Blend = new();

        private static RGBAColor[] s_GLColorPointer;

        private static readonly Dictionary<int, RGBAColor[]> RGBAColorArray = [];

        private static int s_GLColorPointer_additive_position;

        private static int s_GLVertexPointer_additive_position;

        private static readonly Dictionary<int, float[]> FloatArray = [];

        private static GLVertexPointer s_GLVertexPointer;

        private static GLTexCoordPointer s_GLTexCoordPointer;

        private static readonly Dictionary<int, VertexPositionColor[]> VertexPositionColorArray = [];

        private static readonly Dictionary<int, VertexPositionNormalTexture[]> VertexPositionNormalTextureArray = [];

        private static readonly Dictionary<int, VertexPositionColorTexture[]> VertexPositionColorTextureArray = [];

        private static BasicEffect s_effectTexture;

        private static BasicEffect s_effectColor;

        private static BasicEffect s_effectTextureColor;

        private static RasterizerState s_rasterizerStateSolidColor;

        private static RasterizerState s_rasterizerStateTexture;

        private static VertexPositionNormalTexture[] s_LastVertices_PositionNormalTexture;

        private static Microsoft.Xna.Framework.Rectangle ScreenRect = new(0, 0, WP7Singletons.GraphicsDevice.Viewport.Width, WP7Singletons.GraphicsDevice.Viewport.Height);

        private sealed class GLVertexPointer
        {
            public GLVertexPointer(int size, int type, int stride, object pointer)
            {
                pointer_ = (pointer != null) ? ((float[])pointer) : null;
                size_ = size;
            }

            // (get) Token: 0x0600052E RID: 1326 RVA: 0x00025FBD File Offset: 0x000241BD
            public int Count
            {
                get
                {
                    return pointer_ == null || size_ == 0 ? 0 : pointer_.Length / size_;
                }
            }

            public int size_;

            public float[] pointer_;
        }

        private sealed class GLTexCoordPointer
        {
            public GLTexCoordPointer(int size, int type, int stride, object pointer)
            {
                pointer_ = (pointer != null) ? ((float[])pointer) : null;
                size_ = size;
            }

            // (get) Token: 0x06000530 RID: 1328 RVA: 0x00026003 File Offset: 0x00024203
            public int Count
            {
                get
                {
                    return pointer_ == null || size_ == 0 ? 0 : pointer_.Length / size_;
                }
            }

            public int size_;

            public float[] pointer_;
        }
    }
}
