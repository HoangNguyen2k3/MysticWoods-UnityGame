using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChooseManager : MonoBehaviour
{
    [SerializeField] private string level_name;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] public int numLevel;
    public IEnumerator Choose_Level()
    {
        MusicManager.Instance.PlaySFX("EnterGame");
        ApplicationVariables.loadingSceneGame = level_name;
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetString("CurrentLevel",level_name);
        PlayerPrefs.SetInt("CurrentLevelNum", numLevel);
        SceneManager.LoadScene("LoadingScene");
        ApplicationVariables.numLevelCurrency = numLevel;

    }
    public void Choose_Level_Enter()
    {
        StartCoroutine(Choose_Level());
        //Add feature to choose level when start game
    }
    private void Update()
    {
        if (ApplicationVariables.numLevelCurrency == numLevel)
        {
            text.text = "Your here";
        }
        else
        {
            text.text = "";
        }
    }
}
