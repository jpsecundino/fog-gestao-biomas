using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string plantName;
    public float potential;
    
    public float health = 100f;
    [SerializeField] private float maxhealth = 100f;
    public float nutrientConsumptionRate = 10f;
    
    [Range(0f,100f)]
    public float deathRate;

    [SerializeField] private float minAcidity;
    [SerializeField] private float maxAcidity;

    [SerializeField] private float minMoisture;
    [SerializeField] private float maxMoisture;

    public PlantObject plantObject;

    private Nature nature = null;
    private Canvas canvas;
    private float _timeSlice;

    private void Start()
    {
        nature = Nature.instance;
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    internal void Consume()
    {

        _timeSlice = Time.deltaTime;

        if (nature.GetAvailableNutrients(transform.position) > 0)
        {
            if(nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate >= 0)
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

    private void HealthControl(float value)
    {

        health = Mathf.Clamp(health + value, 0, maxhealth);

        if(health == 0)
        {
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
