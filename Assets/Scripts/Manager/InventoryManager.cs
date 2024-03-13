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

    // ����һ��Ĭ�ϵ����������� Start() ʱ������������� Start() Ҳ��ִ�У���һЩ Start() ���ڽ��г�ʼ����Ѱ�ҵ� GameObject �����õȵȣ������Խ��� Start() ����ΪЭ�̣����ӳ�һ����� AddItem()
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
