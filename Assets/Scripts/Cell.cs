using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuangDM.Common;

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

	public Vector2 cellId;

	public bool isTracing;
	public void Initialize(CellField field, Vector2Int placement)
	{
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

	public void Drop()
	{
	}

	public void Collapse(float flyDelay = 0f, bool finisher = false, Action onComplete = null)
	{
	}

	public void PopItem()
	{
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
		if (((cell.cellId.x == cellId.x + 1) || (cell.cellId.x == cellId.x - 1) || (cell.cellId.x == cellId.x))
            && ((cell.cellId.y == cellId.y + 1) || (cell.cellId.y == cellId.y - 1) || (cell.cellId.y == cellId.y)))
        {
			return true;
        }
		return false;
	}
	public bool CheckUpLeft(Cell cell)
    {
		if ((cell.cellId.x == cellId.x - 1) && (cell.cellId.y == cellId.y + 1))
        {
			return true;
        }
		return false;
    }
	public bool CheckUpMid(Cell cell)
    {
		if ((cell.cellId.x == cellId.x) && (cell.cellId.y == cellId.y + 1))
		{
			return true;
		}
		return false;
	}
	public bool CheckUpRight(Cell cell)
	{
		if ((cell.cellId.x == cellId.x + 1) && (cell.cellId.y == cellId.y + 1))
		{
			return true;
		}
		return false;
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
			CellField.instance.idChose = Item.id;
			CellField.instance.cellChoseList.Add(gameObject.GetComponent<Cell>());
			Observer.Instance.Notify(EventName.BeginChain, this);
			for (int i = 0; i < CellField.instance.cellArr.GetLength(0); i++)
			{
				for (int j = 0; j < CellField.instance.cellArr.GetLength(1); j++)
				{
					if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id == CellField.instance.idChose)
					{
						CellField.instance.cellArr[i, j].Item.Highlight();
					}
					else if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id != CellField.instance.idChose)
					{
						CellField.instance.cellArr[i, j].Item.SetGrayedOut();
					}
				}
			}
		}
	}

    protected override void MouseUp()
    {
		for (int i = 0; i < CellField.instance.cellArr.GetLength(0); i++)
		{
			for (int j = 0; j < CellField.instance.cellArr.GetLength(1); j++)
			{
				if (CellField.instance.cellArr[i, j].Item != null)
                {
					CellField.instance.cellArr[i, j].Item.ResetToDefaultLooks();
				}
			}
		}
		if (CellField.instance.cellChoseList.Count >= 2)
		{
			for (int i = 0; i < CellField.instance.cellChoseList.Count; i++)
			{
				if (CellField.instance.cellChoseList[i].Item != null)
				{
					CellField.instance.cellChoseList[i].Item.Disappear();
					CellField.instance.cellChoseList[i].Item = null;
				}
			}
            CellField.instance.cellChoseList.Clear();
			Observer.Instance.Notify(EventName.ResetChain);
			Debug.Log("resetchain");
        }
        else
        {
			CellField.instance.cellChoseList.Clear();
		}
	}

    protected override void MouseEnter()
    {
		if (isMouseHold && Item != null)
        {
			if (CellField.instance.cellChoseList.Count == 0)
			{
				CellField.instance.idChose = Item.id;
				CellField.instance.cellChoseList.Add(gameObject.GetComponent<Cell>());
				Observer.Instance.Notify(EventName.AddToChain, this);
				for (int i = 0; i < CellField.instance.cellArr.GetLength(0); i++)
				{
					for (int j = 0; j < CellField.instance.cellArr.GetLength(1); j++)
					{
						if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id == CellField.instance.idChose)
						{
							CellField.instance.cellArr[i, j].Item.Highlight();
						}
						else if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id != CellField.instance.idChose)
						{
							CellField.instance.cellArr[i, j].Item.SetGrayedOut();
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < CellField.instance.cellArr.GetLength(0); i++)
				{
					for (int j = 0; j < CellField.instance.cellArr.GetLength(1); j++)
					{
						if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id == CellField.instance.idChose)
						{
							CellField.instance.cellArr[i, j].Item.Highlight();
						}
						else if (CellField.instance.cellArr[i, j].Item != null && CellField.instance.cellArr[i, j].Item.id != CellField.instance.idChose)
						{
							CellField.instance.cellArr[i, j].Item.SetGrayedOut();
						}
					}
				}
				if (Item.id == CellField.instance.idChose && !CellField.instance.cellChoseList.Contains(this) && IsNeighbour(CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 1]))
				{
					CellField.instance.cellChoseList.Add(gameObject.GetComponent<Cell>());
					Observer.Instance.Notify(EventName.AddToChain, this);
				}
				else if (Item.id == CellField.instance.idChose && CellField.instance.cellChoseList.Count > 1 && CellField.instance.cellChoseList.Contains(this) && IsNeighbour(CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 1]))
                {
					if (CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 2] == this)
					{
						CellField.instance.cellChoseList.Remove(CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 1]);
						CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 1].Item = null;
						Observer.Instance.Notify(EventName.RemoveFromChain, CellField.instance.cellChoseList[CellField.instance.cellChoseList.Count - 1]);
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
}
