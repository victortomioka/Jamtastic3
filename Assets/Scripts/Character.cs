using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public abstract void TakeHit(float damage);
	
	protected virtual void Die()
	{
		Destroy(this.gameObject);
	}
}
