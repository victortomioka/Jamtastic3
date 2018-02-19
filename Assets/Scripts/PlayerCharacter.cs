using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Character
{
    public override void TakeHit(float damage)
    {
		Die();
    }

	protected override void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}