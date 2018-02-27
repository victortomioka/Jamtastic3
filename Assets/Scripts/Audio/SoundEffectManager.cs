using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour 
{
	[Header("Shared Clips")]
	public AudioClip[] clipsMeleeHit;

	public AudioSource Source { get; private set; }

	private void Start() 
	{
		Source = GetComponent<AudioSource>();
	}

	public void Play()
	{
		Source.Play();
	}

	public void Play(AudioClip clip)
	{
		Source.PlayOneShot(clip);
	}

	public void Play(params AudioClip[] clips) 
	{
		Source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
	}
}
