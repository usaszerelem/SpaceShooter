using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is called for the bolt that is fired from the ship
// and also from the Asteroid game object.

public class Mover : MonoBehaviour {
	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		// The lazer bolt should be traveling forward on its 'Z' axis.
		// The 'Z' axis is also called 'transform.forward'.

		rb.velocity = transform.forward * speed;	
	}
}
