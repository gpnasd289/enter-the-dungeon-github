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
        Observer.Instance.AddObserver(EventName.MatchChain, MatchChain);
    }

    private void MatchChain(object cell)
    {
        HandleMatch(CellField.cellChoseList);
        
    }

    private void ResetChain(object cell)
    {
        //DropBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleMatch(List<Cell> matchedCells)
    {
        float flyDelay = 0f;
        foreach (Cell cell in matchedCells)
        {
            cell.Item.FlyToPlayer(flyDelay, DropBoard);
            cell.ClearItem();
            flyDelay += 0.1f;  // Add delay between each fly animation
        }
    }
    public void DropBoard()
    {
        for (int y = 1; y < CellField.Height; y++)
        {
            for (int x = 0; x < CellField.Width; x++)       
            {
                if (y < CellField.Width)
                {
                    if (CellField.cellArr[x, y].Item != null)
                    {
                        if (x == 0)
                        {
                            if (CellField.cellArr[x, y - 1].Item == null)
                            {
                                CellField.cellArr[x, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y - 1].Item == null && CellField.cellArr[x + 1, y].Item == null)
                            {
                                CellField.cellArr[x + 1, y - 1].DropItem(CellField.cellArr[x, y].Item);

                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                        else if (x > 0 && x < CellField.Width - 1)
                        {
                            if (CellField.cellArr[x, y - 1].Item == null)
                            {
                                CellField.cellArr[x, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y - 1].Item == null && CellField.cellArr[x - 1, y].Item == null)
                            {
                                CellField.cellArr[x - 1, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y - 1].Item == null && CellField.cellArr[x + 1, y].Item == null)
                            {
                                CellField.cellArr[x + 1, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                        else
                        {
                            if (CellField.cellArr[x, y - 1].Item == null)
                            {
                                CellField.cellArr[x, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y - 1].Item == null && CellField.cellArr[x - 1, y].Item == null)
                            {
                                CellField.cellArr[x - 1, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                    }
                }
                if (y == (CellField.Width - 1))
                {
                    if (CellField.cellArr[x, y].Item == null)
                    {
                        CellField.cellArr[x, y].DropItem(CellField.cellItemArr[x, y + 1], () => CellField.CreateItem(x,y));
                    }
                }
            }
        }

        //fill ô hàng tren cùng

        //if (CheckBoard() == false) DropBoard();
    }

    public bool CheckBoard()
    {
        for ( int y= 0; y<CellField.Height; y++) 
            for (int x =0; x<CellField.Width; x++)
                if (CellField.cellArr[x, y].Item == null)
                {
                    return false;
                }
        return true;
    }
}
