using DG.Tweening;
using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingHandler : MonoBehaviour
{
    public CellField CellField;

    //private int MinimumChainLength = 2;

    private bool CollectionActive;

    [SerializeField]
    private CollectionChain CollectionChain;

    public int CollectionSize => 0;

    //public CollectionChain Chain => null;

    public int count = 0;
    public bool BeingProcessed { get; }
    public List<Cell> matchedList = new List<Cell>(); 
    // Start is called before the first frame update
    void Start()
    {
        Observer.Instance.AddObserver(EventName.ResetChain, ResetChain);
        Observer.Instance.AddObserver(EventName.MatchChain, MatchChain);
        //Observer.Instance.AddObserver("CheckDropBoard", CheckDropBoard);
    }

    private void MatchChain(object cell)
    {
        //List<Cell> temp = (List<Cell>)cell;
        matchedList = new List<Cell>(); 
        foreach (Cell c in CellField.cellChoseList)
        {
            matchedList.Add(c);
        }
        HandleMatch(matchedList);
        count = matchedList.Count;
    }

    private void ResetChain(object cell)
    {
        CellField.isContainSpecial = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleMatch(List<Cell> matchedCells)
    {
        float flyDelay = 0f;
        for (int i = 0; i < matchedCells.Count; i++)
        {
            int async = i;
            matchedCells[async].Collider.enabled = false;
            if (i < matchedCells.Count - 1 && matchedCells.Count < 4)
            {
                matchedCells[async].Item.FlyToPlayer(flyDelay, DropBoard);
                matchedCells[async].ClearItem();
                flyDelay += 0.1f;
            }
            else if (i == matchedCells.Count - 1 && matchedCells.Count < 4)
            {
                matchedCells[async].Item.FlyToPlayer(flyDelay, () => {
                    Observer.Instance.Notify(EventName.DoAnim, matchedCells);
                    matchedCells.Clear();
                    DropBoard();
                });
                matchedCells[async].ClearItem();
                flyDelay += 0.1f;
            }
            else if (i < matchedCells.Count - 1 && matchedCells.Count >= 4)
            {
                matchedCells[async].Item.FlyToPlayer(flyDelay, DropBoard);
                matchedCells[async].ClearItem();
                flyDelay += 0.1f;
            }
            else if (i == matchedCells.Count - 1 && matchedCells.Count >= 4)
            {
                matchedCells[async].Item.FlyToPlayer(flyDelay, () => {
                    Observer.Instance.Notify(EventName.DoAnim, matchedCells);
                    matchedCells.Clear();
                    DropBoard();
                });
                matchedCells[async].PopItem(matchedCells.Count); // sua thanh bay xong pop 
                flyDelay += 0.1f;
                Debug.Log("last cell: " + matchedCells[async].name + " contain: " + matchedCells[async].Item.name);
            }
        }
        /*if (CellField.isContainSpecial)
        {
            for (int i = 0; i < matchedCells.Count; i++)
            {
                if (matchedCells[i].Item.isSpecial)
                {
                    CombatManager.Instance.multipleDmg = matchedCells[i].Item.multiply;
                }
            }
        }*/
    }
    public void DropBoard()
    {
        /*for (int x = 1; x < CellField.Height; x++)
        {
            for (int y = 0; y < CellField.Width; y++)
            {
                if (x < (CellField.Height - 1))
                {
                    Debug.Log("loop x = " + x);
                    if (CellField.cellArr[x, y].Item != null)//
                    {
                        if (y == 0)
                        {
                            if (CellField.cellArr[x - 1, y].Item == null && CellField.cellArr[x - 1, y].Breakable && CellField.cellArr[x - 1, y].Health == 0)
                            {
                                CellField.cellArr[x - 1, y].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y + 1].Item == null && CellField.cellArr[x, y + 1].Item == null
                                    && CellField.cellArr[x - 1, y + 1].Breakable && CellField.cellArr[x - 1, y + 1].Health == 0
                                    && CellField.cellArr[x, y + 1].Breakable && CellField.cellArr[x, y + 1].Health == 0)
                            {
                                CellField.cellArr[x - 1, y + 1].DropItem(CellField.cellArr[x, y].Item);

                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                        else if (y > 0 && y < CellField.Width - 1)
                        {
                            if (CellField.cellArr[x - 1, y].Item == null && CellField.cellArr[x - 1, y].Breakable && CellField.cellArr[x - 1, y].Health == 0)
                            {
                                CellField.cellArr[x - 1, y].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y - 1].Item == null && CellField.cellArr[x, y - 1].Item == null
                                    && CellField.cellArr[x - 1, y - 1].Breakable && CellField.cellArr[x - 1, y - 1].Health == 0
                                    && CellField.cellArr[x, y - 1].Breakable && CellField.cellArr[x, y - 1].Health == 0)
                            {
                                CellField.cellArr[x - 1, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y + 1].Item == null && CellField.cellArr[x, y + 1].Item == null
                                    && CellField.cellArr[x - 1, y + 1].Breakable && CellField.cellArr[x - 1, y + 1].Health == 0
                                    && CellField.cellArr[x, y + 1].Breakable && CellField.cellArr[x, y + 1].Health == 0)
                            {
                                CellField.cellArr[x - 1, y + 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                        else
                        {
                            if (CellField.cellArr[x - 1, y].Item == null && CellField.cellArr[x - 1, y].Breakable && CellField.cellArr[x - 1, y].Health == 0)
                            {
                                CellField.cellArr[x - 1, y].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                            else if (CellField.cellArr[x - 1, y - 1].Item == null && CellField.cellArr[x, y - 1].Item == null
                                    && CellField.cellArr[x - 1, y - 1].Breakable && CellField.cellArr[x - 1, y - 1].Health == 0
                                    && CellField.cellArr[x, y - 1].Breakable && CellField.cellArr[x, y - 1].Health == 0)
                            {
                                CellField.cellArr[x - 1, y - 1].DropItem(CellField.cellArr[x, y].Item);
                                CellField.cellArr[x, y].ClearItem();
                            }
                        }
                    }
                }
                if (x == (CellField.Height - 1))
                {
                    if (CellField.cellArr[x, y].Item == null)
                    {
                        CellField.cellArr[x, y].DropItem(CellField.cellItemArr[x + 1, y]);
                        CellField.cellItemArr[x, y] = CellField.cellItemArr[x + 1, y];
                        CellField.cellItemArr[x + 1, y] = null;
                        CellField.CreateItem(x + 1, y);
                        Debug.Log("new item" + (x + 1) + "/" + (y));
                    }
                }
            }
        }*/



        for (int x = 0; x < CellField.Height; x++)
        {
            for (int y = 0; y < CellField.Width; y++)
            {
                if (x < (CellField.Height - 2))
                {
                    if (CellField.cellArr[x, y].Item == null && CellField.cellArr[x, y].cellStt == CellStatus.Available)
                    {
                        if (y == 0)
                        {
                            if (CellField.cellArr[x + 1, y].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y].Item);
                                CellField.cellArr[x + 1, y].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y + 1].Item != null && CellField.cellArr[x, y + 1].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y + 1].Item);
                                CellField.cellArr[x + 1, y + 1].ClearItem();
                            }
                        }
                        else if (y > 0 && y < CellField.Width - 1)
                        {
                            Debug.Log("right case");
                            if (CellField.cellArr[x + 1, y].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y].Item);
                                CellField.cellArr[x + 1, y].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y + 1].Item != null && CellField.cellArr[x, y + 1].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y + 1].Item);
                                CellField.cellArr[x + 1, y + 1].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y - 1].Item != null && CellField.cellArr[x, y - 1].Item != null)
                            {
                                Debug.Log("right case 2");
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y - 1].Item);
                                CellField.cellArr[x + 1, y - 1].ClearItem();
                            }
                        }
                        else
                        {
                            if (CellField.cellArr[x + 1, y].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y].Item);
                                CellField.cellArr[x + 1, y].ClearItem();
                            }
                            else if (CellField.cellArr[x + 1, y - 1].Item != null && CellField.cellArr[x, y - 1].Item != null)
                            {
                                CellField.cellArr[x, y].DropItem(CellField.cellArr[x + 1, y - 1].Item);
                                CellField.cellArr[x + 1, y - 1].ClearItem();
                            }
                        }
                    }
                }


                if (x == (CellField.Height - 2))
                {
                    if (CellField.cellArr[x, y].Item == null && CellField.cellArr[x, y].cellStt == CellStatus.Available)
                    {
                        //CellField.cellArr[x, y].DropItem(CellField.cellItemArr[x, y + 1], () => CellField.CreateItem(x, y));
                        CellField.cellArr[x, y].DropItem(CellField.cellItemArr[x + 1, y]);
                        CellField.cellItemArr[x, y] = CellField.cellItemArr[x + 1, y];
                        CellField.cellItemArr[x + 1, y] = null;
                        CellField.CreateItem(x + 1, y);
                        //Debug.Log("new item" + (x + 1) + "/" + (y));
                    }
                }
            }
        }
        if (CellField.CheckFieldNull() == true)
        {
            DropBoard();
        }
    }
}
