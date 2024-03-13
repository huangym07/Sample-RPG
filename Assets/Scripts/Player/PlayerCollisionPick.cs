using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionPick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.INTERACTABLE)
        {
            PickableObject po = collision.gameObject.GetComponent<PickableObject>();
            if (po != null)
            {
                InventoryManager.Instance.AddItem(po.itemSO);
                Destroy(collision.gameObject);
            }
        }
    }
}
