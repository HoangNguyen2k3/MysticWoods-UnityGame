using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawnOutOfMap : MonoBehaviour
{
    [SerializeField] private bool isDestroyGameObject = true;
    [SerializeField] private GameObject gameObjectSpawn;

    private  float mapMinX = -13.82f;
    private  float mapMaxX = 16.61f;
    private  float mapMinY = -9.63f;
    private  float mapMaxY = 8.76f;

    private void Update()
    {
        Vector2 position = transform.position;

        if (position.x < mapMinX || position.x > mapMaxX || position.y < mapMinY || position.y > mapMaxY)
        {
            if(isDestroyGameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                Instantiate(gameObjectSpawn,spawnPosition,Quaternion.identity);
                Destroy(gameObject);
            }
         
        }
    }

}
