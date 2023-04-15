using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{

    public float moveSpeed = 2f;
    public PlayerController playerController;
    public Enemy enemy;
    
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 playerPosition = new Vector3(-4, -1, 2f);
    private Vector3 enemyPosition = new Vector3(3f, 1f, 2f);
    private Vector3 location;
    private static string MUSHROOM_ANIMATION = "mushroom";
    private static string VOLCANO_ANIMATION = "volcano";
    private static string FLASHLIGHT_ANIMATION = "flashlight";
    private static string FIREEXPLOSION_ANIMATION = "fireexplosion";
    private static string FIREWORK_ANIMATION = "firework";
    private MusicControl musicControl;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        musicControl = GameObject.FindGameObjectWithTag(MusicControl.MUSIC_CONTROLER_TAG).GetComponent<MusicControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != location)
        {
            transform.localPosition += location * moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator PlayerEffectAttackCoroutine()
    {
        musicControl.PlayFlash();
        playerController.SwordAttack();
        animator.SetBool(FLASHLIGHT_ANIMATION, true);
        spriteRenderer.enabled = true;
        transform.localPosition = playerPosition;
        // move to new location
        location = enemyPosition;
        yield return new WaitForSeconds(1f);
        // move to enemyPosition
        transform.localPosition = enemyPosition;
        // set random player attach animation
        musicControl.PlayFlash();
        RandomPlayerAnimation();
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
    }
    
    IEnumerator EnemyEffectAttackCoroutine()
    {
        musicControl.PlayFlash();
        enemy.MoveAttack();
        // change to fire ball
        animator.SetBool(FLASHLIGHT_ANIMATION, false);
        spriteRenderer.enabled = true;
        transform.localPosition = enemyPosition;
        // move to new location
        location = playerPosition;
        yield return new WaitForSeconds(1f);
        // move to player position
        transform.localPosition = playerPosition;
        musicControl.PlayFlash();
        animator.SetTrigger(FIREEXPLOSION_ANIMATION);
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
    }

    IEnumerator FireworkCoroutine(bool win)
    {

        musicControl.PlayFlash();
        if (win)
        {
            transform.localPosition = enemyPosition;
            location = enemyPosition;
            enemy.Hide();
        }
        else
        {
            transform.localPosition = playerPosition;
            location = playerPosition;
            playerController.Hide();
        }

        
        transform.localScale = new Vector3(5f, 5f, 0);
        animator.SetTrigger(FIREWORK_ANIMATION);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(2f);
        transform.localScale = new Vector3(1f, 1f, 0);
        spriteRenderer.enabled = false;
    }

    public void FireWorkEffect(bool win)
    {
        StartCoroutine(FireworkCoroutine(win));
    }

    public void PlayerEffectAttack()
    {
        StartCoroutine(PlayerEffectAttackCoroutine());
    }

    public void EnemyEffectAttack()
    {
        StartCoroutine(EnemyEffectAttackCoroutine());
    }

    private void RandomPlayerAnimation()
    {
        System.Random rand = new System.Random();
        int value = rand.Next(1);
        switch (value)
        {
            case 0:
                animator.SetTrigger(MUSHROOM_ANIMATION);
                break;
            case 1: 
                animator.SetTrigger(VOLCANO_ANIMATION);
                break;
            default:
                animator.SetTrigger(VOLCANO_ANIMATION);
                break;
        }
    }
}
