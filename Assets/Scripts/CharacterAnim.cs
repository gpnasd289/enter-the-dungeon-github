using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHG.AnimatorCoder;
using QuangDM.Common;
using System;

public class CharacterAnim : AnimatorCoder
{
    public List<Cell> matchedCells = new List<Cell>();
    private System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        Observer.Instance.AddObserver(EventName.DoAnim, DoAnim);
    }

    private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        foreach (Cell c in temp)
        {
            matchedCells.Add(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DefaultAnimation(0);
    }
    public override void DefaultAnimation(int layer)
    {
        if (matchedCells.Count < 2)
        {
            Play(new(Animations.IDLE, true, new()));
        }
        else if (matchedCells.Count == 2)
        {
            Play(new(RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true)));
        }
        else if (matchedCells.Count == 3)
        {
            Play(new(RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                     RandomEnumValue<Animations>(1, 4), true))));
        }
        else if (matchedCells.Count == 4)
        {
            Play(new(RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                     RandomEnumValue<Animations>(1, 4), true)))));
        }
        else if (matchedCells.Count == 5)
        {
            Play(new(RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                    RandomEnumValue<Animations>(1, 4), true, new(
                     RandomEnumValue<Animations>(1, 4), true))))));
        }
        else
        {
            Play(new(RandomEnumValue<Animations>(1, 4), true, new()));
            Play(new(RandomEnumValue<Animations>(5, 7), true, new()));
        }
    }
    public Animations RandomEnumValue<Animations>(int min, int max)
    {
        var v = Enum.GetValues(typeof(Animations));
        return (Animations) v.GetValue(rand.Next(min, max));
    }
}
