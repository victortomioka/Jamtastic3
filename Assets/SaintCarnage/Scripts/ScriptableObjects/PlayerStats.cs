using UnityEngine;

namespace Carnapunk.SaintCarnage.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Saint Carnage/Stats/Player", fileName = "New Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Movement")]
        public float speed;

        [Header("Dash")]
        public float dashDistance;
        public float dashSpeed;
        public float dashCooldown;

        [Header("Melee")]
        public float meleeDamage;
        public float knockBackForce;

        [Header("Shield")]
        public bool shield;
        
    }
}