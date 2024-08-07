using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin,healthGlobe,staminaGlobe;

    public void DropItems()
    {
        int randomNum=Random.Range(1,5);
        if (randomNum == 1)
        {
            Instantiate(healthGlobe,transform.position,Quaternion.identity);
        }else if(randomNum == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }else if(randomNum == 3)
        {
            int random_num_coin=Random.Range(1,3);
            for(int i=0;i<random_num_coin;i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
       
    }
}
