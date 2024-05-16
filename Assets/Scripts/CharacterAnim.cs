using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHG.AnimatorCoder;
using QuangDM.Common;
using System;

public class CharacterAnim : AnimatorCoder
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        Observer.Instance.AddObserver(EventName.MatchChain, MatchChain);
    }

    private void MatchChain(object data)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void DefaultAnimation(int layer)
    {
        throw new System.NotImplementedException();
    }
}
