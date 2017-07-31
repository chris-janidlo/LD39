using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : VirusController {

	public Vector2 WarpTimeRange; //x is lower bound of how long it waits to warp; y is upper bound
	public Vector2 CryTimeRange;
	public AudioClip[] Cries;

	private float waitTime = 0.0f;
	private float warpTime = 0.0f;
	private float cryTime = 0.0f;
	private float currWarp; //the time we're currently waiting for
	private float currCry;
	private bool attached = false;

	void Update() {
		transform.LookAt(PlayerController.Location);
		waitTime += Time.deltaTime;
		warpTime += Time.deltaTime;
		if (!attached) {
			if (waitTime >= AttackDelay) {
				transform.parent = PlayerController.Location;
				transform.localPosition = Random.onUnitSphere * AttackDistance;
				attached = true;
			}
			if (warpTime >= currWarp) {
				transform.position = MySceneManager.manager.RandomLocationInsideBounds();
				currWarp = Random.Range(WarpTimeRange.x, WarpTimeRange.y);
				warpTime = 0.0f;
			}
		}
		else {
			cryTime += Time.deltaTime;
			if (cryTime >= currCry) {
				PlayerController.aus.PlayOneShot(RandHelp.Choose(Cries), 1.5f);
				currCry = Random.Range(CryTimeRange.x, CryTimeRange.y);
				cryTime = 0.0f;
			}
		}
	}

	public override void Initialize(Vector3 pos) {
		MySceneManager.manager.DecreaseTime(SpeedDamage);
		transform.position = pos;
		currWarp = Random.Range(WarpTimeRange.x, WarpTimeRange.y);
		currCry = Random.Range(CryTimeRange.x, CryTimeRange.y);
	}
}
