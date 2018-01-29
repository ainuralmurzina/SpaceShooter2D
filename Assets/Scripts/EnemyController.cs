using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawnPoint;
	public GameObject explosion;

	private Rigidbody2D myRigidbody;
	private Vector2 direction;

	void Start(){
		myRigidbody = GetComponent<Rigidbody2D> ();

		StartCoroutine (StartManeuver ());
		StartCoroutine (StartShoot ());

	}

	IEnumerator StartShoot(){
		while (true) {
			Instantiate (shot, shotSpawnPoint.position, Quaternion.identity);
			yield return new WaitForSeconds (Random.Range(0.5f, 2f));
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

}
