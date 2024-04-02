using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using TMPro;
using UnityEngine;

public class GaneOverUI : BaseMono
{
    [SerializeField] private GameObject kitchenComplete;
    [SerializeField] private GameObject gameOverBG;
    [SerializeField] private TextMeshProUGUI ordersDeliveredText;
    [SerializeField] private TextMeshProUGUI ordersFailedText;
    [SerializeField] private TextMeshProUGUI totalPoint;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenManagerOnOnStateChanged;
        Hide();
    }

    private void KitchenManagerOnOnStateChanged(object sender, EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
            Show();
        else Hide();
    }

    void Show()
    {
        kitchenComplete.SetActive(true);
        gameOverBG.SetActive(true);
        ordersDeliveredText.text = DeliveryManager.Instance.GetOrdersDeliveredPoint().ToString();
        ordersFailedText.text = DeliveryManager.Instance.GetOrdersFailedPoint().ToString();
        totalPoint.text = DeliveryManager.Instance.GetTotalPoint().ToString();
    }

    void Hide()
    {
        kitchenComplete.SetActive(false);
        gameOverBG.SetActive(false);
    }
}
