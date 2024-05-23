using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerBase
{
	public int Level { get; private set; }

	public int atk;

	public int maxHealth;
	
	public int currentHealth;
	//public HealthBar healthBar;
	public EnemyAnim enemyAnim;

	public void Initialize()
	{
		// Initialize enemy-specific data
		SetHealth(currentHealth, maxHealth);
		SetAlive(true);
		UpdateHealthBar();
	}
	public void DealDamageToPlayer(Player player)
	{
		Debug.Log("deal " + atk + " damage to " + player.name);
		int damage = CalculateDamage(); // Implement your damage calculation logic
		player.TakeDamage(damage);
		OnDamageDealth?.Invoke();
	}

    private int CalculateDamage()
    {
		return atk;
    }

	/*public void MakeMove(Action onMoveComplete)
	{
		enemyAnim.PlayAttackAnimation(1); // Adjust attack count as needed
		CompleteMoveAction = onMoveComplete;
	}

	protected void CompleteMove()
	{
		CompleteMoveAction?.DynamicInvoke();
	}*/
	/*public override void BindCharacter(Character character)
	{
	}*/

	protected override void OnComboOver()
	{
	}

	protected override void OnMove()
	{
	}
	/*public void Attack(int damage, float attackTime)
	{
		enemyAnimator.SetTrigger("Attack");
		Invoke(nameof(DealDamageToPlayer), attackTime);
		OnDamageDealth?.Invoke();
	}

	private void DealDamageToPlayer()
	{
		int playerDamage = UnityEngine.Random.Range(5, 15); // Example damage range
		playerHealthBar.health -= playerDamage;

		if (playerHealthBar.health <= 0)
		{
			OnComboEndAction?.Invoke();
		}
	}*/
}
