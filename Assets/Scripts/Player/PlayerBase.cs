using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBase : GOManager, IHealth
{
	public delegate void GeneralEvent(PlayerBase player);

	public delegate void HealthEvent(PlayerBase player, float oldVal);

	//public Character Character;

	public Action<float> OnDamageDealth;

	public Action OnComboEndAction;

	public float PauseAfterMove;

	public float PauseAfterFightWon;

	protected Delegate CompleteMoveAction;

	public HealthBar healthBar;

	public string Name { get; private set; }

	public float Health { get; private set; }

	public float HealthCapacity { get; set; }

	public bool Alive { get; private set; }

	public float OverKillAmount { get; private set; }

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

	private Action onDeath;
	public event Action OnDeath
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
			OnComboEndAction += value;
		}
		remove
		{
			onTurnEnd -= value;
			OnComboEndAction += value;
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
	public void SetHealth(float health)
	{
		healthBar.InitializeHealthBar(health);
		Health = health;
		HealthCapacity = health;
	}
	public void SetAlive(bool alive)
	{
		Alive = alive;
	}
	public void Heal(float health)
	{
		float oldHealth = Health;
		Health = Mathf.Clamp(Health + health, 0, HealthCapacity);
        onHeal?.Invoke(this, oldHealth);
		UpdateHealthBar();
	}
	public virtual void TakeDamage(float damageCount, bool canSurvive = false)
	{
		float oldHealth = Health;
		if (Health < damageCount)
        {
			Health -= damageCount;
			Health = Mathf.Clamp(Health, 0, HealthCapacity);
			onDamage?.Invoke(this, oldHealth);
			OverKillAmount = damageCount - Health;
			Debug.Log("OverKillAmount" + OverKillAmount);
			Kill();
		}
		else if (Health == damageCount)
        {
			Health -= damageCount;
			Health = Mathf.Clamp(Health, 0, HealthCapacity);
			onDamage?.Invoke(this, oldHealth);
			Kill();
		}
        else if (Health > damageCount)
        {
			Health -= damageCount;
			Health = Mathf.Clamp(Health, 0, HealthCapacity);
			onDamage?.Invoke(this, oldHealth);

		}
		UpdateHealthBar();
	}

	public void Kill()
	{
		Alive = false;
		//onDeath?.Invoke();
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
		/*if (CombatManager.Instance.currentEnemy.Alive)
        {
			CompleteMoveAction?.DynamicInvoke();
			CompleteMoveAction = null;
		}
        else
        {
			CombatManager.Instance.OnEnemyDefeated();
		}*/
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
	public virtual void OnComboOver()
	{
		onTurnEnd?.Invoke();
	}
}
