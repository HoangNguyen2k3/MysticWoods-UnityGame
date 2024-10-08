/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;
    private int chooseSlot = 1;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
       ToggleActiveSlot((int)chooseSlot);
    }
    public void Sword()
    {
        chooseSlot=1;
    }
    public void Bow()
    {
        chooseSlot= 3;
    }
    public void Magic()
    {
        chooseSlot=2;
    }
    public void Random1()
    {
        chooseSlot =4;
    }
    public void Random2()
    {
        chooseSlot=5;
    }
    public void EquipStartingWeapon()
    {
        ToggleActiveHighLight(0);
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }
    private void ToggleActiveHighLight(int indexNum)
    {
        activeSlotIndexNum = indexNum;
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }


        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        //  ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        // newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;

    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = new PlayerController();
    }
    private void Start()
    {
        playerController.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }
    private void OnEnable()
    {
        playerController.Enable();
    }
    public void EquipStartingWeapon()
    {
        ToggleActiveHighLight(0);
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }
    private void ToggleActiveHighLight(int indexNum)
    {
        activeSlotIndexNum = indexNum;
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }


        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        //  ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        // newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

}
