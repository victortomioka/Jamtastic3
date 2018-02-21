using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
	public static string NextScene { get; private set; }

	public static void Load(string sceneName, bool showLoadingScreen = true)
	{
		if(showLoadingScreen)
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

    public static void Restart(bool showLoadingScreen = true)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Load(sceneName, showLoadingScreen);
    }
}
