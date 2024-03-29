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
        [SerializeField] private DoubleVariable lastTimeLogin;
        [SerializeField] private ShortDoubleEvent loginEvent;

        bool PlayFirstTime
        {
            get => GameData.Get("play_first_time", false);
            set => GameData.Set("play_first_time", value);
        }


#if NOTIFICATION
        [SerializeField] NotificationManager notification;
#endif

        private bool _adsInitialized;
        private bool _analyticsInitialized;
        private bool _remoteConfigFetched;

        private void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            Input.multiTouchEnabled = false;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

            // adsInitialized = true;
            // analyticsInitialized = true;
            // remoteConfigFetched = true;

#if NOTIFICATION
            notification.Schedule();
#endif
            base.Initialize();

            if (TimeUtils.CurrentDays - TimeUtils.SecondsToDays(lastTimeLogin.Value) >= 1)
                loginEvent.Raise(1);
            lastTimeLogin.Value = TimeUtils.CurrentSeconds;
            
            if (!PlayFirstTime)
            {
                loadingScreenEvent.Raise(new LoadingScreenData()
                {
                    IsLaunching = true,
                    IsLoadScene = false,
                    Scene = "GameScene",
                    MinLoadTime = 4,
                    LaunchCondition = () => _adsInitialized && _analyticsInitialized && _remoteConfigFetched
#if !UNITY_EDITOR
                && IsUserAgreePrivacy
#endif
                });
                return;
            }

            loadingScreenEvent.Raise(new LoadingScreenData()
            {
                IsLaunching = true,
                IsLoadScene = false,
                Scene = "HomeScene",
                MinLoadTime = 4,
                LaunchCondition = () => _adsInitialized && _analyticsInitialized && _remoteConfigFetched
#if !UNITY_EDITOR
                && IsUserAgreePrivacy
#endif
            }); 
        }

       
        public void OnAdsInitialized()
        {
            _adsInitialized = true;
        }

        public void OnAnalyticsInitialized()
        {
            _analyticsInitialized = true;
        }

        public void RemoteConfigFetched()
        {
            _remoteConfigFetched = true;
        }
        protected bool IsUserAgreePrivacy
        {
            get => GameData.Get("isUserAgreePrivacy", false);
            set => GameData.Set("isUserAgreePrivacy", value);
        }
    }
}