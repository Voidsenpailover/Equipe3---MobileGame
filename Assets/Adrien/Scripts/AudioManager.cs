using UnityEngine;
public enum AudioSourceType
    {
        Music,
        SFX,
    }

    public enum AudioType
    {
        GameOver,
        GameStart,
        Hit,
        Terre,
        Feu,
        Eau,
        Air,
        Sel,
        Soufre,
        Phosphore,
        Pyrite,
        Mercurehit,
        MercureCash,
        fulgurite,
        World,
        UI,
        Tower,
        Music,
        Attaque,
    }


    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public AudioSource musicSource;
        public AudioSource sfxSource;
    
        public float volume = 0.5f;
        void Awake()
        {
            instance = this;
        }
    
        [System.Serializable]
        public struct AudioData
        {
            public AudioClip clip;
            public AudioType type;
        }
        public AudioData[] audioData;

        void Start()
        {
            musicSource.volume = volume;
            sfxSource.volume = volume;
        }
    
    
        public void PlaySound(AudioType type, AudioSourceType sourceType)
        {
            AudioClip clip = GetClip(type);
            switch (sourceType)
            {
                case AudioSourceType.Music:
                    musicSource.Stop();
                    musicSource.clip = clip; 
                    musicSource.loop = true;
                    musicSource.Play();
                    break;
                case AudioSourceType.SFX:
                    sfxSource.PlayOneShot(clip);
                    break;
            }
        }
    
        AudioClip GetClip(AudioType type)
        {
            foreach (AudioData data in audioData)
            {
                if(data.type == type)
                {
                    return data.clip;
                }
            }
            Debug.LogError("Pas de clip trouvé pour le type " + type);
            return null;
        }
    }