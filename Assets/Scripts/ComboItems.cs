using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItems
{
	public enum CellItems
	{
		Metal = 0,
		Wood = 1,
		Water = 2,
		Fire = 3,
		Earth = 4
	}

	public CellItems Item;

	public int Count;

	public ComboItems(CellItems chosenItem, int count)
	{
	}
}
