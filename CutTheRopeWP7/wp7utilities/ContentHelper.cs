using System;
using System.IO;

using ctr_wp7.ctr_original;

using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    // Token: 0x020000FE RID: 254
    internal class ContentHelper
    {
        // Token: 0x060007AC RID: 1964 RVA: 0x0003C3C9 File Offset: 0x0003A5C9
        internal static string OpenResourceAsString(string name)
        {
            return new StreamReader(OpenResourceAsStream(name)).ReadToEnd();
        }

        // Token: 0x060007AD RID: 1965 RVA: 0x0003C3DC File Offset: 0x0003A5DC
        internal static Stream OpenResourceAsStream(string resPath)
        {
            string text = (resPath ?? string.Empty).Replace('\\', '/').TrimStart('/');
            string text2 = (ResDataPhoneFull.ContentFolder ?? string.Empty).Replace('\\', '/').Trim('/');
            string text3 = (WP7Singletons.Content != null && !string.IsNullOrWhiteSpace(WP7Singletons.Content.RootDirectory)) ? WP7Singletons.Content.RootDirectory : "content";
            foreach (string text4 in new string[] { text3, "content", "Content" })
            {
                foreach (string text5 in (text2.Length == 0) ? new string[] { text } : new string[] { text2 + "/" + text, text })
                {
                    string text6 = (text4.TrimEnd('/') + "/" + text5.TrimStart('/')).Replace('\\', '/');
                    try
                    {
                        return TitleContainer.OpenStream(text6);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            throw new FileNotFoundException("Resource not found in content paths: " + text, text);
        }
    }
}
