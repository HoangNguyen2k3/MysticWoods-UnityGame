using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject miniBoss;
    [SerializeField] private Transform positionBossSpawn;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(OpenPortal());
    }
    private IEnumerator OpenPortal()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(1f);
        Instantiate(miniBoss,positionBossSpawn.position,Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
