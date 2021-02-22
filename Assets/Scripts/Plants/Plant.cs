using UnityEngine;

public class Plant : MonoBehaviour
{
    public TimeController time;

    public float health = 100f;
    public float water = 0f;
    public float profit = 0f;
    public float luminosity = 0f;
    
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
    private Canvas canvas;
    private float _timeSlice;
    private GridMap plantsGridMap;
    private float actualConsumptionLoopTime = 0f;
    private float baseConsumptionLoopTime = 2f;
    private float seed = 0f;

    public float size = 0;
    public float oldSizeAux = 0f;
    public float growthVelocity = 0;
    public float maxSize = 0;
    public float maxHeight = 0;
    public float plantingDay = 0f;
    public float productionPerSecond = 0f;
    public float nutrients;
   

    private void Awake()
    {
        time = GameObject.Find("TimeController").GetComponent<TimeController>();
        plantsGridMap = GridMap.instance;
        nature = Nature.instance;
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        size = plantObject.size;
        growthVelocity = plantObject.growthVelocity;
        maxSize = plantObject.maxSize;
        maxHeight = plantObject.maxHeight;
        plantingDay = time.days;
        productionPerSecond = plantObject.productionPerSecond;
        nutrientConsumptionRate = plantObject.nutrients;
    }

    void FixedUpdate()
    {
        actualConsumptionLoopTime += Time.deltaTime;
        Growth();
        //soil generates nutrients in every cycle
        if (actualConsumptionLoopTime  >= baseConsumptionLoopTime && health >= 0f)
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
                    Debug.Log(nature.GetAvailableNutrients(transform.position) + " and consumed " + _timeSlice * nutrientConsumptionRate + "and the sum is" + (nature.GetAvailableNutrients(transform.position) - _timeSlice * nutrientConsumptionRate));
                    ProduceOrganicMatter();
                }
                else
                {
                    nature.ConsumeNutrients(transform.position, Time.deltaTime * nutrientConsumptionRate);
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

    public void ProduceOrganicMatter()
    {
        seed += productionPerSecond;

        if(seed > 1f)
        {
            float difference = seed - 1f;
            ShopManager.instance.moneyAmount += (int) seed;
            seed = difference;
        }
    }
    
    public void Growth()
    {
        if(health > 0)
        {
            if (size < maxSize) size = Mathf.Round((maxSize/(maxHeight/growthVelocity))*(time.days - plantingDay));
            if (oldSizeAux == 0) oldSizeAux = size;

            if (size > oldSizeAux)
            {
                gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                oldSizeAux = size;
            }
        }
    }
}
