using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	public string tagTarget;
	public float speed;
	public Vector2 direction;
	public GameObject explosion;

	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.velocity = direction * speed;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == tagTarget) {
			GameObject exp = Instantiate (explosion, other.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			Destroy (this.gameObject);
			Destroy (exp, 2f);
		}
	}
}
