using System.Collections;
using Carnapunk.SaintCarnage.Helpers;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class LoadingScreen : MonoBehaviour
    {
        public float fixedDelay;

        private void Start()
        {
            Invoke("LoadNextScene", fixedDelay);
        }

        private void LoadNextScene()
        {
            string nextScene = SceneLoader.NextScene;

            if (string.IsNullOrEmpty(nextScene))
                SceneLoader.Load("Menu", false);
            else
                SceneLoader.Load(nextScene, false);
        }
    }
}