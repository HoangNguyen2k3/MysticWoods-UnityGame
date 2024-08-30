using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChooseManager : MonoBehaviour
{
    [SerializeField] private string level_name;
    public IEnumerator Choose_Level()
    {
        MusicManager.Instance.PlaySFX("EnterGame");
        ApplicationVariables.loadingSceneGame = level_name;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LoadingScene");
    }
    public void Choose_Level_Enter()
    {
        StartCoroutine(Choose_Level());
        //Add feature to choose level when start game
    }
}
