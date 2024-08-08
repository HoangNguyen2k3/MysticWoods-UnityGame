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

    [SerializeField] FloatingHealthbar healthbar;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        healthbar = GetComponentInChildren<FloatingHealthbar>();
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
                Debug.Log("haha");
                ApplicationVariables.boss_alive = false;
                winner = true;
            }
            Instantiate(deathVFXPrefab,transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
            
           
        }
    }


}
