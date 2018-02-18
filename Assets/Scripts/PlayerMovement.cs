using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float speed;

	private Rigidbody rb;

	private void Start() 
	{
		rb = GetComponent<Rigidbody>();	
	}

	void FixedUpdate() 
	{
		float axisHorizontal = Input.GetAxis("Horizontal");
		float axisVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(axisHorizontal, 0, axisVertical) * speed * Time.deltaTime;
		Vector3 position = rb.position + movement;
		rb.MovePosition(position);
	}	
}
