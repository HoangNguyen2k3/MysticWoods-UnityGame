using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRoadToSnowBoss : MonoBehaviour
{
    [SerializeField] private Transform positionPortal;
    [SerializeField] private GameObject Portal;
    [SerializeField] private int numEnemy=4 ;
    private GameObject portal;
    [SerializeField] private int maxEnemyInMap=5;
    [SerializeField] private int maxEnemySpawn = 10;
    [SerializeField] private GameObject EnemySpawn;
    private int currentEnemySpawn=0;
    private int currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        numEnemy = GameObject.FindGameObjectsWithTag("SnowSlime").Length;
        Debug.Log(numEnemy);
        currentEnemySpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        numEnemy = GameObject.FindGameObjectsWithTag("SnowSlime").Length;
        currentEnemy = numEnemy;
        if (numEnemy == 0) {
        portal=Instantiate(Portal, positionPortal.position,Quaternion.identity);


        }
        else
        {
            if (currentEnemySpawn <= maxEnemySpawn)
            {
                if (currentEnemy < maxEnemyInMap)
                {
                    Vector2 positionPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
                    int temp = maxEnemyInMap - currentEnemy;
                    for (int i = 1; i <= temp; i++)
                    {
                        float angle = Random.Range(0f, Mathf.PI * 2f);
                        Vector2 positionRandomSpawn = positionPlayer + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 5f;
                        Instantiate(EnemySpawn, positionRandomSpawn, Quaternion.identity);
                        currentEnemySpawn++;
                    }
                }
            }
            
        }
        if (portal != null)
        {
            Destroy(gameObject);
        }

    }
}
