using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth=3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] bool isBoss = false;
    private bool winner = false;
    private int currentHealth;
    private Flash flash;
    private KnockBack knockBack;
    [SerializeField] bool secondaryBoss = false;
    [SerializeField] bool keyBoss = false;
    [SerializeField] bool flyingBoss = false;
    [SerializeField] private GameObject skeleton;
    [SerializeField] private Transform skeletonTransform;
    private Animator animator;

    [SerializeField] FloatingHealthbar healthbar;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        healthbar = GetComponentInChildren<FloatingHealthbar>();
        animator = GetComponent<Animator>();
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
    private void Start()
    {
        currentHealth = startingHealth;
        healthbar.UpdateHealthBar(currentHealth, startingHealth);

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(currentHealth, startingHealth);
        knockBack.GetKnockedBack(Playercontroller.Instance.transform,knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }
    public void HealingHealth(int numHeal)
    {
        if((currentHealth+numHeal)>startingHealth)
        {
            currentHealth = startingHealth;
        }
        else
        {
            currentHealth += numHeal;
        }
        
        healthbar.UpdateHealthBar(currentHealth, startingHealth);
    }
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            if (isBoss&&winner==false)
            {
                ApplicationVariables.boss_alive = false;
                winner = true;
            }
            if(deathVFXPrefab != null)
            {
                Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
                if (secondaryBoss)
                {
                    GetComponent<PickUpSpawnBoss>().DropBossChest();
                    if (ApplicationVariables.add_boss_skeleton > 0)
                    {
                        Instantiate(skeleton, skeletonTransform.position, Quaternion.identity);
                    }
                    ApplicationVariables.add_boss_skeleton--;
                }
                if (keyBoss)
                {
                    GetComponent<PickUpSpawnBoss>().DropBossChest();
                }
                if (flyingBoss)
                {
                    ApplicationVariables.bossFlyingNum--;
                }

                GetComponent<PickUpSpawner>().DropItems();
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(PlayDeathAnimator());
            }
            
           
            
           
        }
    }
    public IEnumerator PlayDeathAnimator()
    {
        if (gameObject.GetComponent<EnemyAI>())
        {
            gameObject.GetComponent<EnemyAI>().isDead = true;
        }
        
        gameObject.GetComponent<Collider2D>().enabled=false;
        float addTime = 0;
        if (gameObject.GetComponent<BossMechaStone>())
        {
            gameObject.GetComponent<BossMechaStone>().isDead = true;
            addTime = 1f;
        }
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+addTime);
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        if (secondaryBoss)
        {
            GetComponent<PickUpSpawnBoss>().DropBossChest();
            if (ApplicationVariables.add_boss_skeleton > 0)
            {
                Instantiate(skeleton, skeletonTransform.position, Quaternion.identity);
            }
            ApplicationVariables.add_boss_skeleton--;
        }
        if (keyBoss)
        {
            GetComponent<PickUpSpawnBoss>().DropBossChest();
        }
        if (flyingBoss)
        {
            ApplicationVariables.bossFlyingNum--;
        }
        if (gameObject.GetComponent<PickUpSpawner>())
        {
            GetComponent<PickUpSpawner>().DropItems();
        }
        
        Destroy(gameObject);
    }

}
