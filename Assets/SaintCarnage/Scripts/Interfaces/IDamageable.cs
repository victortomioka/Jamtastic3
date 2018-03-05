using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Interfaces
{
    /// <summary>
    /// Interface para objetos que podem receber dano.
    /// </summary>
    public interface IDamageable
    {
        void TakeHit(float damage);
    }
}