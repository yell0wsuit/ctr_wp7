using System;
using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.iframework.core
{
    // Token: 0x02000066 RID: 102
    internal class ViewController : NSObject, TouchDelegate
    {
        // Token: 0x060002FF RID: 767 RVA: 0x000138A8 File Offset: 0x00011AA8
        public ViewController()
        {
            views = new Dictionary<int, View>();
        }

        // Token: 0x06000300 RID: 768 RVA: 0x000138BC File Offset: 0x00011ABC
        public virtual NSObject initWithParent(ViewController p)
        {
            if (base.init() != null)
            {
                controllerState = ControllerState.CONTROLLER_DEACTIVE;
                views = new Dictionary<int, View>();
                childs = new Dictionary<int, ViewController>();
                activeViewID = -1;
                activeChildID = -1;
                pausedViewID = -1;
                parent = p;
            }
            return this;
        }

        // Token: 0x06000301 RID: 769 RVA: 0x0001390B File Offset: 0x00011B0B
        public virtual void activate()
        {
            controllerState = ControllerState.CONTROLLER_ACTIVE;
            Application.sharedRootController().onControllerActivated(this);
        }

        // Token: 0x06000302 RID: 770 RVA: 0x0001391F File Offset: 0x00011B1F
        public virtual void deactivate()
        {
            Application.sharedRootController().onControllerDeactivationRequest(this);
        }

        // Token: 0x06000303 RID: 771 RVA: 0x0001392C File Offset: 0x00011B2C
        public virtual void deactivateImmediately()
        {
            controllerState = ControllerState.CONTROLLER_DEACTIVE;
            if (activeViewID != -1)
            {
                hideActiveView();
            }
            Application.sharedRootController().onControllerDeactivated(this);
            parent.onChildDeactivated(parent.activeChildID);
        }

        // Token: 0x06000304 RID: 772 RVA: 0x00013965 File Offset: 0x00011B65
        public virtual void pause()
        {
            controllerState = ControllerState.CONTROLLER_PAUSED;
            Application.sharedRootController().onControllerPaused(this);
            if (activeViewID != -1)
            {
                pausedViewID = activeViewID;
                hideActiveView();
            }
        }

        // Token: 0x06000305 RID: 773 RVA: 0x00013994 File Offset: 0x00011B94
        public virtual void unpause()
        {
            controllerState = ControllerState.CONTROLLER_ACTIVE;
            if (activeChildID != -1)
            {
                activeChildID = -1;
            }
            Application.sharedRootController().onControllerUnpaused(this);
            if (pausedViewID != -1)
            {
                showView(pausedViewID);
            }
        }

        // Token: 0x06000306 RID: 774 RVA: 0x000139D0 File Offset: 0x00011BD0
        public virtual void update(float delta)
        {
            if (activeViewID == -1)
            {
                return;
            }
            View view = activeView();
            view.update(delta);
        }

        // Token: 0x06000307 RID: 775 RVA: 0x000139F8 File Offset: 0x00011BF8
        public virtual void addViewwithID(View v, int n)
        {
            View view;
            _ = views.TryGetValue(n, out view);
            views[n] = v;
        }

        // Token: 0x06000308 RID: 776 RVA: 0x00013A21 File Offset: 0x00011C21
        public virtual void deleteView(int n)
        {
            views[n] = null;
        }

        // Token: 0x06000309 RID: 777 RVA: 0x00013A30 File Offset: 0x00011C30
        public virtual void hideActiveView()
        {
            View view = views[activeViewID];
            Application.sharedRootController().onControllerViewHide(view);
            if (view != null)
            {
                view.hide();
            }
            activeViewID = -1;
        }

        // Token: 0x0600030A RID: 778 RVA: 0x00013A6C File Offset: 0x00011C6C
        public virtual void showView(int n)
        {
            if (activeViewID != -1)
            {
                hideActiveView();
            }
            activeViewID = n;
            View view = views[n];
            Application.sharedRootController().onControllerViewShow(view);
            view.show();
        }

        // Token: 0x0600030B RID: 779 RVA: 0x00013AB0 File Offset: 0x00011CB0
        public virtual View activeView()
        {
            return views[activeViewID];
        }

        // Token: 0x0600030C RID: 780 RVA: 0x00013AD0 File Offset: 0x00011CD0
        public virtual View getView(int n)
        {
            View view = null;
            _ = views.TryGetValue(n, out view);
            return view;
        }

        // Token: 0x0600030D RID: 781 RVA: 0x00013AF0 File Offset: 0x00011CF0
        public virtual void addChildwithID(ViewController c, int n)
        {
            ViewController viewController = null;
            if (viewController != null)
            {
                viewController.dealloc();
            }
            childs[n] = c;
        }

        // Token: 0x0600030E RID: 782 RVA: 0x00013B18 File Offset: 0x00011D18
        public virtual void deleteChild(int n)
        {
            ViewController viewController = null;
            if (childs.TryGetValue(n, out viewController))
            {
                viewController.dealloc();
                childs[n] = null;
            }
        }

        // Token: 0x0600030F RID: 783 RVA: 0x00013B4C File Offset: 0x00011D4C
        public virtual void deactivateActiveChild()
        {
            ViewController viewController = childs[activeChildID];
            viewController.deactivate();
            activeChildID = -1;
        }

        // Token: 0x06000310 RID: 784 RVA: 0x00013B78 File Offset: 0x00011D78
        public virtual void activateChild(int n)
        {
            if (activeChildID != -1)
            {
                deactivateActiveChild();
            }
            pause();
            activeChildID = n;
            ViewController viewController = childs[n];
            viewController.activate();
        }

        // Token: 0x06000311 RID: 785 RVA: 0x00013BB4 File Offset: 0x00011DB4
        public virtual void onChildDeactivated(int n)
        {
            unpause();
        }

        // Token: 0x06000312 RID: 786 RVA: 0x00013BBC File Offset: 0x00011DBC
        public virtual ViewController activeChild()
        {
            return childs[activeChildID];
        }

        // Token: 0x06000313 RID: 787 RVA: 0x00013BDC File Offset: 0x00011DDC
        public virtual ViewController getChild(int n)
        {
            return childs[n];
        }

        // Token: 0x06000314 RID: 788 RVA: 0x00013BEC File Offset: 0x00011DEC
        private bool checkNoChildsActive()
        {
            foreach (KeyValuePair<int, ViewController> keyValuePair in childs)
            {
                ViewController value = keyValuePair.Value;
                if (value != null && value.controllerState != ControllerState.CONTROLLER_DEACTIVE)
                {
                    return false;
                }
            }
            return true;
        }

        // Token: 0x06000315 RID: 789 RVA: 0x00013C54 File Offset: 0x00011E54
        public Vector convertTouchForLandscape(Vector t)
        {
            throw new NotImplementedException();
        }

        // Token: 0x06000316 RID: 790 RVA: 0x00013C5C File Offset: 0x00011E5C
        public virtual bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            if (activeViewID == -1)
            {
                return false;
            }
            View view = activeView();
            int num = -1;
            for (int i = 0; i < touches.Count; i++)
            {
                CTRTouchState ctrtouchState = touches[i];
                num++;
                if (num > 1)
                {
                    break;
                }
                if (ctrtouchState.State == TouchLocationState.Pressed)
                {
                    return view.onTouchDownXY(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y));
                }
            }
            return false;
        }

        // Token: 0x06000317 RID: 791 RVA: 0x00013CD0 File Offset: 0x00011ED0
        public virtual bool touchesEndedwithEvent(List<CTRTouchState> touches)
        {
            if (activeViewID == -1)
            {
                return false;
            }
            View view = activeView();
            int num = -1;
            for (int i = 0; i < touches.Count; i++)
            {
                CTRTouchState ctrtouchState = touches[i];
                num++;
                if (num > 1)
                {
                    break;
                }
                if (ctrtouchState.State == TouchLocationState.Released)
                {
                    return view.onTouchUpXY(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y));
                }
            }
            return false;
        }

        // Token: 0x06000318 RID: 792 RVA: 0x00013D44 File Offset: 0x00011F44
        public virtual bool touchesMovedwithEvent(List<CTRTouchState> touches)
        {
            if (activeViewID == -1)
            {
                return false;
            }
            View view = activeView();
            int num = -1;
            for (int i = 0; i < touches.Count; i++)
            {
                CTRTouchState ctrtouchState = touches[i];
                num++;
                if (num > 1)
                {
                    break;
                }
                if (ctrtouchState.State == TouchLocationState.Moved)
                {
                    return view.onTouchMoveXY(CtrRenderer.transformX(ctrtouchState.Position.X), CtrRenderer.transformY(ctrtouchState.Position.Y));
                }
            }
            return false;
        }

        // Token: 0x06000319 RID: 793 RVA: 0x00013DB8 File Offset: 0x00011FB8
        public virtual bool touchesCancelledwithEvent(List<CTRTouchState> touches)
        {
            foreach (CTRTouchState ctrtouchState in touches)
            {
                TouchLocationState state = ctrtouchState.State;
            }
            return false;
        }

        // Token: 0x0600031A RID: 794 RVA: 0x00013E08 File Offset: 0x00012008
        public override void dealloc()
        {
            views.Clear();
            views = null;
            childs.Clear();
            childs = null;
            base.dealloc();
        }

        // Token: 0x0600031B RID: 795 RVA: 0x00013E34 File Offset: 0x00012034
        public virtual bool backButtonPressed()
        {
            return false;
        }

        // Token: 0x0600031C RID: 796 RVA: 0x00013E37 File Offset: 0x00012037
        public virtual bool menuButtonPressed()
        {
            return false;
        }

        // Token: 0x040008C8 RID: 2248
        public const int FAKE_TOUCH_UP_TO_DEACTIVATE_BUTTONS = -10000;

        // Token: 0x040008C9 RID: 2249
        public ControllerState controllerState;

        // Token: 0x040008CA RID: 2250
        public int activeViewID;

        // Token: 0x040008CB RID: 2251
        public Dictionary<int, View> views;

        // Token: 0x040008CC RID: 2252
        public int activeChildID;

        // Token: 0x040008CD RID: 2253
        public Dictionary<int, ViewController> childs;

        // Token: 0x040008CE RID: 2254
        public ViewController parent;

        // Token: 0x040008CF RID: 2255
        public int pausedViewID;

        // Token: 0x02000067 RID: 103
        public enum ControllerState
        {
            // Token: 0x040008D1 RID: 2257
            CONTROLLER_DEACTIVE,
            // Token: 0x040008D2 RID: 2258
            CONTROLLER_ACTIVE,
            // Token: 0x040008D3 RID: 2259
            CONTROLLER_PAUSED
        }
    }
}
