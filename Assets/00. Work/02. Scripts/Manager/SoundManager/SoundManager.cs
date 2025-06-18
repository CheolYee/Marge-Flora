using System.Collections.Generic;
using System.IO;
using _00._Work._02._Scripts.Save;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.SoundManager
{
    public class SoundManager : MonoSingleton<SoundManager>
    {

        [Header("Audio Sources")] public AudioSource audioSource;
        public AudioSource sfxSource;

        [Header("Sound Effects")] public List<AudioClip> audioClips;
        public List<AudioClip> sfxClips;
        

        private static string SoundSavePath => Application.persistentDataPath + "/soundData.json";

        private SoundSettings _settings = new SoundSettings();
        
        public float GetBGMVolume() => _settings.bgmVolume;
        public float GetSfxVolume() => _settings.sfxVolume;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
                DontDestroyOnLoad(this);
            LoadSettings();
            ApplyVolume();
        }

        public void PlayBgm(string soundName)
        {
            var clip = audioClips.Find(x => x.name == soundName);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        public void PlaySfx(string soundName)
        {
            var clip = sfxClips.Find(x => x.name == soundName);
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
            else
            {
                Logging.LogWarning($"클립 {soundName}이 존재하지 않습니다.");
            }
        }

        public void SetSfxVolume(float volume)
        {
            _settings.sfxVolume = volume;
            ApplyVolume();
            SaveSettings();
        }
        
        public void SetBgmVolume(float volume)
        {
            _settings.bgmVolume = volume;
            ApplyVolume();
            SaveSettings();
        }

        private void SaveSettings()
        {
            var json = JsonUtility.ToJson(_settings);
            File.WriteAllText(SoundSavePath, json);
        }

        private void LoadSettings()
        {
            if (File.Exists(SoundSavePath))
            {
                var json = File.ReadAllText(SoundSavePath);
                _settings = JsonUtility.FromJson<SoundSettings>(json);
            }
            else
            {
                _settings = new SoundSettings
                {
                    volume = 0.5f,
                    bgmVolume = 0.5f,
                    sfxVolume = 0.5f,
                };
            }     
        }

        private void ApplyVolume()
        {
            audioSource.volume = _settings.bgmVolume;
            sfxSource.volume = _settings.sfxVolume;
        }
    }
}
