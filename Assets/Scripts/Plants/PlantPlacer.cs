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
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject gObject; // objeto que vai ser destruido
    private InventoryManager inventoryManager;
    private bool isHovering = false;
    
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

        public PlantObject plantSelected { get; set; }

        public HoverObj(GameObject plant)
        {
            plantHoverPrefab = Instantiate(plant, new Vector3(50, 50, 50), plant.transform.rotation);
            plantPrefabRenderer = plantHoverPrefab.GetComponentInChildren<MeshRenderer>();
            plantSelected = plantHoverPrefab.GetComponent<Plant>().plantObject;

        }

        public void OccupiedPos()
        {
            plantPrefabRenderer.material = plantSelected.objHoverOcc;
        }

        public void FreePos()
        {
            plantPrefabRenderer.material = plantSelected.objHoverFree;
        }

        public void MoveTo(Vector3 pos, GridMap grid)
        {
            Vector3 newPos = grid.GetNearestPointOnGrid(pos);
            plantHoverPrefab.transform.position = new Vector3(newPos.x, grid.groundTransform.position.y, newPos.z);
        }

        public void Rotate(float angle)
        {
            plantHoverPrefab.transform.Rotate(Vector3.up, angle);
        }

    };

    private HoverObj hoverObj;
    public bool canPlaceOrRemove { get; set; } = true;

    void Start()
    {
       
        gridMap = GridMap.instance;
        inventoryManager = InventoryManager.instance;
        GameManager.OnInventoryClose += DisableHovering;
    }

    private void DisableHovering()
    {
        isHovering = false;
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

            if(isHovering)
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
            if (hitInfo.transform.CompareTag(StringsReferences.groundTag) && !EventSystem.current.IsPointerOverGameObject())
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
        if (Input.GetMouseButtonDown(1))
        {
           ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
           if (Physics.Raycast(ray, 1000f, groundLayer))
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
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, 1000f, groundLayer))
            {
                    Debug.Log("Cliquei");
                    if(gridMap.PutObjectOngrid(hitInfo.point, hoverObj.plantHoverPrefab.transform.rotation, plant))
                    {
                        inventoryManager.RemovePlant(plant.GetComponent<InventoryItem>().id);
                    }             
            }
        }
    }

    public void SetPlant(GameObject buttonPlant)
    {
        isHovering = true;
        hoverObj = new HoverObj(buttonPlant);
        plant = buttonPlant;
    }
}
