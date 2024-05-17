using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncManager : MonoBehaviour
{
    public static FuncManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void DelayTimeFunc(float time, UnityAction func)
    {
        StartCoroutine(IEDelayTimeFunc(time, func));
    }
    public void DelayFunc(bool check, UnityAction func)
    {
        StartCoroutine(IEDelayFunc(check, func));
    }
    private IEnumerator IEDelayTimeFunc(float time, UnityAction func)
    {
        yield return new WaitForSeconds(time);
        func?.Invoke();
    }
    private IEnumerator IEDelayFunc(bool check, UnityAction funcRun)
    {
        yield return new WaitUntil(() => check);
        funcRun?.Invoke();
    }
}
