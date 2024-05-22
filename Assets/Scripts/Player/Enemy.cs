using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerBase
{
	public int Level { get; private set; }

	public int atk;
	public int maxHealth;
	public int currentHealth;
	public HealthBar enemyHealthBar;
	public HealthBar playerHealthBar;
	private void Start()
    {
		
    }
    public void Setup(int currentLevel)
	{
		atk += currentLevel;
		SetHealth(maxHealth);
		HealthCapacity = maxHealth;
		SetAlive(true);
	}

	public override void DealDamage(int damageCount, bool canSurvive = false)
	{
		base.DealDamage(damageCount, canSurvive);
		enemyHealthBar.health = Health;
	}

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
