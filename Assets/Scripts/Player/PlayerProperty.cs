using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;
    public int HPValue = 100;
    public int energyValue = 100;
    public int mentalValue = 100;
    public int level = 1;
    public int currenExp = 0;
    void Awake()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();

        propertyDict.Add(PropertyType.HPValue, new List<Property>());
        propertyDict.Add(PropertyType.MentalValue, new List<Property>());
        propertyDict.Add(PropertyType.EnergyValue, new List<Property>());
        propertyDict.Add(PropertyType.SpeedValue, new List<Property>());
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());

        EventCenter.onEnemyDied += OnEnemyDied;
    }

    public void AddProperty(PropertyType propertyType, int value)
    {
        switch (propertyType)
        {
            case PropertyType.HPValue:
                HPValue += value;
                break;
            case PropertyType .EnergyValue:
                energyValue += value;
                break;
            case PropertyType .MentalValue:
                mentalValue += value;
                break;
        }


        List<Property> list;
        propertyDict.TryGetValue(propertyType, out list);
        list.Add(new Property(propertyType, value));
    }
    public void RemoveProperty(PropertyType propertyType, int value)
    {
        switch (propertyType)
        {
            case PropertyType.HPValue:
                HPValue -= value;
                break ;
            case PropertyType.EnergyValue:
                energyValue -= value;
                break;
            case PropertyType.MentalValue:
                mentalValue -= value;
                break;
        }

        List<Property> list;
        propertyDict.TryGetValue(propertyType, out list);
        list.Remove(list.Find( x => x.value == value ));
    }
    public void UseConsumable(ItemSO itemSO)
    {
        foreach (Property p in itemSO.propertyList)
        {
            AddProperty(p.type, p.value);
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        this.currenExp += enemy.exp;
        if (this.currenExp >= level * 30)
        {
            this.currenExp -= level * 30;
            level++;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
}
