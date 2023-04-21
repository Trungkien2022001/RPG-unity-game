using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{

    public Collider2D enemySelect;

    public void SetSelect(bool flag) {
        enemySelect.enabled = flag;
    }
}

// Đoạn code trên là một class đơn giản trong game engine Unity để bật hoặc tắt Collider2D cho lựa chọn kẻ địch. Cụ thể:
// Đối tượng `EnemySelect` chứa một Collider2D được lưu trữ trong biến `enemySelect`.
// Hàm `SetSelect(bool flag)` được sử dụng để bật hoặc tắt Collider2D bằng cách truyền giá trị boolean flag vào. Khi flag = true, Collider2D sẽ được bật lên để cho phép giao tác với kẻ địch. Còn khi flag = false, Collider2D sẽ được tắt đi để vô hiệu hoá các giao tác với kẻ địch.