using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//managing custom exceptions for debugging purposes
public class ExceptionManager {

	public static void Throw (Exception e) {
		Debug.Log(StackTraceUtility.ExtractStackTrace());
		throw e;
	}
	
}