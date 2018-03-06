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

        private IPattern pattern;

        private void Awake() 
        {
            pattern = PatternFactory.Get(stats.pattern);
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
            if (!waitFireRate)
                StartCoroutine("ShootCoroutine");
        }

        protected IEnumerator ShootCoroutine()
        {
            waitFireRate = true;

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