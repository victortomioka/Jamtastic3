using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage
{
    public class LinearPattern : IPattern
    {
        public void Apply(Rigidbody[] bullets, Vector3 origin, float bulletSpeed, float spread)
        {
            if (bullets == null || bullets.Length == 0)
                return;

            var bullet = bullets[0];
            bullet.AddForce(bullet.transform.forward * bulletSpeed);
        }
    }
}