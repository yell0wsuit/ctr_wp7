using System;
using System.Globalization;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;
using ctre_wp7.Specials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace ctre_wp7.ctr_original
{
	// Token: 0x02000107 RID: 263
	internal class LeaderboardsView : View, ButtonDelegate
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0003E53C File Offset: 0x0003C73C
		public override void draw()
		{
			if (this.NeedsRecreate == 1)
			{
				this.recreateContainerError(false, null);
				this.NeedsRecreate = 0;
			}
			if (this.NeedsRecreate == 2)
			{
				this.recreateContainerError(false, Application.getString(1310831));
				this.NeedsRecreate = 0;
			}
			base.draw();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0003E588 File Offset: 0x0003C788
		private ScoreStruct[] getLeaderboard(LeaderboardReader leaderboardReader)
		{
			ScoreStruct[] array = new ScoreStruct[leaderboardReader.Entries.Count];
			for (int i = 0; i < leaderboardReader.Entries.Count; i++)
			{
				LeaderboardEntry leaderboardEntry = leaderboardReader.Entries[i];
				array[i].name = NSObject.NSS(leaderboardEntry.Gamer.Gamertag);
				array[i].result = leaderboardEntry.Columns.GetValueInt32("BestScore");
				array[i].rank = i + leaderboardReader.PageStart + 1;
			}
			return array;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0003E618 File Offset: 0x0003C818
		private void InitLeaderboard(int n)
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
			if (signedInGamer == null)
			{
				this.recreateContainerError(false, Application.getString(1310831));
				return;
			}
			LeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, n);
			LeaderboardReader.BeginRead(leaderboardIdentity, signedInGamer, 100, new AsyncCallback(this.LeaderboardReadCallback), signedInGamer);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0003E668 File Offset: 0x0003C868
		protected void LeaderboardReadCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer != null)
			{
				try
				{
					this.leaderboardReader = LeaderboardReader.EndRead(result);
					this.canpageup = this.leaderboardReader.CanPageUp;
					this.canpagedown = this.leaderboardReader.CanPageDown;
					this.CurrentLeaderboard = this.getLeaderboard(this.leaderboardReader);
					this.NeedsRecreate = 1;
				}
				catch (Exception)
				{
					this.canpageup = false;
					this.canpagedown = false;
					this.NeedsRecreate = 2;
				}
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		public static NSString numberToSeparatedString(int theScore)
		{
			return NSObject.NSS(theScore.ToString(CultureInfo.CurrentCulture));
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0003E70C File Offset: 0x0003C90C
		public static Button createButtonWithImageQuad1Quad2IDDelegate(int res, int q1, int q2, int bid, NSObject d)
		{
			Image image = Image.Image_createWithResIDQuad(res, q1);
			Image image2 = Image.Image_createWithResIDQuad(res, q2);
			image2.scaleX = (image2.scaleY = 1.2f);
			image.parentAnchor = (image2.parentAnchor = 9);
			image.anchor = (image2.anchor = 9);
			Button button = new Button().initWithUpElementDownElementandID(image, image2, bid);
			button.delegateButtonDelegate = (ButtonDelegate)d;
			button.setTouchIncreaseLeftRightTopBottom((float)(button.width / 2), (float)(button.width / 2), (float)(button.height / 2), (float)(button.height / 2));
			return button;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0003E7AC File Offset: 0x0003C9AC
		public override NSObject initFullscreen()
		{
			if (base.initFullscreen() != null)
			{
				this.x += 0.33f;
				this.container = null;
				Image image = Image.Image_createWithResIDQuad(66, 0);
				image.scaleX = (image.scaleY = 1.25f);
				image.parentAnchor = (image.anchor = 18);
				this.addChild(image);
				this.addChild((BaseElement)new BaseElement().init());
				this.nextb = MenuController.createButton2WithImageQuad1Quad2IDDelegate(392, 0, 1, 0, this);
				this.nextb.scaleX = -1f;
				Image.setElementPositionWithQuadCenter(this.nextb, 390, 7);
				Vector relativeQuadOffset = Image.getRelativeQuadOffset(390, 8, 7);
				this.nextb.x += relativeQuadOffset.x;
				this.nextb.y -= relativeQuadOffset.y;
				this.nextb.setTouchIncreaseLeftRightTopBottom((float)(this.nextb.width / 2), (float)(this.nextb.width / 2), (float)(this.nextb.height / 2), (float)(this.nextb.height / 2));
				this.addChild(this.nextb);
				this.prevb = MenuController.createButton2WithImageQuad1Quad2IDDelegate(392, 0, 1, 1, this);
				this.prevb.setEnabled(false);
				Image.setElementPositionWithQuadCenter(this.prevb, 390, 8);
				this.prevb.setTouchIncreaseLeftRightTopBottom((float)(this.prevb.width / 2), (float)(this.prevb.width / 2), (float)(this.prevb.height / 2), (float)(this.prevb.height / 2));
				this.addChild(this.prevb);
				this.pageupb = MenuController.createButton2WithImageQuad1Quad2IDDelegate(392, 0, 1, 3, this);
				this.pageupb.setEnabled(false);
				this.pageupb.rotation = 90f;
				this.pageupb.x = 200f;
				this.pageupb.y = 450f;
				this.pageupb.setTouchIncreaseLeftRightTopBottom((float)(this.pageupb.width / 2), (float)(this.pageupb.width / 2), (float)(this.pageupb.height / 2), (float)(this.pageupb.height / 2));
				this.addChild(this.pageupb);
				this.pagedownb = MenuController.createButton2WithImageQuad1Quad2IDDelegate(392, 0, 1, 4, this);
				this.pagedownb.setEnabled(false);
				this.pagedownb.rotation = 90f;
				this.pagedownb.scaleX = -1f;
				this.pagedownb.x = 270f;
				this.pagedownb.y = 450f;
				this.pagedownb.setTouchIncreaseLeftRightTopBottom((float)(this.pageupb.width / 2), (float)(this.pageupb.width / 2), (float)(this.pageupb.height / 2), (float)(this.pageupb.height / 2));
				this.addChild(this.pagedownb);
				this.boxTitle = Text.createWithFontandString(5, Application.getString(1310785 + this.mode));
				this.boxTitle.setAlignment(2);
				Image.setElementPositionWithQuadCenter(this.boxTitle, 390, 7);
				this.addChild(this.boxTitle);
				Image image2 = Image.Image_createWithResIDQuad(390, 12);
				image2.scaleX = 0.6f;
				image2.x = 160f;
				image2.y = 50f;
				image2.anchor = 18;
				image2.blendingMode = 1;
				this.addChild(image2);
				Text text = Text.createWithFontandString(5, Application.getString(1310826));
				Image.setElementPositionWithQuadOffset(text, 390, 9);
				text.anchor = 17;
				text.setAlignment(1);
				this.addChild(text);
				Text text2 = Text.createWithFontandString(5, Application.getString(1310827));
				Image.setElementPositionWithQuadOffset(text2, 390, 10);
				text2.anchor = 20;
				text2.setAlignment(4);
				this.addChild(text2);
				RGBAColor rgbacolor = new RGBAColor(1.0, 0.796078431372549, 0.4980392156862745, 1.0);
				text.color = (text2.color = rgbacolor);
				this.playerPlaceContainer = Image.Image_createWithResIDQuad(390, 1);
				this.playerPlaceContainer.doRestoreCutTransparency();
				this.addChild(this.playerPlaceContainer);
				this.playerPlaceContainer.y += 50f;
				this.playerPlaceContainer.setEnabled(false);
				this.liftScrollbar = LiftScrollbar.createWithResIDBackQuadLiftQuadLiftQuadPressed(388, 1, 2, 3);
				Image.setElementPositionWithQuadOffset(this.liftScrollbar, 390, 2);
				this.liftScrollbar.blendingMode = 1;
				this.addChild(this.liftScrollbar);
				this.liftScrollbar.setEnabled(false);
				Image image3 = Image.Image_createWithResIDQuad(87, 0);
				image3.anchor = (image3.parentAnchor = 18);
				image3.scaleX = (image3.scaleY = 2f);
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
				timeline.addKeyFrame(KeyFrame.makeRotation(45.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
				timeline.addKeyFrame(KeyFrame.makeRotation(405.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 75.0));
				timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
				image3.addTimeline(timeline);
				image3.playTimeline(0);
				this.addChildwithID(image3, this.childsCount() + 1);
			}
			return this;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0003ED40 File Offset: 0x0003CF40
		public virtual BaseElement createEntryForRankScoreNameWidth(int rankNumber, int score, NSString userName, float w)
		{
			Vector relativeQuadOffset = Image.getRelativeQuadOffset(390, 13, 4);
			Vector relativeQuadOffset2 = Image.getRelativeQuadOffset(390, 13, 6);
			Vector relativeQuadOffset3 = Image.getRelativeQuadOffset(390, 13, 11);
			BaseElement baseElement = (BaseElement)new BaseElement().init();
			float num = -12f;
			Text text = Text.createWithFontandStringEN(6, NSObject.NSS(rankNumber + ".  "));
			text.scaleX = (text.scaleY = 0.75f);
			text.rotationCenterY = (float)(text.height / 2);
			text.anchor = 12;
			text.parentAnchor = 9;
			text.x = relativeQuadOffset.x;
			text.y = num - 4f;
			baseElement.addChild(text);
			Text text2 = Text.createWithFontandStringEN(6, userName);
			text2.anchor = 9;
			text2.parentAnchor = 9;
			text2.x = relativeQuadOffset2.x + 3f;
			text2.y = num + 3f;
			text2.scaleX = (text2.scaleY = 0.7f);
			text2.rotationCenterX -= (float)(text2.width / 2);
			baseElement.addChild(text2);
			Text text3 = Text.createWithFontandStringEN(6, LeaderboardsView.numberToSeparatedString(score));
			text3.anchor = 12;
			text3.parentAnchor = 9;
			text3.x = relativeQuadOffset3.x;
			text3.y = num + 3f;
			text3.scaleX = (text3.scaleY = 0.7f);
			text3.rotationCenterX += (float)(text3.width / 2);
			baseElement.addChild(text3);
			Font font = (Font)Application.getFontEN(6);
			float num2 = font.stringWidth(NSObject.NSS("."));
			float num3 = text3.x - text2.x - (float)text2.width * 0.7f - (float)text3.width * 0.7f;
			string text4 = "";
			float num4 = (num3 - num2 - 2f) / 0.7f;
			if (num4 > 0f)
			{
				while (font.stringWidth(NSObject.NSS(text4)) < num4)
				{
					text4 += ".";
				}
			}
			Text text5 = Text.createWithFontandStringEN(6, NSObject.NSS(text4));
			text5.anchor = 9;
			text5.parentAnchor = 12;
			text5.x += 2f;
			text2.addChild(text5);
			baseElement.width = (int)w;
			baseElement.height = 36;
			return baseElement;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0003EFE0 File Offset: 0x0003D1E0
		public virtual BaseElement createEntryForScoreWidth(ScoreStruct score, float w)
		{
			if (score.name.ToString() == Gamer.SignedInGamers[PlayerIndex.One].Gamertag)
			{
				BaseElement baseElement = (BaseElement)new BaseElement().init();
				Image image = Image.Image_createWithResIDQuad(390, 1);
				BaseElement baseElement2 = this.createEntryForRankScoreNameWidth(score.rank, score.result, score.name, w);
				baseElement2.parentAnchor = (baseElement2.anchor = 18);
				baseElement2.x = 50f;
				image.parentAnchor = (image.anchor = 18);
				image.scaleX = 0.48f;
				image.scaleY = 0.6f;
				image.x = 13f;
				image.y = 0.16f;
				baseElement.addChild(image);
				baseElement.addChild(baseElement2);
				baseElement.width = baseElement2.width;
				baseElement.height = baseElement2.height;
				baseElement.parentAnchor = (baseElement.anchor = 18);
				return baseElement;
			}
			BaseElement baseElement3 = this.createEntryForRankScoreNameWidth(score.rank, score.result, score.name, w);
			baseElement3.x = 50f;
			return baseElement3;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0003F10F File Offset: 0x0003D30F
		public virtual void cleanContainer()
		{
			Factory.hideProcessingFromView(this);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0003F118 File Offset: 0x0003D318
		public virtual void recreateContainerError(bool isLoading, object error)
		{
			if (this.container != null)
			{
				this.removeChild(this.container);
			}
			Vector relativeQuadOffset = Image.getRelativeQuadOffset(390, 13, 14);
			relativeQuadOffset.x += 50f;
			relativeQuadOffset.y += 50f;
			VBox vbox;
			if (error != null)
			{
				vbox = new VBox().initWithOffsetAlignWidth(0f, 2, FrameworkTypes.SCREEN_WIDTH);
				this.container = new ScrollableContainer().initWithWidthHeightContainer(FrameworkTypes.SCREEN_WIDTH, relativeQuadOffset.y, vbox);
				Image.setElementPositionWithQuadOffset(this.container, 390, 13);
				this.container.x = 0f;
			}
			else
			{
				vbox = new VBox().initWithOffsetAlignWidth(0f, 2, relativeQuadOffset.x);
				this.container = new ScrollableContainer().initWithWidthHeightContainer(relativeQuadOffset.x, relativeQuadOffset.y, vbox);
				Image.setElementPositionWithQuadOffset(this.container, 390, 13);
				this.container.x -= 50f;
			}
			this.container.canSkipScrollPoints = true;
			this.addChildwithID(this.container, 1);
			if (this.playerPlace != null)
			{
				this.removeChild(this.playerPlace);
				this.playerPlace = null;
			}
			Factory.hideProcessingFromView(this);
			if (isLoading)
			{
				Factory.showProcessingOnViewwithTouchesBlocking(this, false);
				this.playerPlaceContainer.setEnabled(false);
				this.liftScrollbar.setEnabled(false);
				return;
			}
			if (error != null)
			{
				Text text = new Text().initWithFont(Application.getFont(6));
				text.setAlignment(2);
				text.setStringandWidth(NSObject.NSS(error.ToString()), 250f);
				if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
				{
					text.scaleX = 0.9f;
					text.scaleY = 0.9f;
				}
				vbox.addChild(text);
				vbox.parentAnchor = 18;
				vbox.anchor = 18;
				return;
			}
			int num = this.CurrentLeaderboard.Length;
			if (num > 0)
			{
				this.container.turnScrollPointsOnWithCapacity(num + 1);
				for (int i = 0; i < num; i++)
				{
					ScoreStruct scoreStruct = this.CurrentLeaderboard[i];
					BaseElement baseElement = this.createEntryForScoreWidth(scoreStruct, relativeQuadOffset.x);
					vbox.addChild(baseElement);
					this.container.addScrollPointAtXY(0f, baseElement.y);
				}
				if (this.container.getMaxScroll().y > 0f)
				{
					this.container.addScrollPointAtXY(0f, this.container.getMaxScroll().y);
				}
				int num2 = (num - 1) * 36;
				if (vbox.height < num2)
				{
					vbox.height = num2;
				}
				this.liftScrollbar.container = this.container;
				this.liftScrollbar.setEnabled(num > 10);
			}
			this.playerPlaceContainer.setEnabled(false);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0003F3E4 File Offset: 0x0003D5E4
		public virtual void updateScores()
		{
			this.prevb.setEnabled(this.mode != 0);
			this.nextb.setEnabled(this.mode != CTRPreferences.getPacksCount() - 1);
			if (this.mode < 0 || this.mode >= CTRPreferences.getPacksCount())
			{
				return;
			}
			this.boxTitle.setString(Application.getString(1310785 + this.mode));
			this.recreateContainerError(true, null);
			this.InitLeaderboard(this.mode);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0003F46C File Offset: 0x0003D66C
		public override void show()
		{
			this.updateScores();
			base.show();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0003F47A File Offset: 0x0003D67A
		public override void dealloc()
		{
			base.dealloc();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0003F484 File Offset: 0x0003D684
		public void onButtonPressed(int n)
		{
			switch (n)
			{
			case 0:
				this.cleanContainer();
				this.mode++;
				this.updateScores();
				return;
			case 1:
				this.cleanContainer();
				this.mode--;
				this.updateScores();
				return;
			case 2:
				break;
			case 3:
				if (!this.canpageup)
				{
					return;
				}
				this.recreateContainerError(true, null);
				this.leaderboardReader.BeginPageUp(new AsyncCallback(this.LeaderboardPageUpCallback), Gamer.SignedInGamers[PlayerIndex.One]);
				return;
			case 4:
				if (!this.canpageup)
				{
					return;
				}
				this.recreateContainerError(true, null);
				this.leaderboardReader.BeginPageUp(new AsyncCallback(this.LeaderboardPageUpCallback), Gamer.SignedInGamers[PlayerIndex.One]);
				break;
			default:
				return;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0003F550 File Offset: 0x0003D750
		protected void LeaderboardPageUpCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer != null)
			{
				try
				{
					this.leaderboardReader.EndPageUp(result);
					this.canpageup = this.leaderboardReader.CanPageUp;
					this.canpagedown = this.leaderboardReader.CanPageDown;
					this.CurrentLeaderboard = this.getLeaderboard(this.leaderboardReader);
					this.NeedsRecreate = 1;
				}
				catch (Exception)
				{
					this.canpageup = false;
					this.canpagedown = false;
				}
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
		protected void LeaderboardPageDownCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer != null)
			{
				try
				{
					this.leaderboardReader.EndPageDown(result);
					this.canpageup = this.leaderboardReader.CanPageUp;
					this.canpagedown = this.leaderboardReader.CanPageDown;
					this.CurrentLeaderboard = this.getLeaderboard(this.leaderboardReader);
					this.NeedsRecreate = 1;
				}
				catch (Exception)
				{
					this.canpageup = false;
					this.canpagedown = false;
				}
			}
		}

		// Token: 0x04000D31 RID: 3377
		private const int Y_OFFSET = 50;

		// Token: 0x04000D32 RID: 3378
		private const int X_OFFSET = 50;

		// Token: 0x04000D33 RID: 3379
		private const int ENTRY_HEIGHT = 36;

		// Token: 0x04000D34 RID: 3380
		private const int LEADERBOARDS_BUTTON_NEXT = 0;

		// Token: 0x04000D35 RID: 3381
		private const int LEADERBOARDS_BUTTON_PREVIOUS = 1;

		// Token: 0x04000D36 RID: 3382
		private const int LEADERBOARDS_BUTTON_EDIT = 2;

		// Token: 0x04000D37 RID: 3383
		private const int LEADERBOARDS_BUTTON_PAGE_UP = 3;

		// Token: 0x04000D38 RID: 3384
		private const int LEADERBOARDS_BUTTON_PAGE_DOWN = 4;

		// Token: 0x04000D39 RID: 3385
		private const int containerID = 1;

		// Token: 0x04000D3A RID: 3386
		private ScoreStruct[] CurrentLeaderboard;

		// Token: 0x04000D3B RID: 3387
		private bool canpageup;

		// Token: 0x04000D3C RID: 3388
		private bool canpagedown;

		// Token: 0x04000D3D RID: 3389
		private int NeedsRecreate;

		// Token: 0x04000D3E RID: 3390
		private LeaderboardReader leaderboardReader;

		// Token: 0x04000D3F RID: 3391
		private int mode;

		// Token: 0x04000D40 RID: 3392
		private LiftScrollbar liftScrollbar;

		// Token: 0x04000D41 RID: 3393
		private ScrollableContainer container;

		// Token: 0x04000D42 RID: 3394
		private BaseElement playerPlace;

		// Token: 0x04000D43 RID: 3395
		private Image playerPlaceContainer;

		// Token: 0x04000D44 RID: 3396
		private Button nextb;

		// Token: 0x04000D45 RID: 3397
		private Button prevb;

		// Token: 0x04000D46 RID: 3398
		private Button pageupb;

		// Token: 0x04000D47 RID: 3399
		private Button pagedownb;

		// Token: 0x04000D48 RID: 3400
		private Text boxTitle;
	}
}
