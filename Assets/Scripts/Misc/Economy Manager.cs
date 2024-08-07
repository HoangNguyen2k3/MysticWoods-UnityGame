using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    public int currentGold;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateCurrentGold()
    {
        currentGold++;
        if (goldText == null)
        {
            goldText=GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();

        }
        goldText.text=currentGold.ToString("D3");
    }
    public void ResetGold()
    {
        currentGold = 0;
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();

        }
        goldText.text = currentGold.ToString("D3");
    }

}
