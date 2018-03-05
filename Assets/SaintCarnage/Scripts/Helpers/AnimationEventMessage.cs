using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventMessage : MonoBehaviour 
{
	public void SendBroadcast(string eventName)
	{
		// Debug.Log("Chamando Evento: " + eventName);
		BroadcastMessage(eventName, null, SendMessageOptions.DontRequireReceiver);
	}

	public void SendUpwards(string eventName)
	{
		// Debug.Log("Chamando Evento: " + eventName);
		SendMessageUpwards(eventName, null, SendMessageOptions.DontRequireReceiver);
	}
}