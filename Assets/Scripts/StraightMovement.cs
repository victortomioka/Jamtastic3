using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StraightMovement : MonoBehaviour 
{
	public float speed;
	public Vector3 direction;

	private Rigidbody rb;

	private void Start() 
	{
		rb = GetComponent<Rigidbody>();

		direction.Normalize();
	}

	void FixedUpdate() 
	{
		if(rb == null)
			return;

		Vector3 movement = direction * speed * Time.deltaTime;
		rb.MovePosition(rb.position + movement);
	}
}
