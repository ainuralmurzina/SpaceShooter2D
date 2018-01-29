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
	public int maxEnemies = 5;
	public Text txtScore;

	private int counterEnemies;
	private float maxWidth, maxHeight;

	void Start () {
		GetScreenMaxParameters ();
		StartGame ();
	}

	void GetScreenMaxParameters(){
		Vector3 currentScreen = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
		maxWidth = currentScreen.x - 1;
		maxHeight = currentScreen.y + 1;
	}

	void StartGame(){
		counterEnemies = 0;
		StartCoroutine (EnemySpawning());
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

}
