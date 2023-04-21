using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{
    // Đây là lớp AnimationController kế thừa từ lớp MonoBehaviour để điều khiển các hoạt cảnh trong trò chơi.
    // Các thuộc tính được khai báo trong lớp này bao gồm:
    // moveSpeed: tốc độ di chuyển của đối tượng.
    // playerController: tham chiếu đến đối tượng PlayerController để sử dụng các thuộc tính và phương thức có sẵn của nó.
    // enemy: đối tượng Enemy sẽ được sử dụng để thực hiện các hành động tương ứng với hoạt cảnh tạo ra.
    // animator: tham chiếu đến Animator (người điều khiển hoạt cảnh) của đối tượng.
    // rb: tham chiếu đến Rigidbody2D (cơ thể rắn) của đối tượng để sử dụng trong việc xử lý va chạm vật lý.
    // spriteRenderer: tham chiếu đến Sprite Renderer của đối tượng để thay đổi hình ảnh trong các hoạt cảnh.
    // playerPosition: vị trí ban đầu của đối tượng Player.
    // enemyPosition: vị trí ban đầu của đối tượng Enemy.
    // location: vị trí hiện tại của đối tượng.
    // Các chuỗi tĩnh MUSHROOM_ANIMATION, VOLCANO_ANIMATION, FLASHLIGHT_ANIMATION, FIREEXPLOSION_ANIMATION và FIREWORK_ANIMATION được sử dụng để định danh cho các hoạt cảnh.
    // musicControl: tham chiếu đến MusicControl để điều khiển âm nhạc trong trò chơi.
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

    // Đoạn code trên là một class định nghĩa các Coroutine (hàm chạy bất đồng bộ) và các hàm trợ giúp khác để xử lý các hiệu ứng và hoạt động liên quan đến choi game đối kháng. Cụ thể:
    // Các biến `animator`, `rb`, `spriteRenderer` và `musicControl` được gán giá trị trong hàm `Start()`.
    // Hàm `Update()` được gọi mỗi frame để cập nhật logic. Trong đoạn code này, nếu vị trí (localPosition) của đối tượng không bằng với vị trí mong muốn (`location`), vị trí sẽ được di chuyển theo hướng và tốc độ xác định.
    // Hàm `PlayerEffectAttackCoroutine()` sử dụng cho các hiệu ứng tấn công của người chơi. Khi được gọi, âm thanh, hành động, sprite và vị trí của đối tượng sẽ thay đổi theo thứ tự được định nghĩa. Khi một IEnumerator được khởi động trong Coroutine, đoạn código sẽ chờ cho một khoảng thời gian xác định bằng câu lệnh `yield return` trước khi chuyển sang bước tiếp theo. Mục đích của việc này là để tạo sự trễ giữa các hành động.
    // Hàm `EnemyEffectAttackCoroutine()` tương tự như hàm `PlayerEffectAttackCoroutine()`, nhưng được sử dụng để xử lý các hiệu ứng tấn công của kẻ địch.
    // Hàm `FireworkCoroutine()` được sử dụng để thực hiện hiệu ứng pháo hoa khi một trong hai bên chiến thắng. Như các IEnumerator khác, đoạn mã sẽ chờ cho một khoảng thời gian xác định trước khi chuyển sang bước tiếp theo.
    // Hàm `FireWorkEffect(bool win)` được gọi để bắt đầu hiệu ứng pháo hoa khi trò chơi kết thúc.
    // Hàm `PlayerEffectAttack()` và `EnemyEffectAttack()` được gọi để bắt đầu hiệu ứng tấn công của người chơi và kẻ địch.
    // Hàm `RandomPlayerAnimation()` được sử dụng để chọn ngẫu nhiên một hành động tấn công của người chơi.
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
