using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerWithPlayer : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Takedd Damage");
            PlayerHealth.Instance.TakeDamage(1, gameObject.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Takedd Damage 1");
            PlayerHealth.Instance.TakeDamage(1, gameObject.transform);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Takedd Damage 1");
            PlayerHealth.Instance.TakeDamage(1, gameObject.transform);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Takedd Damage 1");
            PlayerHealth.Instance.TakeDamage(1, gameObject.transform);
        }
    }
}
