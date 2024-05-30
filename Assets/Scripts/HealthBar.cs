using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSld;
    public Slider easeHealthSld;
    public float maxHealth;
    public float health;
    public TextMeshProUGUI healthTxt;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (health == maxHealth)
            {
                healthTxt.text = maxHealth.ToString();
            }
            else
            {
                healthTxt.text = (Mathf.FloorToInt(easeHealthSld.value)).ToString();
            }
        }
    }
    public void InitializeHealthBar(float healthIn)
    {
        health = healthIn;
        maxHealth = healthIn;
        healthTxt.text = healthIn.ToString();
        healthSld.maxValue = maxHealth;
        easeHealthSld.maxValue = maxHealth;
        healthSld.value = health;
        easeHealthSld.value = health;
    }
}
