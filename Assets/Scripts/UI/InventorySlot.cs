using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weapon;
    public WeaponInfo GetWeaponInfo()
    {
        return weapon;
    }
}
