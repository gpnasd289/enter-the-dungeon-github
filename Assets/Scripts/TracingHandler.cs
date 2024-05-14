using DG.Tweening;
using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingHandler : MonoBehaviour
{
    public CellField CellField;

    private int MinimumChainLength = 2;

    private bool CollectionActive;

    [SerializeField]
    private CollectionChain CollectionChain;

    public int CollectionSize => 0;

    //public CollectionChain Chain => null;

    public int count = 0;
    public bool BeingProcessed { get; }
    // Start is called before the first frame update
    void Start()
    {
        Observer.Instance.AddObserver(EventName.ResetChain, ResetChain);
    }

    private void ResetChain(object cell)
    {
        DropItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropItem()
    {
        
        for (int x = 0; x < CellField.cellArr.GetLength(0); x++)
        {
            for (int y = 0; y < CellField.cellArr.GetLength(1); y++)
            {
                if (CellField.cellArr[x,y].Item == null)
                {
                    
                    if (CellField.cellArr[x, y + 1].Item != null)
                    {
                        CellField.cellArr[x, y + 1].SetItem(CellField.cellArr[x, y].Item);
                        CellField.cellArr[x, y + 1].Item.transform.DOMove(CellField.cellArr[x, y + 1].transform.position, 0.5f);
                        DropItem();
                    }
                    else if (CellField.cellArr[x + 1, y + 1].Item != null)
                    {
                        CellField.cellArr[x + 1, y + 1].SetItem(CellField.cellArr[x, y].Item);
                        CellField.cellArr[x + 1, y + 1].Item.transform.DOMove(CellField.cellArr[x + 1, y + 1].transform.position, 0.5f);
                        DropItem();
                    }
                    else if (CellField.cellArr[x - 1, y + 1].Item != null)
                    {
                        CellField.cellArr[x - 1, y + 1].SetItem(CellField.cellArr[x, y].Item);
                        CellField.cellArr[x - 1, y + 1].Item.transform.DOMove(CellField.cellArr[x - 1, y + 1].transform.position, 0.5f);
                        DropItem();
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
