using System;
using UnityEngine;

public class Soil
{
    public float availableNutrients;
    public float nutrientGenerationRate;
    public float maxNutrients;
    //[SerializeField] private float acidity;
    //[SerializeField] private float moisture;

    public Soil(float _availableNutrients, float _nutrientGenerationRate, float _maxNutrients)
    {
        availableNutrients = _availableNutrients;
        nutrientGenerationRate = _nutrientGenerationRate;
        maxNutrients = _maxNutrients;
        Nature.GenerateNutrients += GenerateNutrients;
    }

    private void OnDestroy()
    {
        Nature.GenerateNutrients -= GenerateNutrients;
    }

    public void GiveNutrients(float consumeValue)
    {
        availableNutrients = Mathf.Clamp(availableNutrients - consumeValue, 0, maxNutrients);
    }

    public void GenerateNutrients()
    {
        availableNutrients = Mathf.Clamp(availableNutrients + nutrientGenerationRate, 0, maxNutrients);
 //       Debug.Log(" Gerei nutrientes" + availableNutrients);
    }

    public void AddNutrients(float nutrients)
    {
//        Debug.Log("Recebi nutrients: " + availableNutrients);
        availableNutrients = Mathf.Clamp(availableNutrients + nutrients, 0, maxNutrients);
    }

    public void RemoveNutrients(float nutrients)
    {
//        Debug.Log("Recebi nutrients: " + availableNutrients);
        availableNutrients = Mathf.Clamp(availableNutrients - nutrients, 0, maxNutrients);
    }
}
