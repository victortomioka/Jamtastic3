using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;

    [HideInInspector] public bool following;

    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!following)
            return;

        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;

        navAgent.SetDestination(targetPos);
    }

    public void SetSpeed(float newSpeed)
    {
        navAgent.speed = newSpeed;
    }

    public void StartFollowing()
    {
        following = true;
    }

    public void StopFollowing()
    {
        following = false;
    }
}
