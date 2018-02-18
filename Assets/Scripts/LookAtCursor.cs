using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour 
{
	public Camera cam;
	public LayerMask groundMask;

	[HideInInspector] public Vector3 lookDirection;

	private void Reset() 
	{
		cam = Camera.main;	
		groundMask = LayerMask.GetMask("Ground");
	}

	private void Update() 
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100, groundMask))
		{
			Vector3 pos = transform.position;

			lookDirection = (hit.point - pos).normalized;
			lookDirection.y = 0;
			transform.LookAt(pos + lookDirection, Vector3.up);
		}
	}
}
