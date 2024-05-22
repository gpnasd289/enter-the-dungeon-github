using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuangDM.Common;
using DG.Tweening;

public class Cell : NonUIObject
{
	public bool Breakable;

	public int Health;

	public bool Empty;

	//[SerializeField]
	//private float EnhancedRadius;

	//[SerializeField]
	//private float DefaultRadius;

	public SphereCollider Collider;

	public Transform Shape;

	public SpriteRenderer Background;

	//public SpriteRenderer BackgroundExtraObject;

	//public Transform DebugShape;

	//public TextMesh DebugPlacementText;

	//public ParticleSystem CollapseCellEffect;

	//public ParticleSystem CollapseCellFinisherEffect;

	//public ParticleSystem DestructionCellEffect;

	//public ParticleSystem DamageCellEffect;

	//public bool DebugInfo;

	public Color GizmoColorVacant;

	public Color GizmoColorOccupied;

	private Vector3 OriginalPosition;

	private CellField Field;

	public Vector3 PunchPositionVector;

	public Vector2 PunchDuration;

	public Vector2 Vibrato;

	public float Elasticity;

	//public Ease Ease;

	public Vector2Int Placement { get; private set; }

	[SerializeField]
	public CellItem Item { get; private set; }

	public bool Stacking { get; private set; }

	public bool Blocking { get; set; }

	//public bool IsActionCell { get; set; }

	public bool Live => false;

	public bool Vacant => false;

	public bool Collectible => false;

	public void Initialize(CellField field, Vector2Int placement)
	{
		this.Field = field;
		this.Placement = placement;
	}

	public void SetItem(CellItem item)
	{
		Item = item;
	}

	public void SnapItem()
	{
	}

	public void ForceSetItem(CellItem item)
	{
	}

	public void DropItem(CellItem newItem)
	{
		SetItem(newItem);
		newItem.transform.DOMove(this.transform.position, 0.5f);
	}
	private bool IsValidDropTarget(Cell targetCell)
	{
		return targetCell.Item == null;
	}
	public void ClearItem()
    {
		Item = null;
    }
	public void Collapse(float flyDelay = 0f, bool finisher = false, Action onComplete = null)
	{
	}

	public void PopItem(int multiply)
	{
		GameObject spawnItem = Instantiate(Field.cellSPItemPrefab, this.transform.position, Quaternion.identity, Field.itemsGroup);
		Field.cellItemArr[Placement.x, Placement.y] = spawnItem.GetComponent<CellItem>();
		this.Item = Field.cellItemArr[Placement.x, Placement.y];
		Item.Placement = new Vector2Int(Placement.x, Placement.y);
		Item.name = $"Item SP {Placement.x} {Placement.y}";
		Item.PopItem(multiply);
	}

	public void SetActive(bool doSet)
	{
	}

	public Vector3 GetItemPosition()
	{
		return default(Vector3);
	}

	private void DamageCell(int damage = 1)
	{
	}

	private void DamageSurroundingCells()
	{
	}

	/*public Cell GetNeighbourCell(FieldDirection direction)
	{
		return null;
	}*/

	/*public bool TryGetAnyNeighbour4D(out Cell neighbour)
	{
		neighbour = null;
		return false;
	}*/

	/*public Cell GetNeighbourCell(FourDirections direction)
	{
		return null;
	}*/

	public bool IsNeighbour(Cell cell)
	{
		return (Mathf.Abs(cell.Placement.x - this.Placement.x) <= 1 && Mathf.Abs(cell.Placement.y - this.Placement.y) <= 1);
	}
	public void EnhanceCollider()
	{
	}

	public void SetColliderDefault()
	{
	}

	/*private Cell GetFurthestAccessibleVacant(FieldDirection direction)
	{
		return null;
	}*/

	private bool Available(Cell cell)
	{
		return false;
	}

	private void PuffEffect(bool finisher)
	{
	}
    protected override void MouseDown()
    {
		if (Item != null)
        {
			Debug.Log(Item.name);
			Field.idChose = Item.id;
			Field.cellChoseList.Add(this);
			Observer.Instance.Notify(EventName.BeginChain, this);
			UpdateCellHighlights();
		}
	}

