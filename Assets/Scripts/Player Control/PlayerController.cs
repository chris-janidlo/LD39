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

	private Rigidbody rb;
	private int jumpCount;
	private bool willJump = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		jumpCount = 0;
	}
	
	void Update () {
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		moveDir = transform.TransformDirection(moveDir);
		moveDir *= isGrounded() ? Speed : AirSpeed;

		rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

		if (isGrounded())
			jumpCount = 0;

		Debug.Log(jumpCount);

		if (Input.GetButtonDown("Jump") && jumpCount < JumpBursts.Length)
			willJump = true;
	}

	void FixedUpdate() {
		rb.AddForce(Vector3.down * Gravity);
		if (willJump) {
			willJump = false;
			rb.AddForce(JumpBursts[jumpCount] * Vector3.up, ForceMode.VelocityChange);
			jumpCount++;
		}
	}

	private bool isGrounded () {
		return Physics.Raycast(transform.position, Vector3.down, HalfHeight + GroundedMargin);
	}
}
