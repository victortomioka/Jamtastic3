using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	[Header("UI")]
	public GameObject panelPause;
	public GameObject panelGameOver;

	[Header("Audio")]
	public AudioSource musicAudioSource;
	public AudioClip gameOverTheme;

	private PlayerController controller;

	public static bool IsPaused { get { return Time.timeScale == 0; } }
	public static GameManager Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);

		controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		GameObject ui = GameObject.Find("UI_InGame");

		if(ui != null)
		{
			panelPause = ui.transform.Find("Panel_Pause").gameObject;
			panelGameOver = ui.transform.Find("Panel_GameOver").gameObject;
		}
	}

	private void Start() 
	{
		HideCursor();	
	}

	public void Pause()
	{
		ShowCursor();

		controller.SetInputEnabled(false);

		Time.timeScale = 0;

		if(panelPause != null)
			panelPause.SetActive(true);
	}

	public void Resume()
	{
		HideCursor();

		controller.SetInputEnabled(true);

		Time.timeScale = 1;

		if(panelPause != null)
			panelPause.SetActive(false);
	}

	public void GameOver()
	{
		ShowCursor();

		controller.SetInputEnabled(false);

		if(panelGameOver != null)
			panelGameOver.SetActive(true);

		if(musicAudioSource != null)
		{
			musicAudioSource.Stop();
			musicAudioSource.PlayOneShot(gameOverTheme);
		}
	}

	public void ShowCursor()
	{
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.visible = false;
	}
}
