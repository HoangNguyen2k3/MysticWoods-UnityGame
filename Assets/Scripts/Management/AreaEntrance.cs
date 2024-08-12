using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    [SerializeField] bool isBossMap = false;
    [SerializeField] Transform spawnPlayer;
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
}
