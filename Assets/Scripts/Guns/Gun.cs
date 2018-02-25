using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour 
{
	public float fireRate;

	public GameObject bulletPrefab;
	public Transform shotOrigin;

	[HideInInspector] public bool waitFireRate;

    [SerializeField]
    protected Weapon m_Weapon;

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

		yield return new WaitForSeconds(m_Weapon.FireRate);

		waitFireRate = false;
	}


    public void WeaponShoot()
    {
        switch (m_Weapon.Class)
        {
            case Weapon.WeaponClass.Shotgun: ShotgunShoot(); break;
            case Weapon.WeaponClass.Pistol: PistolShoot(); break;
        }
    }

    void ShotgunShoot()
    {
        for (int i = 0; i < m_Weapon.ShotgunBulletsCount; i++)
        {
            var BulletRot = shotOrigin.rotation;
            BulletRot.y += Random.Range(-m_Weapon.ShotgunSpread, m_Weapon.ShotgunSpread);
            GameObject GB = Instantiate(m_Weapon.BulletPrefab, shotOrigin.position, BulletRot);
            GB.GetComponent<Rigidbody>().AddForce(GB.transform.forward * m_Weapon.BulletSpeed);
        }
    }

    void PistolShoot()
    {
        GameObject GB = Instantiate(m_Weapon.BulletPrefab, shotOrigin.position, shotOrigin.rotation);
        GB.GetComponent<Rigidbody>().AddForce(shotOrigin.forward * m_Weapon.BulletSpeed);
    }
    protected void SpawnBullet()
    {

        WeaponShoot();
        //GameObject bullet = Instantiate(bulletPrefab, shotOrigin.position, Quaternion.identity);
        //bullet.GetComponent<Projectile>().movement.direction = transform.forward;
        //bullet.SetActive(true);
    }
}