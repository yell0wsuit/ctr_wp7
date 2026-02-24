using System;
using System.Collections.Generic;
using ctre_wp7.ctr_commons;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.core
{
	// Token: 0x0200008C RID: 140
	internal class RootController : ViewController
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public override NSObject initWithParent(ViewController p)
		{
			if (base.initWithParent(p) != null)
			{
				this.viewTransition = -1;
				this.transitionTime = -1f;
				this.previousView = null;
				this.transitionDelay = 0.3f;
				this.screenGrabber = (Grabber)new Grabber().init();
				if (this.prevScreenImage != null && this.prevScreenImage.xnaTexture_ != null)
				{
					this.prevScreenImage.xnaTexture_.Dispose();
				}
				this.prevScreenImage = null;
				if (this.nextScreenImage != null && this.nextScreenImage.xnaTexture_ != null)
				{
					this.nextScreenImage.xnaTexture_.Dispose();
				}
				this.nextScreenImage = null;
				this.deactivateCurrentController = false;
			}
			return this;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public void performTick(float delta)
		{
			this.lastTime += delta;
			if (this.transitionTime == -1f)
			{
				this.currentController.update(delta);
			}
			if (this.deactivateCurrentController)
			{
				this.deactivateCurrentController = false;
				this.currentController.deactivateImmediately();
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		public void performDraw()
		{
			if (this.currentController.activeViewID != -1)
			{
				Application.sharedCanvas().beforeRender();
				OpenGL.glPushMatrix();
				this.applyLandscape();
				if (this.transitionTime == -1f)
				{
					this.currentController.activeView().draw();
				}
				else
				{
					this.drawViewTransition();
					if (this.lastTime > this.transitionTime)
					{
						this.transitionTime = -1f;
						NSObject.NSREL(this.prevScreenImage);
						if (this.prevScreenImage != null && this.prevScreenImage.xnaTexture_ != null)
						{
							this.prevScreenImage.xnaTexture_.Dispose();
						}
						this.prevScreenImage = null;
						NSObject.NSREL(this.nextScreenImage);
						if (this.nextScreenImage != null && this.nextScreenImage.xnaTexture_ != null)
						{
							this.nextScreenImage.xnaTexture_.Dispose();
						}
						this.nextScreenImage = null;
					}
				}
				OpenGL.glPopMatrix();
				Application.sharedCanvas().afterRender();
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001CBD1 File Offset: 0x0001ADD1
		private void applyLandscape()
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001CBD3 File Offset: 0x0001ADD3
		public virtual void setViewTransition(int transition)
		{
			this.viewTransition = transition;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		public virtual void setTransitionTime()
		{
			this.transitionTime = this.lastTime + this.transitionDelay;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001CBF1 File Offset: 0x0001ADF1
		private void setViewTransitionDelay(float delay)
		{
			this.transitionDelay = delay;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		private void drawViewTransition()
		{
			OpenGL.SetWhiteColor();
			OpenGL.glEnable(0);
			OpenGL.glEnable(1);
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			Application.sharedCanvas().setDefaultRealProjection();
			switch (this.viewTransition)
			{
			case 4:
			case 5:
			{
				float num = MathHelper.MIN(1.0, (double)((this.transitionDelay - (this.transitionTime - this.lastTime)) / this.transitionDelay));
				if ((double)num < 0.5)
				{
					if (this.prevScreenImage != null)
					{
						RGBAColor rgbacolor = ((this.viewTransition == 4) ? RGBAColor.MakeRGBA(0.0, 0.0, 0.0, (double)num * 2.0) : RGBAColor.MakeRGBA(1.0, 1.0, 1.0, (double)num * 2.0));
						Grabber.drawGrabbedImage(this.prevScreenImage, 0, 0);
						OpenGL.glDisable(0);
						OpenGL.glEnable(1);
						OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
						GLDrawer.drawSolidRectWOBorder(0f, 0f, FrameworkTypes.REAL_SCREEN_WIDTH, FrameworkTypes.REAL_SCREEN_HEIGHT, rgbacolor);
						OpenGL.glDisable(1);
					}
					else
					{
						if (this.viewTransition == 4)
						{
							OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
						}
						else
						{
							OpenGL.glClearColor(1.0, 1.0, 1.0, 1.0);
						}
						OpenGL.glClear(0);
					}
				}
				else if (this.nextScreenImage != null)
				{
					RGBAColor rgbacolor2 = ((this.viewTransition == 4) ? RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 2.0 - (double)num * 2.0) : RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 2.0 - (double)num * 2.0));
					Grabber.drawGrabbedImage(this.nextScreenImage, 0, 0);
					OpenGL.glDisable(0);
					OpenGL.glEnable(1);
					OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
					GLDrawer.drawSolidRectWOBorder(0f, 0f, FrameworkTypes.REAL_SCREEN_WIDTH, FrameworkTypes.REAL_SCREEN_HEIGHT, rgbacolor2);
					OpenGL.glDisable(1);
				}
				else
				{
					if (this.viewTransition == 4)
					{
						OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
					}
					else
					{
						OpenGL.glClearColor(1.0, 1.0, 1.0, 1.0);
					}
					OpenGL.glClear(0);
				}
				break;
			}
			case 6:
			{
				float num2 = MathHelper.MIN(1.0, (double)((this.transitionDelay - (this.transitionTime - this.lastTime)) / this.transitionDelay));
				OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0 - (double)num2);
				Grabber.drawGrabbedImage(this.prevScreenImage, 0, 0);
				OpenGL.glColor4f(1.0, 1.0, 1.0, (double)num2);
				Grabber.drawGrabbedImage(this.nextScreenImage, 0, 0);
				break;
			}
			}
			this.applyLandscape();
			OpenGL.glDisable(0);
			OpenGL.glDisable(1);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001CF91 File Offset: 0x0001B191
		public override void activate()
		{
			base.activate();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001CF99 File Offset: 0x0001B199
		private void runLoop()
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public virtual void onControllerActivated(ViewController c)
		{
			this.setCurrentController(c);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001CFA5 File Offset: 0x0001B1A5
		public virtual void onControllerDeactivated(ViewController c)
		{
			this.setCurrentController(null);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001CFAE File Offset: 0x0001B1AE
		public virtual void onControllerPaused(ViewController c)
		{
			this.setCurrentController(null);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001CFB7 File Offset: 0x0001B1B7
		public virtual void onControllerUnpaused(ViewController c)
		{
			this.setCurrentController(c);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public virtual void onControllerDeactivationRequest(ViewController c)
		{
			this.deactivateCurrentController = true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001CFCC File Offset: 0x0001B1CC
		public virtual void onControllerViewShow(View v)
		{
			if (this.viewTransition != -1 && this.previousView != null)
			{
				Application.sharedCanvas().setDefaultProjection();
				OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
				OpenGL.glClear(0);
				this.transitionTime = this.lastTime + this.transitionDelay;
				this.applyLandscape();
				this.currentController.activeView().draw();
				NSObject.NSREL(this.nextScreenImage);
				if (this.nextScreenImage != null && this.nextScreenImage.xnaTexture_ != null)
				{
					this.nextScreenImage.xnaTexture_.Dispose();
				}
				this.nextScreenImage = this.screenGrabber.grab();
				NSObject.NSRET(this.nextScreenImage);
				OpenGL.glLoadIdentity();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		public virtual void onControllerViewHide(View v)
		{
			this.previousView = v;
			if (this.viewTransition != -1 && this.previousView != null)
			{
				Application.sharedCanvas().setDefaultProjection();
				OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
				OpenGL.glClear(0);
				this.applyLandscape();
				this.previousView.draw();
				NSObject.NSREL(this.prevScreenImage);
				if (this.prevScreenImage != null && this.prevScreenImage.xnaTexture_ != null)
				{
					this.prevScreenImage.xnaTexture_.Dispose();
				}
				this.prevScreenImage = this.screenGrabber.grab();
				NSObject.NSRET(this.prevScreenImage);
				OpenGL.glLoadIdentity();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001D16B File Offset: 0x0001B36B
		public virtual bool isSuspended()
		{
			return this.suspended;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001D173 File Offset: 0x0001B373
		public virtual void suspend()
		{
			this.suspended = true;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001D17C File Offset: 0x0001B37C
		public virtual void resume()
		{
			this.suspended = false;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001D185 File Offset: 0x0001B385
		public override bool backButtonPressed()
		{
			return this.suspended || this.transitionTime != -1f || this.currentController.backButtonPressed();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001D1AB File Offset: 0x0001B3AB
		public override bool menuButtonPressed()
		{
			return this.suspended || this.transitionTime != -1f || this.currentController.menuButtonPressed();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001D1D1 File Offset: 0x0001B3D1
		public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
		{
			return !this.suspended && (this.transitionTime != -1f || this.currentController.touchesBeganwithEvent(touches));
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public override bool touchesMovedwithEvent(List<CTRTouchState> touches)
		{
			return !this.suspended && (this.transitionTime != -1f || this.currentController.touchesMovedwithEvent(touches));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D21F File Offset: 0x0001B41F
		public override bool touchesEndedwithEvent(List<CTRTouchState> touches)
		{
			return !this.suspended && (this.transitionTime != -1f || this.currentController.touchesEndedwithEvent(touches));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001D246 File Offset: 0x0001B446
		public override bool touchesCancelledwithEvent(List<CTRTouchState> touches)
		{
			return this.currentController.touchesCancelledwithEvent(touches);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D254 File Offset: 0x0001B454
		public virtual void setCurrentController(ViewController c)
		{
			this.currentController = c;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001D25D File Offset: 0x0001B45D
		public virtual ViewController getCurrentController()
		{
			return this.currentController;
		}

		// Token: 0x04000968 RID: 2408
		public const int TRANSITION_SLIDE_HORIZONTAL_RIGHT = 0;

		// Token: 0x04000969 RID: 2409
		public const int TRANSITION_SLIDE_HORIZONTAL_LEFT = 1;

		// Token: 0x0400096A RID: 2410
		public const int TRANSITION_SLIDE_VERTICAL_UP = 2;

		// Token: 0x0400096B RID: 2411
		public const int TRANSITION_SLIDE_VERTICAL_DON = 3;

		// Token: 0x0400096C RID: 2412
		public const int TRANSITION_FADE_OUT_BLACK = 4;

		// Token: 0x0400096D RID: 2413
		public const int TRANSITION_FADE_OUT_WHITE = 5;

		// Token: 0x0400096E RID: 2414
		public const int TRANSITION_REVEAL = 6;

		// Token: 0x0400096F RID: 2415
		public const int TRANSITIONS_COUNT = 7;

		// Token: 0x04000970 RID: 2416
		public const float TRANSITION_DEFAULT_DELAY = 0.3f;

		// Token: 0x04000971 RID: 2417
		public int viewTransition;

		// Token: 0x04000972 RID: 2418
		public float transitionTime;

		// Token: 0x04000973 RID: 2419
		private float transitionDelay;

		// Token: 0x04000974 RID: 2420
		private View previousView;

		// Token: 0x04000975 RID: 2421
		private Texture2D prevScreenImage;

		// Token: 0x04000976 RID: 2422
		private Texture2D nextScreenImage;

		// Token: 0x04000977 RID: 2423
		private Grabber screenGrabber;

		// Token: 0x04000978 RID: 2424
		private bool deactivateCurrentController;

		// Token: 0x04000979 RID: 2425
		private ViewController currentController;

		// Token: 0x0400097A RID: 2426
		private float lastTime;

		// Token: 0x0400097B RID: 2427
		public bool suspended;
	}
}
