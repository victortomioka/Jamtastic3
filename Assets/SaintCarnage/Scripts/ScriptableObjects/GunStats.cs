using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.ScriptableObjects
{
    public enum GunType
    {
        Pistol,
        Shotgun,
        Submachine
    }

    /// <summary>
    /// Representa o asset que armazenas as informações de uma arma.
    /// </summary>
    [CreateAssetMenu(menuName = "Saint Carnage/Stats/Gun", fileName = "New Gun Stats")]
    public class GunStats : ScriptableObject
    {
        [Header("Gun Info")]
        public GunType type;

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

        [Header("Sound Effects")]
        public AudioClip clipShot;
        public AudioClip clipSelected;
        public AudioClip clipEmpty;

        [Header("UI")]
        public Sprite icon;
    }
}