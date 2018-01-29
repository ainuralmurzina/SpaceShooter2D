using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	public float speed;
	public Vector2 direction;
	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.velocity = direction * speed;
	}
}
