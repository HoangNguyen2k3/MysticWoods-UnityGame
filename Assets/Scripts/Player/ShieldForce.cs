using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldForce : MonoBehaviour
{
    private float timePreventDamage = 2f;
    [SerializeField] private GameObject particalOnHitPrefabVFX;
    


    private void Update()
    {
        StartCoroutine(ShieldForce_1());
        transform.position=Playercontroller.Instance.transform.position;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            if (collision.gameObject.GetComponent<Projectile>().isEnemyProjectile)
            {
             Instantiate(particalOnHitPrefabVFX, collision.gameObject.GetComponent<Projectile>().gameObject.transform.position, collision.gameObject.GetComponent<Projectile>().gameObject.transform.rotation);
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("ProjectileEnemy"))
        {
            Instantiate(particalOnHitPrefabVFX, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
        PlayerHealth.Instance.canTakeDamage = false;
    }
    private IEnumerator ShieldForce_1()
    {
        yield return new WaitForSeconds(timePreventDamage);
        PlayerHealth.Instance.canTakeDamage = true;
        Destroy(gameObject);
       
    }
}
