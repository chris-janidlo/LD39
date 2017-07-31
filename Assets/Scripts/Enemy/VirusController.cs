using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour {

	public float Speed = 5.0f;
	public int Health = 1;
	public int NodeDamage = 1;
	public float SpeedDamage = 0.1f; //how much TimeScale is reduced when this virus spawns
	public float AttackDistance = 3.0f; //the distance from a node where this virus will stop moving
	public float AttackDelay = 2.0f; //how long in seconds between each attack on a node
	public float RotationAngle = 5.0f;
	public Vector3[] RotationAxes; //possible axes that this virus can rotate around
	public float[] RotationTimes; //potential times between choosing an axis
	public AudioClip DeathSound;

	private NetworkNode target;
	private float timeSinceLastAttack = 0.0f;
	private float rotationTime = 0.0f;
	private Vector3 rotationAxis;
	
	void Start () {

	}

	public virtual void Initialize (Vector3 pos) {
		target = RandHelp.Choose(MySceneManager.manager.Nodes);

		transform.position = pos;
		transform.LookAt(target.transform.position);
		MySceneManager.manager.DecreaseTime(SpeedDamage);
	}
	
	void Update () {
		if ((target.transform.position - transform.position).magnitude >= AttackDistance) {
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		} else {
			rotateAroundNode();
			timeSinceLastAttack += Time.deltaTime;
			if (timeSinceLastAttack >= AttackDelay) {
				timeSinceLastAttack = 0.0f;
				target.AttackFor(NodeDamage);
			}
		}
	}

	private void rotateAroundNode () {
		rotateAroundPos(target.transform.position);
	}

	protected void rotateAroundPos (Vector3 pos) {
		if (rotationTime <= 0) {
			rotationAxis = RandHelp.Choose(RotationAxes);
			rotationTime = RandHelp.Choose(RotationTimes);
		}
		rotationTime -= Time.deltaTime;
		transform.RotateAround(pos, rotationAxis, RotationAngle * Time.deltaTime);
	}

	public void Damage (int damage) {
		Health -= damage;
		if (Health <= 0) {
			PlayerController.aus.PlayOneShot(DeathSound, .7f);
			MySceneManager.manager.IncreaseTime(SpeedDamage);
			Destroy(gameObject);
		}
	}
}
