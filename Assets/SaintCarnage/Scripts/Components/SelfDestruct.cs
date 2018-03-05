using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    /// <summary>
    /// Componente que destroy o objeto atribuido após um periodo de tempo (em segundo).
    /// </summary>
    public class SelfDestruct : MonoBehaviour
    {

        [SerializeField]
        float Time;

        void Start()
        {
            Destroy(gameObject, Time);
        }
    }
}