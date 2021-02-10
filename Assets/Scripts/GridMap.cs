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

    public Dictionary<GameObject, Vector3> grid;
    public Transform groundTransform = null;
    //public static Action<Vector3> OnNewSoil;

    void Start()
    {
        grid = new Dictionary<GameObject, Vector3>();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if(!Nature.instance.ValidPosition(position)) return default;

        int xCount = Mathf.RoundToInt(position.x / BaseGridSize);
        int zCount = Mathf.RoundToInt(position.z / BaseGridSize);

        Vector3 result = new Vector3(xCount * BaseGridSize, 0, zCount * BaseGridSize);

        return result;
    }

    public void PutObjectOngrid(Vector3 position, Quaternion rotation, GameObject objPrefab)
    {
        Vector3 relatedSoil = GetNearestPointOnGrid(position);

        grid.Add(Instantiate(objPrefab, position, rotation), relatedSoil);
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
