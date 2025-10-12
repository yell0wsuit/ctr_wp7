using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ctre_wp7.ctr_commons;
using ctre_wp7.ctr_original;
using ctre_wp7.game;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.media;
using ctre_wp7.ios;
using ctre_wp7.wp7utilities;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace ctre_wp7
{
	// Token: 0x0200005B RID: 91
	public class GamePage : PhoneApplicationPage
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00011903 File Offset: 0x0000FB03
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/ctre_wp7;component/GamePage.xaml", 2));
			this.mediaElement1 = (MediaElement)base.FindName("mediaElement1");
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0001193C File Offset: 0x0000FB3C
		public GamePage()
		{
			this.InitializeComponent();
			this.contentManager = (Application.Current as App).Content;
			this.timer = new GameTimer();
			this.timer.UpdateInterval = TimeSpan.Zero;
			this.timer.Update += new EventHandler<GameTimerEventArgs>(this.OnUpdate);
			this.timer.Draw += new EventHandler<GameTimerEventArgs>(this.OnDraw);
			GamePage.MainPage = this;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000119C0 File Offset: 0x0000FBC0
		private Language GetSystemLanguage()
		{
			Language language = Language.LANG_EN;
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ru")
			{
				language = Language.LANG_RU;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
			{
				language = Language.LANG_DE;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "fr")
			{
				language = Language.LANG_FR;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "it")
			{
				language = Language.LANG_IT;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "es")
			{
				language = Language.LANG_ES;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "nl")
			{
				language = Language.LANG_NL;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "pt" && CultureInfo.CurrentCulture.Name != "pt-PT")
			{
				language = Language.LANG_BR;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ko")
			{
				language = Language.LANG_KO;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "zh")
			{
				language = Language.LANG_ZH;
			}
			if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja")
			{
				language = Language.LANG_JA;
			}
			return language;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, true);
			this.spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);
			CtrRenderer.onSurfaceCreated();
			CtrRenderer.onSurfaceChanged(480, 800);
			WP7Singletons.GraphicsDevice = SharedGraphicsDeviceManager.Current.GraphicsDevice;
			OpenGL.Init();
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeInit(this.GetSystemLanguage());
			this.timer.Start();
			base.OnNavigatedTo(e);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011B50 File Offset: 0x0000FD50
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.timer.Stop();
			GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, false);
			if (this.Playing)
			{
				this.Playing = false;
				this.PlayingEnded = true;
				this.mediaElement1.Stop();
			}
			Preferences._savePreferences();
			base.OnNavigatedFrom(e);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		private void OnUpdate(object sender, GameTimerEventArgs e)
		{
			try
			{
				if (!App.UpdateHandled && !App.NeedsUpdate)
				{
					GamerServicesDispatcher.Update();
				}
			}
			catch (Exception ex)
			{
				if (ex is GameUpdateRequiredException)
				{
					App.MakeUpdatePopup();
				}
			}
			if (this.Playing)
			{
				if (Enumerable.Count<TouchLocation>(TouchPanel.GetState()) > 0)
				{
					this.mediaElement1.Stop();
					return;
				}
			}
			else
			{
				int milliseconds = e.ElapsedTime.Milliseconds;
				this.DeltaMS += milliseconds - 20;
				if (this.DeltaMS < 0)
				{
					this.DeltaMS = 0;
				}
				if (this.DeltaMS > 50)
				{
					this.DeltaMS = 50;
				}
				CtrRenderer.update((float)milliseconds / 1000f, TouchPanel.GetState());
				if (this.DeltaMS > 20)
				{
					CtrRenderer.update((float)milliseconds / 1000f, TouchPanel.GetState());
					this.DeltaMS -= 20;
				}
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00011C94 File Offset: 0x0000FE94
		private void OnDraw(object sender, GameTimerEventArgs e)
		{
			if (this.NeedReset)
			{
				GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, false);
				this.NeedReset = false;
			}
			if (this.Playing || this.mediaElement1.CurrentState == 3)
			{
				GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, false);
				return;
			}
			SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);
			try
			{
				GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, true);
			}
			catch (Exception)
			{
				this.NeedReset = true;
			}
			if (this.PlayingEnded)
			{
				if (this.ResumeMusicAfterOnVideoEnds)
				{
					this.ResumeMusicAfterOnVideoEnds = false;
					MediaPlayer.Resume();
				}
				if (this.delegateMovieMgrDelegate != null)
				{
					this.delegateMovieMgrDelegate.moviePlaybackFinished(this.MoviePath);
				}
				this.PlayingEnded = false;
			}
			CtrRenderer.onDrawFrame();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011D68 File Offset: 0x0000FF68
		internal void PlayMovie(NSString path, bool mute, MovieMgrDelegate delegateMovieMgr, bool resumeMusicAfterOnVideoEnds)
		{
			this.ResumeMusicAfterOnVideoEnds = resumeMusicAfterOnVideoEnds;
			this.MoviePath = path;
			this.delegateMovieMgrDelegate = delegateMovieMgr;
			this.mediaElement1.Source = new Uri("/" + path, 0);
			this.mediaElement1.Play();
			this.mediaElement1.Volume = (double)(mute ? 0 : 1);
			this.Playing = true;
			this.PlayingEnded = false;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011DD3 File Offset: 0x0000FFD3
		protected override void OnBackKeyPress(CancelEventArgs e)
		{
			CtrRenderer.Java_com_zeptolab_ctr_CtrRenderer_nativeBackPressed();
			e.Cancel = true;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		private void mediaElement1_CurrentStateChanged(object sender, RoutedEventArgs e)
		{
			if (this.mediaElement1.CurrentState == 3 && !this.Playing)
			{
				this.Playing = true;
				this.PlayingEnded = false;
			}
			if ((this.mediaElement1.CurrentState == 5 || this.mediaElement1.CurrentState == 4) && this.Playing)
			{
				this.Playing = false;
				this.PlayingEnded = true;
				this.mediaElement1.Stop();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011E54 File Offset: 0x00010054
		public void AwardAchievement(string Name)
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
			if (signedInGamer == null)
			{
				return;
			}
			if (!CTRPreferences.isLiteVersion())
			{
				signedInGamer.BeginAwardAchievement(Name, new AsyncCallback(this.AwardAchievementCallback), signedInGamer);
				return;
			}
			CTRRootController.openFullVersionPage();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00011E94 File Offset: 0x00010094
		protected void AwardAchievementCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer != null)
			{
				signedInGamer.EndAwardAchievement(result);
				signedInGamer.BeginGetAchievements(new AsyncCallback(App.GetAchievementsCallback), signedInGamer);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011ECC File Offset: 0x000100CC
		public void PostLeaderboard(int pack, int Score)
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
			if (signedInGamer == null)
			{
				return;
			}
			LeaderboardWriter leaderboardWriter = signedInGamer.LeaderboardWriter;
			LeaderboardEntry leaderboard = leaderboardWriter.GetLeaderboard(LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, pack));
			leaderboard.Rating = (long)Score;
		}

		// Token: 0x040008AA RID: 2218
		internal MediaElement mediaElement1;

		// Token: 0x040008AB RID: 2219
		private bool _contentLoaded;

		// Token: 0x040008AC RID: 2220
		private ContentManager contentManager;

		// Token: 0x040008AD RID: 2221
		private GameTimer timer;

		// Token: 0x040008AE RID: 2222
		private SpriteBatch spriteBatch;

		// Token: 0x040008AF RID: 2223
		public static GamePage MainPage;

		// Token: 0x040008B0 RID: 2224
		private int DeltaMS;

		// Token: 0x040008B1 RID: 2225
		private bool NeedReset = true;

		// Token: 0x040008B2 RID: 2226
		private MovieMgrDelegate delegateMovieMgrDelegate;

		// Token: 0x040008B3 RID: 2227
		private NSString MoviePath;

		// Token: 0x040008B4 RID: 2228
		private bool Playing;

		// Token: 0x040008B5 RID: 2229
		private bool PlayingEnded;

		// Token: 0x040008B6 RID: 2230
		private bool ResumeMusicAfterOnVideoEnds;
	}
}
