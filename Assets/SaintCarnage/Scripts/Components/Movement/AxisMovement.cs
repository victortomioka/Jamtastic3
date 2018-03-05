using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class AxisMovement : MonoBehaviour
    {
        public float speed;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(float h, float v, float speed)
        {
            Vector3 movement = new Vector3(h, 0, v);
            movement = movement.normalized * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }

        public void Move(float h, float v)
        {
            Move(h, v, this.speed);
        }
    }
}