using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBase : GOManager, IHealth
{
	public delegate void GeneralEvent(PlayerBase player);

	public delegate void HealthEvent(PlayerBase player, int oldVal);

	//public Character Character;

	public Action OnDamageDealth;

	public Action OnComboEndAction;

	public float PauseAfterMove;

	public float PauseAfterFightWon;

	protected Delegate CompleteMoveAction;

	public HealthBar healthBar;

	public string Name { get; private set; }

	public int Health { get; private set; }

	public int HealthCapacity { get; set; }

	public bool Alive { get; private set; }

	public int OverKillAmount { get; private set; }

	private bool MoveActive { get; set; }

	private HealthEvent onHeal;
	public event HealthEvent OnHeal
	{
		add
		{
			onHeal += value;
		}
		remove
		{
			onHeal -= value;
		}
	}

	private HealthEvent onDamage;
	public event HealthEvent OnDamage
	{
		add
		{
			onDamage += value;
		}
		remove
		{
			onDamage -= value;
		}
	}

	private GeneralEvent onNameChange;
	public event GeneralEvent OnNameChange
	{
		add
		{
			onNameChange += value;
		}
		remove
		{
			onNameChange -= value;
		}
	}

	private GeneralEvent onDeath;
	public event GeneralEvent OnDeath
	{
		add
		{
			onDeath += value;
		}
		remove
		{
			onDeath -= value;
		}
	}

	private Action onTurnEnd;
	public event Action OnTurnEnd
	{
		add
		{
			onTurnEnd += value;
		}
		remove
		{
			onTurnEnd -= value;
		}
	}


	public void Reset()
	{
	}

	public void SetName(string name)
	{
		Name = name;
		onNameChange?.Invoke(this);
	}
	public void SetHealth(int health, int maxHealth)
	{
		Health = health;
		HealthCapacity = maxHealth;
	}
	public void SetAlive(bool alive)
	{
		Alive = alive;
	}
	public void Heal(int health)
	{
		int oldHealth = Health;
		Health = Mathf.Clamp(Health + health, 0, HealthCapacity);
        onHeal?.Invoke(this, oldHealth);
		UpdateHealthBar();
	}
	public virtual void TakeDamage(int damageCount, bool canSurvive = false)
	{
		int oldHealth = Health;
		if (Health < damageCount)
        {
			OverKillAmount = damageCount - Health;
        }
        else
        {
			Health -= damageCount;
			Health = Mathf.Clamp(Health, 0, HealthCapacity);
			onDamage?.Invoke(this, oldHealth);
		}
		
		if (Health <= 0)
		{
			Kill();
		}
		UpdateHealthBar();
	}

	public void Kill()
	{
		Alive = false;
		//onDeath?.Invoke(this);
		//CombatManager.Instance.OnEnemyDefeated();
	}
	public void UpdateHealthBar()
	{
		if (healthBar != null)
		{
			healthBar.health = Health;
		}
	}
	public float GetNormalHealth()
	{
		return 0f;
	}

	public void ChainCollapsed(CollectionChain chain)
	{
	}

	public void ItemsStacked()
	{
	}

	public void ItemsVisuallyCollected(CollectionChain chain)
	{
	}

	public void ComboOver()
	{
	}

	public virtual void MakeMove(Action onMoveComplete)
	{
	}

	public virtual void CompleteMove()
	{
		if (CombatManager.Instance.currentEnemy.Alive)
        {
			CompleteMoveAction?.DynamicInvoke();
			CompleteMoveAction = null;
		}
        else
        {
			CombatManager.Instance.OnEnemyDefeated();
		}
	}

	/*public virtual void BindCharacter(Character character)
	{
	}*/

	

	protected virtual void OnMove()
	{
	}

	protected virtual void OnMoveEnd()
	{
	}

	protected virtual void OnChainCollected(CollectionChain chain)
	{
	}

	protected virtual void OnItemsStacked()
	{
	}

	protected virtual void OnItemsVisuallyCollected(CollectionChain chain)
	{
	}

	protected virtual void OnComboOver()
	{
	}
}
