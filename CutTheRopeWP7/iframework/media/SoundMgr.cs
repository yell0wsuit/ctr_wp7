using System;
using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ctr_wp7.iframework.media
{
    // Token: 0x02000028 RID: 40
    internal class SoundMgr : NSObject
    {
        // Token: 0x06000181 RID: 385 RVA: 0x0000B63C File Offset: 0x0000983C
        public new SoundMgr init()
        {
            LoadedSounds = [];
            activeSounds = [];
            activeLoopedSounds = [];
            activeSoundsIds = [];
            activeLoopedSoundsIds = [];
            return this;
        }

        // Token: 0x06000182 RID: 386 RVA: 0x0000B676 File Offset: 0x00009876
        public static void SetContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        // Token: 0x06000183 RID: 387 RVA: 0x0000B67E File Offset: 0x0000987E
        public void freeSound(int resId)
        {
            _ = LoadedSounds.Remove(resId);
        }

        // Token: 0x06000184 RID: 388 RVA: 0x0000B690 File Offset: 0x00009890
        public SoundEffect getSound(int resId)
        {
            if (resId == 58 || resId == 59)
            {
                return null;
            }
            if (LoadedSounds.TryGetValue(resId, out SoundEffect soundEffect))
            {
                return soundEffect;
            }
            _ = TryLoadAssetWithFallback(resId, out soundEffect);
            if (soundEffect != null)
            {
                LoadedSounds.Add(resId, soundEffect);
            }
            return soundEffect;
        }

        // Token: 0x06000185 RID: 389 RVA: 0x0000B6FC File Offset: 0x000098FC
        private void ClearStopped()
        {
            List<SoundEffectInstance> list = [];
            List<int> list2 = [];
            int num = 0;
            foreach (SoundEffectInstance soundEffectInstance in activeSounds)
            {
                if (soundEffectInstance != null && soundEffectInstance.State != SoundState.Stopped)
                {
                    list.Add(soundEffectInstance);
                    list2.Add(activeSoundsIds[num]);
                }
                num++;
            }
            activeSounds.Clear();
            activeSounds = list;
            activeSoundsIds.Clear();
            activeSoundsIds = list2;
        }

        // Token: 0x06000186 RID: 390 RVA: 0x0000B7A4 File Offset: 0x000099A4
        public void ClearLooped(SoundEffectInstance sound)
        {
            for (int i = 0; i < activeLoopedSounds.Count; i++)
            {
                if (activeLoopedSounds[i] == sound)
                {
                    activeLoopedSounds.RemoveAt(i);
                    activeLoopedSoundsIds.RemoveAt(i);
                    return;
                }
            }
        }

        // Token: 0x06000187 RID: 391 RVA: 0x0000B7F0 File Offset: 0x000099F0
        public virtual void playSound(int sid)
        {
            for (int i = 0; i < activeSoundsIds.Count; i++)
            {
                if (activeSoundsIds[i] == sid)
                {
                    activeSounds[i].Stop();
                }
            }
            activeSounds.Add(play(sid, false));
            activeSoundsIds.Add(sid);
            ClearStopped();
        }

        // Token: 0x06000188 RID: 392 RVA: 0x0000B858 File Offset: 0x00009A58
        public virtual SoundEffectInstance playSoundLooped(int sid)
        {
            for (int i = 0; i < activeLoopedSoundsIds.Count; i++)
            {
                if (activeLoopedSoundsIds[i] == sid)
                {
                    activeLoopedSounds[i].Stop();
                }
            }
            ClearStopped();
            SoundEffectInstance soundEffectInstance = play(sid, true);
            activeLoopedSounds.Add(soundEffectInstance);
            activeLoopedSoundsIds.Add(sid);
            return soundEffectInstance;
        }

        // Token: 0x06000189 RID: 393 RVA: 0x0000B8C4 File Offset: 0x00009AC4
        public virtual void playMusic(int sid)
        {
            try
            {
                if (MediaPlayer.GameHasControl)
                {
                    if (!AllSongs.TryGetValue(sid, out song))
                    {
                        _ = TryLoadAssetWithFallback(sid, out song);
                        if (song == null)
                        {
                            return;
                        }
                        AllSongs.Add(sid, song);
                    }
                    MediaPlayer.IsRepeating = true;
                    if (LastID != sid)
                    {
                        MediaPlayer.Play(song);
                        LastID = sid;
                    }
                    else
                    {
                        MediaPlayer.Resume();
                    }
                    gamePlayingMusic = true;
                }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x0600018A RID: 394 RVA: 0x0000B964 File Offset: 0x00009B64
        public void LoadMusic(int sid)
        {
            if (!AllSongs.TryGetValue(sid, out song))
            {
                _ = TryLoadAssetWithFallback(sid, out song);
                if (song != null)
                {
                    AllSongs.Add(sid, song);
                }
            }
            song = null;
        }

        private static bool TryLoadAssetWithFallback<T>(int resId, out T asset) where T : class
        {
            asset = default(T);
            if (_contentManager == null)
            {
                return false;
            }
            string text = CTRResourceMgr.XNA_ResName(resId);
            foreach (string text2 in new string[] { "ctr/sounds/" + text, "sounds/" + text })
            {
                try
                {
                    asset = _contentManager.Load<T>(text2);
                    if (asset != null)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        // Token: 0x0600018B RID: 395 RVA: 0x0000B9BE File Offset: 0x00009BBE
        public virtual void stopLoopedSounds()
        {
            stopList(activeLoopedSounds);
            activeLoopedSounds.Clear();
            activeLoopedSoundsIds.Clear();
        }

        // Token: 0x0600018C RID: 396 RVA: 0x0000B9E1 File Offset: 0x00009BE1
        public virtual void stopAllSounds()
        {
            stopLoopedSounds();
        }

        // Token: 0x0600018D RID: 397 RVA: 0x0000B9EC File Offset: 0x00009BEC
        public virtual void stopMusic()
        {
            gamePlayingMusic = false;
            try
            {
                if (MediaPlayer.GameHasControl)
                {
                    MediaPlayer.Stop();
                }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x0600018E RID: 398 RVA: 0x0000BA20 File Offset: 0x00009C20
        public virtual void suspend()
        {
        }

        // Token: 0x0600018F RID: 399 RVA: 0x0000BA22 File Offset: 0x00009C22
        public virtual void resume()
        {
        }

        // Token: 0x06000190 RID: 400 RVA: 0x0000BA24 File Offset: 0x00009C24
        public virtual void pause()
        {
            try
            {
                changeListState(activeLoopedSounds, SoundState.Playing, SoundState.Paused);
                if (MediaPlayer.GameHasControl && MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000191 RID: 401 RVA: 0x0000BA68 File Offset: 0x00009C68
        public virtual void unpause()
        {
            try
            {
                changeListState(activeLoopedSounds, SoundState.Paused, SoundState.Playing);
                if (Preferences._getBooleanForKey("MUSIC_ON") && currentMusicId != -1 && (gamePlayingMusic || MediaPlayer.GameHasControl))
                {
                    playMusic(currentMusicId);
                }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000192 RID: 402 RVA: 0x0000BAC4 File Offset: 0x00009CC4
        private SoundEffectInstance play(int sid, bool l)
        {
            SoundEffectInstance soundEffectInstance = null;
            try
            {
                soundEffectInstance = getSound(sid).CreateInstance();
                soundEffectInstance.IsLooped = l;
                soundEffectInstance.Play();
            }
            catch (Exception)
            {
            }
            return soundEffectInstance;
        }

        // Token: 0x06000193 RID: 403 RVA: 0x0000BB04 File Offset: 0x00009D04
        private static void stopList(List<SoundEffectInstance> list)
        {
            foreach (SoundEffectInstance soundEffectInstance in list)
            {
                if (soundEffectInstance != null)
                {
                    soundEffectInstance.Stop();
                }
            }
        }

        // Token: 0x06000194 RID: 404 RVA: 0x0000BB54 File Offset: 0x00009D54
        private static void changeListState(List<SoundEffectInstance> list, SoundState fromState, SoundState toState)
        {
            foreach (SoundEffectInstance soundEffectInstance in list)
            {
                if (soundEffectInstance != null && soundEffectInstance.State == fromState)
                {
                    switch (toState)
                    {
                        case SoundState.Playing:
                            soundEffectInstance.Resume();
                            break;
                        case SoundState.Paused:
                            soundEffectInstance.Pause();
                            break;
                    }
                }
            }
        }

        // Token: 0x040007C1 RID: 1985
        private static ContentManager _contentManager;

        // Token: 0x040007C2 RID: 1986
        private Dictionary<int, SoundEffect> LoadedSounds;

        // Token: 0x040007C3 RID: 1987
        private List<SoundEffectInstance> activeSounds;

        // Token: 0x040007C4 RID: 1988
        private List<int> activeSoundsIds;

        // Token: 0x040007C5 RID: 1989
        private List<SoundEffectInstance> activeLoopedSounds;

        // Token: 0x040007C6 RID: 1990
        private List<int> activeLoopedSoundsIds;

        // Token: 0x040007C7 RID: 1991
        private Song song;

        // Token: 0x040007C8 RID: 1992
        private int LastID = -1;

        // Token: 0x040007C9 RID: 1993
        private Dictionary<int, Song> AllSongs = [];

        // Token: 0x040007CA RID: 1994
        protected static int currentMusicId = -1;

        // Token: 0x040007CB RID: 1995
        public static bool gamePlayingMusic;
    }
}
