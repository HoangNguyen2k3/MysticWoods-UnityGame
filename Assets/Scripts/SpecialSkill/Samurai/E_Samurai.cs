using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Samurai : MonoBehaviour
{
    private Collider2D currentTarget;
    [SerializeField] private Transform teleportPositon;
    [SerializeField] private GameObject bloom;
    [SerializeField] private float teleportSpeed = 15f; // T?c ?? di chuy?n cao

    private Rigidbody2D playerRb;
    private bool isTeleporting = false;
    private Vector2 directTeleport= Vector2.zero;

    private void Awake()
    {
        // L?y Rigidbody2D c?a Player khi b?t ??u
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        directTeleport = (teleportPositon.position-Playercontroller.Instance.transform.position).normalized;
    }

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
            Instantiate(bloom, currentTarget.transform.position, Quaternion.identity);
        }
    }

    public void Destroy()
    {
        Debug.Log("Hakkk");
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (isTeleporting && playerRb != null)
        {
            Debug.Log(playerRb.position);
            playerRb.MovePosition(playerRb.position+directTeleport * teleportSpeed*Time.deltaTime);
            
            if (Vector2.Distance(playerRb.position, teleportPositon.position) < 0.5f)
            {
                isTeleporting = false;
                Debug.Log("End");
            }
        }
    }

    public void Teleport()
    {
        Debug.Log("Bat dau");
        isTeleporting = true; 
    }
}
