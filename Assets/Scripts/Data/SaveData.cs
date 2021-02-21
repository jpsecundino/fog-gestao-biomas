using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<float[]> soilPosList;
    public List<SoilStruct> soilList;

    public List<float[]> plantsPosList;
    public List<PlantStruct> plantsList;

    public float moneyAmount;
    public List<int> shopList;
    public List<ItemStruct> inventoryList;

    public int index;
    public float playingTime;
    public int day;
    public float inGameTime;

    [Serializable]
    public struct SoilStruct
    {
        public float availableNutrients;
        public float nutrientGenerationRate;
        public float maxNutrients;

        public SoilStruct(float _availableNutrients, float _nutrientGenerationRate, float _maxNutrients)
        {
            availableNutrients = _availableNutrients;
            nutrientGenerationRate = _nutrientGenerationRate;
            maxNutrients = _maxNutrients;
        }
    }

    [Serializable]
    public struct PlantStruct
    {
        public int id;
        public float[] rotation;
        public float health;
        public float water;
        public float nutrients;
        public float growthVelocity;
        public float productionPerSecond;
        public float profit;
        public float luminosity;

        public PlantStruct(int _id, float[] _rotation, float _health, float _water, float _nutrients, float _growthVelocity, float _productionPerSecond, float _profit,
            float _luminosity)
        {
            id = _id;
            rotation = new float[3];
            rotation[0] = _rotation[0];
            rotation[1] = _rotation[1];
            rotation[2] = _rotation[2];
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


    public SaveData(Nature nature, GridMap gridmap, ShopManager shopManager, InventoryManager inventoryManager, TimeController timeController, int _index)
    {
        soilPosList = ConvertNaturePos(nature);
        soilList = ConvertNatureSoil(nature);
        plantsPosList = ConvertPlantsPos(gridmap);
        plantsList = ConvertPlants(gridmap);
        moneyAmount = shopManager.GetMoneyAmount();
        shopList = ConvertShop(shopManager);
        inventoryList = ConvertInventory(inventoryManager);
        index = _index;
        playingTime = GameManager.instance.playingTime;
        day = timeController.days;
        inGameTime = timeController.time;
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

    private List<SoilStruct> ConvertNatureSoil(Nature nature)
    {
        List<SoilStruct> list = new List<SoilStruct>();

        foreach (KeyValuePair<Vector3, Soil> soil in nature.soilGrid)
        {
            list.Add(new SoilStruct(soil.Value.availableNutrients, soil.Value.nutrientGenerationRate, soil.Value.maxNutrients));
        }

        return list;
    }

    private List<float[]> ConvertPlantsPos(GridMap plantsGrid)
    {
        List<float[]> list = new List<float[]>();

        foreach (KeyValuePair<GameObject, Vector3> plant in plantsGrid.grid)
        {
            list.Add(new float[3] { plant.Value.x, plant.Value.y, plant.Value.z });
        }

        return list;
    }
    
    private List<PlantStruct> ConvertPlants(GridMap plantsGrid)
    {
        List<PlantStruct> list = new List<PlantStruct>();
        Plant plantClass;
        PlantStruct plantStruct;

        foreach (KeyValuePair<GameObject, Vector3> plant in plantsGrid.grid)
        {
            plantClass = plant.Key.GetComponent<Plant>();
            plantStruct = new PlantStruct(plantClass.GetId(), new float[3] { plantClass.gameObject.transform.rotation.x,
                plantClass.gameObject.transform.rotation.y, plantClass.gameObject.transform.rotation.z },  plantClass.health, plantClass.water,
                plantClass.nutrients, plantClass.growthVelocity, plantClass.productionPerSecond, plantClass.profit, plantClass.luminosity);
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