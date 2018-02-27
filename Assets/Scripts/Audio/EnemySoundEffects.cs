using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEffects : SoundEffectManager 
{
    [Header("Clips")]
    public AudioClip clipAttackMelee;
    public AudioClip clipShot;
    public AudioClip clipDie;
    public AudioClip[] clipsHit;
}
