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
    internal class SoundMgr : NSObject
    {
        public new SoundMgr init()
        {
            LoadedSounds = [];
            activeSounds = [];
            activeLoopedSounds = [];
            activeSoundsIds = [];
            activeLoopedSoundsIds = [];
            return this;
        }

        public static void SetContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void freeSound(int resId)
        {
            _ = LoadedSounds.Remove(resId);
        }

        public SoundEffect getSound(int resId)
        {
            if (resId is 58 or 59)
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
            asset = default;
            if (_contentManager == null)
            {
                return false;
            }
            string text = CTRResourceMgr.XNA_ResName(resId);
            foreach (string text2 in new string[] { "sounds/" + text })
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

        public virtual void stopLoopedSounds()
        {
            stopList(activeLoopedSounds);
            activeLoopedSounds.Clear();
            activeLoopedSoundsIds.Clear();
        }

        public virtual void stopAllSounds()
        {
            stopLoopedSounds();
        }

        public virtual void stopMusic()
        {
            gamePlayingMusic = false;
            LastID = -1;
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

        public virtual void suspend()
        {
        }

        public virtual void resume()
        {
        }

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

        private static void stopList(List<SoundEffectInstance> list)
        {
            foreach (SoundEffectInstance soundEffectInstance in list)
            {
                soundEffectInstance?.Stop();
            }
        }

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

        private static ContentManager _contentManager;

        private Dictionary<int, SoundEffect> LoadedSounds;

        private List<SoundEffectInstance> activeSounds;

        private List<int> activeSoundsIds;

        private List<SoundEffectInstance> activeLoopedSounds;

        private List<int> activeLoopedSoundsIds;

        private Song song;

        private int LastID = -1;

        private readonly Dictionary<int, Song> AllSongs = [];

        protected static int currentMusicId = -1;

        public static bool gamePlayingMusic;
    }
}
