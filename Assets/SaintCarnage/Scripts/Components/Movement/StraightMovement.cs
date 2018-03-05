using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class StraightMovement : MonoBehaviour
    {
        public float speed;
        public Vector3 direction;
        public bool automatic;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            direction.Normalize();
        }

        void FixedUpdate()
        {
            if(automatic)
                Move();
        }

        public void Move()
        {
            Move(this.speed, this.direction);
        }

        public void Move(float speed, Vector3 direction)
        {
            Vector3 movement = direction * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}