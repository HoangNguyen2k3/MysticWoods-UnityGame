using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    GameObject key;
    [SerializeField] private GameObject destroyVFX;
    int health = 3;
    private void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
    }
    private void Update()
    {
        if (health == 0)
        {
            GetComponent<PickUpSpawner>().DropItems();
            if (ApplicationVariables.taked_chaliced == false)
            {
                GetComponent<PickUpSpawnerChest>().DropItems();
                Destroy(key);
            }
            
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSource>() || collision.gameObject.GetComponent<Projectile>())
        {
            health--;
        }
    }
}
