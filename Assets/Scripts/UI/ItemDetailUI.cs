using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public GameObject propertyGrid;
    public GameObject propertyPrefab;
    public Button useButton;

    private ItemSO itemSO;
    private ItemUI itemUI;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void UpdateUI(ItemSO itemSO, ItemUI itemUI)
    {
        this.itemSO = itemSO;
        this.itemUI = itemUI;
        this.gameObject.SetActive(true);
        icon.sprite = itemSO.icon;
        nameText.text = itemSO.itemName;
        string itemType = "";
        switch(itemSO.itemType)
        {
            case ItemType.Weapon:
                itemType = "武器"; break;
            case ItemType.Consumable:
                itemType = "消耗品"; break;
        }
        typeText.text = itemType;
        descriptionText.text = itemSO.description;


        // TODO
        // 处理属性
        // 先销毁原有的属性
        for (int i = 0; i < propertyGrid.transform.childCount; i++)
        {
            Destroy(propertyGrid.transform.GetChild(i).gameObject);
        }
        // 枚举物品拥有的属性
        // 创建 Property，并设置 peopertyGrid 为父物体
        // 获得名称以及数值，设置成文本
        for (int i = 0; i < itemSO.propertyList.Count; i++)
        {
            GameObject itemProperty = GameObject.Instantiate(propertyPrefab);
            itemProperty.transform.SetParent(propertyGrid.transform);
            string propertyText = "";
            switch(itemSO.propertyList[i].type)
            {
                case PropertyType.HPValue:
                    propertyText += "生命"; break;
                case PropertyType.EnergyValue:
                    propertyText += "能量"; break;
                case PropertyType.MentalValue:
                    propertyText += "精神"; break;
                case PropertyType.SpeedValue:
                    propertyText += "速度"; break;
                case PropertyType.AttackValue:
                    propertyText += "攻击"; break;
            }
            propertyText += itemSO.propertyList[i].value;
            GameObject propertyTextGO = itemProperty.transform.Find("PropertyText").gameObject;
            propertyTextGO.GetComponent<TextMeshProUGUI>().text = propertyText;
        }
    }
    public void OnUseButtonClick()
    {
        InventoryUI.Instance.OnUseClick(itemSO, itemUI);
        gameObject.SetActive(false);
    }
}
