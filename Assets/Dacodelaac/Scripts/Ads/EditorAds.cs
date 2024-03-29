using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.Ads
{
    public class EditorAds : BaseMono
    {
        [SerializeField] EditorMediation mediation;

        public override void Initialize()
        {
#if !UNITY_EDITOR
            Destroy(gameObject);
#endif
        }
        
        public override void CleanUp()
        {
        }

        public void OnCloseReward()
        {
            mediation.CloseRewarded();
        }

        public void OnCompleteReward()
        {
            mediation.CompleteRewarded();
        }

        public void OnCloseInterstitial()
        {
            Debug.LogError("?");
            mediation.CloseInterstitial();
        }
    }
}