using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] GameObject plant = null;

    private GridScript grid = null;

    void Start()
    {
        grid = FindObjectOfType<GridScript>();
    }

    void Update()
    {
        OnLeftMouseClick();
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
                    grid.PutObjectOngrid(hitInfo.point, plant);
                }
            }
        }
    }
}
