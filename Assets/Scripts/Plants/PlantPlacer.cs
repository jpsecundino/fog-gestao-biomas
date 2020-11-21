using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    #region Singleton

    public static PlantPlacer instance;

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

    private GameObject plant = null;
    private GridMap gridMap = null;
    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject gObject; // objeto que vai ser destruido
    private InventoryManager inventoryManager;

    public bool canPlaceOrRemove { get; set; } = true;

    void Start()
    {
        gridMap = GridMap.instance;
        inventoryManager = InventoryManager.instance;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (canPlaceOrRemove)
        {
            if (plant)
                OnLeftMouseClick();

            OnRightMouseClick();
        }
    }

    private void OnRightMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag(StringsReferences.plantTag))
                {
                    gridMap.RemoveObject(hitInfo.point, out gObject);
                    inventoryManager.AddItem(hitInfo.transform.parent.GetComponent<InventoryItem>().id, 1);
                    Destroy(gObject);
                }
            }
        }
    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
                {
                    gridMap.PutObjectOngrid(hitInfo.point, plant.transform.rotation, plant);
                    inventoryManager.RemovePlant(plant.GetComponent<InventoryItem>().id);
                }
            }
        }
    }

    public void SetPlant(GameObject buttonPlant)
    {
        plant = buttonPlant;
    }
}
