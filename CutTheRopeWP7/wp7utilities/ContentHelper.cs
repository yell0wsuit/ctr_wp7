using System;
using System.IO;

using ctr_wp7.ctr_original;

using Microsoft.Xna.Framework;

namespace ctr_wp7.wp7utilities
{
    internal sealed class ContentHelper
    {
        internal static string OpenResourceAsString(string name)
        {
            return new StreamReader(OpenResourceAsStream(name)).ReadToEnd();
        }

        internal static Stream OpenResourceAsStream(string resPath)
        {
            string text = (resPath ?? string.Empty).Replace('\\', '/').TrimStart('/');
            string text2 = (ResDataPhoneFull.ContentFolder ?? string.Empty).Replace('\\', '/').Trim('/');
            string text3 = (WP7Singletons.Content != null && !string.IsNullOrWhiteSpace(WP7Singletons.Content.RootDirectory)) ? WP7Singletons.Content.RootDirectory : "content";
            foreach (string text4 in new string[] { text3, "content", "Content" })
            {
                foreach (string text5 in (text2.Length == 0) ? new string[] { text } : [text2 + "/" + text, text])
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
