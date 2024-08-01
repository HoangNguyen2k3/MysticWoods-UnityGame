using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy=collision.gameObject.GetComponent<EnemyAI>();
        if(enemy&&canTakeDamage)
        {
            TakeDamage(1);
            knockBack.GetKnockedBack(collision.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }
    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
