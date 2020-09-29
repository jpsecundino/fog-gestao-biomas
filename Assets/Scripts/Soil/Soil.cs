using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{

    public float nutrientGenerationRate = 10f;
    public float maxNutrients = 100f;
    public float availableNutrients = 50f;

    public List<Plant> plants;

    [SerializeField] private float acidity;
    [SerializeField] private float moisture;


    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateNutrients();
        GiveNutrients();
    }

    private void GiveNutrients()
    {
        foreach (Plant p in plants) {
            p.Consume();
        }
    }

    private void GenerateNutrients()
    {
        availableNutrients = Mathf.Clamp(availableNutrients + Time.deltaTime * nutrientGenerationRate, 0, maxNutrients);
    }
}
