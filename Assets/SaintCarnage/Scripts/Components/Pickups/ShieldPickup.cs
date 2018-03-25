using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class ShieldPickup : Pickup
    {
        protected override void Apply(PlayerCharacter player)
        {
            player.controller.stats.shield = true;

			Destroy(this.gameObject);
        }
    }
}
