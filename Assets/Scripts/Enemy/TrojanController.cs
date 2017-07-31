using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanController : VirusController {

	public float ExplodeTime; //the time this travels for before it release its payload
	public float MaxExplodeDistance; //how far away the payload could potentially be
	public string[] PayloadNames; //edit this to edit the payload
	public VirusController[] Payload; //how many, and what kind, of virus are released
	public AudioClip[] ExplodeSounds;

	private Rigidbody rb;
	private float life = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		life += Time.deltaTime;
		if (life >= ExplodeTime)
			Explode();
	}

	public override void Initialize(Vector3 pos) {
		Payload = new VirusController[PayloadNames.Length];
		for (int i = 0; i < PayloadNames.Length; i++)
			Payload[i] = ((GameObject) Resources.Load(PayloadNames[i])).GetComponent<VirusController>();

		transform.position = pos;
		rb = GetComponent<Rigidbody>();
		rb.velocity = findDirection() * Speed;
		MySceneManager.manager.DecreaseTime(SpeedDamage);
	}

	//find a direction to go that has no immediate collisions
	//terribly unoptimised
	Vector3 findDirection () {
		float distance = Speed * ExplodeTime;
		Vector3 direction = Random.onUnitSphere;
		while (Physics.Raycast(transform.position, direction, distance))
			direction = Random.onUnitSphere;
		return direction;
	}

	void Explode () {
		PlayerController.aus.PlayOneShot(RandHelp.Choose(ExplodeSounds));
		MySceneManager.manager.IncreaseTime(SpeedDamage);
		foreach (VirusController virus in Payload)
			Instantiate(virus).Initialize(transform.position + Random.insideUnitSphere * MaxExplodeDistance);
		Destroy(gameObject);
	}
}
