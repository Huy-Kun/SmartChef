using Dacodelaac.Common;
using Dacodelaac.Core;
using Dacodelaac.Events;
using UnityEngine;
using Event = Dacodelaac.Events.Event;

namespace Dacodelaac.DebugUtils
{
    public class RewardMockUI : BaseMono
    {
        [SerializeField] RandomImage image;
        [SerializeField] Event adsCompleteRewardMockEvent;
        [SerializeField] Event adsCloseRewardMockEvent;

        public override void Initialize()
        {
            base.Initialize();
            image.gameObject.SetActive(false);
        }

        public void OnShow()
        {
            image.gameObject.SetActive(true);
        }
        
        public void OnComplete()
        {
            image.gameObject.SetActive(false);
            adsCompleteRewardMockEvent.Raise();
            adsCloseRewardMockEvent.Raise();
        }

        public void OnCancel()
        {
            image.gameObject.SetActive(false);
            adsCloseRewardMockEvent.Raise();
        }
    }
}