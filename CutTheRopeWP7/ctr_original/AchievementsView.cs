using ctr_wp7.iframework.core;

namespace ctr_wp7.ctr_original
{
    internal sealed class AchievementsView : View
    {
        public static bool Init;

        public static void InitAllAchievements(object achievements)
        {
            Init = achievements != null;
        }

        public void resetScroll()
        {
        }
    }
}
