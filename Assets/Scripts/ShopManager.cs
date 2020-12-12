using System.IO;
using System.Collections;
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
            DontDestroyOnLoad(gameObject);
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
    private int moneyAmount = 1000;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        itemPrefabs = inventoryManager.GetItemsPrefabs();

        string path = Application.persistentDataPath + "/GestaoBiomasSave.bin";

        // Desmarcar isso quando formos tirar o botao de load da cena do jogo e colocar no menu
        //if (!File.Exists(path))
        //{
            for (int i = 0; i <= 3; i++)
            {
                AddItemInShop(i);
            }
        //}
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

    public int GetMoneyAmount()
    {
        return moneyAmount;
    }

    public void SetMoneyAmount(int amount)
    {
        moneyAmount = amount;
    }
}
