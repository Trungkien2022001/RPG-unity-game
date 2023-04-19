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

// Đoạn code trên là một script trong Unity để điều khiển chức năng của MainMenu trong game.

// Đầu tiên, script khai báo các namespace cần thiết và class MainMenu được kế thừa từ MonoBehaviour.

// Các biến private musicControl và IsRun được khởi tạo trong hàm Start().

// Phương thức PlayGame() được gọi khi người chơi chọn nút "Play" trong MainMenu. Khi được gọi, nó sẽ chơi âm thanh click thông qua phương thức PlayClick() của đối tượng musicControl. Sau đó, đoạn code sẽ chuyển đổi scene đến scene có index là 1 bằng cách gọi phương thức LoadScene() của lớp SceneManager. Ngoài ra, một số lượng lớn các giá trị người dùng được lưu trữ bởi PlayerPrefs được xóa bằng cách gọi phương thức DeleteAll() và giá trị sức khỏe ban đầu được gán bằng 100.

// Phương thức QuitGame() được gọi khi người chơi chọn nút "Quit". Nó sẽ cũng chơi âm thanh click, in ra thông báo "QUIT!" trên Console và thoát khỏi ứng dụng bằng cách gọi phương thức Quit() của lớp Application.