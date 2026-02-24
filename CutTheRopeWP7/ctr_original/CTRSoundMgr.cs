using System;

using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.media;

using Microsoft.Xna.Framework.Audio;

namespace ctr_wp7.ctr_original
{
    // Token: 0x02000029 RID: 41
    internal class CTRSoundMgr : SoundMgr
    {
        // Token: 0x06000197 RID: 407 RVA: 0x0000BBF0 File Offset: 0x00009DF0
        public static void _playSound(int s)
        {
            if (Preferences._getBooleanForKey("SOUND_ON"))
            {
                Application.sharedSoundMgr().playSound(s);
            }
        }

        // Token: 0x06000198 RID: 408 RVA: 0x0000BC09 File Offset: 0x00009E09
        public static void EnableLoopedSounds(bool bEnable)
        {
            CTRSoundMgr.s_EnableLoopedSounds = bEnable;
            if (!CTRSoundMgr.s_EnableLoopedSounds)
            {
                CTRSoundMgr._stopLoopedSounds();
            }
        }

        // Token: 0x06000199 RID: 409 RVA: 0x0000BC1D File Offset: 0x00009E1D
        public static SoundEffectInstance _playSoundLooped(int s)
        {
            if (CTRSoundMgr.s_EnableLoopedSounds && Preferences._getBooleanForKey("SOUND_ON"))
            {
                return Application.sharedSoundMgr().playSoundLooped(s);
            }
            return null;
        }

        // Token: 0x0600019A RID: 410 RVA: 0x0000BC3F File Offset: 0x00009E3F
        public static void _playMusic(int f)
        {
            SoundMgr.currentMusicId = f;
            if (Preferences._getBooleanForKey("MUSIC_ON"))
            {
                Application.sharedSoundMgr().playMusic(f);
            }
        }

        // Token: 0x0600019B RID: 411 RVA: 0x0000BC5E File Offset: 0x00009E5E
        public static void _stopLoopedSounds()
        {
            Application.sharedSoundMgr().stopLoopedSounds();
        }

        // Token: 0x0600019C RID: 412 RVA: 0x0000BC6A File Offset: 0x00009E6A
        public static void _stopSounds()
        {
            Application.sharedSoundMgr().stopAllSounds();
        }

        // Token: 0x0600019D RID: 413 RVA: 0x0000BC76 File Offset: 0x00009E76
        public static void _stopAll()
        {
            CTRSoundMgr._stopSounds();
            CTRSoundMgr._stopMusic();
        }

        // Token: 0x0600019E RID: 414 RVA: 0x0000BC82 File Offset: 0x00009E82
        public static void _stopMusic()
        {
            Application.sharedSoundMgr().stopMusic();
        }

        // Token: 0x0600019F RID: 415 RVA: 0x0000BC8E File Offset: 0x00009E8E
        public static void _pause()
        {
            Application.sharedSoundMgr().pause();
        }

        // Token: 0x060001A0 RID: 416 RVA: 0x0000BC9C File Offset: 0x00009E9C
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

        // Token: 0x040007CC RID: 1996
        private static bool s_EnableLoopedSounds = true;
    }
}
