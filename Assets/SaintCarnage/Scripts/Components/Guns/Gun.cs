using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class Gun : MonoBehaviour
    {
        public Transform shotOrigin;
        public GunStats stats;

        [HideInInspector] public bool waitFireRate;

        public int Ammo { get; set; }

        private IPattern pattern;

        private void Start() 
        {
            pattern = PatternFactory.Get(stats.pattern);

            Ammo = stats.maxAmmo;
        }

        private void Reset()
        {
            shotOrigin = transform.Find("ShotOrigin");
        }

        private void OnDisable()
        {
            waitFireRate = false;
        }

        public void Shoot()
        {
            if (waitFireRate)
                return;

            if(stats.unlimitedAmmo || Ammo > 0)
                StartCoroutine("ShootCoroutine");
        }

        protected IEnumerator ShootCoroutine()
        {
            waitFireRate = true;

            if(!stats.unlimitedAmmo)
                Ammo--;

            SpawnBullets();

            yield return new WaitForSeconds(stats.FireRate);

            waitFireRate = false;
        }

        private void SpawnBullets()
        {
            Rigidbody[] bullets = new Rigidbody[stats.bulletCount];

            for (int i = 0; i < stats.bulletCount; i++)
            {
                GameObject bullet = Instantiate(stats.BulletPrefab, shotOrigin.position, shotOrigin.rotation);
                bullet.GetComponent<Projectile>().damage = stats.Damage;
                bullets[i] = bullet.GetComponent<Rigidbody>();
            }

            pattern.Apply(bullets, shotOrigin.position, stats.BulletSpeed, stats.spread);
        }
    }
}