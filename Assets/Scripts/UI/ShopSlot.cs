using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    private InventoryItem item;
    private TMP_Text quantityText;
    private Image image;
    private Button button;

    void Start()
    {
        image = transform.GetChild(0).GetComponentInChildren<Image>();
        quantityText = transform.GetChild(1).GetComponent<TMP_Text>();
        button = GetComponentInChildren<Button>();
    }

    public void AddItemInShop(InventoryItem newItem)
    {
        // Quando terminar a pesquisa adicionar uma nova planta
        item = newItem;
        image.sprite = item.sprite;
        quantityText.text = item.price.ToString();
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
