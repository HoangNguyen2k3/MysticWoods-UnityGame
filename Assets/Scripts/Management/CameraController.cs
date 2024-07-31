using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera=FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = Playercontroller.Instance.transform;
    }

}
