using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateHealthNum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthNum;
    void Start()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            healthNum.text = PlayerPrefs.GetInt("Health").ToString();
        }
        else
        {
            healthNum.text = "10";
        }
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            healthNum.text = PlayerHealth.Instance.currentHealth.ToString();
        }
    }
}
