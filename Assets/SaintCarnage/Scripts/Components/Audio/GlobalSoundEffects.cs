using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class GlobalSoundEffects : MonoBehaviour
    {
        [Header("Clips")]
        public AudioClip blipIn;
        public AudioClip blipOut;
        public AudioClip blipStart;

        public static GlobalSoundEffects Instance { get; private set; }

        private AudioSource source;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            source.PlayOneShot(clip);
        }
    }
}