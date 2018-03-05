using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New ButtonSoundData", menuName = "Button Sounds")]
public class SoundButtonData : ScriptableObject 
{
	public AudioClip selectedClip;
	public AudioClip deselectedClip;
	public AudioClip clickedClip;
}
