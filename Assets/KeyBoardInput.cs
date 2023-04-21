using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class KeyBoardInput : MonoBehaviour
{
	// Đoạn code trên là một class trong game Unity dùng để xử lý các sự kiện khi người chơi nhập liệu từ bàn phím. Các thành phần của class bao gồm:
	// Các biến `text`, `TimerText`, `enemyHealthManager`, `playerHealthManager`, `buttonUI`, `particalSystem`, `currentDamage`, `PlayerMessText`, `PlayerMessageObject`, `animationController`, `EnemyMessText`, `EnemyMessageObject`, `playerController`, `enemy` được định nghĩa để sử dụng trong class.
	// Các biến `playerInput`, `sentence`, `playing`, `compare`, `CORRECT_COLOR_OPEN_TAG`, `INCORRECT_COLOR_OPEN_TAG`, `NOT_TYPING_COLOR_TAG`, `COLOR_END_TAG`, `COLOR_TIMER_TAG`, `accuracy`, `EnemyId`, `EnemyCount2`, `currentHealth`, `size` được sử dụng để xử lý logic của class.
	// Hàm `Start()` được gọi khi đối tượng được khởi tạo để khởi tạo giá trị của các biến.
	// Trong đó, hàm `Update()` được sử dụng để cập nhật trạng thái của đối tượng và logic của trò chơi. Trong hàm này, điều kiện `If (playing)` (nếu đang trong trò chơi) được sử dụng để kiểm tra trạng thái của trò chơi và người chơi có thể nhập liệu từ bàn phím. Nếu trò chơi đang chạy, thời gian sẽ giảm dần và tăng điểm số. Hoặc nếu thời gian hết mà vẫn chưa hoàn thành nội dung, người chơi sẽ bị trừ máu.
	// Hàm `AddKey(string key)` được sử dụng để thêm ký tự mới vào chuỗi `playerInput` khi người chơi nhập từ bàn phím. Nếu đủ số ký tự của câu, thì so sánh chuỗi người chơi nhập vào với chuỗi câu đã định nghĩa. Sau đó, dựa vào kết quả so sánh, tính toán độ chính xác và truyền các thông số liên quan đến chơi game như damage, máu, điểm số, thời gian cho các thành phần khác trong trò chơi.
	// Hàm `StartGame(int level)` được sử dụng để bắt đầu trò chơi, chọn một câu hỏi đúng với cấp độ đã chọn.
	// Hàm `RandomString(int length)` được sử dụng để tạo ra một chuỗi ngẫu nhiên với độ dài `length`.
    public TMP_Text text;
    public TMP_Text TimerText;
    public float timeRemaining = 10;
    public HealthManager enemyHealthManager;
    public PHealthManager playerHealthManager;
    public ButtonUI buttonUI;
    public ParticleSystem particalSystem;
    public float currentDamage;
    public TMP_Text PlayerMessText;
    public GameObject PlayerMessageObject;
    public AnimationController animationController;
    
    public TMP_Text EnemyMessText;
    public GameObject EnemyMessageObject;
    
    public PlayerController playerController;
    public Enemy enemy;
    private StringBuilder playerInput = new StringBuilder("");
	private string sentence = "";
	private bool playing = false;
	private int[] compare;
	private string CORRECT_COLOR_OPEN_TAG = "<color=#207F20>";
	private string INCORRECT_COLOR_OPEN_TAG = "<color=#de0e3a>";
	private string NOT_TYPING_COLOR_TAG = "<color=#F0E1C5>";
	private string COLOR_END_TAG = "</color>";
	private string COLOR_TIMER_TAG = "<color=#442A14>";
	
	private String[] sentenceList;
	private float totalChar;
	private float correctChar;
	private float accuracy;
	private int EnemyId;
	private int EnemyCount2;
	private float currentHealth;
	private int size;

	// Đoạn code trên là cùng class với đoạn mã trong câu hỏi trước, với phần code tổ chức trò chơi. Các thành phần của class bao gồm:
	// Biến `sentenceList` được khởi tạo để lưu danh sách câu hỏi từ hệ thống.
	// Biến `PlayerMessageObject` và `EnemyMessageObject` được sử dụng để ẩn và hiện thông báo cho người chơi.
	// Biến `EnemyCount2` và `currentHealth` được sử dụng để lấy thông tin về máu và vị trí của các kẻ địch.
	// Trong hàm `Update()`, điều kiện `If (playing)` sẽ kiểm tra trạng thái của trò chơi. Nếu trò chơi đang chạy, người chơi có thể nhập liệu từ bàn phím, thời gian sẽ giảm dần và điểm số sẽ tăng theo độ chính xác của người chơi.
	// Hàm `Begin()` sẽ được gọi khi người chơi bắt đầu chơi trò chơi. Câu hỏi sẽ được lấy ngẫu nhiên và kích thước của câu hỏi sẽ được lưu lại.
	// Các hàm hỗ trợ `CompareInput()`, `ShowText()`, `ShowTime()`, `FightTurn()`, `Reset()`, `ResetButton()` được định nghĩa để xử lý và cập nhật trạng thái, thông tin của trò chơi.
	// Trong hàm `Update()`, nếu thời gian đã hết, trò chơi sẽ dừng lại. Khi đó, thông tin liên quan đến máu, vị trí, điểm số của người chơi và kẻ địch cũng được cập nhật.
    // Start is called before the first frame update
    void Start()
    {
		sentenceList = WordGenerator.GenerateDict();
	    PlayerMessageObject.SetActive(false);
		EnemyMessageObject.SetActive(false);
		EnemyCount2 = PlayerPrefs.GetInt("EnemyCount");
		currentHealth = PlayerPrefs.GetFloat("Health");
    }

    // Update is called once per frame
    void Update()
    {
		if (playing == true) {
			if (Input.GetKeyDown(KeyCode.Backspace)) {
				// only remove when the player input something
				if (playerInput.Length > 0)
				{
					playerInput.Length --;
				}
				
			}
        	else if (Input.anyKeyDown&& playerInput.Length<size) {
				if(playerInput.Length>((double)size*.8)){
					//add new sentence rm half of current sentence
					String newSent = WordGenerator.GenerateSentence(sentenceList);
					sentence+= " "+newSent;
					
					size+=(newSent.Length)+1;	
							
				}
            	playerInput.Append(Input.inputString);
			}	
			CompareInput();
			ShowText();
			ShowTime();
			
		}
		if (timeRemaining <= 0)
		{
			playing = false;
			Debug.Log("Correct: " + accuracy);
			if (enemyHealthManager.healthAmount > 0 && playerHealthManager.healthAmount > 0)
			{
				// reset all the status of the game
				StartCoroutine(FightTurn());
				Reset();
				StartCoroutine(ResetButton());
				
			}
			
		}
    }

	public void Begin() {
		if (!playing) {
			sentence = WordGenerator.GenerateSentence(sentenceList);
			playing = true;
			size = sentence.Length;
		}
	}
	// Đoạn code trên tạo ra một hàm `FightTurn()` để quản lý lượt chơi và cập nhật trạng thái của trò chơi, bao gồm:
	// Nếu thời gian đã hết, hàm sẽ tính toán damage và hiển thị thông điệp trên màn hình để thông báo với người chơi về kết quả lượt đánh.
	// Nếu máu của kẻ địch đã hết, hàm sẽ lưu lại thông tin chiến thắng của người chơi và cập nhật các thông số người chơi như số lượng kẻ địch còn lại, sau đó chuyển về màn hình chính. Nếu máu của người chơi đã hết, trò chơi cũng kết thúc và thông báo số điểm của người chơi trên màn hình.
	// control both turn. 
	IEnumerator FightTurn()
	{
		if (timeRemaining <= 0)
		{
			animationController.PlayerEffectAttack();
			yield return new WaitForSeconds(2f);
			float fc = (float)Math.Round(currentDamage * 100f) / 100f;
			Debug.Log("Wait Turn");
			enemyHealthManager.TakeDamage(currentDamage);
			float checkEnemyHealth = enemyHealthManager.healthAmount;
			EnemyMessageObject.SetActive(true);
			EnemyMessText.SetText("Enemy got " + fc + " Damages");
			yield return new WaitForSeconds(1f);
			EnemyMessageObject.SetActive(false);
			
			if (checkEnemyHealth <= 0)
			{
				animationController.FireWorkEffect(true);
				yield return new WaitForSeconds(1f);
				//If win, save player data and return back last position with data. 
				Debug.Log("Player win");
				PlayerPrefs.SetInt("IsWin", 1);
				PlayerMessageObject.SetActive(true);
				PlayerMessText.SetText("Player Win");
				CheckName();
				EnemyCount2--;
				PlayerPrefs.SetInt("EnemyCount",EnemyCount2);
				yield return new WaitForSeconds(1f);
				PlayerMessageObject.SetActive(false);
				SwitchScene();
			}
			else
			{
				// perform animation for enemy attach
				animationController.EnemyEffectAttack();
				yield return new WaitForSeconds(2f);
				playerHealthManager.TakeDamage(5);
				float checkPlayerHealth = playerHealthManager.healthAmount;
				yield return new WaitForSeconds(1f);
				if (checkPlayerHealth <= 0)
				{
					animationController.FireWorkEffect(false);
					yield return new WaitForSeconds(1f);
					//If lose, player dead, reset all player data and return back to Starting point
					Debug.Log("Enemy win");
					PlayerPrefs.SetInt("IsWin", 0);
					PlayerPrefs.DeleteKey("p_x");
					PlayerPrefs.DeleteKey("p_y");
					PlayerPrefs.DeleteKey("TimeToLoad");
					PlayerMessageObject.SetActive(true);
					PlayerMessText.SetText("Player Lose");
					yield return new WaitForSeconds(0.5f);
					PlayerMessageObject.SetActive(false);
					SwitchScene();
				}
				else
				{
					PlayerMessageObject.SetActive(true);
					PlayerMessText.SetText("Player got " + 5 + " Damages");
					PlayerPrefs.SetFloat("Health",checkPlayerHealth);
					yield return new WaitForSeconds(1f);
					PlayerMessageObject.SetActive(false);	
				}
			}
			
			

			
		}
	}
	// Đoạn code trên cũng là phần của class được sử dụng để xử lý logic của trò chơi. Các hàm định nghĩa trong class bao gồm:
	// Hàm `Reset()` để reset lại trò chơi về trạng thái ban đầu. Các biến liên quan đến câu hỏi, kết quả nhập liệu, thời gian đều được đặt lại.
	// Hàm `CheckName()` kiểm tra việc kích hoạt các nhân vật của người chơi.
	// Hàm `CompareInput()` được sử dụng để so sánh chuỗi ký tự nhập vào của người chơi với câu hỏi đã định nghĩa. Sau đó, tính toán độ chính xác và damage tương ứng của người chơi - thông qua biến `accuracy` và `currentDamage`.	
	public void Reset()
	{
		sentence = "";
		playerInput = new StringBuilder("");
		size = 0;
		compare = new int[0];
		ShowText();
		timeRemaining = 10;
	}

	public void CheckName()
	{
		if (PlayerPrefs.GetString("Slime1") == "false")
		{
			PlayerPrefs.SetString("Slime1", "true");
		}
		if (PlayerPrefs.GetString("Slime2") == "false")
		{
			PlayerPrefs.SetString("Slime2", "true");
		}
		if (PlayerPrefs.GetString("Slime3") == "false")
		{
			PlayerPrefs.SetString("Slime3", "true");
		}
		if (PlayerPrefs.GetString("Slime4") == "false")
		{
			PlayerPrefs.SetString("Slime4", "true");
		}
		if (PlayerPrefs.GetString("Slime") == "false")
		{
			PlayerPrefs.SetString("Slime", "true");
		}
		if (PlayerPrefs.GetString("Slime5") == "false")
		{
			PlayerPrefs.SetString("Slime5", "true");
		}
		if (PlayerPrefs.GetString("Slime6") == "false")
		{
			PlayerPrefs.SetString("Slime6", "true");
		}
		if (PlayerPrefs.GetString("Slime7") == "false")
		{
			PlayerPrefs.SetString("Slime7", "true");
		}
		if (PlayerPrefs.GetString("Slime8") == "false")
		{
			PlayerPrefs.SetString("Slime8", "true");
		}
		if (PlayerPrefs.GetString("Slime9") == "false")
		{
			PlayerPrefs.SetString("Slime9", "true");
		}
	}

	// Slow show button event.
	IEnumerator ResetButton()
	{
		yield return new WaitForSeconds(7f);
		buttonUI.show();
	}

	private void CompareInput() {
		int min = Math.Min(sentence.Length, playerInput.Length);
		int max = Math.Max(sentence.Length, playerInput.Length);
		compare = new int[max];
		totalChar = max;
		correctChar = 0;
		for (int i = 0; i < min;i++)
		{
			if (sentence[i] == playerInput[i]) {
				compare[i] = 1;
				correctChar++;
			}
			else {
				compare[i] = -1;
			}
		}
		// calculate accuracy based on character
		accuracy = correctChar / totalChar;
		currentDamage = accuracy * 120;
		//currentDamage =  110;
	}
	
	// Hàm `ShowText()` được sử dụng để hiển thị kết quả câu hỏi và kết quả do người dùng nhập vào. Thông qua vòng lặp, tất cả các ký tự sẽ được so sánh với câu hỏi để xác định độ chính xác / không chính xác, từ đó hiển thị màu sắc tương ứng. Hàm sử dụng biến `sb` để lưu và trả về kết quả text hiển thị.
	// Hàm `ShowTime()` sẽ tính toán thời gian còn lại của trò chơi và hiển thị nó trên màn hình thông qua biến `TimerText`. Nếu thời gian đã hết, hàm sẽ hiển thị thời gian là 0.
	// Hàm `SwitchScene()` được sử dụng để chuyển hướng đến màn hình khác. Hàm này được gọi khi trò chơi kết thúc và nếu người chơi thắng hoặc thua, trò chơi sẽ chuyển tới màn hình kết quả tương ứng. Hàm sử dụng `LoadScene()` để chuyển hướng sang màn hình mới, sau đó chờ một thời gian rồi thực hiện việc chuyển hướng.
	private void ShowText()
	{
		int playerInputLength = playerInput.Length;
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < Math.Min(compare.Length,175); i++) {
			if (compare[i] == 1) {
				sb.Append(CORRECT_COLOR_OPEN_TAG);
				sb.Append(sentence[i]);
				sb.Append(COLOR_END_TAG);
			}
			else if (compare[i] == -1) {
				sb.Append(INCORRECT_COLOR_OPEN_TAG);
				sb.Append(sentence[i]);
				sb.Append(COLOR_END_TAG);
			}
			else if (compare[i] == 0) {
				sb.Append(NOT_TYPING_COLOR_TAG);
				sb.Append(sentence[i]);
				sb.Append(COLOR_END_TAG);
			}

			if (i == playerInputLength - 1)
			{
				sb.Append("|");
			}
		}
		text.text = sb.ToString();
	}

	public void ShowTime()
	{
		StringBuilder sb = new StringBuilder();
		if (playing && timeRemaining >= 0)
		{
			timeRemaining -= Time.deltaTime;
			float seconds = Mathf.FloorToInt(timeRemaining % 60);
			sb.Append(COLOR_TIMER_TAG);	
			sb.Append(seconds);
			sb.Append(COLOR_END_TAG);
			TimerText.text = sb.ToString();
			// if (timeRemaining == 0)
			// {
			// 	playing = false;
			// }
		}
		if (timeRemaining <= 0)
		{
			sb = new StringBuilder();
			sb.Append(COLOR_TIMER_TAG);	
			sb.Append("0");
			sb.Append(COLOR_END_TAG);
			TimerText.text = sb.ToString();
		}

	}
	
	public void SwitchScene()
	{
		StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex -1));
        
	}

	IEnumerator LoadScene(int SceneIndex)
	{
		//wait
		yield return new WaitForSeconds(1f);
		//load scene
		SceneManager.LoadScene(SceneIndex);
	}
}
