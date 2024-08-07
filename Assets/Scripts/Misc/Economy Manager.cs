using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold;

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

}
