using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StraightMovement))]
public class Projectile : MonoBehaviour 
{
	public StraightMovement movement;

	private Collider coll;
	
	private void Start() 
	{
		coll = GetComponent<Collider>();
	}

	private void OnTriggerEnter(Collider other) 
	{
		if(!coll.enabled)
			return;

		Destroy(this.gameObject);
	}

	private void Reset() 
	{
		movement = GetComponent<StraightMovement>();
	}
}