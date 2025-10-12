using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace ctre_wp7.wp7utilities
{
	// Token: 0x020000FE RID: 254
	internal class ContentHelper
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x0003C3C9 File Offset: 0x0003A5C9
		internal static string OpenResourceAsString(string name)
		{
			return new StreamReader(ContentHelper.OpenResourceAsStream(name)).ReadToEnd();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003C3DC File Offset: 0x0003A5DC
		internal static Stream OpenResourceAsStream(string resPath)
		{
			return TitleContainer.OpenStream("Content/ctr/" + resPath);
		}
	}
}
