using System.Collections;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class DashMovement : MonoBehaviour
    {
        #region ATRIBUTOS
        public float distance;
        public float speed;
        public float cooldown;
        public TrailRenderer trail;

        private bool coolingDown;
        private IEnumerator coroutine;
        #endregion

        #region COMPONENTES
        private Rigidbody rb;
        private Collider coll;
        #endregion

        #region PROPRIEDADES
        public bool IsDashing { get; private set; }
        public bool IsDashAllowed { get { return !IsDashing && !coolingDown; } }
        #endregion

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }

        public void Dash(Vector3 direction)
        {
            if (IsDashAllowed)
            {
                coroutine = DashCoroutine(direction, this.distance, this.speed);
                StartCoroutine(coroutine);
            }
        }

        public void Dash(Vector3 direction, float distance, float speed)
        {
            if (IsDashAllowed)
            {
                coroutine = DashCoroutine(direction, distance, speed);
                StartCoroutine(coroutine);
            }
        }

        public void Interrupt()
        {
            StopCoroutine(coroutine);

            DashEnd();
        }

        private IEnumerator DashCoroutine(Vector3 direction, float distance, float speed)
        {

            float travelled = 0; // armazena distancia viajada
            float dashDistance = distance; // armazena distancia máxima do dash
            Vector3 startPosition = rb.position; // armazena a posição inicial do objeto

            // Aplica um raycast (boxcast) verificando se há algum colisor no caminha
            // Caso haja um colisor ajusta a distancia basiado no ponto de contato com ele
            Bounds collBounds = coll.bounds;
            RaycastHit hit;

            if (Physics.BoxCast(collBounds.center, collBounds.extents, direction, out hit, transform.rotation, distance))
            {
                dashDistance = hit.distance;
            }

            // Só aplica o dash se a distáncia que será percorrida for mais que 1 unidade
            if (dashDistance >= 1)
            {
                // Atualiza os status do dash
                DashStart();

                // Aplica um impulso na direção escolhida
                rb.AddForce(direction * speed, ForceMode.Impulse);

                while (travelled <= dashDistance)
                {
                    // Guarda a posição anterior para calcular a distância entre cada frame
                    Vector3 previousPosition = rb.position;

                    yield return new WaitForFixedUpdate();

                    // Calcula a distância percorrida
                    float dist = Vector3.Distance(previousPosition, rb.position);
                    travelled += dist;

                    // Se por algum motivo o movimento parar antes de chegar a distância estipulada, interrompe o dash
                    if (dist == 0)
                        Interrupt();
                }

                // Atualiza os status do dash
                DashEnd();
            }
        }

        private IEnumerator CooldownCoroutine()
        {
            coolingDown = true;
            yield return new WaitForSeconds(cooldown);
            coolingDown = false;
        }

        /// <summary>
        /// Ações para o início do movimento de dash. O método atualiza os estados, chama os eventos
        /// e realiza ações diversas necessárias ao início do dash.
        /// </summary>
        private void DashStart()
        {
            IsDashing = true;
            SendMessage("DashStarted", null, SendMessageOptions.DontRequireReceiver);

            SetTrailEnabled(true);
        }

        /// <summary>
        /// Ações para o fim do movimento de dash.false O método atualiza os estados, chama os eventos
        /// e realiza ações diverssas necessárias ao fim do dash.
        /// </summary>
        private void DashEnd()
        {
            rb.velocity = Vector3.zero;

            IsDashing = false;
            SendMessage("DashEnded", null, SendMessageOptions.DontRequireReceiver);

            SetTrailEnabled(false);

            if (cooldown > 0)
                StartCoroutine(CooldownCoroutine());
        }

        private void SetTrailEnabled(bool enabled)
        {
            if (trail != null)
                trail.enabled = enabled;
        }
    }
}