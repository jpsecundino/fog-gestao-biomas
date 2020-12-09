using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Dictionary<Vector3, Soil> soilGrid;
    public Dictionary<Vector3, GameObject> plantGrid;
    public int moneyAmount;
    public List<InventoryItem> listShop;
    public List<Item> listInventory;

    public SaveData(Nature nature, GridMap gridmap, ShopManager shopManager, InventoryManager inventoryManager)
    {
        soilGrid = nature.soilGrid;
        plantGrid = gridmap.grid;
        moneyAmount = shopManager.GetMoneyAmount();
        listShop = shopManager.GetListItemsShop();
        listInventory = inventoryManager.GetListItems();
    }
}