using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController_Adv : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform[] shotSpawnPoint;
	public GameObject explosion;
	public int maxLives = 3;

	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	private float maxWidth;
	private bool posFixed;
	private int counterLives;

	void Start(){
		posFixed = false;
		counterLives = maxLives;

		myRigidbody = GetComponent<Rigidbody2D> ();

		GetScreenMaxWidth ();
		SetPosition ();
	}

	void GetScreenMaxWidth(){
		Vector3 currentScreen = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
		maxWidth = currentScreen.x - 1;
	}

	void SetPosition(){
		direction = new Vector2 (0f, -1f);
		myRigidbody.velocity = direction * speed;
	}

	void FixedUpdate(){

		if (transform.position.y <= 3 && !posFixed) {
			posFixed = true;
			StartCoroutine (StartManeuver ());
			StartCoroutine (StartShoot ());
		}

		transform.position = new Vector3(
			Mathf.Clamp (transform.position.x, -maxWidth, maxWidth),
			transform.position.y, 
			0f);
	}

	IEnumerator StartShoot(){
		while (true) {
			for (int i = 0; i < shotSpawnPoint.Length; i++) {
				Instantiate (shot, shotSpawnPoint[i].position, Quaternion.identity);
			}
			yield return new WaitForSeconds (Random.Range(1f, 3f));
		}
	}

	IEnumerator StartManeuver(){
		myRigidbody.velocity = Vector2.zero;
		direction = new Vector2 (0f, 0f);
		while (true) {
			yield return new WaitForSeconds (Random.Range(0.5f, 2f));
			direction.x = Random.Range (-1f, 1f);
			myRigidbody.velocity = direction * speed;
		}
	}

	void Explode(GameObject target){
		GameObject exp = Instantiate (explosion, target.transform.position, Quaternion.identity);
		Destroy (target);
		Destroy (exp, 2f);
	}

	public void OnAttack(){
		counterLives--;
		if (counterLives == 0) {
			Explode (this.gameObject);
			GameController.Instance.BossDeath ();
		}
	}
}
