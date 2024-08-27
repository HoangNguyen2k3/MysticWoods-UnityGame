using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpGrade : MonoBehaviour
{
    [SerializeField] private Slider sliderHeath;
    [SerializeField] private Slider sliderStamina;

    [SerializeField] private Button buyHealth;
    [SerializeField] private Button buyStamina;
    private int BEGIN_HEALTH = 10;
    private int BEGIN_STAMINA = 3;
    private int PRICE_HEALTH = 50;
    private int PRICE_STAMINA = 30;
    private void Start()
    { 

        if (PlayerPrefs.HasKey("Health"))
        {
            sliderHeath.value = PlayerPrefs.GetInt("Health")-BEGIN_HEALTH;
        }
        else
        {
            sliderHeath.value = 0;
            PlayerPrefs.SetInt("Health", BEGIN_HEALTH);
        }
        if (PlayerPrefs.HasKey("Stamina"))
        {
            sliderStamina.value = PlayerPrefs.GetInt("Stamina")-BEGIN_STAMINA;
        }
        else
        {
            sliderStamina.value = 0;
            PlayerPrefs.SetInt("Stamina", BEGIN_STAMINA);
        }

    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("totalScore") < PRICE_HEALTH)
        {
            buyHealth.interactable = false;
        }
        else
        {
            buyHealth.interactable = false; 
        }
        if (PlayerPrefs.GetInt("totalScore") < PRICE_STAMINA)
        {
            buyStamina.interactable = false;
        }
        else
        {
            buyStamina.interactable = true;
        }
    }
    public void UpdateHealth()
    {
        if (sliderHeath.value == 5)
        {
            return;
        }
        sliderHeath.value += 1;
        int temp = (int)sliderHeath.value;
        PlayerPrefs.SetInt("Health", temp+BEGIN_HEALTH);
        int money = PlayerPrefs.GetInt("totalScore");
        PlayerPrefs.SetInt("totalScore", money - PRICE_HEALTH);
    }
    public void UpdateStamina()
    {
        if (sliderStamina.value == 2)
        {
            return;
        }
        sliderStamina.value += 1;
        int temp = (int)sliderStamina.value;
        PlayerPrefs.SetInt("Stamina", temp+BEGIN_STAMINA);
        int money = PlayerPrefs.GetInt("totalScore");
        PlayerPrefs.SetInt("totalScore", money - PRICE_STAMINA);
    }
}
