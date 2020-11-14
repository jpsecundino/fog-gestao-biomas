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

    private Canvas canvas;
    private float _timeSlice;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    internal void Consume()
    {

        _timeSlice = Time.deltaTime;

        if (Nature.GetAvailableNutrients(transform.position) > 0)
        {
            if(Nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate >= 0)
            {
                Nature.ConsumeNutrients(transform.position, Time.deltaTime * nutrientConsumptionRate);
                HealthControl((_timeSlice * nutrientConsumptionRate) / deathRate);
            }
            else
            {
                Nature.ConsumeNutrients(transform.position, Time.deltaTime * nutrientConsumptionRate);
                Debug.Log(Nature.GetAvailableNutrients(transform.position) + " and consumed " + _timeSlice * nutrientConsumptionRate + "and the sum is" + (Nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate));
                HealthControl((Nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate) / deathRate);
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
}
