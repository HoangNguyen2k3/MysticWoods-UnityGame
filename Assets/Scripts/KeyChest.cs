using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator=GetComponent<Animator>();    
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Playercontroller>() != null)
        {
            animator.SetTrigger("Open");
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<PickUpSpawnerChest>().DropItems();
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
