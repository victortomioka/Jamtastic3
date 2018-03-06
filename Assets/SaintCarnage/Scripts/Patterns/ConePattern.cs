using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage
{
    public class ConePattern : IPattern
    {
        public void Apply(Rigidbody[] bullets, Vector3 origin, float bulletSpeed, float spread)
        {
            // Espaço entre cada bala (baseado no spread)
            float spaceBetweenBullets = spread / (bullets.Length + 1);

            for (int i = 1; i <= bullets.Length; i++)
            {
                Rigidbody bullet = bullets[i - 1];
                Vector3 rotation = bullet.rotation.eulerAngles;

                // Âgulo mínimo da abertura do tiro            
                float minSpread = -(spread / 2) + rotation.y;

                // Calcular a rotação do projétil baseado no spread
                rotation.y = minSpread + (spaceBetweenBullets * i);
                bullet.transform.rotation = Quaternion.Euler(rotation);

                // Aplica o movimento do projetil
                bullet.AddForce(bullet.transform.forward * bulletSpeed);
            }
        }
    }
}