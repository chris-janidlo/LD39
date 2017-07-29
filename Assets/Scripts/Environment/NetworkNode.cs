using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkNode : MonoBehaviour {

	public int Health = 10;
	public string Name;
	public bool Dead = false;

	private bool toldIsDead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0)
			Dead = true;
		if (Dead && !toldIsDead) {
			Debug.Log(name + " destroyed!");
			toldIsDead = true;
		}
	}

	public void AttackFor (int damage) {
		if (!Dead) {
			Debug.Log(name + " hurt for " + damage);
			Health -= damage;
		}
	}
}
