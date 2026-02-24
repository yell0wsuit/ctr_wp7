using System;
using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

using Microsoft.Xna.Framework.Input.Touch;

namespace ctr_wp7.iframework.core
{
    internal class ViewController : NSObject, TouchDelegate
    {
        public ViewController()
        {
            views = [];
        }

        public virtual NSObject initWithParent(ViewController p)
        {
            if (base.init() != null)
            {
                controllerState = ControllerState.CONTROLLER_DEACTIVE;
                views = [];
                childs = [];
                activeViewID = -1;
                activeChildID = -1;
                pausedViewID = -1;
                parent = p;
            }
            return this;
        }

        public virtual void activate()
        {
            controllerState = ControllerState.CONTROLLER_ACTIVE;
            Application.sharedRootController().onControllerActivated(this);
        }

        public virtual void deactivate()
        {
            Application.sharedRootController().onControllerDeactivationRequest(this);
        }

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

        public virtual void update(float delta)
        {
            if (activeViewID == -1)
            {
                return;
            }
            View view = activeView();
            view.update(delta);
        }

        public virtual void addViewwithID(View v, int n)
        {
            _ = views.TryGetValue(n, out _);
            views[n] = v;
        }

        public virtual void deleteView(int n)
        {
            views[n] = null;
        }

        public virtual void hideActiveView()
        {
            View view = views[activeViewID];
            Application.sharedRootController().onControllerViewHide(view);
            view?.hide();
            activeViewID = -1;
        }

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

        public virtual View activeView()
        {
            return views[activeViewID];
        }

        public virtual View getView(int n)
        {
            _ = views.TryGetValue(n, out View view);
            return view;
        }

        public virtual void addChildwithID(ViewController c, int n)
        {
            ViewController viewController = null;
            viewController?.dealloc();
            childs[n] = c;
        }

        public virtual void deleteChild(int n)
        {
            if (childs.TryGetValue(n, out ViewController viewController))
            {
                viewController.dealloc();
                childs[n] = null;
            }
        }

        public virtual void deactivateActiveChild()
        {
            ViewController viewController = childs[activeChildID];
            viewController.deactivate();
            activeChildID = -1;
        }

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

        public virtual void onChildDeactivated(int n)
        {
            unpause();
        }

        public virtual ViewController activeChild()
        {
            return childs[activeChildID];
        }

        public virtual ViewController getChild(int n)
        {
            return childs[n];
        }

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

        public Vector convertTouchForLandscape(Vector t)
        {
            throw new NotImplementedException();
        }

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

        public virtual bool touchesCancelledwithEvent(List<CTRTouchState> touches)
        {
            foreach (CTRTouchState ctrtouchState in touches)
            {
                _ = ctrtouchState.State;
            }
            return false;
        }

        public override void dealloc()
        {
            views.Clear();
            views = null;
            childs.Clear();
            childs = null;
            base.dealloc();
        }

        public virtual bool backButtonPressed()
        {
            return false;
        }

        public virtual bool menuButtonPressed()
        {
            return false;
        }

        public const int FAKE_TOUCH_UP_TO_DEACTIVATE_BUTTONS = -10000;

        public ControllerState controllerState;

        public int activeViewID;

        public Dictionary<int, View> views;

        public int activeChildID;

        public Dictionary<int, ViewController> childs;

        public ViewController parent;

        public int pausedViewID;

        public enum ControllerState
        {
            CONTROLLER_DEACTIVE,
            CONTROLLER_ACTIVE,
            CONTROLLER_PAUSED
        }
    }
}
