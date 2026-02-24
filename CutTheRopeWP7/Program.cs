using System;

namespace ctr_wp7
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            using Desktop.DesktopGame game = new();
            game.Run();
        }
    }
}
