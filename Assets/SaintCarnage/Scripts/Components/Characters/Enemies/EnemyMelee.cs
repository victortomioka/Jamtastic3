﻿using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Carnapunk.SaintCarnage.Components
{
    public class EnemyMelee : EnemyCharacter
    {
        public float attackRange;
        public float followRange;

        private bool busy;
        private bool dead;
        private bool startedFollow;
        private LayerMask playerMask;

        private Animator anim;
        private Collider coll;
        private FollowTarget followTarget;
        private LookAt lookAt;
        private EnemySoundEffects sfx;


        protected override void Start()
        {
            base.Start();

            playerMask = LayerMask.GetMask("Player");

            anim = GetComponentInChildren<Animator>();
            coll = GetComponent<Collider>();
            followTarget = GetComponent<FollowTarget>();
            lookAt = GetComponent<LookAt>();
            sfx = GetComponentInChildren<EnemySoundEffects>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                followTarget.target = player.transform;
                lookAt.target = player.transform;
            }
        }

        private void Update()
        {
            if (busy)
                return;

            Collider[] hits;

            if (!startedFollow)
            {
                hits = Physics.OverlapSphere(transform.position, followRange, playerMask);
                if (hits.Length > 0)
                {
                    Follow();
                    startedFollow = true;
                }
            }

            hits = Physics.OverlapSphere(transform.position, attackRange, playerMask);
            if (hits.Length > 0)
            {
                AttackStart();
            }

            anim.SetBool("walking", followTarget.IsMoving);
        }

        public void AttackStart()
        {
            anim.SetTrigger("attack");
            busy = true;

            Stop();
        }

        public void AttackHit()
        {
            sfx.Play(sfx.clipAttackMelee);
        }

        public void AttackEnd()
        {
            busy = false;

            Follow();
        }

        public void TakeHitEnd()
        {
            busy = false;

            Follow();
        }

        public override void TakeHit(float damage)
        {
            if (!busy)
            {
                anim.SetTrigger("getHit");
                busy = true;

                Stop();
            }

            base.TakeHit(damage);

            if (!dead)
                sfx.Play(sfx.clipsHit);
        }

        protected override void Die()
        {
            anim.SetTrigger("die");
            dead = true;

            coll.enabled = false;
            lookAt.enabled = false;

            sfx.Play(sfx.clipDie);

            Stop();

            //base.Die();
        }

        private void Stop()
        {
            followTarget.StopFollowing();
        }

        private void Follow()
        {
            if (!dead)
                followTarget.StartFollowing();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                IDamageable dmg = other.gameObject.GetComponent<IDamageable>();
                if (dmg != null)
                {
                    sfx.Play(sfx.clipsMeleeHit);
                    dmg.TakeHit(1);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = new Color(255, 153, 0);
            Gizmos.DrawWireSphere(transform.position, followRange);
        }
    }
}