using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakedDataFromFile : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> enemysPrefab;
    [SerializeField] private List<string> nameTagPrefabEnemys;
    [SerializeField] private List<GameObject> enviromentsPrefab;
    [SerializeField] private List<string> nameTagPrefabEnvironments;
    private SavingFile saving;
    string sceneKey;

    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        sceneKey = sceneName + "_hasLoaded";
        if (PlayerPrefs.HasKey(sceneKey) && PlayerPrefs.GetInt(sceneKey) == 1)
        {
            saving = FindObjectOfType<SavingFile>();
            if (saving != null)
            {
                Debug.Log("saving not null");
                GameObject enemiesParent = GameObject.Find("Enemies");
                if (enemiesParent != null)
                {
                    enemiesParent.SetActive(false); 
                }
                GameObject environmentParent = GameObject.Find("EnvironmentsDestruction");
                if(environmentParent != null)
                {
                    environmentParent.SetActive(false);
                }
                saving.LoadData();
                if (saving.sceneManage != null)
                {
                    
                    foreach (var env in saving.sceneManage.enviroments)
                    {
                        GameObject prefab = null;
                        for(int i=0;i<nameTagPrefabEnvironments.Count;i++)
                        {
                            if (env.name == nameTagPrefabEnvironments[i])
                            {
                                prefab = enviromentsPrefab[i];
                            }
                        }
                        if (prefab != null)
                        {
                            Instantiate(prefab, env.position, Quaternion.identity);
                        }
                    }
                    foreach (var enemy in saving.sceneManage.enemiesInScene)
                    {
                        GameObject prefab_enemy = null;
                        for(int i = 0; i < nameTagPrefabEnemys.Count; i++)
                        {
                            if (enemy.name == nameTagPrefabEnemys[i])
                            {
                                prefab_enemy = enemysPrefab[i];
                            }
                        }
                        if (prefab_enemy != null)
                        {
                            Instantiate(prefab_enemy, enemy.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        PlayerPrefs.SetInt(sceneKey, 1);
    }
    private void Start()
    {
        if (sceneKey != null)
        {
            if (ApplicationVariables.numNameSaveScene >= ApplicationVariables.nameKeySaveScene.Count)
            {
                ApplicationVariables.nameKeySaveScene.Add(sceneKey);
                ApplicationVariables.numNameSaveScene++;
            }
            else
            {
                ApplicationVariables.nameKeySaveScene[ApplicationVariables.numNameSaveScene] = sceneKey;
            }
            
        }
    }
}
