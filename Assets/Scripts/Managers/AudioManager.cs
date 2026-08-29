using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip pickupSound;
    public AudioClip explosionSound;
    public AudioClip buttonSound;
    public AudioClip levelCompleteSound;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        if(clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonSound()
    {
        Play(buttonSound);
    }

    public void PlayMusic(AudioClip clip)
    {
        if(clip == null) return;
        if(musicSource.clip == clip && musicSource.isPlaying) return; 
        //eger muzik caliyorsa coktan bastan baslatma.

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }
}