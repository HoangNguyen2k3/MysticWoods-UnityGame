using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public Vector2 moveInput;
    private float move_Speed = 20f;
    private Rigidbody2D rb; // Assuming 2D, change to Rigidbody if using 3D

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        if (moveInput != Vector2.zero)
        {
            moveInput = moveInput.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * move_Speed * Time.fixedDeltaTime);
    }


}
