using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] GameObject plant = null;

    private GridMap grid = null;

    void Start()
    {
        grid = FindObjectOfType<GridMap>();
    }

    void Update()
    {
        OnLeftMouseClick();
        OnRightMouseClick();
    }

    private void OnRightMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Plant"))
                {
                    GameObject g;
                    GridMap.RemoveObject(hitInfo.point, out g);

                    Destroy(g);
                }
            }
        }
    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    GridMap.PutObjectOngrid(hitInfo.point, Quaternion.identity, plant);
                }
            }
        }
    }
}
