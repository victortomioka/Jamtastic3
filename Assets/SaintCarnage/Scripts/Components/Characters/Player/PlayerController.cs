using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Interfaces;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{

    [System.Serializable]
    public struct WeaponSlot
    {
        public Gun gun;
        public GameObject physicalGun;

        public void SetActive(bool active)
        {
            gun.gameObject.SetActive(active);
            physicalGun.SetActive(active);
        }
    }

    public class PlayerController : MonoBehaviour
    {
        public AxisMovement movement;
        public WeaponSlot primaryWeapon;
        public WeaponSlot secondaryWeapon;

        public DashMovement dashMovement;
        public LookAtCursor lookAtCursor;

        public float meleeDamage;
        public float knockBackForce;

        [HideInInspector] public bool primaryWeaponAvailable;
        [HideInInspector] public bool secondaryWeaponAvailable;
        public bool HasWeaponAvailable { get { return primaryWeaponAvailable || secondaryWeaponAvailable; } }
        public bool BothWeaponAvailable { get { return primaryWeaponAvailable && secondaryWeaponAvailable; } }

        private bool attacking;
        private int groundLayer;
        private GunStats.WeaponCategory selectedWeapon;


        private Animator anim;
        private PlayerSoundEffects sfx;

        private void Start()
        {
            groundLayer = LayerMask.NameToLayer("Ground");

            anim = GetComponentInChildren<Animator>();
            sfx = GetComponentInChildren<PlayerSoundEffects>();

            primaryWeapon.SetActive(false);
            secondaryWeapon.SetActive(false);

            // SetSelectedWeapon(Weapon.WeaponCategory.PrimaryWeapon);

            GameManager.Instance.SetWeaponsUI(GunStats.WeaponCategory.PrimaryWeapon, primaryWeaponAvailable, secondaryWeaponAvailable);
        }

        private void Reset()
        {
            movement = GetComponent<AxisMovement>();
            dashMovement = GetComponent<DashMovement>();
            lookAtCursor = GetComponent<LookAtCursor>();
        }

        void FixedUpdate()
        {
            float axisHorizontal = Input.GetAxisRaw("Horizontal");
            float axisVertical = Input.GetAxisRaw("Vertical");

            if (IsEnabled(movement))
            {
                movement.Move(axisHorizontal, axisVertical);
                anim.SetBool("running", axisHorizontal != 0 || axisVertical != 0);
            }
        }

        private void Update()
        {
            if (Input.GetButton("Shoot") && HasWeaponAvailable)
            {
                SetShootAnim();
            }
            if (Input.GetButtonDown("Dash") && IsEnabled(dashMovement))
                dashMovement.Dash(transform.forward);

            if (Input.GetButtonDown("Melee") && !attacking && !dashMovement.IsDashing)
                AttackStart();

            if (Input.GetButtonDown("SwitchWeapon") && !attacking && !dashMovement.IsDashing && BothWeaponAvailable)
            {
                SwitchWeapon();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                if (GameManager.IsPaused)
                    GameManager.Instance.Resume();
                else
                    GameManager.Instance.Pause();
            }

            SetIsShooting();
        }

        public void AttackStart()
        {
            anim.SetTrigger("attack");
            sfx.Play(sfx.clipAttackMelee);

            attacking = true;

            movement.enabled = false;
            primaryWeapon.gun.enabled = false;
            secondaryWeapon.gun.enabled = false;
        }

        public void AttackEnd()
        {
            attacking = false;

            movement.enabled = true;
            primaryWeapon.gun.enabled = true;
            secondaryWeapon.gun.enabled = true;
        }

        private void DashStarted()
        {
            movement.enabled = false;
            primaryWeapon.gun.enabled = false;
            secondaryWeapon.gun.enabled = false;
            lookAtCursor.enabled = false;

            anim.SetBool("dashing", true);
            sfx.Play(sfx.clipDash);
        }

        private void DashEnded()
        {
            movement.enabled = true;
            primaryWeapon.gun.enabled = true;
            secondaryWeapon.gun.enabled = true;
            lookAtCursor.enabled = true;

            anim.SetBool("dashing", false);
        }

        public void SetInputEnabled(bool enable)
        {
            lookAtCursor.enabled = enable;
            enabled = enable;
        }

        private void SwitchWeapon()
        {
            if (selectedWeapon == GunStats.WeaponCategory.PrimaryWeapon)
                SetSelectedWeapon(GunStats.WeaponCategory.SecondaryWeapon);
            else
                SetSelectedWeapon(GunStats.WeaponCategory.PrimaryWeapon);
        }

        private void SetSelectedWeapon(GunStats.WeaponCategory category)
        {
            selectedWeapon = category;

            primaryWeapon.SetActive(category == GunStats.WeaponCategory.PrimaryWeapon);
            secondaryWeapon.SetActive(category == GunStats.WeaponCategory.SecondaryWeapon);

            anim.SetBool("pistol", category == GunStats.WeaponCategory.PrimaryWeapon);
            anim.SetBool("shotgun", category == GunStats.WeaponCategory.SecondaryWeapon);

            sfx.Play(category == GunStats.WeaponCategory.PrimaryWeapon ? sfx.clipSwitchPistol : sfx.clipSwitchShotgun);

            GameManager.Instance.SetWeaponsUI(category, primaryWeaponAvailable, secondaryWeaponAvailable);
        }

        public Gun GetSelectedGun()
        {
            if(selectedWeapon == GunStats.WeaponCategory.PrimaryWeapon)
                return primaryWeapon.gun;
            else if(selectedWeapon == GunStats.WeaponCategory.SecondaryWeapon)
                return secondaryWeapon.gun;
            else
                return null;
        }

        private void SetShootAnim()
        {
            switch (selectedWeapon)
            {
                case GunStats.WeaponCategory.PrimaryWeapon:
                    if (!primaryWeapon.gun.waitFireRate)
                    {
                        anim.SetTrigger("shoot");
                    }
                    break;
                case GunStats.WeaponCategory.SecondaryWeapon:
                    if (!secondaryWeapon.gun.waitFireRate)
                    {
                        anim.SetTrigger("shoot");
                    }
                    break;
            }
        }

        void SetIsShooting()
        {
            anim.SetBool("IsShooting", Input.GetButton("Shoot"));
        }

        public void ShootWeapon()
        {
            switch (selectedWeapon)
            {
                case GunStats.WeaponCategory.PrimaryWeapon:
                    if (!primaryWeapon.gun.waitFireRate)

                        primaryWeapon.gun.Shoot();
                    sfx.Play(sfx.clipShotPistol);
                    break;
                case GunStats.WeaponCategory.SecondaryWeapon:
                    if (!secondaryWeapon.gun.waitFireRate)

                        secondaryWeapon.gun.Shoot();
                    sfx.Play(sfx.clipShotShotgun);
                    break;
            }

            GameManager.Instance.SetAmmoText();
        }

        public void SetWeaponAvailable(GunStats.WeaponCategory category)
        {
            switch (category)
            {
                case GunStats.WeaponCategory.PrimaryWeapon:
                    primaryWeaponAvailable = true;
                    SetSelectedWeapon(category);
                    break;
                case GunStats.WeaponCategory.SecondaryWeapon:
                    secondaryWeaponAvailable = true;
                    SetSelectedWeapon(category);
                    break;
            }
        }

        private bool IsEnabled(MonoBehaviour component)
        {
            return component != null && component.enabled;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyCharacter enemy = other.gameObject.GetComponent<EnemyCharacter>();

                if (enemy != null)
                {
                    enemy.KnockBack(knockBackForce, -enemy.transform.forward);
                    sfx.Play(sfx.clipsMeleeHit);

                    IDamageable dmg = enemy.gameObject.GetComponent<IDamageable>();
                    if (dmg != null)
                        dmg.TakeHit(meleeDamage);
                }
            }
        }
    }
}