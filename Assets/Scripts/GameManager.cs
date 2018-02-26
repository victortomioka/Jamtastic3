using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	[Header("UI")]
	public GameObject panelPause;
	public GameObject panelGameOver;

	public Image primarySlot;
	public Image secondarySlot;
	public Image iconPistol;
	public Image iconShotgun;
	public Sprite slotNormal;
	public Sprite slotSelected;

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

	public void SetWeaponsUI(Weapon.WeaponCategory selected, bool primarySlotAvailable, bool secondarySlotAvailable)
	{
		if(!primarySlotAvailable && !secondarySlotAvailable)
		{
			iconPistol.enabled = false;
			iconShotgun.enabled = false;
		}
		else
		{
			if(primarySlotAvailable)
				iconPistol.enabled = true;

			if(secondarySlotAvailable)
				iconShotgun.enabled = true;

			if(selected == Weapon.WeaponCategory.PrimaryWeapon)
			{
				primarySlot.sprite = slotSelected;
				secondarySlot.sprite = slotNormal;
			}
			else
			{
				primarySlot.sprite = slotNormal;
				secondarySlot.sprite = slotSelected;
			}
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