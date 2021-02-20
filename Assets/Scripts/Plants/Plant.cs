using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float health = 100f;
    public float water = 0f;
    public float nutrients = 0f;
    public float growthVelocity = 0f;
    public float productionPerSecond = 0f;
    public float profit = 0f;
    public float luminosity = 0f;
    
    //tudo pro scriptable
    private string plantName;
    private float potential;
    public bool isPlaced = false;
    public PlantObject plantObject;
    private float nutrientConsumptionRate = 2f;

    [Range(0f,100f)]
    public float deathRate;
    [SerializeField] private float maxhealth = 100f;
    [SerializeField] private float minAcidity;
    [SerializeField] private float maxAcidity;
    [SerializeField] private float minMoisture;
    [SerializeField] private float maxMoisture;

    private Nature nature = null;
    private PlantPlacer plantPlacer = null;
    private Canvas canvas;
    private float _timeSlice;
    private GridMap plantsGridMap;
    private float actualConsumptionLoopTime = 0;
    private float baseConsumptionLoopTime = 2;

    private void Start()
    {
        plantPlacer = PlantPlacer.instance;
        plantsGridMap = GridMap.instance;
        nature = Nature.instance;
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        actualConsumptionLoopTime = 0;
    }

    void FixedUpdate()
    {

        actualConsumptionLoopTime += Time.deltaTime;

        //soil generates nutrients in every cycle
        if (actualConsumptionLoopTime  >= baseConsumptionLoopTime)
        {
            actualConsumptionLoopTime = 0f;
            Consume();    
        }
    }

    void Consume()
    {
        if (isPlaced)
        {
            _timeSlice = Time.deltaTime;
            
            float _availableNutrients = nature.GetAvailableNutrients(transform.position);

            if (_availableNutrients > 0)
            {
                if (_availableNutrients - _timeSlice * nutrientConsumptionRate >= 0)
                {
                    nature.ConsumeNutrients(transform.position, Time.deltaTime * nutrientConsumptionRate);
                    HealthControl((_timeSlice * nutrientConsumptionRate) / deathRate);
                }
                else
                {
                    nature.ConsumeNutrients(transform.position, Time.deltaTime * nutrientConsumptionRate);
                    Debug.Log(nature.GetAvailableNutrients(transform.position) + " and consumed " + _timeSlice * nutrientConsumptionRate + "and the sum is" + (nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate));
                    HealthControl((nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate) / deathRate);
                    //Nature.GetAvailableNutrients(transform.position) = 0;
                }
            }
            else
            {
                HealthControl(-(_timeSlice * nutrientConsumptionRate) / deathRate);
            }
        }
    }

    private void HealthControl(float value)
    {

        health = Mathf.Clamp(health + value, 0, maxhealth);

        if(health == 0)
        {
            Debug.LogWarning("O solo nao recebeu nutrientes após a morte da planta, pois uma linha de código está comentada");
            //Descomentar essa linha quando o solo estiver terminado
            nature.soilGrid[nature.GetNearestPointOnGrid(transform.position)].AddNutrients(plantObject.nutrientsGivenToSoil);
            Debug.Log("A planta " + plantObject.name + " morreu");
            plantsGridMap.grid.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public int GetId()
    {
        return plantObject.id;
    }

    public PlantObject GetPlantObject()
    {
        return plantObject;
    }
}
