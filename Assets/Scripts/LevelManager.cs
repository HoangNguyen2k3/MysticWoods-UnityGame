using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string nameRound;
    private void Start()
    {
        if (PlayerPrefs.HasKey(nameRound))
        {
            gameObject.SetActive(true);
        }
    }
}
