using UnityEngine;

namespace Carnapunk.SaintCarnage.ScriptableObjects
{
    public enum TurretType
    {
        Fixed,
        Rotatory,
        Follower
    }

    [CreateAssetMenu(menuName = "Saint Carnage/Stats/Turret Gun", fileName = "New Turret Stats")]
    public class TurretStats : GunStats
    {
        [Header("Turret Info")]
        public TurretType turretType;

        [Range(0, 360)]
        public int rotationAngle;
        public float rotationSpeed;
    }
}