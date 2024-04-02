using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : BaseMono
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenManagerOnOnStateChanged;
        Hide();
    }

    public override void Tick()
    {
        base.Tick();
        countdownText.text = Math.Ceiling(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void KitchenManagerOnOnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStart())
            Show();
        else Hide();
    }

    void Show()
    {
        countdownText.gameObject.SetActive(true);
    }

    void Hide()
    {
        countdownText.gameObject.SetActive(false);
    }
}
