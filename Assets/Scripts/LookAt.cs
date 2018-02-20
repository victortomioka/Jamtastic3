using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LookAt : MonoBehaviour
{
    public Transform target;
    public float turnSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Turn(direction);
    }

    public void Turn(Vector3 direction)
    {
        // Ajusta a rotação de acordo com a direção
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Remove a rotação em X e Z para o objeto não inclinar
        rotation.x = 0;
        rotation.z = 0;

        // Aplica a rotação de acordo com a velocidade
        rb.rotation = Quaternion.Lerp(rb.rotation, rotation, turnSpeed * Time.deltaTime);
    }
}