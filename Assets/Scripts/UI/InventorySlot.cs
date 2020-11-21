using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    private TMP_Text quantityText;
    private Image image;
    private Button button;

    void Start()
    {
        image = transform.GetChild(0).GetComponentInChildren<Image>();
        quantityText = transform.GetChild(1).GetComponent<TMP_Text>();
        button = GetComponentInChildren<Button>();
    }

    public void AddItem(Item newItem)
    {
        if (newItem.quantity > 1)
        {
            quantityText.text = newItem.quantity.ToString();
        }
        else
        {
            ClearSlot();
        }
        item = newItem;
        image.sprite = item.inventoryItem.sprite;
        image.enabled = true;
        button.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        quantityText.text = "";
        image.sprite = null;
        image.enabled = false;
        button.interactable = false;
    }
}
