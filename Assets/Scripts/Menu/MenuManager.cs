using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject selectIcon;
    [SerializeField] private List<GameObject> menuList;
    int menuIndex = 0;
    [SerializeField] private string nameSelecting = "Menu-Start";
    private string currentLevel = "Scene1";
    [SerializeField] private TextMeshProUGUI textMenu;
    
    void SelectNextMenu() {
        menuIndex++;
        if (menuIndex >= 3)
        {
            menuIndex = 0;
        }

        GameObject selectingMenu = menuList[menuIndex];
        selectIcon.transform.position = new Vector2(selectIcon.transform.position.x, selectingMenu.transform.position.y);
        nameSelecting = selectingMenu.name;
        

    }
    void SelectPreviousMenu() {
        menuIndex--;
        if (menuIndex < 0)
        {
            menuIndex = 2;
        }
        GameObject selectingMenu = menuList[menuIndex];
        selectIcon.transform.position = new Vector2(selectIcon.transform.position.x, selectingMenu.transform.position.y);
        nameSelecting = selectingMenu.name;

    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("CurrentLevelNum"))
        {
            int num = PlayerPrefs.GetInt("CurrentLevelNum");
            textMenu.text = $"Start Game\n (Level {num})";
        }
        else
        {
            textMenu.text = "Start Game\n (Level 1)";
        }
/*        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectPreviousMenu();
            PlaySourceClick();
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNextMenu();
            PlaySourceClick();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(enterGame());
            if (nameSelecting == "Menu-Start")
            {
                
                ApplicationVariables.loadingSceneGame = "StartTalk";
                SceneManager.LoadScene("LoadingScene");
            }else if(nameSelecting == "Menu-Setting")
            {
               ApplicationVariables.loadingSceneGame = "Setting";
               
            }else if (nameSelecting == "Menu-Exit")
            {
                Application.Quit();
            }
        }*/
    }
    private void CheckLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetString("CurrentLevel");
        }
        else
        {
            PlayerPrefs.SetString("CurrentLevel", "Scene1");
        }
    }
    IEnumerator enterGame()
    {
        PlaySoundEnter();
        CheckLevel();
        if (ApplicationVariables.startTalk == false&&currentLevel=="Scene1")
        {
            Debug.Log("hi");
            ApplicationVariables.loadingSceneGame = "StartTalk";
            ApplicationVariables.startTalk = true;
        }
        else
        {
            ApplicationVariables.loadingSceneGame = currentLevel;
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LoadingScene");
    }
   public void PlaySourceClick()
    {
        MusicManager.Instance.PlaySFX("Click");
    }
    public void PlaySoundEnter()
    {
        MusicManager.Instance.PlaySFX("EnterGame");
    }
    public void Click_Start_Game()
    {
        StartCoroutine(enterGame());
    }
   public  void Click_Settings_Game()
    {
        PlaySourceClick();
        SceneManager.LoadScene("Settings");
    }
    public void Click_Levels_Game()
    {
        PlaySourceClick();
        SceneManager.LoadScene("LevelMenu");
    }
    public void Click_Shop_Game()
    {
        PlaySourceClick();
        SceneManager.LoadScene("Shop");
    }
    public void Click_Quit_Game()
    {
        PlaySourceClick();
        Application.Quit();
    }
}
