using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] GameObject plant = null;

    private GridMap grid = null;
    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject gObject; // objeto que vai ser destruido

    public bool canPlaceOrRemove { get; set; } = true;

    void Start()
    {
        grid = FindObjectOfType<GridMap>();
    }

    void Update()
    {
        if (canPlaceOrRemove)
        {
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
                    GridMap.RemoveObject(hitInfo.point, out gObject);

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
                    GridMap.PutObjectOngrid(hitInfo.point, Quaternion.identity, plant);
                }
            }
        }
    }
}
