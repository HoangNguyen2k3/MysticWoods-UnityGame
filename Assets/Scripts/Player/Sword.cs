using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour,IWeapon
{


    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;//tao thoi gian hoi de su dung kiem
    [SerializeField] private WeaponInfo weaponInfo;
    private Transform weaponCollider;
    private Animator my_animator;
    //private PlayerController playercontroller;

    //private bool attackButtonDown, isAttacking = false; //2 bien quan ly viec su dung kiem cua player

    private GameObject slashAnim;
    private void Awake()
    {
    
        my_animator = GetComponent<Animator>();
        //playercontroller = new PlayerController();
    }
    private void Start()
    {
        weaponCollider = Playercontroller.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }
    /*private void OnEnable()
    {
        playercontroller.Enable();
    }*/
    /*   void Start()
      {
          playercontroller.Combat.Attack.started += _ => StartAttacking();
          playercontroller.Combat.Attack.canceled += _ => StopAttacking();
      }*/
    private void Update()
    {
        MouseFollowWithOffset();
    }
/*    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }*/
    public void Attack()
    {
       
            //isAttacking = true;
            my_animator.SetTrigger("Attack");
        MusicManager.Instance.PlaySFX("Sword");

        weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
           
        

    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        // isAttacking = false;
        Debug.Log("SwordAttacking");
    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (Playercontroller.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Playercontroller.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(Playercontroller.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
