using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHG.AnimatorCoder;
using QuangDM.Common;
using System;
using UnityEngine.Events;

public class CharacterAnim : AnimatorCoder
{
    public List<Cell> matchedCells = new List<Cell>();
    private readonly System.Random rand = new System.Random();
    private AnimationData COMBO;
    private readonly AnimationData IDLE = new(Animations.IDLE, false, new());
    private int timeLoop;
    private Animations[] comboAnim = new Animations[3];
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        Observer.Instance.AddObserver(EventName.DoAnim, DoAnim);
        COMBO = new(RandomEnumValue<Animations>(1, 4));
        var v = Enum.GetValues(typeof(Animations));
        for (int i = 0; i < comboAnim.Length; i++)
        {
            comboAnim[i] = (Animations)v.GetValue(i);
        }
    }

    private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        foreach (Cell c in temp)
        {
            matchedCells.Add(c);
            timeLoop += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DefaultAnimation(0);
    }
    public override void DefaultAnimation(int layer)
    {
        if (matchedCells.Count > 0)
        {
            for (int i = 0; i < matchedCells.Count; i++)
            {
                if (timeLoop > 0)
                {
                    StartCoroutine(LoopCombo(!animator.GetCurrentAnimatorStateInfo(0).IsName(COMBO.animation.ToString()), (() => { 
                        Play(COMBO);
                        matchedCells.Clear();
                    })));
                    timeLoop--;
                }
            }
        }
        else
        {
            Play(IDLE);
        }
    }
    public void StopCombo()
    {

    }
    public void CheckFinish()
    {

    }
    IEnumerator LoopCombo(bool check, UnityAction func)
    {
        func?.Invoke();
        yield return new WaitUntil(() => check);
    }
    public Animations RandomEnumValue<Animations>(int min, int max)
    {
        var v = Enum.GetValues(typeof(Animations));
        return (Animations) v.GetValue(rand.Next(min, max));
    }
}
