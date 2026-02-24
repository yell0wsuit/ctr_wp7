using System;
using System.IO.IsolatedStorage;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200004A RID: 74
    internal class Texture2D : NSObject
    {
        // Token: 0x06000252 RID: 594 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
        public static void drawRectAtPoint(Texture2D t, Rectangle rect, Vector point)
        {
            float num = t._invWidth * rect.x;
            float num2 = t._invHeight * rect.y;
            float num3 = num + t._invWidth * rect.w;
            float num4 = num2 + t._invHeight * rect.h;
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

        // Token: 0x06000253 RID: 595 RVA: 0x0000F41D File Offset: 0x0000D61D
        public Texture2D name()
        {
            return this;
        }

        // Token: 0x06000254 RID: 596 RVA: 0x0000F420 File Offset: 0x0000D620
        public bool isWvga()
        {
            return _isWvga;
        }

        // Token: 0x06000255 RID: 597 RVA: 0x0000F428 File Offset: 0x0000D628
        public virtual void setQuadsCapacity(int n)
        {
            quadsCount = n;
            quads = new Quad2D[quadsCount];
            quadRects = new Rectangle[quadsCount];
            quadOffsets = new Vector[quadsCount];
        }

        // Token: 0x06000256 RID: 598 RVA: 0x0000F464 File Offset: 0x0000D664
        public virtual void setQuadAt(Rectangle rect, int n)
        {
            quads[n] = GLDrawer.getTextureCoordinates(this, rect);
            quadRects[n] = rect;
            quadOffsets[n] = vectZero;
        }

        // Token: 0x06000257 RID: 599 RVA: 0x0000F4B1 File Offset: 0x0000D6B1
        public virtual void setWvga()
        {
            _isWvga = true;
        }

        // Token: 0x06000258 RID: 600 RVA: 0x0000F4BA File Offset: 0x0000D6BA
        public virtual void setScale(float scaleX, float scaleY)
        {
            _scaleX = scaleX;
            _scaleY = scaleY;
            calculateForQuickDrawing();
        }

        // Token: 0x06000259 RID: 601 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
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

        // Token: 0x0600025A RID: 602 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
        public static void drawAtPoint(Texture2D t, Vector point)
        {
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

        // Token: 0x0600025B RID: 603 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
        public virtual void calculateForQuickDrawing()
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

        // Token: 0x0600025C RID: 604 RVA: 0x0000F782 File Offset: 0x0000D982
        public static void setAntiAliasTexParameters()
        {
        }

        // Token: 0x0600025D RID: 605 RVA: 0x0000F784 File Offset: 0x0000D984
        public static void setAliasTexParameters()
        {
        }

        // Token: 0x0600025E RID: 606 RVA: 0x0000F786 File Offset: 0x0000D986
        public virtual void reg()
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

        // Token: 0x0600025F RID: 607 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
        public virtual void unreg()
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

        // Token: 0x06000260 RID: 608 RVA: 0x0000F824 File Offset: 0x0000DA24
        public virtual Texture2D initWithPath(string path, bool assets)
        {
            if (base.init() == null)
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

        // Token: 0x06000261 RID: 609 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
        private static int calcRealSize(int size)
        {
            return size;
        }

        // Token: 0x06000262 RID: 610 RVA: 0x0000F9AC File Offset: 0x0000DBAC
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

        // Token: 0x06000263 RID: 611 RVA: 0x0000FA13 File Offset: 0x0000DC13
        private void resume()
        {
        }

        // Token: 0x06000264 RID: 612 RVA: 0x0000FA15 File Offset: 0x0000DC15
        public static void setDefaultAlphaPixelFormat(Texture2DPixelFormat format)
        {
            _defaultAlphaPixelFormat = format;
        }

        // Token: 0x06000265 RID: 613 RVA: 0x0000FA1D File Offset: 0x0000DC1D
        public void optimizeMemory()
        {
        }

        // Token: 0x06000266 RID: 614 RVA: 0x0000FA28 File Offset: 0x0000DC28
        public virtual void suspend()
        {
        }

        // Token: 0x06000267 RID: 615 RVA: 0x0000FA2C File Offset: 0x0000DC2C
        public static void suspendAll()
        {
            for (Texture2D texture2D = root; texture2D != null; texture2D = texture2D.next)
            {
                texture2D.suspend();
            }
        }

        // Token: 0x06000268 RID: 616 RVA: 0x0000FA54 File Offset: 0x0000DC54
        public static void resumeAll()
        {
            for (Texture2D texture2D = root; texture2D != null; texture2D = texture2D.next)
            {
                texture2D.resume();
            }
        }

        // Token: 0x06000269 RID: 617 RVA: 0x0000FA7C File Offset: 0x0000DC7C
        public virtual NSObject initFromPixels(int x, int y, int w, int h)
        {
            if (base.init() == null)
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

        // Token: 0x0600026A RID: 618 RVA: 0x0000FB8E File Offset: 0x0000DD8E
        public override void dealloc()
        {
            if (xnaTexture_ != null)
            {
                Images.free(_resName);
                xnaTexture_ = null;
            }
        }

        // Token: 0x0600026B RID: 619 RVA: 0x0000FBAC File Offset: 0x0000DDAC
        public virtual Texture2D initWithTexture(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            if (base.init() == null)
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

        // Token: 0x0600026C RID: 620 RVA: 0x0000FC20 File Offset: 0x0000DE20
        public virtual Texture2D initWithImagePath(string path)
        {
            if (base.init() == null)
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

        // Token: 0x0400083A RID: 2106
        private const int UNDEFINED_TEXTURE = 65536;

        // Token: 0x0400083B RID: 2107
        public Microsoft.Xna.Framework.Graphics.Texture2D xnaTexture_;

        // Token: 0x0400083C RID: 2108
        public string _resName;

        // Token: 0x0400083D RID: 2109
        private uint _name;

        // Token: 0x0400083E RID: 2110
        public Quad2D[] quads;

        // Token: 0x0400083F RID: 2111
        private uint _width;

        // Token: 0x04000840 RID: 2112
        private uint _height;

        // Token: 0x04000841 RID: 2113
        public int _lowypoint;

        // Token: 0x04000842 RID: 2114
        public float _maxS;

        // Token: 0x04000843 RID: 2115
        public float _maxT;

        // Token: 0x04000844 RID: 2116
        private float _scaleX;

        // Token: 0x04000845 RID: 2117
        private float _scaleY;

        // Token: 0x04000846 RID: 2118
        private Texture2DPixelFormat _format;

        // Token: 0x04000847 RID: 2119
        private Vector _size;

        // Token: 0x04000848 RID: 2120
        private bool _hasPremultipliedAlpha;

        // Token: 0x04000849 RID: 2121
        public Vector[] quadOffsets;

        // Token: 0x0400084A RID: 2122
        public Rectangle[] quadRects;

        // Token: 0x0400084B RID: 2123
        public int quadsCount;

        // Token: 0x0400084C RID: 2124
        public int _realWidth;

        // Token: 0x0400084D RID: 2125
        public int _realHeight;

        // Token: 0x0400084E RID: 2126
        public float _invWidth;

        // Token: 0x0400084F RID: 2127
        public float _invHeight;

        // Token: 0x04000850 RID: 2128
        public Vector preCutSize;

        // Token: 0x04000851 RID: 2129
        private bool _isWvga;

        // Token: 0x04000852 RID: 2130
        private TexParams _localTexParams;

        // Token: 0x04000853 RID: 2131
        private static TexParams _defaultTexParams;

        // Token: 0x04000854 RID: 2132
        private static TexParams _texParams;

        // Token: 0x04000855 RID: 2133
        private static TexParams _texParamsCopy;

        // Token: 0x04000856 RID: 2134
        private static Texture2D root;

        // Token: 0x04000857 RID: 2135
        private static Texture2D tail;

        // Token: 0x04000858 RID: 2136
        private Texture2D next;

        // Token: 0x04000859 RID: 2137
        private Texture2D prev;

        // Token: 0x0400085A RID: 2138
        public static Texture2DPixelFormat kTexture2DPixelFormat_Default = Texture2DPixelFormat.kTexture2DPixelFormat_RGBA8888;

        // Token: 0x0400085B RID: 2139
        private static Texture2DPixelFormat _defaultAlphaPixelFormat = kTexture2DPixelFormat_Default;

        // Token: 0x0200004B RID: 75
        public enum Texture2DPixelFormat
        {
            // Token: 0x0400085D RID: 2141
            kTexture2DPixelFormat_RGBA8888,
            // Token: 0x0400085E RID: 2142
            kTexture2DPixelFormat_RGB565,
            // Token: 0x0400085F RID: 2143
            kTexture2DPixelFormat_RGBA4444,
            // Token: 0x04000860 RID: 2144
            kTexture2DPixelFormat_RGB5A1,
            // Token: 0x04000861 RID: 2145
            kTexture2DPixelFormat_A8,
            // Token: 0x04000862 RID: 2146
            kTexture2DPixelFormat_PVRTC2,
            // Token: 0x04000863 RID: 2147
            kTexture2DPixelFormat_PVRTC4
        }

        // Token: 0x0200004C RID: 76
        private struct TexParams
        {
            // Token: 0x04000864 RID: 2148
            private uint minFilter;

            // Token: 0x04000865 RID: 2149
            private uint magFilter;

            // Token: 0x04000866 RID: 2150
            private uint wrapS;

            // Token: 0x04000867 RID: 2151
            private uint wrapT;
        }
    }
}
