using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000D5 RID: 213
    internal sealed class LoadingController : ViewController, ResourceMgrDelegate
    {
        // Token: 0x0600063E RID: 1598 RVA: 0x000301B4 File Offset: 0x0002E3B4
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

        // Token: 0x0600063F RID: 1599 RVA: 0x00030278 File Offset: 0x0002E478
        public override void activate()
        {
            AndroidAPI.showBanner();
            base.activate();
            LoadingView loadingView = (LoadingView)getView(0);
            loadingView.game = nextController == 0;
            showView(0);
        }

        // Token: 0x06000640 RID: 1600 RVA: 0x000302B3 File Offset: 0x0002E4B3
        public void resourceLoaded(int res)
        {
        }

        // Token: 0x06000641 RID: 1601 RVA: 0x000302B5 File Offset: 0x0002E4B5
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

        // Token: 0x04000BA8 RID: 2984
        public int nextController;

        // Token: 0x04000BA9 RID: 2985
        public int MusicToLoad = -1;

        // Token: 0x020000D6 RID: 214
        private enum ViewID
        {
            // Token: 0x04000BAB RID: 2987
            VIEW_LOADING
        }
    }
}
