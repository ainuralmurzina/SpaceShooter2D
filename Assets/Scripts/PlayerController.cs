using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawnPoint;
	public GameObject explosion;

	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	private float maxWidth;

	void Start(){

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
			GameObject expEnemy = Instantiate (explosion, other.transform.position, Quaternion.identity);
			GameObject expPlayer = Instantiate (explosion, transform.position, Quaternion.identity);

			Destroy (other.gameObject);
			Destroy (this.gameObject);

			Destroy (expEnemy, 2f);
			Destroy (expPlayer, 2f);
		}
	}

}
