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
