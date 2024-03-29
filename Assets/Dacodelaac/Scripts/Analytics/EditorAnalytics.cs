using System;
using System.Collections.Generic;
using Dacodelaac.DebugUtils;
using Dacodelaac.Utils;
using UnityEngine;

namespace Dacodelaac.Analytics
{
    [CreateAssetMenu(menuName = "Analytics/EditorAnalytics")]
    public class EditorAnalytics : AbstractAnalyticsProvider
    {
        public override void Initialize(Action onCompleted)
        {
            onCompleted?.Invoke();
        }

        public override void LogEvent(string eventName, Dictionary<string, object> param = null)
        {
            Dacoder.Log(eventName, param.ToString<string, object>());
        }

        public override void OnLevelStart(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OnLevelCompleted(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OnLevelFailed(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OnLevelRestart(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void RollSkill()
        {
        }

        public override void OnAdPaid(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OnAdRewardedCompleted(Dictionary<string, object> param)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OnPurchaseProduct(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void TutorialUpLevel1(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void TutorialUpLevel2(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void TutorialUpLevel3(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void TutorialUseUltimate(Dictionary<string, object> param = null)
        {
           Dacoder.Log(param.ToString<string, object>());
        }

        public override void KillMiniBoss1(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void KillMiniBoss2(Dictionary<string, object> param = null)
        {
           Dacoder.Log(param.ToString<string, object>());
        }

        public override void KillBoss1(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void KillBoss2(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void LoseInFirstGame(Dictionary<string, object> param = null)
        {
          Dacoder.Log(param.ToString<string, object>());
        }

        public override void PlayChapter2(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void WinChapter2(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void ClickDailyReward(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void ClaimDailyReward(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OpenRareChestFirstTime(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void OpenGoldChestFirstTime(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void UpgradeHeroFirstTime(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }

        public override void ClickBattlePass(Dictionary<string, object> param = null)
        {
           Dacoder.Log(param.ToString<string, object>());
        }

        public override void TakeQuickAfk()
        {
        }

        public override void TakeAllDailyBattlePass()
        {
        }

        public override void TakeAllWeekyBattlePass()
        {
        }

        public override void UnlockLevelBattlePass()
        {
        }

        public override void EvoleHero()
        {
        }

        public override void UpgradeHero()
        {
        }

        public override void UpgradeArtifact()
        {
        }

        public override void TakeDiamond()
        {
        }

        public override void TakeCoin()
        {
        }

        public override void TakeAllDailyMission()
        {
            Dacoder.Log("CLEAR_ALL_DAILY_MISSIONS");
        }

        public override void TakeAllWeeklyMisson()
        {
            Dacoder.Log("CLEAR_ALL_WEEKLY_MISSIONS");
        }

        public override void BuyBattlePass(Dictionary<string, object> param = null)
        {
            Dacoder.Log(param.ToString<string, object>());
        }
    }
}