using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    #region Singleton

    public static GridMap instance;

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

    public float BaseGridSize = 1f;
    public int xSize = 20;
    public int zSize = 20;

    public float numPlants;

    public Dictionary<Vector3, GameObject> grid;
    public Transform groundTransform = null;
    //public static Action<Vector3> OnNewSoil;

    void Start()
    {
        grid = new Dictionary<Vector3, GameObject>();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if (position.x > xSize || position.z > zSize) return default;

        int xCount = Mathf.RoundToInt(position.x / BaseGridSize);
        int zCount = Mathf.RoundToInt(position.z / BaseGridSize);

        Vector3 result = new Vector3(xCount * BaseGridSize, 0, zCount * BaseGridSize);

        return result;
    }

    public bool PutObjectOngrid(Vector3 position, Quaternion rotation, GameObject objPrefab)
    {
        Vector3 finalPosGrid = GetNearestPointOnGrid(position);
        
        Vector3 finalPosObj = finalPosGrid;

        finalPosObj.y = position.y;

        if (IsPositionFree(finalPosGrid))
        {
            grid.Add(finalPosGrid, Instantiate(objPrefab, finalPosObj, rotation));
            return true;
        }

        return false;

    }

    public bool RemoveObject(Vector3 clickPoint, out GameObject objectInGrid)
    {
        Vector3 nearestPoint = GetNearestPointOnGrid(clickPoint);
        // nearestPoint.y += groundTransform.position.y;
        Debug.Log("Click point: " + clickPoint);
        Debug.Log("Nearest point: " + nearestPoint);
        if (grid.TryGetValue(nearestPoint, out objectInGrid))
        {
            return grid.Remove(nearestPoint);
        }

        return objectInGrid;
    }

    public bool IsPositionFree(Vector3 position)
    {
        return !grid.ContainsKey(GetNearestPointOnGrid(position));
    }

    public GameObject GetObjectAtPosition(Vector3 position)
    {
        if (!IsPositionFree(position))
        {
            return grid[GetNearestPointOnGrid(position)];
        }
        else return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (float x = 0; x < xSize; x+= BaseGridSize)
        {
            for (float z = 0; z < zSize; z+= BaseGridSize)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                point.y += 1f;
                numPlants = xSize/BaseGridSize * zSize/BaseGridSize;
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
