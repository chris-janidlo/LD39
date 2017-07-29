using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public static SceneManager manager;

	public float SpawnDelay; //how long, in seconds, between each spawn
	public float GameOverTimeScale = 0.1f; //what time scale constitutes a game over
	public readonly float FixedTimeRatio = 0.02f; //whenever we change the time scale, we also change the physics time step to TimeScale * FixedTimeRatio. the default Unity physics step is 0.02 seconds
	public float TimeSinceLastSpawn = 0.0f;
	public float TimeScale = 1.0f;

	//fill the following for the scene:
	public NetworkNode[] Nodes;
	public Spawner[] Spawns;

	void Start () {
		manager = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeScale <= GameOverTimeScale)
			GameOver();
		TimeSinceLastSpawn += Time.deltaTime;
		Time.timeScale = TimeScale;
		Time.fixedDeltaTime = TimeScale * FixedTimeRatio;
		if (TimeSinceLastSpawn >= SpawnDelay) {
			TimeSinceLastSpawn = 0.0f;
			Spawner sp = RandHelp.Choose(Spawns);
			sp.Spawn();
		}
	}

	public void DecreaseTime (float amount) {
		if (TimeScale >= amount)
			TimeScale -= amount;
		else
			TimeScale = GameOverTimeScale;
	}

	public void IncreaseTime(float amount) {
		if (TimeScale < 1)
			TimeScale += amount;
		if (TimeScale >= 1)
			TimeScale = 1;
	}

	private void GameOver () {
		Debug.Log("Game over!");
	}
}
