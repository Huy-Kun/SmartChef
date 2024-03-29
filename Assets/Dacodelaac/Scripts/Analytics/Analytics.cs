using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.DebugUtils;
using UnityEngine;
using Event = Dacodelaac.Events.Event;

namespace Dacodelaac.Analytics
{
    public class Analytics : BaseMono
    {
        [SerializeField] Event analyticsInitializedEvent;
        [SerializeField] AbstractAnalyticsProvider[] providers;

        int initializedCount;

        public override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
            
            if (providers.Length == 0)
            {
                analyticsInitializedEvent.Raise();
                return;
            }

            initializedCount = 0;

            foreach (var provider in providers)
            {
                provider.Initialize(OnProviderInitialized);
            }
        }
        
        public override void CleanUp()
        {
        }

        void OnProviderInitialized()
        {
            initializedCount++;
            if (initializedCount >= providers.Length)
            {
                Dacoder.Log("Analytics initialized!");
                analyticsInitializedEvent.Raise();
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.LogEvent(eventName, param);
            }
        }

        #region Level

        public void OnLevelStart(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnLevelStart(param);
            }
        }

        public void OnLevelCompleted(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnLevelCompleted(param);
            }
        }

        public void OnLevelFailed(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnLevelFailed(param);
            }
        }

        public void OnLevelRestart(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnLevelRestart(param);
            }
        }
        public void TutorialUpLevel1(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.TutorialUpLevel1(param);
            }
        }
        public void TutorialUpLevel2(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.TutorialUpLevel2(param);
            }
        }

        public void TutorialUpLevel3(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.TutorialUpLevel3(param);
            }
        }
        public void TutorialUseUltimate(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.TutorialUseUltimate(param);
            }
        }
        public void KillMiniBoss1(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.KillMiniBoss1(param);
            }
        }
        public void KillMiniBoss2(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.KillMiniBoss2(param);
            }
        }


        public void KillBoss1(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.KillBoss1(param);
            }
        }
        public void KillBoss2(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.KillBoss2(param);
            }
        }
        public void LoseInFirstGame(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.LoseInFirstGame(param);
            }
        }
        public void PlayChapter2(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.PlayChapter2(param);
            }
        }
        public void WinChapter2(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.WinChapter2(param);
            }
        }
        #endregion

        #region Ads

        public void OnAdPaid(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnAdPaid(param);
            }
        }

        public void OnAdRewardedCompleted(
            Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnAdRewardedCompleted(param);
            }
        }

        #endregion

        #region IAP

        public void OnPurchaseProduct(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OnPurchaseProduct(param);
            }
        }
        public void BuyBattlePass(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.BuyBattlePass(param);
            }
        }

        #endregion
        #region GameSpecifics
        public void ClickDailyReward(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.ClickDailyReward(param);
            }
        }
        public void ClaimDailyReward(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.ClaimDailyReward(param);
            }
        }
        public void OpenGoldChestFirstTime(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OpenGoldChestFirstTime(param);
            }
        }
        public void OpenRareChestFirstTime(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.OpenRareChestFirstTime(param);
            }
        }
        public void UpgradeHeroFirstTime(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.UpgradeHeroFirstTime(param);
            }
        }
        public void ClickBattlePass(Dictionary<string, object> param = null)
        {
            foreach (var provider in providers)
            {
                provider.ClickBattlePass(param);
            }
        }
        
        public  void TakeQuickAfk()
        {
            foreach (var provider in providers)
            {
                provider.TakeQuickAfk();
            }
        }

        public  void TakeAllDailyBattlePass()
        {
            foreach (var provider in providers)
            {
                provider.TakeAllDailyBattlePass();
            }
        }

        public  void TakeAllWeekyBattlePass()
        {
            foreach (var provider in providers)
            {
                provider.TakeAllWeekyBattlePass();
            }
        }

        public  void UnlockLevelBattlePass()
        {
            foreach (var provider in providers)
            {
                provider.UnlockLevelBattlePass();
            }
        }

        public  void EvoleHero()
        {
            foreach (var provider in providers)
            {
                provider.EvoleHero();
            }
        }

        public  void UpgradeHero()
        {
            foreach (var provider in providers)
            {
                provider.UpgradeHero();
            }
        }

        public  void UpgradeArtifact()
        {
            foreach (var provider in providers)
            {
                provider.UpgradeArtifact();
            }
        }

        public  void TakeDiamond()
        {
            foreach (var provider in providers)
            {
                provider.TakeDiamond();
            }
        }

        public  void TakeCoin()
        {
            foreach (var provider in providers)
            {
                provider.TakeCoin();
            }
        }

        public  void TakeAllDailyMission()
        {
            foreach (var provider in providers)
            {
                provider.TakeAllDailyMission();
            }
        }

        public  void TakeAllWeeklyMisson()
        {
            foreach (var provider in providers)
            {
                provider.TakeAllWeeklyMisson();
            }
        }

        public  void RollSkill()
        {
            foreach (var provider in providers)
            {
                provider.RollSkill();
            }
        }

        #endregion
    }
}