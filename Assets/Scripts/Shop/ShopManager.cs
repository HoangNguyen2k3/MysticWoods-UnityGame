using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_gold;
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button btn3;
    [SerializeField] private List<GameObject> listUpdate;

    private void Start()
    {
        Upgrade();
        if (PlayerPrefs.HasKey("totalScore"))
        {
            score_gold.text = PlayerPrefs.GetInt("totalScore").ToString();
        }
        else
        {
            score_gold.text = "0";
        }

    }
    private void Update()
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
    public void Upgrade()
    {
        btn1.interactable = false;
        btn2.interactable = true;
        btn3.interactable = true;
        listUpdate[0].gameObject.SetActive(true);
        listUpdate[1].gameObject.SetActive(false);
        listUpdate[2].gameObject.SetActive(false);
    }
    public void Weapon()
    {
        btn1.interactable = true;
        btn2.interactable = false;
        btn3.interactable = true;
        listUpdate[0].gameObject.SetActive(false);
        listUpdate[1].gameObject.SetActive(true);
        listUpdate[2].gameObject.SetActive(false);
    }
    public void Character()
    {
        btn1.interactable = true;
        btn2.interactable = true;
        btn3.interactable = false;
        listUpdate[0].gameObject.SetActive(false);
        listUpdate[1].gameObject.SetActive(false);
        listUpdate[2].gameObject.SetActive(true);
    }
}
