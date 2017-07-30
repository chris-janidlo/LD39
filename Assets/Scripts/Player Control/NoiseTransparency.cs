using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTransparency : MonoBehaviour {

	private CanvasGroup canv;

	// Use this for initialization
	void Start () {
		canv = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		canv.alpha = 1.0f - (Time.timeScale + .5f);
	}
}
