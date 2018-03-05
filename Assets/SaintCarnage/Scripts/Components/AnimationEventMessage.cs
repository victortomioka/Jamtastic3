using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    /// <summary>
    /// Classe auxiliar que permite a um objeto repassar um eventos de animação para objetos pai ou objetos filhos.
    /// Ela usa o sistema de Mensagens do Unity para chamar um evento pelo nome em classes pai ou classes filhas.
    /// </summary>
    public class AnimationEventMessage : MonoBehaviour
    {
        /// <summary>
        /// Chama o método "eventName" nos componentes filhos do Animator que este componente está inserido.
        /// </summary>
        /// <param name="eventName">Nome do método a ser chamado nos componentes filhos.</param>
        public void SendBroadcast(string eventName)
        {
            BroadcastMessage(eventName, null, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Chama o método "eventName" nos componentes pai do Animator que este componente está inserido.
        /// </summary>
        /// <param name="eventName">Nome do método a ser chamado nos componentes pai.</param>
        public void SendUpwards(string eventName)
        {
            SendMessageUpwards(eventName, null, SendMessageOptions.DontRequireReceiver);
        }
    }
}