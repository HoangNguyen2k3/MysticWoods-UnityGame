using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_Magician : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamage(1);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
