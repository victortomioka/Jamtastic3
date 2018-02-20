using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public PlayerMovement movement;
	public Gun gun;
    public DashMovement dashMovement;
    
    private int groundLayer;

    private void Start() 
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void Reset() 
	{
		movement = GetComponent<PlayerMovement>();
		gun = GetComponentInChildren<Gun>();
        dashMovement = GetComponent<DashMovement>();
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
        if (Input.GetMouseButton(0) && IsEnabled(gun))
			gun.Shoot();

        if (Input.GetMouseButtonDown(1) && IsEnabled(dashMovement))
            dashMovement.Dash(transform.forward);
	}

    private void DashStarted()
    {
        movement.enabled = false;
        gun.enabled = false;
    }

    private void DashEnded()
    {
        movement.enabled = true;
        gun.enabled = true;
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

}
