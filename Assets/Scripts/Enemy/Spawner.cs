using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public string[] PossibleSpawnNames; //edit this to give the spawner the list of resource names it must be aware of
	public VirusController[] PossibleSpawns; //array of possible viruses that is randomly chosen from when asked to spawn

	void Start () {
		PossibleSpawns = new VirusController[PossibleSpawnNames.Length];
		for (int i = 0; i < PossibleSpawnNames.Length; i++)
			PossibleSpawns[i] = ((GameObject) Resources.Load(PossibleSpawnNames[i])).GetComponent<VirusController>();
	}
	
	void Update () {
		
	}

	public void Spawn () {
		Instantiate(RandHelp.Choose(PossibleSpawns)).Initialize(this);
	}
}
