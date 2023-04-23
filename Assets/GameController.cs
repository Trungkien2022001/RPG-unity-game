using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController _playerController;
    public ButtonUI _battleSystem;
    public Vector3 returnPoint;

    float transitionTime = 0.5f;

    /*
    SwitchScene(): phương thức này được gọi khi muốn chuyển sang cảnh mới.
    Hàm này sẽ bắt đầu một coroutine để chờ một thời gian và sau đó chuyển sang cảnh tiếp theo
    bằng cách gọi hàm LoadScene().
    */
    public void SwitchScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /*
    GoBack(): phương thức này được gọi khi muốn quay lại cảnh trước đó.
    Hàm này sẽ bắt đầu một coroutine để chờ một thời gian và sau đó chuyển về cảnh trước đó
     bằng cách gọi hàm LoadScene().
    */

    public void GoBack()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    /*
    IEnumerator LoadScene(int SceneIndex): phương thức này nhận đầu vào là một số nguyên biểu thị cho cảnh 
    cần chuyển đến. Hàm này bắt đầu một coroutine để chờ một thời gian và sau đó chuyển sang cảnh 
    mới bằng cách gọi hàm LoadScene() của lớp SceneManager. 
    Coroutine được sử dụng để cho phép thực hiện các hành động khác nhau cùng lúc với việc chờ đợi.
    */

    IEnumerator LoadScene(int SceneIndex)
    {
        //play animation
        //transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(SceneIndex);
    }
}


// Tổng kết: Tệp mã nguồn này chứa lớp GameController để quản lý trạng thái của trò chơi, chẳng hạn như điểm số, số mạng, và các đối tượng trong trò chơi.
