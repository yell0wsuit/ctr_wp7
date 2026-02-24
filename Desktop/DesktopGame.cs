using System;
using System.Collections.Generic;
using System.Globalization;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework;
using ctr_wp7.iframework.media;
using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.Desktop
{
    internal sealed class DesktopGame : Game
    {
        private const int PortraitWidth = 480;
        private const int PortraitHeight = 800;

        public DesktopGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferMultiSampling = false,
                SynchronizeWithVerticalRetrace = true,
                PreferredBackBufferWidth = PortraitWidth,
                PreferredBackBufferHeight = PortraitHeight,
            };

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;
            IsMouseVisible = true;
            Content.RootDirectory = "content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(16.666667);
        }

        protected override void Initialize()
        {
            WP7Singletons.GraphicsDevice = GraphicsDevice;
            WP7Singletons.Content = Content;
            SoundMgr.SetContentManager(Content);

            OpenGL.Init();
            CtrRenderer.onSurfaceCreated();
            ResizeSurfaceFromViewport();
            CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeInit(GetSystemLanguage());
            _mouseBridge = new MouseTouchBridge();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
                return;
            }

            List<TouchLocation> touches = _mouseBridge.BuildTouches();
            CtrRenderer.update((float)gameTime.ElapsedGameTime.TotalSeconds, touches);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            CtrRenderer.onDrawFrame();
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Window.ClientSizeChanged -= OnClientSizeChanged;
            }

            base.Dispose(disposing);
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (_resizing) return;

            int width = Window.ClientBounds.Width;
            int height = Window.ClientBounds.Height;
            if (width <= 0 || height <= 0) return;

            int targetWidth = width;
            int targetHeight = height;

            bool widthChanged = width != _lastClientWidth;
            bool heightChanged = height != _lastClientHeight;

            if (widthChanged)
            {
                targetHeight = ScaleHeightFromWidth(width);
            }
            else if (heightChanged)
            {
                targetWidth = ScaleWidthFromHeight(height);
            }
            else
            {
                targetHeight = ScaleHeightFromWidth(width);
            }

            if (targetWidth != width || targetHeight != height)
            {
                _resizing = true;
                _graphics.PreferredBackBufferWidth = targetWidth;
                _graphics.PreferredBackBufferHeight = targetHeight;
                _graphics.ApplyChanges();
                _resizing = false;
                _lastClientWidth = targetWidth;
                _lastClientHeight = targetHeight;
                return;
            }

            _lastClientWidth = width;
            _lastClientHeight = height;
            ResizeSurfaceFromViewport();
        }

        private static int ScaleHeightFromWidth(int width)
        {
            return Math.Max(1, (int)Math.Round(width * PortraitHeight / (double)PortraitWidth));
        }

        private static int ScaleWidthFromHeight(int height)
        {
            return Math.Max(1, (int)Math.Round(height * PortraitWidth / (double)PortraitHeight));
        }

        private void ResizeSurfaceFromViewport()
        {
            int viewportWidth = GraphicsDevice?.Viewport.Width ?? 0;
            int viewportHeight = GraphicsDevice?.Viewport.Height ?? 0;
            if (viewportWidth <= 0 || viewportHeight <= 0)
            {
                viewportWidth = Window.ClientBounds.Width > 0 ? Window.ClientBounds.Width : PortraitWidth;
                viewportHeight = Window.ClientBounds.Height > 0 ? Window.ClientBounds.Height : PortraitHeight;
            }

            CtrRenderer.onSurfaceChanged(viewportWidth, viewportHeight);
        }

        private static Language GetSystemLanguage()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            string languageCode = culture.TwoLetterISOLanguageName;

            return languageCode switch
            {
                "ru" => Language.LANG_RU,
                "de" => Language.LANG_DE,
                "fr" => Language.LANG_FR,
                "it" => Language.LANG_IT,
                "es" => Language.LANG_ES,
                "nl" => Language.LANG_NL,
                "ko" => Language.LANG_KO,
                "zh" => Language.LANG_ZH,
                "ja" => Language.LANG_JA,
                "pt" when culture.Name != "pt-PT" => Language.LANG_BR,
                _ => Language.LANG_EN
            };
        }

        private readonly GraphicsDeviceManager _graphics;
        private MouseTouchBridge _mouseBridge;
        private bool _resizing;
        private int _lastClientWidth = PortraitWidth;
        private int _lastClientHeight = PortraitHeight;
    }
}
