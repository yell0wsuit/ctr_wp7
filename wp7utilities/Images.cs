using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7.wp7utilities
{
	// Token: 0x02000044 RID: 68
	internal class Images
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000EF24 File Offset: 0x0000D124
		private static ContentManager getContentManager(string imgName)
		{
			ContentManager contentManager = null;
			Images._contentManagers.TryGetValue(imgName, ref contentManager);
			if (contentManager == null)
			{
				contentManager = new ContentManager((Application.Current as App).Services, "Content");
				Images._contentManagers.Add(imgName, contentManager);
			}
			return contentManager;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000EF6C File Offset: 0x0000D16C
		public static Texture2D get(string imgName)
		{
			ContentManager contentManager = Images.getContentManager(imgName);
			Texture2D texture2D = null;
			try
			{
				texture2D = contentManager.Load<Texture2D>(imgName);
			}
			catch (Exception)
			{
			}
			return texture2D;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public static void free(string imgName)
		{
			ContentManager contentManager = Images.getContentManager(imgName);
			contentManager.Unload();
		}

		// Token: 0x0400082B RID: 2091
		private static Dictionary<string, ContentManager> _contentManagers = new Dictionary<string, ContentManager>();
	}
}
