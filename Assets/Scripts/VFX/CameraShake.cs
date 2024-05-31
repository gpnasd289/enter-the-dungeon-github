using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    private void OnShake(float duration, float strength)
    {
        transform.DOPunchPosition(new Vector3(0.5f,0,0), 0.5f, 0, 0);
        //transform.DOShakePosition(duration, strength);
        //transform.DOShakeRotation(duration, strength);
    }

    public static void Shake(float duration, float strength) => Instance.OnShake(duration, strength);
}
