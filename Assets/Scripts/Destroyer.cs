using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){

		if (other.tag == "Enemy")
			GameController.Instance.CountDeadEnemies ();

		Destroy (other.gameObject);
	}
}
