using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroy by time is currently used for explosions. It is assumed that
// an explosion will not last more than a few seconds, this script is attached
// and triggered when the explosion object is initialized. The lifetime is
// set to two seconds from within the Game Controller.
public class DestroyByTime : MonoBehaviour {

	public float lifetime;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifetime);
	}
}
