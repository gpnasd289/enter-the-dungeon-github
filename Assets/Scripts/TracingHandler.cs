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

    public bool BeingProcessed { get; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
