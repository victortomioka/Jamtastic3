using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage
{
	public interface IPattern
	{
		void Apply(Rigidbody[] bullets, Vector3 origin, float bulletSpeed, float spread);
	}
}