using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

[CreateAssetMenu()]
public class ItemScriptObject : ScriptableObject
{
    public int id;
    public string name;
    public ItemType itemType;
    public string description;
    public List<Property> propertyList;
    public Sprite icon;
    public GameObject prefab;

}
public enum ItemType
{
    Weapon,
    Consumable
}
[Serializable]
public class Property
{
    public PropertyType propertyType;
    public int value;
    public Property()
    {

    }
    public Property(PropertyType propertyType , int value)
    {

    }
}
public enum PropertyType
{
    HPValue,
    EnergyValue,
    MentalValue,
    SpeedValue,
    AttackValue
}
