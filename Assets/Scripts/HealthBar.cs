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
            if (health == 100)
            {
                healthTxt.text = 100.ToString();
            }
            else
            {
                healthTxt.text = (Mathf.FloorToInt(easeHealthSld.value)).ToString();
            }
        }
    }
}
