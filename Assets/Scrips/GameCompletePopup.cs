using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.Events;
using Dacodelaac.UI.Popups;
using TMPro;
using UnityEngine;

public class GameCompletePopup : BasePopup
{
    [SerializeField] private GameObject kitchenComplete;
    [SerializeField] private GameObject gameOverBG;
    [SerializeField] private TextMeshProUGUI ordersDeliveredText;
    [SerializeField] private TextMeshProUGUI ordersFailedText;
    [SerializeField] private TextMeshProUGUI totalPoint;
    [SerializeField] private LoadingScreenEvent loadingScreenEvent;

    protected override void BeforeShow(object data = null)
    {
        base.BeforeShow(data);
        kitchenComplete.SetActive(true);
        gameOverBG.SetActive(true);
        ordersDeliveredText.text = DeliveryManager.Instance.GetOrdersDeliveredPoint().ToString();
        ordersFailedText.text = DeliveryManager.Instance.GetOrdersFailedPoint().ToString();
        totalPoint.text = DeliveryManager.Instance.GetTotalPoint().ToString();
    }

    public void OnComplete()
    {
        loadingScreenEvent.Raise(new LoadingScreenData()
        {
            IsLaunching = false,
            IsLoadScene = true,
            LaunchCondition = null,
            MinLoadTime = 2f,
            Scene = "HomeScene"
        });
    }
}
