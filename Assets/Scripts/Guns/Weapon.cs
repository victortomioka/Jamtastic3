using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Weapon", menuName = "Weapons")]
public class Weapon : ScriptableObject {
    public enum WeaponClass {Shotgun, Pistol}
    public WeaponClass Class;
    public enum WeaponCategory {PrimaryWeapon, SecondaryWeapon}
    public WeaponCategory Category = WeaponCategory.PrimaryWeapon;

    public float Damage;
    public float FireRate;
    public float BulletSpeed;

    public float ShotgunBulletsCount;
    public float ShotgunSpread;

    public new string name;

    public GameObject BulletPrefab;

   
	
}
