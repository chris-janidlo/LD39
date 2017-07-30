using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour {

	public static MySceneManager manager;

	public EndGameManager EndGameObject;

	public float SpawnDelay; //how long, in seconds, between each spawn
	public float GameOverMargin; //margin of error for when a game over will happen
	public readonly float FixedTimeRatio = 0.02f; //whenever we change the time scale, we also change the physics time step to TimeScale * FixedTimeRatio. the default Unity physics step is 0.02 seconds
	public float TimeSinceLastSpawn = 0.0f;
	public float TimeScale = 1.0f;

	//fill the following for the scene:
	public NetworkNode[] Nodes;
	public Spawner[] Spawns;
	public Vector3 LowerBounds;
	public Vector3 UpperBounds;

	void Start () {
		manager = this;
	}
	
	// Update is called once per frame
	void Update () {
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
		if (TimeScale >= amount + GameOverMargin)
			TimeScale -= amount;
		else
			GameOver();
	}

	public void IncreaseTime(float amount) {
		if (TimeScale < 1)
			TimeScale += amount;
		if (TimeScale > 1)
			TimeScale = 1;
	}

	public Vector3 RandomLocationInsideBounds() {
		float x = Random.Range(LowerBounds.x, UpperBounds.x);
		float y = Random.Range(LowerBounds.y, UpperBounds.y);
		float z = Random.Range(LowerBounds.z, UpperBounds.z);
		return new Vector3(x, y, z);
	}

	private void GameOver () {
		Instantiate(EndGameObject).Initialize(Time.timeSinceLevelLoad);
		UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
	}
}
