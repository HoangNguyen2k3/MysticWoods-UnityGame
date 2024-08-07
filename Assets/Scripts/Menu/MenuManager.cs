using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceComponent;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip enter;
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
                
                ApplicationVariables.loadingSceneGame = "Scene1";
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
        yield return new WaitForSeconds(0.6f);
        ApplicationVariables.loadingSceneGame = "Scene1";
        SceneManager.LoadScene("LoadingScene");
    }
   public void PlaySourceClick()
    {
        audioSourceComponent.PlayOneShot(click);
    }
    public void PlaySoundEnter()
    {
        audioSourceComponent.PlayOneShot(enter);
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
        ApplicationVariables.loadingSceneGame = "Setting";
    }
    public void Click_Quit_Game()
    {
        PlaySourceClick();
        Application.Quit();
    }
}
