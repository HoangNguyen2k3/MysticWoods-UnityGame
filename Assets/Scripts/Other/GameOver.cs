using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private TMP_Text goldText;
    const string COIN_AMOUNT_TEXT = "Point";
    private void Start()
    {
        if(goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        goldText.text=EconomyManager.Instance.currentGold.ToString()+" POINTS";
    }
    public void Restart()
    {
        PlayerHealth.Instance.currentHealth = 4;
        Stamina.Instance.ResetStamina();
        EconomyManager.Instance.ResetGold();
        if (GameObject.Find("Player") != null)
        {
            Destroy(GameObject.Find("Player"));
        }
        SceneManager.LoadScene("Scene1");
    }
    public void ToMenu()
    {
        GameObject inventory = GameObject.Find("UICanvas");
        GameObject player = GameObject.Find("Player");
        Playercontroller playercontroller = GetComponent<Playercontroller>();
        SceneManager.LoadSceneAsync(2);
        if (player != null)
        {
            Destroy(player);
        }
        if (playercontroller != null)
        {
            Destroy(playercontroller);
        }
        if (inventory != null)
        {
            inventory.SetActive(false);
        }
    }
}
