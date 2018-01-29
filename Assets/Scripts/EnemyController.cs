using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawnPoint;

	void Start(){
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot(){
		while (true) {
			Instantiate (shot, shotSpawnPoint.position, Quaternion.identity);
			yield return new WaitForSeconds (Random.Range(0.5f, 2f));
		}
	}
}
