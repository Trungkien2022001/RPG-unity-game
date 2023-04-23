using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Đoạn code trên triển khai một menu tạm dừng (pause menu) cho game. Khi nhấn phím Esc, nếu trò chơi đang ở trạng thái chưa tạm dừng, 
thì trò chơi sẽ bị tạm dừng, menu tạm dừng được hiển thị và biến GameIsPaused được đặt thành true. Nếu trò chơi đã bị tạm dừng, 
thì trò chơi sẽ tiếp tục chạy, menu tạm dừng sẽ bị ẩn và biến GameIsPaused được đặt thành false.

Ngoài ra, đoạn code cũng cung cấp các phương thức để thoát game và quay lại menu chính.*/

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
