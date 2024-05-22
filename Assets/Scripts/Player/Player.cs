using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : PlayerBase
{
	public LevelManager levelManager;
	public float DelayAfterGrowth;

	public int atk;
	public int maxHealth;
	public int currentHealth;
	public HealthBar playerHealthBar;
	public HealthBar enemyHealthBar;

	private int ShuffleMovesLeft;

	public int ActiveWeaponID { get; private set; }

	public event Action OnPlayerTurn
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}
	public void DealDamageToEnemy()
	{ // Example damage range
		enemyHealthBar.health -= atk;

		if (enemyHealthBar.health <= 0)
		{
			//OnComboEndAction?.Invoke();
			levelManager.GenerateEnemy();
		}
	}
	public void EquipWeapon(int index)
	{
	}

	private void MoveOrShuffle()
	{
	}

	/*public override void BindCharacter(Character character)
	{
	}*/

	protected override void OnChainCollected(CollectionChain chain)
	{
	}

	protected override void OnItemsVisuallyCollected(CollectionChain chain)
	{
	}

	protected override void OnComboOver()
	{
	}

	protected override void OnMove()
	{
	}

	protected override void OnMoveEnd()
	{
	}
}
