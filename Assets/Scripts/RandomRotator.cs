using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	

		// Angular velocity is how fast the rigid body rotates.
		// InsideUnitSphere gives a random Vector3 value.

		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
