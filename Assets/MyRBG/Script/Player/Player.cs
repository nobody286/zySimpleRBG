using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }
    public void UseItem(ItemScriptObject itemSO)
    {
        switch (itemSO.itemType)
        {
            case ItemType.Weapon:
                playerAttack.LoadWeapon(itemSO);
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }
    }
}
