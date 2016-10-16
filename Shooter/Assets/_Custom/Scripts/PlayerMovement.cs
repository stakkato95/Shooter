using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float cameraRayLength = 100;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask ("Floor");
	}

	void FixedUpdate()
	{
		var horizontaMove = Input.GetAxisRaw ("Horizontal");
		var verticalMove = Input.GetAxisRaw ("Vertical");

		MovePlayer (horizontaMove, verticalMove);

		Turn ();
		AnimateMovement (horizontaMove, verticalMove);
	}

	void MovePlayer(float h, float v)
	{
		movement.Set (h, 0, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turn()
	{
		var cameraRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (cameraRay, out floorHit, cameraRayLength, floorMask)) {
			var playerToMouse = floorHit.point - transform.position;

			Debug.DrawRay (cameraRay.origin, floorHit.point);

			var rotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (rotation);
		}
	}

	void AnimateMovement(float horizontaMove, float verticalMove)
	{
		var isWalking = horizontaMove != 0 || verticalMove != 0;

		anim.SetBool ("IsWalking", isWalking); 
	}
}