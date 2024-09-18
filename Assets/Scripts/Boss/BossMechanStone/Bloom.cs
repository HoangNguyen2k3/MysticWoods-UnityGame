using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(2,transform);
        }
    }
    public void DestroyBloom()
    {
        Destroy(gameObject);
    }
}
