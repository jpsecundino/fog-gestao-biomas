using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;


public class PlantPlacer : MonoBehaviour
{
    private GridMap grid = null;
    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject gObject; // objeto que vai ser destruido
    public PlantSO plantSelected;
    private struct HoverObj { 
        public GameObject plantHoverPrefab { get; set; }
        public MeshRenderer plantPrefabRenderer { get; set; }

        public PlantSO plantSelectedSO { get; set; }


        public HoverObj(PlantSO plant)
        {
            plantHoverPrefab = Instantiate(plant.plantPrefab, new Vector3(50, 50, 50), Quaternion.identity);
            plantPrefabRenderer = plantHoverPrefab.GetComponent<MeshRenderer>();
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

        public void MoveTo(Vector3 pos)
        {
            plantHoverPrefab.transform.position = GridMap.GetNearestPointOnGrid(pos);
        }

    };

    private HoverObj hoverObj;
    public bool canPlaceOrRemove { get; set; } = true;

    void Start()
    {
        grid = FindObjectOfType<GridMap>();

        //change it when we get the OnChangeItem Event -> only instantiate a hover object when change plant
        
        hoverObj = new HoverObj(plantSelected);

    }

    void Update()
    {
        if (canPlaceOrRemove)
        {
            OnLeftMouseClick();
            OnRightMouseClick();
        }

        Hover();
    }

    private void Hover()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
            {
                Vector3 pos = hitInfo.point;
                if (GridMap.IsPositionFree(pos))
                {
                    hoverObj.FreePos();
                    hoverObj.MoveTo(pos);
                }
                else
                {
                    hoverObj.OccupiedPos();
                    hoverObj.MoveTo(pos);
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
                if (hitInfo.transform.CompareTag(StringsReferences.plantTag))
                {
                    GridMap.RemoveObject(hitInfo.point, out gObject);

                    Destroy(gObject);
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
                    GridMap.PutObjectOngrid(hitInfo.point, Quaternion.identity, plantSelected.plantPrefab);
                }
            }
        }
    }
}
