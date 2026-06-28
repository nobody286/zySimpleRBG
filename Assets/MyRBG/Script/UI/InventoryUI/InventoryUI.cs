using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    private GameObject content;
    private GameObject uiGanmeObject;
    public GameObject itemPrefab;
    private bool isShow = false;


    public ItemDetailUI itemDetailUI;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    void Start()
    {
        content = transform.Find("invUI/Listbg/Scroll View/Viewport/Content").gameObject;
        uiGanmeObject = transform.Find("invUI").gameObject;
        Hide();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isShow)
            {
                Hide();
                isShow = false;
            }
            else
            {
                Show();
                isShow = true;
            }
        }
    }
    public void Show()
    {
        uiGanmeObject.SetActive(true);
    }
    public void Hide()
    {
        uiGanmeObject.SetActive(false);
    }
    public void AddItem(ItemScriptObject itemSO)
    {
        GameObject itemGO =  GameObject.Instantiate(itemPrefab);
        itemGO.transform.SetParent(content.transform, false);
        itemUI itemUI = itemGO.GetComponent<itemUI>();

        itemUI.InitItem(itemSO);
    }
    public void OnItemClick(ItemScriptObject itemSO)
    {
        itemDetailUI.UpdateItemDetailUI(itemSO);
    }
}
