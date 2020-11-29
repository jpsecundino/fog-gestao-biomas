using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Nature : MonoBehaviour
{
    #region Singleton
    public static Nature instance;

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

    [SerializeField] private float xInitialRegion = 0;
    [SerializeField] private float zInitialRegion = 0;

    public static Action GenerateNutrients;
    public float timeLoop;
    private float actualTime;

    public static Dictionary<Vector3, Soil> soilGrid;
    private GridMap gridMap = null;

    private void Start()
    {
        gridMap = GridMap.instance;
        soilGrid = new Dictionary<Vector3, Soil>();
        InitializeGrid();
    }

    private void FixedUpdate()
    {
        actualTime += Time.deltaTime;

        if(actualTime >= timeLoop)
        {
            GenerateNutrients();
            actualTime = 0f;
        }
    }
    private void InitializeGrid()
    {
        
        for (int i = 0; i < gridMap.xSize; i++)
        {
            for (int j = 0; j < gridMap.zSize; j++)
            {
                //preeche uma porção inicial de solo
                if (i <= xInitialRegion && j <= zInitialRegion)
                {
                    Debug.Log("Inicializei o solo");
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(10f, 10f, 100));
                }
                else
                {
                    Debug.Log("Não Inicializei o solo");
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(0, 0, 100));
                }
            }
        }
    }

    public float GetAvailableNutrients(Vector3 pos)
    {
        return soilGrid[gridMap.GetNearestPointOnGrid(pos)].availableNutrients;
    }

    public void ConsumeNutrients(Vector3 pos, float consumeValue)
    {
        soilGrid[gridMap.GetNearestPointOnGrid(pos)].GiveNutrients(consumeValue);
    }


}
