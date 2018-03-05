using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Interfaces;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class Projectile : MonoBehaviour
    {
        public float damage;

        private Collider coll;

        private void Start()
        {
            coll = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!coll.enabled)
                return;

            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.TakeHit(damage);


            Destroy(this.gameObject);
        }
    }
}