using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTransparency : MonoBehaviour {

	private CanvasGroup canv;

	// Use this for initialization
	void Start() {
		canv = GetComponent<CanvasGroup>();
	}

	// Update is called once per frame
	void Update() {
		canv.alpha = Mathf.Clamp(.015f * Mathf.Pow(2.0f, 2.6f * (1.0f - Time.timeScale) + 3.7f) - .6f, 0, 1);
	}
}