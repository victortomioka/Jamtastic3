using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyCharacter
{
    public float attackRange;
    public float followRange;

    private bool startedFollow;
    private LayerMask playerMask;

    private FollowTarget followTarget;
    private Animator anim;

    protected override void Start()
    {
        base.Start();

        playerMask = LayerMask.GetMask("Player");
        followTarget = GetComponent<FollowTarget>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Collider[] hits;

        if (!startedFollow)
        {
            hits = Physics.OverlapSphere(transform.position, followRange, playerMask);
            if (hits.Length > 0)
            {
                followTarget.StartFollowing();
                startedFollow = false;
            }
        }

        hits = Physics.OverlapSphere(transform.position, attackRange, playerMask);
        if(hits.Length > 0)
        {
            AttackStart();
        }

        anim.SetBool("walking", followTarget.following);
    }

    public void AttackStart()
    {
        anim.SetTrigger("attack");
        followTarget.StopFollowing();
    }

    public void AttackEnd()
    {
        followTarget.StartFollowing();
    }

    public override void TakeHit(float damage)
    {
        anim.SetTrigger("getHit");

        base.TakeHit(damage);
    }

    protected override void Die()
    {
        anim.SetTrigger("die");

        followTarget.StopFollowing();
        
        //base.Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = new Color(255, 153, 0);
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}