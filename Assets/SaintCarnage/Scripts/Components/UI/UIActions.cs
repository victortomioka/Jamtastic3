﻿using Carnapunk.SaintCarnage.Helpers;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    /// <summary>
    /// Possui funções para serem chamadas nos eventos de elementos de UI.
    /// </summary>
    public class UIActions : MonoBehaviour
    {
        public void LoadScene(string name)
        {
            if (GameManager.IsPaused)
                Time.timeScale = 1;

            SceneLoader.Load(name);
        }

        public void RestartScene()
        {
            if (GameManager.IsPaused)
                Time.timeScale = 1;

            SceneLoader.Restart();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ResumeGame()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.Resume();
        }
    }
}