using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryManager.InventoryType inventoryType = default;

    public int id = 0;
    public Sprite sprite = null;
    public int price = 0;

    private void Awake()
    {
        sprite = GetComponent<Sprite>();

        if (inventoryType == InventoryManager.InventoryType.Plant)
        {
            id = GetComponent<Plant>().GetId();
        }
    }
}
