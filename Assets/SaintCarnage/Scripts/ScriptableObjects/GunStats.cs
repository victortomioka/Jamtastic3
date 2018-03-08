using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.ScriptableObjects
{
    /// <summary>
    /// Representa o asset que armazenas as informações de uma arma.
    /// </summary>
    [CreateAssetMenu(menuName = "Saint Carnage/Gun", fileName = "New Gun Stats")]
    public class GunStats : ScriptableObject
    {
        public enum WeaponClass { Shotgun, Pistol }
        public WeaponClass Class;
        public enum WeaponCategory { PrimaryWeapon, SecondaryWeapon }
        public WeaponCategory Category = WeaponCategory.PrimaryWeapon;

        public float Damage;
        public float FireRate;
        public float BulletSpeed;

        public int bulletCount;
        public float spread;

        public int maxAmmo;
        public bool unlimitedAmmo;

        public new string name;

        public GameObject BulletPrefab;
        public Patterns pattern;
    }
}