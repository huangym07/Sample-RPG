using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    private ItemSO itemSO;

    public void InitItem(ItemSO itemSO)
    {
        string itemType = "";
        switch (itemSO.itemType)
        {
            case ItemType.Weapon:
                itemType = "武器"; break;
            case ItemType.Consumable:
                itemType = "消耗品"; break;
        }
        iconImage.sprite = itemSO.icon;
        nameText.text = itemSO.itemName;
        typeText.text = itemType;
        this.itemSO = itemSO;
    }

    public void OnClick()
    {
        InventoryUI.Instance.OnItemClick(itemSO, this);
    }
}
