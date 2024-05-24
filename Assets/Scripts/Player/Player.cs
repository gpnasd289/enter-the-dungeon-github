using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
	public void DealDamageToEnemy(Enemy enemy, int atkTime)
	{
		int damage = CalculateDamage(atkTime); // Implement your damage calculation logic
		enemy.TakeDamage(damage);
		OnDamageDealth?.Invoke(damage);
	}

    private int CalculateDamage(int atkTime)
    {
		Debug.Log("attack time: " + atkTime);
		if (atkTime > 0)
        {
			Debug.Log("attack " + atkTime + " damage " + atk);
			return atk;
		}
        else
        {
			Debug.Log("dmg multiply: " + atk * CombatManager.Instance.multipleDmg);
			return atk * CombatManager.Instance.multipleDmg;
        }
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

	public override void OnComboOver()
	{
		/*OnComboEndAction = (() => CombatManager.Instance.EnemyAttack(CombatManager.Instance.atkTime));
		OnComboEndAction?.Invoke();*/
		if (CombatManager.Instance.currentEnemy.Alive)
        {
			CombatManager.Instance.EnemyAttack(CombatManager.Instance.atkTime);
		}
        else
        {
			CombatManager.Instance.OnEnemyDefeated();
        }
	}

	protected override void OnMove()
	{
	}

	protected override void OnMoveEnd()
	{
	}
}
