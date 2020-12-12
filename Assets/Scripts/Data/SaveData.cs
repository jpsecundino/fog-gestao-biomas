using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<float[]> soilPosList;
    public List<Soil> soilList;

    public List<float[]> plantsPosList;
    public List<PlantStruct> plantsList;

    public int moneyAmount;
    public List<int> shopList;
    public List<ItemStruct> inventoryList;

    [Serializable]
    public struct PlantStruct
    {
        public float health;
        public float water;
        public float nutrients;
        public float growthVelocity;
        public float productionPerSecond;
        public float profit;
        public float luminosity;

        public PlantStruct(float _health, float _water, float _nutrients, float _growthVelocity, float _productionPerSecond, float _profit, 
            float _luminosity)
        {
            health = _health;
            water = _water;
            nutrients = _nutrients;
            growthVelocity = _growthVelocity;
            productionPerSecond = _productionPerSecond;
            profit = _profit;
            luminosity = _luminosity;
        }
    }

    [Serializable]
    public struct ItemStruct
    {
        public int id;
        public int quantity;

        public ItemStruct(int _id, int _quantity)
        {
            id = _id;
            quantity = _quantity;
        }
    }


    public SaveData(Nature nature, GridMap gridmap, ShopManager shopManager, InventoryManager inventoryManager)
    {
        soilPosList = ConvertNaturePos(nature);
        soilList = ConvertNatureSoil(nature);
        plantsPosList = ConvertPlantsPos(gridmap);
        plantsList = ConvertPlants(gridmap);
        moneyAmount = shopManager.GetMoneyAmount();
        shopList = ConvertShop(shopManager);
        inventoryList = ConvertInventory(inventoryManager);
    }

    private List<float[]> ConvertNaturePos(Nature nature)
    {
        List<float[]> list = new List<float[]>();

        foreach (KeyValuePair<Vector3, Soil> soil in nature.soilGrid)
        {
            list.Add(new float[3] { soil.Key.x, soil.Key.y , soil.Key.z });
        }

        return list;
    }

    private List<Soil> ConvertNatureSoil(Nature nature)
    {
        List<Soil> list = new List<Soil>();

        foreach (KeyValuePair<Vector3, Soil> soil in nature.soilGrid)
        {
            list.Add(soil.Value);
        }

        return list;
    }

    private List<float[]> ConvertPlantsPos(GridMap plantsGrid)
    {
        List<float[]> list = new List<float[]>();

        foreach (KeyValuePair<Vector3, GameObject> plant in plantsGrid.grid)
        {
            list.Add(new float[3] { plant.Key.x, plant.Key.y, plant.Key.z });
        }

        return list;
    }
    
    private List<PlantStruct> ConvertPlants(GridMap plantsGrid)
    {
        List<PlantStruct> list = new List<PlantStruct>();
        Plant plantClass;

        foreach (KeyValuePair<Vector3, GameObject> plant in plantsGrid.grid)
        {
            plantClass = plant.Value.GetComponent<Plant>();
            PlantStruct plantStruct = new PlantStruct(plantClass.health, plantClass.water, plantClass.nutrients, plantClass.growthVelocity,
                plantClass.productionPerSecond, plantClass.profit, plantClass.luminosity);
            list.Add(plantStruct);
        }

        return list;
    }

    private List<int> ConvertShop(ShopManager shopManager)
    {
        List<InventoryItem> list = shopManager.GetListItemsShop();
        List<int> lista = new List<int>();

        foreach (InventoryItem item in list)
        {
            lista.Add(item.id);
        }
        return lista;
    }

    private List<ItemStruct> ConvertInventory(InventoryManager inventoryManager)
    {
        List<ItemStruct> list = new List<ItemStruct>();
        List<Item> lista = inventoryManager.GetListItems();

        foreach(Item item in lista)
        {
            list.Add(new ItemStruct(item.inventoryItem.id, item.quantity));
        }

        return list;
    }
}