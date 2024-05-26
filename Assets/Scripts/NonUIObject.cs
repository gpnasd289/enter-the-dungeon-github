using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

	protected virtual void MouseDrag()
    {

    }
	public static bool IsPointerOverUIObject()
	{
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
            return true;
        }
        else
        {
            return false;
        }
	}

	private void OnMouseDown()
	{
		if (!IsPointerOverUIObject()) {
            MouseDown();
        }
	}

	private void OnMouseUp()
	{
        if (!IsPointerOverUIObject())
        {
            MouseUp();
        }
    }

	private void OnMouseEnter()
	{
        if (!IsPointerOverUIObject())
        {
            MouseEnter();
        }
    }

	private void OnMouseExit()
	{
        if (!IsPointerOverUIObject())
        {
            MouseExit();
        }
    }

	private void OnMouseOver()
	{
        if (!IsPointerOverUIObject())
        {
            MouseOver();
        }
    }
    private void OnMouseDrag()
    {
        if (!IsPointerOverUIObject())
        {
            MouseDrag();
        }
    }
}
