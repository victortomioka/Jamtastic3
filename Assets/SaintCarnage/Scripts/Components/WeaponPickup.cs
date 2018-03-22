using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class WeaponPickup : MonoBehaviour
    {
        public Gun gunPrefab;
        public GameObject gunModelPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();

                Gun gunInstance = Instantiate(gunPrefab).GetComponent<Gun>();
                gunInstance.gameObject.name = gunInstance.stats.name;

                GameObject model = Instantiate(gunModelPrefab);
                model.name = gunModelPrefab.name;

                player.guns.Add(gunInstance, model);


                Destroy(this.gameObject);
            }
        }
    }
}