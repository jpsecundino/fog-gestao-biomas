using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    private InventoryManager inventoryManager;
    private InventorySlot[] slots;
    private List<Item> lista;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryManager.onItemChangedCallback += UpdateUI;
    }

    private void UpdateUI()
    {
        //Debug.Log("Updating Inventory");

        lista = inventoryManager.GetListItems();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < lista.Count)
            {
                slots[i].AddItem(lista[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
