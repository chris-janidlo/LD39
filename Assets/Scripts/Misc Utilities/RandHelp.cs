using UnityEngine;

public class RandHelp {

	public static T Choose<T> (T[] arr) {
		return arr[Random.Range(0, arr.Length)];
	}

}
