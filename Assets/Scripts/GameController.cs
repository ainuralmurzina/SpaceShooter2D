using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController Instance {
		get;
		private set;
	}

	void Awake(){
		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}

	//==============================================

	public GameObject enemy;
	public GameObject player;
	public int maxEnemies = 5;
	public Text txtScore;
	public Text txtLives;
	public GameObject pnlMenu;
	public Text txtGameResult;
	public int counterDeadEnemies;

	private int counterEnemies;
	private float maxWidth, maxHeight;

	void Start () {
		GetScreenMaxParameters ();
	}

	void GetScreenMaxParameters(){
		Vector3 currentScreen = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
		maxWidth = currentScreen.x - 1;
		maxHeight = currentScreen.y + 1;
	}

	public void StartGame(){

		ResetGame ();

		pnlMenu.SetActive (false);

		//create player
		Vector2 targetPosition = new Vector2 (0f, -maxHeight + 2);
		Instantiate (player, targetPosition, Quaternion.identity);

		StartCoroutine ("EnemySpawning");
	}

	void FinishGame(){
		
		StopCoroutine ("EnemySpawning");

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
			Destroy (g);
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Shot"))
			Destroy (g);
		Destroy (GameObject.FindGameObjectWithTag ("Player"));

		int lives = System.Int32.Parse (txtLives.text);
		if (lives <= 0)
			txtGameResult.text = "YOU LOST!";
		else
			txtGameResult.text = "YOU WON!";
		pnlMenu.SetActive (true);
	}

	IEnumerator EnemySpawning(){

		while (counterEnemies < maxEnemies) {
			Vector2 targetPosition = new Vector2 (Random.Range(-maxWidth, maxWidth), maxHeight);

			Instantiate (enemy, targetPosition, enemy.transform.rotation);
			counterEnemies++;

			yield return new WaitForSeconds (Random.Range (1f, 5f));
		}
		
	}

	public void AddScore(){
		int score = System.Int32.Parse (txtScore.text);
		score += 100;
		txtScore.text = score.ToString ();
	}

	public void UpdateLives(int leftLives){
		txtLives.text = leftLives.ToString ();
		if (leftLives <= 0)
			FinishGame ();
	}

	void ResetGame(){
		counterDeadEnemies = 0;

		counterEnemies = 0;
		txtScore.text = counterEnemies.ToString ();

		txtLives.text = player.GetComponent<PlayerController> ().maxLives.ToString ();

		txtGameResult.text = "";
	}

	public void CountDeadEnemies(){
		counterDeadEnemies++;
		if (counterDeadEnemies >= maxEnemies)
			FinishGame ();
	}
}
