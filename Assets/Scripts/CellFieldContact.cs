using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFieldContact : NonUIObject
{
	public CellField Field;

	protected override void MouseDown()
	{
		Debug.Log("mouse down cell field");
	}

	protected override void MouseUp()
	{
	}

	protected override void Construct()
	{
	}
}
