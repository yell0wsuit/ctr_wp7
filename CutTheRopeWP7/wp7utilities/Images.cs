using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.wp7utilities
{
    internal static class Images
    {
        public static Texture2D get(string imgName)
        {
            if (WP7Singletons.Content == null || string.IsNullOrWhiteSpace(imgName))
            {
                return null;
            }

            foreach (string candidate in GetAssetCandidates(imgName))
            {
                try
                {
                    Texture2D texture = WP7Singletons.Content.Load<Texture2D>(candidate);
                    if (texture != null)
                    {
                        return texture;
                    }
                }
                catch (Exception)
                {
                }
            }

            return null;
        }

        public static void free(string imgName)
        {
            // Shared ContentManager lifetime is owned by the game.
        }

        private static IEnumerable<string> GetAssetCandidates(string imgName)
        {
            string normalized = imgName.Replace('\\', '/').Trim();
            HashSet<string> yielded = new HashSet<string>(StringComparer.Ordinal);

            foreach (string candidate in ExpandCandidates(normalized))
            {
                if (yielded.Add(candidate))
                {
                    yield return candidate;
                }
            }
        }

        private static IEnumerable<string> ExpandCandidates(string path)
        {
            yield return path;

            const string CtrPrefix = "ctr/";
            const string ImagesPrefix = "images/";

            if (path.StartsWith(CtrPrefix, StringComparison.OrdinalIgnoreCase))
            {
                string suffix = path.Substring(CtrPrefix.Length);
                yield return suffix;
                yield return ImagesPrefix + suffix;
                yield break;
            }

            if (path.StartsWith(ImagesPrefix, StringComparison.OrdinalIgnoreCase))
            {
                string suffix = path.Substring(ImagesPrefix.Length);
                yield return suffix;
                yield return CtrPrefix + suffix;
                yield break;
            }

            yield return CtrPrefix + path;
            yield return ImagesPrefix + path;
        }
    }
}
