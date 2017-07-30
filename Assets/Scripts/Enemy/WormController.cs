using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : VirusController {

	public Vector2 WarpTimeRange; //x is lower bound of how long it waits to warp; y is upper bound

	private float waitTime = 0.0f;
	private float warpTime = 0.0f;
	private float currWarp; //the time we're currently waiting for
	private bool attached = false;

	void Update() {
		transform.LookAt(PlayerController.Location);
		waitTime += Time.deltaTime;
		warpTime += Time.deltaTime;
		if (!attached && waitTime >= AttackDelay) {
			transform.parent = PlayerController.Location;
			transform.localPosition = Random.onUnitSphere * AttackDistance;
			attached = true;
		}
		if (!attached && warpTime >= currWarp) {
			transform.position = MySceneManager.manager.RandomLocationInsideBounds();
			currWarp = Random.Range(WarpTimeRange.x, WarpTimeRange.y);
			warpTime = 0.0f;
		}
	}

	public override void Initialize(Vector3 pos) {
		MySceneManager.manager.DecreaseTime(SpeedDamage);
		transform.position = pos;
		currWarp = Random.Range(WarpTimeRange.x, WarpTimeRange.y);
	}
}
