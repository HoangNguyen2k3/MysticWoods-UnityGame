using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_gold;

    private void Start()
    {
        if (PlayerPrefs.HasKey("totalScore"))
        {
            score_gold.text = PlayerPrefs.GetInt("totalScore").ToString();
        }
        else
        {
            score_gold.text = "0";
        }

    }
}
