using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

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

    public void StopMusic()
    {
        AudioManager.instance.StopSound();
    }
}
