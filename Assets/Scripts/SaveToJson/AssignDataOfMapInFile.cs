using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AssignDataOfMapInFile : MonoBehaviour
{
    [SerializeField] private List<string> enemyTag;
    [SerializeField] private List<string> environmentsTag;
    private GameObject[] enemy;
    private GameObject[] environments;
    private SavingFile saving;
    private void OnEnable()
    {
        saving=gameObject.GetComponent<SavingFile>();
    }
    private void LateUpdate()
    {
        SaveCurrentState();
    }

    private void SaveCurrentState()
    {
        if (saving == null)
        {
            Debug.Log("SavingFile component not found!");
            return;
        }
        saving.sceneManage.enemiesInScene.Clear();
        saving.sceneManage.enviroments.Clear();

        for(int i = 0; i < enemyTag.Count; i++)
        {
            enemy = GameObject.FindGameObjectsWithTag(enemyTag[i]);
            foreach (var e in enemy)
            {
                EnemiesInScene enemyData = new EnemiesInScene();
                enemyData.name= e.tag;
                enemyData.position = e.transform.position;
                saving.sceneManage.enemiesInScene.Add(enemyData);
            }
        }

        for(int i = 0;i < environmentsTag.Count; i++)
        {
            environments = GameObject.FindGameObjectsWithTag(environmentsTag[i]);
            foreach (var b in environments)
            {
                Enviroment envData = new Enviroment();
                envData.name = b.tag;
                envData.position = b.transform.position;
                saving.sceneManage.enviroments.Add(envData);
            }
        }     
    }
    private void OnDestroy()
    {
        if (saving == null)
        {
            Debug.Log("Not found");
            return;
        }
        saving.SaveData();
    }
}
