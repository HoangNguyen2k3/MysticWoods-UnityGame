using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
        }
    }
    IEnumerator enterGame()
    {
        PlaySoundEnter();     
        ApplicationVariables.loadingSceneGame = "StartTalk";
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
        
        /*ApplicationVariables.loadingSceneGame = "Scene1";
        SceneManager.LoadScene("LoadingScene");*/
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
    public void Click_Quit_Game()
    {
        PlaySourceClick();
        Application.Quit();
    }
}
