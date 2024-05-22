using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOManager : MonoBehaviour
{
    public bool Awoken { get; private set; }
    protected bool isMouseHold;
    protected bool isMouseUp;
    protected virtual void Construct()
    {
    }

    protected virtual void Destruct()
    {
    }

    protected virtual void Process()
    {
    }

    private void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isMouseHold = true;
            isMouseUp = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseHold = false;
            isMouseUp = true;
        }
        else
        {
            isMouseHold = false;
            isMouseUp = true;
        }
    }
}
