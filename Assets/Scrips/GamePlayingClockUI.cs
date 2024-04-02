using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.Utils;
using TMPro;
using UnityEngine;

public class GamePlayingClockUI : BaseMono
{
    [SerializeField] private TextMeshProUGUI timer;

    public override void Tick()
    {
        base.Tick();
        timer.gameObject.SetActive(!KitchenGameManager.Instance.IsGameOver());
        timer.text = TimeUtils.FormatTimeSpan(KitchenGameManager.Instance.GetGamePlayingTimer());
    }
}
