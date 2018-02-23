using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventMessage : MonoBehaviour 
{
	public void SendBroadcast(string eventName)
	{
		BroadcastMessage(eventName, null, SendMessageOptions.DontRequireReceiver);
	}

	public void SendUpwards(string eventName)
	{
		SendMessageUpwards(eventName, null, SendMessageOptions.DontRequireReceiver);
	}
}