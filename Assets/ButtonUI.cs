using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public GameObject textField;

	public KeyBoardInput keyboardInput;
    public GameObject ItemField;

    private MusicControl musicControl;

    private void Start()
    {
        Debug.Log("Fight Scene Start");
        ItemField.SetActive(false);
        musicControl = GameObject.FindGameObjectWithTag(MusicControl.MUSIC_CONTROLER_TAG).GetComponent<MusicControl>();
    }
    
    public void ItemHide()
    {
        ItemField.SetActive(false);
    }

    //Creating fight scene button
    public void RunButton()
    {
        musicControl.PlayClick();
        PlayerPrefs.SetInt("IsRun", 1);
        PlayerPrefs.SetInt("TimeToLoad",1);
        string value = PlayerPrefs.GetInt("IsWin", 1).ToString();
        Debug.Log("Set isWin of Run " + value);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void FightButton()
    {
        musicControl.PlayClick();
        print("Start To fight with the enemy");
        hide();
		keyboardInput.Begin();
    }

    public void ItemButton()
    {
        musicControl.PlayClick();
        print("Open Storage");
        ItemField.SetActive(true);
        
    }

    public void DefendButton()
    {
        musicControl.PlayClick();
        print("Defend");
    }
    
    public void hide()
    {
        textField.SetActive(false);
    }
    
    public void show()
    {
        textField.SetActive(true);
    }
}
