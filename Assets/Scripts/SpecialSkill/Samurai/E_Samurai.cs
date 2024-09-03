using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Samurai : MonoBehaviour
{
    private Collider2D currentTarget;
    [SerializeField] private Transform teleportPositon;
    [SerializeField] private GameObject bloom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            currentTarget = collision;
        }
    }

    public void Attack()
    {
        if (currentTarget != null)
        {
            EnemyHealth enemyHealth = currentTarget.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(1);
            Instantiate(bloom, currentTarget.transform.position,Quaternion.identity);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void Teleport()
    {
        GameObject.Find("Player").transform.position = teleportPositon.position;
    }
}
