using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MusicControl musicControl;
    private void Start()
    {
        PlayerPrefs.SetInt("IsRun",0);
        musicControl = GameObject.FindGameObjectWithTag(MusicControl.MUSIC_CONTROLER_TAG).GetComponent<MusicControl>();
    }

    public void PlayGame ()
    {
        musicControl.PlayClick();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Health",100f);
    }

    public void QuitGame ()
    {
        musicControl.PlayClick();
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
