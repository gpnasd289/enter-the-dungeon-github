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
        public int ID;

        public ComboItems.CellItems CellItem;

        public int MAXAmount;
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
	private Transform itemsGroup;

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

	public Cell LastTouchedCell { get; private set; }

	public int CurrentCollectionSize => 0;

	public bool Stacking { get; private set; }

	public bool Shuffling { get; private set; }
	public List<GameObject> listItemPrefab;
	public Cell[,] cellArr;
	public CellItem[,] cellItemArr;
	public List<Cell> cellChoseList;
	public int idChose;

    private void Awake()
    {
		cellChoseList = new List<Cell>();
		cellArr = new Cell[cellSize.x, cellSize.y];
		cellItemArr = new CellItem[cellSize.x, cellSize.y];
		Width = cellSize.x;
		Height = cellSize.y;
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
					GameObject itemPrefab = listItemPrefab[UnityEngine.Random.Range(0, listItemPrefab.Count)];
					GameObject spawnItem = Instantiate(itemPrefab, cellPosition, Quaternion.identity, itemsGroup);
					cell.SetItem(spawnItem.GetComponent<CellItem>());
					cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
                    spawnItem.GetComponent<CellItem>().Placement = new Vector2Int(x, y);
                    spawnItem.name = $"Item {x} {y}";
				}
				else
                {
					// Instantiate item drop
					GameObject itemPrefab = listItemPrefab[UnityEngine.Random.Range(0, listItemPrefab.Count)];
					GameObject spawnItem = Instantiate(itemPrefab, cellPosition, Quaternion.identity, itemsGroup);
					cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
					spawnItem.GetComponent<CellItem>().Placement = new Vector2Int(x, y);
					spawnItem.name = $"Item {x} {y}";
				}
			}
        }
    }
	public void CreateItem(int x, int y)
    {
		// Instantiate item drop
		Vector3 cellPosition = new Vector3(x * cellPrefab.transform.localScale.x, y * cellPrefab.transform.localScale.y);
		GameObject itemPrefab = listItemPrefab[UnityEngine.Random.Range(0, listItemPrefab.Count)];
		GameObject spawnItem = Instantiate(itemPrefab, cellPosition, Quaternion.identity, itemsGroup);
		cellItemArr[x, y] = spawnItem.GetComponent<CellItem>();
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
	
}
