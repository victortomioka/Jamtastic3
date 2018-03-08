using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Carnapunk.SaintCarnage.Components
{
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
        public Text textAmmo;

        [Header("Audio")]
        public AudioSource musicAudioSource;
        public AudioClip gameOverTheme;

        private PlayerCharacter player;
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

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
            controller = player.GetComponent<PlayerController>();

            GameObject ui = GameObject.Find("UI_InGame");

            if (ui != null)
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

            if (panelPause != null)
                panelPause.SetActive(true);

            PlayerSound(GlobalSoundEffects.Instance.blipOut);
        }

        public void Resume()
        {
            HideCursor();

            controller.SetInputEnabled(true);

            Time.timeScale = 1;

            if (panelPause != null)
                panelPause.SetActive(false);

            PlayerSound(GlobalSoundEffects.Instance.blipOut);
        }

        public void GameOver()
        {
            ShowCursor();

            controller.SetInputEnabled(false);

            if (panelGameOver != null)
                panelGameOver.SetActive(true);

            if (musicAudioSource != null)
            {
                musicAudioSource.Stop();
                musicAudioSource.PlayOneShot(gameOverTheme);
            }
        }

        public void SetWeaponsUI(GunStats.WeaponCategory selected, bool primarySlotAvailable, bool secondarySlotAvailable)
        {
            if (!primarySlotAvailable && !secondarySlotAvailable)
            {
                iconPistol.enabled = false;
                iconShotgun.enabled = false;
            }
            else
            {
                if (primarySlotAvailable)
                    iconPistol.enabled = true;

                if (secondarySlotAvailable)
                    iconShotgun.enabled = true;

                if (selected == GunStats.WeaponCategory.PrimaryWeapon)
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

            SetAmmoText();
        }

        public void SetAmmoText()
        {
            Gun selectedGun = controller.GetSelectedGun();
            if(selectedGun != null)
            {
                textAmmo.text = selectedGun.Ammo.ToString();
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

        private void PlayerSound(AudioClip clip)
        {
            if (GlobalSoundEffects.Instance != null)
                GlobalSoundEffects.Instance.Play(clip);
        }
    }
}