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


/*
Đây là đoạn mã cho lớp `MusicControl`, đây là một script được sử dụng để kiểm soát âm thanh trong game.

- `MUSIC_CONTROLER_TAG` là một hằng số chuỗi được sử dụng để đánh dấu MusicController trong game.
- `click`, `flash`, và `invalid` là các biến âm thanh được sử dụng trong game.
- `isMute` là một biến bool, đại diện cho trạng thái mute/unmute của âm thanh trong game.
- `volumn` là một biến float, chứa giá trị âm lượng hiện tại của âm thanh.
- `effectVolumn` là một biến float, chứa giá trị âm lượng hiện tại của hiệu ứng âm thanh.
- `audioSource` là một đối tượng AudioSource được sử dụng để phát âm thanh.

Các phương thức trong đoạn mã bao gồm:

- `Awake()` được gọi khi script được tạo ra, nó khởi tạo `audioSource` và đặt giá trị âm lượng ban đầu.
- `PlayMusic()` được sử dụng để phát nhạc nền.
- `StopMusic()` dừng phát nhạc nền.
- `SetVolume(float value)` được sử dụng để đặt giá trị âm lượng.
- `PlayClick()`, `PlayFlash()` và `PlayInvalid()` được sử dụng để phát âm thanh các hiệu ứng khác nhau trong game.

*/
