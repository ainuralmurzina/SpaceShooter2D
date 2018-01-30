using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawnPoint;
	public GameObject explosion;
	public int maxLives = 3;

	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	private float maxWidth;
	private int counterLives;

	void Start(){

		counterLives = maxLives;
		GetScreenMaxWidth ();

		direction = Vector2.zero;
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void GetScreenMaxWidth(){
		Vector3 currentScreen = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
		maxWidth = currentScreen.x - 1;
	}

	void FixedUpdate (){
		Move ();

		if (Input.GetKeyDown (KeyCode.Space))
			Shoot ();
	}

	void Move(){
		direction.x = Input.GetAxis ("Horizontal");
		myRigidbody.velocity = direction * speed;

		transform.position = new Vector3(
			Mathf.Clamp (transform.position.x, -maxWidth, maxWidth),
			transform.position.y, 
			0f);
	}

	void Shoot() {
		Instantiate (shot, shotSpawnPoint.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			counterLives = 0;
			GameController.Instance.UpdateLives (counterLives);

			Explode (other.gameObject);
			Explode (this.gameObject);
		}
	}

	void Explode(GameObject target){
		GameObject exp = Instantiate (explosion, target.transform.position, Quaternion.identity);
		Destroy (target);
		Destroy (exp, 2f);
	}

	public void OnAttack(){
		counterLives--;
		GameController.Instance.UpdateLives (counterLives);
		if (counterLives == 0) {
			Explode (this.gameObject);
		}
	}

}
