using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public string Name;
    public bool IsSoundPlaying { get { return AudioManager.instance.SoundIsPlaying(); } }
    private void Update()
    {
        if (AudioManager.instance.SoundIsPlaying() == false && AudioManager.instance.MusicIsPlaying() == false)
        {
            AudioManager.instance.PlayInGameMusic();
        }
    }

    public void PlayMusic()
    {
        AudioManager.instance.PauseInGameMusic();
        AudioManager.instance.PlayPictureMusic(clip, false);
    }

    public void PauseMusic()
    {
        AudioManager.instance.PlayInGameMusic();
        AudioManager.instance.PausePictureMusic(clip);
    }

    public void ResumMusic()
    {
        AudioManager.instance.PauseInGameMusic();
        AudioManager.instance.ResumePictureMusic(clip);
    }

    public void StopMusic()
    {
        AudioManager.instance.StopSound();
        AudioManager.instance.PlayInGameMusic();
    }

    public (float clipLength, float soundTime) GetPictureState()
    {
        return AudioManager.instance.GetState();
    }
}
