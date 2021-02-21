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

    public static Action GenerateNutrients;
    public float nutrientGeneratinTimeLoop;
    private float actualNutrientGenerationTime;

    public float time = 0f;
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
            GenerateNutrients();
            ShareNutrientsPrep();
        }
    }
    private void InitializeGrid()
    {
        
        for (float i = 0; i < gridMap.xSize * gridMap.BaseGridSize; i+= gridMap.BaseGridSize)
        {
            for (float j = 0; j < gridMap.zSize * gridMap.BaseGridSize ; j += gridMap.BaseGridSize)
            {   
                soilGrid.Add(new Vector3(i, 0, j), new Soil(5f, 0, 100));
            }
        }

    }

    public void ShareNutrientsPrep()
    {
        
        Vector3 randomSoil = new Vector3(UnityEngine.Random.Range(0, (int) ((gridMap.xSize - 1) * gridMap.BaseGridSize - 1)), 0, UnityEngine.Random.Range(0, (int) ((gridMap.zSize - 1)* gridMap.BaseGridSize)));
        
        Dictionary<Vector2, bool> _visited = new Dictionary<Vector2, bool>();
     
        ShareNutrients(randomSoil, _visited);
    }

    public void ShareNutrients(Vector3 currentSoilIdx, Dictionary<Vector2, bool> visited) {

        //if out of bounds
        if (!IsValidPosition(currentSoilIdx))
            return;

        int x = (int)currentSoilIdx.x, z = (int)currentSoilIdx.z;

        //if visited    
        if(visited.ContainsKey(new Vector2(currentSoilIdx.x, currentSoilIdx.z)))
            return;
        

        //mark as visited
        visited.Add(new Vector2(currentSoilIdx.x, currentSoilIdx.z), true);
        
        //Debug.Log("entrei");
        
        List<Vector3> neighboursPos = new List<Vector3>();

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
            if(IsValidPosition(neighbourPos)){
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

    public bool IsValidPosition(Vector3 pos){
        if (pos.x % gridMap.BaseGridSize != 0 || pos.z % gridMap.BaseGridSize != 0) return false;

        return IsInsideGrid(pos);
    }

    public bool IsInsideGrid(Vector3 pos)
    {
        return !(pos.x < 0 || pos.x >= gridMap.xSize * gridMap.BaseGridSize || pos.z < 0 || pos.z >= gridMap.zSize * gridMap.BaseGridSize);
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
