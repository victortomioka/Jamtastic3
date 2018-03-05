using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    [SerializeField]
    float Time;

    void Start () {
        Destroy(gameObject, Time);
	}
	
}
