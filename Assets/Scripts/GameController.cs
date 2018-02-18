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
	public GameObject boss;
	public GameObject boss2;
	public int maxEnemies = 5;
	public Text txtScore;
	public Text txtLives;
	public GameObject pnlMenu;
	public Text txtGameResult;
	public int counterDeadEnemies;
	public int wave = 1;

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

	public void FinishGame(){
		
		StopCoroutine ("EnemySpawning");

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
			Destroy (g);
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Shot"))
			Destroy (g);
		Destroy (GameObject.FindGameObjectWithTag ("Player"));
		Destroy (GameObject.FindGameObjectWithTag ("Boss"));

		int lives = System.Int32.Parse (txtLives.text);
		if (lives <= 0)
			txtGameResult.text = "YOU LOST!";
		else
			txtGameResult.text = "YOU WON!";
		pnlMenu.SetActive (true);
	}

	IEnumerator EnemySpawning(){

		if (wave == 1) {
			while (counterEnemies < maxEnemies) {
				Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);

				Instantiate (enemy, targetPosition, enemy.transform.rotation);
				counterEnemies++;

				yield return new WaitForSeconds (Random.Range (1f, 5f));
			}
		}

		if (wave == 2) {
			while (counterEnemies < maxEnemies) {
				Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);

				Instantiate (enemy, targetPosition, enemy.transform.rotation);
				counterEnemies++;

				yield return new WaitForSeconds (Random.Range (1f, 5f));
			}
		}


		if (wave == 3) {
			while (counterEnemies < maxEnemies) {
				Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);

				Instantiate (enemy, targetPosition, enemy.transform.rotation);
				counterEnemies++;

				yield return new WaitForSeconds (Random.Range (1f, 5f));
			}
		}
	}

	void SpawnBoss(){
		if (wave == 1) {
			Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);
			Instantiate (boss, targetPosition, boss.transform.rotation);
		}
		if (wave == 2) {
			Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);
			Instantiate (boss2, targetPosition, boss2.transform.rotation);
		}

		if (wave == 3) {
			Vector2 targetPosition = new Vector2 (Random.Range (-maxWidth, maxWidth), maxHeight);
			Instantiate (boss2, targetPosition, boss2.transform.rotation);
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
		if (counterDeadEnemies >= maxEnemies && GameObject.FindGameObjectWithTag("Boss") == null) {
			SpawnBoss ();
		}
	}

	public void BossDeath(){
		wave++;

		if (wave == 4) {
			FinishGame ();
			return;
		}

		ResetGame ();
		StartCoroutine ("EnemySpawning");
	}

}
