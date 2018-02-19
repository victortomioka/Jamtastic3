using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour 
{
	public float fireRate;

	public GameObject bulletPrefab;
	public Transform shotOrigin;

	bool waitFireRate;

	private void Reset() 
	{
		shotOrigin = transform.Find("ShotOrigin");
	}

	public void Shoot()
	{
		if(!waitFireRate)
			StartCoroutine("ShootCoroutine");
	}

	protected IEnumerator ShootCoroutine()
	{
		waitFireRate = true;

        SpawnBullet();

		yield return new WaitForSeconds(fireRate);

		waitFireRate = false;
	}

    protected void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotOrigin.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().movement.direction = transform.forward;
        bullet.SetActive(true);
    }
}
