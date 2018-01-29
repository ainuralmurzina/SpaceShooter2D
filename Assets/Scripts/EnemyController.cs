using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawnPoint;

	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	private float maxWidth;

	void Start(){
		myRigidbody = GetComponent<Rigidbody2D> ();

		GetScreenMaxWidth ();

		StartCoroutine (StartManeuver ());
		StartCoroutine (StartShoot ());

	}

	void GetScreenMaxWidth(){
		Vector3 currentScreen = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
		maxWidth = currentScreen.x - 1;
	}

	void FixedUpdate(){
		transform.position = new Vector3(
			Mathf.Clamp (transform.position.x, -maxWidth, maxWidth),
			transform.position.y, 
			0f);
	}

	IEnumerator StartShoot(){
		while (true) {
			Instantiate (shot, shotSpawnPoint.position, Quaternion.identity);
			yield return new WaitForSeconds (Random.Range(1f, 3f));
		}
	}

	IEnumerator StartManeuver(){
		direction = new Vector2 (0f, -1f);
		myRigidbody.velocity = direction * speed;

		while (true) {
			yield return new WaitForSeconds (Random.Range(0.5f, 2f));
			direction.x = Random.Range (-1f, 1f);
			myRigidbody.velocity = direction * speed;
		}
	}

	void OnDestroy(){
		GameController.Instance.AddScore ();
	}

}
