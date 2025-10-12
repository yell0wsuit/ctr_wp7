using System;
using System.IO;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;
using ctre_wp7.Specials;
using ctre_wp7.wp7utilities;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7.ctr_original
{
	// Token: 0x020000F8 RID: 248
	internal class AchievementsView : View
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0003BA14 File Offset: 0x00039C14
		public virtual BaseElement createEntryForAchievementWidth(AchievementsView.AchievementHelper achievement, float w, int scotchNum)
		{
			BaseElement baseElement = (BaseElement)new BaseElement().init();
			NSString title = achievement.title;
			NSString description = achievement.description;
			Image image = null;
			if (achievement.achieved)
			{
				ctre_wp7.iframework.visual.Texture2D texture2D = new ctre_wp7.iframework.visual.Texture2D().initWithTexture(achievement.texture);
				image = Image.Image_create(texture2D);
				Image image2 = Image.Image_createWithResID(393 + scotchNum);
				image2.y = 10f;
				image2.parentAnchor = 10;
				image2.anchor = 18;
				image.addChild(image2);
			}
			if (image == null)
			{
				image = Image.Image_createWithResID(391);
			}
			image.blendingMode = 1;
			image.parentAnchor = 9;
			image.scaleX = (image.scaleY = 0.6666667f);
			image.passTransformationsToChilds = false;
			baseElement.addChild(image);
			Text text = Text.createWithFontandString(5, title);
			text.parentAnchor = 9;
			text.anchor = 17;
			baseElement.addChild(text);
			text.x = 70f;
			text.y = 17f;
			text.rotationCenterX = (float)(-(float)text.width / 2);
			float num = 1f;
			while (text.x + (float)text.width * num > w)
			{
				num -= 0.05f;
			}
			text.scaleX = (text.scaleY = num);
			Text text2 = new Text().initWithFont(Application.getFont(6));
			text2.setStringandWidth(description, 190f);
			text2.parentAnchor = 9;
			text2.anchor = 9;
			baseElement.addChild(text2);
			text2.rotationCenterX = (float)(-(float)text2.width / 2);
			text2.x = 70f;
			text2.y = 15f;
			num = 1f;
			if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH || ResDataPhoneFull.LANGUAGE == Language.LANG_KO || ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
			{
				text2.y += 10f;
				while (text2.formattedStrings.Count > 2)
				{
					num -= 0.1f;
					text2.setStringandWidth(description, 190f / num);
					text2.scaleX = (text2.scaleY = num);
					text2.x -= 2f;
				}
				if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA && description.ToString().Length > 25)
				{
					num = 0.7f;
					text2.setStringandWidth(description, 190f / num);
					text2.scaleX = (text2.scaleY = num);
					text2.y -= 15f;
					text2.x -= 2f;
				}
				if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO && description.ToString().Length > 25)
				{
					text2.x -= 4f;
				}
			}
			else if (description.ToString().Length > 60)
			{
				num = 0.9f;
				text2.setStringandWidth(description, 190f / num);
				text2.scaleX = (text2.scaleY = num);
			}
			baseElement.height = 95;
			baseElement.width = (int)w;
			return baseElement;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003BD38 File Offset: 0x00039F38
		public virtual void recreateContainerError(bool isLoading, object error)
		{
			if (this.container != null)
			{
				this.removeChild(this.container);
			}
			Vector vector = new Vector(180f, 380f);
			vector.x += (float)this.CONTAINER_ADDITIONAL_WIDTH;
			VBox vbox = new VBox().initWithOffsetAlignWidth(0f, 2, vector.x);
			this.container = new ScrollableContainer().initWithWidthHeightContainer(vector.x, vector.y, vbox);
			this.container.canSkipScrollPoints = true;
			this.container.y += 54f;
			this.addChild(this.container);
			if (isLoading)
			{
				Factory.showProcessingOnViewwithTouchesBlocking(this, true);
				return;
			}
			Factory.hideProcessingFromView(this);
			if (error != null)
			{
				Text text = Text.createWithFontandString(6, NSObject.NSS(error.ToString()));
				text.setAlignment(2);
				vbox.addChild(text);
				vbox.parentAnchor = 18;
				vbox.anchor = 34;
				return;
			}
			if (AchievementsView.Init)
			{
				this.container.turnScrollPointsOnWithCapacity(AchievementsView.ACHIEVEMENTS.Length + 1);
				for (int i = 0; i < AchievementsView.ACHIEVEMENTS.Length; i++)
				{
					AchievementsView.AchievementHelper achievementHelper = AchievementsView.ACHIEVEMENTS[i];
					BaseElement baseElement = this.createEntryForAchievementWidth(achievementHelper, vector.x, i % 4);
					vbox.addChild(baseElement);
					this.container.addScrollPointAtXY(baseElement.x, baseElement.y);
				}
				if (this.container.getMaxScroll().y > 0f)
				{
					this.container.addScrollPointAtXY(0f, this.container.getMaxScroll().y);
				}
				this.liftScrollbar.container = this.container;
				return;
			}
			int num = 19;
			if (num > 0)
			{
				this.container.turnScrollPointsOnWithCapacity(num + 1);
				if (this.container.getMaxScroll().y > 0f)
				{
					this.container.addScrollPointAtXY(0f, this.container.getMaxScroll().y);
				}
				this.liftScrollbar.container = this.container;
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003BF50 File Offset: 0x0003A150
		public static void InitAllAchievements(AchievementCollection achievements)
		{
			try
			{
				AchievementsView.ACHIEVEMENTS = new AchievementsView.AchievementHelper[achievements.Count];
				for (int i = 0; i < achievements.Count; i++)
				{
					Achievement achievement = achievements[i];
					AchievementsView.ACHIEVEMENTS[i] = default(AchievementsView.AchievementHelper);
					AchievementsView.ACHIEVEMENTS[i].achieved = achievement.IsEarned;
					AchievementsView.ACHIEVEMENTS[i].description = NSObject.NSS(achievement.Description);
					AchievementsView.ACHIEVEMENTS[i].title = NSObject.NSS(achievement.Name);
					using (Stream picture = achievement.GetPicture())
					{
						AchievementsView.ACHIEVEMENTS[i].texture = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(WP7Singletons.GraphicsDevice, picture);
					}
					if (!Preferences._getBooleanForKey(achievement.Key) && achievement.IsEarned)
					{
						Preferences._setBooleanforKey(true, achievement.Key, true);
					}
				}
				AchievementsView.Init = true;
			}
			catch (Exception)
			{
				AchievementsView.Init = false;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003C068 File Offset: 0x0003A268
		public override NSObject initFullscreen()
		{
			if (base.initFullscreen() != null)
			{
				Image image = Image.Image_createWithResIDQuad(66, 0);
				image.scaleY = FrameworkTypes.SCREEN_BG_SCALE_Y;
				image.scaleX = FrameworkTypes.SCREEN_BG_SCALE_X;
				image.parentAnchor = (image.anchor = 18);
				this.addChild(image);
				this.liftScrollbar = LiftScrollbar.createWithResIDBackQuadLiftQuadLiftQuadPressed(388, 1, 2, 3);
				this.liftScrollbar.x = 200f;
				this.liftScrollbar.y = 60f;
				this.liftScrollbar.x += (float)this.CONTAINER_ADDITIONAL_WIDTH;
				this.liftScrollbar.blendingMode = 1;
				this.addChild(this.liftScrollbar);
				Image image2 = Image.Image_createWithResIDQuad(74, 0);
				image2.anchor = (image2.parentAnchor = 18);
				image2.scaleX = (image2.scaleY = 2f);
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
				timeline.addKeyFrame(KeyFrame.makeRotation(45.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
				timeline.addKeyFrame(KeyFrame.makeRotation(405.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 75.0));
				timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
				image2.addTimeline(timeline);
				image2.playTimeline(0);
				this.addChild(image2);
			}
			return this;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003C1B6 File Offset: 0x0003A3B6
		public override void show()
		{
			this.recreateContainerError(false, null);
			base.show();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003C1C6 File Offset: 0x0003A3C6
		public override void dealloc()
		{
			base.dealloc();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003C1CE File Offset: 0x0003A3CE
		public void resetScroll()
		{
			if (this.container != null && this.liftScrollbar != null)
			{
				this.container.show();
				this.liftScrollbar.update(0f);
			}
		}

		// Token: 0x04000CFA RID: 3322
		private static AchievementsView.AchievementHelper[] ACHIEVEMENTS;

		// Token: 0x04000CFB RID: 3323
		public static bool Init = false;

		// Token: 0x04000CFC RID: 3324
		private int CONTAINER_ADDITIONAL_WIDTH = 80;

		// Token: 0x04000CFD RID: 3325
		private ScrollableContainer container;

		// Token: 0x04000CFE RID: 3326
		private LiftScrollbar liftScrollbar;

		// Token: 0x020000F9 RID: 249
		public struct AchievementHelper
		{
			// Token: 0x04000CFF RID: 3327
			public NSString title;

			// Token: 0x04000D00 RID: 3328
			public NSString description;

			// Token: 0x04000D01 RID: 3329
			public bool achieved;

			// Token: 0x04000D02 RID: 3330
			public Microsoft.Xna.Framework.Graphics.Texture2D texture;
		}
	}
}
