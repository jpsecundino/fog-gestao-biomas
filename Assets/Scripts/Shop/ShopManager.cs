using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Singleton

    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public delegate void OnItemShopChanged();
    public OnItemShopChanged onItemShopChangedCallback;

    public delegate void OnItemPrice();
    public OnItemPrice onItemPriceCallback;

    private GameObject[] itemPrefabs = null;
    private InventoryManager inventoryManager = null;

    private List<InventoryItem> items = new List<InventoryItem>();
    public float moneyAmount = 0;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        itemPrefabs = inventoryManager.GetItemsPrefabs();
    }

    public void AddItemInShop(int id)
    {
        InventoryItem newItem = itemPrefabs[id].GetComponent<InventoryItem>();
        items.Add(newItem);
        if (onItemShopChangedCallback != null)
            onItemShopChangedCallback.Invoke();
    }

    public void ClickItemInShop(int pos)
    {
        int i = 0;
        foreach (InventoryItem item in items)
        {
            if (i == pos && moneyAmount >= item.price)
            {
                moneyAmount -= item.price;
                inventoryManager.AddItem(item.id, 1);
                break;
            }
            i++;
        }
        if (onItemPriceCallback != null)
            onItemPriceCallback.Invoke();

    }
    public List<InventoryItem> GetListItemsShop()
    {
        return items;
    }
    public void SetListItemsShop(List<InventoryItem> list)
    {
        items = list;
    }

    public void EraseList()
    {
        items = new List<InventoryItem>();
    }

    public int GetMoneyAmount()
    {
        return (int)moneyAmount;
    }

    public void SetMoneyAmount(int amount)
    {
        moneyAmount = amount;
    }
}
