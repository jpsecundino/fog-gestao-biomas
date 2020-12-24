using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    private Button button;
    private ShopManager shopManager;

    void Start()
    {
        shopManager = ShopManager.instance;
        button = GetComponent<Button>();
        int pos = transform.parent.GetSiblingIndex();
        button.onClick.AddListener(
            delegate
            {
                shopManager.ClickItemInShop(pos);
            });
    }
}
