using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloom_Magician : MonoBehaviour
{
    private Collider2D currentTarget;

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
            enemyHealth.TakeDamage(2);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
