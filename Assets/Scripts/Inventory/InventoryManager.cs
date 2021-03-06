﻿using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int quantity;
    public InventoryItem inventoryItem;

    public Item(int quantity, InventoryItem inventoryItem)
    {
        this.quantity = quantity;
        this.inventoryItem = inventoryItem;
    }
}
public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager instance;

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

    [SerializeField] private GameObject[] itemPrefabs = null;
    public Item selectedItem;
    public enum InventoryType
    {
        Plant,
        Seed
    }

    private List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    private PlantPlacer plantPlacer;


    void Start()
    {
        plantPlacer = PlantPlacer.instance;
        selectedItem = null;
    }

    public List<Item> GetListItems()
    {
        return items;
    }

    public void EraseList()
    {
        items = new List<Item>();
    }

    public void AddItem(int id, int quantity)
    {
        foreach (Item itemList in items)
        {
            if (itemList.inventoryItem.id == id)
            {
                itemList.quantity += quantity;
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }

        InventoryItem newItem = itemPrefabs[id].GetComponent<InventoryItem>();
        Item item = new Item(quantity, newItem);
        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemovePlant(int id)
    {
        foreach (Item itemList in items)
        {
            if (itemList.inventoryItem.id == id)
            {
                if (itemList.quantity > 1)
                {
                    itemList.quantity--;
                }
                else
                {
                    itemList.quantity--;
                    items.Remove(itemList);
                    plantPlacer.SetPlant(null);
                }
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }
    }

    public Item FindItemById(int id)
    {
        foreach (Item itemList in items)
        {
            if (itemList.inventoryItem.id == id)
            {
                return itemList;
            }
        }
        return null;
    }

    public bool HasItems()
    {

        if (selectedItem != null)
            return selectedItem.quantity > 0;
        else 
            return false;
    }

    public void ClickButtonInventory(int pos)
    {
        int i = 0;

        foreach (Item item in items)
        {
            if (i == pos)
            {
                int id = item.inventoryItem.id;

                plantPlacer.SetPlant(itemPrefabs[id]);
                selectedItem = item;
                return;
            }
            i++;
        }
        plantPlacer.SetPlant(null);
    }

    public GameObject[] GetItemsPrefabs()
    {
        return itemPrefabs;
    }
}
