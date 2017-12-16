using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is called for the Asteroid object when a enter
// trigger is detected.

public class DestroyByContact : MonoBehaviour {
	
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' object.");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
			return;

		// We always instantiate an explosion for the Asteroid, but we do
		// not instantiate one for the enemy bolt.

		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}

		// If the other object is the player, then explode the player also.
		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore(scoreValue);

		// --------------------------------------------------------
		// Note that Destroy() marks the object to be destroyed and
		// this might not be immediatelly.
		// --------------------------------------------------------

		// Destroys the object entering the mesh zone
		Destroy(other.gameObject);

		// Destroys the object itself.
		Destroy(gameObject);
	}
}
