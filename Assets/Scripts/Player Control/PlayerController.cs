using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float Speed = 5.0f;
	public float Gravity = 10.0f;
	public float[] JumpBursts = { 5.0f, 3.0f, 1.0f }; //each respective value is the nth jump: 0 is single jump, 1 is double jump, etc
	public float AirSpeed = 5.0f; //air strafing speed
	public float HalfHeight = 1.0f; //height from ground to origin
	public float GroundedMargin = 0.1f; //margin of error for ground check
	public float JumpDelay = 0.1f; //how long to wait after jumping before being grounded resets jumpCount (needed because you are still grounded a couple frames after jumping)

	private Rigidbody rb;
	private int jumpCount;
	private float jumpTime;
	private float vertSpeed;

	void Start () {
		rb = GetComponent<Rigidbody>();
		jumpCount = 0;
	}
	
	void Update () {
		if (jumpTime > 0)
			jumpTime -= Time.deltaTime;

		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		moveDir = transform.TransformDirection(moveDir);
		moveDir *= isGrounded() ? Speed : AirSpeed;

		if (isGrounded() && jumpTime <= 0) {
			Debug.Log("hell yeah");
			jumpCount = 0;
		}
		else
			Debug.Log("nope");

		if (Input.GetButtonDown("Jump") && jumpCount < JumpBursts.Length) {
			if (jumpCount == 0)
				jumpTime = JumpDelay;
			Debug.Log(jumpCount);
			vertSpeed = JumpBursts[jumpCount];
			jumpCount++;
		}
		else
			vertSpeed = rb.velocity.y;

		rb.velocity = new Vector3(moveDir.x, vertSpeed, moveDir.z);
	}

	void FixedUpdate() {
		rb.AddForce(Vector3.down * Gravity);
	}

	private bool isGrounded () {
		int layerMask = ~(1 << 8);
		return Physics.Raycast(transform.position, Vector3.down, HalfHeight + GroundedMargin, layerMask);
	}
}
