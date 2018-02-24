using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public PlayerMovement movement;
	public Gun gun;
    public DashMovement dashMovement;
    public LookAtCursor lookAtCursor;
    
    public float meleeDamage;
    public float knockBackForce;

    private bool attacking;    
    private int groundLayer;

    private Animator anim;

    private void Start() 
    {
        groundLayer = LayerMask.NameToLayer("Ground");

        anim = GetComponentInChildren<Animator>();
    }

    private void Reset() 
	{
		movement = GetComponent<PlayerMovement>();
		gun = GetComponentInChildren<Gun>();
        dashMovement = GetComponent<DashMovement>();
        lookAtCursor = GetComponent<LookAtCursor>();
    }

	void FixedUpdate() 
	{
		float axisHorizontal = Input.GetAxisRaw("Horizontal");
		float axisVertical = Input.GetAxisRaw("Vertical");

		if(IsEnabled(movement))
			movement.Move(axisHorizontal, axisVertical);
	}

	private void Update() 
	{
        if (Input.GetButton("Shoot") && IsEnabled(gun))
			gun.Shoot();

        if (Input.GetButtonDown("Dash") && IsEnabled(dashMovement))
            dashMovement.Dash(transform.forward);

        if(Input.GetButtonDown("Melee") && !attacking && !dashMovement.IsDashing)
            AttackStart();

        if(Input.GetButtonDown("Cancel"))
		{
			if(GameManager.IsPaused) 
				GameManager.Instance.Resume();
			else
				GameManager.Instance.Pause();
		}
	}

    public void AttackStart()
    {
        anim.SetTrigger("attack");
        attacking = true;

        movement.enabled = false;
        gun.enabled = false;
    }

    public void AttackEnd()
    {
        attacking = false;

        movement.enabled = true;
        gun.enabled = true;
    }

    private void DashStarted()
    {
        movement.enabled = false;
        gun.enabled = false;
        lookAtCursor.enabled = false;
    }

    private void DashEnded()
    {
        movement.enabled = true;
        gun.enabled = true;
        lookAtCursor.enabled = true;
    }

    public void SetInputEnabled(bool enable)
    {
        lookAtCursor.enabled = enable;
        enabled = enable;
    }

	private bool IsEnabled(MonoBehaviour component)
	{
		return component != null && component.enabled;
	}

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.layer == groundLayer)
            return;

        if (dashMovement.IsDashing)
            dashMovement.Interrupt();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyCharacter enemy = other.gameObject.GetComponent<EnemyCharacter>();

            if(enemy != null)
            {
                enemy.KnockBack(knockBackForce, -enemy.transform.forward);

                IDamageable dmg = enemy.gameObject.GetComponent<IDamageable>();
                if(dmg != null)
                    dmg.TakeHit(meleeDamage);
            }
        }
    }
}
