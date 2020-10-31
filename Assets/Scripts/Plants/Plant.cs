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

    public Soil soilAttached;

    [SerializeField] private float minAcidity;
    [SerializeField] private float maxAcidity;

    [SerializeField] private float minMoisture;
    [SerializeField] private float maxMoisture;


    internal void Consume()
    {

        float _timeSlice = Time.deltaTime;

        if (soilAttached.availableNutrients > 0)
        {
            if(soilAttached.availableNutrients - _timeSlice * nutrientConsumptionRate >= 0)
            {
                soilAttached.availableNutrients -= Time.deltaTime * nutrientConsumptionRate;
                HealthControl((_timeSlice * nutrientConsumptionRate) / deathRate);
            }
            else
            {
                Debug.Log(soilAttached.availableNutrients + " and consumed " + _timeSlice * nutrientConsumptionRate + "and the sum is" + (soilAttached.availableNutrients - _timeSlice * nutrientConsumptionRate));
                HealthControl((soilAttached.availableNutrients - _timeSlice * nutrientConsumptionRate) / deathRate);
                soilAttached.availableNutrients = 0;
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
}
