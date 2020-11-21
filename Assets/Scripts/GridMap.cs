using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] public static float BaseGridSize = 1f;
    [SerializeField] public static float xSize = 20f;
    [SerializeField] public static float zSize = 20f;
    [SerializeField] private static Dictionary<Vector3, GameObject> grid { get; set; }
    //public static Action<Vector3> OnNewSoil;

    void Start()
    {
        grid = new Dictionary<Vector3, GameObject>();
    }

    public static Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if (position.x > xSize || position.z > zSize) return default;

        int xCount = Mathf.RoundToInt(position.x / BaseGridSize);
        int yCount = 0;
        int zCount = Mathf.RoundToInt(position.z / BaseGridSize);

        Vector3 result = new Vector3(xCount * BaseGridSize, yCount * BaseGridSize, zCount * BaseGridSize);


        return result;
    }

    public static bool PutObjectOngrid(Vector3 position, Quaternion rotation, GameObject objPrefab)
    {
        Vector3 finalPos = GetNearestPointOnGrid(position);

        if (IsPositionFree(finalPos))
        {
            grid.Add(finalPos, Instantiate(objPrefab, finalPos, rotation));
            return true;
        }

        return false;

    }

    public static bool RemoveObject(Vector3 ClickPoint, out GameObject objectInGrid)
    {
        Vector3 nearestPoint = GetNearestPointOnGrid(ClickPoint);

        if (grid.TryGetValue(nearestPoint, out objectInGrid))
        {
            return grid.Remove(nearestPoint);
        }

        return objectInGrid;
    }

    public static bool IsPositionFree(Vector3 position)
    {
        Debug.Log(!grid.ContainsKey(GetNearestPointOnGrid(position)));
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
    private static void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (float x = 0; x < 40; x+= BaseGridSize)
        {
            for (float z = 0; z < 40; z+= BaseGridSize)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
