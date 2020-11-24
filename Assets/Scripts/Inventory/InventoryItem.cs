using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryManager.InventoryType inventoryType = default;

    // Refatorar essa parte depois para melhorar o desempenho
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
        else if (inventoryType == InventoryManager.InventoryType.Seed)
        {
            
        }
    }
}
