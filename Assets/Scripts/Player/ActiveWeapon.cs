using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
/*    [SerializeField] private MonoBehaviour currentActiveWeapon;
*/
    public MonoBehaviour CurrentActiveWeapon {  get; private set; }
    private PlayerController playerController;
    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking = false;
    protected override void Awake()
    {
        base.Awake();
        playerController = new PlayerController();
    }
    private void OnEnable()
    {
        playerController.Enable();
    }
    //add
    private void OnDisable()
    {
        playerController.Disable();
    }
    private void Start()
    {
        playerController.Combat.Attack.started += _ => StartAttacking();
        playerController.Combat.Attack.canceled += _ => StopAttacking();
        AttackCooldown();
    }
    private void Update()
    {
        Attack();
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon=newWeapon;
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }
    public void WeaponNull()
    {
        CurrentActiveWeapon=null;
    }
    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking=false;
    }
/*    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }*/
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    private void Attack()
    {
        if (attackButtonDown&&!isAttacking&&CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
            
        }
    }

}
