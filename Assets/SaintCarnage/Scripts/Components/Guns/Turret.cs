using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class Turret : MonoBehaviour
    {
        public TurretStats stats;
        public Transform turretTop;
        public AutomaticGun gunComponent;
        public LookAt lookComponent;
        

        private int minAngle;
        private int maxAngle;
        private Vector3 direction = Vector3.up;

        private void Awake() 
        {
            gunComponent.stats = this.stats;
        }

        private void Start()
        {
            minAngle = Mathf.FloorToInt(turretTop.rotation.eulerAngles.y - (stats.rotationAngle / 2));
            maxAngle = Mathf.FloorToInt(turretTop.rotation.eulerAngles.y + (stats.rotationAngle / 2));

            if(stats.turretType == TurretType.Follower)
            {
                lookComponent.target = GameManager.Instance.player.transform;
                lookComponent.turnSpeed = stats.rotationSpeed;
                lookComponent.enabled = true;
            }
        }

        private void Update()
        {
            if(stats.turretType == TurretType.Rotatory)
                Rotate();                
        }

        private void Rotate()
        {
            turretTop.Rotate(direction * stats.rotationSpeed * Time.deltaTime);

            Vector3 rotation = turretTop.rotation.eulerAngles;

            if (rotation.y >= maxAngle)
            {
                direction = Vector3.down;

                rotation.y = Mathf.Clamp(rotation.y, minAngle, maxAngle);
                turretTop.rotation = Quaternion.Euler(rotation);
            }
            else if (rotation.y <= minAngle)
            {
                direction = Vector3.up;

                rotation.y = Mathf.Clamp(rotation.y, minAngle, maxAngle);
                turretTop.rotation = Quaternion.Euler(rotation);
            }
        }
    }
}