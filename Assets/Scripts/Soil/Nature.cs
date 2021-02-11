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
    public float nutrientGeneratinTimeLoop;
    private float actualNutrientGenerationTime;

    public Dictionary<Vector3, Soil> soilGrid;
    private GridMap gridMap = null;
    

    private void Start()
    {
        gridMap = GridMap.instance;
        soilGrid = new Dictionary<Vector3, Soil>();
        InitializeGrid();
    }

    private void FixedUpdate()
    {
        actualNutrientGenerationTime += Time.deltaTime;
        
        //soil generates nutrients in every cycle
        if(actualNutrientGenerationTime >= nutrientGeneratinTimeLoop)
        {
            actualNutrientGenerationTime = 0f;
            //GenerateNutrients();
            //ShareNutrientsPrep();
        }
    }
    private void InitializeGrid()
    {
        
        for (int i = 0; i < gridMap.xSize; i++)
        {
            for (int j = 0; j < gridMap.zSize; j++)
            {   
                if(i == 2 && j == 2)
                {
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(0f, 0f, 100));
                }
                //preeche uma porção inicial de solo
                else if (i <= xInitialRegion && j <= zInitialRegion)
                {
                    //Debug.Log("Inicializei o solo");
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(10f, 0, 100));
                }
                else
                {
                    //Debug.Log("Não Inicializei o solo");
                    soilGrid.Add(new Vector3(i, 0, j), new Soil(0, 0, 100));
                }
            }
        }

    }

    public void ShareNutrientsPrep()
    {
        
        Vector3 randomSoil = new Vector3(UnityEngine.Random.Range(0, gridMap.xSize), 0, UnityEngine.Random.Range(0, gridMap.zSize));
        int[,] visited = new int[gridMap.xSize,gridMap.zSize];

        ShareNutrients(randomSoil, visited);
    }

    public void ShareNutrients(Vector3 currentSoilIdx, int[,] visited) {
        Debug.Log("Entrei");

        //if out of bounds
        if (!ValidPosition(currentSoilIdx))
            return;

        int x = (int)currentSoilIdx.x, z = (int)currentSoilIdx.z;

        //if visited    
        if(visited[x, z] == 1)
            return;
        
        /*
        if(currentSoilIdx.x == 2 && currentSoilIdx.z == 2)
        {
            Debug.LogWarning(soilGrid[currentSoilIdx].availableNutrients);
        }
        else
        {
            Debug.Log(soilGrid[currentSoilIdx].availableNutrients);
        }
        */

        //mark as visited
        visited[x, z] = 1;

        List<Vector3> neighboursPos = new List<Vector3>();

        if (gridMap.BaseGridSize != 1) 
        {
            Debug.LogWarning("Verifique o valor de BaseGridSize. O mesmo deve ser 1 para que o compartilhamento de nutrientes funcione.");
        }

        neighboursPos.Add(new Vector3(currentSoilIdx.x, 0,currentSoilIdx.z + gridMap.BaseGridSize));
        neighboursPos.Add(new Vector3(currentSoilIdx.x, 0,currentSoilIdx.z - gridMap.BaseGridSize));
        neighboursPos.Add(new Vector3(currentSoilIdx.x + gridMap.BaseGridSize,0, currentSoilIdx.z));
        neighboursPos.Add(new Vector3(currentSoilIdx.x - gridMap.BaseGridSize,0, currentSoilIdx.z));

        //get number of neighboursPos

        //visit neighboursPos
        ShareNutrients(neighboursPos[0], visited);
        ShareNutrients(neighboursPos[1], visited);
        ShareNutrients(neighboursPos[2], visited);
        ShareNutrients(neighboursPos[3], visited);
        
        //share nutrients
        foreach(Vector3 neighbourPos in neighboursPos){
            //if this soil has more nutrients than its current neighbour, share nutrients
            if(ValidPosition(neighbourPos)){
                float myNutrients = soilGrid[currentSoilIdx].availableNutrients;
                float theirNutrients = soilGrid[neighbourPos].availableNutrients;
                if(myNutrients > theirNutrients){
                    float nutDiff = myNutrients - theirNutrients;
                    soilGrid[neighbourPos].AddNutrients( nutDiff / 4);
                    soilGrid[currentSoilIdx].RemoveNutrients( nutDiff / 4);
                }
            }
        }

    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if (position.x > gridMap.xSize || position.z > gridMap.zSize) return default;

        int xCount = Mathf.RoundToInt(position.x / gridMap.BaseGridSize);
        int yCount = 0;
        int zCount = Mathf.RoundToInt(position.z / gridMap.BaseGridSize);

        Vector3 result = new Vector3(xCount * gridMap.BaseGridSize, yCount * gridMap.BaseGridSize, zCount * gridMap.BaseGridSize);

        return result;
    }

    public bool ValidPosition(Vector3 pos){
        return !(pos.x < 0 || pos.x >= gridMap.xSize || pos.z < 0 || pos.z >= gridMap.zSize);
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
