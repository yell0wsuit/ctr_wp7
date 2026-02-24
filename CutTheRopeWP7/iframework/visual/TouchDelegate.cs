using System;
using System.Collections.Generic;
using ctr_wp7.ctr_commons;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x0200000B RID: 11
	internal interface TouchDelegate
	{
		// Token: 0x060000C0 RID: 192
		bool touchesBeganwithEvent(List<CTRTouchState> touches);

		// Token: 0x060000C1 RID: 193
		bool touchesEndedwithEvent(List<CTRTouchState> touches);

		// Token: 0x060000C2 RID: 194
		bool touchesMovedwithEvent(List<CTRTouchState> touches);

		// Token: 0x060000C3 RID: 195
		bool touchesCancelledwithEvent(List<CTRTouchState> touches);

		// Token: 0x060000C4 RID: 196
		bool backButtonPressed();

		// Token: 0x060000C5 RID: 197
		bool menuButtonPressed();
	}
}
