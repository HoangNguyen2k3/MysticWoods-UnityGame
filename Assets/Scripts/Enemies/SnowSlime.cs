using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnowSlime : MonoBehaviour,IEnemy
{
    [SerializeField] private TextMeshProUGUI appearPlayer;
    [SerializeField]  private float rangeAttack=6f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    [SerializeField] private GameObject addDamage;
    [SerializeField] private GameObject SpecialAttack;
    public bool isLeft = true;
    void Awake()
    {
        appearPlayer.text = string.Empty;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        addDamage.SetActive(false);
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > rangeAttack)
            {
                appearPlayer.text = "";


            }
            else
            {
                appearPlayer.text = "!";
                FollowPlayer();
            }
        }
    }
    private void FollowPlayer()
    {
        if (transform.position.x > Playercontroller.Instance.transform.position.x)
        {
            spriteRenderer.flipX = false;
            isLeft = true;
        }
        else
        {
            spriteRenderer.flipX = true;
            isLeft = false;
        }
    }
    public void Attack()
    {
        Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetTrigger("Attack");
        GameObject specialAttackInstance = Instantiate(SpecialAttack, playerPosition.position, Quaternion.identity);
        SpecialAttackSnowSlime specialAttackScript = specialAttackInstance.GetComponent<SpecialAttackSnowSlime>();
        specialAttackScript.isLeft = isLeft;
        addDamage.SetActive(true);
    }
    public void DestroyAttack()
    {
        addDamage.SetActive(false);
    }
}
