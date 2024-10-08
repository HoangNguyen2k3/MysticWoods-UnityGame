using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerButton : MonoBehaviour
{
    
    public void ExitToMenu()
    {
        ApplicationVariables.taked_key = false;
        GameObject inventory = GameObject.Find("UICanvas");
        GameObject player = GameObject.Find("Player");
        Playercontroller playercontroller=GetComponent<Playercontroller>();
        SceneManager.LoadSceneAsync(0);
        if(player != null)
        {   
            Destroy(player);
        }
        if(playercontroller != null)
        {
            Destroy(playercontroller);
        }
        if( inventory != null )
        {
            inventory.SetActive(false);
        }
        Time.timeScale = 1.0f;

    }
}
