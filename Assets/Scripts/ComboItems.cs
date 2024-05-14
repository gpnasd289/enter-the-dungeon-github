using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItems
{
	public enum CellItems
	{
		Pizza = 0,
		Chips = 1,
		Donut = 2,
		HotDog = 3,
		Energy = 4
	}

	public CellItems Item;

	public int Count;

	public ComboItems(CellItems chosenItem, int count)
	{
	}
}
