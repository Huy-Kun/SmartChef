using Dacodelaac.DataStorage;
using Dacodelaac.Events;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using UnityEngine;
#if NOTIFICATION
using Dacodelaac.Notifications;
#endif

namespace Dacodelaac.Core
{
    public class Launcher : BaseLauncher
    {
        [SerializeField] private LoadingScreenEvent loadingScreenEvent;
        private void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            
            base.Initialize();

            loadingScreenEvent.Raise(new LoadingScreenData()
            {
                IsLaunching = true,
                IsLoadScene = false,
                Scene = "HomeScene",
                MinLoadTime = 4,
                LaunchCondition = null
            }); 
        }
    }
}