using UnityEngine.EventSystems;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer = default;

    private GameManager gameManager;
    private GameObject plantGameObject = null;
    private GridMap gridMap = null;
    private RaycastHit hitInfo;
    private Ray ray;
    private InventoryManager inventoryManager;
    private Nature nature;
    private SoundManager soundManager;
    public bool isHovering = false;
    
    #region Singleton

    public static PlantPlacer instance;

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

    #region HoverObj
    private class HoverObj {
        public GameObject plantHoverPrefab;
        public MeshRenderer plantPrefabRenderer;
        public PlantObject plantSelected;

        public HoverObj(GameObject plant)
        {
            plantHoverPrefab = Instantiate(plant, new Vector3(50, 50, 50), plant.transform.rotation);
            plantHoverPrefab.GetComponent<Plant>().enabled = false;
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
            plantHoverPrefab.transform.position = pos;
        }

        public void Rotate(float angle)
        {
            if (plantHoverPrefab)
                plantHoverPrefab.transform.Rotate(Vector3.up, angle);
        }
        public void Active(bool isActive)
        {
            if (plantHoverPrefab)
                plantHoverPrefab.SetActive(isActive);
        }

    };
    #endregion

    private HoverObj hoverObj;
    public bool canPlaceOrRemove = true;

    void Start()
    {
        gameManager = GameManager.instance;
        soundManager = SoundManager.instance;
        gridMap = GridMap.instance;
        nature = Nature.instance;
        inventoryManager = InventoryManager.instance;
        gameManager.OnInventoryClose += DisableHovering;
    }

    private void DisableHovering()
    {
        isHovering = false;
    }

    void Update()
    {
        if (canPlaceOrRemove)
        {
            if (plantGameObject)
                OnLeftMouseClick();

            if (isHovering && inventoryManager.HasItems() && hoverObj.plantPrefabRenderer)
            {
                Hover();
            }
        }
    }

    private void Hover()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, 1000f, groundLayer))
        {
            if (hitInfo.transform.CompareTag(StringsReferences.groundTag) && !EventSystem.current.IsPointerOverGameObject()) // Is pointer over UI
            {
                Vector3 pos = hitInfo.point;
                
                hoverObj.FreePos();
                hoverObj.MoveTo(pos, gridMap);

                if (!inventoryManager.HasItems())
                {
                    Debug.Log(inventoryManager.HasItems());
                    hoverObj.OccupiedPos();
                } 
                
            }
        }
        else
        {
            hoverObj.Active(false);
        }

    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, 1000f, groundLayer))
            {
                if(inventoryManager.HasItems() && InsideGrid(hitInfo.point))
                {
                    soundManager.PlaySound("Place Plant");
                    gridMap.PutObjectOngrid(hitInfo.point, hoverObj.plantHoverPrefab.transform.rotation, plantGameObject);
                    plantGameObject.GetComponent<Plant>().isPlaced = true;
                    inventoryManager.RemovePlant(plantGameObject.GetComponent<InventoryItem>().id);
                }
            }
        }
    }

    private bool InsideGrid(Vector3 position)
    {
        return nature.IsInsideGrid(position);
    }

    public void SetPlant(GameObject buttonPlant)
    {
        if (buttonPlant == null)
        {
            hoverObj.Active(false);
            return;
        }

        isHovering = true;

        if (hoverObj != null)
            Destroy(hoverObj.plantHoverPrefab);

        hoverObj = new HoverObj(buttonPlant);
        plantGameObject = buttonPlant;
    }
}