    protected override void MouseUp()
    {
		if (Item != null)
        {
			Debug.Log("mouse up");
			ResetAllCellHighlights();
			if (Field.cellChoseList.Count >= 2 && Field.cellChoseList.Count < 4)
			{
				Observer.Instance.Notify(EventName.MatchChain);
				Field.LastTouchedCell = Field.cellChoseList[^1];
				for (int i = 0; i < Field.cellChoseList.Count; i++)
				{
					if (Field.cellChoseList[i].Item != null)
					{
						Field.cellChoseList[i].Item.Disappear();
						Field.cellChoseList[i].Item = null;
					}
				}
				Field.cellChoseList.Clear();
				Observer.Instance.Notify(EventName.ResetChain);
				Debug.Log("resetchain");
			}
			else if (Field.cellChoseList.Count >= 4)
            {
				Observer.Instance.Notify(EventName.MatchChain, Field.cellChoseList.Count);
				Field.LastTouchedCell = Field.cellChoseList[^1];
				for (int i = 0; i < Field.cellChoseList.Count - 1; i++)
				{
					if (Field.cellChoseList[i].Item != null)
					{
						Field.cellChoseList[i].Item.Disappear();
						Field.cellChoseList[i].Item = null;
					}
				}
				Field.cellChoseList.Clear();
				Observer.Instance.Notify(EventName.ResetChain);
				Debug.Log("resetchain");
			}
			else
			{
				Field.cellChoseList[0] = null;
				Field.cellChoseList.Clear();
			}
		}
	}

    protected override void MouseEnter()
    {
		if (isMouseHold && Item != null)
        {
            /*if (Field.cellChoseList.Count == 0)
            {
				Debug.Log(Item.name);
				Field.idChose = Item.id;
                Field.cellChoseList.Add(this);
				UpdateCellHighlights();
				Observer.Instance.Notify(EventName.BeginChain, this);
            }*/
            if (Field.cellChoseList.Count > 0)
			{
				UpdateCellHighlights();
				if (Item.id == Field.idChose && !Field.cellChoseList.Contains(this) && IsNeighbour(Field.cellChoseList[^1])) // ^1 == list[listcount - 1]
				{
					Field.cellChoseList.Add(this);
					Debug.Log("cell add: " + Field.cellChoseList[^1].name);
					Observer.Instance.Notify(EventName.AddToChain, this);
				}
				else if (Item.isSpecial && !Field.isContainSpecial && IsNeighbour(Field.cellChoseList[^1]))
                {
					Field.cellChoseList.Add(this);
					Field.isContainSpecial = true;
					Debug.Log("cell add: " + Field.cellChoseList[^1].name);
					Observer.Instance.Notify(EventName.AddToChain, this);
				}
				else if (Item.id == Field.idChose && Field.cellChoseList.Count > 1 && Field.cellChoseList.Contains(this) && IsNeighbour(Field.cellChoseList[^1]))
                {
					if (Field.cellChoseList[^2] == this)
					{
						Debug.Log("cell remove: " + Field.cellChoseList[^1].name);
						Field.cellChoseList.Remove(Field.cellChoseList[^1]);
						//Field.cellChoseList[^1] = null;
						
						Observer.Instance.Notify(EventName.RemoveFromChain, Field.cellChoseList[^1]);
					}
				}
				else if (Item.isSpecial && Field.cellChoseList.Count > 1 && Field.cellChoseList.Contains(this) && IsNeighbour(Field.cellChoseList[^1]))
				{
					if (Field.cellChoseList[^2] == this)
					{
						Debug.Log("cell remove: " + Field.cellChoseList[^1].name);
						Field.cellChoseList.Remove(Field.cellChoseList[^1]);
						//Field.cellChoseList[^1] = null;
						Field.isContainSpecial = false;
						Observer.Instance.Notify(EventName.RemoveFromChain, Field.cellChoseList[^1]);
					}
				}
			}
		}
		
    }

    protected override void MouseExit()
    {
    }

	protected override void MouseOver()
	{
		
	}

    protected override void Construct()
    {
    }

    private void OnDrawGizmos()
    {
    }

    public override string ToString()
    {
        return null;
    }
	private void UpdateCellHighlights()
	{
		for (int i = 0; i < Field.cellArr.GetLength(0); i++)
		{
			for (int j = 0; j < Field.cellArr.GetLength(0); j++)
			{
				if (Field.cellArr[i, j].Item != null)
				{
					if (Field.cellArr[i, j].Item.id == Field.idChose || Field.cellArr[i,j].Item.isSpecial)
					{
						Field.cellArr[i, j].Item.Highlight();
					}
					else
					{
						Field.cellArr[i, j].Item.SetGrayedOut();
					}
				}
			}
		}
	}
	private void ResetAllCellHighlights()
	{
		for (int i = 0; i < Field.cellArr.GetLength(0); i++)
		{
			for (int j = 0; j < Field.cellArr.GetLength(0); j++)
			{
				if (Field.cellArr[i, j].Item != null)
				{
					Field.cellArr[i, j].Item.ResetToDefaultLooks();
				}
			}
		}
	}
	[Button]
	public void DebugNameAndItem()
    {
		Debug.Log(this.name);
		Debug.Log(this.Item.name);
    }
	
}
