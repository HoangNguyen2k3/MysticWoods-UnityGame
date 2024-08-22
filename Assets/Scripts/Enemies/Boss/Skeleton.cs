using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemy
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private Collider2D damageCollider; // Reference to the damage collider
    [SerializeField] private Vector2 damageColliderOffset; // Offset for the damage collider position
    [SerializeField] private Vector2 damageColliderFlippedOffset; // Offset for the damage collider when flipped
    [SerializeField] private int DistanceFollowPlayer=10;

    [SerializeField] private GameObject slashBossRight;
    [SerializeField] private GameObject slashBossLeft;
    private bool turnleft=true;
    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageCollider.enabled = false; // Disable the collider initially
        slashBossLeft.SetActive(false);
        slashBossRight.SetActive(false);
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Update()
    {
        if (target != null && Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) <DistanceFollowPlayer)
        {
            speed = 5f;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (transform.position.x - Playercontroller.Instance.transform.position.x < 0)
            {
                spriteRenderer.flipX = false;
                damageCollider.offset = damageColliderOffset; // Set collider position for non-flipped state
                turnleft = true;
            }
            else
            {
                spriteRenderer.flipX = true;
                damageCollider.offset = damageColliderFlippedOffset; // Set collider position for flipped state
                turnleft=false;
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK_HASH);
       // isAttacking = true;
        StartCoroutine(EnableDamageCollider());
    }

    private IEnumerator EnableDamageCollider()
    {
        // Enable the collider during the attack animation
        yield return new WaitForSeconds(0.7f);
        damageCollider.enabled = true;
        MusicManager.Instance.PlaySFX("2ndBossAttack");

        yield return new WaitForSeconds(0.2f); // Adjust the duration based on the attack animation length
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && damageCollider.enabled)
        {
            PlayerHealth.Instance.TakeDamage(1,damageCollider.transform);
        }
    }
    public IEnumerator AttackSlashAddBoss()
    {
        if (turnleft)
        {
            //Instantiate(slashBossLeft,slashBossLeft.transform.position, Quaternion.identity);
            slashBossLeft.gameObject.SetActive(true);
        }
        else
        {
            //Instantiate(slashBossRight, slashBossRight.transform.position, Quaternion.identity);
            slashBossRight.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
        slashBossLeft.gameObject.SetActive(false);
        slashBossRight.gameObject.SetActive(false);
    }

}
