using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionChain : GOManager
{
	public Action<Cell> OnChainBegin;

	public Action OnChainEnd;

	public ParticleSystem LowFinisherReadyEffect;

	public ParticleSystem MedFinisherReadyEffect;

	public ParticleSystem HighFinisherReadyEffect;

	protected readonly List<Cell> Cells;

	public LineRenderer Line;

	public int Length => 0;

	public int ChainItemId { get; protected set; }

	public virtual void ResetChain()
	{
	}

	public void Begin(Cell startingCell)
	{
		OnChainBegin(startingCell);
	}

	public virtual void End(bool chainMatched)
	{
		OnChainEnd();
	}

	public List<Cell> CollectCells(Predicate<Cell> predicate = null)
	{
		//(Cell c) => { return c.Item.id == ChainItemId; };
		return null;
	}

	public void AddCell(Cell cell, bool highlight = false)
	{
	}

	protected void RemoveCell(Cell cell)
	{
	}

	protected virtual void UpdateDisplay()
	{
	}

	public bool ContainsFinisherPowerup()
	{
		return false;
	}

	protected virtual int GetChainItemID()
	{
		return 0;
	}

	public virtual void EnterCell(Cell cell)
	{
	}

	public void ExitCell(Cell cell)
	{
	}

	public void OverCell(Cell cell)
	{
	}
}
