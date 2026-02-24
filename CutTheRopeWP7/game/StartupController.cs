using System;
using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.media;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000ED RID: 237
    internal sealed class StartupController : ViewController, ResourceMgrDelegate, MovieMgrDelegate
    {
        // Token: 0x0600071F RID: 1823 RVA: 0x000393FF File Offset: 0x000375FF
        public override NSObject initWithParent(ViewController p)
        {
            _ = base.initWithParent(p);
            return this;
        }

        // Token: 0x06000720 RID: 1824 RVA: 0x0003940C File Offset: 0x0003760C
        public void moviePlaybackFinished(NSString url)
        {
            ResourceMgr resourceMgr = Application.sharedResourceMgr();
            resourceMgr.resourcesDelegate = this;
            resourceMgr.initLoading();
            resourceMgr.loadPack(PACK_COMMON);
            resourceMgr.loadPack(PACK_COMMON_IMAGES);
            resourceMgr.loadPack(PACK_MENU);
            resourceMgr.loadPack(PACK_MUSIC);
            resourceMgr.startLoading();
            showView(0);
        }

        // Token: 0x06000721 RID: 1825 RVA: 0x00039468 File Offset: 0x00037668
        public override void activate()
        {
            _LOG("!!!!!!!!!!!!! activate");
            base.activate();
            StartupView startupView = (StartupView)new StartupView().initFullscreen();
            addViewwithID(startupView, 0);
            NSREL(startupView);
            moviePlaybackFinished(null);
        }

        // Token: 0x06000722 RID: 1826 RVA: 0x000394AA File Offset: 0x000376AA
        public void onVideoBannerFinished()
        {
            Application.sharedRootController().setViewTransition(4);
            base.deactivate();
        }

        // Token: 0x06000723 RID: 1827 RVA: 0x000394BD File Offset: 0x000376BD
        public override void update(float delta)
        {
            base.update(delta);
        }

        // Token: 0x06000724 RID: 1828 RVA: 0x000394C6 File Offset: 0x000376C6
        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            return true;
        }

        // Token: 0x06000725 RID: 1829 RVA: 0x000394C9 File Offset: 0x000376C9
        public void resourceLoaded(int resName)
        {
            _LOG("res loaded");
        }

        // Token: 0x06000726 RID: 1830 RVA: 0x000394D8 File Offset: 0x000376D8
        public void allResourcesLoaded()
        {
            GC.Collect();
            _LOG("all res loaded");
            int num = Preferences._getIntForKey("PREFS_GAME_STARTS");
            Preferences._setIntforKey(num + 1, "PREFS_GAME_STARTS", false);
            if (CTRPreferences.isBannersMustBeShown())
            {
                AndroidAPI.showVideoBanner();
                return;
            }
            onVideoBannerFinished();
        }

        // Token: 0x06000727 RID: 1831 RVA: 0x00039520 File Offset: 0x00037720
        public override bool backButtonPressed()
        {
            return false;
        }

        // Token: 0x020000EE RID: 238
        private enum StartupControllerViewId
        {
            // Token: 0x04000CAA RID: 3242
            VIEW_ZEPTOLAB
        }
    }
}
