using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class EnemySoundEffects : SoundEffectManager
    {
        [Header("Clips")]
        public AudioClip clipAttackMelee;
        public AudioClip clipShot;
        public AudioClip clipDie;
        public AudioClip[] clipsHit;
    }
}