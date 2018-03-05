using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class AutomaticGun : Gun
    {
        private void Start()
        {
            InvokeRepeating("SpawnBullet", 0, m_Weapon.FireRate);
        }
    }
}