using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour, IAnimatable
{
    private readonly System.Random rand = new System.Random();
    public Enemy enemyHandle;
    private Animator anim;
    public int atkTime;
    public int randIndex;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

/*    public void DoAnim(int attackCount)
    {
        atkTime = attackCount;
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
    void Update()
    {
    }

    public void OnAnimFinish()
    {
        if (atkTime > 0)
        {
            atkTime--;
            randIndex = randomIntExcept(0, 3, randIndex);
            anim.SetInteger("AtkIndex", randIndex);
            anim.SetTrigger("Atk");
            CombatManager.Instance.OnEnemyAnimationComplete();
        }
        if (atkTime == 0)
        {
            anim.SetInteger("AtkTime", atkTime);
            enemyHandle.CompleteMove();
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
