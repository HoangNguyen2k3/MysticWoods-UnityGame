using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSnowSlime : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    public bool isLeft = true;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
       
    }
    private void Start()
    {
        SetFlip();
    }
    private void Update()
    {
        SetFlip();
    }
    public void SetFlip()
    {
        if (isLeft == true)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
