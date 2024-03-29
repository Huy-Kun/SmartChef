#if ADJUST
using System;
using System.Collections.Generic;
using com.adjust.sdk;
using Dacodelaac.DebugUtils;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using PlayFab.ClientModels;
using SurvivorRoguelike;
using UnityEngine;

namespace Dacodelaac.Analytics
{
    [CreateAssetMenu(menuName = "Analytics/Adjust")]
    public class AdjustAnalytics : AbstractAnalyticsProvider
    {
        [SerializeField] string adjustAppToken = "7zffljai7lhc";
        [Header("Tokens")] 
        [SerializeField] private string[] onStartLevelTokens;
        [SerializeField] private string[] onFailLevelTokens;
        [SerializeField] private string[] onCompletedLevelTokens;
        [SerializeField] private string onLevelRestartToken;
        [SerializeField] private string onTakeQuickAfk;
        [SerializeField] private string onTakeAllDailyBattlePass;
        [SerializeField] private string onTakeAllWeeklyBattlePass;
        [SerializeField] private string onUnlockLevelBattlePass;
        [SerializeField] private string onEvovleChacter;
        [SerializeField] private string onUpgradeHero;
        [SerializeField] private string onUpgradeArtifact;
        [SerializeField] private string onTakeDiamond;
        [SerializeField] private string onTakeCoin;
        [SerializeField] private string onTakeAllDailyMissions;
        [SerializeField] private string onTakeAllWeeklyMissions;
        [SerializeField] private string onRollSkill;
        [SerializeField] private IntegerVariable gameMode;

        public override void Initialize(Action onCompleted)
        {
            var adjustConfig = new AdjustConfig(adjustAppToken,
                AdjustEnvironment.Production, // AdjustEnvironment.Sandbox to test in dashboard
                true
            );
            adjustConfig.setLogLevel(AdjustLogLevel.Error); // AdjustLogLevel.Suppress to disable logs
            adjustConfig.setSendInBackground(true);
            new GameObject("Adjust").AddComponent<Adjust>(); // do not remove or rename
            // Adjust.addSessionCallbackParameter("foo", "bar"); // if requested to set session-level parameters
            //adjustConfig.setAttributionChangedDelegate((adjustAttribution) => {
            //  Debug.LogFormat("Adjust Attribution Callback: ", adjustAttribution.trackerName);
            //});
            
            Adjust.start(adjustConfig);

            onCompleted?.Invoke();
        }

        public override void LogEvent(string eventName,
            Dictionary<string, object> param = null)
        {
            Dacoder.Log("Adjust.LogEvent", eventName,
                param.ToString<string, object>());
#if !UNITY_EDITOR
            Adjust.trackEvent(new AdjustEvent(eventName));
#endif
        }

        public override void OnLevelStart(
            Dictionary<string, object> param = null)
        {
            if(gameMode.Value != (int) GameMode.Normal) return;
            if (param == null) return;
            
            int index = int.Parse(param["index"].ToString()) - 1;
            if (index < onStartLevelTokens.Length)
                LogEvent(onStartLevelTokens[index]);
        }

        public override void OnLevelCompleted(
            Dictionary<string, object> param = null)
        {
            if(gameMode.Value != (int) GameMode.Normal) return;
            if (param == null) return;
            
            int index = int.Parse(param["index"].ToString()) - 1;
            if (index < onCompletedLevelTokens.Length)
                LogEvent(onCompletedLevelTokens[index]);
        }

        public override void OnLevelFailed(
            Dictionary<string, object> param = null)
        {
            if(gameMode.Value != (int) GameMode.Normal) return;
            if (param == null) return;
            
            int index = int.Parse(param["index"].ToString()) - 1;
            if (index < onFailLevelTokens.Length)
            {
                LogEvent(onFailLevelTokens[index]);
            }
        }

        public override void OnLevelRestart(
            Dictionary<string, object> param = null)
        {
            if(gameMode.Value != (int) GameMode.Normal) return;
            
            LogEvent(onLevelRestartToken);
        }

        public override void TutorialUpLevel1(
            Dictionary<string, object> param = null) { }

        public override void TutorialUpLevel2(
            Dictionary<string, object> param = null) { }

        public override void TutorialUpLevel3(
            Dictionary<string, object> param = null) { }

        public override void TutorialUseUltimate(
            Dictionary<string, object> param = null) { }

        public override void KillMiniBoss1(
            Dictionary<string, object> param = null) { }

        public override void KillMiniBoss2(
            Dictionary<string, object> param = null) { }

        public override void KillBoss1(Dictionary<string, object> param = null) { }

        public override void KillBoss2(Dictionary<string, object> param = null) { }

        public override void LoseInFirstGame(
            Dictionary<string, object> param = null) { }

        public override void PlayChapter2(
            Dictionary<string, object> param = null) { }

        public override void WinChapter2(
            Dictionary<string, object> param = null) { }

        public override void ClickDailyReward(
            Dictionary<string, object> param = null) { }

        public override void ClaimDailyReward(
            Dictionary<string, object> param = null) { }

        public override void OpenRareChestFirstTime(
            Dictionary<string, object> param = null) { }

        public override void OpenGoldChestFirstTime(
            Dictionary<string, object> param = null) { }

        public override void UpgradeHeroFirstTime(
            Dictionary<string, object> param = null) { }

        public override void ClickBattlePass(
            Dictionary<string, object> param = null) { }

        public override void TakeQuickAfk()
        {
            LogEvent(onTakeQuickAfk);
        }

        public override void TakeAllDailyBattlePass()
        {
            LogEvent(onTakeAllDailyBattlePass);
        }

        public override void TakeAllWeekyBattlePass()
        {
            LogEvent(onTakeAllWeeklyBattlePass);
        }

        public override void UnlockLevelBattlePass()
        {
            LogEvent(onUnlockLevelBattlePass);
        }

        public override void EvoleHero()
        {
            LogEvent(onEvovleChacter);
        }

        public override void UpgradeHero()
        {
            LogEvent(onUpgradeHero);
        }

        public override void UpgradeArtifact()
        {
            LogEvent(onUpgradeArtifact);
        }

        public override void TakeDiamond()
        {
            LogEvent(onTakeDiamond);
        }

        public override void TakeCoin()
        {
            LogEvent(onTakeCoin);
        }

        public override void TakeAllDailyMission()
        {
            LogEvent(onTakeAllDailyMissions);
        }

        public override void TakeAllWeeklyMisson()
        {
            LogEvent(onTakeAllWeeklyMissions);
        }

        public override void RollSkill()
        {
            LogEvent(onRollSkill);
        }

        public override void OnAdRewardedCompleted(
            Dictionary<string, object> param) { }

        public override void BuyBattlePass(
            Dictionary<string, object> param = null) { }


        public override void OnAdPaid(Dictionary<string, object> param = null)
        {
            Dacoder.Log("Adjust.OnAdPaid", param.ToString<string, object>());
            if (param != null)
            {
                var adRevenue =
                    new AdjustAdRevenue(param["mediation"].ToString());
                adRevenue.setRevenue((double)param["revenue"],
                    param["currency"].ToString());
                adRevenue.setAdRevenueNetwork(param["network"].ToString());
                adRevenue.setAdRevenuePlacement(param["placement"].ToString());
                adRevenue.setAdRevenueUnit(param["unit"].ToString());
                // Debug.Log("adRevenue TEST : " + adRevenue.revenue);
                Adjust.trackAdRevenue(adRevenue);
            }
        }

        public override void OnPurchaseProduct(
            Dictionary<string, object> param = null)
        {
            // logged in prodata
        }
    }
}
#endif