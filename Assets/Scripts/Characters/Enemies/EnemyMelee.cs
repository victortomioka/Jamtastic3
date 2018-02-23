using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    

    protected override void Start()
    {
        base.Start();

        playerMask = LayerMask.GetMask("Player");

        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
        followTarget = GetComponent<FollowTarget>();
        lookAt = GetComponent<LookAt>();
    }

    private void Update()
    {
        if(busy)
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
        if(hits.Length > 0)
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
        if(!busy)
        {
            anim.SetTrigger("getHit");
            busy = true;

            Stop();
        }
        
        base.TakeHit(damage);
    }

    protected override void Die()
    {
        anim.SetTrigger("die");
        dead = true;

        coll.enabled = false;
        lookAt.enabled = false;

        Stop();
        
        //base.Die();
    }

    private void Stop()
    {
        followTarget.StopFollowing();
    }

    private void Follow()
    {
        if(!dead)
            followTarget.StartFollowing();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            IDamageable dmg = other.gameObject.GetComponent<IDamageable>();
            if(dmg != null)
                dmg.TakeHit(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = new Color(255, 153, 0);
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}