using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Carnapunk.SaintCarnage.Components
{
    public class PlayerCharacter : Character
    {
        private Animator anim;
        [HideInInspector] public PlayerController controller;
        private PlayerSoundEffects sfx;

        private bool dead;

        protected void Start()
        {
            anim = GetComponentInChildren<Animator>();
            controller = GetComponent<PlayerController>();
            sfx = GetComponentInChildren<PlayerSoundEffects>();
        }

        public override void TakeHit(float damage)
        {
            if (dead)
                return;

            sfx.Play(sfx.clipsHit);

            if(controller.stats.shield)
            {
                controller.stats.shield = false;
                return;
            }         
            
            Die();
        }

        protected override void Die()
        {
            dead = true;
            controller.SetInputEnabled(false);
            anim.SetTrigger("die");
            sfx.Play(sfx.clipDie);
        }

        private void DieAnimationEnd()
        {
            Invoke("GameOver", 2);
        }

        private void GameOver()
        {
            GameManager.Instance.GameOver();
        }
    }
}