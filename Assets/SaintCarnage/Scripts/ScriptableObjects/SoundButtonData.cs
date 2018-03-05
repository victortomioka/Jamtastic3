using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.ScriptableObjects
{
    /// <summary>
    /// Representa um asset que armazena os sons tocados por um botão.
    /// </summary>
    [CreateAssetMenu(menuName = "Saint Carnage/UI/Button Sound Data", fileName = "New Button Sound Data")]
    public class SoundButtonData : ScriptableObject
    {
        public AudioClip selectedClip;
        public AudioClip deselectedClip;
        public AudioClip clickedClip;
    }
}