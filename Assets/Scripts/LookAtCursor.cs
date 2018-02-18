using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour 
{
	public Camera cam;

	[HideInInspector] public Vector3 lookDirection;

	private Plane groundPlane;

	private void Start()
	{
		groundPlane = new Plane(Vector3.up, Vector2.zero);	
	}

	private void Reset() 
	{
		cam = Camera.main;
	}

	private void Update() 
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		float enter;
		if(groundPlane.Raycast(ray, out enter))
		{
			Vector3 hitPoint = ray.GetPoint(enter);
			Vector3 lookDirection = (hitPoint - transform.position).normalized;
			lookDirection.y = 0;

			transform.LookAt(transform.position + lookDirection, Vector3.up);
		}
	}
}
