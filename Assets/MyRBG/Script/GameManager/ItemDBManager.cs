using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDBManager : MonoBehaviour
{
    public static ItemDBManager Instance { get; private set; }
    public ItemDBSO itemDB;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    public ItemScriptObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemDB.itemList.Count);
     
        return itemDB.itemList[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
