using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using ctre_wp7.ctr_commons;
using ctre_wp7.ctr_original;
using ctre_wp7.game.remotedata;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.media;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;
using ctre_wp7.Specials;
using ctre_wp7.wp7utilities;
using FlurryWP7SDK;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7
{
	// Token: 0x02000030 RID: 48
	public class App : Application
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
		public PhoneApplicationFrame RootFrame { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000C1B2 File Offset: 0x0000A3B2
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000C1BA File Offset: 0x0000A3BA
		public ContentManager Content { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000C1CB File Offset: 0x0000A3CB
		public GameTimer FrameworkDispatcherTimer { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public AppServiceProvider Services { get; private set; }

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public App()
		{
			base.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
			this.InitializeComponent();
			this.InitializePhoneApplication();
			this.InitializeXnaApplication();
			if (Debugger.IsAttached)
			{
				PhoneApplicationService.Current.UserIdleDetectionMode = 1;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C238 File Offset: 0x0000A438
		private void Application_Launching(object sender, LaunchingEventArgs e)
		{
			this.RootFrame.Obscured += new EventHandler<ObscuredEventArgs>(this.Obscured);
			this.RootFrame.Unobscured += new EventHandler(this.Unobscured);
			Api.StartSession("JJWVKTYXK7HCRZRJ4ZYY");
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C274 File Offset: 0x0000A474
		private void Application_Activated(object sender, ActivatedEventArgs e)
		{
			Api.StartSession("JJWVKTYXK7HCRZRJ4ZYY");
			bool flag = CTRPreferences.isLiteVersion();
			CTRPreferences.IsTrial = Guide.IsTrialMode;
			if (flag != CTRPreferences.isLiteVersion())
			{
				App.Quit();
			}
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeResume();
			lock (App.achievementsLockObject)
			{
				if (App.achievements != null)
				{
					AchievementsView.InitAllAchievements(App.achievements);
					App.achievements = null;
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		private void Application_Deactivated(object sender, DeactivatedEventArgs e)
		{
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativePause();
			Preferences._savePreferences();
			this.UpdateApplicationTile();
			while (!Preferences.isSaveFinished())
			{
				Thread.Sleep(100);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C312 File Offset: 0x0000A512
		private void Application_Closing(object sender, ClosingEventArgs e)
		{
			Preferences._savePreferences();
			this.UpdateApplicationTile();
			while (!Preferences.isSaveFinished())
			{
				Thread.Sleep(100);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C32F File Offset: 0x0000A52F
		private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C33D File Offset: 0x0000A53D
		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (e.ExceptionObject is GameUpdateRequiredException)
			{
				App.MakeUpdatePopup();
				e.Handled = true;
				return;
			}
			if (e.ExceptionObject is App.QuitException)
			{
				this.Application_Closing(null, null);
				return;
			}
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C37C File Offset: 0x0000A57C
		private void InitializePhoneApplication()
		{
			if (this.phoneApplicationInitialized)
			{
				return;
			}
			this.RootFrame = new PhoneApplicationFrame();
			this.RootFrame.Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
			this.RootFrame.NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
			this.phoneApplicationInitialized = true;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C3D2 File Offset: 0x0000A5D2
		private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
		{
			if (base.RootVisual != this.RootFrame)
			{
				base.RootVisual = this.RootFrame;
			}
			this.RootFrame.Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C408 File Offset: 0x0000A608
		private void InitializeXnaApplication()
		{
			this.Services = new AppServiceProvider();
			foreach (object obj in base.ApplicationLifetimeObjects)
			{
				if (obj is IGraphicsDeviceService)
				{
					this.Services.AddService(typeof(IGraphicsDeviceService), obj);
				}
			}
			this.Content = new ContentManager(this.Services, "Content");
			this.FrameworkDispatcherTimer = new GameTimer();
			this.FrameworkDispatcherTimer.FrameAction += new EventHandler<EventArgs>(this.FrameworkDispatcherFrameAction);
			this.FrameworkDispatcherTimer.Start();
			this.Content.RootDirectory = "Content";
			SoundMgr.SetContentManager(this.Content);
			VideoDataManager.initVideoDataManager();
			try
			{
				CTRPreferences.IsTrial = Guide.IsTrialMode;
				SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
				GamerServicesDispatcher.Initialize(this.Services);
			}
			catch (Exception ex)
			{
				if (ex is GameUpdateRequiredException)
				{
					App.MakeUpdatePopup();
				}
				GamerServicesNotAvailableException ex2 = ex as GamerServicesNotAvailableException;
				ArgumentNullException ex3 = ex as ArgumentNullException;
				InvalidOperationException ex4 = ex as InvalidOperationException;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C53C File Offset: 0x0000A73C
		protected void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			if (gamer != null)
			{
				gamer.BeginGetAchievements(new AsyncCallback(App.GetAchievementsCallback), gamer);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C568 File Offset: 0x0000A768
		public static void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer == null)
			{
				return;
			}
			lock (App.achievementsLockObject)
			{
				App.achievements = signedInGamer.EndGetAchievements(result);
				if (!CtrRenderer.IsPaused)
				{
					AchievementsView.InitAllAchievements(App.achievements);
					App.achievements = null;
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		private void FrameworkDispatcherFrameAction(object sender, EventArgs e)
		{
			FrameworkDispatcher.Update();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		public static void Quit()
		{
			throw new App.QuitException();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		private void Obscured(object sender, ObscuredEventArgs e)
		{
			if (e != null && !e.IsLocked)
			{
				CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativePause();
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		private void Unobscured(object sender, EventArgs e)
		{
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeResume();
			lock (App.achievementsLockObject)
			{
				if (App.achievements != null)
				{
					AchievementsView.InitAllAchievements(App.achievements);
					if (AchievementsView.Init)
					{
						App.achievements = null;
					}
				}
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C650 File Offset: 0x0000A850
		public static void MakeUpdatePopup()
		{
			if (!App.UpdateHandled)
			{
				App.UpdateHandled = true;
				UpdatePopup.showUpdatePopup();
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C664 File Offset: 0x0000A864
		private void UpdateApplicationTile()
		{
			int totalStars = CTRPreferences.getTotalStars();
			if (totalStars != this.tileStars && CtrRenderer.IsInit)
			{
				ShellTile shellTile = Enumerable.First<ShellTile>(ShellTile.ActiveTiles);
				if (shellTile != null)
				{
					string text = ((totalStars == 0) ? "ctr/ctr_live_tile_0" : "ctr/ctr_live_tile_star");
					ctre_wp7.iframework.visual.Texture2D texture2D = new ctre_wp7.iframework.visual.Texture2D().initWithPath(text, true);
					Application.sharedResourceMgr().setTextureInfo(texture2D, new XMLNode(), true, 1.5f, 1.5f);
					if (texture2D != null)
					{
						Microsoft.Xna.Framework.Graphics.Texture2D texture2D2 = texture2D.xnaTexture_;
						GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, true);
						if (totalStars > 0)
						{
							texture2D.setWvga();
							Image image = Image.Image_create(texture2D);
							Text text2 = new Text().initWithFont(Application.getFont(5));
							text2.setString(totalStars.ToString());
							text2.anchor = 36;
							text2.parentAnchor = 36;
							text2.x = -33f;
							text2.y = 9f;
							image.addChild(text2);
							image.x = -0.5f;
							image.y = -0.5f;
							OpenGL.glViewport(0, 0, 173, 173);
							OpenGL.glMatrixMode(15);
							OpenGL.glLoadIdentity();
							OpenGL.glOrthof(0.0, 173.0, 173.0, 0.0, -1.0, 1.0);
							OpenGL.glMatrixMode(14);
							OpenGL.glLoadIdentity();
							OpenGL.glPushMatrix();
							OpenGL.SetWhiteColor();
							OpenGL.glEnable(0);
							OpenGL.glEnable(1);
							OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
							RenderTarget2D renderTarget2D = new RenderTarget2D(WP7Singletons.GraphicsDevice, texture2D._realWidth, texture2D._realHeight, false, SurfaceFormat.Color, DepthFormat.None);
							WP7Singletons.GraphicsDevice.SetRenderTarget(renderTarget2D);
							WP7Singletons.GraphicsDevice.Clear(Color.Black);
							image.draw();
							WP7Singletons.GraphicsDevice.SetRenderTarget(null);
							texture2D2 = renderTarget2D;
							OpenGL.glDisable(0);
							OpenGL.glDisable(1);
							OpenGL.glPopMatrix();
						}
						using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
						{
							using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.CreateFile("Shared\\ShellContent\\Tile.png"))
							{
								texture2D2.SaveAsPng(isolatedStorageFileStream, 173, 173);
								isolatedStorageFileStream.Close();
								this.tileStars = totalStars;
							}
						}
						StandardTileData standardTileData = new StandardTileData
						{
							Title = "",
							BackgroundImage = new Uri("isostore:/Shared/ShellContent/Tile.png", 1),
							Count = new int?(0),
							BackTitle = "",
							BackBackgroundImage = null,
							BackContent = ""
						};
						shellTile.Update(standardTileData);
					}
				}
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C924 File Offset: 0x0000AB24
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/ctre_wp7;component/App.xaml", 2));
		}

		// Token: 0x040007D7 RID: 2007
		private bool phoneApplicationInitialized;

		// Token: 0x040007D8 RID: 2008
		private static object achievementsLockObject = new object();

		// Token: 0x040007D9 RID: 2009
		private static AchievementCollection achievements;

		// Token: 0x040007DA RID: 2010
		public static bool UpdateHandled = false;

		// Token: 0x040007DB RID: 2011
		public static bool NeedsUpdate = false;

		// Token: 0x040007DC RID: 2012
		private int tileStars = -1;

		// Token: 0x040007DD RID: 2013
		private bool _contentLoaded;

		// Token: 0x02000031 RID: 49
		private class QuitException : Exception
		{
		}
	}
}
