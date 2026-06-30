using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;
    public int hpValue = 100;
    public int energyValue = 100;
    public int mentalValue = 100;
    public int level = 1;
    public int currentExp = 0;
    // Start is called before the first frame update
    void Awake()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());
        propertyDict.Add(PropertyType.SpeedValue, new List<Property>());

        AddProperty(PropertyType.SpeedValue, 5);
        AddProperty(PropertyType.AttackValue, 20);

        EventCenter.OnEnemyDied += OnEnemyDied;
    }
    public void UseDrug(ItemScriptObject itemSO)
    {
        foreach(Property p in itemSO.propertyList)
        {
            AddProperty(p.propertyType , p.value);
        }
    }
    public void AddProperty(PropertyType pt , int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                hpValue += value;
                return;
            case PropertyType.EnergyValue:
                energyValue += value;
                return;
            case PropertyType.MentalValue:
                mentalValue += value;
                return;
             
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt , value));
    }
    public void Remove(PropertyType pt , int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                hpValue -= value;
                return;
            case PropertyType.EnergyValue:
                energyValue -= value;
                return;
            case PropertyType.MentalValue:
                mentalValue -= value;
                return;

        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Remove(list.Find(x => x.value == value));
    }
    private void OnDestroy()
    {
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }
    private void OnEnemyDied(EnemyMove enemy)
    {
        this.currentExp += enemy.exp;
        if(currentExp >= level * 30)
        {
            currentExp -= level * 30;
            level++;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
}
