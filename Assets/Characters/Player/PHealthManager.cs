using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount;
    public KeyBoardInput keyBoardInput;
    // Start is called before the first frame update
    private void Start()
    {
        healthAmount = PlayerPrefs.GetFloat("Health");
        Debug.Log(healthAmount);
        healthBar.fillAmount = healthAmount/100f;
        
    }
    
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }
    
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount /100f;
    }
}
// Đây là một đoạn mã nguồn được viết bằng ngôn ngữ lập trình C# trên Unity. Cụ thể, đoạn mã này liên quan đến việc quản lý thanh máu của nhân vật và bao gồm các thành phần sau:

// Các thư viện: using System, using System.Collections, using System.Collections.Generic, using UnityEngine, using UnityEngine.UI.
// Lớp PHealthManager: lớp này được định nghĩa là một lớp kế thừa từ lớp MonoBehaviour trong Unity.
// Các biến thành viên của lớp PHealthManager:
// healthBar: một đối tượng Image, đại diện cho thanh máu trên giao diện người dùng.
// healthAmount: một số thực, đại diện cho số lượng máu hiện tại của nhân vật.
// keyBoardInput: một đối tượng KeyBoardInput, có thể được sử dụng để điều khiển hành động của nhân vật.
// Phương thức Start: phương thức này được gọi khi đối tượng được khởi tạo trong Unity. Nó khởi tạo giá trị của healthAmount bằng cách lấy giá trị từ PlayerPrefs và thiết lập giá trị fillAmount cho thanh máu.
// Phương thức TakeDamage: phương thức này giảm lượng máu của nhân vật bằng số lượng damage và cập nhật giá trị fillAmount cho thanh máu.
// Phương thức Heal: phương thức này tăng lượng máu của nhân vật bằng số lượng healingAmount và giới hạn lượng máu trong khoảng từ 0 đến 100. Sau đó, nó cập nhật giá trị fillAmount cho thanh máu.
// Với đoạn mã này, người dùng có thể quản lý thanh máu của nhân vật và điều khiển hành động của nhân vật dựa trên giá trị của thanh máu.