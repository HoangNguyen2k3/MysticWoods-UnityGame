using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSetAll : MonoBehaviour
{
    public LocaleSelector localeSelector;
    private void Awake()
    {
        int value = PlayerPrefs.GetInt("LocaleKey");
        if (value == 0)
        {
            localeSelector.ChangeLocale(0);
        }
        else
        {
            localeSelector.ChangeLocale(1);
        }
    }
}
