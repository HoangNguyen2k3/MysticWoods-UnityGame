using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechaStone : MonoBehaviour
{
    [SerializeField] private GameObject Laser;
    [SerializeField] private Transform spawnLaser;
    [SerializeField] private GameObject StoneMelee;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform spawnBullet;
    private EnemyHealth enemyHealth;
    private bool canAttack = true;
    private float attackCoolDown = 3f;
    private Animator animator;
    public bool isDead = false;
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        int chieuthuc = Random.Range(0, 10);
        if (canAttack)
        {
            canAttack = false;
            if (chieuthuc >= 8)
            {
                AttackLaser();
            } else if (chieuthuc < 2)
            {
                Healing();
            }else if (chieuthuc < 5&&chieuthuc>=2)
            {
                AttackMelee();
            }else if (chieuthuc < 8 && chieuthuc >= 5)
            {
                AttackBullet();
            }
            StartCoroutine(AttackCooldownRoutine());
        }

    }
    void AttackBullet()
    {
        animator.SetTrigger("BulletAttack");
        
    }
    public void SpawnAttackBullet()
    {
        Instantiate(Bullet, spawnBullet.position, Quaternion.identity);
    }
    void AttackMelee()
    {
        animator.SetTrigger("MeleeAttack");
    }
    public void SpawnAttackStone()
    {
        Instantiate(StoneMelee, Playercontroller.Instance.transform.position, Quaternion.identity);
    }
    void AttackLaser()
    {
        animator.SetTrigger("LaserAttack");
        Instantiate(Laser, spawnLaser.position, Quaternion.identity);      
    }
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
    private void Healing()
    {
        animator.SetTrigger("Buff");
        StartCoroutine(HealingHealth());
    }
    private IEnumerator HealingHealth()
    {
        yield return new WaitForSeconds(0.8f);
        enemyHealth.HealingHealth(2);
    }
    
}
