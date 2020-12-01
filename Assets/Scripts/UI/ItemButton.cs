using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private Button button;
    private InventoryManager inventoryManager;
    private int pos;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        button = GetComponent<Button>();
        pos = transform.parent.GetSiblingIndex();
        button.onClick.AddListener(
            delegate 
            { 
                inventoryManager.ClickButtonInventory(pos); 
            });
    }
}
