using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableObject : InteractableObject
{
    public ItemScriptObject itemSO;
    protected override void Interact()
    {
        Destroy(this.gameObject);
        InventoryManager.Instance.Additem(itemSO);
    }
}
