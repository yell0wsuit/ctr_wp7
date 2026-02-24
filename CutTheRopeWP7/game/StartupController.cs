using System;
using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.media;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class StartupController : ViewController, ResourceMgrDelegate, MovieMgrDelegate
    {
        public override NSObject initWithParent(ViewController p)
        {
            _ = base.initWithParent(p);
            return this;
        }

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

        public override void activate()
        {
            _LOG("!!!!!!!!!!!!! activate");
            base.activate();
            StartupView startupView = (StartupView)new StartupView().initFullscreen();
            addViewwithID(startupView, 0);
            NSREL(startupView);
            moviePlaybackFinished(null);
        }

        public void onVideoBannerFinished()
        {
            Application.sharedRootController().setViewTransition(4);
            deactivate();
        }

        public override void update(float delta)
        {
            base.update(delta);
        }

        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            return true;
        }

        public void resourceLoaded(int resName)
        {
            _LOG("res loaded");
        }

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

        public override bool backButtonPressed()
        {
            return false;
        }

        private enum StartupControllerViewId
        {
            VIEW_ZEPTOLAB
        }
    }
}
