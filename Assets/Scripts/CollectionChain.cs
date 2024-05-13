using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuangDM.Common;

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

    private void Start()
    {
		//Observer.Instance.AddObserver(EventName.EnterCell, EnterCell);
		//Observer.Instance.AddObserver(EventName.BeginChain, BeginChain);
		//Observer.Instance.AddObserver(EventName.ResetChain, ResetChain);
	}
    public virtual void ResetChain(object data)
	{
	}

	public void BeginChain(object startingCell)
	{
		Cell startCell = ((Cell)startingCell);
		ChainItemId = startCell.Item.id;
		AddCell(startCell, true);
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
		Cells.Add(cell);
	}

	protected void RemoveCell(Cell cell)
	{
		Cells.Remove(cell);
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
		return ChainItemId;
	}

	public virtual void EnterCell(object cell)
	{

	}


	public void ExitCell(Cell cell)
	{
	}

	public void OverCell(Cell cell)
	{
	}
}
