using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Character
{
    public Gun[] slots;

    private int selectedSlot;

    private Animator anim;
    private PlayerController controller;

    protected void Start()
    {
        slots = new Gun[2];

        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
    }

    public void SetWeapon(Gun weapon, int slotIndex)
    {
        if(slotIndex >= slots.Length)
            return;

        slots[slotIndex] = weapon;
    }

    public override void TakeHit(float damage)
    {
		Die();
    }

	protected override void Die()
	{
        controller.SetInputEnabled(false);
        anim.SetTrigger("die");
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