using UnityEngine;

namespace Sound
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        [Header("AudioSource")]
        public AudioSource musicSource;
        public AudioSource sfxSource;

        [Header("Audio Clips")]
        public AudioClip gameMusic;
        public AudioClip whistle;
        public AudioClip stunned;
        public AudioClip coinCollect;
        public AudioClip gemCollect;

        private void Awake()
        {
            // Singleton Pattern to ensure a single instance of AudioManager exists across the application
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Check if AudioSources are assigned
            if (musicSource == null || sfxSource == null)
            {
                Debug.LogWarning("AudioSource(s) not assigned in AudioManager! Please attach them in the Inspector.");
            }
        }

        private void Start()
        {
            // Play background music if available
            if (musicSource != null && gameMusic != null)
            {
                musicSource.clip = gameMusic;
                musicSource.loop = true; // Ensure the music loops
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning("MusicSource or gameMusic clip is not assigned. Background music will not play.");
            }
        }

        /// <summary>
        /// Plays a Sound Effect through the SFX AudioSource
        /// </summary>
        /// <param name="clip">The AudioClip to play</param>
        public void PlaySoundEffect(AudioClip clip)
        {
            if (sfxSource == null)
            {
                Debug.LogError("SFX Source is null in AudioManager. Unable to play sound effects.");
                return;
            }

            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Attempted to play a null AudioClip on SFX Source.");
            }
        }

        /// <summary>
        /// Stops the background music.
        /// </summary>
        public void StopMusic()
        {
            if (musicSource != null && musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            else
            {
                Debug.LogWarning("Cannot stop music: MusicSource is null or music is not playing.");
            }
        }

        /// <summary>
        /// Changes the background music to a new AudioClip and starts playing it.
        /// </summary>
        /// <param name="newMusic">The new background music clip</param>
        public void ChangeMusic(AudioClip newMusic)
        {
            if (musicSource == null)
            {
                Debug.LogError("MusicSource is null in AudioManager. Cannot change music.");
                return;
            }

            if (newMusic != null)
            {
                musicSource.Stop();
                musicSource.clip = newMusic;
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning("Attempted to change music to a null AudioClip.");
            }
        }

        /// <summary>
        /// Plays one of the predefined sound effect clips by name.
        /// </summary>
        /// <param name="clipName">The name of the sound effect to play</param>
        public void PlayPredefinedSoundEffect(string clipName)
        {
            switch (clipName.ToLower())
            {
                case "whistle":
                    PlaySoundEffect(whistle);
                    break;

                case "stunned":
                    PlaySoundEffect(stunned);
                    break;

                case "coin":
                case "coin collect":
                    PlaySoundEffect(coinCollect);
                    break;

                case "gem":
                case "gem collect":
                    PlaySoundEffect(gemCollect);
                    break;

                default:
                    Debug.LogWarning($"No predefined sound effect found for: {clipName}");
                    break;
            }
        }

        /// <summary>
        /// Gets the singleton instance of AudioManager
        /// </summary>
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("AudioManager instance is null! Ensure that the AudioManager is present in the scene.");
                }
                return _instance;
            }
        }
    }
}