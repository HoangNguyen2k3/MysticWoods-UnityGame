using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerButton : MonoBehaviour
{
    public void ExitToMenu()
    {
        GameObject inventory = GameObject.Find("UICanvas");
        GameObject player = GameObject.Find("Player");
        Playercontroller playercontroller=GetComponent<Playercontroller>();
        SceneManager.LoadSceneAsync(2);
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
            Destroy(inventory );
        }
        
    }
}
