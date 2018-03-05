using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class PlayerSoundEffects : SoundEffectManager
    {

        [Header("Clips")]
        public AudioClip clipAttackMelee;
        public AudioClip clipDash;
        public AudioClip clipShotPistol;
        public AudioClip clipShotShotgun;
        public AudioClip clipSwitchPistol;
        public AudioClip clipSwitchShotgun;
        public AudioClip clipDie;
        public AudioClip[] clipsHit;
    }
}