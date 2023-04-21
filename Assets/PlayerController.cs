
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    // Đoạn code trên định nghĩa một class `PlayerControl` để điều khiển các hoạt động di chuyển của nhân vật người chơi. Các thuộc tính và đối tượng được sử dụng trong class bao gồm:
    // `moveSpeed`: tốc độ di chuyển của nhân vật, được định nghĩa qua một số thực.
    // `collisionOffset`: giá trị thêm vào khoảng cách chạm để xử lý va chạm.
    // `movementFilter`: bộ lọc va chạm, được dùng để xử lý va chạm giữa nhân vật và các đối tượng khác trong môi trường.
    // `enemySelect`: đối tượng đại diện cho kẻ thù của game.
    // `player`: đối tượng đại diện cho nhân vật người chơi.
    // `musicControl`: đối tượng điều khiển âm nhạc.
    // Các hàm được định nghĩa trong class bao gồm:
    // Hàm `Start()` được gọi khi bắt đầu trò chơi. Hàm sẽ kiểm tra trường hợp đang tiếp tục chơi hoặc trở về từ màn hình chiến đấu trước đó để đặt lại trò chơi qua việc sử dụng `PlayerPrefs`. Sau đó, hàm sẽ khởi tạo các biến và đối tượng cần thiết cho trò chơi.
    // Hàm `FixedUpdate()` được gọi mỗi khung hình để xử lý các hoạt động di chuyển của nhân vật. Hàm sử dụng `Input.GetAxisRaw()` để bắt đầu và kết thúc các hoạt động di chuyển dọc và ngang của nhân vật. Sau đó, công thức di chuyển được tính bằng cách nhân hướng, thời gian và tốc độ của nhân vật với nhau và gán giá trị đó cho thuộc tính `movementInput`.
    // Các hàm `Move()` và `SetMoving()`, `StartMoving()` và `StopMoving()` được sử dụng để xử lý các hoạt động di chuyển của nhân vật. Hàm `CastCollisions()` được sử dụng để xác định xem nhân vật có va chạm với các đối tượng khác trong môi trường hay không.
    // Hàm `DestroyEnemy()` được sử dụng để xóa đối tượng kẻ thù khỏi môi trường sau khi nhân vật đã chiến thắng.
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
    // Các hàm được định nghĩa trong class `PlayerControl` bao gồm:
    // Hàm `toRun()` được sử dụng trong trường hợp nhân vật muốn thực hiện cử chỉ chạy. Hàm tạm dừng hoạt động của collider để xử lý chuyển động chạy.
    // Hàm `Awake()` được gọi khi khởi tạo đối tượng `PlayerControl`. Hàm được sử dụng để đọc dữ liệu từ `PlayerPrefs` khi hoạt động bắt đầu.
    // Các hàm `PlayerPosSave()` và `PlayerPosLoad()` được sử dụng để lưu và trả về vị trí hiện tại của nhân vật trong trò chơi.
    // Hàm `TryMove()` được sử dụng để xác định xem có va chạm giữa nhân vật và môi trường hoặc kẻ thù. Nếu không, nhân vật sẽ di chuyển trong hướng được xác định bởi `direction`.
    // Hàm `OnMove()` được gọi khi người chơi bắt đầu nhập vào lệnh chuyển động. Hàm sử dụng `movementValue` để xác định hướng di chuyển của nhân vật.
    // Hàm `FixedUpdate()` được gọi mỗi khung hình để cập nhật cho chuyển động di chuyển. Hàm sử dụng `animator` và `spriteRenderer` để cập nhật trạng thái di chuyển và phản chiếu nhân vật, tùy thuộc vào hướng di chuyển.
    // Hàm `DestroyEnemy()` được sử dụng để xóa các kẻ thù trong trò chơi sau khi nhân vật đã chiến thắng. Hàm sử dụng `GameObject.Find()` và `Destroy()` để xóa các đối tượng kẻ thù.
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


    // Hàm `OnTriggerEnter2D()` trong class `PlayerControl` được sử dụng để xử lý sự kiện va chạm giữa nhân vật và kẻ thù trong trò chơi. Các hàm và đối tượng được sử dụng trong hàm bao gồm:
    // `PlayerPrefs`: lưu trữ và truy xuất các thông tin và thay đổi trạng thái của nhân vật trong trò chơi.
    // `SwitchScene()`: chuyển đổi từ màn hình hiện tại đến màn hình tiếp theo.
    // `LoadScene()`: tải màn hình mới sau khi chuyển đổi.
    // `animator`: đối tượng sử dụng các trạng thái và hoạt động di chuyển của nhân vật.
    // `musicControl`: đối tượng điều khiển âm nhạc.
    // `spriteRenderer`: đối tượng quản lý hình ảnh của nhân vật.
    // Các lệnh và hoạt động của hàm `OnTriggerEnter2D()` bao gồm:
    // Nếu nhân vật đang trong trạng thái chạy, collider của kẻ thù sẽ được đặt vào chế độ isTrigger sau 2 giây.
    // Nếu nhân vật chạm vào kẻ thù, trạng thái hiện tại của nhân vật sẽ được lưu vào `PlayerPrefs` và các hoạt động đánh kiếm và chuyển đổi scene được thực hiện.
    // Hàm `SwordAttack()` và `Hide()` được sử dụng để quản lý việc tấn công và hiển thị của nhân vật trong quá trình chuyển đổi scene.
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
