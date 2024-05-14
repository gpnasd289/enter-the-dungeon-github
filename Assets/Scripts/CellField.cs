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
	private Vector2 cellSize;

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
	public Cell[,] cellArr = new Cell[6,6];
	public List<Cell> cellChoseList;
	public int idChose;
	public static CellField instance;

    private void Awake()
    {
		instance = this;
		cellChoseList = new List<Cell>();
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
                GameObject spawnCell = Instantiate(cellPrefab.gameObject, new Vector3(x * cellPrefab.transform.localScale.y, y * cellPrefab.transform.localScale.x), Quaternion.identity, cellsGroup);
				GameObject spawnItem = Instantiate(listItemPrefab[UnityEngine.Random.Range(0, 5)], new Vector3(x * cellPrefab.transform.localScale.y, y * cellPrefab.transform.localScale.x), Quaternion.identity, itemsGroup);
				spawnCell.GetComponent<Cell>().SetItem(spawnItem.GetComponent<CellItem>());
				spawnCell.GetComponent<Cell>().cellId.x = x;
				spawnCell.GetComponent<Cell>().cellId.y = y;
				cellArr[x, y] = spawnCell.GetComponent<Cell>();
				spawnCell.name = $"Cell {x} {y}";
				spawnItem.name = $"Item {x} {y}";
			}
        }
    }
}
