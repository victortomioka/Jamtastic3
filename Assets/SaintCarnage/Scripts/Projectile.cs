using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StraightMovement))]
public class Projectile : MonoBehaviour 
{
	public float damage;

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

		IDamageable damageable = other.GetComponent<IDamageable>();

		if(damageable != null)
			damageable.TakeHit(damage);


		Destroy(this.gameObject);
	}

	private void Reset() 
	{
		movement = GetComponent<StraightMovement>();
	}
}