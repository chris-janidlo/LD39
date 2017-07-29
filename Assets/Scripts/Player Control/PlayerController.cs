using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float Speed = 5.0f;
	public float Gravity = 10.0f;
	public float[] JumpBursts = { 5.0f, 3.0f, 1.0f }; //each respective value is the nth jump: 0 is single jump, 1 is double jump, etc
	public float AirSpeed = 5.0f; //air strafing speed

	private Rigidbody rb;
	private int jumpCount;

	void Start () {
		rb = GetComponent<Rigidbody>();
		jumpCount = 0;
	}
	
	void Update () {
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		moveDir = transform.TransformDirection(moveDir);
		moveDir *= isGrounded() ? Speed : AirSpeed;

		rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
	}

	void FixedUpdate() {
		rb.AddForce(Vector3.down * Gravity);
	}

	private bool isGrounded () {
		return true;
	}
}
