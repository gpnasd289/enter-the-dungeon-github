using DG.Tweening;
using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class PlayerAnim : MonoBehaviour
{
    private readonly System.Random rand = new System.Random();
    public int atkTime;
    private Animator anim;
    public GameObject hitTxtPrefab;
    public ObjectPool<GameObject> _pool;
    public GameObject txtParent;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _pool = new ObjectPool<GameObject>(CreateHitTxt, OnGetObjectFromPool, OnReturnObjectFromPool, OnDestroyObjectFromPool, true, 5, 10);
        Observer.Instance.AddObserver(EventName.DoAnim, DoAnim);
    }

    private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        atkTime = temp.Count;
        if (atkTime > 1)
        {
            anim.SetInteger("AtkIndex", rand.Next(3));
            anim.SetInteger("AtkTime", atkTime);
            anim.SetTrigger("Atk");
        }
    }
    public void AnimEnd()
    {
        GameObject go = _pool.Get();
        atkTime--;
        anim.SetInteger("AtkTime", atkTime);
        StartCoroutine(DeactHitTxt(go));
    }
    public int RandomIntExcept(int min, int max, int except)
    {
        int r = rand.Next(min, max);
        if (r == except)
        {
            r = RandomIntExcept(min, max, except);
        }
        return r;
    }
    IEnumerator DeactHitTxt(GameObject go)
    {
        yield return new WaitForSeconds(1);
        _pool.Release(go);
    }
    private GameObject CreateHitTxt()
    {
        GameObject hitTxt = Instantiate(hitTxtPrefab, txtParent.transform);
        return hitTxt;
    }
    private void OnGetObjectFromPool(GameObject hitTxt)
    {
        hitTxt.SetActive(true);
        hitTxt.transform.localScale = Vector3.zero;
        hitTxt.transform.DOScale(1, 1);
    }
    private void OnReturnObjectFromPool(GameObject hitTxt)
    {
        hitTxt.transform.DOScale(0, 1).OnComplete(() => hitTxt.SetActive(false));
    }
    private void OnDestroyObjectFromPool(GameObject hitTxt)
    {
        Destroy(hitTxt);
    }
}
