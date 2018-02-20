using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	public string tagTarget;
	public string tagTarget2 = "";
	public float speed;
	public Vector2 direction;
	public GameObject explosion;

	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.velocity = direction * speed;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == tagTarget || other.tag == tagTarget2) {
			GameObject exp = Instantiate (explosion, other.transform.position, Quaternion.identity);

			if (other.tag == "Player")
				other.GetComponent<PlayerController> ().OnAttack ();
			else if (other.tag == "Boss") {
				GameController.Instance.AddScore ();
				other.GetComponent<BossController> ().OnAttack ();
			}
			else {
				Debug.Log ("Enemy Killed");
				Destroy (other.gameObject);
				GameController.Instance.AddScore ();
				GameController.Instance.CountDeadEnemies ();
			}

			Destroy (this.gameObject);
			Destroy (exp, 2f);
		}
	}
}
