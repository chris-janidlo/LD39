using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkNode : MonoBehaviour {

	public int Health;
	public int MaxHealth = 10;
	public float RepairTime = 3.5f; //how much time has passed since last attack on this node for it to repair itself
	public int RepairAmount = 2; //how much this node repairs itself for every RepairTime seconds
	public float TimeHealth = 0.2f; //how much SceneManager's TimeScale is reduced when this dies
	public string Name;
	public bool Dead = false;

	private bool knownDead = false;
	private float timeSinceLastRepairAction; //time since either the node was attacked or the node repaired itself

	// Use this for initialization
	void Start () {
		timeSinceLastRepairAction = 0.0f;
		Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastRepairAction += Time.deltaTime;
		if (Health < MaxHealth && timeSinceLastRepairAction >= RepairTime) {
			Debug.Log(name + " healed for " + RepairAmount);
			timeSinceLastRepairAction = 0.0f;
			if (Health < MaxHealth - RepairAmount)
				Health += RepairAmount;
			else {
				Health = MaxHealth;
				if (Dead) {
					Debug.Log(name + " is back up");
					Dead = false;
					SceneManager.manager.IncreaseTime(TimeHealth);
				}
			}
		}
		if (Health <= 0)
			Dead = true;
		if (Dead && !knownDead) {
			Debug.Log(name + " destroyed!");
			SceneManager.manager.DecreaseTime(TimeHealth);
			knownDead = true;
		}
	}

	public void AttackFor (int damage) {
		timeSinceLastRepairAction = 0.0f; //this goes outside the _if_ so that machines can only repair when all viruses are gone
		if (!Dead) {
			Debug.Log(name + " hurt for " + damage);
			Health -= damage;
		}
	}
}
