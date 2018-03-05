using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour 
{
	public Camera cam;

	public Transform crossHair;
	[HideInInspector] public Vector3 lookDirection;
	[HideInInspector] public Vector3 mousePos;

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
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		float enter;
		if(groundPlane.Raycast(ray, out enter))
		{
			Vector3 hitPoint = ray.GetPoint(enter);
			if(crossHair != null)
				crossHair.position = new Vector3(hitPoint.x, crossHair.position.y, hitPoint.z);
			Vector3 lookDirection = (hitPoint - transform.position).normalized;
			lookDirection.y = 0;

			transform.LookAt(transform.position + lookDirection, Vector3.up);
		}
	}
}
