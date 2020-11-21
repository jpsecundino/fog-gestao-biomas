using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private Button button;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        button = GetComponent<Button>();
        int pos = transform.parent.GetSiblingIndex();
        button.onClick.AddListener(
            delegate 
            { 
                inventoryManager.ClickButtonInventory(pos); 
            });
    }
}
