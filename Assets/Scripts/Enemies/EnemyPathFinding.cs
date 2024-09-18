using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockback;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool reverse_Enemies = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(WaitReverseDir());
    }
    private IEnumerator WaitReverseDir()
    {
        
        yield return new WaitForSeconds(0.5f);
        moveDir = -moveDir;
        Debug.Log("Detect something");
    }
    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (reverse_Enemies == false)
        {
            if (moveDir.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveDir.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {

            if (moveDir.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveDir.x < 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

}
