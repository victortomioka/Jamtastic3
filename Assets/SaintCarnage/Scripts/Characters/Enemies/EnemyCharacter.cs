using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character 
{
    public float maxHealth;	
    public float Health { get; private set; }

    protected Rigidbody rb;

	protected virtual void Start()
	{
		Health = maxHealth;

        rb = GetComponent<Rigidbody>();
    }

	public override void TakeHit(float damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, maxHealth);

        if (Health == 0)
		{
			Die();
		}
    }

    public void KnockBack(float force, Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
