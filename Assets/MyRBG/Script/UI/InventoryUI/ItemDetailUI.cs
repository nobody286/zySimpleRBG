using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public GameObject propertyGrid;
    public GameObject propertyTemplate;

    private ItemScriptObject itemSO;
    private itemUI itemUI;
    private void Start()
    {
        propertyTemplate.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void UpdateItemDetailUI(ItemScriptObject itemSO , itemUI itemUI)
    {
        this.itemSO = itemSO;
        this.itemUI = itemUI;
        this.gameObject.SetActive(true);
        string type = "";
        switch (itemSO.itemType)
        {
            case ItemType.Weapon:
                type = "武器";
                break;
            case ItemType.Consumable:
                type = "可消耗品";
                break;
        }
        iconImage.sprite = itemSO.icon;
        nameText.text = itemSO.name;
        typeText.text = type;
        descriptionText.text = itemSO.description;

        foreach(Transform child in propertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (ItemProperty property in itemSO.propertyList)
        {
            string propertyStr = "";
            string propertyName = "";
            switch (property.propertyType)
            {
                case ItemPropertyType.HPValue:
                    propertyName = "生命值:";
                    break;
                case ItemPropertyType.EnergyValue:
                    propertyName = "饥饿值:";
                    break;
                case ItemPropertyType.MentalValue:
                    propertyName = "精神值:";
                    break;
                case ItemPropertyType.SpeedValue:
                    propertyName = "速度:";
                    break;
                case ItemPropertyType.AttackValue:
                    propertyName = "攻击力:";
                    break;

            }
            propertyStr += propertyName;
            propertyStr += property.value;
            GameObject go = GameObject.Instantiate(propertyTemplate);
            go.SetActive(true);
            go.transform.parent = propertyGrid.transform;
            go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
        }                   

    }

    public void OnUseButtonClick()
    {
        InventoryUI.Instance.OnItemUse(itemSO, itemUI);
        this.gameObject.SetActive(false);
    }
}
