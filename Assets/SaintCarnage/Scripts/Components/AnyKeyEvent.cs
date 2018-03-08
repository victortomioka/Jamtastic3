using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnyKeyEvent : MonoBehaviour {

	public UnityEvent onPressAnyKey;
	
	void Update () 
	{
		if(Input.anyKeyDown && onPressAnyKey != null)
			onPressAnyKey.Invoke();
	}
}
