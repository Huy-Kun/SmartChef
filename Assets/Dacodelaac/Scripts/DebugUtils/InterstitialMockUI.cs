using Dacodelaac.Common;
using Dacodelaac.Core;
using UnityEngine;
using Event = Dacodelaac.Events.Event;

namespace Dacodelaac.DebugUtils
{
    public class InterstitialMockUI : BaseMono
    {
        [SerializeField] RandomImage image;
        [SerializeField] Event adsCloseInterstitialMockEvent;
        
        public override void Initialize()
        {
            base.Initialize();
            image.gameObject.SetActive(false);
        }

        public void OnShow()
        {
            image.gameObject.SetActive(true);
        }

        public void OnClose()
        {
            image.gameObject.SetActive(false);
            adsCloseInterstitialMockEvent.Raise();
        }
    }
}