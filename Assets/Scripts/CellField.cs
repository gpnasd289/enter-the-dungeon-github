using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellField : GOManager
{
    [Serializable]
    public class Element
    {
        public ComboItems.CellItems CellItem;

		public GameObject Prefab;

		[Range(0f, 100f)] public float Chance = 100f;

		[HideInInspector] public double _weight;
    }

    public bool FlowToTheDeepestAdjacentCell;

	public float ItemStepDuration;

	public float ItemStepArching;

	[SerializeField]
	private float ShuffleMoveDuration;

	[SerializeField]
	private Vector2Int cellSize;

	//public WeaponBar WeaponProgressBar;

	//public WidgetWeaponActivator WeaponActivator;

	public RectTransform WeaponHolderRect;

	/*[SerializeField]
	private Image GaugeSmall;*/

	/*[SerializeField]
	private Transform ActionCellsGroup;*/

	[SerializeField]
	private Cell cellPrefab;

	[SerializeField]
	private Transform cellsGroup;

	[SerializeField]
	public Transform itemsGroup;

	[SerializeField]
	private Transform Mask;

	[SerializeField]
	private SpriteRenderer BackgroundRenderer;

	public Transform FxGroup;

	private readonly List<Cell> Cells;

	private Vector2 BackgroundUnitSize;

	//private List<ECell> FillPattern;

	private int FillPatternRowIndex;

	private CellItem[] FillPatternItemPrefabs;

	//private Food[] ItemsAmountLimits;

	private IFieldHandler FieldHandler;

	public int Width { get; private set; }

	public int Height { get; private set; }

	public bool Interactive { get; set; }

	public int DestroyedBlocksAmount { get; set; }

	public Cell[] ConstraintCells { get; private set; }

	public Cell LastTouchedCell { get; set; }

	public int CurrentCollectionSize => 0;

	public bool Stacking { get; private set; }

	public bool Shuffling { get; private set; }
	public Cell[,] cellArr;
	public CellItem[,] cellItemArr;
	public List<Element> listElement;
	[SerializeField]
	public List<Cell> cellChoseList;
	public int idChose;

	private double accumulateWeights;
	private System.Random rand = new System.Random();
	public GameObject cellSPItemPrefab;
	public bool isContainSpecial;
	private void Awake()
    {
		cellChoseList = new List<Cell>();
		cellArr = new Cell[cellSize.x, cellSize.y];
		cellItemArr = new CellItem[cellSize.x, cellSize.y];
		Width = cellSize.x;
		Height = cellSize.y;
		CalcWeights();
	}
    // Start is called before the first frame update
    void Start()
    {
        GenerateField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateField()
    {
        for (int x = 0; x < cellSize.x; x++)
        {
            for (int y = 0; y < cellSize.y; y++)
            {
				// Instantiate cell
				Vector3 cellPosition = new Vector3(x * cellPrefab.transform.localScale.x, y * cellPrefab.transform.localScale.y);
				if (y < cellSize.x)
                {
					Cell cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, cellsGroup).GetComponent<Cell>();
					cell.Initialize(this, new Vector2Int(x, y));
					cellArr[x, y] = cell;
					cell.name = $"Cell {x} {y}";

					// Instantiate item
					Element itemPrefab = listElement[GetRandomElementIndex()];
					GameObject spawnItem = Instantiate(itemPrefab.Prefab, cellPosition, Quaternion.identity, itemsGroup);
					cell.SetItem(spawnItem.GetComponent<CellItem>());
					cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
                    spawnItem.GetComponent<CellItem>().Placement = new Vector2Int(x, y);
                    spawnItem.name = $"Item {x} {y}";
					Debug.Log("element: " + itemPrefab.CellItem + " chance: " + itemPrefab.Chance + "%");
				}
				else
                {
					// Instantiate item drop
					Element itemPrefab = listElement[GetRandomElementIndex()];
					GameObject spawnItem = Instantiate(itemPrefab.Prefab, cellPosition, Quaternion.identity, itemsGroup);
					cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
					spawnItem.GetComponent<CellItem>().Placement = new Vector2Int(x, y);
					spawnItem.GetComponent<CellItem>().SetGrayedOut();
					spawnItem.name = $"Item {x} {y}";
					Debug.Log("element: " + itemPrefab.CellItem + " chance: " + itemPrefab.Chance + "%");
				}
			}
        }
    }
	public void CreateItem(int x, int y)
    {
		// Instantiate item drop
		Vector3 cellPosition = new Vector3(x * cellPrefab.transform.localScale.x, y * cellPrefab.transform.localScale.y);
		Element itemPrefab = listElement[GetRandomElementIndex()];
		GameObject spawnItem = Instantiate(itemPrefab.Prefab, cellPosition, Quaternion.identity, itemsGroup);
		cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
		spawnItem.GetComponent<CellItem>().SetGrayedOut();
		spawnItem.name = $"ItemAdd {x} {y}";
	}
	public bool IsValidPosition(Vector2Int position)
	{
		return position.x >= 0 && position.x < cellSize.x && position.y >= 0 && position.y < cellSize.y;
	}
	public Cell GetCellAt(Vector2Int position)
	{
		if (IsValidPosition(position))
		{
			return cellArr[position.x, position.y];
		}
		return null;
	}
	public void ResetAllHighlightAndCol()
    {
		for (int i = 0; i < cellArr.GetLength(0); i++)
		{
			for (int j = 0; j < cellArr.GetLength(0); j++)
			{
				if (cellArr[i, j].Item != null)
				{
					cellArr[i, j].Item.ResetToDefaultLooks();
					cellArr[i, j].Collider.enabled = true;
				}
			}
		}
	}
	public void UpdateAllHighlightAndCol()
    {
        for (int i = 0; i < cellArr.GetLength(0); i++)
        {
            for (int j = 0; j < cellArr.GetLength(0); j++)
            {
                if (cellArr[i, j].Item != null)
                {
					cellArr[i, j].Item.SetGrayedOut();
					cellArr[i, j].Collider.enabled = false;
				}
            }
        }
    }
	public void UpdateChoseCellHighlightAndCol()
    {
		for (int i = 0; i < cellArr.GetLength(0); i++)
		{
			for (int j = 0; j < cellArr.GetLength(0); j++)
			{
				if (cellArr[i, j].Item != null)
				{
					if (cellArr[i, j].Item.id == idChose || cellArr[i, j].Item.isSpecial)
					{
						cellArr[i, j].Item.Highlight();
						cellArr[i, j].Collider.enabled = true;
					}
					else
					{
						cellArr[i, j].Item.SetGrayedOut();
						cellArr[i, j].Collider.enabled = false;
					}
				}
			}
		}
	}
	private void CalcWeights()
    {
		accumulateWeights = 0f;
		foreach(Element ele in listElement)
        {
			accumulateWeights += ele.Chance;
			ele._weight = accumulateWeights;
        }
    }
	public bool CheckSpecialContain()
    {
		foreach (Cell c in cellChoseList)
        {
			if (c.Item.isSpecial)
            {
				return true;
            }
			return false;
        }
		return false;
	}
	private int GetRandomElementIndex()
    {
		double r = rand.NextDouble() * accumulateWeights;
		for (int i = 0; i < listElement.Count; i++)
        {
			if (listElement[i]._weight >= r)
			{
				return i;
			}
		}
		return 0;
    }
}
