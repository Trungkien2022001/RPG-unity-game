/*
using System.Collections; và using System.Collections.Generic; 
là khai báo các namespace được sử dụng trong đoạn mã này để 
sử dụng các lớp và cấu trúc dữ liệu thuộc các namespace đó.
*/
using System.Collections;
using System.Collections.Generic;
/*
using UnityEngine; là khai báo namespace được sử dụng để 
truy cập đến các lớp, thuộc tính, phương thức của Unity Engine.
*/
using UnityEngine;
using UnityEngine.UI;


/*
public class HealthManager : MonoBehaviour là khai báo một lớp được đặt tên là HealthManager 
kế thừa từ lớp MonoBehaviour trong Unity Engine. 
Lớp này quản lý các hoạt động liên quan đến sức khỏe của đối tượng trong trò chơi.
*/
public class HealthManager : MonoBehaviour
{
    // public Image healthBar; là khai báo một biến kiểu Image, biểu thị thanh máu hiển thị trên giao diện người dùng.
    public Image healthBar;
    // public float healthAmount = 100f; là khai báo một biến kiểu float, biểu thị số lượng máu ban đầu của đối tượng.
    public float healthAmount = 100f;
    // public KeyBoardInput keyBoardInput; là khai báo một biến kiểu KeyBoardInput, đại diện cho việc nhập liệu từ bàn phím.
    public KeyBoardInput keyBoardInput;

    // Start is called before the first frame update

    /*
    Hàm "public void TakeDamage(float damage)" là hàm để giảm điểm máu của nhân vật khi bị tấn công.
    Tham số đầu vào là một giá trị số thực biểu thị lượng sát thương nhận được.
    Hàm này sẽ giảm giá trị healthAmount (biến lưu trữ lượng máu của nhân vật)
     theo đúng lượng sát thương và cập nhật lại giá trị của thanh máu (healthBar).
    */
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    /*
    Hàm "public void Heal(float healingAmount)" là hàm để tăng điểm máu của nhân vật.
    Tham số đầu vào là một giá trị số thực biểu thị lượng máu được hồi phục.
    Hàm này sẽ tăng giá trị healthAmount theo đúng lượng máu hồi phục,
    nhưng không vượt quá giá trị tối đa (100). Sau khi tăng giá trị healthAmount,
    hàm sẽ cập nhật lại giá trị của thanh máu (healthBar).
    */

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}


//Tổng kết: Tệp mã nguồn này chứa lớp HealthManager để quản lý sức khỏe cho các nhân vật trong trò chơi.