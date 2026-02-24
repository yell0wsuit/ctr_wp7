using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ctre_wp7.wp7utilities
{
    internal static class WP7Singletons
    {
        public static GraphicsDevice GraphicsDevice { get; set; }

        public static ContentManager Content { get; set; }
    }
}
