using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class WeaponPickup : MonoBehaviour
    {
        public GunStats.WeaponCategory category;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();

                player.SetWeaponAvailable(category);

                Destroy(this.gameObject);
            }
        }
    }
}