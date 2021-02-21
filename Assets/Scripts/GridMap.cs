using System.Collections.Generic;
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

    public Dictionary<GameObject, Vector3> grid;
    public Transform groundTransform = null;
    

    void Start()
    {
        grid = new Dictionary<GameObject, Vector3>();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if(!Nature.instance.IsInsideGrid(position)) return default;

        int xCount = Mathf.RoundToInt(position.x / BaseGridSize);
        int zCount = Mathf.RoundToInt(position.z / BaseGridSize);

        Vector3 result = new Vector3(xCount * BaseGridSize, 0, zCount * BaseGridSize);

        return result;
    }

    public Soil GetNearestSoil(Vector3 position)
    {
        if (!Nature.instance.IsValidPosition(position)) return default;

        int xCount = Mathf.RoundToInt(position.x / BaseGridSize);
        int zCount = Mathf.RoundToInt(position.z / BaseGridSize);

        Vector3 resultPos = new Vector3(xCount * BaseGridSize, 0, zCount * BaseGridSize);

        if (Nature.instance.soilGrid.ContainsKey(resultPos))
        {
            return Nature.instance.soilGrid[resultPos];
        }
        else
        {
            Debug.LogWarning($"O solo em {resultPos} não existe.");
            return null;
        }
    }

    public void PutObjectOngrid(Vector3 position, Quaternion rotation, GameObject objPrefab)
    {
        Vector3 relatedSoil = GetNearestPointOnGrid(position);
        Debug.Log(relatedSoil);
        grid.Add(Instantiate(objPrefab, position, rotation), relatedSoil);
    }   


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (float x = 0; x < xSize * BaseGridSize; x+= BaseGridSize)
        {
            for (float z = 0; z < zSize * BaseGridSize; z+= BaseGridSize)
            {
                Vector3 point = new Vector3(x, 1, z);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
