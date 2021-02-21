using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Transform shopItemsParent;

    private ShopManager shopManager;
    private ShopSlot[] slots;
    private List<InventoryItem> lista;

    private void Start()
    {
        slots = shopItemsParent.GetComponentsInChildren<ShopSlot>();
        shopManager = ShopManager.instance;
        shopManager.onItemShopChangedCallback += UpdateUI;
        shopManager.onItemPriceCallback += UpdateButton;
    }

    private void UpdateUI()
    {
        lista = shopManager.GetListItemsShop();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < lista.Count)
            {
                slots[i].AddItemInShop(lista[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    private void UpdateButton()
    {
        lista = shopManager.GetListItemsShop();

        for (int i = 0; i < lista.Count; i++)
        {
            if (shopManager.GetMoneyAmount() >= lista[i].price)
            {
                slots[i].GetComponentInChildren<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponentInChildren<Button>().interactable = false;
            }
        }
    }
}
