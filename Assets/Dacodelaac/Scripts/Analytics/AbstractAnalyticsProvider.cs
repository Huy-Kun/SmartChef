using System.Collections.Generic;
using Dacodelaac.Core;
using JetBrains.Annotations;

namespace Dacodelaac.Analytics
{
    public abstract class AbstractAnalyticsProvider : BaseSO
    {
        public abstract void Initialize(System.Action onCompleted);
        public abstract void LogEvent(string eventName, Dictionary<string, object> param = null);

        #region Level
        public abstract void OnLevelStart(Dictionary<string, object> param = null);
        public abstract void OnLevelCompleted(Dictionary<string, object> param = null);
        public abstract void OnLevelFailed(Dictionary<string, object> param = null);
        public abstract void OnLevelRestart(Dictionary<string, object> param = null);
        public abstract void TutorialUpLevel1(Dictionary<string, object> param = null);
        public abstract void TutorialUpLevel2(Dictionary<string, object> param = null);
        public abstract void TutorialUpLevel3(Dictionary<string, object> param = null);
        public abstract void TutorialUseUltimate(Dictionary<string, object> param = null);
        public abstract void KillMiniBoss1(Dictionary<string, object> param = null);
        public abstract void KillMiniBoss2(Dictionary<string, object> param = null);
        public abstract void KillBoss1(Dictionary<string, object> param = null);
        public abstract void KillBoss2(Dictionary<string, object> param = null);
        public abstract void LoseInFirstGame(Dictionary<string, object> param = null);
        public abstract void PlayChapter2(Dictionary<string, object> param = null);
        public abstract void WinChapter2(Dictionary<string, object> param = null);

        #endregion

        #region GameSpecifics
        public abstract void ClickDailyReward(Dictionary<string, object> param = null);
        public abstract void ClaimDailyReward(Dictionary<string, object> param = null);
        public abstract void OpenRareChestFirstTime(Dictionary<string, object> param = null);
        public abstract void OpenGoldChestFirstTime(Dictionary<string, object> param = null);
        public abstract void UpgradeHeroFirstTime(Dictionary<string, object> param = null);
        public abstract void ClickBattlePass(Dictionary<string, object> param = null);
        public abstract void TakeQuickAfk();
        public abstract void TakeAllDailyBattlePass();
        public abstract void TakeAllWeekyBattlePass();
        public abstract void UnlockLevelBattlePass();
        public abstract void EvoleHero();
        public abstract void UpgradeHero();
        public abstract void UpgradeArtifact();
        public abstract void TakeDiamond();
        public abstract void TakeCoin();
        public abstract void TakeAllDailyMission();
        public abstract void TakeAllWeeklyMisson();
        public abstract void RollSkill();
        

        #endregion

        #region Ads

        public abstract void OnAdPaid(Dictionary<string, object> param = null);

        public abstract void OnAdRewardedCompleted(
            Dictionary<string, object> param);
        #endregion

        #region IAP
        public abstract void BuyBattlePass(Dictionary<string, object> param = null);
        public abstract void OnPurchaseProduct(Dictionary<string, object> param = null);

        #endregion
    }
}