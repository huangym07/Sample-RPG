using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
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
    public PropertyType type;
    public int value;
    public Property()
    {

    }
    public Property(PropertyType propertyType, int value)
    {
        this.type = propertyType;
        this.value = value;
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
