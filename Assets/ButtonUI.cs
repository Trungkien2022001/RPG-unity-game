using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public GameObject textField; //là khai báo một biến kiểu GameObject, biểu thị trường văn bản trên giao diện người dùng.
    public KeyBoardInput keyboardInput; //là khai báo một biến kiểu KeyBoardInput, đại diện cho việc nhập liệu từ bàn phím.
    public GameObject ItemField; //là khai báo một biến kiểu GameObject, biểu thị trường hiển thị hộp đựng các mục vật phẩm.
    private MusicControl musicControl; //là khai báo một biến kiểu MusicControl, biểu thị trình điều khiển âm nhạc.

    private void Start() //là phương thức được gọi khi khởi tạo một thể hiện của lớp ButtonUI. Trong phương thức này, biến ItemField sẽ được ẩn và biến musicControl sẽ được khởi tạo.
    {
        Debug.Log("Fight Scene Start");
        ItemField.SetActive(false);
        musicControl = GameObject
            .FindGameObjectWithTag(MusicControl.MUSIC_CONTROLER_TAG)
            .GetComponent<MusicControl>();
    }

    public void ItemHide() //là phương thức để ẩn hộp đựng các mục vật phẩm.
    {
        ItemField.SetActive(false);
    }

    /*
      public void RunButton() là phương thức để điều khiển hành động khi người chơi ấn nút "Run".
      Phương thức này sẽ cập nhật trạng thái "IsRun" và "TimeToLoad" của đối tượng PlayerPrefs,
      sau đó tải lại cảnh trước đó.
    */
    public void RunButton()
    {
        musicControl.PlayClick();
        PlayerPrefs.SetInt("IsRun", 1);
        PlayerPrefs.SetInt("TimeToLoad", 1);
        string value = PlayerPrefs.GetInt("IsWin", 1).ToString();
        Debug.Log("Set isWin of Run " + value);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void FightButton() //là phương thức để điều khiển hành động khi người chơi ấn nút "Fight". Phương thức này sẽ ẩn trường văn bản và bắt đầu nhập liệu từ bàn phím.
    {
        musicControl.PlayClick();
        print("Start To fight with the enemy");
        hide();
        keyboardInput.Begin();
    }

    public void ItemButton() //là phương thức để điều khiển hành động khi người chơi ấn nút "Item". Phương thức này sẽ hiển thị hộp đựng các mục vật phẩm
    {
        musicControl.PlayClick();
        print("Open Storage");
        ItemField.SetActive(true);
    }

    public void DefendButton() //là phương thức để điều khiển hành động khi người chơi ấn nút "Defend". Phương thức này sẽ in ra thông báo "Defend".
    {
        musicControl.PlayClick();
        print("Defend");
    }

    public void hide() //là phương thức để ẩn trường văn bản.
    {
        textField.SetActive(false);
    }

    public void show() //là phương thức để hiển thị trường văn bản.
    {
        textField.SetActive(true);
    }
}


//Tổng kết: Tệp mã nguồn này chứa lớp ButtonUI để xử lý các sự kiện click vào các nút trong trò chơi.
