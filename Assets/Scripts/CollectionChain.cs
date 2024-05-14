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
		Observer.Instance.AddObserver(EventName.EnterCell, EnterCell);
		Observer.Instance.AddObserver(EventName.BeginChain, BeginChain);
		Observer.Instance.AddObserver(EventName.ResetChain, ResetChain);
		Observer.Instance.AddObserver(EventName.AddToChain, AddToChain);
		Observer.Instance.AddObserver(EventName.RemoveFromChain, RemoveFromChain);

	}
	public virtual void ResetChain(object data)
	{
		Line.positionCount = 1;
		
	}

	public void BeginChain(object startingCell)
	{
		Cell startCell = (Cell)startingCell;
		Line.SetPosition(0, startCell.transform.position);
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
		//Cells.Add(cell);
	}

	protected void RemoveCell(Cell cell)
	{
		//Cells.Remove(cell);
	}
	public void AddToChain(object cell)
    {
		Cell cellAdd = (Cell)cell;
		Line.positionCount++;
		Line.SetPosition(Line.positionCount - 1, cellAdd.transform.position);
	}
	public void RemoveFromChain(object cell)
    {
		Cell cellRemove = (Cell)cell;
		Line.positionCount--;
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
	public void FillCell()
    {
		
    }
}
