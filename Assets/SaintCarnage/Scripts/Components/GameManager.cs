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

        public Image[] gunSlots;
        public Image[] gunIcons;

        public Text textAmmo;

        public Image primarySlot;
        public Image secondarySlot;
        public Image iconPistol;
        public Image iconShotgun;
        public Sprite slotNormal;
        public Sprite slotSelected;


        [Header("Audio")]
        public AudioSource musicAudioSource;
        public AudioClip gameOverTheme;

        public PlayerCharacter player;
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
            SetGunUI();
        }

        private void OnEnable() 
        {
            controller.guns.OnSelectedGun += OnSelectedGun;
        }
        private void OnDisable() 
        {
            controller.guns.OnSelectedGun -= OnSelectedGun;
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

        private void SetGunUI()
        {
            for (int i = 0; i < gunSlots.Length; i++)
            {
                Image uiSlot = gunSlots[i];
                GunSlot slot = controller.guns.slots[i];

                SetGunSlotIcon(i, slot);

                if (!slot.IsEmpty)
                {
                    if (i == controller.guns.selectedSlot)
                        uiSlot.sprite = slotSelected;
                    else
                        uiSlot.sprite = slotNormal;
                }
            }

            SetAmmoText(controller.guns.SelectedGun);
        }

        private void OnSelectedGun(int slotIndex, Gun selectedGun)
        {
            if(selectedGun == null)
                return;
                
            for (int i = 0; i < gunSlots.Length; i++)
            {
                if(i == slotIndex)
                    gunSlots[i].sprite = slotSelected;
                else
                    gunSlots[i].sprite = slotNormal;
            }

            SetAmmoText(controller.guns.SelectedGun);
        }

        public void SetGunSlotIcon(int index, GunSlot slot)
        {
            if (slot.IsEmpty)
            {
                gunIcons[index].gameObject.SetActive(false);
            }
            else
            {
                gunIcons[index].gameObject.SetActive(true);
                gunIcons[index].sprite = slot.gun.stats.icon;
            }
        }

        public void SetAmmoText(Gun gun)
        {
            if (gun != null)
            {
                textAmmo.text = gun.Ammo.ToString();
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