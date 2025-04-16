using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load all music from Resources/Music
            musicClips = Resources.LoadAll<AudioClip>("Music");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }
}
