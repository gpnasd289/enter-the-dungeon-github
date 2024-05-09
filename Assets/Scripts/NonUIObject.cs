using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonUIObject : GOManager
{
	protected virtual void MouseDown()
	{
	}

	protected virtual void MouseUp()
	{
	}

	protected virtual void MouseEnter()
	{
	}

	protected virtual void MouseExit()
	{
	}

	protected virtual void MouseOver()
	{
	}

	public static bool IsPointerOverUIObject()
	{
		return false;
	}

	private void OnMouseDown()
	{
		MouseDown();
	}

	private void OnMouseUp()
	{
		MouseUp();
	}

	private void OnMouseEnter()
	{
		MouseEnter();
	}

	private void OnMouseExit()
	{
		MouseExit();
	}

	private void OnMouseOver()
	{
		MouseOver();
	}

}
