using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class WeaponPickup : Pickup
    {
        public Gun gunPrefab;
        public GameObject gunModelPrefab;

        protected override void Apply(PlayerCharacter player)
        {
            Gun gunInstance = Instantiate(gunPrefab).GetComponent<Gun>();
            gunInstance.gameObject.name = gunInstance.stats.name;

            GameObject model = Instantiate(gunModelPrefab);
            model.name = gunModelPrefab.name;

            player.controller.guns.Add(gunInstance, model);

            Destroy(this.gameObject);
        }
    }
}