using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnBoss : MonoBehaviour
{
    [SerializeField] private GameObject chestBoss;


    public void DropBossChest()
    {
        Instantiate(chestBoss, transform.position, Quaternion.identity);
    }
}
