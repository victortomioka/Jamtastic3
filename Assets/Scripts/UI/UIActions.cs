using System.Collections;
using UnityEngine;

public class UIActions : MonoBehaviour 
{
	public void LoadScene(string name)
	{
		SceneLoader.Load(name);
	}

    public void Quit()
    {
        Application.Quit();
    }
}
