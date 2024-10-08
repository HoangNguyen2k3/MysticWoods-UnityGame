using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dragon_Boss_Phase2 : MonoBehaviour,IEnemy
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private Transform target;//Player tranform
    [SerializeField] private float speed;
    [SerializeField] private int DistanceFollowPlayer = 15;
    [SerializeField] private GameObject fireCirclePrefab; // Reference to the fire circle prefab
    [SerializeField] private float fireDuration = 1f; // Duration before damage is applied
    [SerializeField] private int fireDamage = 1; // Amount of damage to apply
    [SerializeField] private GameObject Fire;
    //3 flame appear
    [SerializeField] private Transform left1;
    [SerializeField] private Transform left2;
    [SerializeField] private Transform left3;
    [SerializeField] private Transform right1;
    [SerializeField] private Transform right2;
    [SerializeField] private Transform right3;
    //3 flame appear
    private Transform target_fire_1;
    private Transform target_fire_2;
    private Transform target_fire_3;
    private GameObject spawnedFire1;
    private GameObject spawnedFire2;
    private GameObject spawnedFire3;

    readonly int ATTACK_NO_BREATH = Animator.StringToHash("Attack");
    readonly int ATTACK_BREATH = Animator.StringToHash("Breath");
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Update()
    {
        
        if (target != null && Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < DistanceFollowPlayer)
        {
            if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > 10)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (transform.position.x - Playercontroller.Instance.transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
                target_fire_1 = right1;
                target_fire_2 = right2;
                target_fire_3 = right3;
            }
            else
            {
                spriteRenderer.flipX = false;
                target_fire_1 = left1;
                target_fire_2 = left2;
                target_fire_3 = left3;
            }
        }
        if(spawnedFire1 != null)
        {
            spawnedFire1.transform.position = Vector2.MoveTowards(spawnedFire1.transform.position, target.position, 3f * Time.deltaTime);
        }
        if (spawnedFire2 != null)
        {
            spawnedFire2.transform.position = Vector2.MoveTowards(spawnedFire2.transform.position, target.position, 3f * Time.deltaTime);
        }
        if (spawnedFire3 != null)
        {
            spawnedFire3.transform.position = Vector2.MoveTowards(spawnedFire3.transform.position, target.position, 3f * Time.deltaTime);
        }

    }

    public void Attack()
    {
        int rand_kind_attack = Random.Range(1, 5);
        if (rand_kind_attack >= 1 && rand_kind_attack <= 2)
        {
            animator.SetTrigger(ATTACK_NO_BREATH);
        }
        else
        {
            animator.SetTrigger(ATTACK_BREATH);
        }
    }
    public void Attack_No_Breath()
    {
        MusicManager.Instance.PlaySFX("CircleBoss");
        GameObject fireCircle = Instantiate(fireCirclePrefab, target.position, Quaternion.identity);
        Collider2D fireCollider = fireCircle.GetComponent<Collider2D>();
        StartCoroutine(DealDamageAfterDelay(fireCollider));
    }

    private IEnumerator DealDamageAfterDelay(Collider2D fireCollider)
    {
        yield return new WaitForSeconds(fireDuration-0.3f);
        if (fireCollider.bounds.Contains(target.position))
        {
            PlayerHealth.Instance.TakeDamage(fireDamage,target.transform); 
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(fireCollider.gameObject);
    }
    public void Attack_Breath()
    {
        MusicManager.Instance.PlaySFX("FireBoss");

        spawnedFire1 = Instantiate(Fire, target_fire_1.position, Quaternion.identity);
        spawnedFire2 = Instantiate(Fire, target_fire_2.position, Quaternion.identity);
        spawnedFire3 = Instantiate(Fire, target_fire_3.position, Quaternion.identity);
        StartCoroutine(FireWait(spawnedFire1));
        StartCoroutine(FireWait(spawnedFire2));
        StartCoroutine(FireWait(spawnedFire3));
    }

    private IEnumerator FireWait(GameObject spawnedFire)
    {
        yield return new WaitForSeconds(3f);
        Destroy(spawnedFire);
    }


}
