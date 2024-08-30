using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    [SerializeField] private List<int> priceCharacters;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<TextMeshProUGUI> price;
    [SerializeField] private List<Image> coins;
    private int num_Character = 4;
    private void Start()
    {
        PlayerPrefs.SetInt("Samurai", 1);
        PlayerPrefs.SetInt("Magician", 1);
        if (PlayerPrefs.HasKey("CharacterInUse"))
        {
            int temp = PlayerPrefs.GetInt("CharacterInUse");
            buttons[temp].interactable = false;
            buttons[temp].GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
            if (temp != 0)
            {
                buttons[0].interactable = true;
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
            }
            if (temp == 1)
            {
                CheckButton("Samurai", 2, temp);
                CheckButton("Magician", 3,temp);
            }
            else if (temp == 2)
            {
                CheckButton("Bower", 1, temp);
                CheckButton("Magician", 3,temp);
            }
            else if (temp == 3)
            {
                CheckButton("Bower", 1, temp);
                CheckButton("Samurai", 2,temp);
            }else if (temp == 0)
            {
                CheckButton("Bower", 1, temp);
                CheckButton("Samurai", 2, temp);
                CheckButton("Magician", 3, temp);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CharacterInUse", 0);
            buttons[0].interactable = false;
            TextMeshProUGUI text = buttons[0].GetComponentInChildren<TextMeshProUGUI>();
            text.text = "SELECTED";
        }

    }
    private void Update()
    {
        int temp = PlayerPrefs.GetInt("CharacterInUse");
        buttons[temp].interactable = false;
        buttons[temp].GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
        if (temp != 0)
        {
            buttons[0].interactable = true;
            buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
        }
        int money = PlayerPrefs.GetInt("totalScore");
        CheckMoneyCanOpenButton("Bower", money, 800, 1);
        CheckMoneyCanOpenButton("Samurai", money, 1500, 2);
        CheckMoneyCanOpenButton("Magician", money, 3000, 3);
        if (temp == 1)
        {
            CheckButton("Samurai", 2, temp);
            CheckButton("Magician", 3, temp);
        }
        else if (temp == 2)
        {
            CheckButton("Bower", 1, temp);
            CheckButton("Magician", 3, temp);
        }
        else if (temp == 3)
        {
            CheckButton("Bower", 1, temp);
            CheckButton("Samurai", 2, temp);
        }
        else if (temp == 0)
        {
            CheckButton("Bower", 1, temp);
            CheckButton("Samurai", 2, temp);
            CheckButton("Magician", 3, temp);
        }
    }

    private void FixedUpdate()
    {

        if (PlayerPrefs.HasKey("Bower"))
        {
            price[1].text = "OWNED";
            coins[0].enabled = false;
        }
        if (PlayerPrefs.HasKey("Samurai"))
        {
            price[2].text = "OWNED";
            coins[1].enabled = false;
        }
        if (PlayerPrefs.HasKey("Magician"))
        {
            price[3].text = "OWNED";
            coins[2].enabled = false;
        }
    }

    private void CheckButton(string name,int index,int temp)
    {
        if (PlayerPrefs.HasKey(name) && index != temp)
        {
            buttons[index].interactable = true;
            buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
        }
    }
    private void CheckMoneyCanOpenButton(string name,int currentMoney,int price,int index)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return;
        }
        if (currentMoney >= price)
        {
            buttons[index].interactable = true;
        }
        else
        {
            buttons[index].interactable = false;
        }
    }
    public void BuyBower()
    {
        if (PlayerPrefs.HasKey("Bower"))
        {
            PlayerPrefs.SetInt("CharacterInUse",1);
            return;
        }
        int money = PlayerPrefs.GetInt("totalScore");
        money -= 800;
        PlayerPrefs.SetInt("totalScore", money);
        PlayerPrefs.SetInt("Bower", 1);
        PlayerPrefs.SetInt("CharacterInUse", 1);
    }
    public void BuySamurai()
    {
        if (PlayerPrefs.HasKey("Samurai"))
        {
            PlayerPrefs.SetInt("CharacterInUse", 2);
            return;
        }
        int money = PlayerPrefs.GetInt("totalScore");
        money -= 1500;
        PlayerPrefs.SetInt("totalScore", money);
        PlayerPrefs.SetInt("Samurai", 1);
        PlayerPrefs.SetInt("CharacterInUse", 2);
    }
    public void BuyMagician()
    {
        if (PlayerPrefs.HasKey("Magician"))
        {
            PlayerPrefs.SetInt("CharacterInUse", 3);
            return;
        }
        int money = PlayerPrefs.GetInt("totalScore");
        money -= 3000;
        PlayerPrefs.SetInt("totalScore", money);
        PlayerPrefs.SetInt("Magician", 1);
        PlayerPrefs.SetInt("CharacterInUse", 3);
    }
    public void DefaultCharacter()
    {
        PlayerPrefs.SetInt("CharacterInUse", 0);
    }
}
