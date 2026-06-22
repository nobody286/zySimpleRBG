using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu()]
public class ItemScriptObject : ScriptableObject
{
    public int id;
    public string name;
    public ItemType itemType;
    public string description;
    public List<ItemProperty> propertyList;
    public Sprite icon;
    public GameObject prefab;

}
public enum ItemType
{
    Weapon,
    Consumable
}
[Serializable]
public class ItemProperty
{
    public ItemPropertyType propertyType;
    public int value;
}
public enum ItemPropertyType
{
    HPValue,
    EnergyValue,
    MentalValue,
    SpeedValue,
    AttackValue
}
