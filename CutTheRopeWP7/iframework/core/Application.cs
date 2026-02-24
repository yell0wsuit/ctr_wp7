using ctr_wp7.ctr_original;
using ctr_wp7.game;
using ctr_wp7.iframework.media;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

namespace ctr_wp7.iframework.core
{
    // Token: 0x02000019 RID: 25
    internal class Application : NSObject
    {
        // Token: 0x06000129 RID: 297 RVA: 0x0000A12A File Offset: 0x0000832A
        public static CTRPreferences sharedPreferences()
        {
            return prefs;
        }

        // Token: 0x0600012A RID: 298 RVA: 0x0000A131 File Offset: 0x00008331
        public static CTRResourceMgr sharedResourceMgr()
        {
            resourceMgr ??= (CTRResourceMgr)new CTRResourceMgr().init();
            return resourceMgr;
        }

        // Token: 0x0600012B RID: 299 RVA: 0x0000A153 File Offset: 0x00008353
        public static RootController sharedRootController()
        {
            root ??= (CTRRootController)new CTRRootController().initWithParent(null);
            return root;
        }

        // Token: 0x0600012C RID: 300 RVA: 0x0000A176 File Offset: 0x00008376
        public static ApplicationSettings sharedAppSettings()
        {
            return appSettings;
        }

        // Token: 0x0600012D RID: 301 RVA: 0x0000A17D File Offset: 0x0000837D
        public static GLCanvas sharedCanvas()
        {
            return canvas;
        }

        // Token: 0x0600012E RID: 302 RVA: 0x0000A184 File Offset: 0x00008384
        public static SoundMgr sharedSoundMgr()
        {
            soundMgr ??= new SoundMgr().init();
            return soundMgr;
        }

        // Token: 0x0600012F RID: 303 RVA: 0x0000A1A1 File Offset: 0x000083A1
        public static MovieMgr sharedMovieMgr()
        {
            movieMgr ??= new MovieMgr();
            return movieMgr;
        }

        // Token: 0x06000130 RID: 304 RVA: 0x0000A1B9 File Offset: 0x000083B9
        public virtual ApplicationSettings createAppSettings()
        {
            return (ApplicationSettings)new ApplicationSettings().init();
        }

        // Token: 0x06000131 RID: 305 RVA: 0x0000A1CA File Offset: 0x000083CA
        public virtual GLCanvas createCanvas()
        {
            return new GLCanvas().initWithFrame(new Rectangle(0f, 0f, SCREEN_WIDTH, SCREEN_HEIGHT));
        }

        // Token: 0x06000132 RID: 306 RVA: 0x0000A1EF File Offset: 0x000083EF
        public virtual CTRResourceMgr createResourceMgr()
        {
            return (CTRResourceMgr)new CTRResourceMgr().init();
        }

        // Token: 0x06000133 RID: 307 RVA: 0x0000A200 File Offset: 0x00008400
        public virtual SoundMgr createSoundMgr()
        {
            return new SoundMgr().init();
        }

        // Token: 0x06000134 RID: 308 RVA: 0x0000A20C File Offset: 0x0000840C
        public virtual CTRPreferences createPreferences()
        {
            return (CTRPreferences)new CTRPreferences().init();
        }

        // Token: 0x06000135 RID: 309 RVA: 0x0000A21D File Offset: 0x0000841D
        public virtual RootController createRootController()
        {
            return (CTRRootController)new CTRRootController().initWithParent(null);
        }

        // Token: 0x06000136 RID: 310 RVA: 0x0000A230 File Offset: 0x00008430
        public virtual void applicationDidFinishLaunching(UIApplication application)
        {
            appSettings = createAppSettings();
            canvas = createCanvas();
            resourceMgr ??= createResourceMgr();
            root = createRootController();
            soundMgr = createSoundMgr();
            prefs = createPreferences();
            canvas.touchDelegate = root;
            canvas.show();
            root.activate();
        }

        // Token: 0x06000137 RID: 311 RVA: 0x0000A2A9 File Offset: 0x000084A9
        internal static FontGeneric getFont(int fontResID)
        {
            return (fontResID == 5 || fontResID == 6) && (LANGUAGE == Language.LANG_ZH || LANGUAGE == Language.LANG_KO || LANGUAGE == Language.LANG_JA)
                ? new FontWP7(fontResID)
                : (FontGeneric)sharedResourceMgr().loadResource(fontResID, ResourceMgr.ResourceType.FONT);
        }

        // Token: 0x06000138 RID: 312 RVA: 0x0000A2E3 File Offset: 0x000084E3
        internal static FontGeneric getFontEN(int fontResID)
        {
            return (FontGeneric)sharedResourceMgr().loadResource(fontResID, ResourceMgr.ResourceType.FONT);
        }

        // Token: 0x06000139 RID: 313 RVA: 0x0000A2F6 File Offset: 0x000084F6
        internal static Texture2D getTexture(int textureResID)
        {
            return (Texture2D)sharedResourceMgr().loadResource(textureResID, ResourceMgr.ResourceType.IMAGE);
        }

        // Token: 0x0600013A RID: 314 RVA: 0x0000A309 File Offset: 0x00008509
        internal static NSString getString(int strResID)
        {
            return (NSString)sharedResourceMgr().loadResource(strResID, ResourceMgr.ResourceType.STRINGS);
        }

        // Token: 0x04000764 RID: 1892
        private static CTRPreferences prefs;

        // Token: 0x04000765 RID: 1893
        private static CTRResourceMgr resourceMgr;

        // Token: 0x04000766 RID: 1894
        protected static RootController root;

        // Token: 0x04000767 RID: 1895
        private static ApplicationSettings appSettings;

        // Token: 0x04000768 RID: 1896
        private static GLCanvas canvas;

        // Token: 0x04000769 RID: 1897
        private static SoundMgr soundMgr;

        // Token: 0x0400076A RID: 1898
        private static MovieMgr movieMgr;
    }
}
