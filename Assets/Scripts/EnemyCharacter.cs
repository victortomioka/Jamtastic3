using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character 
{
	public float maxHealth;	
    public float Health { get; private set; }

	private void Start() 
	{
		Health = maxHealth;
	}

	public override void TakeHit(float damage)
    {
		Health = Mathf.Clamp(Health - damage, 0, maxHealth);

		if(Health == 0)
		{
			Die();
		}
    }
}
