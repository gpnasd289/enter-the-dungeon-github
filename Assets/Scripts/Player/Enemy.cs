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
	
	public EnemyAnim enemyAnim;

	private Rigidbody[] _ragdollRbs;



    private void OnEnable()
    {
		_ragdollRbs = GetComponentsInChildren<Rigidbody>();
    }
    public void Initialize()
	{
		// Initialize enemy-specific data
		SetHealth(currentHealth, maxHealth);
		SetAlive(true);
		UpdateHealthBar();
		DisableRagdoll();
	}
	public void DealDamageToPlayer(Player player)
	{
		int damage = CalculateDamage(); // Implement your damage calculation logic
		player.TakeDamage(damage);
		OnDamageDealth?.Invoke(damage);
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

	public override void OnComboOver()
	{
		
        if (CombatManager.Instance.player.Alive)
        {
            base.OnComboOver();
        }
        else
        {
			CombatManager.Instance.OnPlayerDefeated();
        }
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
	private void DisableRagdoll()
    {
		foreach (var rb in _ragdollRbs)
        {
			rb.isKinematic = true;
        }
    }
	public void EnableRagdoll()
    {
		enemyAnim.anim.enabled = false;
		foreach (var rb in _ragdollRbs)
		{
			rb.isKinematic = false;
		}
	}
	[Button]
	public void DebugState()
    {
		Debug.Log("enemy alive: " + Alive);
    }
}
