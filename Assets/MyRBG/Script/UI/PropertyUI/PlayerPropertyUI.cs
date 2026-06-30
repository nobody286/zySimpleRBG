using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    public static PlayerPropertyUI Instance { get ; private set; }
    private GameObject uiGameobject;

    private Image hpProgressBar;
    private  TextMeshProUGUI hpText;

    private Image levelProgressBar;
    private TextMeshProUGUI levelText;

    private GameObject propertyGrid;
    private GameObject propertyTemplate;
    private Image WeaponIcon;

    private PlayerProperty pp;
    private PlayerAttack pa;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    void Start()
    {


        uiGameobject = transform.Find("proUI").gameObject;
        hpProgressBar = transform.Find("proUI/HPProgressBar/ProgressBar").GetComponent<Image>();
        hpText = transform.Find("proUI/HPProgressBar/HPText").GetComponent<TextMeshProUGUI>();
        levelProgressBar = transform.Find("proUI/LevelProgressBar/ProgressBar").GetComponent<Image>();
        levelText = transform.Find("proUI/LevelProgressBar/LevelText").GetComponent<TextMeshProUGUI>();
        propertyGrid = transform.Find("proUI/PropertyGrid").gameObject;
        propertyTemplate = transform.Find("proUI/PropertyGrid/PropertyTemplate").gameObject;

        WeaponIcon = transform.Find("proUI/WeaponIcon").GetComponent<Image>();

        propertyTemplate.SetActive(false);

        GameObject player =  GameObject.FindGameObjectWithTag(TagManager.PLAYER);

        pp = player.GetComponent<PlayerProperty>();
        pa = player.GetComponent<PlayerAttack>();

        UpdatePlayerPropertyUI();

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (uiGameobject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
    public void UpdatePlayerPropertyUI()
    {
        hpProgressBar.fillAmount = pp.hpValue / 100.0f;
        hpText.text = pp.hpValue + "/100";

        levelProgressBar.fillAmount = pp.currentExp * 1.0f/ (pp.level * 30);
        levelText.text = pp.level.ToString();

        ClearGrid();

        AddProperty("饥饿值:" + pp.energyValue); 
        AddProperty("精神值:" + pp.mentalValue);

        foreach (var item in pp.propertyDict)
        {
            string propertyName = "";
            switch (item.Key)
            {
                case PropertyType.HPValue:
                    propertyName = "生命值:";
                    break;
                case PropertyType.EnergyValue:
                    propertyName = "饥饿值:";
                    break;
                case PropertyType.MentalValue:
                    propertyName = "精神值:";
                    break;
                case PropertyType.SpeedValue:
                    propertyName = "速度:";
                    break;
                case PropertyType.AttackValue:
                    propertyName = "攻击力:";
                    break;
            }
            int sum = 0;
            foreach (var item1 in item.Value)
            {
                sum += item1.value;
            }
            AddProperty(propertyName + sum);
        }
        if(pa.weaponIcon != null)
        {
            WeaponIcon.sprite = pa.weaponIcon;
        }
    }
    private void ClearGrid()
    {
        foreach (Transform child in propertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }
    private void AddProperty(string propertyStr)
    {
        GameObject go = GameObject.Instantiate(propertyTemplate);
        go.SetActive(true);
        go.transform.parent = propertyGrid.transform;
        go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
    }
    private void Show()
    {
        uiGameobject.SetActive(true);
    }
    private void Hide()
    {
        uiGameobject.SetActive(false);
    }
}
