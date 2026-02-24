using ctre_wp7.iframework.core;

namespace ctre_wp7.ctr_original
{
    internal class AchievementsView : View
    {
        public static bool Init = false;

        public static void InitAllAchievements(object achievements)
        {
            Init = achievements != null;
        }

        public void resetScroll()
        {
        }
    }
}
