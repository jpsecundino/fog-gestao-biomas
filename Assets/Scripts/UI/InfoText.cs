using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoText : MonoBehaviour
{
    //[SerializeField] private Plant plant;

    private TMP_Text[] text = new TMP_Text[12];
    [SerializeField] private PlantObject plantObject = null;

    private void Start()
    {
        text = GetComponentsInChildren<TMP_Text>();
        //plantObject = GetPlantObject();
        text[0].text = plantObject.id.ToString();
        text[1].text = plantObject.name.ToString();
        text[4].text = plantObject.stagesPerSize.ToString();
        text[6].text = plantObject.maxSize.ToString();
        text[7].text = plantObject.fruits.ToString();
    }

    private void FixedUpdate()
    {
        text[2].text = plantObject.water.ToString();
        text[3].text = plantObject.nutrients.ToString();
        text[5].text = plantObject.GrowthVelocity.ToString();
        text[8].text = plantObject.productonPerSecond.ToString();
        text[9].text = plantObject.profit.ToString();
        text[10].text = plantObject.luminosity.ToString();
        text[11].text = plantObject.health.ToString();
    }
}
