using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

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

    [Header("UI")]
    [SerializeField] private Button placeButton = null;
    [SerializeField] private Button infoButton = null;
    [SerializeField] private Button inventoryButton = null;
    [SerializeField] private Button shopButton = null;
    [SerializeField] private GameObject inventory = null;
    [SerializeField] private GameObject disableCanvas = null;
    [SerializeField] private GameObject mainCanvas = null;
    [SerializeField] private GameObject shop = null;
    [SerializeField] private GameObject pauseCanvas = null;
    [SerializeField] private GameObject configurationsCanvas = null;
    [SerializeField] private ScreenshotManager screenshotManager = null;

    public float playingTime = 0f;

    private PlantPlacer plantPlacer = null;
    private InfoManager infoManager = null;
    private Nature nature = null;
    private GridMap plantsGrid = null;
    private ShopManager shopManager = null;
    private InventoryManager inventoryManager = null;
    private GameObject[] gameObjects = null;
    private SoundManager soundManager;
    private TimeController timeController = null;

    public Action OnInventoryClose;

    private void Start()
    {
        soundManager = SoundManager.instance;
        inventory.transform.SetParent(disableCanvas.transform, false);
        shop.transform.SetParent(disableCanvas.transform, false);
        placeButton.interactable = false;
        plantPlacer = PlantPlacer.instance;
        infoManager = InfoManager.instance;
        nature = Nature.instance;
        plantsGrid = GridMap.instance;
        shopManager = FindObjectOfType<ShopManager>();
        inventoryManager = InventoryManager.instance;
        timeController = FindObjectOfType<TimeController>();
        gameObjects = inventoryManager.GetItemsPrefabs();

        string path = Application.persistentDataPath + SceneManagement.index + ".bin";
        Debug.Log(Application.persistentDataPath);

        if (File.Exists(path))
        {
            LoadGame(SceneManagement.index);
        }
        else
        {
            ShopManager.instance.moneyAmount = 20;
            Debug.Log(shopManager);
            
            for (int i = 0; i <= 3; i++)
            {
                ShopManager.instance.AddItemInShop(i);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        playingTime += Time.deltaTime;
    }

    public void PlaceButtonClick()
    {
        placeButton.interactable = false;
        infoButton.interactable = true;
        plantPlacer.canPlaceOrRemove = true;
        infoManager.canShowInfo = false;
    }

    public void InfoButtonClick()
    {
        soundManager.PlaySound("Button Click");
        infoButton.interactable = false;
        placeButton.interactable = true;
        plantPlacer.canPlaceOrRemove = false;
        infoManager.canShowInfo = true;
    }

    public void CloseInventoryButtonClick()
    {
        soundManager.PlaySound("Button Click");
        inventoryButton.interactable = true;
        inventory.transform.SetParent(disableCanvas.transform, false);
        OnInventoryClose();
    }

    public void OpenInventoryButtonClick()
    {
        if (inventoryManager.onItemChangedCallback != null)
            inventoryManager.onItemChangedCallback.Invoke();

        soundManager.PlaySound("Button Click");
        inventoryButton.interactable = false;
        inventory.transform.SetParent(mainCanvas.transform, false);
    }

    public void CloseShopButtonClick()
    {
        soundManager.PlaySound("Button Click");
        shopButton.interactable = true;
        shop.transform.SetParent(disableCanvas.transform, false);
    }

    public void OpenShopButtonClick()
    {
        if (shopManager.onItemShopChangedCallback != null)
            shopManager.onItemShopChangedCallback.Invoke();

        if (shopManager.onItemPriceCallback != null)
            shopManager.onItemPriceCallback.Invoke();

        soundManager.PlaySound("Button Click");
        shopButton.interactable = false;
        shop.transform.SetParent(mainCanvas.transform, false);
    }
    public void PauseGame()
    {
        soundManager.PlaySound("Button Click");
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void ClosePauseCanvas()
    {
        soundManager.PlaySound("Button Click");
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void ConfigurationsButtonClick()
    {
        soundManager.PlaySound("Button Click");
        pauseCanvas.SetActive(false);
        configurationsCanvas.SetActive(true);
    }

    public void CloseConfigurationsButtonClick()
    {
        soundManager.PlaySound("Button Click");
        configurationsCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void SaveGame(int index)
    {
        soundManager.PlaySound("Button Click");
        SaveSystem.SaveGame(nature, plantsGrid, shopManager, inventoryManager, timeController, index);
        screenshotManager.TakeScreenshot();
    }

    public void LoadGame(int index)
    {
        soundManager.PlaySound("Button Click");
        SaveData saveData = SaveSystem.LoadGame(index);
        nature.soilGrid = new Dictionary<Vector3, Soil>();
        plantsGrid.grid = new Dictionary<GameObject, Vector3>();

        for(int i = 0; i < saveData.soilList.Count; i++)
        {
            nature.soilGrid.Add(new Vector3(saveData.soilPosList[i][0], saveData.soilPosList[i][1], saveData.soilPosList[i][2]), 
                new Soil(saveData.soilList[i].availableNutrients, saveData.soilList[i].nutrientGenerationRate, saveData.soilList[i].maxNutrients));
        }

        for (int i = 0; i < saveData.plantsList.Count; i++)
        {
            Vector3 plantPosition = new Vector3(saveData.plantsPosList[i][0], saveData.plantsPosList[i][1], saveData.plantsPosList[i][2]);
            Quaternion plantRotation = new Quaternion(saveData.plantsList[i].rotation[0], saveData.plantsList[i].rotation[1], saveData.plantsList[i].rotation[2], 0);
            GameObject plant = Instantiate(gameObjects[saveData.plantsList[i].id], plantPosition, plantRotation);
            Plant plantClass = plant.GetComponent<Plant>();
            plantClass.health = saveData.plantsList[i].health;
            plantClass.water = saveData.plantsList[i].water;
            plantClass.nutrients = saveData.plantsList[i].nutrients;
            plantClass.growthVelocity = saveData.plantsList[i].growthVelocity;
            plantClass.productionPerSecond = saveData.plantsList[i].productionPerSecond;
            plantClass.profit = saveData.plantsList[i].profit;
            plantClass.luminosity = saveData.plantsList[i].luminosity;
            plantsGrid.grid.Add(plant, plantsGrid.GetNearestPointOnGrid(plantPosition));
            plantsGrid.realPositionPlants.Add(plant, plantPosition);
        }

        shopManager.SetMoneyAmount(saveData.moneyAmount);

        shopManager.EraseList();

        for (int i = 0; i < saveData.shopList.Count; i++)
        {
            shopManager.AddItemInShop(saveData.shopList[i]);
        }

        inventoryManager.EraseList();

        for (int i = 0; i < saveData.inventoryList.Count; i++)
        {
            inventoryManager.AddItem(saveData.inventoryList[i].id, saveData.inventoryList[i].quantity);
        }

        playingTime = saveData.playingTime;
        timeController.days = saveData.day;
        timeController.time = saveData.inGameTime;
    }
}
