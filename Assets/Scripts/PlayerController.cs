using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public PlayerMovement movement;
	public Gun gun;

	private void Reset() 
	{
		movement = GetComponent<PlayerMovement>();
		gun = GetComponentInChildren<Gun>();
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
		if(Input.GetMouseButton(0) && IsEnabled(gun))
			gun.Shoot();
	}

	private bool IsEnabled(MonoBehaviour component)
	{
		return component != null && component.enabled;
	}
}
