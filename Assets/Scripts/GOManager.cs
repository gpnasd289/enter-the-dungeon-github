using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOManager : MonoBehaviour
{
    public bool Awoken { get; private set; }
    public bool isMouseHold;
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
        }
        else
        {
            isMouseHold = false;
        }
    }
}
