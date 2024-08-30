using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckNumLevel : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    private void Start()
    {
        ApplicationVariables.numLevelCurrency = currentLevel;
    }
}
