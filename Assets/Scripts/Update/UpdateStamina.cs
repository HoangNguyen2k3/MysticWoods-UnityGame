using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStamina : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("Stamina"))
        {
            for(int i = 0; i <= PlayerPrefs.GetInt("Stamina") - 1; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            for(int i = PlayerPrefs.GetInt("Stamina"); i < 6; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
