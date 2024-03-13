using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractbleObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        InventoryManager.Instance.AddItem(itemSO);
        Destroy(gameObject);
        //print("Interacting with PickableObject");
    }
}
