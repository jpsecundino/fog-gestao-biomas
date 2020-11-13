using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nature : MonoBehaviour
{
    /*
    public float availableNutrients = 50f;
    public float nutrientGenerationRate = 10f;
    public float maxNutrients = 100f;
    */

    public struct Soil {
        public float availableNutrients { get; set; }
        public float nutrientGenerationRate { get; set; }
        public float maxNutrients { get; set; }
        //[SerializeField] private float acidity;
        //[SerializeField] private float moisture;
        public Soil(float _availableNutrients, float _nutrientGenerationRate, float _maxNutrients)
        {
            availableNutrients = _availableNutrients;
            nutrientGenerationRate = _nutrientGenerationRate ;
            maxNutrients = _maxNutrients ;
        }

    };

    [SerializeField] private float xInitialRegion = 0;
    [SerializeField] private float zInitialRegion = 0;

    public static Dictionary<Vector3, Soil> soilGrid;

    //a cada unidade de tempo, o solo se comunique:
    // solo -> solo
    // planta -> solo
    private void Start()
    {
        soilGrid = new Dictionary<Vector3, Soil>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < GridMap.xSize; i++)
        {
            for (int j = 0; j < GridMap.zSize; j++)
            {
                if (i <= xInitialRegion && j <= zInitialRegion)
                {
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(100f, 10f, 100));
                }
                else
                {
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(0, 0, 100));
                }
                Debug.Log(soilGrid[new Vector3(i, 0, j)].availableNutrients);
            }
        }
    }

    public static float GetAvailableNutrients(Vector3 pos)
    {
        return soilGrid[GridMap.GetNearestPointOnGrid(pos)].availableNutrients;
    }

    public static void ConsumeNutrients(Vector3 pos, float consumeValue)
    {
        Soil s = soilGrid[GridMap.GetNearestPointOnGrid(pos)];
        s.availableNutrients = Mathf.Clamp(soilGrid[GridMap.GetNearestPointOnGrid(pos)].availableNutrients - consumeValue, 0, soilGrid[GridMap.GetNearestPointOnGrid(pos)].maxNutrients);
        soilGrid[GridMap.GetNearestPointOnGrid(pos)] = s;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateNutrients();
        GiveNutrients();
    }

    private void GiveNutrients()
    {
    }

    private void GenerateNutrients()
    {

    }

    public void ConsumeFromSoil(Vector3 pos, float nutrients)
    {

    }
}
