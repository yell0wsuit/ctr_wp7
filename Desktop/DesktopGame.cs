using System;
using System.Collections.Generic;
using System.Globalization;

using ctre_wp7.ctr_commons;
using ctre_wp7.iframework;
using ctre_wp7.wp7utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctre_wp7.Desktop
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

            OpenGL.Init();
            CtrRenderer.onSurfaceCreated();
            CtrRenderer.onSurfaceChanged(
                Window.ClientBounds.Width > 0 ? Window.ClientBounds.Width : 480,
                Window.ClientBounds.Height > 0 ? Window.ClientBounds.Height : 800);
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
            int w = Window.ClientBounds.Width;
            int h = Window.ClientBounds.Height;
            if (w <= 0 || h <= 0) return;

            int portraitWidth = Math.Max(1, h * PortraitWidth / PortraitHeight);
            if (w != portraitWidth)
            {
                _resizing = true;
                _graphics.PreferredBackBufferWidth = portraitWidth;
                _graphics.PreferredBackBufferHeight = h;
                _graphics.ApplyChanges();
                _resizing = false;
                return;
            }

            CtrRenderer.onSurfaceChanged(w, h);
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
    }
}
