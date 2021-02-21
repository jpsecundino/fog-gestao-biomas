using UnityEngine;
using TMPro;

public class InfoText : MonoBehaviour
{
    [SerializeField] private PlantObject plantObject = null;

    private TMP_Text[] text = new TMP_Text[12];
    private Plant plant;

    private void Start()
    {
        plant = GetComponentInParent<Plant>();
        text = GetComponentsInChildren<TMP_Text>();
        text[0].text = plantObject.id.ToString();
        text[1].text = plantObject.name.ToString();
        text[4].text = plantObject.size.ToString();
        text[6].text = plantObject.maxSize.ToString();
        text[7].text = plantObject.fruits.ToString();
    }

    private void FixedUpdate()
    {
        text[2].text = plant.water.ToString();
        text[3].text = plant.nutrients.ToString();
        text[5].text = plant.growthVelocity.ToString();
        text[8].text = plant.productionPerSecond.ToString();
        text[9].text = plant.profit.ToString();
        text[10].text = plant.luminosity.ToString();
        text[11].text = plant.health.ToString();
    }
}
