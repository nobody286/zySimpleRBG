using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public List<ItemScriptObject> itemList;
    //public ItemScriptObject defultWeapon;
    ////IEnumerator Start()
    ////{
    ////    yield return new WaitForSeconds(1);
    ////    Additem(defultWeapon);
    ////}
    public void Additem(ItemScriptObject item)
    {
        itemList.Add(item);
        InventoryUI.Instance.AddItem(item);

        MessageUI.Instance.Show("你获得了一个:" + item.name);
    }
    public void RemoveItem(ItemScriptObject itemSO)
    {
        itemList.Remove(itemSO);
    }
}
