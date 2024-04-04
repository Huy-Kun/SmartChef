using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.Events;
using UnityEngine;

public class HomePopups : BaseMono
{
    
    [SerializeField] private LoadingScreenEvent loadingScreenEvent;
    
    public void OnPlay()
    {
        loadingScreenEvent.Raise(new LoadingScreenData()
        {
            IsLaunching = true,
            IsLoadScene = false,
            Scene = "GameScene",
            MinLoadTime = 2,
            LaunchCondition = null
        }); 
    }
}
