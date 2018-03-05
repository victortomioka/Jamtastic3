using System.Collections;
using UnityEngine;

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