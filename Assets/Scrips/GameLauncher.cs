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
        [SerializeField] private Event loadingEventDoneEvent;
        [SerializeField] private FloatVariable timeScale;
        
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
                MinLoadTime = 2,
                LaunchCondition = null
            }); 
        }
        
        public void OnPauseGame(bool pause)
        {
            Time.timeScale = pause ? 0f : timeScale.Value;
        }
    }
}