using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SceneManage
{
    public List<EnemiesInScene> enemiesInScene;
    public List<Enviroment> enviroments;
}

[System.Serializable]
public class EnemiesInScene
{
    public Vector3 position;
}
[System.Serializable]
public class Enviroment
{
    public string name;
    public Vector3 position;
}
public class SavingFile :MonoBehaviour
{
    public SceneManage sceneManage;

    private void Start()
    {
        LoadData();
    }
    
    public void LoadData()
    {
        string sceneName=SceneManager.GetActiveScene().name;
        string file = sceneName+"_save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            sceneManage = JsonUtility.FromJson<SceneManage>(json);
        }
        else
        {
            sceneManage = new SceneManage();
        }
    }
    public void SaveData() {
        string sceneName=SceneManager.GetActiveScene().name;
        string file = sceneName+"_save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        string json=JsonUtility.ToJson(sceneManage,true);
        File.WriteAllText(filePath, json);
    }
}
