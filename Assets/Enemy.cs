using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Đoạn code trên là một script trong Unity để điều khiển đối tượng Enemy. Nó bao gồm các hàm để xử lý sức khỏe, tấn công, 
di chuyển, ẩn và xóa đối tượng Enemy. Các biến public health, speed và isFinish cho phép người dùng điều chỉnh thuộc tính của 
đối tượng Enemy. Script cũng sử dụng các hàm trong Animator để chuyển đổi giữa các trạng thái như bị tấn công, bị đánh bại và 
di chuyển. Ngoài ra, script sử dụng biến MOVE_ANIMATION để lưu tên của animation di chuyển, và spriteRenderer để ẩn đối tượng Enemy.*/

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float health = 100;
    public float speed = 3000;
    public int isFinish = 0;

    private float time;
    private string word;
    private static string MOVE_ANIMATION = "MOVE";
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFinish = PlayerPrefs.GetInt("IsFinish");
        if (isFinish == 1)
        {
            
        }
    }

    public float Health {
        set {
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }
    
    public void TakeDamage() {
        animator.SetTrigger("DAMAGE");
    }

    public void Defeated() {
        animator.SetTrigger("DEFEATED");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public void MoveAttack()
    {
        animator.SetTrigger(MOVE_ANIMATION);
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }
}
