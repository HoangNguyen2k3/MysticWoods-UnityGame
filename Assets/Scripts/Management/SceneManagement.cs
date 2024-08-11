using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
   public string SceneTransitionName {  get; private set; }
   
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
    private void Update()
    {
        if (ApplicationVariables.boss_alive == false)
        {
            StartCoroutine(Wait());
        }
        
    }
    private IEnumerator Wait()
    {ApplicationVariables.boss_alive = true;
        yield return new WaitForSeconds(1);
         
        SceneManager.LoadScene("Winner");
    }
}
