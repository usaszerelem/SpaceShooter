using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerID;

	void Awake () {
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown (PointerEventData data) {
		//Debug.Log ("OnPointerDown: Touched: " + touched);

		if (touched == false) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
	}

	public void OnDrag (PointerEventData data) {
		if (data.pointerId == pointerID) {
			//Debug.Log ("Inside OnDrag");

			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		} else {
			Debug.Log ("Outside OnDrag");
		}
	}

	public void OnPointerUp (PointerEventData data) {
		//Debug.Log ("Inside OnPointerUp");

		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public Vector2 GetDirection () {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
