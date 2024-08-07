using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFaded spriteFaded;

    private void Awake()
    {
        spriteFaded = GetComponent<SpriteFaded>();
    }
    private void Start()
    {
        StartCoroutine(spriteFaded.SlowFadeRoutine());
        Invoke("DisableCollider", 0.2f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth=collision.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }
    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
