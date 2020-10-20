using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    // Struct para mapear quais plantas estão em qual lugar, para salvar o progresso
    private struct plantInfo
    {
        private GameObject plant;
        private int x;
        private int z;
    }

    [SerializeField] GameObject plant = null;

    private GridScript grid = null;
    //private GameObject[,] plantsPos = new GameObject[50, 50];

    void Start()
    {
        grid = FindObjectOfType<GridScript>();
        //Dar load nas posicoes das plantas
        // plantsPos = LoadPlantsPos
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {      
                    PlacePlantNear(hitInfo.point, plant);
                    /*
                    int x = Mathf.RoundToInt(hitInfo.point.x);
                    int z = Mathf.RoundToInt(hitInfo.point.z);
                    plantsPos[x, z] = plant;
                    Debug.Log("x = " + x + "\n" + "z = " + z);
                    Debug.Log(hitInfo.point);
                    */
                }
            }
        }
    }

    private void PlacePlantNear(Vector3 ClickPoint, GameObject plant)
    {
        Vector3 finalPos = grid.GetNearestPointOnGrid(ClickPoint);
        Instantiate(plant, finalPos, transform.rotation);
    }
}
