using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{

    public const string MUSIC_CONTROLER_TAG = "Music";
    public AudioClip click;
    public AudioClip flash;
    public AudioClip invalid;
    

    private bool isMute = false;
    private float volumn = 0.3f;
    private float effectVolumn = 1f;
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumn;
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            if (!isMute)
            {
                audioSource.volume = volumn;
                audioSource.Play();
            }
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolumn(float value)
    {
        audioSource.volume = value;
        volumn = value;
    }

    public void PlayClick()
    {
        if (!isMute)
        {
            audioSource.PlayOneShot(click, effectVolumn);
        }
    }

    public void PlayFlash()
    {
        if (!isMute)
        {
            audioSource.PlayOneShot(flash, effectVolumn);
        }
    }

    public void PlayInvalid()
    {
        if (!isMute)
        {
            audioSource.PlayOneShot(invalid, effectVolumn);
        }
    }
}
