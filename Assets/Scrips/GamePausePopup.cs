using System;
using Dacodelaac.Core;
using Dacodelaac.Events;
using Dacodelaac.UI.Popups;
using UnityEngine;

public class GamePausePopup : BasePopup
{
    [SerializeField] private LoadingScreenEvent loadingScreenEvent;
    [SerializeField] private BooleanEvent pauseGameEvent;
    
    protected override void BeforeShow(object data = null)
    {
        base.BeforeShow(data);
        pauseGameEvent.Raise(true);
    }

    protected override void AfterDismissed()
    {
        base.AfterDismissed();
        pauseGameEvent.Raise(false);
    }
    public override void Close()
    {
        Controller.Dismiss(this, false);
    }

    public void OnQuitGame()
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