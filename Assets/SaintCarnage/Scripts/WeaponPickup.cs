using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour 
{
	public Weapon.WeaponCategory category;

	private void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Player"))
		{
			PlayerController player = other.GetComponent<PlayerController>();
			
			player.SetWeaponAvailable(category);

			Destroy(this.gameObject);
		}
	}
}