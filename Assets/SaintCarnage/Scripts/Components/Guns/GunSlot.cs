using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class GunSlot : MonoBehaviour
    {
        public GunType type;
        public Gun gun;

        public bool IsEmpty { get { return transform.childCount == 0; } }

        private void Start() 
        {
            Gun childGun = GetComponentInChildren<Gun>();
            
            if(childGun != null)
            {
                if(childGun.stats.type == type)
                    SetGun(childGun);
                else
                    Destroy(childGun.gameObject);
            }
        }

        public void SetGun(Gun newGun)
        {
            if(!IsEmpty)
            {
                if(gun.stats.name == newGun.stats.name)
                {   // Se for a mesma arma, recupera a munição
                    gun.Ammo = gun.stats.maxAmmo;
                    return;
                }
                else
                {   // Se for outra arma, remove a arma atual antes de inserir a proxima
                    gun.transform.SetParent(null);
                    Destroy(gun.gameObject);
                }
            }

            newGun.transform.SetParent(this.transform);
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
            gun = newGun;
        }
    }
}