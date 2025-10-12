using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7.wp7utilities
{
	// Token: 0x020000C7 RID: 199
	internal class WP7Singletons
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0002C72F File Offset: 0x0002A92F
		private static SpriteBatchHelper SpriteBatchHelper
		{
			get
			{
				if (WP7Singletons.spriteBatchHelper_ == null)
				{
					WP7Singletons.spriteBatchHelper_ = new SpriteBatchHelper();
				}
				return WP7Singletons.spriteBatchHelper_;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0002C747 File Offset: 0x0002A947
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0002C74E File Offset: 0x0002A94E
		public static SpriteBatch SpriteBatch
		{
			get
			{
				return WP7Singletons.spriteBatch_;
			}
			set
			{
				WP7Singletons.spriteBatch_ = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0002C756 File Offset: 0x0002A956
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0002C75D File Offset: 0x0002A95D
		public static GraphicsDevice GraphicsDevice
		{
			get
			{
				return WP7Singletons.graphicsDevice_;
			}
			set
			{
				WP7Singletons.graphicsDevice_ = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0002C765 File Offset: 0x0002A965
		private static Camera2d Camera2d
		{
			get
			{
				if (WP7Singletons.camera2d_ == null)
				{
					WP7Singletons.camera2d_ = new Camera2d(new Vector2((float)ScreenSizes.Width2, (float)ScreenSizes.Height2));
				}
				return WP7Singletons.camera2d_;
			}
		}

		// Token: 0x04000B37 RID: 2871
		private static SpriteBatchHelper spriteBatchHelper_;

		// Token: 0x04000B38 RID: 2872
		private static SpriteBatch spriteBatch_;

		// Token: 0x04000B39 RID: 2873
		private static GraphicsDevice graphicsDevice_;

		// Token: 0x04000B3A RID: 2874
		private static Camera2d camera2d_;
	}
}
