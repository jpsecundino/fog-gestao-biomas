using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine;


public class PlantPlacer : MonoBehaviour
{
    public PlantSO plantSelected; //planta selecionada no momento
    private GameObject plant = null;
    private GridMap gridMap = null;
    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject gObject; // objeto que vai ser destruido
    private InventoryManager inventoryManager;
    
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

    [SerializeField] private struct HoverObj { 
        public GameObject plantHoverPrefab { get; set; }
        public MeshRenderer plantPrefabRenderer { get; set; }

        public PlantSO plantSelectedSO { get; set; }

        public HoverObj(PlantSO plant)
        {
            plantHoverPrefab = Instantiate(plant.plantPrefab, new Vector3(50, 50, 50), plant.plantPrefab.transform.rotation);
            plantPrefabRenderer = plantHoverPrefab.GetComponentInChildren<MeshRenderer>();
            plantSelectedSO = plant;

        }

        public void OccupiedPos()
        {
            plantPrefabRenderer.material = plantSelectedSO.objHoverOcc;
        }

        public void FreePos()
        {
            plantPrefabRenderer.material = plantSelectedSO.objHoverFree;
        }

        public void MoveTo(Vector3 pos, GridMap grid)
        {
            plantHoverPrefab.transform.position = grid.GetNearestPointOnGrid(pos);
        }

        public void Rotate(float angle)
        {
            plantHoverPrefab.transform.Rotate(Vector3.up, angle);
            Debug.Log("Rodei: " + plantHoverPrefab.transform.rotation);
        }

    };

    private HoverObj hoverObj;
    public bool canPlaceOrRemove { get; set; } = true;

    void Start()
    {
        
        //change it when we get the OnChangeItem Event -> only instantiate a hover object when change plant
        hoverObj = new HoverObj(plantSelected);
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
            Rotate();
            Hover();
        }

    }

    private void Rotate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Debug.Log("asdf");
            hoverObj.Rotate(90f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            hoverObj.Rotate(-90f);
        }
    }

    private void Hover()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
            {
                Vector3 pos = hitInfo.point;
                if (gridMap.IsPositionFree(pos))
                {
                    hoverObj.FreePos();
                    hoverObj.MoveTo(pos, gridMap);
                }
                else
                {
                    hoverObj.OccupiedPos();
                    hoverObj.MoveTo(pos, gridMap);

                }  
            }
        }

    }

    //corrigir: pegar referencia da planta no grid e não pelo raycast
    private void OnRightMouseClick()
    {
        if (Input.GetMouseButton(1))
        {
           ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
           if (Physics.Raycast(ray, out hitInfo))
           {
                GameObject g = null;

                if (gridMap.RemoveObject(hitInfo.point, out g))
                {
                    inventoryManager.AddItem(hitInfo.transform.parent.GetComponent<InventoryItem>().id, 1);
                    Destroy(g);
                }

           }
        }
    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
                {
                    if(gridMap.PutObjectOngrid(hitInfo.point, hoverObj.plantHoverPrefab.transform.rotation, plantSelected.plantPrefab))
                    {
                        inventoryManager.RemovePlant(plant.GetComponent<InventoryItem>().id);
                    }

                }
            }
        }
    }

    public void SetPlant(GameObject buttonPlant)
    {
        plant = buttonPlant;
    }
}
