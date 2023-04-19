using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private PlayerController _playerController;

   
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        print("Switch S.cene");
        if (other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) {
                // Switch Scene
                SwitchScene();
            }

        }
        
    }

    public void SwitchScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadScene(int SceneIndex)
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(SceneIndex);
    }
}

// Đây là đoạn code trong game Unity để thực hiện chuyển đổi giữa các cảnh (scene) trong game.

// Các thư viện được sử dụng là System.Collections, System.Collections.Generic, UnityEngine và UnityEngine.SceneManagement.

// Đoạn code khai báo một lớp SceneLoader là một thành phần (component) của một đối tượng (game object) trong game.

// Lớp này có hai biến công khai (public): transition và transitionTime. Biến transition là một đối tượng Animator để phát hiện chuyển tiếp giữa các cảnh, và biến transitionTime là thời gian chuyển đổi giữa các cảnh.

// Lớp này có hai phương thức công khai (public): SwitchScene() và LoadScene(int SceneIndex). Phương thức SwitchScene() được gọi khi một đối tượng va chạm với một đối tượng khác có tag "Enemy". Nó sử dụng StartCoroutine() để gọi phương thức LoadScene() với tham số SceneIndex được tính bằng cách lấy chỉ mục của cảnh hiện tại và cộng thêm một.

// Phương thức LoadScene(int SceneIndex) sử dụng biến transition để chuyển đổi giữa các cảnh. Trước khi chuyển đổi, phương thức này đặt cờ Start để kích hoạt chuyển đổi bằng cách gọi transition.SetTrigger("Start"). Sau đó, phương thức này đợi trong một thời gian transitionTime và sử dụng SceneManager.LoadScene(SceneIndex) để chuyển đổi đến cảnh mới.

// Phương thức OnTriggerEnter(Collider other) được gọi khi một đối tượng va chạm với một đối tượng khác trong game. Nếu tag của đối tượng va chạm là "Enemy", phương thức này kiểm tra xem đối tượng va chạm có là một đối tượng Enemy hay không. Nếu đúng, nó gọi phương thức SwitchScene() để chuyển đổi đến cảnh mới.