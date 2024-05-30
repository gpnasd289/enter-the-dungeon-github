using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour, IAnimatable
{
    private readonly System.Random rand = new System.Random();
    public Player playerHandle;
    private Animator anim;
    public int atkTime;
    public int randIndex;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    /*private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        atkTime = temp.Count;
        randIndex = rand.Next(0, 3);
        anim.SetTrigger("Atk");
        anim.SetInteger("AtkIndex", randIndex);
        anim.SetInteger("AtkTime", atkTime);
    }*/
    public void PlayAttackAnimation(int attackCount)
    {
        atkTime = attackCount;
        randIndex = rand.Next(0, 3);
        anim.SetTrigger("Atk");
        anim.SetInteger("AtkIndex", randIndex);
        anim.SetInteger("AtkTime", atkTime);
    }
    public void OnAnimFinish()
    {
        
        if (atkTime > 0)
        {
            CombatManager.Instance.OnPlayerAnimationComplete(atkTime);
            atkTime--;
            randIndex = randomIntExcept(0, 3, randIndex);
            anim.SetInteger("AtkIndex", randIndex);
            anim.SetInteger("AtkTime", atkTime);
            
        }
        if (atkTime == 0)
        {
            anim.SetInteger("AtkTime", atkTime);
            /*playerHandle.MakeMove(() => CombatManager.Instance.EnemyAttack(CombatManager.Instance.atkTime));
            playerHandle.CompleteMove();*/
            playerHandle.OnComboOver();
        }
        
    }
    public void OnAnimDoDmg()
    {
        if (atkTime > 0)
        {
            CombatManager.Instance.OnPlayerAnimationComplete(atkTime);
        }
    }
    public int randomIntExcept(int min, int max, int except)
    {
        int random = except;

        while (random == except)
        {
            random = rand.Next(min, max);
        }

        return random;
    }
}
