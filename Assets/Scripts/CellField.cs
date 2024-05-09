using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellField : MonoBehaviour
{
	/*[Serializable]
	public class Food
	{
		public int ID;

		public ComboItems.CellItems CellItem;

		public int MAXAmount;
	}*/

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

	//private IFieldHandler FieldHandler;

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
                GameObject spawnCell = Instantiate(cellPrefab.gameObject, new Vector3(y, x), Quaternion.identity, cellsGroup);
				GameObject spawnItem = Instantiate(listItemPrefab[Random.Range(0, 5)], new Vector3(y, x), Quaternion.identity, itemsGroup);
				spawnCell.GetComponent<Cell>().SetItem(spawnItem.GetComponent<CellItem>());
                spawnCell.name = $"Cell {x} {y}";
				spawnItem.name = $"Item {x} {y}";
			}
        }
    }
}
