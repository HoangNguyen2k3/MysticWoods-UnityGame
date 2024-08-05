using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI loadingText;
    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    { 

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ApplicationVariables.loadingSceneGame);
        asyncLoad.allowSceneActivation = false; //after scene is load, don't active it
        while (asyncLoad.progress < 0.9f) {
            loadingText.text = "Loading..." +Mathf.RoundToInt(asyncLoad.progress * 100)+"%";
            yield return null;
        }
       
        yield return new WaitForSeconds(2);
        loadingText.text = "Loading...100%";
        asyncLoad.allowSceneActivation = true;

    }

}
