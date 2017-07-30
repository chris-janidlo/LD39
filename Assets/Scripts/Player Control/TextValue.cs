using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextValue : MonoBehaviour {

	private Text value;

	// Use this for initialization
	void Start () {
		value = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		value.text = (MySceneManager.manager.TimeScale * 100).ToString("F0") + " %";
	}
}
