using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialSkill : MonoBehaviour
{
    [SerializeField] private List<GameObject> CharacterSpecialSkill;
    void Start()
    {
            if (PlayerPrefs.HasKey("CharacterInUse"))
            {
            CharacterSpecialSkill[PlayerPrefs.GetInt("CharacterInUse")].SetActive(true);
            for (int j = 0; j < CharacterSpecialSkill.Count; j++)
                {
                    if (j != PlayerPrefs.GetInt("CharacterInUse"))
                    {
                        CharacterSpecialSkill[j].SetActive(false);
                    }
                }
        }
        else
        {
            CharacterSpecialSkill[0].SetActive(true);
            for(int i = 1; i <= 3; i++)
            {
                CharacterSpecialSkill[i].SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("CharacterInUse"))
        {
            CharacterSpecialSkill[PlayerPrefs.GetInt("CharacterInUse")].SetActive(true);
            for (int j = 0; j < CharacterSpecialSkill.Count; j++)
            {
                if (j != PlayerPrefs.GetInt("CharacterInUse"))
                {
                    CharacterSpecialSkill[j].SetActive(false);
                }
            }
        }
        else
        {
            CharacterSpecialSkill[0].SetActive(true);
            for (int i = 1; i <= 3; i++)
            {
                CharacterSpecialSkill[i].SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        
    }
}
