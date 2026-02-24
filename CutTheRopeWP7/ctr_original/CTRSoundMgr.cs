using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.media;

using Microsoft.Xna.Framework.Audio;

namespace ctr_wp7.ctr_original
{
    internal sealed class CTRSoundMgr : SoundMgr
    {
        public static void _playSound(int s)
        {
            if (Preferences._getBooleanForKey("SOUND_ON"))
            {
                Application.sharedSoundMgr().playSound(s);
            }
        }

        public static void EnableLoopedSounds(bool bEnable)
        {
            s_EnableLoopedSounds = bEnable;
            if (!s_EnableLoopedSounds)
            {
                _stopLoopedSounds();
            }
        }

        public static SoundEffectInstance _playSoundLooped(int s)
        {
            return s_EnableLoopedSounds && Preferences._getBooleanForKey("SOUND_ON") ? Application.sharedSoundMgr().playSoundLooped(s) : null;
        }

        public static void _playMusic(int f)
        {
            currentMusicId = f;
            if (Preferences._getBooleanForKey("MUSIC_ON"))
            {
                Application.sharedSoundMgr().playMusic(f);
            }
        }

        public static void _stopLoopedSounds()
        {
            Application.sharedSoundMgr().stopLoopedSounds();
        }

        public static void _stopSounds()
        {
            Application.sharedSoundMgr().stopAllSounds();
        }

        public static void _stopAll()
        {
            _stopSounds();
            _stopMusic();
        }

        public static void _stopMusic()
        {
            Application.sharedSoundMgr().stopMusic();
        }

        public static void _pause()
        {
            Application.sharedSoundMgr().pause();
        }

        public static void _unpause()
        {
            int activeChildID = Application.sharedRootController().activeChildID;
            if (activeChildID == 2)
            {
                return;
            }
            if (activeChildID == 3)
            {
                GameController gameController = (GameController)Application.sharedRootController().getCurrentController();
                if (gameController.isGamePaused && !gameController.BoxCloseHandled && !gameController.BoxLevelWonClosing)
                {
                    return;
                }
            }
            Application.sharedSoundMgr().unpause();
        }

        private static bool s_EnableLoopedSounds = true;
    }
}
