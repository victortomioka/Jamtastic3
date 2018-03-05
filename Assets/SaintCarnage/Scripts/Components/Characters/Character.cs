using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Interfaces;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        public abstract void TakeHit(float damage);

        protected virtual void Die()
        {
            Destroy(this.gameObject);
        }
    }
}