using System;
using System.IO.IsolatedStorage;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework.visual
{
    internal sealed class Texture2D : NSObject
    {
        public static void drawRectAtPoint(Texture2D t, Rectangle rect, Vector point)
        {
            float num = t._invWidth * rect.x;
            float num2 = t._invHeight * rect.y;
            float num3 = num + (t._invWidth * rect.w);
            float num4 = num2 + (t._invHeight * rect.h);
            float[] array = [num, num2, num3, num2, num, num4, num3, num4];
            float[] array2 = new float[12];
            array2[0] = point.x;
            array2[1] = point.y;
            array2[3] = rect.w + point.x;
            array2[4] = point.y;
            array2[6] = point.x;
            array2[7] = rect.h + point.y;
            array2[9] = rect.w + point.x;
            array2[10] = rect.h + point.y;
            float[] array3 = array2;
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(t.name());
            OpenGL.glVertexPointer(3, 5, 0, array3);
            OpenGL.glTexCoordPointer(2, 5, 0, array);
            OpenGL.glDrawArrays(8, 0, 4);
        }

        public Texture2D name()
        {
            return this;
        }

        public bool isWvga()
        {
            return _isWvga;
        }

        public void setQuadsCapacity(int n)
        {
            quadsCount = n;
            quads = new Quad2D[quadsCount];
            quadRects = new Rectangle[quadsCount];
            quadOffsets = new Vector[quadsCount];
        }

        public void setQuadAt(Rectangle rect, int n)
        {
            quads[n] = GLDrawer.getTextureCoordinates(this, rect);
            quadRects[n] = rect;
            quadOffsets[n] = vectZero;
        }

        public void setWvga()
        {
            _isWvga = true;
        }

        public void setScale(float scaleX, float scaleY)
        {
            _scaleX = scaleX;
            _scaleY = scaleY;
            calculateForQuickDrawing();
        }

        public static void drawQuadAtPoint(Texture2D t, int q, Vector point)
        {
            Quad2D quad2D = t.quads[q];
            float[] array = new float[12];
            array[0] = point.x;
            array[1] = point.y;
            array[3] = t.quadRects[q].w + point.x;
            array[4] = point.y;
            array[6] = point.x;
            array[7] = t.quadRects[q].h + point.y;
            array[9] = t.quadRects[q].w + point.x;
            array[10] = t.quadRects[q].h + point.y;
            float[] array2 = array;
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(t.name());
            OpenGL.glVertexPointer(3, 5, 0, array2);
            OpenGL.glTexCoordPointer(2, 5, 0, quad2D.toFloatArray());
            OpenGL.glDrawArrays(8, 0, 4);
        }

        public static void drawAtPoint(Texture2D t, Vector point)
        {
            float[] array =
            [
                default,
                default,
                t._maxS,
                default,
                default,
                t._maxT,
                t._maxS,
                t._maxT
            ];
            float[] array2 = new float[12];
            array2[0] = point.x;
            array2[1] = point.y;
            array2[3] = t._realWidth + point.x;
            array2[4] = point.y;
            array2[6] = point.x;
            array2[7] = t._realHeight + point.y;
            array2[9] = t._realWidth + point.x;
            array2[10] = t._realHeight + point.y;
            float[] array3 = array2;
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(t.name());
            OpenGL.glVertexPointer(3, 5, 0, array3);
            OpenGL.glTexCoordPointer(2, 5, 0, array);
            OpenGL.glDrawArrays(8, 0, 4);
        }

        public void calculateForQuickDrawing()
        {
            if (_isWvga)
            {
                _realWidth = (int)(_width * _maxS / _scaleX);
                _realHeight = (int)(_height * _maxT / _scaleY);
                _invWidth = 1f / (_width / _scaleX);
                _invHeight = 1f / (_height / _scaleY);
                return;
            }
            _realWidth = (int)(_width * _maxS);
            _realHeight = (int)(_height * _maxT);
            _invWidth = 1f / _width;
            _invHeight = 1f / _height;
        }

        public static void setAntiAliasTexParameters()
        {
        }

        public static void setAliasTexParameters()
        {
        }

        public void reg()
        {
            prev = tail;
            if (prev != null)
            {
                prev.next = this;
            }
            else
            {
                root = this;
            }
            tail = this;
        }

        public void unreg()
        {
            if (prev != null)
            {
                prev.next = next;
            }
            else
            {
                root = next;
            }
            if (next != null)
            {
                next.prev = prev;
            }
            else
            {
                tail = prev;
            }
            next = prev = null;
        }

        public Texture2D initWithPath(string path, bool assets)
        {
            if (init() == null)
            {
                return null;
            }
            _resName = path;
            _name = 65536U;
            _localTexParams = _texParams;
            reg();
            if (assets)
            {
                xnaTexture_ = Images.get(path);
                if (xnaTexture_ == null)
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            xnaTexture_ = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(WP7Singletons.GraphicsDevice, isolatedStorageFileStream);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            imageLoaded(xnaTexture_.Width, xnaTexture_.Height);
            quadsCount = 0;
            calculateForQuickDrawing();
            resume();
            return this;
        }

        private static int calcRealSize(int size)
        {
            return size;
        }

        private void imageLoaded(int w, int h)
        {
            _lowypoint = h;
            int num = calcRealSize(w);
            int num2 = calcRealSize(h);
            _size = new Vector(num, num2);
            _width = (uint)num;
            _height = (uint)num2;
            _format = _defaultAlphaPixelFormat;
            _maxS = w / (float)num;
            _maxT = h / (float)num2;
            _hasPremultipliedAlpha = true;
        }

        private static void resume()
        {
        }

        public static void setDefaultAlphaPixelFormat(Texture2DPixelFormat format)
        {
            _defaultAlphaPixelFormat = format;
        }

        public static void optimizeMemory()
        {
        }

        public static void suspend()
        {
        }

        public static void suspendAll()
        {
            for (Texture2D texture2D = root; texture2D != null; texture2D = texture2D.next)
            {
                suspend();
            }
        }

        public static void resumeAll()
        {
            for (Texture2D texture2D = root; texture2D != null; texture2D = texture2D.next)
            {
                resume();
            }
        }

        public NSObject initFromPixels(int x, int y, int w, int h)
        {
            if (init() == null)
            {
                return null;
            }
            _name = 65536U;
            _lowypoint = -1;
            _localTexParams = _defaultTexParams;
            reg();
            int num = calcRealSize(w);
            int num2 = calcRealSize(h);
            RenderTarget2D renderTarget2D = new(WP7Singletons.GraphicsDevice, WP7Singletons.GraphicsDevice.PresentationParameters.BackBufferWidth, WP7Singletons.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None);
            float transitionTime = Application.sharedRootController().transitionTime;
            Application.sharedRootController().transitionTime = -1f;
            WP7Singletons.GraphicsDevice.SetRenderTarget(renderTarget2D);
            CtrRenderer.onDrawFrame();
            WP7Singletons.GraphicsDevice.SetRenderTarget(null);
            Application.sharedRootController().transitionTime = transitionTime;
            xnaTexture_ = renderTarget2D;
            _format = Texture2DPixelFormat.kTexture2DPixelFormat_RGBA8888;
            _size = new Vector(num, num2);
            _width = (uint)num;
            _height = (uint)num2;
            _maxS = w / (float)num;
            _maxT = h / (float)num2;
            _hasPremultipliedAlpha = true;
            quadsCount = 0;
            calculateForQuickDrawing();
            resume();
            return this;
        }

        public override void dealloc()
        {
            if (xnaTexture_ != null)
            {
                Images.free(_resName);
                xnaTexture_ = null;
            }
        }

        public Texture2D initWithTexture(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            if (init() == null)
            {
                return null;
            }
            _name = 65536U;
            _localTexParams = _texParams;
            reg();
            xnaTexture_ = texture;
            if (xnaTexture_ == null)
            {
                return null;
            }
            imageLoaded(xnaTexture_.Width, xnaTexture_.Height);
            quadsCount = 0;
            calculateForQuickDrawing();
            resume();
            return this;
        }

        public Texture2D initWithImagePath(string path)
        {
            if (init() == null)
            {
                return null;
            }
            _resName = path;
            _name = 65536U;
            _localTexParams = _texParams;
            reg();
            xnaTexture_ = null;
            try
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream isolatedStorageFileStream = new(path, (System.IO.FileMode)3, userStoreForApplication))
                    {
                        xnaTexture_ = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(WP7Singletons.GraphicsDevice, isolatedStorageFileStream);
                    }
                }
            }
            catch (Exception)
            {
            }
            if (xnaTexture_ == null)
            {
                return null;
            }
            imageLoaded(xnaTexture_.Width, xnaTexture_.Height);
            quadsCount = 0;
            calculateForQuickDrawing();
            resume();
            return this;
        }

        private const int UNDEFINED_TEXTURE = 65536;

        public Microsoft.Xna.Framework.Graphics.Texture2D xnaTexture_;

        public string _resName;

        private uint _name;

        public Quad2D[] quads;

        private uint _width;

        private uint _height;

        public int _lowypoint;

        public float _maxS;

        public float _maxT;

        private float _scaleX;

        private float _scaleY;

        private Texture2DPixelFormat _format;

        private Vector _size;

        private bool _hasPremultipliedAlpha;

        public Vector[] quadOffsets;

        public Rectangle[] quadRects;

        public int quadsCount;

        public int _realWidth;

        public int _realHeight;

        public float _invWidth;

        public float _invHeight;

        public Vector preCutSize;

        private bool _isWvga;

        private TexParams _localTexParams;

        private static TexParams _defaultTexParams;

        private static TexParams _texParams;

        private static TexParams _texParamsCopy;

        private static Texture2D root;

        private static Texture2D tail;

        private Texture2D next;

        private Texture2D prev;

        public static Texture2DPixelFormat kTexture2DPixelFormat_Default = Texture2DPixelFormat.kTexture2DPixelFormat_RGBA8888;

        private static Texture2DPixelFormat _defaultAlphaPixelFormat = kTexture2DPixelFormat_Default;

        public enum Texture2DPixelFormat
        {
            kTexture2DPixelFormat_RGBA8888,
            kTexture2DPixelFormat_RGB565,
            kTexture2DPixelFormat_RGBA4444,
            kTexture2DPixelFormat_RGB5A1,
            kTexture2DPixelFormat_A8,
            kTexture2DPixelFormat_PVRTC2,
            kTexture2DPixelFormat_PVRTC4
        }

        private struct TexParams
        {
            private readonly uint minFilter;

            private readonly uint magFilter;

            private readonly uint wrapS;

            private readonly uint wrapT;
        }
    }
}
