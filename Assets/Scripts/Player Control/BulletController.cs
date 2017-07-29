using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float Speed = 50.0f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize(Vector3 direction) {
		rb = GetComponent<Rigidbody>();
		rb.velocity = direction * Speed;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Enemy")
			Destroy(collision.gameObject);
		Destroy(gameObject);
	}
}
