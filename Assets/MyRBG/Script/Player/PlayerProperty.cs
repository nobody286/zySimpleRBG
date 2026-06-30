using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;
    public int hpValue = 100;
    public int energyValue = 100;
    public int mentalValue = 100;
    // Start is called before the first frame update
    void Start()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        propertyDict.Add(PropertyType.HPValue , new List<Property>());
        propertyDict.Add(PropertyType.EnergyValue, new List<Property>());
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());
        propertyDict.Add(PropertyType.MentalValue, new List<Property>());
        propertyDict.Add(PropertyType.SpeedValue, new List<Property>());

        AddProperty(PropertyType.SpeedValue, 5);
        AddProperty(PropertyType.AttackValue, 20);
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
}
