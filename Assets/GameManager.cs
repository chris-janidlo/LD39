using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public float SpawnDelay; //how long, in seconds, each spawn happens
	public float TimeScaleLoss; //how much TimeScale is reduced after each spawn
	public float TimeSinceLastSpawn = 0.0f;
	//fill the following for the scene:
	public NetworkNode[] Nodes;
	public Spawner[] Spawns;

	// Use this for initialization
	void Start () {
		manager = this;
	}
	
	// Update is called once per frame
	void Update () {
		TimeSinceLastSpawn += Time.deltaTime;
		if (TimeSinceLastSpawn >= SpawnDelay) {
			DecreaseTime();
			TimeSinceLastSpawn = 0.0f;
			//todo: function for this
			NetworkNode nn = Nodes[UnityEngine.Random.Range(0, Nodes.Length)];
			Spawner sp = Spawns[UnityEngine.Random.Range(0, Spawns.Length)];

			VirusController vc = ((GameObject) Instantiate(Resources.Load("Virus"))).GetComponent<VirusController>();
			vc.Initialize(sp, nn);
		}
	}

	public void DecreaseTime () {
		if (Time.timeScale > TimeScaleLoss)
			Time.timeScale -= TimeScaleLoss;
	}

	public void IncreaseTime() {
		if (Time.timeScale < 1)
			Time.timeScale += TimeScaleLoss;
		if (Time.timeScale >= 1)
			Time.timeScale = 1;
	}
}
