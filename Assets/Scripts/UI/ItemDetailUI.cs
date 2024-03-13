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
                itemType = "����"; break;
            case ItemType.Consumable:
                itemType = "����Ʒ"; break;
        }
        typeText.text = itemType;
        descriptionText.text = itemSO.description;


        // TODO
        // ��������
        // ������ԭ�е�����
        for (int i = 0; i < propertyGrid.transform.childCount; i++)
        {
            Destroy(propertyGrid.transform.GetChild(i).gameObject);
        }
        // ö����Ʒӵ�е�����
        // ���� Property�������� peopertyGrid Ϊ������
        // ��������Լ���ֵ�����ó��ı�
        for (int i = 0; i < itemSO.propertyList.Count; i++)
        {
            GameObject itemProperty = GameObject.Instantiate(propertyPrefab);
            itemProperty.transform.SetParent(propertyGrid.transform);
            string propertyText = "";
            switch(itemSO.propertyList[i].type)
            {
                case PropertyType.HPValue:
                    propertyText += "����"; break;
                case PropertyType.EnergyValue:
                    propertyText += "����"; break;
                case PropertyType.MentalValue:
                    propertyText += "����"; break;
                case PropertyType.SpeedValue:
                    propertyText += "�ٶ�"; break;
                case PropertyType.AttackValue:
                    propertyText += "����"; break;
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
