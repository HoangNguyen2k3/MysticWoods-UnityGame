using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private GameObject particalOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    private Vector3 direction;  
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        direction = (targetPosition - startPosition).normalized;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particalOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (indestructible)
            {
                Instantiate(particalOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("ForceGround"))
        {
            Instantiate(particalOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World);
    }
}
