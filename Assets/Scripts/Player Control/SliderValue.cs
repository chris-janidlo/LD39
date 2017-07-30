﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

	private Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = SceneManager.manager.TimeScale;
	}
}
