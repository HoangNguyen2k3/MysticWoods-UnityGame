using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    public Vector3 direction; 
    public float speed = 5f; 

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
        void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            animator.SetTrigger("Destroy");
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamage(1);

        }
        else if (collision.gameObject.GetComponent<Indestructible>() && !collision.gameObject.GetComponent<Transparent_Detection>())
        {
            animator.SetTrigger("Destroy");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
