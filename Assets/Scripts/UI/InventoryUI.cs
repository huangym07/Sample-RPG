using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    private GameObject uiGameObject;
    public GameObject itemPrefab;

    private GameObject content;
    private bool isShow = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); return;
        }
        Instance = this;
    }

    private void Start()
    {
        uiGameObject = transform.Find("UI").gameObject;
        uiGameObject?.SetActive(false);
        content = transform.Find("UI/ListBg/Scroll View/Viewport/Content").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isShow)
            {
                Hide();
                isShow = false;
            } else
            {
                Show();
                isShow = true;
            }
        }
    }
    public void Show()
    {
        uiGameObject?.SetActive(true);
    }
    public void Hide()
    {
        uiGameObject?.SetActive(false);
    }

    public void AddItem(ItemSO itemSO)
    {
        GameObject itemGameObject = GameObject.Instantiate(itemPrefab);
        itemGameObject.transform.SetParent(content.transform);
        //itemGameObject.transform.parent = content.transform; // VS 报警告，让使用 SetParent()    itemGameObject.transform.SetParent(content.transform);

        ItemUI itemUI = itemGameObject.GetComponent<ItemUI>();
        
        itemUI.InitItem(itemSO);
    }

    public void OnItemClick(ItemSO itemSO, ItemUI itemUI)
    {
        uiGameObject.transform.Find("ItemDetailUI").GetComponent<ItemDetailUI>().UpdateUI(itemSO, itemUI);
        //print("应该更新细节页面了");
    }
    public void OnUseClick(ItemSO itemSO, ItemUI itemUI)
    {
        // 移除 itemUI
        Destroy(itemUI.gameObject);
        // 删除 InventoryManager 中的数据
        InventoryManager.Instance.RemoveItem(itemSO);
        // 对武器或者消耗品的功能性处理，由于这两种 item 的效果都作用于 Player，所以处理移交给 Player
        GameObject.FindWithTag(Tags.PLAYER).GetComponent<Player>().UseItem(itemSO);
    }
}
