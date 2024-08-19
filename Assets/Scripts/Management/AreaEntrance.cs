using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    [SerializeField] bool isBossMap = false;
    [SerializeField] Transform spawnPlayer;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform boss_entrance;
    private void Start()
    {
        if (isBossMap)
        {
            Playercontroller.Instance.transform.position = spawnPlayer.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
        }
        else
        {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
                {
                    Playercontroller.Instance.transform.position=this.transform.position;
                    CameraController.Instance.SetPlayerCameraFollow();
                    UIFade.Instance.FadeToClear();
                }
        }
        
    }
    private void Update()
    {
        if (boss && boss_entrance)
        {
            if (ApplicationVariables.bossFlyingNum == 0)
            {
                StartCoroutine(BossAppear());
                ScreenShakeManager.Instance.ShakeScreen();
                ScreenShakeManager.Instance.ShakeScreen();
                
                ApplicationVariables.bossFlyingNum = -1;
            }
        }

    }
    private IEnumerator BossAppear()
    {
        yield return new WaitForSeconds(2f);
        ScreenShakeManager.Instance.ShakeScreen();
        Instantiate(boss, boss_entrance.position, Quaternion.identity);
    }

}
