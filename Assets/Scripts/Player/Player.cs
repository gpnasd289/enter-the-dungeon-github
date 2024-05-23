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
	//public HealthBar healthBar;
	public PlayerAnim playerAnim;

	public void Initialize()
	{
		// Initialize enemy-specific data
		SetHealth(currentHealth, maxHealth);
		SetAlive(true);
		UpdateHealthBar();
	}
	public void DealDamageToEnemy(Enemy enemy)
	{
		int damage = CalculateDamage(); // Implement your damage calculation logic
		enemy.TakeDamage(damage);
		OnDamageDealth?.Invoke();
	}

    private int CalculateDamage()
    {
		return atk;
    }

    public override void MakeMove(Action onMoveComplete)
    {
        //playerAnim.PlayAttackAnimation(1); // Adjust attack count as needed
        CompleteMoveAction = onMoveComplete;
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
