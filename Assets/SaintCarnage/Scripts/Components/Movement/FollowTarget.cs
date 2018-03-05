using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Carnapunk.SaintCarnage.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent navAgent;

        public bool IsMoving { get { return !navAgent.isStopped; } }

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();

            StopFollowing();
        }

        private void Update()
        {
            if (navAgent.isStopped)
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
            navAgent.isStopped = false;
        }

        public void StopFollowing()
        {
            navAgent.isStopped = true;
        }
    }
}