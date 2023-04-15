
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public EnemySelect enemySelect;

    public GameObject player;
    private MusicControl musicControl;
    
    Rigidbody2D rb;
    Vector2 movementInput;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public Animator animator;
    SpriteRenderer spriteRenderer;
    public bool canMove = true;
    public float transitionTime = 1f;

    private int EnemyCount = 10;
    // Start is called before the first frame update

    private static string SWORDATTACK_ANIMATION = "swordAttach";
    void Start()
    {
        // When starting sample scene, check if it is starting game or return from fight scene.
        // If return from fight scene, check if win or lose. if win, start player at last position and destroys enemy. lose, restart game.
        // if run, same behavior with win case.
        if (PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            float pX = player.transform.position.x;
            float pY = player.transform.position.y;
            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat(("p_y"));
            if (PlayerPrefs.GetInt("IsWin") == 1)
            {
                player.transform.position = new Vector2(pX, pY);
                DestroyEnemy();
                EnemyCount = PlayerPrefs.GetInt("EnemyCount");
                Debug.Log(PlayerPrefs.GetInt("EnemyCount"));
                PlayerPrefs.SetInt("IsWin",0);
            }
            else if (PlayerPrefs.GetInt("IsRun") == 1)
            {
                player.transform.position = new Vector2(pX, pY);
            }

            if (EnemyCount == 0)
            {
                Debug.Log(EnemyCount);
            }
            Debug.Log(pX);
            Debug.Log(pY);
            PlayerPrefs.SetInt("TimeToLoad", 0);
            PlayerPrefs.Save();
            canMove = true;
        }
        //Debug.Log(EnemyId.ToString());
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        musicControl = GameObject.FindGameObjectWithTag(MusicControl.MUSIC_CONTROLER_TAG).GetComponent<MusicControl>();
    }

    IEnumerator toRun()
    {
        GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(3f);
        GetComponent<Collider2D>().isTrigger = true;
    }

   
    
    private void Awake()
    {
        PlayerPosLoad();
    }

    // save last position of player before switch scene.
    public void PlayerPosSave()
    {
        PlayerPrefs.SetFloat("p_x",gameObject.transform.position.x);
        PlayerPrefs.SetFloat("p_y",gameObject.transform.position.y);
        PlayerPrefs.SetInt("Saved", 1);
        Debug.Log(PlayerPrefs.GetFloat("p_x"));
        Debug.Log(PlayerPrefs.GetFloat("p_y"));
        PlayerPrefs.Save();
    }
    // load player postion
    public void PlayerPosLoad()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
        Debug.Log("Load Working");
    }
    

    private void FixedUpdate()
    {
        if (canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);
                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success) {
                        success = TryMove(new Vector2(movementInput.y, 0));
                    }
                }
                animator.SetBool("isMoving", success);
            }
            else {
                animator.SetBool("isMoving", false);
            }
            // flip player based on direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
        
    }
    private bool TryMove(Vector2 direction) {
        // Check for potential collisions
        enemySelect.SetSelect(false);
        int count = rb.Cast(
                direction, 
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
        enemySelect.SetSelect(true);
        if(count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    public void DestroyEnemy()
    {
        if (PlayerPrefs.GetString("Slime") == "true")
        {
            Destroy(GameObject.Find("Slime"));
        }
        if (PlayerPrefs.GetString("Slime1") == "true")
        {
            Destroy(GameObject.Find("Slime1"));
        }
        if (PlayerPrefs.GetString("Slime2") == "true")
        {
            Destroy(GameObject.Find("Slime2"));
        }
        if (PlayerPrefs.GetString("Slime3") == "true")
        {
            Destroy(GameObject.Find("Slime3"));
        }
        if (PlayerPrefs.GetString("Slime4") == "true")
        {
            Destroy(GameObject.Find("Slime4"));
        }
        if (PlayerPrefs.GetString("Slime5") == "true")
        {
            Destroy(GameObject.Find("Slime5"));
        }
        if (PlayerPrefs.GetString("Slime6") == "true")
        {
            Destroy(GameObject.Find("Slime6"));
        }
        if (PlayerPrefs.GetString("Slime7") == "true")
        {
            Destroy(GameObject.Find("Slime7"));
        }
        if (PlayerPrefs.GetString("Slime8") == "true")
        {
            Destroy(GameObject.Find("Slime8"));
        }
        if (PlayerPrefs.GetString("Slime9") == "true")
        {
            Destroy(GameObject.Find("Slime9"));
        }
    }



    IEnumerator OnTriggerEnter2D(Collider2D other) {
        print("Touch enemy in player");
        if (PlayerPrefs.GetInt("IsRun") == 1)
        {
            other.isTrigger = false;
            yield return new WaitForSeconds(2f);
            PlayerPrefs.SetInt("IsRun",0);
            other.isTrigger = true;
        }
        else {
            if (other.tag == "Enemy") {
                PlayerPosSave();
                PlayerPrefs.SetInt("EnemyCount",EnemyCount);
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // get enemy name.
                    PlayerPrefs.SetString(enemy.name,"false");
                    Debug.Log(enemy.name);
                    // lock player move
                    canMove = false;
                    // stop animation moving
                    animator.SetBool("isMoving", false);
                    // swork attach
                    musicControl.PlayClick();
                    animator.SetTrigger(SWORDATTACK_ANIMATION);
                    //Wait attach finished
                    yield return new WaitForSeconds(transitionTime);
                    // switch scene
                    SwitchScene();
                }

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
        //transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(SceneIndex);
    }

    public void SwordAttack()
    {
        animator.SetTrigger(SWORDATTACK_ANIMATION);
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

}
