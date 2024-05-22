using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
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
        Observer.Instance.AddObserver(EventName.DoAnim, DoAnim);
    }

    private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        atkTime = temp.Count;
        randIndex = rand.Next(0, 3);
        anim.SetTrigger("Atk");
        anim.SetInteger("AtkIndex", randIndex);
        anim.SetInteger("AtkTime", atkTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAnimFinish()
    {
        playerHandle.DealDamageToEnemy();
        atkTime--;
        randIndex = randomIntExcept(0, 3, randIndex);
        anim.SetInteger("AtkIndex", randIndex);
        anim.SetInteger("AtkTime", atkTime);
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
