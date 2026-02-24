using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class LoadingController : ViewController, ResourceMgrDelegate
    {
        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                LoadingView loadingView = (LoadingView)new LoadingView().initFullscreen();
                addViewwithID(loadingView, 0);
                Text text = new Text().initWithFont(Application.getFont(5));
                text.setAlignment(2);
                if (LANGUAGE == Language.LANG_KO)
                {
                    text.setStringandWidth(Application.getString(1310752), 200.0);
                }
                else if (LANGUAGE == Language.LANG_IT)
                {
                    text.setStringandWidth(Application.getString(1310752), 320.0);
                }
                else
                {
                    text.setStringandWidth(Application.getString(1310752), 300.0);
                }
                text.anchor = text.parentAnchor = 18;
                _ = loadingView.addChild(text);
            }
            return this;
        }

        public override void activate()
        {
            AndroidAPI.showBanner();
            base.activate();
            LoadingView loadingView = (LoadingView)getView(0);
            loadingView.game = nextController == 0;
            showView(0);
        }

        public void resourceLoaded(int res)
        {
        }

        public void allResourcesLoaded()
        {
            if (MusicToLoad > 0)
            {
                CTRSoundMgr._playMusic(MusicToLoad);
                CTRSoundMgr._stopMusic();
                MusicToLoad = -1;
            }
            GC.Collect();
            AndroidAPI.hideBanner();
            deactivate();
        }

        public int nextController;

        public int MusicToLoad = -1;

        private enum ViewID
        {
            VIEW_LOADING
        }
    }
}
