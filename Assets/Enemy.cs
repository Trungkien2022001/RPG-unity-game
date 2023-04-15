using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
