using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Magician : MonoBehaviour
{
    [SerializeField] private GameObject bloom;
    private Vector3 direction;
    private float angle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>()|| collision.gameObject.GetComponent<Indestructible>())
        {
            Instantiate(bloom, gameObject.transform.position, Quaternion.identity);
            Destroy_E();
        }
       else  if (collision.gameObject.GetComponent<Indestructible>() )
        {
            Debug.Log("va cham");
            Instantiate(bloom, gameObject.transform.position, Quaternion.identity);
            Destroy_E();
        }
    }

    private void Start()
    {
        Vector3 vector3 = GameObject.Find("Player").transform.position;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        direction = (worldPosition - vector3).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * 8f * Time.deltaTime);
    }

    public void Destroy_E()
    {
        Destroy(gameObject);
    }
}
