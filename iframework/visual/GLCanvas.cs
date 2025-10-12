using System;
using System.Collections.Generic;
using ctre_wp7.ctr_commons;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x0200000C RID: 12
	internal class GLCanvas : NSObject
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x0000702B File Offset: 0x0000522B
		public virtual GLCanvas initWithFrame(Rectangle frame)
		{
			base.init();
			return this;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007035 File Offset: 0x00005235
		public virtual void show()
		{
			this.destroyFramebuffer();
			this.createFramebuffer();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007044 File Offset: 0x00005244
		public virtual void hide()
		{
			this.destroyFramebuffer();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000704C File Offset: 0x0000524C
		public virtual void beforeRender()
		{
			this.setDefaultProjection();
			OpenGL.glDisable(1);
			OpenGL.glEnableClientState(11);
			OpenGL.glEnableClientState(12);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007068 File Offset: 0x00005268
		public virtual void afterRender()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000706A File Offset: 0x0000526A
		public virtual void initFPSMeterWithFont(FontGeneric font)
		{
			this.fpsFont = font;
			this.fpsText = new Text().initWithFont(this.fpsFont);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000708C File Offset: 0x0000528C
		public virtual void drawFPS(int fps)
		{
			if (this.fpsText == null || this.fpsFont == null)
			{
				return;
			}
			NSString nsstring = NSObject.NSS(fps.ToString());
			this.fpsText.setString(nsstring);
			OpenGL.SetWhiteColor();
			OpenGL.glEnable(0);
			OpenGL.glEnable(1);
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			this.fpsText.x = 5f;
			this.fpsText.y = 5f;
			this.fpsText.draw();
			OpenGL.glDisable(1);
			OpenGL.glDisable(0);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000711A File Offset: 0x0000531A
		public virtual bool createFramebuffer()
		{
			this.backingWidth = (int)FrameworkTypes.SCREEN_WIDTH;
			this.backingHeight = (int)FrameworkTypes.SCREEN_HEIGHT;
			this.setDefaultProjection();
			OpenGL.glEnableClientState(11);
			OpenGL.glEnableClientState(12);
			return true;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000714C File Offset: 0x0000534C
		public virtual void setDefaultProjection()
		{
			OpenGL.glViewport(0.0, 0.0, (double)FrameworkTypes.REAL_SCREEN_WIDTH, (double)FrameworkTypes.REAL_SCREEN_HEIGHT);
			OpenGL.glMatrixMode(15);
			OpenGL.glLoadIdentity();
			OpenGL.glOrthof((double)(-(double)FrameworkTypes.SCREEN_OFFSET_X), (double)(FrameworkTypes.SCREEN_WIDTH + FrameworkTypes.SCREEN_OFFSET_X), (double)(FrameworkTypes.SCREEN_HEIGHT + FrameworkTypes.SCREEN_OFFSET_Y), (double)(-(double)FrameworkTypes.SCREEN_OFFSET_Y), -1.0, 1.0);
			OpenGL.glMatrixMode(14);
			OpenGL.glLoadIdentity();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000071D4 File Offset: 0x000053D4
		public virtual void setDefaultRealProjection()
		{
			OpenGL.glViewport(0.0, 0.0, (double)FrameworkTypes.REAL_SCREEN_WIDTH, (double)FrameworkTypes.REAL_SCREEN_HEIGHT);
			OpenGL.glMatrixMode(15);
			OpenGL.glLoadIdentity();
			OpenGL.glOrthof(0.0, (double)FrameworkTypes.REAL_SCREEN_WIDTH, (double)FrameworkTypes.REAL_SCREEN_HEIGHT, 0.0, -1.0, 1.0);
			OpenGL.glMatrixMode(14);
			OpenGL.glLoadIdentity();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007251 File Offset: 0x00005451
		public virtual void destroyFramebuffer()
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007253 File Offset: 0x00005453
		public virtual void touchesBeganwithEvent(List<CTRTouchState> touches)
		{
			if (this.touchDelegate != null)
			{
				this.touchDelegate.touchesBeganwithEvent(touches);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000726A File Offset: 0x0000546A
		public virtual void touchesMovedwithEvent(List<CTRTouchState> touches)
		{
			if (this.touchDelegate != null)
			{
				this.touchDelegate.touchesMovedwithEvent(touches);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007281 File Offset: 0x00005481
		public virtual void touchesEndedwithEvent(List<CTRTouchState> touches)
		{
			if (this.touchDelegate != null)
			{
				this.touchDelegate.touchesEndedwithEvent(touches);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007298 File Offset: 0x00005498
		public virtual void touchesCancelledwithEvent(List<CTRTouchState> touches)
		{
			if (this.touchDelegate != null)
			{
				this.touchDelegate.touchesCancelledwithEvent(touches);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000072AF File Offset: 0x000054AF
		public virtual bool backButtonPressed()
		{
			return this.touchDelegate != null && this.touchDelegate.backButtonPressed();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000072C6 File Offset: 0x000054C6
		public virtual bool menuButtonPressed()
		{
			return this.touchDelegate != null && this.touchDelegate.menuButtonPressed();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000072DD File Offset: 0x000054DD
		public override void dealloc()
		{
			NSObject.NSREL(this.fpsFont);
			NSObject.NSREL(this.fpsText);
			this.hide();
		}

		// Token: 0x04000721 RID: 1825
		public int backingWidth;

		// Token: 0x04000722 RID: 1826
		public int backingHeight;

		// Token: 0x04000723 RID: 1827
		public uint viewRenderbuffer;

		// Token: 0x04000724 RID: 1828
		public uint viewFramebuffer;

		// Token: 0x04000725 RID: 1829
		public uint depthRenderbuffer;

		// Token: 0x04000726 RID: 1830
		public FontGeneric fpsFont;

		// Token: 0x04000727 RID: 1831
		public Text fpsText;

		// Token: 0x04000728 RID: 1832
		public TouchDelegate touchDelegate;
	}
}
