using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


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
public class Enviroment
{
    string name;
    public Vector3 position;
}
public class SavingFile : MonoBehaviour
{
    public SceneManage sceneManage;

    private void Start()
    {
        LoadData();
    }
    public void LoadData()
    {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        sceneManage=JsonUtility.FromJson<SceneManage>(File.ReadAllText(filePath));
    }
    public void SaveData() {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        string json=JsonUtility.ToJson(sceneManage);
        File.WriteAllText(filePath, json);
    }
}
