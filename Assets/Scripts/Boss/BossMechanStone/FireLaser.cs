using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private Transform target;
    [SerializeField] private GameObject bloom;
     private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        target=Playercontroller.Instance.transform;
        Vector3 direction=(-target.position+transform.position).normalized;
        float angle = Mathf.Atan2(direction.y,direction.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 180f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Playercontroller>())
        {
            Instantiate(bloom, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
    public void DestroyLaser()
    {
        Destroy(gameObject);
    }
    public void CheckCollider()
    {
        collider.enabled = true;
    }
}
