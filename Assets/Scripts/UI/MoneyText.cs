using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    private TMP_Text moneyText;
    private ShopManager shopManager;

    void Start()
    {
        moneyText = GetComponent<TMP_Text>();
        shopManager = ShopManager.instance;
    }

    void FixedUpdate()
    {
        moneyText.text = shopManager.GetMoneyAmount().ToString() + " seeds";
    }
}
