using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gắn cho obj AudioMangager
//Giúp quản lý các âm thanh trong game

public enum AudioState
{
    Playing,
    Pause,
    End
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Souce")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip[] inGame;


    private void Awake()
    {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetMuteMusic(true);
        SetMuteSounds(true);

        PlayInGameMusic();
    }

    private bool isMuteMusic;
    public void SetMuteMusic(bool mute)
    {
        if (mute)
        {
            musicSource.volume = 1f;
            musicSource.mute = false;
        }
        else {
            musicSource.volume = 0f;
            musicSource.mute = true;
        }
    }

    public (float clipLength, float soundTime) GetState()
    {
        return (soundSource.clip.length, soundSource.time);
    }

    public void SetMuteSounds(bool mute)
    {
        soundSource.mute = !mute;
    }

    public void PlayInGameMusic()
    {
        if (!musicSource.isPlaying)
        {
            if (musicSource.clip == null)
            {
                if (inGame.Length > 0)
                {
                    AudioClip audioClip = inGame[Random.Range(0, inGame.Length)];

                    if (audioClip != null)
                    {
                        musicSource.clip = audioClip;
                        musicSource.Play();
                    }
                }
            }
            else
            {
                musicSource.UnPause();
            }
        }
    }

    public void PausePictureMusic(AudioClip clip)
    {
        if (clip == null) return;
        soundSource.clip = clip;

        if (soundSource.isPlaying)
            soundSource.Pause();
    }

    public void ResumePictureMusic(AudioClip clip)
    {
        if (clip == null) return;
        soundSource.clip = clip;

        if(!soundSource.isPlaying)
            soundSource.Play();
    }

    public void PlayPictureMusic(AudioClip clip, bool repeat)
    {
        if (!soundSource.isPlaying)
        {
            if (clip != null)
            {
                if (repeat)
                {
                    soundSource.loop = true;
                    soundSource.clip = clip;
                    musicSource.Play();
                }
                else
                {
                    soundSource.loop = false;
                    soundSource.clip = clip;
                    soundSource.Play();
                }
            }
        }
        else {

            if (clip != null)
            {
                if (clip == soundSource.clip) return;

                if (repeat)
                {
                    soundSource.loop = true;
                    soundSource.clip = clip;
                    musicSource.Play();
                }
                else
                {
                    soundSource.loop = false;
                    soundSource.clip = clip;
                    soundSource.Play();
                }
            }
        }
    }

    public void StopInGameMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PauseInGameMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void StopSound()
    {
        if (soundSource.isPlaying)
        {
            soundSource.Stop();
        }
    }

    public bool SoundIsPlaying() { return soundSource.isPlaying; }
    public bool MusicIsPlaying() { return musicSource.isPlaying; }

    public void PlaySfx(AudioClip audioClip, bool repeat = false)
    {
        if (audioClip != null)
        {
            if (repeat)
            {
                soundSource.loop = true;
                soundSource.clip = audioClip;
                soundSource.Play();
            }
            else
            {
                soundSource.loop = false;
                soundSource.PlayOneShot(audioClip);
            }

        }
    }
}
