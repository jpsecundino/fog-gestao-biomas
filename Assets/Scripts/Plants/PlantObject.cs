using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Inventory/Plant")]
public class PlantObject : ScriptableObject
{
    public Sprite icon = null;
    public int id = 0;
    public string nome = "New name";
    public float water = 0f;
    public float nutrients = 0f;
    public int stagesPerSize = 1;
    public float GrowthVelocity = 0f;
    public float maxSize = 1f;
    public int fruits = 1;
    public float productonPerSecond = 0f;
    public float profit = 0f;
    public float luminosity = 0f;
    public float health = 0f;
}
