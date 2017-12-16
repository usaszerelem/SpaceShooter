using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float Speed;
	public float SideTilt;
	public Boundary boundary;

	public GameObject shot;

	// This public property could be declared as a GameObject, but then
	// the position would need to be referenced as shotSpawn.transform.position
	// By declaring this a Transform, Unity is smart to know that it is 
	// the transform withing the GameObject that we are interesting in.
	public Transform shotSpawn;

	public float fireRate;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	private float nextFire;
	private Quaternion calibrationQuaternion;
	private Rigidbody rb;
	private AudioSource audioSource;
	private bool IsMobile;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		Debug.Log ("App Platform: " + Application.platform );

		if (Application.platform == RuntimePlatform.Android ||
		    Application.platform == RuntimePlatform.IPhonePlayer ||
			Application.platform == RuntimePlatform.OSXEditor) {

			Debug.Log ("Mobile platform detected");
			IsMobile = true;
		
			// Ideally this should be done by a button where
			// we ask the user to sit back to a comfortable
			// position and calibrate the device.
			CalibrateAcceloremeter ();
		} else {
			IsMobile = false;
		}
	}

	// Update is called once per frame. Note that in mobile mode,
	// unity translates every touch on the mobilescreen as a
	// left mouse button down event. That is why we can fire
	// bolts by clicking on the screen.

	void Update () {

		bool CanFire = false;

		if (IsMobile == true) {
			CanFire = areaButton.CanFire ();
		} else {
			// We check for the default firing button. This is as an example
			// the left mouse down.
			CanFire = Input.GetButton ("Fire1");
		}

		if (CanFire == true && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			audioSource.Play();
		}
	}

	// Used to calibrate the Input.acceleation input. We are resolving
	// the problem that the device can be held comfortably instead of
	// perfectly horizontally.

	void CalibrateAcceloremeter()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	// Get the 'calibrated' value from the Input. This is multiplied
	// by the acceleration value to provide faster movement to the
	// player ship.
	// This is called for every frame to get us a fixed acceleration
	// as an offset of the original position.

	Vector3 FixAcceleration(Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return(fixedAcceleration);
	}

	void FixedUpdate()
	{
		Vector3 movement;

		if (IsMobile == true) {
			Vector2 direction = touchPad.GetDirection ();
			movement = new Vector3 (direction.x, 0.0f, direction.y);

			//Vector3 acceleration = FixAcceleration (Input.acceleration);
			//movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		} else {
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		}

		//Vector2 direction = touchPad.GetDirection ();
		//Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		rb.velocity = movement * Speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -SideTilt);
	}
}
