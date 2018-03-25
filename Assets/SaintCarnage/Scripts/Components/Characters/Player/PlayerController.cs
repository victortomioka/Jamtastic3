using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Interfaces;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerStats stats;

        public AxisMovement movement;

        public DashMovement dashMovement;
        public LookAtCursor lookAtCursor;
        public GunHolder guns;

        private bool attacking;
        private int groundLayer;

        private Animator anim;
        private PlayerSoundEffects sfx;

        private void Start()
        {
            groundLayer = LayerMask.NameToLayer("Ground");

            anim = GetComponentInChildren<Animator>();
            sfx = GetComponentInChildren<PlayerSoundEffects>();
        }

        private void OnEnable() 
        {
            guns.OnSelectedGun += OnSelectedGun;
        }
        private void OnDisable() 
        {
            guns.OnSelectedGun -= OnSelectedGun;
        }

        private void Reset()
        {
            movement = GetComponent<AxisMovement>();
            dashMovement = GetComponent<DashMovement>();
            lookAtCursor = GetComponent<LookAtCursor>();
            guns = GetComponentInChildren<GunHolder>();
        }

        void FixedUpdate()
        {
            float axisHorizontal = Input.GetAxisRaw("Horizontal");
            float axisVertical = Input.GetAxisRaw("Vertical");

            if (IsEnabled(movement))
            {
                movement.Move(axisHorizontal, axisVertical, stats.speed);
                anim.SetBool("running", axisHorizontal != 0 || axisVertical != 0);
            }
        }

        private void Update()
        {
            if (Input.GetButton("Shoot"))
            {
                SetShootAnim();
            }

            if (Input.GetButtonDown("Dash") && IsEnabled(dashMovement))
                dashMovement.Dash(transform.forward, stats.dashDistance, stats.dashSpeed);

            if (Input.GetButtonDown("Melee") && !attacking && !dashMovement.IsDashing)
                AttackStart();

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
        }

        public void AttackEnd()
        {
            attacking = false;

            movement.enabled = true;
        }

        private void DashStarted()
        {
            movement.enabled = false;
            lookAtCursor.enabled = false;

            anim.SetBool("dashing", true);
            sfx.Play(sfx.clipDash);
        }

        private void DashEnded()
        {
            movement.enabled = true;
            lookAtCursor.enabled = true;

            anim.SetBool("dashing", false);
        }

        public void SetInputEnabled(bool enable)
        {
            lookAtCursor.enabled = enable;
            enabled = enable;
        }

        private void OnSelectedGun(int slotIndex, Gun gun)
        {
            if(gun != null)
            {
                anim.SetBool("pistol", gun.stats.type == GunType.Pistol);
                anim.SetBool("shotgun", gun.stats.type == GunType.Shotgun);

                sfx.Play(gun.stats.clipSelected);
            }
        }

        private void SetShootAnim()
        {
            Gun gun = guns.SelectedGun;

            if(gun != null && !gun.waitFireRate)
                anim.SetTrigger("shoot");
        }

        void SetIsShooting()
        {
            anim.SetBool("IsShooting", Input.GetButton("Shoot"));
        }

        public void ShootWeapon()
        {
            Gun gun = guns.SelectedGun;
            if(gun != null)
            {
                guns.SelectedGun.Shoot();
                sfx.Play(gun.stats.clipShot);
            }

            GameManager.Instance.SetAmmoText(gun);
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
                    enemy.KnockBack(stats.knockBackForce, -enemy.transform.forward);
                    sfx.Play(sfx.clipsMeleeHit);

                    IDamageable dmg = enemy.gameObject.GetComponent<IDamageable>();
                    if (dmg != null)
                        dmg.TakeHit(stats.meleeDamage);
                }
            }
        }
    }
}