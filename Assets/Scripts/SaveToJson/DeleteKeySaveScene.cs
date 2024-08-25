using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteKeySaveScene : Singleton<DeleteKeySaveScene>
{
    private void OnApplicationQuit()
    {
        for(int i = 0; i < ApplicationVariables.numNameSaveScene; i++)
        {
            Debug.Log(ApplicationVariables.nameKeySaveScene[i]);
            PlayerPrefs.DeleteKey(ApplicationVariables.nameKeySaveScene[i]);
        }
    }
}
