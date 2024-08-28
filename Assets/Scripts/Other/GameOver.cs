using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private TMP_Text goldText;
    const string COIN_AMOUNT_TEXT = "Point";
    [SerializeField] private bool isWinner = true;
    private void Start()
    {
        if(goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        goldText.text=EconomyManager.Instance.currentGold.ToString()+" POINTS";
        if (!PlayerPrefs.HasKey("totalScore"))
        {
            PlayerPrefs.SetInt("totalScore", EconomyManager.Instance.currentGold);
        }
        else
        {
            int tempScore = PlayerPrefs.GetInt("totalScore");
            PlayerPrefs.SetInt("totalScore", tempScore+EconomyManager.Instance.currentGold);
        }
        if (EconomyManager.Instance.currentGold > PlayerPrefs.GetInt("bestScore")&&PlayerPrefs.HasKey("bestScore")) {
            PlayerPrefs.SetInt("bestScore", EconomyManager.Instance.currentGold);
        }
        if (isWinner)
        {
            Debug.Log("You win");
            MusicManager.Instance.PlaySFX("Winner");
            if (!PlayerPrefs.HasKey("Round1"))
            {
                Debug.Log("Haha");
                PlayerPrefs.SetString("Round1", "win");
            }
        }
        else
        {
            MusicManager.Instance.PlaySFX("GameOver");
        }
        
    }
    public void Restart()
    {
        ApplicationVariables.add_boss_skeleton = 1;
        PlayerHealth.Instance.currentHealth = 4;
        Stamina.Instance.ResetStamina();
        EconomyManager.Instance.ResetGold();
        DeleteKeySaveScene.Instance.RestartSceneGame();
        if (GameObject.Find("Player") != null)
        {
            Destroy(GameObject.Find("Player"));
        }
        ApplicationVariables.taked_key = false;
        SceneManager.LoadScene("Scene1");
    }
    public void ToMenu()
    {
        GameObject inventory = GameObject.Find("UICanvas");
        GameObject player = GameObject.Find("Player");
        Playercontroller playercontroller = GetComponent<Playercontroller>();
        ApplicationVariables.taked_key = false;
        DeleteKeySaveScene.Instance.RestartSceneGame();
        SceneManager.LoadSceneAsync(0);
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
