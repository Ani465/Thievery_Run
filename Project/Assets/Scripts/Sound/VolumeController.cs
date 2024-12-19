using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    public class VolumeController : MonoBehaviour
    {
        public Slider musicSlider;
        public Slider soundEffectSlider;
        public AudioSource musicSource;
        public AudioSource sfxSource;

        private void Start()
        {
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 1f);
            }
            if (!PlayerPrefs.HasKey("SFXVolume"))
            {
                PlayerPrefs.SetFloat("SFXVolume", 1f);
            }

            LoadMusicVolume();
            LoadSfxVolume();
        }

        public void SetMusicVolume()
        {
            if (musicSource != null)
            {
                musicSource.volume = musicSlider.value;
                SaveMusicVolume();
            }
            else
            {
                Debug.LogWarning("MusicSource is null or destroyed. Please ensure it exists before calling SetMusicVolume().");
                musicSlider.onValueChanged.RemoveAllListeners(); // Optionally remove listeners
            }
        }

        public void SetSfxVolume()
        {
            if (sfxSource != null)
            {
                sfxSource.volume = soundEffectSlider.value;
                SaveSfxVolume();
            }
            else
            {
                Debug.LogWarning("SFXSource is null or destroyed. Please ensure it exists before calling SetSfxVolume().");
                soundEffectSlider.onValueChanged.RemoveAllListeners(); // Optionally remove listeners
            }
        }

        private void LoadMusicVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            if (musicSource != null)
            {
                musicSource.volume = musicSlider.value;
            }
            else
            {
                Debug.LogWarning("MusicSource is null or destroyed during LoadMusicVolume.");
            }
        }

        private void SaveMusicVolume()
        {
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        }

        private void LoadSfxVolume()
        {
            soundEffectSlider.value = PlayerPrefs.GetFloat("SFXVolume");

            if (sfxSource != null)
            {
                sfxSource.volume = soundEffectSlider.value;
            }
            else
            {
                Debug.LogWarning("SFXSource is null or destroyed during LoadSfxVolume.");
            }
        }

        private void SaveSfxVolume()
        {
            PlayerPrefs.SetFloat("SFXVolume", soundEffectSlider.value);
        }
    }
}