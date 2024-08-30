using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_player_skillQ : MonoBehaviour
{
    [SerializeField] private GameObject tornadoSkill;
    [SerializeField] private float speed;
    [SerializeField] private float spawnOffset;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        for(int i = 0; i < 9; i++)
        {
            float angle = 45 * (i+1);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 spawnPosition = transform.position + rotation * Vector3.forward * spawnOffset;
            GameObject tornado = Instantiate(tornadoSkill, spawnPosition, Quaternion.identity);
            tornado.GetComponent<TornadoMovement>().direction = rotation * Vector3.right;

        }
    }
    private void Update()
    {
        transform.Translate(Vector3.right*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            animator.SetTrigger("Destroy");
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            
            enemyHealth.TakeDamage(1);
            
        }
        else if (collision.gameObject.GetComponent<Indestructible>()&&!collision.gameObject.GetComponent<Transparent_Detection>())
        {
            animator.SetTrigger("Destroy");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
