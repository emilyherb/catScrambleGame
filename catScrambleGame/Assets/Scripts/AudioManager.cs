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

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        musicClips = Resources.LoadAll<AudioClip>("Music");
    }
    else if (Instance != this)
    {
        Destroy(gameObject); // Destroy the duplicate
    }
}


    public void PlayMusic(int index)
    {
        if (musicSource == null)
        {
            Debug.LogError("AudioManager: No AudioSource found!");
            return;
        }

        if (index >= 0 && index < musicClips.Length)
        {
            if (musicSource.clip != musicClips[index]) // Prevent restarting same song
            {
                musicSource.clip = musicClips[index];
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("AudioManager: Invalid music index " + index);
        }
    }
}
