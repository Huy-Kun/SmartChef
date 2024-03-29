#pragma warning disable CS0067
using System;
using Dacodelaac.DebugUtils;
using UnityEngine;

namespace Dacodelaac.Ads
{
    [CreateAssetMenu(menuName = "Ads/Dummy Mediation")]
    public class DummyMediation : AbstractMediation
    {
        public override void Initialize(Action onCompleted)
        {
            onCompleted?.Invoke();
        }

        public override void OnApplicationPaused(bool isPaused)
        {
        }

        public override void OnAdPaid(string source, string id, double value, string network, string placement, string adFormat)
        {
            Dacoder.Log(source, id, value, network, placement, adFormat);
        }
        
        public override event Action OnBannerLoaded;
        public override event Action<string> OnBannerFailedToLoad;
        public override event Action OnBannerOpening;
        public override event Action OnBannerClosed;
        public override event Action OnBannerClicked;
        public override event Action OnBannerLeavingApplication;
        public override event Action<string, string, double, string, string, string> OnBannerAdPaid;

        public override void LoadBanner(BannerPosition bannerPosition)
        {
            OnBannerLoaded?.Invoke();
        }

        public override void ShowBanner()
        {
            OnBannerOpening?.Invoke();
        }

        public override void HideBanner()
        {
        }

        public override event Action OnInterstitialLoaded;
        public override event Action<string> OnInterstitialFailedToLoad;
        public override event Action OnInterstitialShow;
        public override event Action<string> OnInterstitialFailedToShow;
        public override event Action OnInterstitialClicked;
        public override event Action OnInterstitialClosed;
        public override event Action OnInterstitialLeavingApplication;
        public override event Action<string, string, double, string, string, string> OnInterstitialAdPaid;
        public override bool IsInterstitialAvailable => isInterstitialAvailable;

        bool isInterstitialAvailable;
        public override void LoadInterstitial()
        {
            isInterstitialAvailable = true;
            OnInterstitialLoaded?.Invoke();
        }

        public override void ShowInterstitial()
        {
            isInterstitialAvailable = false;
            OnInterstitialShow?.Invoke();
            OnInterstitialClosed?.Invoke();
        }

        public override event Action OnRewardedLoaded;
        public override event Action<string> OnRewardedFailedToLoad;
        public override event Action OnRewardedShow;
        public override event Action<string> OnRewardedFailedToShow;
        public override event Action OnRewardedClicked;
        public override event Action OnRewardedUserEarnedReward;
        public override event Action OnRewardedClosed;
        public override event Action<string, string, double, string, string, string> OnRewardedAdPaid;
        public override bool IsRewardedAvailable => isRewardedAvailable;
        
        bool isRewardedAvailable;
        
        public override void LoadRewarded()
        {
            isRewardedAvailable = true;
            OnRewardedLoaded?.Invoke();
        }

        public override void ShowRewarded()
        {
            isRewardedAvailable = false;
            OnRewardedShow?.Invoke();
            OnRewardedUserEarnedReward?.Invoke();
            OnRewardedAdPaid?.Invoke("dummy", "rewarded", 1234, "editor", "placement", "adFormat");
            OnRewardedClosed?.Invoke();
        }
    }
}