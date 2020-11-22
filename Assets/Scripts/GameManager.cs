using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    [SerializeField] private GameObject inventory = null;
    [SerializeField] private GameObject disableCanvas = null;
    [SerializeField] private GameObject canvas = null;

    private PlantPlacer plantPlacer = null;
    private InfoManager infoManager = null;

    public static Action OnInventoryClose;

    private void Start()
    {
        inventory.transform.SetParent(disableCanvas.transform, false);
        plantPlacer = FindObjectOfType<PlantPlacer>();
        infoManager = FindObjectOfType<InfoManager>();
        placeButton.interactable = false;
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
        inventoryButton.interactable = false;
        inventory.transform.SetParent(canvas.transform, false);
    }
}
