using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5;

	Vector3 offset;

	void Awake () {
		offset = transform.position - target.position;
	}
	
	void FixedUpdate () {
		var cameraTargetPosition = offset + target.position;
		transform.position = Vector3.Lerp (transform.position, cameraTargetPosition , smoothing * Time.deltaTime);
	}
}
