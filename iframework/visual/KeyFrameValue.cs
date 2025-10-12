using System;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x020000A4 RID: 164
	internal class KeyFrameValue
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x00021E30 File Offset: 0x00020030
		public KeyFrameValue()
		{
			this.action = new ActionParams();
			this.scale = new ScaleParams();
			this.pos = new PosParams();
			this.rotation = new RotationParams();
			this.color = new ColorParams();
		}

		// Token: 0x040009E4 RID: 2532
		public PosParams pos;

		// Token: 0x040009E5 RID: 2533
		public ScaleParams scale;

		// Token: 0x040009E6 RID: 2534
		public RotationParams rotation;

		// Token: 0x040009E7 RID: 2535
		public ColorParams color;

		// Token: 0x040009E8 RID: 2536
		public ActionParams action;
	}
}
