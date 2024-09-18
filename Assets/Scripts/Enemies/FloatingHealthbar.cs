using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI numHealth;
    private float maxHealth;
    public void UpdateHealthBar(float currentValue,float maxValue)
    {
        slider.value = currentValue / maxValue;
        maxHealth = maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        if(numHealth != null)
        {
            numHealth.text = (slider.value*maxHealth).ToString();
        }
    }
}
