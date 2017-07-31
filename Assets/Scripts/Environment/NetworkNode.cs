using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkNode : MonoBehaviour {

	public int MaxHealth = 10;
	public int Health;
	public float RepairTime = 3.5f; //how much time has passed since last attack on this node for it to repair itself
	public int RepairAmount = 2; //how much this node repairs itself for every RepairTime seconds
	public float TimeHealth = 0.2f; //how much SceneManager's TimeScale is reduced when this dies
	public string Name;
	public bool Dead = false;
	public Material HealthyMat;
	public Material DeadMat;
	public AudioClip DeathSound;
	public AudioClip AliveAgainSound;

	private bool knownDead = false;
	private float timeSinceLastRepairAction; //time since either the node was attacked or the node repaired itself
	private Slider healthBar;
	private Renderer rend;

	void Start () {
		timeSinceLastRepairAction = 0.0f;
		Health = MaxHealth;
		healthBar = GetComponentInChildren<Slider>();
		rend = GetComponent<Renderer>();
	}
	
	void Update () {
		timeSinceLastRepairAction += Time.deltaTime;
		if (Health < MaxHealth && timeSinceLastRepairAction >= RepairTime) {
			timeSinceLastRepairAction = 0.0f;
			if (Health < MaxHealth - RepairAmount)
				Health += RepairAmount;
			else {
				Health = MaxHealth;
				if (Dead) {
					PlayerController.aus.PlayOneShot(AliveAgainSound);
					rend.material = HealthyMat;
					Dead = false;
					knownDead = false;
					MySceneManager.manager.IncreaseTime(TimeHealth);
				}
			}
		}
		if (Health <= 0)
			Dead = true;
		if (Dead && !knownDead) {
			PlayerController.aus.PlayOneShot(DeathSound, .4f * Mathf.Pow(2.0f, 2.6f * (1.0f - Time.timeScale)) + 1.6f);
			rend.material = DeadMat;
			MySceneManager.manager.DecreaseTime(TimeHealth);
			knownDead = true;
		}
		healthBar.value = (float) Health / MaxHealth;
	}

	public void AttackFor (int damage) {
		timeSinceLastRepairAction = 0.0f; //this goes outside the _if_ so that machines can only repair when all viruses are gone
		if (!Dead) {
			Debug.Log(name + " hurt for " + damage);
			Health -= damage;
		}
	}
}
