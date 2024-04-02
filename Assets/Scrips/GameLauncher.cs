using Dacodelaac.DataStorage;
using Dacodelaac.Events;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using UnityEngine;
using Event = Dacodelaac.Events.Event;
#if NOTIFICATION
using Dacodelaac.Notifications;
#endif

namespace Dacodelaac.Core
{
    public class GameLauncher : BaseLauncher
    {
        [SerializeField] private LoadingScreenEvent loadingScreenEvent;
        [SerializeField] Event loadingEventDoneEvent;
        private void Start()
        {
            Initialize();
            loadingEventDoneEvent.Raise();
        }

        public override void Initialize()
        {
            
            base.Initialize();

            loadingScreenEvent.Raise(new LoadingScreenData()
            {
                IsLaunching = true,
                IsLoadScene = false,
                Scene = "GameScene",
                MinLoadTime = 4,
                LaunchCondition = null
            }); 
        }
    }
}