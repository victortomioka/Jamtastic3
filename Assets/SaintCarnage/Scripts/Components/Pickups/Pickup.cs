using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public abstract class Pickup : MonoBehaviour
    {
        protected abstract void Apply(PlayerCharacter player);
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();

                if(player != null)
                    Apply(player);
            }
        }
    }
}