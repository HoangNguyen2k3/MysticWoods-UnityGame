using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnerChest : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin,key;
    [SerializeField] private bool iskeychest=false;
    public void DropItems()
    {
        int randomNum = Random.Range(1, 2);
        if (randomNum == 2)
        {
            int random_num_coin = Random.Range(1, 5);
            for (int i = 0; i < random_num_coin; i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
        if (ApplicationVariables.taked_key == false&&iskeychest)
        {
            Debug.Log("Key chest spawn");
            Instantiate(key, transform.position, Quaternion.identity);
        }
        if (ApplicationVariables.taked_key == true)
        {
            Debug.Log("Chalice spawn");
            Instantiate(key, transform.position, Quaternion.identity);
        }
    }
}
