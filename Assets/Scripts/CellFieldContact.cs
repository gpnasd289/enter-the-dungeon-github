using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFieldContact : NonUIObject
{
	public CellField Field;

	protected override void MouseDown()
	{
		Debug.Log("mouse down cell field");
		/*Field.isMouseDown = true;
		Field.isMouseUp = false;*/
	}

	protected override void MouseUp()
	{
		Debug.Log("mouse up cell field");
		/*Field.isMouseDown = false;
		Field.isMouseUp = true;*/
	}

	protected override void Construct()
	{
	}
}
