/*using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : Singleton<Playercontroller>
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;


 //   private PlayerController playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;
    private float startingMoveSpeed;
    private bool facingLeft = false;
    private bool isDashing = false;

    public Joystick movementJoyStick;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
      //  playerControls = new PlayerController();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myTrailRenderer.emitting = false;
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        //playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();
    }
    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
       AdjustPlayerFacingDirection();
        Move1();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {
        movement = new Vector2(movementJoyStick.Horizontal, movementJoyStick.Vertical);
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }
    private void Move1()
    {
        if (knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

    }
    private void AdjustPlayerFacingDirection()
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Quay m?t v? bên trái
            facingLeft = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Quay m?t v? bên ph?i
            facingLeft = false;
        }
    }
    public void Dash()
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            MusicManager.Instance.PlaySFX("Dash");
            PlayerHealth.Instance.canTakeDamage = false;
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed += dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        //moveSpeed -= dashSpeed;
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
        PlayerHealth.Instance.canTakeDamage = true;
    }
}*/












using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : Singleton<Playercontroller>
{
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private RuntimeAnimatorController[] characterAnimators;


    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;


    private PlayerController playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;
    private float startingMoveSpeed;
    private bool facingLeft = false;
    private bool isDashing = false;
    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerController();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myTrailRenderer.emitting = false;
        knockBack = GetComponent<KnockBack>();
       
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("CharacterInUse"))
        {
            PlayerPrefs.SetInt("CharacterInUse", 0);
        }
        if (PlayerPrefs.HasKey("Bower") && PlayerPrefs.GetInt("CharacterInUse") == 1)
        {
            ChangeCharacter(1);
        }
        else if (PlayerPrefs.HasKey("Samurai") && PlayerPrefs.GetInt("CharacterInUse") == 2)
        {
            ChangeCharacter(2);
        }
        else if (PlayerPrefs.HasKey("Magician") && PlayerPrefs.GetInt("CharacterInUse") == 3)
        {
            ChangeCharacter(3);
        }
        else
        {
            ChangeCharacter(0);
        }
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();
    }
    //Change Character
    public void ChangeCharacter(int characterIndex)
    {
        spriteRenderer.sprite = characterSprites[characterIndex];
        myAnimator.runtimeAnimatorController = characterAnimators[characterIndex];
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
       playerControls.Disable();
    }
    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move1();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }
    private void Move1()
    {
        if (knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 placeScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < placeScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }
    private void Dash()
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            MusicManager.Instance.PlaySFX("Dash");
            PlayerHealth.Instance.canTakeDamage = false;
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed += dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        //moveSpeed -= dashSpeed;
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
        PlayerHealth.Instance.canTakeDamage = true;
    }
}