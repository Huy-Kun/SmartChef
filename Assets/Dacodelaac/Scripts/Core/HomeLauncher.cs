using Dacodelaac.Core;
using Dacodelaac.DataStorage;
using Dacodelaac.Events;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using UnityEngine;
using Event = Dacodelaac.Events.Event;

namespace SurvivorRoguelike
{
    public class HomeLauncher : BaseLauncher
    {
        [SerializeField] private BooleanVariable isLoadProcess;
        [SerializeField] Event loadingEventDoneEvent;

        // hardcode both launcher and home launcher use this key
        void Start()
        {
            Initialize();
            isLoadProcess.Value = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            Time.timeScale = 1f;
            loadingEventDoneEvent.Raise();
        }
        

        void OnApplicationQuit()
        {
            GameData.Save();
        }
    }
}