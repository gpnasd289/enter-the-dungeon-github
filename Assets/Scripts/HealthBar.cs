using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSld;
    public Slider easeHealthSld;
    public float maxHealth;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        healthSld.maxValue = maxHealth;
        easeHealthSld.maxValue = maxHealth;
        healthSld.value = health;
        easeHealthSld.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSld.value != health)
        {
            healthSld.value = health;
        }
        if (healthSld.value != easeHealthSld.value)
        {
            easeHealthSld.DOValue(healthSld.value, 1f);
        }
    }
}
