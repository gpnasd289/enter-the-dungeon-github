using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBase : GOManager
{
	public delegate void GeneralEvent(PlayerBase player);

	public delegate void HealthEvent(PlayerBase player, int oldVal);

	//public Character Character;

	public Action OnDamageDealth;

	public Action OnComboEndAction;

	public float PauseAfterMove;

	public float PauseAfterFightWon;

	public Color LogColor;

	private Delegate CompleteMoveAction;

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
	public void SetHealth(int health)
	{
		Health = health;
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
	}

	public void Damage(int damage, bool canSurvive = false)
	{
	}

	public void Kill()
	{
		Alive = false;
		onDeath?.Invoke(this);
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

	public void MakeMove(Action onMoveComplete)
	{
	}

	protected void CompleteMove()
	{
	}

	/*public virtual void BindCharacter(Character character)
	{
	}*/

	public virtual void DealDamage(int damageCount, bool canSurvive = false)
	{
		int oldHealth = Health;
		Health -= damageCount;
		Health = Mathf.Clamp(Health, 0, HealthCapacity);
		onDamage?.Invoke(this, oldHealth);
		if (Health <= 0 && !canSurvive)
		{
			Kill();
		}
	}

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
