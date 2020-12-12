using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

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

    private PlantPlacer plantPlacer = null;
    private InfoManager infoManager = null;
    private Nature nature = null;
    private GridMap plantsGrid = null;
    private ShopManager shopManager = null;
    private InventoryManager inventoryManager = null;

    public Action OnInventoryClose;

    private void Start()
    {
        inventory.transform.SetParent(disableCanvas.transform, false);
        shop.transform.SetParent(disableCanvas.transform, false);
        plantPlacer = FindObjectOfType<PlantPlacer>();
        infoManager = FindObjectOfType<InfoManager>();
        nature = Nature.instance;
        plantsGrid = GridMap.instance;
        shopManager = ShopManager.instance;
        inventoryManager = InventoryManager.instance;
        placeButton.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
        infoButton.interactable = false;
        placeButton.interactable = true;
        plantPlacer.canPlaceOrRemove = false;
        infoManager.canShowInfo = true;
    }

    public void CloseInventoryButtonClick()
    {
        inventoryButton.interactable = true;
        inventory.transform.SetParent(disableCanvas.transform, false);
        OnInventoryClose();
    }

    public void OpenInventoryButtonClick()
    {
        if (inventoryManager.onItemChangedCallback != null)
            inventoryManager.onItemChangedCallback.Invoke();

        inventoryButton.interactable = false;
        inventory.transform.SetParent(mainCanvas.transform, false);
    }

    public void CloseShopButtonClick()
    {
        shopButton.interactable = true;
        shop.transform.SetParent(disableCanvas.transform, false);
    }

    public void OpenShopButtonClick()
    {
        if (shopManager.onItemShopChangedCallback != null)
            shopManager.onItemShopChangedCallback.Invoke();

        if (shopManager.onItemPriceCallback != null)
            shopManager.onItemPriceCallback.Invoke();

        shopButton.interactable = false;
        shop.transform.SetParent(mainCanvas.transform, false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void ClosePauseCanvas()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void ConfigurationsButtonClick()
    {
        pauseCanvas.SetActive(false);
        configurationsCanvas.SetActive(true);
    }

    public void CloseConfigurationsButtonClick()
    {
        configurationsCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(nature, plantsGrid, shopManager, inventoryManager);
    }
}
