using System;

namespace ctre_wp7
{
    internal static class App
    {
        public static bool NeedsUpdate { get; set; }

        public static bool UpdateHandled { get; set; }

        public static void Quit()
        {
            Environment.Exit(0);
        }

        public static void MakeUpdatePopup()
        {
            NeedsUpdate = true;
            UpdateHandled = true;
        }

        public static void GetAchievementsCallback(IAsyncResult result)
        {
        }
    }
}
