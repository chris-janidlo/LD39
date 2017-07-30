using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {

	private Text value;

	// Use this for initialization
	void Start () {
		value = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		value.text = ((int)Time.timeSinceLevelLoad / 60).ToString("D2") + ":" + ((int)Time.timeSinceLevelLoad % 60).ToString("D2");
	}
}
