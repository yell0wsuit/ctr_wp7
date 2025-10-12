using System;
using System.Collections.Generic;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x020000A7 RID: 167
	internal class KeyFrame
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00021ED5 File Offset: 0x000200D5
		public KeyFrame()
		{
			this.value = new KeyFrameValue();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00021EE8 File Offset: 0x000200E8
		public static KeyFrame makeAction(List<Action> actions, float time)
		{
			KeyFrameValue keyFrameValue = new KeyFrameValue();
			keyFrameValue.action.actionSet = actions;
			return new KeyFrame
			{
				timeOffset = time,
				trackType = Track.TrackType.TRACK_ACTION,
				transitionType = KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR,
				value = keyFrameValue
			};
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00021F2A File Offset: 0x0002012A
		public static KeyFrame makeSingleAction(BaseElement target, string action, int p, int sp, double time)
		{
			return KeyFrame.makeSingleAction(target, action, p, sp, (float)time);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00021F38 File Offset: 0x00020138
		public static KeyFrame makeSingleAction(BaseElement target, string action, int p, int sp, float time)
		{
			List<Action> list = new List<Action>();
			list.Add(Action.createAction(target, action, p, sp));
			return KeyFrame.makeAction(list, time);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00021F62 File Offset: 0x00020162
		public static KeyFrame makePos(double x, double y, KeyFrame.TransitionType transition, double time)
		{
			return KeyFrame.makePos((int)x, (int)y, transition, (float)time);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00021F70 File Offset: 0x00020170
		public static KeyFrame makePos(int x, int y, KeyFrame.TransitionType transition, float time)
		{
			KeyFrameValue keyFrameValue = new KeyFrameValue();
			keyFrameValue.pos.x = (float)x;
			keyFrameValue.pos.y = (float)y;
			return new KeyFrame
			{
				timeOffset = time,
				trackType = Track.TrackType.TRACK_POSITION,
				transitionType = transition,
				value = keyFrameValue
			};
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00021FC0 File Offset: 0x000201C0
		public static KeyFrame makeScale(double x, double y, KeyFrame.TransitionType transition, double time)
		{
			return KeyFrame.makeScale((float)x, (float)y, transition, (float)time);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00021FD0 File Offset: 0x000201D0
		public static KeyFrame makeScale(float x, float y, KeyFrame.TransitionType transition, float time)
		{
			KeyFrameValue keyFrameValue = new KeyFrameValue();
			keyFrameValue.scale.scaleX = x;
			keyFrameValue.scale.scaleY = y;
			return new KeyFrame
			{
				timeOffset = time,
				trackType = Track.TrackType.TRACK_SCALE,
				transitionType = transition,
				value = keyFrameValue
			};
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002201E File Offset: 0x0002021E
		public static KeyFrame makeRotation(double r, KeyFrame.TransitionType transition, double time)
		{
			return KeyFrame.makeRotation((int)r, transition, (float)time);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002202C File Offset: 0x0002022C
		public static KeyFrame makeRotation(int r, KeyFrame.TransitionType transition, float time)
		{
			KeyFrameValue keyFrameValue = new KeyFrameValue();
			keyFrameValue.rotation.angle = (float)r;
			return new KeyFrame
			{
				timeOffset = time,
				trackType = Track.TrackType.TRACK_ROTATION,
				transitionType = transition,
				value = keyFrameValue
			};
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0002206F File Offset: 0x0002026F
		public static KeyFrame makeColor(RGBAColor c, KeyFrame.TransitionType transition, double time)
		{
			return KeyFrame.makeColor(c, transition, (float)time);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0002207C File Offset: 0x0002027C
		public static KeyFrame makeColor(RGBAColor c, KeyFrame.TransitionType transition, float time)
		{
			KeyFrameValue keyFrameValue = new KeyFrameValue();
			keyFrameValue.color.rgba = c;
			return new KeyFrame
			{
				timeOffset = time,
				trackType = Track.TrackType.TRACK_COLOR,
				transitionType = transition,
				value = keyFrameValue
			};
		}

		// Token: 0x040009EE RID: 2542
		public bool debugBreak;

		// Token: 0x040009EF RID: 2543
		public float timeOffset;

		// Token: 0x040009F0 RID: 2544
		public Track.TrackType trackType;

		// Token: 0x040009F1 RID: 2545
		public KeyFrame.TransitionType transitionType;

		// Token: 0x040009F2 RID: 2546
		public KeyFrameValue value;

		// Token: 0x020000A8 RID: 168
		public enum TransitionType
		{
			// Token: 0x040009F4 RID: 2548
			FRAME_TRANSITION_LINEAR,
			// Token: 0x040009F5 RID: 2549
			FRAME_TRANSITION_IMMEDIATE,
			// Token: 0x040009F6 RID: 2550
			FRAME_TRANSITION_EASE_IN,
			// Token: 0x040009F7 RID: 2551
			FRAME_TRANSITION_EASE_OUT
		}
	}
}
