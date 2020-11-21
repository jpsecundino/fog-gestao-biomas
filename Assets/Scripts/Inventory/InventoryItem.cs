using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryManager.InventoryType inventoryType = default;

    public int id = 0;
    public Sprite sprite = null;

    private void Awake()
    {
        sprite = GetComponent<Sprite>();

        if (inventoryType == InventoryManager.InventoryType.Plant)
        {
            id = GetComponent<Plant>().GetId();
        }
        else if (inventoryType == InventoryManager.InventoryType.Seed)
        {
            
        }
    }
}
