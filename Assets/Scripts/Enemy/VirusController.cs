using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour {

	public float Speed = 5.0f;
	public int Damage = 1;
	public float AttackDistance = 3.0f; //the distance from a node where this virus will stop moving
	public float AttackDelay = 2.0f; //how long in seconds between each attack on a node

	private NetworkNode target;
	private float timeSinceLastAttack = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

	public void Initialize (Spawner spawnPoint, NetworkNode attackPoint) {
		transform.position = spawnPoint.transform.position;
		transform.LookAt(attackPoint.transform.position);
		target = attackPoint;
	}
	
	// Update is called once per frame
	void Update () {
		if ((target.transform.position - transform.position).magnitude >= AttackDistance) {
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		} else {
			timeSinceLastAttack += Time.deltaTime;
			if (timeSinceLastAttack >= AttackDelay) {
				timeSinceLastAttack = 0.0f;
				target.AttackFor(Damage);
			}
		}
	}
}
