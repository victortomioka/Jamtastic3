using UnityEngine;
using UnityEngine.SceneManagement;

namespace Carnapunk.SaintCarnage.Helpers
{
    /// <summary>
    /// Classe auxiliar para carregamento de cenas.
    /// </summary>
    public class SceneLoader
    {
        public static string NextScene { get; private set; }

        /// <summary>
        /// Carrega uma cena pelo nome. A cena pode ser carregada de forma
        /// assíncrona ou carregada diretamente através dos parâmetros definidos na
        /// chamada do método.
        /// </summary>
        /// <param name="sceneName">Nome da cena que será carregada.</param>
        /// <param name="showLoadingScreen">
        /// Define se será exibida a tela de carregamento. Ao ser definida como "true",
        /// antes de carregar a cena de destino, carrega a cena "Loading" que possui uma animação
        /// indicando o carregamento e, em seguida, carrega a cena especificada no outro parâmetro.
        /// Ao ser definada com o valor "false", a cena de destino é carregada diretamente (de forma síncrona).
        /// </param>
        public static void Load(string sceneName, bool showLoadingScreen = true)
        {
            if (showLoadingScreen)
            {
                NextScene = sceneName;
                SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
            }
            else
            {
                NextScene = null;
                SceneManager.LoadScene(sceneName);
            }
        }

        /// <summary>
        /// Recarrega a cena atual.A cena pode ser carregada de forma
        /// assíncrona ou carregada diretamente através dos parâmetros definidos na
        /// chamada do método.
        /// </summary>
        /// <param name="showLoadingScreen">
        /// Define se será exibida a tela de carregamento. Ao ser definida como "true",
        /// antes de carregar a cena de destino, carrega a cena "Loading" que possui uma animação
        /// indicando o carregamento e, em seguida, carrega a cena especificada no outro parâmetro.
        /// Ao ser definada com o valor "false", a cena de destino é carregada diretamente (de forma síncrona).
        /// </param>
        public static void Restart(bool showLoadingScreen = true)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            Load(sceneName, showLoadingScreen);
        }
    }
}