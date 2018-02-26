using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponSlot
{
    public Gun gun;
    public GameObject physicalGun;

    public void SetActive(bool active)
    {
        gun.gameObject.SetActive(active);
        physicalGun.SetActive(active);
    }
}

public class PlayerController : MonoBehaviour 
{
	public PlayerMovement movement;
    public WeaponSlot primaryWeapon;
    public WeaponSlot secondaryWeapon;

    public DashMovement dashMovement;
    public LookAtCursor lookAtCursor;
    
    public float meleeDamage;
    public float knockBackForce;

    [HideInInspector] public bool primaryWeaponAvailable;
    [HideInInspector] public bool secondaryWeaponAvailable;
    public bool HasWeaponAvailable { get { return primaryWeaponAvailable || secondaryWeaponAvailable; } }
    public bool BothWeaponAvailable { get { return primaryWeaponAvailable && secondaryWeaponAvailable; } }

    private bool attacking;
    private int groundLayer;
    private Weapon.WeaponCategory selectedWeapon;
    

    private Animator anim;

    private void Start() 
    {
        groundLayer = LayerMask.NameToLayer("Ground");

        anim = GetComponentInChildren<Animator>();

        primaryWeapon.SetActive(false);
        secondaryWeapon.SetActive(false);
        
        // SetSelectedWeapon(Weapon.WeaponCategory.PrimaryWeapon);

        GameManager.Instance.SetWeaponsUI(Weapon.WeaponCategory.PrimaryWeapon, primaryWeaponAvailable, secondaryWeaponAvailable);
    }

    private void Reset() 
	{
		movement = GetComponent<PlayerMovement>();
        dashMovement = GetComponent<DashMovement>();
        lookAtCursor = GetComponent<LookAtCursor>();
    }

	void FixedUpdate() 
	{
		float axisHorizontal = Input.GetAxisRaw("Horizontal");
		float axisVertical = Input.GetAxisRaw("Vertical");

		if(IsEnabled(movement))
        {
			movement.Move(axisHorizontal, axisVertical);
            anim.SetBool("running", axisHorizontal != 0 || axisVertical != 0);
        }
	}

	private void Update() 
	{
        if (Input.GetButton("Shoot") && HasWeaponAvailable)
        {
            SetShootAnim();
        }
        if (Input.GetButtonDown("Dash") && IsEnabled(dashMovement))
            dashMovement.Dash(transform.forward);

        if(Input.GetButtonDown("Melee") && !attacking && !dashMovement.IsDashing)
            AttackStart();

        if(Input.GetButtonDown("SwitchWeapon") && !attacking && !dashMovement.IsDashing && BothWeaponAvailable)
        {
            SwitchWeapon();   
        }

        if(Input.GetButtonDown("Cancel"))
		{
			if(GameManager.IsPaused) 
				GameManager.Instance.Resume();
			else
				GameManager.Instance.Pause();
		}

        SetIsShooting();
    }

    public void AttackStart()
    {
        anim.SetTrigger("attack");
        attacking = true;

        movement.enabled = false;
        primaryWeapon.gun.enabled = false;
        secondaryWeapon.gun.enabled = false;
    }

    public void AttackEnd()
    {
        attacking = false;

        movement.enabled = true;
        primaryWeapon.gun.enabled = true;
        secondaryWeapon.gun.enabled = true;
    }

    private void DashStarted()
    {
        movement.enabled = false;
        primaryWeapon.gun.enabled = false;
        secondaryWeapon.gun.enabled = false;
        lookAtCursor.enabled = false;

        anim.SetBool("dashing", true);
    }

    private void DashEnded()
    {
        movement.enabled = true;
        primaryWeapon.gun.enabled = true;
        secondaryWeapon.gun.enabled = true;
        lookAtCursor.enabled = true;

        anim.SetBool("dashing", false);
    }

    public void SetInputEnabled(bool enable)
    {
        lookAtCursor.enabled = enable;
        enabled = enable;
    }

    private void SwitchWeapon()
    {
        if(selectedWeapon == Weapon.WeaponCategory.PrimaryWeapon)
            SetSelectedWeapon(Weapon.WeaponCategory.SecondaryWeapon);
        else
            SetSelectedWeapon(Weapon.WeaponCategory.PrimaryWeapon);
    }

    private void SetSelectedWeapon(Weapon.WeaponCategory category)
    {
        selectedWeapon = category;

        primaryWeapon.SetActive(category == Weapon.WeaponCategory.PrimaryWeapon);
        secondaryWeapon.SetActive(category == Weapon.WeaponCategory.SecondaryWeapon);

        anim.SetBool("pistol", category == Weapon.WeaponCategory.PrimaryWeapon);
        anim.SetBool("shotgun", category == Weapon.WeaponCategory.SecondaryWeapon);

        GameManager.Instance.SetWeaponsUI(category, primaryWeaponAvailable, secondaryWeaponAvailable);
    }

    private void SetShootAnim()
    {
        switch (selectedWeapon)
        {
            case Weapon.WeaponCategory.PrimaryWeapon:
                if (!primaryWeapon.gun.waitFireRate)
                {
                    anim.SetTrigger("shoot");
                }    
                break;
            case Weapon.WeaponCategory.SecondaryWeapon:
                if (!secondaryWeapon.gun.waitFireRate)
                {
                    anim.SetTrigger("shoot");
                }
                break;
        }
    }

    void SetIsShooting()
    {
        anim.SetBool("IsShooting", Input.GetButton("Shoot"));
    }

    public void ShootWeapon()
    {
        switch (selectedWeapon)
        {
            case Weapon.WeaponCategory.PrimaryWeapon:
                if(!primaryWeapon.gun.waitFireRate)
                    
                primaryWeapon.gun.Shoot(); 
                break;
            case Weapon.WeaponCategory.SecondaryWeapon:
                if(!secondaryWeapon.gun.waitFireRate)

                secondaryWeapon.gun.Shoot();
                break;
        }   
    }

    public void SetWeaponAvailable(Weapon.WeaponCategory category)
    {
        switch (category)
        {
            case Weapon.WeaponCategory.PrimaryWeapon: 
                primaryWeaponAvailable = true; 
                SetSelectedWeapon(category);
                break;
            case Weapon.WeaponCategory.SecondaryWeapon: 
                secondaryWeaponAvailable = true;
                SetSelectedWeapon(category);
                break;
        }
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
