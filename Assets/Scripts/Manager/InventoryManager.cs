using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSO defaultWeapon;
    public List<ItemSO> itemList;
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this; 
    }

    // 生成一个默认的武器，但是 Start() 时，有许多其他的 Start() 也在执行（有一些 Start() 是在进行初始化，寻找到 GameObject 的引用等等），所以将本 Start() 设置为协程，并延迟一秒调用 AddItem()
    //IEnumerator Start()
    //{

    //    yield return new WaitForSeconds(1);
    //    AddItem(defaultWeapon);
    //}
    public void AddItem(ItemSO item)
    {

        itemList.Add(item);
        InventoryUI.Instance.AddItem(item);
    }

    public void RemoveItem(ItemSO itemSO)
    {
        itemList.Remove(itemSO);
    }
}
