using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    public LocaleSelector localeSelector;
    public  TMPro.TMP_Dropdown  dropdown;
    private void Awake()
    {
        int value = PlayerPrefs.GetInt("LocaleKey");
        if (value == 0)
        {
            dropdown.value = 0;
        }
        else
        {
            dropdown.value = 1;
        }
        dropdown.RefreshShownValue();
    }
    private void Start()
    {
        
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    public void OnDropdownValueChanged(int index)
    {
        localeSelector.ChangeLocale(index);
    }
}
