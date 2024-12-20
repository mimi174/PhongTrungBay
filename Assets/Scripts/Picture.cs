﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gắn cho các obj ảnh bên trong Phòng trưng bày Đông Vu 
//Giúp quản lý các ảnh để phát nội dung thuyết minh cho ảnh
public class Picture : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public string Name;
    public bool IsSoundPlaying { get { return AudioManager.instance.SoundIsPlaying(); } }
    private void Update()
    {
        if (PauseMenu.instance.isPaused) return;
        
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
        AudioManager.instance.PausePictureMusic();
    }

    public void ResumMusic()
    {
        AudioManager.instance.PauseInGameMusic();
        AudioManager.instance.ResumePictureMusic();
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
