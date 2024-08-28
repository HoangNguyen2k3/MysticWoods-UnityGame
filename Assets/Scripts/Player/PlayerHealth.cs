using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    public bool shield = false;
    private Slider heathSlider;
    public int currentHealth;
    public bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    readonly int DEATH_HASH = Animator.StringToHash("Death");
    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        if (PlayerPrefs.HasKey("Health"))
        {
            maxHealth = PlayerPrefs.GetInt("Health");
        }
    }
    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        UpdatHealthSlider();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy=collision.gameObject.GetComponent<EnemyAI>();
        if(enemy)
        {
            TakeDamage(1,collision.transform);
            
        }
       

    }
/*    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlashBossAttack"))
        {
            Debug.Log("take damage");
            TakeDamage(1, collision.transform);
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("danger"))
        {
            TakeDamage(1, collision.transform);
        }
      
    }

    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth ++;
            UpdatHealthSlider();
        }
    }
    public void TakeDamage(int damageAmount,Transform hitTransform)
    {
        if (shield == true)
        {
            MusicManager.Instance.PlaySFX("PlayerTakeDamage");
            ScreenShakeManager.Instance.ShakeScreen();
            knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
            shield = false;
            return;
        }
        if (!canTakeDamage) { return; }
        MusicManager.Instance.PlaySFX("PlayerTakeDamage");
        ScreenShakeManager.Instance.ShakeScreen();

        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
        StartCoroutine(DamageRecoveryRoutine());
        UpdatHealthSlider();
        CheckIfPlayerDeath();
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0&&!isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        SceneManager.LoadScene("GameOver");
    }
    private void UpdatHealthSlider()
    {
        if (heathSlider==null)
        {
            heathSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }
        heathSlider.maxValue = maxHealth;
        heathSlider.value = currentHealth;
    }
}
