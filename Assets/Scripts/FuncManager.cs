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
    public void DelayFunc(float time, UnityAction func)
    {
        StartCoroutine(IEDelayFunc(time, func));
    }
    private IEnumerator IEDelayFunc(float time, UnityAction func)
    {
        yield return new WaitForSeconds(time);
        func?.Invoke();
    }
}
