using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI loadingText;
    private GameObject inventory;
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
        if (GameObject.Find("UICanvas")==null)
        {
             inventory = FindInactiveObjectByName("UICanvas");
        if (inventory != null && !inventory.activeSelf)
        {
            inventory.SetActive(true);
            GameObject.Find("PauseMenu").SetActive(false);
        }
        }
       

    }
    GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None && obj.name == name)
            {
                return obj.gameObject;
            }
        }
        return null;
    }
}
