using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float Speed = 50.0f;
	public int Damage = 1;

	private Rigidbody rb;

	void Start () {
		
	}

	void Update () {
		
	}

	public void Initialize(Vector3 direction) {
		rb = GetComponent<Rigidbody>();
		rb.velocity = direction * Speed;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Enemy")
			collision.gameObject.GetComponent<VirusController>().Damage(Damage);
		Destroy(gameObject);
	}
}
