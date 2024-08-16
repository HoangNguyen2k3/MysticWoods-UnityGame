using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class BestPoint : MonoBehaviour
{
    [SerializeField] private LocalizedString localStringScore;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private int score;

    private void Start()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            score = PlayerPrefs.GetInt("bestScore");
        }
        else
        {
           PlayerPrefs.SetInt("bestScore",0);
            score = 0;
        }
        UpdateScore();
    }
    private void OnEnable()
    {
        localStringScore.Arguments = new object[] { score };
        localStringScore.StringChanged += UpdateText;

    }
    private void UpdateText(string value)
    {
        textMeshPro.text = value;
    }
    private void OnDisable()
    {
        localStringScore.StringChanged -= UpdateText;
    }
    private void UpdateScore()
    {
        localStringScore.Arguments[0] = score;
        localStringScore.RefreshString();
    }
}
