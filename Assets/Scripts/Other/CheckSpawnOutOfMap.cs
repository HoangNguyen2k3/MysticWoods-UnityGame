using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawnOutOfMap : MonoBehaviour
{
    [SerializeField] private bool isDestroyGameObject = true;
    [SerializeField] private GameObject gameObjectSpawn;

    private  float mapMinX = -14f;
    private  float mapMaxX = 17f;
    private  float mapMinY = -10f;
    private float mapMaxY = 9f;

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
