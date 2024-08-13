using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDemon : MonoBehaviour,IEnemy
{
    [SerializeField] private GameObject projectile;
    private Transform target;
    [SerializeField] private float DistanceFollowPlayer=10f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 5f;
    private Transform target_fire;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {

        if (target != null && Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < DistanceFollowPlayer)
        {
            if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > 30)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (transform.position.x - Playercontroller.Instance.transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
                target_fire = right;
            }
            else
            {
                spriteRenderer.flipX = false;
                target_fire = left;
            }
        }

    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void Fire()
    {
        Instantiate(projectile, target_fire.position, Quaternion.identity);
    }
}