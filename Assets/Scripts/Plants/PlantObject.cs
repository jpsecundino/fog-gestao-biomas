using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Inventory/Plant")]
public class PlantObject : ScriptableObject
{
    public Sprite icon = null;
    public int id = 0;
    public string nome = "New name";

    public float health = 0f;
    public float luminosity = 0f;
    public float water = 0f;
    public float nutrients = 0f;
    public float nutrientsGivenToSoil = 0f;

    public float size = 1f;
    public float growthVelocity = 1f;
    public float maxSize = 1f;
    public float maxHeight = 1f;

    public int fruits = 1;
    public float productionPerSecond = 0f;   
    public float profit = 0f;
        
    public float price = 0f;
    public GameObject plantPrefab;
    public Material objHoverFree;
    public Material objHoverOcc;
}
